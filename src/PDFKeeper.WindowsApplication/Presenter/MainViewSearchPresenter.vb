'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
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

    Public Sub FileDeleteToolStripCommandExecute()
        Try
            ProcessSelectedDocuments(Enums.SelectedDocumentsFunction.Delete)
            view.RefreshSearchResultsView()
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub FileExportToolStripCommandExecute()
        Dim displayService As IMessageDisplayService = New MessageDisplayService
        ProcessSelectedDocuments(Enums.SelectedDocumentsFunction.Export)
        view.SelectNoneInSearchResultsView()
        displayService.ShowInformation(String.Format(CultureInfo.CurrentCulture, _
                                                     My.Resources.ResourceManager.GetString("SelectedFilesHaveBeenExported"), _
                                                     exportFolder))
    End Sub

    Public Sub SearchOptionsTabControlSelected()    ' All/Flagged Documents tab only.
        ResetSearchResultsView()
        Try
            Dim docsDao1 As IDocsDao = New DocsDao
            Dim totalCount As Integer = docsDao1.TotalRecordCount
            Dim docsDao2 As IDocsDao = New DocsDao
            Dim flaggedCount As Integer = docsDao2.FlaggedRecordCount
            view.DBDocumentRecordsCountMessage = _
                String.Format(CultureInfo.CurrentCulture, _
                              My.Resources.ResourceManager.GetString("DocumentRecordsCountMessage"), _
                              totalCount, flaggedCount)
            view.QueryDocumentsEnabled = True
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub SearchStringComboBoxTextChanged()
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

    Public Sub SearchStringComboBoxEnter()
        GetSearchStringHistory()
    End Sub

    Public Sub DoSearch(ByVal useLastSearchString As Boolean)
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
                Dim docsDao As IDocsDao = New DocsDao
                FillSearchResults(docsDao.GetAllRecordsBySearchString(view.SearchString))
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
        GetSearchStringHistory()
    End Sub

    Public Sub AuthorComboBoxDropDown()
        Dim docsDao As IDocsDao = New DocsDao
        If view.SearchOptionsSelectedIndex = 1 Then
            view.Authors1 = docsDao.GetAllAuthors
        ElseIf view.SearchOptionsSelectedIndex = 3 Then
            view.Authors2 = docsDao.GetAllAuthors
        End If
    End Sub

    Public Sub Author1ComboBoxDropDownClosed()
        If view.Author1 IsNot Nothing Then
            Try
                Dim docsDao As IDocsDao = New DocsDao
                FillSearchResults(docsDao.GetAllRecordsByAuthor(view.Author1))
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        Else
            ResetSearchResultsView()
        End If
    End Sub

    Public Sub Subject1ComboBoxDropDown()
        Try
            Dim docsDao As IDocsDao = New DocsDao
            view.Subjects1 = docsDao.GetAllSubjects
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub Subject1ComboBoxDropDownClosed()
        If view.Subject1 IsNot Nothing Then
            Try
                Dim docsDao As IDocsDao = New DocsDao
                FillSearchResults(docsDao.GetAllRecordsBySubject(view.Subject1))
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        Else
            ResetSearchResultsView()
        End If
    End Sub

    Public Sub Author2ComboBoxDropDownClosed()
        If Not view.Author2 Is Nothing Then
            If Not view.Author2 = lastAuthor2Selected Then
                view.ClearSubject2Selection()
            End If
            lastAuthor2Selected = view.Author2
            view.ClearSubject2Selection()
        End If
    End Sub

    Public Sub Subject2ComboBoxDropDown()
        Dim docsDao As IDocsDao = New DocsDao
        view.Subjects2 = docsDao.GetAllSubjectsByAuthor(view.Author2)
    End Sub

    Public Sub Subject2ComboBoxDropDownClosed()
        If view.Subject2 IsNot Nothing Then
            Try
                Dim docsDao As IDocsDao = New DocsDao
                FillSearchResults(docsDao.GetAllRecordsByAuthorAndSubject(view.Author2, _
                                                                          view.Subject2))
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        Else
            ResetSearchResultsView()
        End If
    End Sub

    Public Sub SearchDateTimePickerValueChanged()
        Try
            Dim docsDao As IDocsDao = New DocsDao
            FillSearchResults(docsDao.GetAllRecordsByDateAdded(view.SearchDate))
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub QueryDocumentsButtonClick()
        Try
            Dim docsDao As IDocsDao = New DocsDao
            If view.FlaggedDocumentsOnly Then
                FillSearchResults(docsDao.GetAllFlaggedRecords)
            Else
                If ApplicationPolicy.DisableQueryAllDocuments = False Then
                    FillSearchResults(docsDao.GetAllRecords)
                Else
                    Dim displayService As IMessageDisplayService = New MessageDisplayService
                    displayService.ShowError(My.Resources.FeatureDisabledByPolicy)
                    Exit Sub
                End If
            End If
            view.DBDocumentRecordsCountMessage = Nothing
            view.QueryDocumentsEnabled = False
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
        End Try
    End Sub

    Public Sub SearchResultsDataGridViewSorted()
        searchResultsSortParameters.SortedColumn = view.SearchResultsSortedColumn
        searchResultsSortParameters.SortOrder = view.SearchResultsSortOrder
    End Sub
    
    Public Sub SearchResultsDataGridViewRowsAdded()
        SetTotalRecordsCountLabel()
    End Sub

    Public Sub SearchResultsDataGridViewRowsRemoved()
        SetTotalRecordsCountLabel()
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
                ExportDocument(id)
            End If
            view.DeleteExportProgressPerformStep()
        Next
        view.DeleteExportProgressVisible = False
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    Private Sub DeleteDocument(ByVal id As Integer)
        Dim docsDao As IDocsDao = New DocsDao
        docsDao.DeleteRecordById(id)
    End Sub

    Private Sub ExportDocument(ByVal id As Integer)
        Try
            Dim pdfFile As New PdfFile(Path.Combine(exportFolder, _
                                                    My.Application.Info.ProductName & id & ".pdf"))
            Dim suppDataXml As String = Path.Combine(exportFolder, _
                                                     My.Application.Info.ProductName & id & ".xml")
            Dim suppData As New PdfFileSupplementalData
            Dim docsDaoPdf As IDocsDao = New DocsDao
            docsDaoPdf.GetPdfById(id, pdfFile.FullName)
            Dim docsDaoNotes As IDocsDao = New DocsDao
            Dim dataTableNotes As DataTable = docsDaoNotes.GetNotesById(id)
            suppData.Notes = Convert.ToString(dataTableNotes.Rows(0)("doc_notes"), _
                                              CultureInfo.CurrentCulture)
            Dim docsDaoFlagState As IDocsDao = New DocsDao
            Dim dataTableFlagState As DataTable = docsDaoFlagState.GetFlagStateById(id)
            suppData.FlagState = Convert.ToInt32(dataTableFlagState.Rows(0)("doc_flag"), _
                                                 CultureInfo.CurrentCulture)
            SerializerHelper.FromObjToXml(suppData, suppDataXml)
        Catch ex As InvalidOperationException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(String.Format(CultureInfo.CurrentCulture, _
                                                   My.Resources.ResourceManager.GetString( _
                                                       "ExportDocumentRecordMayHaveBeenDeleted"), _
                                                   ex.Message, id))
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

    Private Sub ResetSearchResultsView()
        If view.SearchResults IsNot Nothing Then
            view.SearchResults.Reset()
        End If
        view.ResetSearchResultsViewHeader()
    End Sub

    Private Sub GetSearchStringHistory()
        view.SearchStringHistory = searchStringHistory.ToArray(False)
    End Sub

    Private Sub FillSearchResults(ByVal dataTable As DataTable)
        view.SearchResultsFullView = False
        view.SearchResultsViewEnabled = False
        view.SearchResults = dataTable
        view.SortSearchResults(searchResultsSortParameters.SortColumnIndex, _
                               searchResultsSortParameters.SortDirection)
        If view.SearchResultsViewRowCount > 0 Then
            If My.Settings.SearchResultsSelectLastRow Then
                view.SelectSearchResultsViewLastRow()
            End If
        End If
    End Sub

    Private Sub SetTotalRecordsCountLabel()
        view.TotalRecordsCountLabel = view.SearchResultsViewRowCount
    End Sub
End Class
