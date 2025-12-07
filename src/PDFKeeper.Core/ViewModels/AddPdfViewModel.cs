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

using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.FileIO.PDF;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.IO;
using System.Windows.Input;

namespace PDFKeeper.Core.ViewModels
{
    [CLSCompliant(false)]
    public class AddPdfViewModel : ColumnDataListsViewModel, IUploadProfile
    {
        private Document document;
        private IFileDialogService openFileDialogService;
        private IMessageBoxService messageBoxService;
        private IPasswordDialogService passwordDialogService;
        private IRestrictedPdfViewerService restrictedPdfViewerService;
        private UploadProfile uploadProfile;
        private string viewText;
        private string selectedPdf;
        private PdfFile pdfFile;
        private PdfMetadata pdfMetadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPdfViewModel"/> class.
        /// </summary>
        /// <param name="document">
        /// The <see cref="Document"/> instance to associate with the view model. This parameter is
        /// optional and can be <see langword="null"/>.
        /// </param>
        public AddPdfViewModel(Document document = null)
        {
            this.document = document;
            GetServices(ServiceLocator.Services);
            InitializeCommands();            
        }

        public Action OnSelectTitleControl { get; set; }

        /// <summary>
        /// Prompts the user to select the PDF from the file system. If the PDF contains an
        /// <c>Owner</c> password, the user will be prompted to enter it.
        /// </summary>
        public ICommand SelectPdfCommand { get; private set; }

        public ICommand ViewPdfCommand { get; private set; }
        public ICommand SetTitleToPdfFileNameCommand { get; private set; }
        public ICommand GetSubjectsCommand { get; private set; }

        /// <summary>
        /// Adds a copy of the PDF with the specified information metadata applied and the
        /// corresponding XML containing the specified external metadata to the
        /// <c>UploadStaging</c> folder.
        /// <para>
        /// <see cref="ICommand.Execute(bool)"/>: true or false to delete the source PDF to the
        /// Operating System Recycle Bin.
        /// </para>
        /// </summary>
        public ICommand AddPdfCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public UploadProfile UploadProfile
        {
            get => uploadProfile;
            // On a set, trigger all model properties bound to the view to update.
            set => SetProperty(ref uploadProfile, value, nameof(Title));
        }

        public string ViewText
        {
            get => viewText;
            set => SetProperty(ref viewText, value);
        }

        public string SelectedPdf
        {
            get => selectedPdf;
            set => SetProperty(ref selectedPdf, value);
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
            set
            {
                uploadProfile.Title = value;
                OnPropertyChanged();
            }
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
            foreach (var service in serviceProvider.GetServices<IFileDialogService>())
            {
                switch (service.GetType().Name)
                {
                    case "OpenFileDialogService":
                        openFileDialogService = service;
                        break;
                }
            }

            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
            passwordDialogService = serviceProvider.GetService<IPasswordDialogService>();
            restrictedPdfViewerService = serviceProvider.GetService<IRestrictedPdfViewerService>();
        }

