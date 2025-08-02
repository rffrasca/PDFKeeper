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
using PDFKeeper.Core.Application;
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
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;

namespace PDFKeeper.Core.ViewModels
{
    [CLSCompliant(false)]
    public class MainViewModel : ViewModelBase
    {
        private readonly IntPtr viewHandle;
        private IDialogService addPdfDialogService;
        private IDialogService setTitleDialogService;
        private IDialogService setAuthorDialogService;
        private IDialogService setSubjectDialogService;
        private IDialogService setCategoryDialogService;
        private IDialogService setTaxYearDialogService;
        private IDialogService setDateTimeAddedDialogService;
        private IDialogService setPreviewPixelDensityDialogService;
        private IDialogService optionsDialogService;
        private IDialogService aboutBoxDialogService;
        private IFileDialogService openFileDialogService;
        private IFileDialogService saveFileDialogService;
        private IFolderBrowserDialogService folderBrowserDialogService;
        private IFolderExplorerService folderExplorerService;
        private IMessageBoxService messageBoxService;
        private IPdfViewerService pdfViewerService;
        private IPrintDialogService printDialogService;
        private IPrintPreviewDialogService printPreviewDialogService;
        private readonly FileCache fileCache;
        private bool toolStripVisible;
        private bool statusStripVisible;
        private bool fileAddMenuEnabled;
        private bool fileOpenMenuEnabled;
        private bool fileSaveMenuEnabled;
        private bool fileSaveAsMenuEnabled;
        private bool fileBurstMenuEnabled;
        private bool fileExtractMenuEnabled;
        private bool fileExtractAllAttachmentsMenuEnabled;
        private bool fileExtractAllEmbeddedFilesMenuEnabled;
        private bool fileCopyPdfToClipboardEnabled;
        private bool filePrintMenuEnabled;
        private bool filePrintPreviewMenuEnabled;
        private bool fileExportMenuEnabled;
        private bool editUndoMenuEnabled;
        private bool editCutMenuEnabled;
        private bool editCopyMenuEnabled;
        private bool editPasteMenuEnabled;
        private bool editSelectAllMenuEnabled;
        private bool editRestoreMenuEnabled;
        private bool editAppendDateTimeMenuEnabled;
        private bool editAppendTextMenuEnabled;
        private bool editFlagDocumentMenuEnabled;
        private bool editFlagDocumentMenuChecked;
        private bool documentsFindMenuEnabled;
        private bool documentsSelectMenuEnabled;
        private bool documentsSetTitleMenuEnabled;
        private bool documentsSetAuthorMenuEnabled;
        private bool documentsSetSubjectMenuEnabled;
        private bool documentsSetCategoryMenuEnabled;
        private bool documentsSetTaxYearMenuEnabled;
        private bool documentsSetDateTimeAddedMenuEnabled;
        private bool documentsDeleteMenuEnabled;
        private bool viewSetPreviewPixelDensityMenuEnabled;
        private bool viewToolBarChecked;
        private bool viewStatusBarChecked;
        private bool toolsUploadProfilesMenuEnabled;
        private bool toolsMoveDatabaseMenuVisible;
        private DataTable documents;
        private readonly Collection<int> checkedDocumentIds;
        private bool documentsEnabled;
        private int currentDocumentId;
        private bool documentDataEnabled;
        private string notes;
        private string selectedNotes;
        private bool notesFocused;
        private bool notesReadOnly;
        private string keywords;
        private string selectedKeywords;
        private bool keywordsFocused;
        private string text;
        private string selectedText;
        private bool textFocused;
        private string searchTermSnippets;
        private string selectedSearchTermSnippets;
        private bool searchTermSnippetsFocused;
        private bool searchTermSnippetsVisible;
        private Image preview;
        private bool documentsProgressBarVisible;
        private int documentsProgressBarMinimum;
        private int documentsProgressBarMaximum;
        private bool uploadProgressBarVisible;
        private bool refreshingDocumentsImageVisible;
        private bool flagImageVisible;
        private bool uploadRejectedImageVisible;
        private readonly PrintDocument printDocument;
        private readonly PdfUploader pdfUploader;
        private readonly DirectoryInfo uploadRejectedDirectory;
        private readonly ExecutingAssembly executingAssembly;
        private Document currentDocument;
        private string textToPrint;

        public enum StartupAction { None, FindFlaggedDocuments, ShowAllDocuments }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="viewHandle">The <c>Handle</c> of the view.</param>
        public MainViewModel(IntPtr viewHandle)
        {
            this.viewHandle = viewHandle;
            GetServices(ServiceLocator.Services);
            printDocument = new PrintDocument();
            pdfUploader = new PdfUploader();
            fileCache = new FileCache();
            uploadRejectedDirectory = new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.UploadRejected);
            executingAssembly = new ExecutingAssembly();
            printDocument.PrintPage += PrintDocument_PrintPage;
            FindDocumentsViewState.OnFindDocumentsParamChanged = () => GetListOfDocuments(false);
            checkedDocumentIds = new Collection<int>();
            InitializeCommands();
        }

        public Action OnSettingsChanged { get; set; }
        public Action OnCheckForUpdate { get; set; }
        public Action OnGetViewState { get; set; }
        public Action OnUndoNotes { get; set; }
        public Action OnCutNotes { get; set; }
        public Action OnCopyText { get; set; }
        public Action OnPasteNotes { get; set; }
        public Action OnSelectAllText { get; set; }
        public Action OnFindDocuments { get; set; }
        public Action<bool> OnSelectAllDocuments { get; set; }
        public Action OnManageUploadProfiles { get; set; }
        public Action OnShowHelp { get; set; }
        public Action<PdfFile> OnPdfDoDragDrop { get; set; }
        public Action OnScrollToEndOfNotesText { get; set; }        
        public Action OnBlockingUploadStarted { get; set; }
        public Action OnBlockingUploadFinished { get; set; }
        public Action OnProgressBarPerformStep { get; set; }
        public Action OnCheckedDocumentsProcessed { get; set; }
        public Action OnCheckForFlaggedDocumentsStarted { get; set; }
        public Action OnCheckForFlaggedDocumentsFinished { get; set; }
        public Action OnCheckForDocumentsListChangesStarted { get; set; }
        public Action OnCheckForDocumentsListChangesFinished { get; set; }
        public Action OnUploadPdfFilesStarted { get; set; }
        public Action OnUploadPdfFilesFinished { get; set; }
        public Action OnSetDocumentsListHasChangesStarted { get; set; }
        public Action OnSetDocumentsListHasChangesFinished { get; set; }
        public Action OnSetViewState { get; set; }
        public ICommand ClipboardUpdateCommand { get; private set; }

        /// <summary>
        /// Performs actions that need to happen during the loading of the view.
        /// <para>
        /// <see cref="ICommand.Execute(StartupAction)"/>: The <see cref="StartupAction"/> to perform.
        /// </para>
        /// </summary>
        public ICommand ViewLoadCommand { get; private set; }
        
        public ICommand AddPdfCommand { get; private set; }
        public ICommand OpenPdfForEachSelectedDocumentCommand { get; private set; }
        public ICommand SaveNotesCommand { get; private set; }
        public ICommand PdfOrTextSaveAsCommand { get; private set; }
        public ICommand BurstCurrentDocumentPdfCommand { get; private set; }

        /// <summary>
        /// Extracts all attached files from the PDF of the selected document.
        /// <para>
        /// <see cref="ICommand.Execute(PdfFile.AttachedFilesType)"/>: The
        /// <see cref="PdfFile.AttachedFilesType"/> to extract from the PDF.
        /// </para>
        /// </summary>
        public ICommand ExtractAllAttachedFromCurrentDocumentPdfCommand { get; private set; }

