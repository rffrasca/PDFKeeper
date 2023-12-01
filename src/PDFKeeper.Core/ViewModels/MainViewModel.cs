// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2023 Robert F. Frasca
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

using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;

namespace PDFKeeper.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool fileOpenMenuEnabled;
        private bool fileSaveMenuEnabled;
        private bool fileSaveAsMenuEnabled;
        private bool fileBurstMenuEnabled;
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
        private bool documentsDeleteMenuEnabled;
        private bool viewSetPreviewPixelDensityMenuEnabled;
        private bool toolsMoveDatabaseMenuVisible;
        private DataTable documents;
        private readonly Collection<int> checkedDocumentIds;
        private bool documentsEnabled;
        private int currentDocumentId;
        private bool documentDataEnabled;
        private string notes;
        private bool notesFocused;
        private string keywords;
        private bool keywordsFocused;
        private string text;
        private bool textFocused;
        private string searchTermSnippets;
        private bool searchTermSnippetsFocused;
        private Image preview;
        private bool progressBarVisible;
        private int progressBarMaximum;
        private bool refreshingDocumentsImageVisible;
        private bool flagImageVisible;
        private bool uploadRunningImageVisible;
        private bool uploadRejectedImageVisible;

        public MainViewModel()
        {
            checkedDocumentIds = new Collection<int>();
        }

        public bool FileOpenMenuEnabled
        {
            get => fileOpenMenuEnabled;
            set => SetProperty(ref fileOpenMenuEnabled, value);
        }

        public bool FileSaveMenuEnabled
        {
            get => fileSaveMenuEnabled;
            set => SetProperty(ref fileSaveMenuEnabled, value);
        }

        public bool FileSaveAsMenuEnabled
        {
            get => fileSaveAsMenuEnabled;
            set => SetProperty(ref fileSaveAsMenuEnabled, value);
        }
        
        public bool FileBurstMenuEnabled
        {
            get => fileBurstMenuEnabled;
            set => SetProperty(ref fileBurstMenuEnabled, value);
        }

        public bool FilePrintMenuEnabled
        {
            get => filePrintMenuEnabled;
            set => SetProperty(ref filePrintMenuEnabled, value);
        }

        public bool FilePrintPreviewMenuEnabled
        {
            get => filePrintPreviewMenuEnabled;
            set => SetProperty(ref filePrintPreviewMenuEnabled, value);
        }
        
        public bool FileExportMenuEnabled
        {
            get => fileExportMenuEnabled;
            set => SetProperty(ref fileExportMenuEnabled, value);
        }

        public bool EditUndoMenuEnabled
        {
            get => editUndoMenuEnabled;
            set => SetProperty(ref editUndoMenuEnabled, value);
        }

        public bool EditCutMenuEnabled
        {
            get => editCutMenuEnabled;
            set => SetProperty(ref editCutMenuEnabled, value);
        }

        public bool EditCopyMenuEnabled
        {
            get => editCopyMenuEnabled;
            set => SetProperty(ref editCopyMenuEnabled, value);
        }

        public bool EditPasteMenuEnabled
        {
            get => editPasteMenuEnabled;
            set => SetProperty(ref editPasteMenuEnabled, value);
        }

        public bool EditSelectAllMenuEnabled
        {
            get => editSelectAllMenuEnabled;
            set => SetProperty(ref editSelectAllMenuEnabled, value);
        }

        public bool EditRestoreMenuEnabled
        {
            get => editRestoreMenuEnabled;
            set => SetProperty(ref editRestoreMenuEnabled, value);
        }

        public bool EditAppendDateTimeMenuEnabled
        {
            get => editAppendDateTimeMenuEnabled;
            set => SetProperty(ref editAppendDateTimeMenuEnabled, value);
        }

        public bool EditAppendTextMenuEnabled
        {
            get => editAppendTextMenuEnabled;
            set => SetProperty(ref editAppendTextMenuEnabled, value);
        }

        public bool EditFlagDocumentMenuEnabled
        {
            get => editFlagDocumentMenuEnabled;
            set => SetProperty(ref editFlagDocumentMenuEnabled, value);
        }

        public bool EditFlagDocumentMenuChecked
        {
            get => editFlagDocumentMenuChecked;
            set => SetProperty(ref editFlagDocumentMenuChecked, value);
        }

        public bool DocumentsFindMenuEnabled
        {
            get => documentsFindMenuEnabled;
            set => SetProperty(ref documentsFindMenuEnabled, value);
        }

        public bool DocumentsSelectMenuEnabled
        {
            get => documentsSelectMenuEnabled;
            set => SetProperty(ref documentsSelectMenuEnabled, value);
        }

        public bool DocumentsSetTitleMenuEnabled
        {
            get => documentsSetTitleMenuEnabled;
            set => SetProperty(ref documentsSetTitleMenuEnabled, value);
        }

        public bool DocumentsSetAuthorMenuEnabled
        {
            get => documentsSetAuthorMenuEnabled;
            set => SetProperty(ref documentsSetAuthorMenuEnabled, value);
        }

        public bool DocumentsSetSubjectMenuEnabled
        {
            get => documentsSetSubjectMenuEnabled;
            set => SetProperty(ref documentsSetSubjectMenuEnabled, value);
        }

        public bool DocumentsSetCategoryMenuEnabled
        {
            get => documentsSetCategoryMenuEnabled;
            set => SetProperty(ref documentsSetCategoryMenuEnabled, value);
        }

        public bool DocumentsSetTaxYearMenuEnabled
        {
            get => documentsSetTaxYearMenuEnabled;
            set => SetProperty(ref documentsSetTaxYearMenuEnabled, value);
        }

        public bool DocumentsDeleteMenuEnabled
        {
            get => documentsDeleteMenuEnabled;
            set => SetProperty(ref documentsDeleteMenuEnabled, value);
        }

        public bool ViewSetPreviewPixelDensityMenuEnabled
        {
            get => viewSetPreviewPixelDensityMenuEnabled;
            set => SetProperty(ref viewSetPreviewPixelDensityMenuEnabled, value);
        }

        public bool ToolsMoveDatabaseMenuVisible
        {
            get => toolsMoveDatabaseMenuVisible;
            set => SetProperty(ref toolsMoveDatabaseMenuVisible, value);
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
            set => SetProperty(ref documentsEnabled, value);
        }

        public int CurrentDocumentId 
        {
            get => currentDocumentId;
            set => SetProperty(ref currentDocumentId, value);
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
                EditFlagDocumentMenuEnabled = documentDataEnabled;
                ViewSetPreviewPixelDensityMenuEnabled = documentDataEnabled;
            }
        }

        public string Notes
        {
            get => notes;
            set => SetProperty(ref notes, value);
        }

        public string SelectedNotes { get; set; }
        public string PreviousNotes { get; set; }
        public bool NotesChanged { get; set; }

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

        public string SelectedKeywords { get; set; }

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

        public string SelectedText { get; set; }

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

        public string SelectedSearchTermSnippets { get; set; }

        public bool SearchTermSnippetsFocused
        {
            get => searchTermSnippetsFocused;
            set => searchTermSnippetsFocused = value;
        }

        public Image Preview
        {
            get => preview;
            set => SetProperty(ref preview, value);
        }

        public bool ProgressBarVisible
        {
            get => progressBarVisible;
            set => SetProperty(ref progressBarVisible, value);
        }

        public int ProgressBarMaximum
        {
            get => progressBarMaximum;
            set => SetProperty(ref progressBarMaximum, value);
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

        public bool UploadRunningImageVisible
        {
            get => uploadRunningImageVisible;
            set => SetProperty(ref uploadRunningImageVisible, value);
        }

        public bool UploadRejectedImageVisible
        {
            get => uploadRejectedImageVisible;
            set => SetProperty(ref uploadRejectedImageVisible, value);
        }
    }
}