        private void InitializeCommands()
        {
            SelectPdfCommand = new RelayCommand(SelectPdf);
            ViewPdfCommand = new RelayCommand(ViewPdf);
            SetTitleToPdfFileNameCommand = new RelayCommand(SetTitleToPdfFileName);
            GetSubjectsCommand = new RelayCommand(GetSubjects);
            AddPdfCommand = new RelayCommand<bool>(AddPdf);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SelectPdf()
        {
            var selectedPdfPath = openFileDialogService.ShowDialog(Resources.PdfFilter);
            if (selectedPdfPath.Length > 0)
            {
                try
                {
                    pdfFile = new PdfFile(new FileInfo(selectedPdfPath));
                    var passwordType = pdfFile.GetPasswordType();
                    if (passwordType.Equals(PdfFile.PasswordType.None))
                    {
                        pdfMetadata = new PdfMetadata(pdfFile);
                        SelectedPdf = pdfFile.FullName;
                        SetUploadProfile();
                        GetCollections();
                    }
                    else if (passwordType.Equals(PdfFile.PasswordType.Owner))
                    {
                        var pdfOwnerPassword = passwordDialogService.ShowDialog();
                        if (pdfOwnerPassword != null)
                        {
                            if (pdfOwnerPassword.Length > 0)
                            {
                                try
                                {
                                    pdfMetadata = new PdfMetadata(pdfFile, pdfOwnerPassword);
                                    SelectedPdf = pdfFile.FullName;
                                    SetUploadProfile();
                                    GetCollections();
                                }
                                catch (ArgumentException)
                                {
                                    messageBoxService.ShowMessage(
                                        Resources.PdfOwnerPasswordIncorrect,
                                        true);
                                    OnCloseView?.Invoke();
                                }
                                pdfOwnerPassword.MakeReadOnly();
                            }
                            else
                            {
                                messageBoxService.ShowMessage(
                                    Resources.PdfOwnerPasswordRequired,
                                    true);
                                OnCloseView?.Invoke();
                            }
                        }
                        else
                        {
                            OnCloseView?.Invoke();
                        }
                    }
                    else if (passwordType.Equals(PdfFile.PasswordType.User))
                    {
                        messageBoxService.ShowMessage(Resources.PdfContainsUserPassword, true);
                        OnCloseView?.Invoke();
                    }
                    else if (passwordType.Equals(PdfFile.PasswordType.Unknown))
                    {
                        messageBoxService.ShowMessage(Resources.PdfInvalid, true);
                        OnCloseView?.Invoke();
                    }
                }
                catch (ArgumentException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                    OnCloseView?.Invoke();
                }
            }
            else
            {
                OnCloseView?.Invoke();
            }
        }

        private void ViewPdf() => restrictedPdfViewerService.Show(SelectedPdf);

        private void SetTitleToPdfFileName()
        {
            OnApplyPendingChanges?.Invoke();
            Title = Path.GetFileNameWithoutExtension(SelectedPdf);
            OnSelectTitleControl?.Invoke();
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

        private void AddPdf(bool deleteSourcePdf = false)
        {
            CancelViewClosing = false;
            OnApplyPendingChanges?.Invoke();
            pdfMetadata.ImportUploadProfile(UploadProfile);
            restrictedPdfViewerService.Close();

            try
            {
                var targetPdfFile = pdfMetadata.Write();
                new Commands.UploadStagingCommand(targetPdfFile).Execute(null);

                if (deleteSourcePdf)
                {
                    new FileInfo(pdfFile.FullName).DeleteToRecycleBin();
                }

                OnCloseViewOKResult?.Invoke();
            }
            catch (NullReferenceException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
                OnCancelCloseView?.Invoke();
            }
            catch (iText.IO.Exceptions.IOException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
                OnCancelCloseView?.Invoke();
            }
        }

        private void Cancel()
        {
            CancelViewClosing = false;

            if (messageBoxService.ShowQuestion(Resources.CancelQuestion).Equals(6))
            {
                restrictedPdfViewerService.Close();
                OnCloseViewCancelResult?.Invoke();
            }
            else
            {
                OnCancelCloseView?.Invoke();
            }
        }

        private void GetCollections()
        {
            try
            {
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

        private void SetUploadProfile()
        {
            if (document != null)
            {
                ViewText = Resources.ReplacePdf;
                pdfMetadata.Id = document.Id;
                UploadProfile = new UploadProfile
                {
                    Title = document.Title,
                    Author = document.Author,
                    Subject = document.Subject,
                    Keywords = document.Keywords,
                    Category = document.Category,
                    TaxYear = document.TaxYear,
                    FlagDocument = Convert.ToBoolean(document.Flag),
                };
            }
            else
            {
                UploadProfile = pdfMetadata.ExportUploadProfile();
            }
        }
    }
}
