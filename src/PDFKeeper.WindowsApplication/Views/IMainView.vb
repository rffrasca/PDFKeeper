'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Interface IMainView
    Inherits ICommonView
    ReadOnly Property SearchFunctionsEnabled As Boolean
    ReadOnly Property SelectedSearchFunction As Integer
    Property SearchTextControlEnabled As Boolean
    Property SearchTextHistory As Object
    Property SearchText As String
    Property SearchTextErrorProviderMessage As String
    Property SearchEnabled As Boolean
    Property AuthorEnabled As Boolean
    Property SubjectEnabled As Boolean
    Property CategoryEnabled As Boolean
    Property ClearSelectionsEnabled As Boolean
    Property SearchBySelectionsEnabled As Boolean
    Property SearchDatePickerEnabled As Boolean
    ReadOnly Property SearchDate As String
    Property SearchResultsEnabled As Boolean
    Property SearchResults As DataTable
    ReadOnly Property SearchResultsSortedColumn As DataGridViewColumn
    ReadOnly Property SearchResultsSortOrder As SortOrder
    ReadOnly Property SearchResultsRowCount As Integer
    ReadOnly Property SelectedSearchResultsIds As Object
    ReadOnly Property SelectedSearchResultsIdsCount As Integer
    ReadOnly Property DocumentRecordId As Integer
    Property DocumentRecordFlagState As Integer
    Property DocumentRecordPanelEnabled As Boolean
    Property DocumentRecordPanelSelectedTab As Integer
    ReadOnly Property TextElementSelectedText As String
    Property DocumentRecordNotes As String
    Property DocumentRecordNotesChanged As Boolean
    Property DocumentRecordKeywords As String
    Property DocumentPreview As System.Drawing.Image
    Property DocumentText As String
    Property DeleteExportProgressVisible As Boolean
    Property DeleteExportProgressMaximum As Integer
    Property UploadRunningVisible As Boolean
    Property UploadFolderErrorVisible As Boolean
    Property UploadStagingFolderErrorVisible As Boolean
    Property FlaggedDocumentsExistVisible As Boolean
    Property FlaggedDocumentsCheckTimerEnabled As Boolean
    Sub RemoveAllDocumentsFromSearchFunctions()
    Sub SelectSearchResultsLastRow()
    Sub SelectSearchResultRowById(ByVal id As Integer)
    Sub SelectDeselectAllSearchResults(ByVal selectionState _
                                       As SelectionState)
    Sub SortSearchResults(ByVal sortColumnIndex As Integer, _
                          ByVal sortDirection As ListSortDirection)
    Sub RefreshSearchResults()
    Sub ResetSearchResultsHeader()
    Sub ScrollToEndInNotesElement()
    Sub DeleteExportProgressPerformStep()
End Interface