        public ICommand CopyCurrentDocumentPdfToClipboardCommand { get; private set; }
        public ICommand PrintDocumentDataTextCommand { get; private set; }
        public ICommand PrintDocumentDataTextWithPreviewCommand { get; private set; }
        public ICommand ExportEachSelectedDocumentCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand UndoNotesCommand { get; private set; }
        public ICommand CutNotesCommand { get; private set; }
        public ICommand CopyTextCommand { get; private set; }
        public ICommand PasteNotesCommand { get; private set; }
        public ICommand SelectAllTextCommand { get; private set; }
        public ICommand RestoreNotesCommand { get; private set; }
        public ICommand AppendDateTimeIntoNotesCommand { get; private set; }
        public ICommand AppendTextFromFileIntoNotesCommand { get; private set; }
        public ICommand UpdateCurrentDocumentFlagStateCommand { get; private set; }
        public ICommand FindDocumentsCommand { get; private set; }
        public ICommand SelectAllDocumentsCommand { get; private set; }
        public ICommand SelectNoDocumentsCommand { get; private set; }
        public ICommand SetTitleOnEachSelectedDocumentCommand { get; private set; }
        public ICommand SetAuthorOnEachSelectedDocumentCommand { get; private set; }
        public ICommand SetSubjectOnEachSelectedDocumentCommand { get; private set; }
        public ICommand SetCategoryOnEachSelectedDocumentCommand { get; private set; }
        public ICommand SetTaxYearOnEachSelectedDocumentCommand { get; private set; }
        public ICommand SetDateTimeAddedOnEachSelectedDocumentCommand { get; private set; }
        public ICommand DeleteEachSelectedDocumentCommand { get; private set; }
        public ICommand SetViewMenuItemsCheckedStateCommand { get; private set; }
        public ICommand SetPreviewPixelDensityCommand { get; private set; }
        public ICommand ToggleToolStripVisibleStateCommand { get; private set; }
        public ICommand ToggleStatusStripVisibleStateCommand { get; private set; }
        public ICommand ShowOptionsCommand { get; private set; }
        public ICommand ManageUploadProfilesCommand { get; private set; }
        public ICommand MoveLocalDatabaseCommand { get; private set; }
        public ICommand ShowHelpCommand { get; private set; }
        public ICommand ShowAboutBoxCommand { get; private set; }

        /// <summary>
        /// Sets a collection of checked document ID's and sets menu state on applicable menu items.
        /// <para>
        /// <see cref="ICommand.Execute(Collection{T})"/>: The <see cref="Collection{T}"/> of
        /// document ID's, where <c>T</c> is <see cref="int"/>.
        /// </para>
        /// </summary>
        public ICommand SetCheckedDocumentIdsCommand { get; private set; }

        /// <summary>
        /// Opens the PDF for the current document.
        /// <para>
        /// <see cref="ICommand.Execute(bool)"/>: <c>true</c> or <c>false</c> to show PDF with
        /// default application.
        /// </para>
        /// </summary>
        public ICommand OpenPdfForCurrentDocumentCommand { get; private set; }

        public ICommand DoDragDropPdfForCurrentDocumentCommand { get; private set; }

        /// <summary>
        /// Sets the state for a text box enter event.
        /// <para>
        /// <see cref="ICommand.Execute(bool)"/>: <c>true</c> or <c>false</c> if the user can undo
        /// the previous operation.
        /// </para>
        /// </summary>
        public ICommand SetStateOnTextBoxEnterEventCommand { get; private set; }

        /// <summary>
        /// Method that is to be called when the Notes text changes.
        /// <para>
        /// <see cref="ICommand.Execute(bool)"/>: <c>true</c> or <c>false</c> if the user can undo
        /// the previous operation.
        /// </para>
        /// </summary>
        public ICommand NotesTextChangedCommand { get; private set; }

        public ICommand SetStateForTextBoxSelectedTextCommand { get; private set; }
        public ICommand SetStateOnTextBoxLeaveEventCommand { get; private set; }
        public ICommand ExploreUploadRejectedFolderCommand { get; private set; }
        public ICommand CheckForFlaggedDocumentsCommand { get; private set; }
        public ICommand CheckForDocumentsListChangesCommand { get; private set; }
        public ICommand UploadPdfFilesCommand { get; private set; }
        public ICommand SetDocumentsListHasChangesCommand { get; private set; }
        public ICommand BeforeViewClosingCommand { get; private set; }
        public ICommand ViewClosingCommand { get; private set; }
        public bool ViewMinimized { get; set; }
        public bool CompactLocalDatabaseAfterDelete { get; set; }
        public decimal PreviewPixelDensity { get; set; }
        public bool ShowPdfWithDefaultApplication { get; set; }
        
        public bool ToolStripVisible
        {
            get => toolStripVisible;
            set
            {
                toolStripVisible = value;
                OnPropertyChanged();
                ViewToolBarChecked = value;
            }
        }

        public bool StatusStripVisible
        {
            get => statusStripVisible;
            set
            {
                statusStripVisible = value;
                OnPropertyChanged();
                ViewStatusBarChecked = value;
            }
        }

        public Size ViewSize { get; set; }

        public bool FileAddMenuEnabled
        {
            get => fileAddMenuEnabled;
            set
            {
                fileAddMenuEnabled = VerifyInsertGranted(value);
                OnPropertyChanged();
            }
        }

