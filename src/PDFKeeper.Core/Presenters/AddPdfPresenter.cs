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

using PDFKeeper.Core.Commands;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.ViewModels;
using System;
using System.IO;
using System.Linq;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.FileIO.PDF;

namespace PDFKeeper.Core.Presenters
{
    public class AddPdfPresenter : PresenterBase<AddPdfViewModel>
    {
        private readonly IFileDialogService openFileDialogService;
        private readonly IPasswordDialogService passwordDialogService;
        private readonly IRestrictedPdfViewerService restrictedPdfViewerService;
        private readonly IMessageBoxService messageBoxService;
        private PdfFile pdfFile;
        private PdfMetadata pdfMetadata;

        /// <summary>
        /// Initializes a new instance of the AddPdfPresenter class.
        /// </summary>
        /// <param name="openFileDialogService">The OpenFileDialogService instance.</param>
        /// <param name="passwordDialogService">The PasswordDialogService instance.</param>
        /// <param name="restrictedPdfViewerService">
        /// The RestrictedPdfViewerService instance.
        /// </param>
        /// <param name="messageBoxService">The MessageBoxService instance.</param>
        public AddPdfPresenter(IFileDialogService openFileDialogService,
            IPasswordDialogService passwordDialogService,
            IRestrictedPdfViewerService restrictedPdfViewerService,
            IMessageBoxService messageBoxService)
        {
            this.openFileDialogService = openFileDialogService;
            this.passwordDialogService = passwordDialogService;
            this.restrictedPdfViewerService = restrictedPdfViewerService;
            this.messageBoxService = messageBoxService;
            ViewModel = new AddPdfViewModel();
        }

        public void GetCollections()
        {
            try
            {
                ViewModel.Authors = ColumnData.GetAuthors(null, null, null).OrderBy(
                    author => author).ToArray();
                ViewModel.Categories = ColumnData.GetCategories(null, null, null).OrderBy(
                    category => category).ToArray();
                ViewModel.TaxYears = TaxYear.GetYearRange().ToArray();
            }
            catch (DatabaseException ex)
            {
                this.messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        /// <summary>
        /// Prompts the user to select the PDF from the file system.If the PDF contains an Owner
        /// password, the user will be prompted to enter it.
        /// </summary>
        public void SelectPdf()
        {
            var selectedPdfPath = openFileDialogService.ShowDialog(Resources.PdfFilter, null);
            if (selectedPdfPath.Length > 0)
            {
                try
                {
                    pdfFile = new PdfFile(new FileInfo(selectedPdfPath));
                    var passwordType = pdfFile.GetPasswordType();
                    if (passwordType.Equals(PdfFile.PasswordType.None))
                    {
                        pdfMetadata = new PdfMetadata(pdfFile, null);
                        ViewModel.SelectedPdf = pdfFile.FullName;
                        ViewModel.UploadProfile = pdfMetadata.ExportUploadProfile();
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
                                    ViewModel.SelectedPdf = pdfFile.FullName;
                                    ViewModel.UploadProfile = pdfMetadata.ExportUploadProfile();
                                }
                                catch (ArgumentException)
                                {
                                    messageBoxService.ShowMessage(Resources.PdfOwnerPasswordIncorrect,
                                        true);
                                    OnViewCloseRequested();
                                }
                                pdfOwnerPassword.MakeReadOnly();
                            }
                            else
                            {
                                messageBoxService.ShowMessage(Resources.PdfOwnerPasswordRequired,
                                    true);
                                OnViewCloseRequested();
                            }
                        }
                        else
                        {
                            OnViewCloseRequested();
                        }
                    }
                    else if (passwordType.Equals(PdfFile.PasswordType.User))
                    {
                        messageBoxService.ShowMessage(Resources.PdfContainsUserPassword, true);
                        OnViewCloseRequested();
                    }
                    else if (passwordType.Equals(PdfFile.PasswordType.Unknown))
                    {
                        messageBoxService.ShowMessage(Resources.PdfInvalid, true);
                        OnViewCloseRequested();
                    }
                }
                catch (ArgumentException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                    OnViewCloseRequested();
                }
            }
            else
            {
                OnViewCloseRequested();
            }
        }

        public void ViewPdf()
        {
            restrictedPdfViewerService.Show(ViewModel.SelectedPdf);
        }

        public void SetTitleToPdfFileName()
        {
            OnApplyPendingChangesRequested();
            ViewModel.Title = Path.GetFileNameWithoutExtension(ViewModel.SelectedPdf);
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
                this.messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        /// <summary>
        /// Adds a copy of the PDF with the specified information metadata applied and the
        /// corresponding XML containing the specified external metadata to the UploadStaging
        /// folder.
        /// </summary>
        /// <param name="deleteSourcePdf">
        /// To delete the source PDF to the Operating System Recycle Bin(true or false).
        /// </param>
        public void AddPdf(bool deleteSourcePdf)
        {
            CancelViewClosing = false;
            OnApplyPendingChangesRequested();
            pdfMetadata.ImportUploadProfile(ViewModel.UploadProfile);
            restrictedPdfViewerService.Close();
            try
            {
                var targetPdfFile = pdfMetadata.Write();
                new UploadStagingCommand(targetPdfFile).Execute();
                if (deleteSourcePdf)
                {
                    new FileInfo(pdfFile.FullName).DeleteToRecycleBin();
                }
            }
            catch (NullReferenceException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
                OnViewCloseCancelled();
            }
            catch (iText.IO.Exceptions.IOException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
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
            else
            {
                restrictedPdfViewerService.Close();
            }
        }
    }
}
