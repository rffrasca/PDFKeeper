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
using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Rules;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.ViewModels;
using System;
using System.Linq;

namespace PDFKeeper.Core.Presenters
{
    public class UploadProfileEditorPresenter : PresenterBase<UploadProfileEditorViewModel>
    {
        private IMessageBoxService messageBoxService;
        private readonly string uploadProfileName;
        private readonly UploadProfileManager uploadProfileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProfileEditorPresenter"/> class.
        /// </summary>
        /// <param name="uploadProfileName">
        /// The upload profile name only when editing an existing upload profile.
        /// </param>
        public UploadProfileEditorPresenter(string uploadProfileName = null)
        {
            GetServices(ServicesLocator.Services);
            this.uploadProfileName = uploadProfileName;
            uploadProfileManager = new UploadProfileManager();

            if (uploadProfileName != null)
            {
                ViewModel = new UploadProfileEditorViewModel(uploadProfileName,
                    uploadProfileManager.GetUploadProfile(uploadProfileName));
            }
            else
            {
                ViewModel = new UploadProfileEditorViewModel(uploadProfileName,
                    new UploadProfile());
            }
        }

        public void GetCollections()
        {
            try
            {
                ViewModel.TitleTokens = TitleToken.GetTokens().ToArray();
                ViewModel.Authors = ColumnData.GetAuthors(null, null, null).OrderBy(
                    author => author).ToArray();
                ViewModel.Categories = ColumnData.GetCategories(null, null, null).OrderBy(
                    category => category).ToArray();
                ViewModel.TaxYears = TaxYear.GetYearRange().ToArray();
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        public void GetSubjects()
        {
            try
            {
                var entry = ViewModel.Subject;
                ViewModel.Subjects = ColumnData.GetSubjects(ViewModel.Author, null, null).OrderBy(
                    subject => subject).ToArray();
                ViewModel.Subject = entry;
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        public void SetNameToAuthorAndSubject()
        {
            OnApplyPendingChangesRequested();
            ViewModel.Name = string.Concat(ViewModel.Author, " ", ViewModel.Subject);
        }

        /// <summary>
        /// <para>Saves the upload profile.</para>
        /// <para>The following requirements must be met for the save to be performed:</para>
        /// <br>Name, Title, Author, and Subject cannot be blank.</br>
        /// <br>
        /// Name cannot contain invalid file name characters as defined by the operating system.
        /// </br>
        /// <br>Name cannot already exist when saving a new profile.</br>
        /// </summary>
        public void SaveUploadProfile()
        {
            var error = false;
            CancelViewClosing = false;
            OnApplyPendingChangesRequested();
            var rule = new PdfMetadataRule(ViewModel.UploadProfile);
            if (string.IsNullOrEmpty(ViewModel.Name))
            {
                error = true;
                messageBoxService.ShowMessage(Resources.NameCannotBeBlank, true);
            }
            else if (rule.ViolationFound)
            {
                error = true;
                messageBoxService.ShowMessage(rule.ViolationMessage, true);
            }
            else if (ViewModel.Name.ContainInvalidFileNameChars())
            {
                error = true;
                messageBoxService.ShowMessage(Resources.NameContainsCharsNotAllowed, true);
            }
            else if (uploadProfileManager.GetUploadProfile(ViewModel.Name) != null &&
                uploadProfileName == null)
            {
                error = true;
                messageBoxService.ShowMessage(Resources.UploadProfileExists, true);
            }
            if (error == false)
            {
                uploadProfileManager.SaveUploadProfile(ViewModel.Name, ViewModel.UploadProfile,
                    uploadProfileName);
            }
            else
            {
                OnViewCloseCancelled();
            }
        }

        public void Cancel()
        {
            CancelViewClosing = false;
            if (messageBoxService.ShowQuestion(Resources.CancelQuestion, false).Equals(7))
            {
                OnViewCloseCancelled();
            }
        }

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }
    }
}
