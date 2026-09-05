// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2026 Robert F. Frasca
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
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Interfaces.Services.Upload;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.IO;
using System.Security;

namespace PDFKeeper.Core.ViewModels
{
    /// <summary>
    /// View model for adding a PDF document, including functionality for selecting, viewing,
    /// and adding PDFs with metadata.
    /// </summary>
    [CLSCompliant(false)]
    public sealed class AddPdfViewModel : ColumnDataListsViewModel
    {
        private readonly IMessageBoxService messageBoxService;
        private readonly IPasswordDialogService passwordDialogService;
        private readonly IPdfMetadataService pdfMetadataService;
        private readonly IPdfSecurityService pdfSecurityService;
        private readonly IPdfUploadStagingService pdfUploadStagingService;
        private readonly IPdfViewerService pdfViewerService;
        private readonly IFileDialogService openFileDialogService;
        private Document document;
        private UploadProfile uploadProfile;
        private string viewText;
        private string selectedPdf;
        private PdfMetadataDto pdfMetadataDto;
        private SecureString pdfOwnerPassword;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPdfViewModel"/> class.
        /// </summary>
        /// <param name="keyedServiceResolver">
        /// The <see cref="IKeyedServiceResolver"/> instance.
        /// </param>
        /// <param name="messageBoxService">
        /// The <see cref="IMessageBoxService"/> instance.
        /// </param>
        /// <param name="passwordDialogService">
        /// The <see cref="IPasswordDialogService"/> instance.
        /// </param>
        /// <param name="pdfMetadataService">
        /// The <see cref="IPdfMetadataService"/> instance.
        /// </param>
        /// <param name="pdfSecurityService">
        /// The <see cref="IPdfSecurityService"/> instance.
        /// </param>
        /// <param name="pdfUploadStagingService">
        /// The <see cref="IPdfUploadStagingService"/> instance.
        /// </param>
        /// <param name="pdfViewerService">
        /// The <see cref="IPdfViewerService"/> instance.
        /// </param>
        public AddPdfViewModel(
            IKeyedServiceResolver keyedServiceResolver,
            IMessageBoxService messageBoxService,
            IPasswordDialogService passwordDialogService,
            IPdfMetadataService pdfMetadataService,
            IPdfSecurityService pdfSecurityService,
            IPdfUploadStagingService pdfUploadStagingService,
            IPdfViewerService pdfViewerService)
        {
            if (keyedServiceResolver is null)
            {
                throw new ArgumentNullException(nameof(keyedServiceResolver));
            }

            this.messageBoxService = messageBoxService;
            this.passwordDialogService = passwordDialogService;
            this.pdfMetadataService = pdfMetadataService;
            this.pdfSecurityService = pdfSecurityService;
            this.pdfUploadStagingService = pdfUploadStagingService;
            this.pdfViewerService = pdfViewerService;
            openFileDialogService = keyedServiceResolver.GetRequiredKeyedService<
                IFileDialogService>(FileDialogServiceKey.OpenFile);
            InitializeCommands();            
        }

        public Action OnSelectTitleControl { get; set; }

        /// <summary>
        /// Prompts the user to select the PDF from the file system. If the PDF contains an
        /// <c>Owner</c> password, the user will be prompted to enter it.
        /// <para>
        /// <see cref="IRelayCommand.Execute(string)"/>: Optional initial path for the PDF file to
        /// select.
        /// </para>
        /// </summary>
        public IRelayCommand SelectPdfCommand { get; private set; }

        public IRelayCommand ViewPdfCommand { get; private set; }
        public IRelayCommand SetTitleToPdfFileNameCommand { get; private set; }
        public IRelayCommand GetSubjectsCommand { get; private set; }

        /// <summary>
        /// Adds a copy of the PDF with the specified information metadata applied and the
        /// corresponding XML containing the specified external metadata to the
        /// <c>UploadStaging</c> folder.
        /// <para>
        /// <see cref="IRelayCommand.Execute(bool)"/>: true or false to delete the source PDF to
        /// the Operating System Recycle Bin.
        /// </para>
        /// </summary>
        public IRelayCommand AddPdfCommand { get; private set; }

        public IRelayCommand CancelCommand { get; private set; }

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

        /// <summary>
        /// Sets the <see cref="Document"/> representing the document being replaced.
        /// This method is called only when the PDF in an existing database record is
        /// being replaced.
        /// </summary>
        /// <param name="document">
        /// The <see cref="Document"/> instance that represents the document being replaced.
        /// </param>
        public void SetDocument(Document document)
        {
            this.document = document;
        }

        private void InitializeCommands()
        {
            SelectPdfCommand = new RelayCommand<string>(SelectPdf);
            ViewPdfCommand = new RelayCommand(ViewPdf);
            SetTitleToPdfFileNameCommand = new RelayCommand(SetTitleToPdfFileName);
            GetSubjectsCommand = new RelayCommand(GetSubjects);
            AddPdfCommand = new RelayCommand<bool>(AddPdf);
            CancelCommand = new RelayCommand(Cancel);
        }

