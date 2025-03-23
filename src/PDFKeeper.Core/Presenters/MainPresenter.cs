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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.Commands;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.FileIO.PDF;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Interop;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Rules;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace PDFKeeper.Core.Presenters
{
    public class MainPresenter : PresenterBase<MainViewModel>
    {
        private readonly IntPtr handle;
        private readonly IPdfViewerService pdfViewerService;
        private readonly IFolderBrowserDialogService folderBrowserDialogService;
        private readonly IMessageBoxService messageBoxService;
        private readonly IFolderExplorerService folderExplorerService;
        private readonly IDialogService setTitleDialogService;
        private readonly IDialogService setAuthorDialogService;
        private readonly IDialogService setSubjectDialogService;
        private readonly IDialogService setCategoryDialogService;
        private readonly IDialogService setTaxYearDialogService;
        private readonly IDialogService setDateTimeAddedDialogService;
        private readonly IFileDialogService openFileDialogService;
        private readonly IFileDialogService saveFileDialogService;
        private readonly IPrintDialogService printDialogService;
        private readonly IPrintPreviewDialogService printPreviewDialogService;
        private readonly PrintDocument printDocument;
        private readonly PdfUploader pdfUploader;
        private readonly FileCache fileCache;
        private readonly DirectoryInfo uploadRejectedDirectory;
        private readonly ExecutingAssembly executingAssembly;
        private Document currentDocument;
        private string textToPrint;

        /// <summary>
        /// Initializes a new instance of the MainPresenter class.
        /// </summary>
        /// <param name="handle">
        /// The handle of the view.
        /// </param>
        /// <param name="pdfViewerService">
        /// The PdfViewerService instance.
        /// </param>
        /// <param name="folderBrowserDialogService">
        /// The FolderBrowserDialogService instance.
        /// </param>
        /// <param name="messageBoxService">
        /// The MessageBoxService instance.
        /// </param>
        /// <param name="folderExplorerService">
        /// The FolderExplorerService instance.
        /// </param>
        /// <param name="setTitleDialogService">
        /// The SetTitleDialogService instance.
        /// </param>
        /// <param name="setAuthorDialogService">
        /// The SetAuthorDialogService instance.
        /// </param>
        /// <param name="setSubjectDialogService">
        /// The SetSubjectDialogService instance.
        /// </param>
        /// <param name="setCategoryDialogService">
        /// The SetCategoryDialogService instance.
        /// </param>
        /// <param name="setTaxYearDialogService">
        /// The SetTaxYearDialogService instance.
        /// </param>
        /// <param name="setDateTimeAddedDialogService">
        /// The SetDateTimeAddedDialogService instance.
        /// </param>
        /// <param name="openFileDialogService">
        /// The OpenFileDialogService instance.
        /// </param>
        /// <param name="saveFileDialogService">
        /// The SaveFileDialogService instance.
        /// </param>
        /// <param name="printDialogService">
        /// The PrintDialogService instance.
        /// </param>
        /// <param name="printPreviewDialogService">
        /// The PrintPreviewDialogService instance.
        /// </param>
        public MainPresenter(
            IntPtr handle,
            IPdfViewerService pdfViewerService,
            IFolderBrowserDialogService folderBrowserDialogService,
            IMessageBoxService messageBoxService,
            IFolderExplorerService folderExplorerService,
            IDialogService setTitleDialogService,
            IDialogService setAuthorDialogService,
            IDialogService setSubjectDialogService,
            IDialogService setCategoryDialogService,
            IDialogService setTaxYearDialogService,
            IDialogService setDateTimeAddedDialogService,
            IFileDialogService openFileDialogService,
            IFileDialogService saveFileDialogService,
            IPrintDialogService printDialogService,
            IPrintPreviewDialogService printPreviewDialogService)
        {
            this.handle = handle;
            this.pdfViewerService = pdfViewerService;
            this.folderBrowserDialogService = folderBrowserDialogService;
            this.messageBoxService = messageBoxService;
            this.folderExplorerService = folderExplorerService;
            this.setTitleDialogService = setTitleDialogService;
            this.setAuthorDialogService = setAuthorDialogService;
            this.setSubjectDialogService = setSubjectDialogService;
            this.setCategoryDialogService = setCategoryDialogService;
            this.setTaxYearDialogService = setTaxYearDialogService;
            this.setDateTimeAddedDialogService = setDateTimeAddedDialogService;
            this.openFileDialogService = openFileDialogService;
            this.saveFileDialogService = saveFileDialogService;
            this.printDialogService = printDialogService;
            this.printPreviewDialogService = printPreviewDialogService;
            ViewModel = new MainViewModel();
            printDocument = new PrintDocument();
            pdfUploader = new PdfUploader();
            fileCache = new FileCache();
            uploadRejectedDirectory = new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.UploadRejected);
            executingAssembly = new ExecutingAssembly();
            AddEventHandlers();
        }

        public event EventHandler CheckedDocumentsProcessed;
        public event EventHandler ScrollToEndOfNotesTextRequested;
        public event EventHandler ProgressBarPerformStepRequested;

        /// <summary>
        /// Gets the file cache instance.
        /// </summary>
        public FileCache FileCache => fileCache;

        public void SetInitialState()
        {
            ViewModel.FileAddMenuEnabled = true;
            ViewModel.FileOpenMenuEnabled = false;
            ViewModel.FileSaveMenuEnabled = false;
            ViewModel.FileSaveAsMenuEnabled = false;
            ViewModel.FileBurstMenuEnabled = false;
            ViewModel.FileExtractMenuEnabled = false;
            ViewModel.FileExtractAllAttachmentsMenuEnabled = false;
            ViewModel.FileExtractAllEmbeddedFilesMenuEnabled = false;
            ViewModel.FileCopyPdfToClipboardEnabled = false;
            ViewModel.FilePrintMenuEnabled = false;
            ViewModel.FilePrintPreviewMenuEnabled = false;
            ViewModel.FileExportMenuEnabled = false;
            ViewModel.EditUndoMenuEnabled = false;
            ViewModel.EditCutMenuEnabled = false;
            ViewModel.EditCopyMenuEnabled = false;
            ViewModel.EditPasteMenuEnabled = false;
            ViewModel.EditSelectAllMenuEnabled = false;
            ViewModel.EditRestoreMenuEnabled = false;
            ViewModel.EditAppendDateTimeMenuEnabled = false;
            ViewModel.EditAppendTextMenuEnabled = false;
            ViewModel.EditFlagDocumentMenuEnabled = false;
            ViewModel.DocumentsSelectMenuEnabled = false;
            ViewModel.DocumentsSetTitleMenuEnabled = false;
            ViewModel.DocumentsSetAuthorMenuEnabled = false;
            ViewModel.DocumentsSetSubjectMenuEnabled = false;
            ViewModel.DocumentsSetCategoryMenuEnabled = false;
            ViewModel.DocumentsSetTaxYearMenuEnabled = false;
            ViewModel.DocumentsSetDateTimeAddedMenuEnabled = false;
            ViewModel.DocumentsDeleteMenuEnabled = false;
            ViewModel.ViewSetPreviewPixelDensityMenuEnabled = false;
            ViewModel.ToolsUploadProfilesMenuEnabled = true;
            if (DatabaseSession.PlatformName.Equals(DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                ViewModel.ToolsMoveDatabaseMenuVisible = true;
            }
            else
            {
                ViewModel.ToolsMoveDatabaseMenuVisible = false;
            }
            ViewModel.DocumentsEnabled = true;
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                ViewModel.SearchTermSnippetsVisible =
                    documentRepository.SearchTermSnippetsSupported;
            }
            if (ApplicationPolicy.GetPolicyValue(ApplicationPolicy.PolicyName.SynchronousUpload))
            {
                ViewModel.SyncUploadTimerEnabled = true;
            }
            else
            {
                ViewModel.AsyncUploadTimerEnabled = true;
            }
        }

        /// <summary>
        /// Sets the Paste menu and button enabled state.
        /// </summary>
        /// <param name="enabled">Enabled (true or false)</param>
        public void SetPasteEnabledState(bool enabled)
        {
            ViewModel.EditPasteMenuEnabled = enabled;
        }

        /// <summary>
        /// Adds the window into the clipboard format listener list.
        /// </summary>
        public void AddClipboardFormatListener()
        {
            NativeMethods.AddClipboardFormatListener(handle);
        }

        /// <summary>
        /// Removes the window from the clipboard format listener list.
        /// </summary>
        public void RemoveClipboardFormatListener()
        {
            NativeMethods.RemoveClipboardFormatListener(handle);
        }

        /// <summary>
        /// Opens the PDF for the current document or each selected (checked) document.
        /// </summary>
        /// <param name="showPdfWithDefaultApplication">
        /// Show PDF with default application. (true or false).
        /// </param>
        public void OpenPdfForEachSelectedDocument(bool showPdfWithDefaultApplication)
        {
            if (ViewModel.CheckedDocumentIds.Count > 0)
            {
                var openMaximum = 12;
                var count = 0;
                foreach (int id in ViewModel.CheckedDocumentIds)
                {
                    count += 1;
                    if (count <= openMaximum)
                    {
                        try
                        {
                            Document currentDocument;
                            using (var documentRepository = 
                                DatabaseSession.GetDocumentRepository())
                            {
                                currentDocument = documentRepository.GetDocument(
                                    id,
                                    null,
                                    !fileCache.IsPdfCached(id));
                            }
                            fileCache.AddPdf(currentDocument.Id, currentDocument.Pdf);
                            pdfViewerService.Show(fileCache.GetPdfFile(id).FullName,
                                showPdfWithDefaultApplication);
                        }
                        catch (DatabaseException ex)
                        {
                            messageBoxService.ShowMessage(ResourceHelper.GetString(
                                "DefaultDocumentException", ex.Message, id.ToString()), true);
                        }
                    }
                }
                if (count > openMaximum)
                {
                    messageBoxService.ShowMessage(
                        ResourceHelper.GetString("OpenCheckedDocumentsMaximumReached",
                        openMaximum.ToString(), null), false);
                }
                OnCheckedDocumentsProcessed();
            }
            else
            {
                OpenPdfForCurrentDocument(showPdfWithDefaultApplication);
            }
        }

        public void SaveNotes()
        {
            var rule = new NotesSizeRule(ViewModel.Notes);
            if (!rule.ViolationFound)
            {
                using (var documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    var document = documentRepository.GetDocument(ViewModel.CurrentDocumentId, null);
                    var notesInDatabase = document.Notes;
                    ViewModel.Notes = ViewModel.Notes.Trim();
                    if (document.Notes.Equals(ViewModel.PreviousNotes, StringComparison.Ordinal))
                    {
                        try
                        {
                            OnLongRunningOperationStarted();
                            ViewModel.PreviousNotes = ViewModel.Notes;
                            document.Notes = ViewModel.Notes;
                            documentRepository.UpdateDocument(document);
                            ViewModel.Notes = ViewModel.Notes;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            messageBoxService.ShowMessage(
                                ResourceHelper.GetString(
                                    "DocumentMayHaveBeenDeletedException",
                                    ex.Message,
                                    null),
                                true);
                        }
                        catch (DatabaseException ex)
                        {
                            ViewModel.PreviousNotes = ViewModel.Notes;
                            ViewModel.Notes = notesInDatabase;
                            messageBoxService.ShowMessage(ex.Message, true);
                        }
                        finally
                        {
                            OnLongRunningOperationFinished();
                        }
                    }
                    else
                    {
                        var dataPackage = new DataPackage();
                        dataPackage.SetText(ViewModel.Notes);
                        Clipboard.SetContent(dataPackage);
                        ViewModel.PreviousNotes = ViewModel.Notes;
                        ViewModel.Notes = notesInDatabase;
                        messageBoxService.ShowMessage(Resources.UnableToSaveNotes, true);
                    }
                }
                SetStateForTextBoxSelectedText();
            }
            else
            {
                messageBoxService.ShowMessage(rule.ViolationMessage, true);
            }
        }

        public void PdfOrTextSaveAs()
        {
            var text = GetDocumentDataText(GetFocusedDocumentDataType());
            string filter;
            if (string.IsNullOrEmpty(text))
            {
                filter = Resources.PdfFilter;
            }
            else
            {
                filter = Resources.TextFilter;
            }
            var targetFilePath = saveFileDialogService.ShowDialog(filter, currentDocument.Title);
            if (!string.IsNullOrEmpty(targetFilePath))
            {
                if (string.IsNullOrEmpty(text))
                {
                    fileCache.GetPdfFile(currentDocument.Id).CopyTo(targetFilePath, true);
                }
                else
                {
                    text.WriteToFile(targetFilePath);
                }
            }
        }

        public async void BurstCurrentDocumentPdf()
        {
            var selectedPath = folderBrowserDialogService.ShowDialog(Resources.SelectBurstFolder);
            if (selectedPath.Length > 0)
            {
                var pdfFile = fileCache.GetPdfFile(ViewModel.CurrentDocumentId);
                try
                {
                    await Task.Run(() => pdfFile.Split(
                        new DirectoryInfo(selectedPath))).ConfigureAwait(true);
                }
                catch (UnauthorizedAccessException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
            }
        }

        /// <summary>
        /// Extracts all attached files from the PDF of the selected document.
        /// </summary>
        /// <param name="attachedFilesType">
        /// The type of attached files in the PDF to extract.
        /// </param>
        public async void ExtractAllAttachedFilesFromCurrentDocumentPdf(
            PdfFile.AttachedFilesType attachedFilesType)
        {
            string resource = null;
            switch (attachedFilesType)
            {
                case PdfFile.AttachedFilesType.Attachment:
                    resource = Resources.ExtractAttachments;
                    break;
                case PdfFile.AttachedFilesType.EmbeddedFile:
                    resource = Resources.ExtractEmbeddedFiles;
                    break;
            }
            try
            {
                var pdfFile = fileCache.GetPdfFile(ViewModel.CurrentDocumentId);
                switch (messageBoxService.ShowQuestion(resource, true))
                {
                    case 6:
                        var zipFilePath = saveFileDialogService.ShowDialog(
                            Resources.ZipFilter,
                            currentDocument.Title);
                        if (!string.IsNullOrEmpty(zipFilePath))
                        {
                            await Task.Run(() => pdfFile.ExtractAllAttachedFiles(
                                attachedFilesType,
                                new FileInfo(zipFilePath))).ConfigureAwait(true);
                        }
                        break;
                    case 7:
                        var selectedPath = folderBrowserDialogService.ShowDialog(
                            Resources.SelectExtractFolder);
                        if (selectedPath.Length > 0)
                        {
                            await Task.Run(() => pdfFile.ExtractAllAttachedFiles(
                                attachedFilesType,
                                new DirectoryInfo(selectedPath))).ConfigureAwait(true);
                        }
                        break;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        public void CopyCurrentDocumentPdfToClipboard()
        {
            var pdfFile = fileCache.GetPdfFile(ViewModel.CurrentDocumentId);
            pdfFile.CopyToClipboard();
        }

        /// <summary>
        /// Provides document data text printing.
        /// </summary>
        /// <param name="usePrintPreviewDialog">Use Print Preview Dialog. (true or false)</param>
        /// <param name="printPreviewDialogSize">The size of the Print Preview dialog.</param>
        public void PrintDocumentDataText(bool usePrintPreviewDialog, Size printPreviewDialogSize)
        {
            textToPrint = GetDocumentDataText(GetFocusedDocumentDataType());
            if (usePrintPreviewDialog)
            {
                printPreviewDialogService.ShowDialog(printDocument, printPreviewDialogSize);
            }
            else
            {
                if (printDialogService.ShowDialog(printDocument).Equals(1))
                {
                    printDocument.Print();
                }
            }
        }

        public void ExportEachSelectedDocument()
        {
            var selectedPath = folderBrowserDialogService.ShowDialog(Resources.SelectExportFolder);
            if (selectedPath.Length > 0)
            {
                selectedPath = Path.Combine(selectedPath,
                    string.Concat(executingAssembly.ProductName, "-", Resources.Export, "_",
                    DateTime.Now.ToString("yyyy-MM-dd_HH.mm", CultureInfo.CurrentCulture)));
                Directory.CreateDirectory(selectedPath);
                ProcessEachCheckedDocument(CheckedDocumentAction.Export, selectedPath);
            }
        }

        public void RestoreNotes()
        {
            var originalNotes = ViewModel.PreviousNotes;
            ViewModel.PreviousNotes = originalNotes;
            ViewModel.Notes = ViewModel.PreviousNotes;
        }

        public void AppendDateTimeIntoNotes()
        {
            string notes = null;
            var newLine = Environment.NewLine;
            OnScrollToEndOfNotesTextRequested();
            if (ViewModel.Notes.Length > 0)
            {
                notes = string.Concat(ViewModel.Notes, newLine, newLine);
            }
            ViewModel.Notes = string.Concat(notes, "--- ", DateTime.Now, " (",
                DatabaseSession.UserName, ") ---", newLine);
            OnScrollToEndOfNotesTextRequested();
        }

        public void AppendTextFromFileIntoNotes()
        {
            var textFilePath = openFileDialogService.ShowDialog(Resources.TextFilter, null);
            if (textFilePath.Length > 0)
            {
                var textFile = new FileInfo(textFilePath);
                OnScrollToEndOfNotesTextRequested();
                ViewModel.Notes = ViewModel.Notes.AppendTextFile(textFile);
                if (messageBoxService.ShowQuestion(ResourceHelper.GetString("DeleteToRecycleBin",
                    textFile.FullName, null), false).Equals(6))
                {
                    textFile.DeleteToRecycleBin();
                }
                OnScrollToEndOfNotesTextRequested();
            }
        }

        public void UpdateCurrentDocumentFlagState()
        {
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                var document = documentRepository.GetDocument(ViewModel.CurrentDocumentId, null);
                document.Flag = Convert.ToInt32(!ViewModel.EditFlagDocumentMenuChecked);
                try
                {
                    OnLongRunningOperationStarted();
                    documentRepository.UpdateDocument(document);
                }
                catch (IndexOutOfRangeException ex)
                {
                    messageBoxService.ShowMessage(ResourceHelper.GetString(
                        "DocumentMayHaveBeenDeletedException", ex.Message, null), true);
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                finally
                {
                    OnLongRunningOperationFinished();
                }
            }
        }

        /// <summary>
        /// Sets a collection of checked document ID's in the CheckedDocumentIds property in the
        /// ViewModel and sets menu state on applicable menu items.
        /// </summary>
        /// <param name="ids">The collection of document ID's.</param>
        public void SetCheckedDocumentIds(Collection<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }
            bool enabled = false;
            ViewModel.CheckedDocumentIds.Clear();
            if (ids.Count > 0)
            {
                enabled = true;
                foreach (int id in ids)
                {
                    ViewModel.CheckedDocumentIds.Add(id);
                }
            }
            ViewModel.DocumentsSetTitleMenuEnabled = enabled;
            ViewModel.DocumentsSetAuthorMenuEnabled = enabled;
            ViewModel.DocumentsSetSubjectMenuEnabled = enabled;
            ViewModel.DocumentsSetCategoryMenuEnabled = enabled;
            ViewModel.DocumentsSetTaxYearMenuEnabled = enabled;
            ViewModel.DocumentsSetDateTimeAddedMenuEnabled = enabled;
            ViewModel.DocumentsDeleteMenuEnabled = enabled;
            ViewModel.FileExportMenuEnabled = enabled;
        }

        public void SetTitleOnEachSelectedDocument()
        {
            var value = setTitleDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetTitle, value);
            }
        }

        public void SetAuthorOnEachSelectedDocument()
        {
            var value = setAuthorDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetAuthor, value);
            }
        }

        public void SetSubjectOnEachSelectedDocument()
        {
            var value = setSubjectDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetSubject, value);
            }
        }

        public void SetCategoryOnEachSelectedDocument()
        {
            var value = setCategoryDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetCategory, value);
            }
        }

        public void SetTaxYearOnEachSelectedDocument()
        {
            var value = setTaxYearDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetTaxYear, value);
            }
        }

        public void SetDateTimeAddedOnEachSelectedDocument()
        {
            var value = setDateTimeAddedDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetDateTimeAdded, value);
            }
        }

        /// <summary>
        /// Deletes each selected document from the database.
        /// </summary>
        /// <param name="compact">
        /// Compact the database following the deletion of all documents when the database platform
        /// is SQLite? (true or false)
        /// </param>
        public void DeleteEachSelectedDocument(bool compact)
        {
            if (messageBoxService.ShowQuestion(Resources.DeleteSelectedDocuments, false).Equals(6))
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.Delete, null);
                if (compact)
                {
                    try
                    {
                        using (var documentRepository = DatabaseSession.GetDocumentRepository())
                        {
                            documentRepository.CompactDatabase();
                        }
                    }
                    catch (NotSupportedException) { }
                    catch (DatabaseException ex)
                    {
                        messageBoxService.ShowMessage(ex.Message, true);
                    }
                }
            }
        }

        public void MoveLocalDatabase()
        {
            var selectedPath = folderBrowserDialogService.ShowDialog(Resources.SelectExportFolder);
            if (selectedPath.Length > 0)
            {
                try
                {
                    OnLongRunningOperationStarted();
                    WaitForUploadToFinish();
                    var databasePath = Path.Combine(selectedPath,
                        string.Concat(executingAssembly.ProductName, ".sqlite"));
                    File.Move(DatabaseSession.LocalDatabasePath, databasePath);
                    DatabaseSession.LocalDatabasePath = databasePath;
                }
                catch (IOException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                finally
                {
                    OnLongRunningOperationFinished();
                }               
            }
        }

        public void GetListOfFlaggedDocuments()
        {
            var findDocumentsParam = new FindDocumentsParam
            {
                FindFlaggedDocumentsChecked = true
            };
            FindDocumentsViewState.FindDocumentsParam = findDocumentsParam;
            GetListOfDocuments(false);
        }

        public void GetListOfAllDocuments()
        {
            var findDocumentsParam = new FindDocumentsParam
            {
                AllDocumentsChecked = true
            };
            FindDocumentsViewState.FindDocumentsParam = findDocumentsParam;
            GetListOfDocuments(false);
        }

        /// <summary>
        /// Reads the selected document from the database and populates the form controls. 
        /// </summary>
        /// <param name="previewPixelDensity">
        /// The pixel density (pixels per inch) of the PDF preview image.
        /// </param>
        public void DocumentSelectionChanged(decimal previewPixelDensity)
        {
            ViewModel.NotesChanged = false;
            if (ViewModel.CurrentDocumentId > 0)
            {
                try
                {
                    OnLongRunningOperationStarted();
                    using (var documentRepository = DatabaseSession.GetDocumentRepository())
                    {
                        if (FindDocumentsViewState.FindDocumentsParam.FindBySearchTermChecked)
                        {
                            currentDocument = documentRepository.GetDocument(
                                ViewModel.CurrentDocumentId,
                                FindDocumentsViewState.FindDocumentsParam.SearchTerm,
                                !fileCache.IsPdfCached(ViewModel.CurrentDocumentId));
                        }
                        else
                        {
                            currentDocument = documentRepository.GetDocument(
                                ViewModel.CurrentDocumentId,
                                null,
                                !fileCache.IsPdfCached(ViewModel.CurrentDocumentId));
                        }
                    }
                    var cachePdfTask = Task.Run(() => fileCache.AddPdf(
                        currentDocument.Id,
                        currentDocument.Pdf));
                    ViewModel.EditFlagDocumentMenuChecked = Convert.ToBoolean(currentDocument.Flag);
                    ViewModel.PreviousNotes = currentDocument.Notes;    // must be set before
                                                                        // ViewModel.Notes
                    ViewModel.Notes = currentDocument.Notes;                   
                    ViewModel.Keywords = currentDocument.Keywords;
                    ViewModel.Text = currentDocument.Text;
                    ViewModel.SearchTermSnippets = currentDocument.SearchTermSnippets;
                    ViewModel.DocumentDataEnabled = true;
                    cachePdfTask.Wait();
                    var pdfFile = fileCache.GetPdfFile(currentDocument.Id);
                    ViewModel.FileExtractMenuEnabled =
                        pdfFile.ContainsAttachments ||
                        pdfFile.ContainsEmbeddedFiles;
                    ViewModel.FileExtractAllAttachmentsMenuEnabled = pdfFile.ContainsAttachments;
                    ViewModel.FileExtractAllEmbeddedFilesMenuEnabled = pdfFile.ContainsEmbeddedFiles;
                    SetPreviewImage(previewPixelDensity);
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                finally
                {
                    OnLongRunningOperationFinished();
                }
            }
            else
            {
                ViewModel.PreviousNotes = null;
                ViewModel.EditFlagDocumentMenuChecked = false;
                ViewModel.DocumentDataEnabled = false;
                ViewModel.FileExtractMenuEnabled = false;
                ViewModel.FileExtractAllAttachmentsMenuEnabled = false;
                ViewModel.FileExtractAllEmbeddedFilesMenuEnabled = false;
                ViewModel.Notes = null;                
                ViewModel.Keywords = null;
                ViewModel.Text = null;
                ViewModel.SearchTermSnippets = null;
                ViewModel.Preview = null;
            }
        }

        /// <summary>
        /// Opens the PDF for the current document.
        /// </summary>
        /// <param name="showPdfWithDefaultApplication">
        /// Show PDF with default application. (true or false).
        /// </param>
        public void OpenPdfForCurrentDocument(bool showPdfWithDefaultApplication)
        {
            pdfViewerService.Show(
                fileCache.GetPdfFile(ViewModel.CurrentDocumentId).FullName,
                showPdfWithDefaultApplication);
        }

        /// <summary>
        /// Sets the PDF preview image for the selected document in the view.
        /// </summary>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) of the PDF preview image.
        /// </param>
        public void SetPreviewImage(decimal pixelDensity)
        {
            OnLongRunningOperationStarted();
            fileCache.CreatePreview(ViewModel.CurrentDocumentId, pixelDensity);
            ViewModel.Preview = fileCache.GetPreview(ViewModel.CurrentDocumentId, pixelDensity);
            OnLongRunningOperationFinished();
        }

        /// <summary>
        /// Sets the state for a text box enter event.
        /// </summary>
        /// <param name="canUndo">Can the user undo the previous operation? (true) or false.</param>
        public void SetTextBoxEnterState(bool canUndo)
        {
            var focusedDocumentDataType = GetFocusedDocumentDataType();
            var documentData = GetDocumentDataText(focusedDocumentDataType);
            var printable = false;
            if (focusedDocumentDataType != DocumentDataType.Keywords)
            {
                if (documentData.Length > 0)
                {
                    printable = true;
                }
            }
            ViewModel.FilePrintMenuEnabled = printable;
            ViewModel.FilePrintPreviewMenuEnabled = printable;
            ViewModel.EditUndoMenuEnabled = false;
            ViewModel.EditCutMenuEnabled = false;
            ViewModel.EditCopyMenuEnabled = false;
            if (documentData.Length > 0)
            {
                ViewModel.EditSelectAllMenuEnabled = true;
            }
            else
            {
                ViewModel.EditSelectAllMenuEnabled = false;
            }
            ViewModel.NotesReadOnly = true;
            if (focusedDocumentDataType.Equals(DocumentDataType.Notes) && !ViewModel.NotesReadOnly)
            {
                ViewModel.EditAppendDateTimeMenuEnabled = true;
                ViewModel.EditAppendTextMenuEnabled = true;
                ViewModel.EditPasteMenuEnabled = Clipboard.GetContent().Contains(
                    StandardDataFormats.Text);
                SetStateForNotesChanged(ViewModel.NotesChanged, canUndo);
            }
            else
            {
                ViewModel.EditAppendDateTimeMenuEnabled = false;
                ViewModel.EditAppendTextMenuEnabled = false;
            }
        }

        /// <summary>
        /// Method that is to be called when the Notes text changes.
        /// </summary>
        /// <param name="canUndo">
        /// Can the user undo the previous operation? (true) or false.
        /// </param>
        public void OnNotesTextChanged(bool canUndo)
        {
            ViewModel.NotesChanged = ViewModel.Notes != ViewModel.PreviousNotes;
            var documentsEnabled = false;
            if (!ViewModel.NotesChanged)
            {
                documentsEnabled = true;
                if (ViewModel.CheckedDocumentIds.Count > 0)
                {
                    ViewModel.DocumentsSetTitleMenuEnabled = true;
                    ViewModel.DocumentsSetAuthorMenuEnabled = true;
                    ViewModel.DocumentsSetSubjectMenuEnabled = true;
                    ViewModel.DocumentsSetCategoryMenuEnabled = true;
                    ViewModel.DocumentsSetTaxYearMenuEnabled = true;
                    ViewModel.DocumentsSetDateTimeAddedMenuEnabled = true;
                    ViewModel.DocumentsDeleteMenuEnabled = true;
                    ViewModel.FileExportMenuEnabled = true;
                }
            }
            // The "if" check is needed to prevent user from having to check / uncheck checkbox in
            // DocumentsDataGridView when Notes length > 0.
            if (ViewModel.NotesFocused)
            {
                ViewModel.DocumentsEnabled = documentsEnabled;
            }
            if (ViewModel.CurrentDocumentId > 0)
            {
                SetStateForNotesChanged(ViewModel.NotesChanged, canUndo);
            }
        }

        public void SetStateForTextBoxSelectedText()
        {
            var focusedDocumentDataType = GetFocusedDocumentDataType();
            var documentData = GetDocumentDataText(focusedDocumentDataType);
            var selectedDocumentData = GetDocumentDataSelectedText(focusedDocumentDataType);
            var readOnly = !focusedDocumentDataType.Equals(DocumentDataType.Notes) || ViewModel.NotesReadOnly;
            if (!string.IsNullOrEmpty(selectedDocumentData))
            {
                ViewModel.EditCutMenuEnabled = !readOnly;
                ViewModel.EditCopyMenuEnabled = true;
            }
            else
            {
                ViewModel.EditCutMenuEnabled = false;
                ViewModel.EditCopyMenuEnabled = false;
            }
            if (!string.IsNullOrEmpty(documentData))
            {
                ViewModel.EditSelectAllMenuEnabled = !documentData.Length.Equals(
                    selectedDocumentData.Length);
            }
            else
            {
                ViewModel.EditSelectAllMenuEnabled = false;
            }
        }
        
        /// <summary>
        /// Sets the state for a text box leave event.
        /// </summary>
        public void SetTextBoxLeaveState()
        {
            ViewModel.FilePrintMenuEnabled = false;
            ViewModel.FilePrintPreviewMenuEnabled = false;
            ViewModel.EditUndoMenuEnabled = false;
            ViewModel.EditCutMenuEnabled = false;
            ViewModel.EditCopyMenuEnabled = false;
            ViewModel.EditPasteMenuEnabled = false;
            ViewModel.EditSelectAllMenuEnabled = false;
            ViewModel.EditRestoreMenuEnabled = false;
            ViewModel.EditAppendDateTimeMenuEnabled = false;
            ViewModel.EditAppendTextMenuEnabled = false;
        }

        public void ExploreUploadRejected()
        {
            folderExplorerService.Explore(uploadRejectedDirectory);
        }
        
        public void CheckForFlaggedDocuments()
        {
            if (!ViewModel.ViewMinimized)
            {
                try
                {
                    int count; 
                    using (var documentRepository = DatabaseSession.GetDocumentRepository())
                    {
                        count = documentRepository.GetListOfFlaggedDocuments().Rows.Count;
                    }
                    if (count > 0)
                    {
                        ViewModel.FlagImageVisible = true;
                    }
                    else
                    {
                        ViewModel.FlagImageVisible = false;
                    }
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
            }            
        }

        public void CheckForDocumentsListChanges()
        {
            if (ViewModel.DocumentsEnabled &&
                ViewModel.CheckedDocumentIds.Count.Equals(0) &&
                !ViewModel.ViewMinimized)
            {
                if (DatabaseSession.DocumentsListHasChanges)
                {
                    GetListOfDocuments(true);
                    DatabaseSession.DocumentsListHasChanges = false;
                }
            }
        }

        public void ExecuteUploadDirectoryMaintenance()
        {
            pdfUploader.ExecuteUploadDirectoryMaintenance();
        }
        
        /// <summary>
        /// Executes the PDF Upload process.
        /// </summary>
        /// <param name="showWaitCursor">Show WaitCursor? (true or false)</param>
        public void ExecuteUpload(bool showWaitCursor)
        {
            var uploader = new PdfUploader();
            if (uploader.PdfFilesReadyToUpload)
            {
                try
                {
                    if (showWaitCursor)
                    {
                        OnLongRunningOperationStarted();
                    }
                    ViewModel.UploadProgressBarVisible = true;
                    uploader.ExecuteUpload();
                }
                catch (ArgumentException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                catch (iText.IO.Exceptions.IOException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                finally
                {
                    ViewModel.UploadProgressBarVisible = false;
                    if (showWaitCursor)
                    {
                        OnLongRunningOperationFinished();
                    }
                }
            }
        }

        public void CheckForRejectedPdfFiles()
        {
            ViewModel.UploadRejectedImageVisible = pdfUploader.UploadRejectedContainsPdfFiles;
        }

        public static void SetDocumentsListHasChanges()
        {
            if (!DatabaseSession.PlatformName.Equals(
                DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                DatabaseSession.DocumentsListHasChanges = true;
            }
        }

        public void WaitForUploadToFinish()
        {
            while (ViewModel.UploadProgressBarVisible)
            {
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// Raises the CheckedDocumentsProcessed event to notify the view that all checked
        /// documents have been processed.
        /// </summary>
        public void OnCheckedDocumentsProcessed()
        {
            CheckedDocumentsProcessed?.Invoke(this, null);
        }

        /// <summary>
        /// Raises the ScrollToEndOfNotesTextRequested event to notify the view that a request was
        /// made to scroll to the end of the Notes text.
        /// </summary>
        public void OnScrollToEndOfNotesTextRequested()
        {
            ScrollToEndOfNotesTextRequested?.Invoke(this, null);
        }

        /// <summary>
        /// Raises the ProgressBarPerformStepRequested event to notify the view that a progress bar
        /// perform step was requested.
        /// </summary>
        public void OnProgressBarPerformStepRequested()
        {
            ProgressBarPerformStepRequested?.Invoke(this, null);
        }

        public void SaveNotesPromptBeforeClosing()
        {
            var choice = messageBoxService.ShowQuestion(Resources.NotesModified, true);
            if (choice.Equals(6))
            {
                SaveNotes();
                CancelViewClosing = false;
            }
            else if (choice.Equals(7))
            {
                RestoreNotes();
                CancelViewClosing = false;
            }
            else
            {
                CancelViewClosing = true;
            }
        }

        private enum CheckedDocumentAction
        {
            SetTitle,
            SetAuthor,
            SetSubject,
            SetCategory,
            SetTaxYear,
            SetDateTimeAdded,
            Delete,
            Export
        }

        private enum DocumentDataType
        {
            None,
            Notes,
            Keywords,
            Text,
            SearchTermSnippets,
        }

        private void AddEventHandlers()
        {
            FindDocumentsViewState.FindDocumentsParamChanged += ApplicationGlobal_FindDocumentsParamChanged;
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void ApplicationGlobal_FindDocumentsParamChanged(object sender, EventArgs e)
        {
            GetListOfDocuments(false);
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (var font = new Font("Lucida Console", 10))
            {
                e.Graphics.MeasureString(textToPrint, font, e.MarginBounds.Size,
                    StringFormat.GenericTypographic, out int charactersOnPage,
                    out int linesPerPage);
                e.Graphics.DrawString(textToPrint, font, Brushes.Black, e.MarginBounds,
                    StringFormat.GenericTypographic);
                textToPrint = textToPrint.Substring(charactersOnPage);
                e.HasMorePages = textToPrint.Length > 0;
            }
        }

        /// <summary>
        /// Sets the menu and tool bar items state based on if Notes text changed and if the Notes
        /// text can be undone. 
        /// </summary>
        /// <param name="notesChanged">Notes text changed (true) or false.</param>
        /// <param name="canUndo">
        /// Can the user undo the previous operation? (true) or false.
        /// </param>
        private void SetStateForNotesChanged(bool notesChanged, bool canUndo)
        {
            ViewModel.FileSaveMenuEnabled = notesChanged;
            ViewModel.DocumentsSelectMenuEnabled = !notesChanged;
            if (notesChanged)
            {
                ViewModel.FileExportMenuEnabled = false;
                ViewModel.DocumentsFindMenuEnabled = false;
                ViewModel.DocumentsSetTitleMenuEnabled = false;
                ViewModel.DocumentsSetAuthorMenuEnabled = false;
                ViewModel.DocumentsSetSubjectMenuEnabled = false;
                ViewModel.DocumentsSetCategoryMenuEnabled = false;
                ViewModel.DocumentsSetTaxYearMenuEnabled = false;
                ViewModel.DocumentsSetDateTimeAddedMenuEnabled = false;
                ViewModel.DocumentsDeleteMenuEnabled = false;
            }
            else
            {
                ViewModel.DocumentsFindMenuEnabled = true;
            }
            ViewModel.EditUndoMenuEnabled = canUndo;
            ViewModel.EditRestoreMenuEnabled = notesChanged;
            ViewModel.EditFlagDocumentMenuEnabled = !notesChanged;
        }

        /// <summary>
        /// Gets a list of documents based on the properties in
        /// ApplicationGlobal.FindDocumentsParam
        /// </summary>
        /// <param name="selectCurrentDocument">
        /// Select the current document after getting documents.
        /// </param>
        private void GetListOfDocuments(bool selectCurrentDocument)
        {
            var findDocumentsParam = FindDocumentsViewState.FindDocumentsParam;
            if (findDocumentsParam != null)
            {
                var currentDocumentId = ViewModel.CurrentDocumentId;
                try
                {
                    OnLongRunningOperationStarted();
                    ViewModel.DocumentsFindMenuEnabled = false;
                    ViewModel.RefreshingDocumentsImageVisible = true;
                    using (var documentRepository = DatabaseSession.GetDocumentRepository())
                    {
                        if (findDocumentsParam.FindBySearchTermChecked)
                        {
                            ViewModel.Documents = documentRepository.GetListOfDocumentsBySearchTerm(
                                findDocumentsParam.SearchTerm);
                        }
                        else if (findDocumentsParam.FindBySelectionsChecked)
                        {
                            ViewModel.Documents = documentRepository.GetListOfDocuments(
                                findDocumentsParam.Author, findDocumentsParam.Subject,
                                findDocumentsParam.Category, findDocumentsParam.TaxYear);
                        }
                        else if (findDocumentsParam.FindByDateAddedChecked)
                        {
                            ViewModel.Documents = documentRepository.GetListOfDocumentsByDateAdded(
                                findDocumentsParam.DateAdded);
                        }
                        else if (findDocumentsParam.FindFlaggedDocumentsChecked)
                        {
                            ViewModel.Documents = documentRepository.GetListOfFlaggedDocuments();
                        }
                        else if (findDocumentsParam.AllDocumentsChecked)
                        {
                            ViewModel.Documents = documentRepository.GetListOfDocuments();
                        }
                    }
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                finally
                {
                    if (selectCurrentDocument)
                    {
                        var columnName = ViewModel.Documents.Columns[0].ColumnName;
                        foreach (DataRow row in ViewModel.Documents.Rows)
                        {
                            var id = Convert.ToInt32(row[columnName].ToString());
                            if (id.Equals(currentDocumentId))
                            {
                                ViewModel.CurrentDocumentId = currentDocumentId;
                            }
                        }
                    }
                    ViewModel.RefreshingDocumentsImageVisible = false;
                    ViewModel.DocumentsFindMenuEnabled = true;
                    OnLongRunningOperationFinished();
                }
            }
        }

        /// <summary>
        /// Processes each checked document.
        /// </summary>
        /// <param name="checkedDocumentAction">The CheckedDocumentAction.</param>
        /// <param name="value">
        /// The value to be applied to each document processeed or
        /// null (Category and Tax Year only). When performing an export, this value must be the
        /// export target directory.
        /// </param>
        private void ProcessEachCheckedDocument(CheckedDocumentAction checkedDocumentAction,
            string value)
        {
            ViewModel.DocumentsProgressBarVisible = true;
            ViewModel.DocumentsProgressBarMinimum = 0;
            ViewModel.DocumentsProgressBarMaximum = ViewModel.CheckedDocumentIds.Count;
            foreach (int id in ViewModel.CheckedDocumentIds)
            {
                try
                {
                    OnLongRunningOperationStarted();
                    using (var documentRepository = DatabaseSession.GetDocumentRepository())
                    {
                        var document = documentRepository.GetDocument(id, null);
                        if (checkedDocumentAction.Equals(CheckedDocumentAction.SetTitle))
                        {
                            document.Title = value;
                            documentRepository.UpdateDocument(document);
                        }
                        else if (checkedDocumentAction.Equals(CheckedDocumentAction.SetAuthor))
                        {
                            document.Author = value;
                            documentRepository.UpdateDocument(document);
                        }
                        else if (checkedDocumentAction.Equals(CheckedDocumentAction.SetSubject))
                        {
                            document.Subject = value;
                            documentRepository.UpdateDocument(document);
                        }
                        else if (checkedDocumentAction.Equals(CheckedDocumentAction.SetCategory))
                        {
                            document.Category = value;
                            documentRepository.UpdateDocument(document);
                        }
                        else if (checkedDocumentAction.Equals(CheckedDocumentAction.SetTaxYear))
                        {
                            document.TaxYear = value;
                            documentRepository.UpdateDocument(document);
                        }
                        else if (checkedDocumentAction.Equals(CheckedDocumentAction.SetDateTimeAdded))
                        {
                            document.Added = value;
                            documentRepository.UpdateDocument(document);
                        }
                        else if (checkedDocumentAction.Equals(CheckedDocumentAction.Delete))
                        {
                            documentRepository.DeleteDocument(id);
                            fileCache.Delete(id);
                        }
                        else if (checkedDocumentAction.Equals(CheckedDocumentAction.Export))
                        {
                            new ExportDocumentCommand(
                                id,
                                new DirectoryInfo(value),
                                fileCache).Execute();
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    messageBoxService.ShowMessage(ResourceHelper.GetString(
                        "DocumentMayHaveBeenDeletedException", ex.Message, id.ToString()), true);
                }
                catch (IndexOutOfRangeException ex)
                {
                    messageBoxService.ShowMessage(ResourceHelper.GetString(
                        "DocumentMayHaveBeenDeletedException", ex.Message, id.ToString()), true);
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ResourceHelper.GetString(
                        "DefaultDocumentException", ex.Message, id.ToString()), true);
                }
                finally
                {
                    OnLongRunningOperationFinished();
                    OnProgressBarPerformStepRequested();
                }
            }
            OnCheckedDocumentsProcessed();
            ViewModel.DocumentsProgressBarVisible = false;
        }

        /// <summary>
        /// Gets the DocumentDataType with focus.
        /// </summary>
        /// <returns>The DocumentDataType enumerated constant.</returns>
        private DocumentDataType GetFocusedDocumentDataType()
        {
            DocumentDataType documentDataType = DocumentDataType.None;
            if (ViewModel.NotesFocused)
            {
                documentDataType = DocumentDataType.Notes;
            }
            else if (ViewModel.KeywordsFocused)
            {
                documentDataType = DocumentDataType.Keywords;
            }
            else if (ViewModel.TextFocused)
            {
                documentDataType = DocumentDataType.Text;
            }
            else if (ViewModel.SearchTermSnippetsFocused)
            {
                documentDataType = DocumentDataType.SearchTermSnippets;
            }
            return documentDataType;
        }

        /// <summary>
        /// Gets the text from the ViewModel for the document data type.
        /// </summary>
        /// <param name="documentDataType">The document data type enumerated constant.</param>
        /// <returns>The text.</returns>
        private string GetDocumentDataText(DocumentDataType documentDataType)
        {
            string result = null;
            if (documentDataType.Equals(DocumentDataType.Notes))
            {
                result = ViewModel.Notes;
            }
            else if (documentDataType.Equals(DocumentDataType.Keywords))
            {
                result = ViewModel.Keywords;
            }
            else if (documentDataType.Equals(DocumentDataType.Text))
            {
                result = ViewModel.Text;
            }
            else if (documentDataType.Equals(DocumentDataType.SearchTermSnippets))
            {
                result = ViewModel.SearchTermSnippets;
            }
            return result;
        }

        /// <summary>
        /// Gets the selected text from the ViewModel for the document data type.
        /// </summary>
        /// <param name="documentDataType">The document data type enumerated constant.</param>
        /// <returns>The selected text.</returns>
        private string GetDocumentDataSelectedText(DocumentDataType documentDataType)
        {
            string result = null;
            if (documentDataType.Equals(DocumentDataType.Notes))
            {
                result = ViewModel.SelectedNotes;
            }
            else if (documentDataType.Equals(DocumentDataType.Keywords))
            {
                result = ViewModel.SelectedKeywords;
            }
            else if (documentDataType.Equals(DocumentDataType.Text))
            {
                result = ViewModel.SelectedText;
            }
            else if (documentDataType.Equals(DocumentDataType.SearchTermSnippets))
            {
                result = ViewModel.SelectedSearchTermSnippets;
            }
            return result;
        }
    }
}
