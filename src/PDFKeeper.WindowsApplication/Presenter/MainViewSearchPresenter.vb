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
Public Class MainViewSearchPresenter
    Private view As IMainViewSearch
    Private searchStringHistory As New GenericList(Of String)
    Private lastSearchString As String = String.Empty
    Private lastAuthor2Selected As String
    Private searchResultsSortParameters As New SearchResultsSortParameters
    Private exportFolder As String

    Public Sub New(view As IMainViewSearch)
        Me.view = view
    End Sub

    Public Sub ValidateSearchString()
        view.SearchString = view.SearchString.TrimStart
        If view.SearchString.Length > 0 Then
            If view.SearchString.ValidateProperUsageOfQueryOperators Then
                view.SearchStringErrorProviderMessage = Nothing
                view.SearchEnabled = True
            Else
                view.SearchStringErrorProviderMessage = _
                    My.Resources.SearchStringImproperUsageOfQueryOperators
                view.SearchEnabled = False
            End If
        Else
            view.SearchStringErrorProviderMessage = Nothing
            view.SearchEnabled = False
        End If
    End Sub

    Public Sub GetSearchStringHistory()
        view.SearchStringHistory = searchStringHistory.ToArray(False)
    End Sub

    Public Sub GetSearchResultsBySearchString(ByVal useLastSearchString As Boolean)
        If useLastSearchString Then
            view.SearchString = lastSearchString
        End If
        If view.SearchString.Length > 0 Then
            Try
                ' Need to capture view.SearchString into a new string and set
                ' view.SearchString to that string after the query to prevent
                ' the Search String combox box from selecting the first string
                ' in the drop down list that contains the string in the text
                ' box after the query.
                Dim currentSearchString As String = view.SearchString
                Dim queryService As IQueryService = Nothing
                QueryServiceHelper.SetQueryService(queryService)
                FillSearchResults(queryService.GetSearchResultsBySearchString(view.SearchString))
                view.SearchString = currentSearchString
                If view.SearchResultsViewRowCount > 0 Then
                    searchStringHistory.Add(view.SearchString)
                End If
                lastSearchString = view.SearchString
                view.SearchEnabled = False
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        Else
            ResetSearchResultsView()
        End If
    End Sub

    Public Sub GetAuthors()
        If view.SearchOptionsSelectedIndex = 1 Then
            SharedPresenterQueries.GetAuthors(view.Authors1)
        ElseIf view.SearchOptionsSelectedIndex = 3 Then
            SharedPresenterQueries.GetAuthors(view.Authors2)
        End If
    End Sub

    Public Sub GetSearchResultsByAuthor()
        If view.Author1 IsNot Nothing Then
            Try
                Dim queryService As IQueryService = Nothing
                QueryServiceHelper.SetQueryService(queryService)
                FillSearchResults(queryService.GetSearchResultsByAuthor(view.Author1))
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        Else
            ResetSearchResultsView()
        End If
    End Sub

    Public Sub OnAuthor2Changed()
        If Not view.Author2 Is Nothing Then
            If Not view.Author2 = lastAuthor2Selected Then
                view.ClearSubject2Selection()
            End If
            lastAuthor2Selected = view.Author2
            view.ClearSubject2Selection()
        End If
    End Sub

    Public Sub GetSubjects()
        Try
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            view.Subjects1 = queryService.GetSubjects
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub GetSearchResultsBySubject()
        If view.Subject1 IsNot Nothing Then
            Try
                Dim queryService As IQueryService = Nothing
                QueryServiceHelper.SetQueryService(queryService)
                FillSearchResults(queryService.GetSearchResultsBySubject(view.Subject1))
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        Else
            ResetSearchResultsView()
        End If
    End Sub

    Public Sub GetSubjectsByAuthor()
        SharedPresenterQueries.GetSubjectsByAuthor(view.Author2, view.Subjects2)
    End Sub

    Public Sub GetSearchResultsByAuthorAndSubject()
        If view.Subject2 IsNot Nothing Then
            Try
                Dim queryService As IQueryService = Nothing
                QueryServiceHelper.SetQueryService(queryService)
                FillSearchResults(queryService.GetSearchResultsByAuthorAndSubject(view.Author2, _
                                                                                  view.Subject2))
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        Else
            ResetSearchResultsView()
        End If
    End Sub

    Public Sub GetSearchResultsByDateAdded()
        Try
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            FillSearchResults(queryService.GetSearchResultsByDateAdded(view.SearchDate))
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub GetDBDocumentRecordsCount()
        ResetSearchResultsView()
        If ApplicationPolicy.DisableQueryAllDocuments Then
            view.DBDocumentRecordsCountMessage = My.Resources.FeatureDisabledByPolicy
            view.QueryAllDocumentsVisible = False
        Else
            view.QueryAllDocumentsVisible = True
            Try
                Dim queryService As IQueryService = Nothing
                QueryServiceHelper.SetQueryService(queryService)
                view.DBDocumentRecordsCountMessage = _
                    String.Format(CultureInfo.CurrentCulture, _
                                  My.Resources.ResourceManager.GetString("DBDocumentRecordsCountMessage"), _
                                  queryService.DBDocumentRecordsCount)
                view.QueryAllDocumentsEnabled = True
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        End If
    End Sub

    Public Sub GetAllDBDocumentRecords()
        Try
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            FillSearchResults(queryService.GetAllDBDocumentRecords)
            view.DBDocumentRecordsCountMessage = Nothing
            view.QueryAllDocumentsEnabled = False
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub SetSearchResultsSortParameters()
        searchResultsSortParameters.SortedColumn = view.SearchResultsSortedColumn
        searchResultsSortParameters.SortOrder = view.SearchResultsSortOrder
    End Sub

    Public Sub DeleteSelectedDocuments()
        Try
            ProcessSelectedDocuments(Enums.SelectedDocumentsFunction.Delete)
            view.RefreshSearchResultsView()
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub ExportSelectedDocuments()
        Dim displayService As IMessageDisplayService = New MessageDisplayService
        ProcessSelectedDocuments(Enums.SelectedDocumentsFunction.Export)
        view.SelectNoneInSearchResultsView()
        displayService.ShowInformation(String.Format(CultureInfo.CurrentCulture, _
                                                     My.Resources.ResourceManager.GetString("SelectedFilesHaveBeenExported"), _
                                                     exportFolder))
    End Sub

    Private Sub ResetSearchResultsView()
        If view.SearchResults IsNot Nothing Then
            view.SearchResults.Reset()
        End If
        view.ResetSearchResultsViewHeader()
    End Sub

    Private Sub FillSearchResults(ByVal dataTable As DataTable)
        view.SearchResultsFullView = False
        view.SearchResultsViewEnabled = False
        view.SearchResults = dataTable
        view.SortSearchResults(searchResultsSortParameters.SortColumnIndex, _
                               searchResultsSortParameters.SortDirection)
        SelectSearchResultsViewLastRowIfOptionSelected()
    End Sub

    Private Sub SelectSearchResultsViewLastRowIfOptionSelected()
        If view.SearchResultsViewRowCount > 0 Then
            If My.Settings.SearchResultsSelectLastRow Then
                view.SelectSearchResultsViewLastRow()
            End If
        End If
    End Sub

    Private Sub ProcessSelectedDocuments(ByVal functionToPerform As Enums.SelectedDocumentsFunction)
        If functionToPerform = Enums.SelectedDocumentsFunction.Export Then
            exportFolder = FileExportToolStripCommand.ExportFolder
        End If
        view.DeleteExportProgressVisible = True
        view.DeleteExportProgressMaximum = view.SelectedSearchResultsIdsCount
        For Each id As Object In view.SelectedSearchResultsIds
            Dim idToProcess As Integer = CInt(id)
            If functionToPerform = Enums.SelectedDocumentsFunction.Delete Then
                DeleteDocument(idToProcess)
            ElseIf functionToPerform = Enums.SelectedDocumentsFunction.Export Then
                ExportDocumentPdf(id)
                ExportDocumentPdfText(id)
            End If
            view.DeleteExportProgressPerformStep()
        Next
        view.DeleteExportProgressVisible = False
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    Private Sub DeleteDocument(ByVal id As Integer)
        Dim nonQueryService As INonQueryService = Nothing
        NonQueryServiceHelper.SetNonQueryService(nonQueryService)
        nonQueryService.DeleteDocument(id)
    End Sub

    Private Sub ExportDocumentPdf(ByVal id As Integer)
        Try
            Dim pdfFile As New PdfFile(Path.Combine(exportFolder, _
                                                My.Application.Info.ProductName & id & ".pdf"))
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            queryService.GetDocumentPdf(id, pdfFile.FullName)
        Catch ex As InvalidOperationException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(String.Format(CultureInfo.CurrentCulture, _
                                                   My.Resources.ResourceManager.GetString( _
                                                       "ExportDocumentRecordMayHaveBeenDeleted"), _
                                                   ex.Message, id))
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(String.Format(CultureInfo.CurrentCulture, _
                                                   My.Resources.ResourceManager.GetString( _
                                                       "DocumentIdException"), _
                                                   id, ex.Message))
        End Try
    End Sub

    Private Sub ExportDocumentPdfText(ByVal id As Integer)
        Try
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            Dim dataTableNotes As DataTable = queryService.GetDocumentNotes(id)
            Dim documentNotes As String = Convert.ToString(dataTableNotes.Rows(0)("doc_notes"), _
                                                           CultureInfo.CurrentCulture)
            If documentNotes.Length > 0 Then
                documentNotes.SaveToFile(Path.Combine(exportFolder, _
                                                      My.Application.Info.ProductName & id & ".txt"))
            End If
        Catch ex As IndexOutOfRangeException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(String.Format(CultureInfo.CurrentCulture, _
                                                   My.Resources.ResourceManager.GetString( _
                                                       "ExportDocumentRecordMayHaveBeenDeleted"), _
                                                   ex.Message, id))
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(String.Format(CultureInfo.CurrentCulture, _
                                                   My.Resources.ResourceManager.GetString( _
                                                       "DocumentIdException"), _
                                                   id, ex.Message))
        End Try
    End Sub

    Public Sub SetTotalRecordsCountLabel()
        view.TotalRecordsCountLabel = view.SearchResultsViewRowCount
    End Sub
End Class