        /// <summary>
        /// Opens a dialog to select a PDF file, handles password protection if present, loads PDF
        /// metadata, and updates related properties and collections.
        /// </summary>
        /// <param name="pdfPath">
        /// Optional initial path for the PDF file to select.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the selected PDF file path is invalid.
        /// </exception>
        /// <exception cref="IOException">
        /// Thrown when an I/O error occurs while accessing the PDF file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when access to the PDF file is denied.
        /// </exception>
        private void SelectPdf(string pdfPath = null)
        {
            var selectedPdfPath = !string.IsNullOrEmpty(pdfPath)
                ? pdfPath
                : openFileDialogService.ShowDialog(GetWindowHandle.Invoke(), Resources.PdfFilter);

            if (selectedPdfPath.Length > 0)
            {
                try
                {
                    var passwordType = pdfSecurityService.GetPasswordType(selectedPdfPath);
    
                    switch (passwordType)
                    {
                        case PdfPasswordType.None:
                            pdfMetadataDto = pdfMetadataService.Read(selectedPdfPath);
                            SelectedPdf = selectedPdfPath;
                            SetUploadProfile();
                            GetCollections();
                            break;
                        case PdfPasswordType.Owner:
                            pdfOwnerPassword = passwordDialogService.ShowDialog(
                                GetWindowHandle.Invoke());

                            if (pdfOwnerPassword != null)
                            {
                                if (pdfOwnerPassword.Length > 0)
                                {
                                    pdfMetadataDto = pdfMetadataService.Read(
                                        selectedPdfPath,
                                        pdfOwnerPassword);
                                    SelectedPdf = selectedPdfPath;
                                    SetUploadProfile();
                                    GetCollections();
                                    pdfOwnerPassword.MakeReadOnly();
                                }
                                else
                                {
                                    messageBoxService.ShowMessage(
                                        GetWindowHandle.Invoke(),
                                        Resources.PdfOwnerPasswordRequired,
                                        true);
                                    OnCloseView?.Invoke();
                                }
                            }
                            else
                            {
                                OnCloseView?.Invoke();
                            }

                            break;
                        case PdfPasswordType.User:
                            messageBoxService.ShowMessage(
                                GetWindowHandle.Invoke(),
                                Resources.PdfContainsUserPassword,
                                true);
                            OnCloseView?.Invoke();
                            break;
                        case PdfPasswordType.Unknown:
                            messageBoxService.ShowMessage(
                                GetWindowHandle.Invoke(),
                                Resources.PdfInvalid,
                                true);
                            OnCloseView?.Invoke();
                            break;
                    }
                }
                catch (Exception ex) when (
                    ex is ArgumentException ||
                    ex is IOException ||
                    ex is UnauthorizedAccessException)
                {
                    messageBoxService.ShowMessage(GetWindowHandle.Invoke(), ex.Message, true);
                    OnCloseView?.Invoke();
                }
            }
            else
            {
                OnCloseView?.Invoke();
            }
        }

        private void ViewPdf() => pdfViewerService.OpenPdfInRestrictedViewer(SelectedPdf);

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
                messageBoxService.ShowMessage(GetWindowHandle.Invoke(), ex.Message, true);
            }
        }

        private void AddPdf(bool deleteSourcePdf = false)
        {
            CancelViewClosing = false;
            OnApplyPendingChanges?.Invoke();
            var pdfMetadataDto = new PdfMetadataDto();
            pdfMetadataDto.ToPdfMetadataDto(UploadProfile);
            pdfViewerService.CloseRestrictedViewer();

            try
            {
                var modifiedPdfPath = pdfMetadataService.Write(
                    SelectedPdf,
                    pdfMetadataDto,
                    pdfOwnerPassword);
                pdfUploadStagingService.StagePdf(modifiedPdfPath);

                if (deleteSourcePdf)
                {
                    new FileInfo(SelectedPdf).DeleteToRecycleBin();
                }

                OnCloseViewOKResult?.Invoke();
            }
            catch (Exception ex) when (
                ex is NullReferenceException ||
                ex is iText.IO.Exceptions.IOException)
            {
                messageBoxService.ShowMessage(GetWindowHandle.Invoke(), ex.Message, true);
                OnCloseView?.Invoke();
            }
        }

        private void Cancel()
        {
            CancelViewClosing = false;

            if (messageBoxService.ShowQuestion(
                GetWindowHandle.Invoke(),
                Resources.CancelQuestion) == 6)
            {
                pdfViewerService.CloseRestrictedViewer();
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
                messageBoxService.ShowMessage(GetWindowHandle.Invoke(), ex.Message, true);
            }
        }

        private void SetUploadProfile()
        {
            if (document != null)
            {
                ViewText = Resources.ReplacePdf;
                pdfMetadataDto.Id = document.Id;
                
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
                UploadProfile = pdfMetadataDto.ToUploadProfile();
            }
        }
    }
}