        public bool FileOpenMenuEnabled
        {
            get => fileOpenMenuEnabled;
            set
            {
                fileOpenMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FileSaveMenuEnabled
        {
            get => fileSaveMenuEnabled;
            set
            {
                fileSaveMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool FileSaveAsMenuEnabled
        {
            get => fileSaveAsMenuEnabled;
            set
            {
                fileSaveAsMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FileBurstMenuEnabled
        {
            get => fileBurstMenuEnabled;
            set
            {
                fileBurstMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FileExtractMenuEnabled
        {
            get => fileExtractMenuEnabled;
            set
            {
                fileExtractMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FileExtractAllAttachmentsMenuEnabled
        {
            get => fileExtractAllAttachmentsMenuEnabled;
            set
            {
                fileExtractAllAttachmentsMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FileExtractAllEmbeddedFilesMenuEnabled
        {
            get => fileExtractAllEmbeddedFilesMenuEnabled;
            set
            {
                fileExtractAllEmbeddedFilesMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FileCopyPdfToClipboardEnabled
        {
            get => fileCopyPdfToClipboardEnabled;
            set
            {
                fileCopyPdfToClipboardEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FilePrintMenuEnabled
        {
            get => filePrintMenuEnabled;
            set
            {
                filePrintMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FilePrintPreviewMenuEnabled
        {
            get => filePrintPreviewMenuEnabled;
            set
            {
                filePrintPreviewMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool FileExportMenuEnabled
        {
            get => fileExportMenuEnabled;
            set
            {
                fileExportMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditUndoMenuEnabled
        {
            get => editUndoMenuEnabled;
            set
            {
                editUndoMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditCutMenuEnabled
        {
            get => editCutMenuEnabled;
            set
            {
                editCutMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditCopyMenuEnabled
        {
            get => editCopyMenuEnabled;
            set
            {
                editCopyMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditPasteMenuEnabled
        {
            get => editPasteMenuEnabled;
            set
            {
                editPasteMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditSelectAllMenuEnabled
        {
            get => editSelectAllMenuEnabled;
            set
            {
                editSelectAllMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditRestoreMenuEnabled
        {
            get => editRestoreMenuEnabled;
            set
            {
                editRestoreMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditAppendDateTimeMenuEnabled
        {
            get => editAppendDateTimeMenuEnabled;
            set
            {
                editAppendDateTimeMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditAppendTextMenuEnabled
        {
            get => editAppendTextMenuEnabled;
            set
            {
                editAppendTextMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool EditFlagDocumentMenuEnabled
        {
            get => editFlagDocumentMenuEnabled;
            set
            {
                editFlagDocumentMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool EditFlagDocumentMenuChecked
        {
            get => editFlagDocumentMenuChecked;
            set
            {
                editFlagDocumentMenuChecked = value;
                OnPropertyChanged();
            }
        }

        public bool DocumentsFindMenuEnabled
        {
            get => documentsFindMenuEnabled;
            set => SetProperty(ref documentsFindMenuEnabled, value);
        }

        public bool DocumentsSelectMenuEnabled
        {
            get => documentsSelectMenuEnabled;
            set
            {
                documentsSelectMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool DocumentsSetTitleMenuEnabled
        {
            get => documentsSetTitleMenuEnabled;
            set
            {
                documentsSetTitleMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool DocumentsSetAuthorMenuEnabled
        {
            get => documentsSetAuthorMenuEnabled;
            set
            {
                documentsSetAuthorMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool DocumentsSetSubjectMenuEnabled
        {
            get => documentsSetSubjectMenuEnabled;
            set
            {
                documentsSetSubjectMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool DocumentsSetCategoryMenuEnabled
        {
            get => documentsSetCategoryMenuEnabled;
            set
            {
                documentsSetCategoryMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool DocumentsSetTaxYearMenuEnabled
        {
            get => documentsSetTaxYearMenuEnabled;
            set
            {
                documentsSetTaxYearMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool DocumentsSetDateTimeAddedMenuEnabled
        {
            get => documentsSetDateTimeAddedMenuEnabled;
            set
            {
                documentsSetDateTimeAddedMenuEnabled = VerifyUpdateGranted(value);
                OnPropertyChanged();
            }
        }

        public bool DocumentsDeleteMenuEnabled
        {
            get => documentsDeleteMenuEnabled;
            set
            {
                documentsDeleteMenuEnabled = VerifyDeleteGranted(value);
                OnPropertyChanged();
            }
        }

        public bool ViewSetPreviewPixelDensityMenuEnabled
        {
            get => viewSetPreviewPixelDensityMenuEnabled;
            set
            {
                viewSetPreviewPixelDensityMenuEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool ViewToolBarChecked
        {
            get => viewToolBarChecked;
            set
            {
                viewToolBarChecked = value;
                OnPropertyChanged();
            }
        }

        public bool ViewStatusBarChecked
        {
            get => viewStatusBarChecked;
            set
            {
                viewStatusBarChecked = value;
                OnPropertyChanged();
            }
        }

        public bool ToolsUploadProfilesMenuEnabled
        {
            get => toolsUploadProfilesMenuEnabled;
            set
            {
                toolsUploadProfilesMenuEnabled = VerifyInsertGranted(value);
                OnPropertyChanged();
            }
        }

        public bool ToolsMoveDatabaseMenuVisible
        {
            get => toolsMoveDatabaseMenuVisible;
            set
            {
                toolsMoveDatabaseMenuVisible = value;
                OnPropertyChanged();
            }
        }

        public DataTable Documents
        {
            get => documents;
            set
            {
                SetProperty(ref documents, value);
                if (documents.Rows.Count > 0)
                {
                    DocumentsSelectMenuEnabled = true;
                }
                else
                {
                    DocumentsSelectMenuEnabled = false;
                }
            }
        }

        public Collection<int> CheckedDocumentIds { get => checkedDocumentIds; }

        public bool DocumentsEnabled
        {
            get => documentsEnabled;
            set
            {
                documentsEnabled = value;
                OnPropertyChanged();
            }
        }

        public int CurrentDocumentId 
        {
            get => currentDocumentId;
            set
            {
                SetProperty(ref currentDocumentId, value);
                OnDocumentSelectionChanged();
            }
        }

        public bool DocumentDataEnabled
        {
            get => documentDataEnabled;
            set
            {
                SetProperty(ref documentDataEnabled, value);
                FileOpenMenuEnabled = documentDataEnabled;
                FileSaveAsMenuEnabled = documentDataEnabled;
                FileBurstMenuEnabled = documentDataEnabled;
                FileCopyPdfToClipboardEnabled = documentDataEnabled;
                EditFlagDocumentMenuEnabled = documentDataEnabled;
                ViewSetPreviewPixelDensityMenuEnabled = documentDataEnabled;
            }
        }
        
        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }

        public string SelectedNotes
        {
            get => selectedNotes;
            set
            {
                selectedNotes = value;
                SetStateForTextBoxSelectedText();
            }
        }

        public string PreviousNotes { get; set; }
        public bool NotesChanged { get; set; }

        public bool NotesReadOnly
        {
            get => notesReadOnly;
            set => notesReadOnly = !VerifyUpdateGranted(value);
        }

        public bool NotesFocused
        {
            get => notesFocused;
            set => notesFocused = value;            
        }

        public string Keywords
        {
            get => keywords;
            set => SetProperty(ref keywords, value);
        }

        public string SelectedKeywords
        {
            get => selectedKeywords;
            set
            {
                selectedKeywords = value;
                SetStateForTextBoxSelectedText();
            }
        }

        public bool KeywordsFocused
        {
            get => keywordsFocused;
            set => keywordsFocused = value;
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string SelectedText
        {
            get => selectedText;
            set
            {
                selectedText = value;
                SetStateForTextBoxSelectedText();
            }
        }

        public bool TextFocused
        {
            get => textFocused;
            set => textFocused = value;
        }

        public string SearchTermSnippets
        {
            get => searchTermSnippets;
            set => SetProperty(ref searchTermSnippets, value);
        }
        
        public string SelectedSearchTermSnippets
        {
            get => selectedSearchTermSnippets;
            set
            {
                selectedSearchTermSnippets = value;
                SetStateForTextBoxSelectedText();
            }
        }

        public bool SearchTermSnippetsFocused
        {
            get => searchTermSnippetsFocused;
            set => searchTermSnippetsFocused = value;
        }

        public bool SearchTermSnippetsVisible
        {
            get => searchTermSnippetsVisible;
            set
            {
                searchTermSnippetsVisible = value;
                OnPropertyChanged();
            }
        }

        public Image Preview
        {
            get => preview;
            set => SetProperty(ref preview, value);
        }

        public bool DocumentsProgressBarVisible
        {
            get => documentsProgressBarVisible;
            set
            {
                SetProperty(ref documentsProgressBarVisible, value);
                DocumentsFindMenuEnabled = !value;
            }
        }

        public int DocumentsProgressBarMinimum
        {
            get => documentsProgressBarMinimum;
            set => SetProperty(ref documentsProgressBarMinimum, value);
        }

        public int DocumentsProgressBarMaximum
        {
            get => documentsProgressBarMaximum;
            set => SetProperty(ref documentsProgressBarMaximum, value);
        }

        public bool UploadProgressBarVisible
        {
            get => uploadProgressBarVisible;
            set => SetProperty(ref uploadProgressBarVisible, value);
        }

        public bool RefreshingDocumentsImageVisible
        {
            get => refreshingDocumentsImageVisible;
            set => SetProperty(ref refreshingDocumentsImageVisible, value);
        }

        public bool FlagImageVisible
        {
            get => flagImageVisible;
            set => SetProperty(ref flagImageVisible, value);
        }

        public bool UploadRejectedImageVisible
        {
            get => uploadRejectedImageVisible;
            set => SetProperty(ref uploadRejectedImageVisible, value);
        }

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            foreach (var service in serviceProvider.GetServices<IDialogService>())
            {
                switch (service.GetType().Name)
                {
                    case "AddPdfDialogService":
                        addPdfDialogService = service;
                        break;
                    case "SetTitleDialogService":
                        setTitleDialogService = service;
                        break;
                    case "SetAuthorDialogService":
                        setAuthorDialogService = service;
                        break;
                    case "SetSubjectDialogService":
                        setSubjectDialogService = service;
                        break;
                    case "SetCategoryDialogService":
                        setCategoryDialogService = service;
                        break;
                    case "SetTaxYearDialogService":
                        setTaxYearDialogService = service;
                        break;
                    case "SetDateTimeAddedDialogService":
                        setDateTimeAddedDialogService = service;
                        break;
                    case "SetPreviewPixelDensityDialogService":
                        setPreviewPixelDensityDialogService = service;
                        break;
                    case "OptionsDialogService":
                        optionsDialogService = service;
                        break;
                    case "AboutBoxDialogService":
                        aboutBoxDialogService = service;
                        break;
                }
            }

            foreach (var service in serviceProvider.GetServices<IFileDialogService>())
            {
                switch (service.GetType().Name)
                {
                    case "OpenFileDialogService":
                        openFileDialogService = service;
                        break;
                    case "SaveFileDialogService":
                        saveFileDialogService = service;
                        break;
                }
            }

            folderBrowserDialogService = serviceProvider.GetService<IFolderBrowserDialogService>();
            folderExplorerService = serviceProvider.GetService<IFolderExplorerService>();
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
            pdfViewerService = serviceProvider.GetService<IPdfViewerService>();
            printDialogService = serviceProvider.GetService<IPrintDialogService>();
            printPreviewDialogService = serviceProvider.GetService<IPrintPreviewDialogService>();
        }

        private void InitializeCommands()
        {
            ClipboardUpdateCommand = new RelayCommand(
                OnClipboardUpdate);
            ViewLoadCommand = new RelayCommand<StartupAction>(
                OnViewLoad);
            AddPdfCommand = new RelayCommand(
                AddPdf);
            OpenPdfForEachSelectedDocumentCommand = new RelayCommand(
                OpenPdfForEachSelectedDocument);
            SaveNotesCommand = new RelayCommand(
                SaveNotes);
            PdfOrTextSaveAsCommand = new RelayCommand(
                PdfOrTextSaveAs);
            BurstCurrentDocumentPdfCommand = new AsyncRelayCommand(
                BurstCurrentDocumentPdf);
            ExtractAllAttachedFromCurrentDocumentPdfCommand = 
                new AsyncRelayCommand<PdfFile.AttachedFilesType>(
                    ExtractAllAttachedFromCurrentDocumentPdf);
            CopyCurrentDocumentPdfToClipboardCommand = new RelayCommand(
                CopyCurrentDocumentPdfToClipboard);
            PrintDocumentDataTextCommand = new RelayCommand(
                PrintDocumentDataText);
            PrintDocumentDataTextWithPreviewCommand = new RelayCommand(
                PrintDocumentDataTextWithPreview);
            ExportEachSelectedDocumentCommand = new RelayCommand(
                ExportEachSelectedDocument);
            CloseCommand = new RelayCommand(
                Close);
            UndoNotesCommand = new RelayCommand(
                UndoNotes);
            CutNotesCommand = new RelayCommand(
                CutNotes);
            CopyTextCommand = new RelayCommand(
                CopyText);
            PasteNotesCommand = new RelayCommand(
                PasteNotes);
            SelectAllTextCommand = new RelayCommand(
                SelectAllText);
            RestoreNotesCommand = new RelayCommand(
                RestoreNotes);
            AppendDateTimeIntoNotesCommand = new RelayCommand(
                AppendDateTimeIntoNotes);
            AppendTextFromFileIntoNotesCommand = new RelayCommand(
                AppendTextFromFileIntoNotes);
            UpdateCurrentDocumentFlagStateCommand = new RelayCommand(
                UpdateCurrentDocumentFlagState);
            FindDocumentsCommand = new RelayCommand(
                FindDocuments);
            SelectAllDocumentsCommand = new RelayCommand(
                SelectAllDocuments);
            SelectNoDocumentsCommand = new RelayCommand(
                SelectNoDocuments);
            SetTitleOnEachSelectedDocumentCommand = new RelayCommand(
                SetTitleOnEachSelectedDocument);
            SetAuthorOnEachSelectedDocumentCommand = new RelayCommand(
                SetAuthorOnEachSelectedDocument);
            SetSubjectOnEachSelectedDocumentCommand = new RelayCommand(
                SetSubjectOnEachSelectedDocument);
            SetCategoryOnEachSelectedDocumentCommand = new RelayCommand(
                SetCategoryOnEachSelectedDocument);
            SetTaxYearOnEachSelectedDocumentCommand = new RelayCommand(
                SetTaxYearOnEachSelectedDocument);
            SetDateTimeAddedOnEachSelectedDocumentCommand = new RelayCommand(
                SetDateTimeAddedOnEachSelectedDocument);
            DeleteEachSelectedDocumentCommand = new RelayCommand(
                DeleteEachSelectedDocument);
            SetPreviewPixelDensityCommand = new RelayCommand(
                SetPreviewPixelDensity);
            ToggleToolStripVisibleStateCommand = new RelayCommand(
                ToggleToolStripVisibleState);
            ToggleStatusStripVisibleStateCommand = new RelayCommand(
                ToggleStatusStripVisibleState);
            ShowOptionsCommand = new RelayCommand(
                ShowOptions);
            ManageUploadProfilesCommand = new RelayCommand(
                ManageUploadProfiles);
            MoveLocalDatabaseCommand = new RelayCommand(
                MoveLocalDatabase);
            ShowHelpCommand = new RelayCommand(
                ShowHelp);
            ShowAboutBoxCommand = new RelayCommand(
                ShowAboutBox);
            SetCheckedDocumentIdsCommand = new RelayCommand<Collection<int>>(
                SetCheckedDocumentIds);
            OpenPdfForCurrentDocumentCommand = new RelayCommand<bool>(
                OpenPdfForCurrentDocument);
            DoDragDropPdfForCurrentDocumentCommand = new RelayCommand(
                DoDragDropPdfForCurrentDocument);
            SetStateOnTextBoxEnterEventCommand = new RelayCommand<bool>(
                SetStateOnTextBoxEnterEvent);
            NotesTextChangedCommand = new RelayCommand<bool>(
                OnNotesTextChanged);
            SetStateForTextBoxSelectedTextCommand = new RelayCommand(
                SetStateForTextBoxSelectedText);
            SetStateOnTextBoxLeaveEventCommand = new RelayCommand(
                SetStateOnTextBoxLeaveEvent);
            ExploreUploadRejectedFolderCommand = new RelayCommand(
                ExploreUploadRejectedFolder);
            CheckForFlaggedDocumentsCommand = new AsyncRelayCommand(
                CheckForFlaggedDocuments);
            CheckForDocumentsListChangesCommand = new RelayCommand(
                CheckForDocumentsListChanges);
            UploadPdfFilesCommand = new AsyncRelayCommand(
                UploadPdfFiles);
            SetDocumentsListHasChangesCommand = new RelayCommand(
                SetDocumentsListHasChanges);
            BeforeViewClosingCommand = new RelayCommand(
                OnBeforeViewClosing);
            ViewClosingCommand = new RelayCommand(
                OnViewClosing);
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

        private void OnClipboardUpdate()
        {
            if (NotesFocused)
            {
                EditPasteMenuEnabled = Clipboard.GetContent().Contains(StandardDataFormats.Text);
            }
        }

        /// <summary>
        /// Performs actions that need to happen during the loading of the view.
        /// </summary>
        /// <param name="startupAction">The <see cref="StartupAction"/>.</param>
        private void OnViewLoad(StartupAction startupAction)
        {
            NativeMethods.AddClipboardFormatListener(viewHandle);
            OnSettingsChanged?.Invoke();
            OnCheckForUpdate?.Invoke();
            OnGetViewState?.Invoke();
            SetInitialState();

            if (!startupAction.Equals(StartupAction.None))
            {
                var findDocumentsParam = new FindDocumentsParam();
                switch (startupAction)
                {
                    case StartupAction.FindFlaggedDocuments:
                        findDocumentsParam.FindFlaggedDocumentsChecked = true;
                        break;
                    case StartupAction.ShowAllDocuments:
                        findDocumentsParam.AllDocumentsChecked = true;
                        break;
                }
                FindDocumentsViewState.FindDocumentsParam = findDocumentsParam;

                GetListOfDocuments(false);
            }
        }

        private void AddPdf()
        {
            addPdfDialogService.ShowDialog();
        }

        private void OpenPdfForEachSelectedDocument()
        {
            if (CheckedDocumentIds.Count > 0)
            {
                var openMaximum = 12;
                var count = 0;
                foreach (int id in CheckedDocumentIds)
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
                            pdfViewerService.Show(
                                fileCache.GetPdfFile(id).FullName,
                                ShowPdfWithDefaultApplication);
                        }
                        catch (DatabaseException ex)
                        {
                            var message = ResourceHelper.GetString(
                                Resources.ResourceManager,
                                "DefaultDocumentException",
                                ex.Message,
                                id.ToString());
                            messageBoxService.ShowMessage(message, true);
                        }
                    }
                }
                if (count > openMaximum)
                {
                    var message = ResourceHelper.GetString(
                        Resources.ResourceManager,
                        "OpenCheckedDocumentsMaximumReached",
                        openMaximum.ToString());
                    messageBoxService.ShowMessage(message);
                }
                OnCheckedDocumentsProcessed();
            }
            else
            {
                OpenPdfForCurrentDocument(ShowPdfWithDefaultApplication);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Usage",
            "CA2245:Do not assign a property to itself",
            Justification = "Needed to trigger Notes update on the view.")]
        private void SaveNotes()
        {
            var rule = new NotesSizeRule(Notes);
            if (!rule.ViolationFound)
            {
                using (var documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    var document = documentRepository.GetDocument(CurrentDocumentId, null);
                    var notesInDatabase = document.Notes;
                    Notes = Notes.Trim();

                    if (document.Notes.Equals(PreviousNotes, StringComparison.Ordinal))
                    {
                        try
                        {
                            OnLongOperationStarted?.Invoke();
                            PreviousNotes = Notes;
                            document.Notes = Notes;
                            documentRepository.UpdateDocument(document);
                            Notes = Notes;  // CA2245 suppressed.
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            var message = ResourceHelper.GetString(
                                Resources.ResourceManager,
                                "DocumentMayHaveBeenDeletedException",
                                ex.Message);
                            messageBoxService.ShowMessage(message, true);
                        }
                        catch (DatabaseException ex)
                        {
                            PreviousNotes = Notes;
                            Notes = notesInDatabase;
                            messageBoxService.ShowMessage(ex.Message, true);
                        }
                        finally
                        {
                            OnLongOperationFinished?.Invoke();
                        }
                    }
                    else
                    {
                        var dataPackage = new DataPackage();
                        dataPackage.SetText(Notes);
                        Clipboard.SetContent(dataPackage);
                        PreviousNotes = Notes;
                        Notes = notesInDatabase;
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

        private void PdfOrTextSaveAs()
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

        private async Task BurstCurrentDocumentPdf()
        {
            var selectedPath = folderBrowserDialogService.ShowDialog(Resources.SelectBurstFolder);
            if (selectedPath.Length > 0)
            {
                var pdfFile = fileCache.GetPdfFile(CurrentDocumentId);
                try
                {
                    await Task.Run(() => pdfFile.Split(
                        new DirectoryInfo(
                            selectedPath))).ConfigureAwait(true);
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
        /// The type of attachment in the PDF to extract.
        /// </param>
        private async Task ExtractAllAttachedFromCurrentDocumentPdf(
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
                var pdfFile = fileCache.GetPdfFile(CurrentDocumentId);

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

        private void CopyCurrentDocumentPdfToClipboard()
        {
            var pdfFile = fileCache.GetPdfFile(CurrentDocumentId);
            pdfFile.CopyToClipboard();
        }

        private void PrintDocumentDataText()
        {
            textToPrint = GetDocumentDataText(GetFocusedDocumentDataType());

            if (printDialogService.ShowDialog(printDocument).Equals(1))
            {
                printDocument.Print();
            }
        }

        private void PrintDocumentDataTextWithPreview()
        {
            textToPrint = GetDocumentDataText(GetFocusedDocumentDataType());
            printPreviewDialogService.ShowDialog(printDocument, ViewSize);
        }

        private void ExportEachSelectedDocument()
        {
            var selectedPath = folderBrowserDialogService.ShowDialog(Resources.SelectExportFolder);
            if (selectedPath.Length > 0)
            {
                selectedPath = Path.Combine(
                    selectedPath,
                    string.Concat(
                        executingAssembly.ProductName,
                        "-",
                        Resources.Export,
                        "_",
                        DateTime.Now.ToString(
                            "yyyy-MM-dd_HH.mm",
                            CultureInfo.CurrentCulture)));
                Directory.CreateDirectory(selectedPath);
                ProcessEachCheckedDocument(CheckedDocumentAction.Export, selectedPath);
            }
        }

        private void Close() => OnCloseView?.Invoke();
        private void UndoNotes() => OnUndoNotes?.Invoke();
        private void CutNotes() => OnCutNotes?.Invoke();
        private void CopyText() => OnCopyText?.Invoke();
        private void PasteNotes() => OnPasteNotes?.Invoke();
        private void SelectAllText() => OnSelectAllText?.Invoke();

        private void RestoreNotes()
        {
            var originalNotes = PreviousNotes;
            PreviousNotes = originalNotes;
            Notes = PreviousNotes;
        }

        private void AppendDateTimeIntoNotes()
        {
            string notes = null;
            var newLine = Environment.NewLine;
            OnScrollToEndOfNotesText?.Invoke();

            if (Notes.Length > 0)
            {
                notes = string.Concat(Notes, newLine, newLine);
            }

            Notes = string.Concat(
                notes,
                "--- ",
                DateTime.Now,
                " (",
                DatabaseSession.UserName,
                ") ---",
                newLine);
            OnScrollToEndOfNotesText?.Invoke();
        }

        private void AppendTextFromFileIntoNotes()
        {
            var textFilePath = openFileDialogService.ShowDialog(Resources.TextFilter);
            if (textFilePath.Length > 0)
            {
                var textFile = new FileInfo(textFilePath);
                OnScrollToEndOfNotesText?.Invoke();
                Notes = Notes.AppendTextFile(textFile);

                if (messageBoxService.ShowQuestion(
                    ResourceHelper.GetString(
                        Resources.ResourceManager,
                        "DeleteToRecycleBin",
                        textFile.FullName)).Equals(6))
                {
                    textFile.DeleteToRecycleBin();
                }

                OnScrollToEndOfNotesText?.Invoke();
            }
        }

        private void UpdateCurrentDocumentFlagState()
        {
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                var document = documentRepository.GetDocument(CurrentDocumentId, null);
                document.Flag = Convert.ToInt32(!EditFlagDocumentMenuChecked);

                try
                {
                    OnLongOperationStarted?.Invoke();
                    documentRepository.UpdateDocument(document);
                }
                catch (IndexOutOfRangeException ex)
                {
                    var message = ResourceHelper.GetString(
                        Resources.ResourceManager,
                        "DocumentMayHaveBeenDeletedException",
                        ex.Message);
                    messageBoxService.ShowMessage(message, true);
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                finally
                {
                    OnLongOperationFinished?.Invoke();
                }
            }
        }

        private void FindDocuments() => OnFindDocuments?.Invoke();

        private void SelectAllDocuments()
        {
            OnLongOperationStarted?.Invoke();
            OnSelectAllDocuments?.Invoke(true);
            OnLongOperationFinished?.Invoke();
        }

        private void SelectNoDocuments()
        {
            OnLongOperationStarted?.Invoke();
            OnSelectAllDocuments?.Invoke(false);
            OnLongOperationFinished?.Invoke();
        }

        private void SetTitleOnEachSelectedDocument()
        {
            var value = setTitleDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetTitle, value);
            }
        }

        private void SetAuthorOnEachSelectedDocument()
        {
            var value = setAuthorDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetAuthor, value);
            }
        }

        private void SetSubjectOnEachSelectedDocument()
        {
            var value = setSubjectDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetSubject, value);
            }
        }

        private void SetCategoryOnEachSelectedDocument()
        {
            var value = setCategoryDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetCategory, value);
            }
        }

        private void SetTaxYearOnEachSelectedDocument()
        {
            var value = setTaxYearDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetTaxYear, value);
            }
        }

        private void SetDateTimeAddedOnEachSelectedDocument()
        {
            var value = setDateTimeAddedDialogService.ShowDialog();
            if (value != null)
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.SetDateTimeAdded, value);
            }
        }

        private void DeleteEachSelectedDocument()
        {
            if (messageBoxService.ShowQuestion(Resources.DeleteSelectedDocuments).Equals(6))
            {
                ProcessEachCheckedDocument(CheckedDocumentAction.Delete, null);

                if (CompactLocalDatabaseAfterDelete)
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

        private void SetPreviewPixelDensity()
        {
            setPreviewPixelDensityDialogService.ShowDialog();
            OnSettingsChanged?.Invoke();
            SetPreviewImageForCurrentDocument();
        }

        private void ToggleToolStripVisibleState() => ToolStripVisible = !ToolStripVisible;
        private void ToggleStatusStripVisibleState() => StatusStripVisible = !StatusStripVisible;

        private void ShowOptions()
        {
            optionsDialogService.ShowDialog();
            OnSettingsChanged?.Invoke();
        }

        private void ManageUploadProfiles() => OnManageUploadProfiles?.Invoke();

        private void MoveLocalDatabase()
        {
            var selectedPath = folderBrowserDialogService.ShowDialog(Resources.SelectExportFolder);
            if (selectedPath.Length > 0)
            {
                try
                {
                    OnLongOperationStarted?.Invoke();
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
                    OnLongOperationFinished?.Invoke();
                }
            }
        }

        private void ShowHelp() => OnShowHelp?.Invoke();
        private void ShowAboutBox() => aboutBoxDialogService.ShowDialog();

        /// <summary>
        /// Sets a collection of checked document ID's in the <see cref="CheckedDocumentIds"/>
        /// property and sets the state on applicable menu data bound properties.
        /// </summary>
        /// <param name="ids">The collection of document ID's.</param>
        private void SetCheckedDocumentIds(Collection<int> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            bool enabled = false;
            CheckedDocumentIds.Clear();

            if (ids.Count > 0)
            {
                enabled = true;

                foreach (int id in ids)
                {
                    CheckedDocumentIds.Add(id);
                }
            }

            DocumentsSetTitleMenuEnabled = enabled;
            DocumentsSetAuthorMenuEnabled = enabled;
            DocumentsSetSubjectMenuEnabled = enabled;
            DocumentsSetCategoryMenuEnabled = enabled;
            DocumentsSetTaxYearMenuEnabled = enabled;
            DocumentsSetDateTimeAddedMenuEnabled = enabled;
            DocumentsDeleteMenuEnabled = enabled;
            FileExportMenuEnabled = enabled;
        }

        /// <summary>
        /// Opens the PDF for the current document.
        /// </summary>
        /// <param name="showPdfWithDefaultApplication">
        /// <c>true</c> or <c>false</c> to show PDF with default application.
        /// </param>
        private void OpenPdfForCurrentDocument(bool showPdfWithDefaultApplication) =>
            pdfViewerService.Show(
                fileCache.GetPdfFile(
                    CurrentDocumentId).FullName,
                showPdfWithDefaultApplication);

        private void DoDragDropPdfForCurrentDocument()
        {
            var pdfFile = fileCache.GetPdfFile(CurrentDocumentId);
            OnPdfDoDragDrop?.Invoke(pdfFile);
        }

        /// <summary>
        /// Sets the state of the view on a <c>TextBox</c> <c>Enter</c> event.
        /// </summary>
        /// <param name="canUndo">
        /// <c>true</c> or <c>false</c> if the user can undo the previous operation.
        /// </param>
        private void SetStateOnTextBoxEnterEvent(bool canUndo)
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

            FilePrintMenuEnabled = printable;
            FilePrintPreviewMenuEnabled = printable;
            EditUndoMenuEnabled = false;
            EditCutMenuEnabled = false;
            EditCopyMenuEnabled = false;

            if (documentData.Length > 0)
            {
                EditSelectAllMenuEnabled = true;
            }
            else
            {
                EditSelectAllMenuEnabled = false;
            }

            NotesReadOnly = true;

            if (focusedDocumentDataType.Equals(DocumentDataType.Notes) && !NotesReadOnly)
            {
                EditAppendDateTimeMenuEnabled = true;
                EditAppendTextMenuEnabled = true;
                EditPasteMenuEnabled = Clipboard.GetContent().Contains(StandardDataFormats.Text);
                SetStateForNotesChanged(NotesChanged, canUndo);
            }
            else
            {
                EditAppendDateTimeMenuEnabled = false;
                EditAppendTextMenuEnabled = false;
            }
        }

        /// <summary>
        /// Sets the state of the view when <c>Notes</c> text changed.
        /// </summary>
        /// <param name="canUndo">
        /// <c>true</c> or <c>false</c> if the user can undo the previous operation.
        /// </param>
        private void OnNotesTextChanged(bool canUndo)
        {
            NotesChanged = Notes != PreviousNotes;
            var documentsEnabled = false;

            if (!NotesChanged)
            {
                documentsEnabled = true;

                if (CheckedDocumentIds.Count > 0)
                {
                    DocumentsSetTitleMenuEnabled = true;
                    DocumentsSetAuthorMenuEnabled = true;
                    DocumentsSetSubjectMenuEnabled = true;
                    DocumentsSetCategoryMenuEnabled = true;
                    DocumentsSetTaxYearMenuEnabled = true;
                    DocumentsSetDateTimeAddedMenuEnabled = true;
                    DocumentsDeleteMenuEnabled = true;
                    FileExportMenuEnabled = true;
                }
            }

            // The "if" checks are needed to prevent user from having to check / uncheck checkbox
            // in DocumentsDataGridView when Notes length > 0.

            if (NotesFocused)
            {
                DocumentsEnabled = documentsEnabled;
            }

            if (CurrentDocumentId > 0)
            {
                SetStateForNotesChanged(NotesChanged, canUndo);
            }
        }

        private void SetStateForTextBoxSelectedText()
        {
            var focusedDocumentDataType = GetFocusedDocumentDataType();
            var documentData = GetDocumentDataText(focusedDocumentDataType);
            var selectedDocumentData = GetDocumentDataSelectedText(focusedDocumentDataType);
            var readOnly = !focusedDocumentDataType.Equals(
                DocumentDataType.Notes) || NotesReadOnly;

            if (!string.IsNullOrEmpty(selectedDocumentData))
            {
                EditCutMenuEnabled = !readOnly;
                EditCopyMenuEnabled = true;
            }
            else
            {
                EditCutMenuEnabled = false;
                EditCopyMenuEnabled = false;
            }

            if (!string.IsNullOrEmpty(documentData))
            {
                EditSelectAllMenuEnabled = !documentData.Length.Equals(
                    selectedDocumentData.Length);
            }
            else
            {
                EditSelectAllMenuEnabled = false;
            }
        }

        private void SetStateOnTextBoxLeaveEvent()
        {
            FilePrintMenuEnabled = false;
            FilePrintPreviewMenuEnabled = false;
            EditUndoMenuEnabled = false;
            EditCutMenuEnabled = false;
            EditCopyMenuEnabled = false;
            EditPasteMenuEnabled = false;
            EditSelectAllMenuEnabled = false;
            EditRestoreMenuEnabled = false;
            EditAppendDateTimeMenuEnabled = false;
            EditAppendTextMenuEnabled = false;
        }

        private void ExploreUploadRejectedFolder()
        {
            folderExplorerService.Explore(uploadRejectedDirectory);
        }

        private async Task CheckForFlaggedDocuments()
        {
            if (!ViewMinimized)
            {
                OnCheckForFlaggedDocumentsStarted?.Invoke();
                
                try
                {
                    int count;
                    using (var documentRepository = DatabaseSession.GetDocumentRepository())
                    {
                        count = await Task.Run(() => documentRepository
                            .GetListOfFlaggedDocuments()
                            .Rows.Count)
                            .ConfigureAwait(true);
                    }
                    if (count > 0)
                    {
                        FlagImageVisible = true;
                    }
                    else
                    {
                        FlagImageVisible = false;
                    }
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }

                OnCheckForFlaggedDocumentsFinished?.Invoke();
            }
        }

        private void CheckForDocumentsListChanges()
        {
            if (DocumentsEnabled &&
                CheckedDocumentIds.Count.Equals(0) &&
                !ViewMinimized &&
                DatabaseSession.DocumentsListHasChanges)
            {
                OnCheckForDocumentsListChangesStarted?.Invoke();
                GetListOfDocuments(true);
                DatabaseSession.DocumentsListHasChanges = false;
                OnCheckForDocumentsListChangesFinished?.Invoke();
            }
        }

        private async Task UploadPdfFiles()
        {
            OnUploadPdfFilesStarted?.Invoke();
            await Task.Run(() => pdfUploader
                .ExecuteUploadDirectoryMaintenance())
                .ConfigureAwait(true);

            if (pdfUploader.PdfFilesReadyToUpload)
            {
                try
                {
                    if (ApplicationPolicy.GetPolicyValue(
                        ApplicationPolicy.PolicyName.BlockingUpload))
                    {
                        OnBlockingUploadStarted?.Invoke();
                    }
                    else
                    {
                        UploadProgressBarVisible = true;
                    }

                    await Task.Run(() => pdfUploader.ExecuteUpload()).ConfigureAwait(true);
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
                    if (ApplicationPolicy.GetPolicyValue(
                        ApplicationPolicy.PolicyName.BlockingUpload))
                    {
                        OnBlockingUploadFinished?.Invoke();
                    }
                    else
                    {
                        UploadProgressBarVisible = false;
                    }
                }
            }

            UploadRejectedImageVisible = pdfUploader.UploadRejectedContainsPdfFiles;
            OnUploadPdfFilesFinished?.Invoke();
        }

        private void SetDocumentsListHasChanges()
        {
            if (!DatabaseSession.PlatformName.Equals(
                DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                OnSetDocumentsListHasChangesStarted?.Invoke();
                DatabaseSession.DocumentsListHasChanges = true;
                OnSetDocumentsListHasChangesFinished?.Invoke();
            }
        }

        private void OnBeforeViewClosing()
        {
            if (NotesChanged)
            {
                var choice = messageBoxService.ShowQuestion(Resources.NotesModified, true);
                switch (choice)
                {
                    case 6:
                        SaveNotes();
                        CancelViewClosing = false;
                        break;
                    case 7:
                        RestoreNotes();
                        CancelViewClosing = false;
                        break;
                    default:
                        CancelViewClosing = true;
                        break;
                }
            }
        }

        private void OnViewClosing()
        {
            WaitForUploadToFinish();
            NativeMethods.RemoveClipboardFormatListener(viewHandle);
            OnSetViewState?.Invoke();
        }
        
        private void SetInitialState()
        {
            FileAddMenuEnabled = true;
            FileOpenMenuEnabled = false;
            FileSaveMenuEnabled = false;
            FileSaveAsMenuEnabled = false;
            FileBurstMenuEnabled = false;
            FileExtractMenuEnabled = false;
            FileExtractAllAttachmentsMenuEnabled = false;
            FileExtractAllEmbeddedFilesMenuEnabled = false;
            FileCopyPdfToClipboardEnabled = false;
            FilePrintMenuEnabled = false;
            FilePrintPreviewMenuEnabled = false;
            FileExportMenuEnabled = false;
            EditUndoMenuEnabled = false;
            EditCutMenuEnabled = false;
            EditCopyMenuEnabled = false;
            EditPasteMenuEnabled = false;
            EditSelectAllMenuEnabled = false;
            EditRestoreMenuEnabled = false;
            EditAppendDateTimeMenuEnabled = false;
            EditAppendTextMenuEnabled = false;
            EditFlagDocumentMenuEnabled = false;
            DocumentsSelectMenuEnabled = false;
            DocumentsSetTitleMenuEnabled = false;
            DocumentsSetAuthorMenuEnabled = false;
            DocumentsSetSubjectMenuEnabled = false;
            DocumentsSetCategoryMenuEnabled = false;
            DocumentsSetTaxYearMenuEnabled = false;
            DocumentsSetDateTimeAddedMenuEnabled = false;
            DocumentsDeleteMenuEnabled = false;
            ViewSetPreviewPixelDensityMenuEnabled = false;
            ToolsUploadProfilesMenuEnabled = true;

            if (DatabaseSession.PlatformName.Equals(DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                ToolsMoveDatabaseMenuVisible = true;
            }
            else
            {
                ToolsMoveDatabaseMenuVisible = false;
            }

            DocumentsEnabled = true;

            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                SearchTermSnippetsVisible = documentRepository.SearchTermSnippetsSupported;
            }
        }

        private static bool VerifyInsertGranted(bool value)
        {
            if (DatabaseSession.InsertGranted)
            {
                return value;
            }
            else
            {
                return false;
            }
        }

        private static bool VerifyUpdateGranted(bool value)
        {
            if (DatabaseSession.UpdateGranted)
            {
                return value;
            }
            else
            {
                return false;
            }
        }

        private static bool VerifyDeleteGranted(bool value)
        {
            if (DatabaseSession.DeleteGranted)
            {
                return value;
            }
            else
            {
                return false;
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (var font = new Font("Lucida Console", 10))
            {
                e.Graphics.MeasureString(
                    textToPrint,
                    font,
                    e.MarginBounds.Size,
                    StringFormat.GenericTypographic,
                    out int charactersOnPage,
                    out int linesPerPage);
                e.Graphics.DrawString(
                    textToPrint,
                    font,
                    Brushes.Black,
                    e.MarginBounds,
                    StringFormat.GenericTypographic);
                textToPrint = textToPrint.Substring(charactersOnPage);
                e.HasMorePages = textToPrint.Length > 0;
            }
        }

        private void OnDocumentSelectionChanged()
        {
            NotesChanged = false;

            if (CurrentDocumentId > 0)
            {
                try
                {
                    OnLongOperationStarted?.Invoke();

                    using (var documentRepository = DatabaseSession.GetDocumentRepository())
                    {
                        if (FindDocumentsViewState.FindDocumentsParam.FindBySearchTermChecked)
                        {
                            currentDocument = documentRepository.GetDocument(
                                CurrentDocumentId,
                                FindDocumentsViewState.FindDocumentsParam.SearchTerm,
                                !fileCache.IsPdfCached(CurrentDocumentId));
                        }
                        else
                        {
                            currentDocument = documentRepository.GetDocument(
                                CurrentDocumentId,
                                null,
                                !fileCache.IsPdfCached(CurrentDocumentId));
                        }
                    }

                    var cachePdfTask = Task.Run(() => fileCache.AddPdf(
                        currentDocument.Id,
                        currentDocument.Pdf));
                    EditFlagDocumentMenuChecked = Convert.ToBoolean(currentDocument.Flag);
                    PreviousNotes = currentDocument.Notes;  // Must be set before Notes.
                    Notes = currentDocument.Notes;
                    Keywords = currentDocument.Keywords;
                    Text = currentDocument.Text;
                    SearchTermSnippets = currentDocument.SearchTermSnippets;
                    DocumentDataEnabled = true;
                    cachePdfTask.Wait();
                    var pdfFile = fileCache.GetPdfFile(currentDocument.Id);
                    FileExtractMenuEnabled = pdfFile.ContainsAttachments ||
                        pdfFile.ContainsEmbeddedFiles;
                    FileExtractAllAttachmentsMenuEnabled = pdfFile.ContainsAttachments;
                    FileExtractAllEmbeddedFilesMenuEnabled = pdfFile.ContainsEmbeddedFiles;
                    SetPreviewImageForCurrentDocument();
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                }
                finally
                {
                    OnLongOperationFinished?.Invoke();
                }
            }
            else
            {
                PreviousNotes = null;
                EditFlagDocumentMenuChecked = false;
                DocumentDataEnabled = false;
                FileExtractMenuEnabled = false;
                FileExtractAllAttachmentsMenuEnabled = false;
                FileExtractAllEmbeddedFilesMenuEnabled = false;
                Notes = null;
                Keywords = null;
                Text = null;
                SearchTermSnippets = null;
                Preview = null;
            }
        }
        
        private void SetPreviewImageForCurrentDocument()
        {
            OnLongOperationStarted?.Invoke();
            fileCache.CreatePreview(CurrentDocumentId, PreviewPixelDensity);
            Preview = fileCache.GetPreview(CurrentDocumentId, PreviewPixelDensity);
            OnLongOperationFinished?.Invoke();
        }

        /// <summary>
        /// Sets the menu and tool bar items state based on if <c>Notes</c> text changed and if the
        /// <c>Notes</c> text can be undone.
        /// </summary>
        /// <param name="notesChanged">
        /// <c>true</c> or <c>false</c> if <c>Notes</c> text changed.
        /// </param>
        /// <param name="canUndo">
        /// <c>true</c> or <c>false</c> if the user can <c>Undo</c> the previous operation.
        /// </param>
        private void SetStateForNotesChanged(bool notesChanged, bool canUndo)
        {
            FileSaveMenuEnabled = notesChanged;
            DocumentsSelectMenuEnabled = !notesChanged;

            if (notesChanged)
            {
                FileExportMenuEnabled = false;
                DocumentsFindMenuEnabled = false;
                DocumentsSetTitleMenuEnabled = false;
                DocumentsSetAuthorMenuEnabled = false;
                DocumentsSetSubjectMenuEnabled = false;
                DocumentsSetCategoryMenuEnabled = false;
                DocumentsSetTaxYearMenuEnabled = false;
                DocumentsSetDateTimeAddedMenuEnabled = false;
                DocumentsDeleteMenuEnabled = false;
            }
            else
            {
                DocumentsFindMenuEnabled = true;
            }
            
            EditUndoMenuEnabled = canUndo;
            EditRestoreMenuEnabled = notesChanged;
            EditFlagDocumentMenuEnabled = !notesChanged;
        }

        /// <summary>
        /// Gets a list of documents based on the properties in <see cref="FindDocumentsParam"/>.
        /// </summary>
        /// <param name="selectCurrentDocument">
        /// <c>true</c> or <c>false</c> to select the current document after getting documents.
        /// </param>
        private void GetListOfDocuments(bool selectCurrentDocument)
        {
            var findDocumentsParam = FindDocumentsViewState.FindDocumentsParam;
            if (findDocumentsParam != null)
            {
                var currentDocumentId = CurrentDocumentId;
                try
                {
                    OnLongOperationStarted?.Invoke();
                    DocumentsFindMenuEnabled = false;
                    RefreshingDocumentsImageVisible = true;
                    
                    DataTable documents = null;
                    using (var documentRepository = DatabaseSession.GetDocumentRepository())
                    {
                        if (findDocumentsParam.FindBySearchTermChecked)
                        {
                            documents = documentRepository.GetListOfDocumentsBySearchTerm(
                                findDocumentsParam.SearchTerm);
                        }
                        else if (findDocumentsParam.FindBySelectionsChecked)
                        {
                            documents = documentRepository.GetListOfDocuments(
                                findDocumentsParam.Author,
                                findDocumentsParam.Subject,
                                findDocumentsParam.Category,
                                findDocumentsParam.TaxYear);
                        }
                        else if (findDocumentsParam.FindByDateAddedChecked)
                        {
                            documents = documentRepository.GetListOfDocumentsByDateAdded(
                                findDocumentsParam.DateAdded);
                        }
                        else if (findDocumentsParam.FindFlaggedDocumentsChecked)
                        {
                            documents = documentRepository.GetListOfFlaggedDocuments();
                        }
                        else if (findDocumentsParam.AllDocumentsChecked)
                        {
                            documents = documentRepository.GetListOfDocuments();
                        }
                    }

                    if (selectCurrentDocument &&
                        documents.Compare(Documents) ||
                        !selectCurrentDocument)
                    {
                        Documents = documents;
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
                        var columnName = Documents.Columns[0].ColumnName;

                        foreach (DataRow row in Documents.Rows)
                        {
                            var id = Convert.ToInt32(row[columnName].ToString());
                            if (id.Equals(currentDocumentId))
                            {
                                CurrentDocumentId = currentDocumentId;
                            }
                        }
                    }
                    
                    RefreshingDocumentsImageVisible = false;
                    DocumentsFindMenuEnabled = true;
                    OnLongOperationFinished?.Invoke();
                }
            }
        }

        /// <summary>
        /// Processes each checked document.
        /// </summary>
        /// <param name="checkedDocumentAction">
        /// The <see cref="CheckedDocumentAction"/>.
        /// </param>
        /// <param name="value">
        /// The value to be applied to each document processeed or null
        /// (Category and Tax Year only). When performing an export, this value must be the export
        /// target directory.
        /// </param>
        private void ProcessEachCheckedDocument(
            CheckedDocumentAction checkedDocumentAction,
            string value)
        {
            OnLongOperationStarted?.Invoke();
            DocumentsEnabled = false;
            DocumentsProgressBarVisible = true;
            DocumentsProgressBarMinimum = 0;
            DocumentsProgressBarMaximum = CheckedDocumentIds.Count;

            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                foreach (int id in CheckedDocumentIds)
                {
                    var error = false;
                    try
                    {
                        var document = documentRepository.GetDocument(id, null);
                        switch (checkedDocumentAction)
                        {
                            case CheckedDocumentAction.SetTitle:
                                document.Title = value;
                                documentRepository.UpdateDocument(document);
                                break;
                            case CheckedDocumentAction.SetAuthor:
                                document.Author = value;
                                documentRepository.UpdateDocument(document);
                                break;
                            case CheckedDocumentAction.SetSubject:
                                document.Subject = value;
                                documentRepository.UpdateDocument(document);
                                break;
                            case CheckedDocumentAction.SetCategory:
                                document.Category = value;
                                documentRepository.UpdateDocument(document);
                                break;
                            case CheckedDocumentAction.SetTaxYear:
                                document.TaxYear = value;
                                documentRepository.UpdateDocument(document);
                                break;
                            case CheckedDocumentAction.SetDateTimeAdded:
                                document.Added = value;
                                documentRepository.UpdateDocument(document);
                                break;
                            case CheckedDocumentAction.Delete:
                                documentRepository.DeleteDocument(id);
                                fileCache.Delete(id);
                                break;
                            case CheckedDocumentAction.Export:
                                new Commands.ExportDocumentCommand(
                                    id,
                                    new DirectoryInfo(value),
                                    fileCache).Execute(null);
                                break;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        error = true;
                        OnLongOperationFinished?.Invoke();
                        var message = ResourceHelper.GetString(
                            Resources.ResourceManager,
                            "DocumentMayHaveBeenDeletedException",
                            ex.Message,
                            id.ToString());
                        messageBoxService.ShowMessage(message, true);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        error = true;
                        OnLongOperationFinished?.Invoke();
                        var message = ResourceHelper.GetString(
                            Resources.ResourceManager,
                            "DocumentMayHaveBeenDeletedException",
                            ex.Message,
                            id.ToString());
                        messageBoxService.ShowMessage(message, true);
                    }
                    catch (DatabaseException ex)
                    {
                        error = true;
                        OnLongOperationFinished?.Invoke();
                        var message = ResourceHelper.GetString(
                            Resources.ResourceManager,
                            "DefaultDocumentException",
                            ex.Message,
                            id.ToString());
                        messageBoxService.ShowMessage(message, true);
                    }
                    finally
                    {
                        if (error)
                        {
                            OnLongOperationStarted?.Invoke();
                        }

                        OnProgressBarPerformStep?.Invoke();
                    }
                }
            }
  
            OnCheckedDocumentsProcessed?.Invoke();
            DocumentsProgressBarVisible = false;
            DocumentsEnabled = true;
            OnLongOperationFinished?.Invoke();
        }

        /// <summary>
        /// Gets the <see cref="DocumentDataType"/> member for the data bound property of the
        /// <c>TextBox</c> with focus.
        /// </summary>
        /// <returns>The <see cref="DocumentDataType"/> member.</returns>
        private DocumentDataType GetFocusedDocumentDataType()
        {
            DocumentDataType documentDataType = DocumentDataType.None;
            if (NotesFocused)
            {
                documentDataType = DocumentDataType.Notes;
            }
            else if (KeywordsFocused)
            {
                documentDataType = DocumentDataType.Keywords;
            }
            else if (TextFocused)
            {
                documentDataType = DocumentDataType.Text;
            }
            else if (SearchTermSnippetsFocused)
            {
                documentDataType = DocumentDataType.SearchTermSnippets;
            }
            return documentDataType;
        }

        /// <summary>
        /// Gets the text for the <see cref="DocumentDataType"/> member.
        /// </summary>
        /// <param name="documentDataType">The <see cref="DocumentDataType"/> member.</param>
        /// <returns>The text for the <see cref="DocumentDataType"/> member.</returns>
        private string GetDocumentDataText(DocumentDataType documentDataType)
        {
            string result = null;
            switch (documentDataType)
            {
                case DocumentDataType.Notes:
                    result = Notes;
                    break;
                case DocumentDataType.Keywords:
                    result = Keywords;
                    break;
                case DocumentDataType.Text:
                    result = Text;
                    break;
                case DocumentDataType.SearchTermSnippets:
                    result = SearchTermSnippets;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets the selected text for the <see cref="DocumentDataType"/> member.
        /// </summary>
        /// <param name="documentDataType">The <see cref="DocumentDataType"/> member.</param>
        /// <returns>The selected text for the <see cref="DocumentDataType"/> member.</returns>
        private string GetDocumentDataSelectedText(DocumentDataType documentDataType)
        {
            string result = null;
            switch (documentDataType)
            {
                case DocumentDataType.Notes:
                    result = SelectedNotes;
                    break;
                case DocumentDataType.Keywords:
                    result = SelectedKeywords;
                    break;
                case DocumentDataType.Text:
                    result = SelectedText;
                    break;
                case DocumentDataType.SearchTermSnippets:
                    result = SelectedSearchTermSnippets;
                    break;
            }
            return result;
        }

        private void WaitForUploadToFinish()
        {
            while (UploadProgressBarVisible)
            {
                Thread.Sleep(5000);
            }
        }
    }
}
