'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2018 Robert F. Frasca
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
Public Interface IMainViewSearch
    ReadOnly Property SearchOptionsSelectedIndex As Integer
    Property SearchString As String
    Property SearchStringErrorProviderMessage As String
    Property SearchStringHistory As Object
    Property SearchEnabled As Boolean
    Property Authors1 As DataTable
    ReadOnly Property Author1 As String
    Property Subjects1 As DataTable
    ReadOnly Property Subject1 As String
    Property Authors2 As DataTable
    ReadOnly Property Author2 As String
    Property Subjects2 As DataTable
    ReadOnly Property Subject2 As String
    ReadOnly Property SearchDate As String
    Property DBDocumentRecordsCountMessage As String
    Property QueryAllDocumentsVisible As Boolean
    Property QueryAllDocumentsEnabled As Boolean
    Property SearchResultsFullView As Boolean
    Property SearchResultsViewEnabled As Boolean
    Property SearchResults As DataTable
    ReadOnly Property SearchResultsSortedColumn As DataGridViewColumn
    ReadOnly Property SearchResultsSortOrder As SortOrder
    ReadOnly Property SearchResultsViewRowCount As Integer
    ReadOnly Property SelectedSearchResultsIds As Object
    ReadOnly Property SelectedSearchResultsIdsCount As Integer
    Property TotalRecordsCountLabel As String
    Property DeleteExportProgressVisible As Boolean
    Property DeleteExportProgressMaximum As Integer
    Sub ClearSubject2Selection()
    Sub ResetSearchResultsViewHeader()
    Sub SortSearchResults(ByVal sortColumnIndex As Integer, _
                          ByVal sortDirection As ListSortDirection)
    Sub SelectSearchResultsViewLastRow()
    Sub RefreshSearchResultsView()
    Sub SelectNoneInSearchResultsView()
    Sub DeleteExportProgressPerformStep()
End Interface
