'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
Imports System.Collections.ObjectModel

Public Interface IMainView
    Inherits IViewCommon
    Property ViewToolBarMenuItemChecked As Boolean
    Property ViewStatusBarMenuItemChecked As Boolean
    Property ToolStripVisible As Boolean
    Property SplitterDistance As Integer
    Property DocumentRetrievalGroupEnabled As Boolean
    ReadOnly Property DocumentRetrievalChoicesListEnabled As Boolean
    Property DocumentRetrievalChoiceSelectedIndex As Integer
    Property SearchTerm As String
    Property SearchTermItems As Object
    Property SearchTermEnabled As Boolean
    Property FindBySearchTermEnabled As Boolean
    Property AuthorEnabled As Boolean
    Property SubjectEnabled As Boolean
    Property CategoryEnabled As Boolean
    Property TaxYearEnabled As Boolean
    Property ClearSelectionsEnabled As Boolean
    Property FindBySelectionsEnabled As Boolean
    ReadOnly Property DateAdded As String
    Property DateAddedEnabled As Boolean
    Property DocumentList As DataTable
    Property DocumentListViewEnabled As Boolean
    ReadOnly Property DocumentListRowCount As Integer
    Property DocumentListColumn0AutoSizeMode As DataGridViewAutoSizeColumnMode
    ReadOnly Property CheckedDocumentIdItems As Collection(Of Integer)
    ReadOnly Property SelectedDocumentId As Integer
    Property FlagState As Integer
    Property DocumentTabControlSelectedIndex As Integer
    Property DocumentTabControlEnabled As Boolean
    ReadOnly Property DocumentTabControlSelectedTabTextBoxText As String
    ReadOnly Property TextFromFocusedTextBox As String
    Property Notes As String
    ReadOnly Property NotesSelectionLength As Integer
    Property NotesChanged As Boolean
    ReadOnly Property NotesFocused As Boolean
    ReadOnly Property NotesCanUndo As Boolean
    ReadOnly Property NotesReadOnly As Boolean
    Property Keywords As String
    Property Preview As Image
    Property Text As String
    Property SearchTermSnippets As String
    Property StatusStripVisible As Boolean
    Property DocumentListCountStatusText As String
    Property ProgressBarVisible As Boolean
    Property ProgressBarMaximum As Integer
    Property FlagImageVisible As Boolean
    Property UploadRunningImageVisible As Boolean
    Property UploadRejectedImageVisible As Boolean
    Sub RemoveListAllDocumentsChoice()
    Sub SelectAllDocumentCheckBoxes(ByVal check As Boolean)
    Sub SelectDocument(ByVal id As Integer)
    Sub ScrollToEndOfNotesText()
    Sub PerformStepOnProgressBar()
    Sub SetErrorProviderMessage(ByVal message As String)
    Function ShowOpenTextFileDialog() As String
    Function ShowSaveFileDialog(ByVal filter As String, ByVal fileNamePrefill As String) As String
    Function ShowFolderBrowserDialog(ByVal description As String) As String
    Sub PrintSelectectedTextBoxText(ByVal usePreview As Boolean)
End Interface
