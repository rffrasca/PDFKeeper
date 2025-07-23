// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2025 Robert F. Frasca
// *
// * This file is part of PDFKeeper.
// *
// * PDFKeeper is free software: you can redistribute it and/or modify it
// * under the terms of the GNU General Public License as published by the
// * Free Software Foundation, either version 3 of the License, or (at your
// * option) any later version.
// *
// * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
// * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// * more details.
// *
// * You should have received a copy of the GNU General Public License along
// * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
// ****************************************************************************

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Rules;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.DataAccess;

namespace PDFKeeper.Core.ViewModels
{
    [CLSCompliant(false)]
    public class UploadProfileEditorViewModel : ColumnDataListsViewModel, IUploadProfile
    {
        private readonly string uploadProfileName;
        private string name;
        private IMessageBoxService messageBoxService;
        private readonly UploadProfileManager uploadProfileManager;
        private UploadProfile uploadProfile;
        private IEnumerable<string> titleTokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProfileEditorViewModel"/> class.
        /// </summary>
        /// <param name="uploadProfileName">
        /// The upload profile name only when editing an existing upload profile.
        /// </param>
        public UploadProfileEditorViewModel(string uploadProfileName = null)
        {
            this.uploadProfileName = uploadProfileName;
            name = uploadProfileName;
            GetServices(ServiceLocator.Services);
            uploadProfileManager = new UploadProfileManager();
            SetUploadProfile();
            InitializeCommands();            
        }

        public ICommand GetCollectionsCommand { get; private set; }
        public ICommand GetSubjectsCommand { get; private set; }
        public ICommand SetNameToAuthorAndSubjectCommand { get; private set; }

        /// <summary>
        /// Saves the upload profile.
        /// <para>The following requirements must be met for the save to be performed:</para>
        /// <list type="bullet">
        /// <c>Name</c>, <c>Title</c>, <c>Author</c>, and <c>Subject</c> cannot be blank.
        /// </list>
        /// <list type="bullet">
        /// <c>Name</c> cannot contain invalid file name characters as defined by the operating system.
        /// </list>
        /// <list type="bullet">
        /// <c>Name</c> cannot already exist when saving a new profile.
        /// </list>
        /// </summary>
        public ICommand SaveUploadProfileCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }
        public UploadProfile UploadProfile => uploadProfile;

        public IEnumerable<string> TitleTokens
        {
            get => titleTokens;
            set => SetProperty(ref titleTokens, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Category
        {
            get => uploadProfile.Category;
            set => uploadProfile.Category = value;
        }

        public string TaxYear
        {
            get => uploadProfile.TaxYear;
            set => uploadProfile.TaxYear = value;
        }

        public bool FlagDocument
        {
            get => uploadProfile.FlagDocument;
            set => uploadProfile.FlagDocument = value;
        }

        public bool OcrPdfTextAndImageDataPages
        {
            get => uploadProfile.OcrPdfTextAndImageDataPages;
            set => uploadProfile.OcrPdfTextAndImageDataPages = value;
        }

        public string Title
        {
            get => uploadProfile.Title;
            set => uploadProfile.Title = value;
        }

        public string Author
        {
            get => uploadProfile.Author;
            set => uploadProfile.Author = value;
        }

        public string Subject
        {
            get => uploadProfile.Subject;
            set
            {
                uploadProfile.Subject = value;
                OnPropertyChanged();
            }
        }

        public string Keywords
        {
            get => uploadProfile.Keywords;
            set => uploadProfile.Keywords = value;
        }

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }

        private void InitializeCommands()
        {
            GetCollectionsCommand = new RelayCommand(GetCollections);
            GetSubjectsCommand = new RelayCommand(GetSubjects);
            SetNameToAuthorAndSubjectCommand = new RelayCommand(SetNameToAuthorAndSubject);
            SaveUploadProfileCommand = new RelayCommand(SaveUploadProfile);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SetUploadProfile()
        {
            if (uploadProfileName == null)
            {
                uploadProfile = new UploadProfile();
            }
            else
            {
                uploadProfile = uploadProfileManager.GetUploadProfile(uploadProfileName);
            }
        }

        private void GetCollections()
        {
            try
            {
                TitleTokens = TitleToken.GetTokens();
                Authors = ColumnData.GetAuthors(null, null, null);
                Categories = ColumnData.GetCategories(null, null, null);
                TaxYears = ColumnData.GetRangeOfTaxYears();
                OnResetBindings?.Invoke();
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        private void GetSubjects()
        {
            try
            {
                var entry = Subject;
                Subjects = ColumnData.GetSubjects(Author, null, null);
                Subject = entry;
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        private void SetNameToAuthorAndSubject()
        {
            OnApplyPendingChanges?.Invoke();
            Name = string.Concat(Author, " ", Subject);
        }

        private void SaveUploadProfile()
        {
            var error = false;
            CancelViewClosing = false;
            OnApplyPendingChanges?.Invoke();
            var rule = new PdfMetadataRule(UploadProfile);

            if (string.IsNullOrEmpty(Name))
            {
                error = true;
                messageBoxService.ShowMessage(Resources.NameCannotBeBlank, true);
            }
            else if (rule.ViolationFound)
            {
                error = true;
                messageBoxService.ShowMessage(rule.ViolationMessage, true);
            }
            else if (Name.ContainInvalidFileNameChars())
            {
                error = true;
                messageBoxService.ShowMessage(Resources.NameContainsCharsNotAllowed, true);
            }
            else if (uploadProfileManager.GetUploadProfile(Name) != null &&
                uploadProfileName is null)
            {
                error = true;
                messageBoxService.ShowMessage(Resources.UploadProfileExists, true);
            }

            if (error.Equals(false))
            {
                uploadProfileManager.SaveUploadProfile(Name, UploadProfile, uploadProfileName);
                OnCloseViewOKResult?.Invoke();
            }
            else
            {
                OnCancelCloseView?.Invoke();
            }
        }

        private void Cancel()
        {
            CancelViewClosing = false;

            if (messageBoxService.ShowQuestion(Resources.CancelQuestion, false).Equals(6))
            {
                OnCloseViewCancelResult?.Invoke();
            }
            else
            {
                OnCancelCloseView?.Invoke();
            }
        }
    }
}
