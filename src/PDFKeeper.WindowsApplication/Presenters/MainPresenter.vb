'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
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
Public Class MainPresenter
    Implements IDisposable
    Private view As IMainView
    Private uploadDirInfo As New DirectoryInfo(UserProfile.UploadPath)
    Private uploadStagingDirInfo As New DirectoryInfo( _
        UserProfile.UploadStagingPath)
    Private searchStringHistory As New GenericList(Of String)
    Private message As IMessageDisplayService = New MessageDisplayService
    Private question As IQuestionDisplayService = New QuestionDisplayService
    Private folderBrowser As IFolderBrowserDisplayService = New FolderBrowserDisplayService
    Private setCategory As ISetCategoryDisplayService =
        New SetCategoryDisplayService
    Private pdfViewer As IPdfViewerService = New PdfViewerService
    Private searchResultsSortParameters As New DataGridViewSortParameters
    Private fileHashes As New GenericDictionaryList(Of String, String)
    Private cachePathName As CacheFilePathName
    Private refreshFlag As Boolean
    Private lastSearchString As String = String.Empty
    Private lastAuthorPairSelected As String
    Private lastDocumentNotes As String
    Private preUpdateDocumentNotes As String

    Public Sub New(view As IMainView)
        Me.view = view
    End Sub

#Region "View ToolStrip Members"
    Public Sub OpenSelectedDocumentPdf()
        cachePathName = New CacheFilePathName(view.DocumentRecordId)
        pdfViewer.Open(cachePathName.Pdf, My.Settings.OpenPdfWithDefaultApplication)
    End Sub

    Public Sub SaveSelectedDocumentNotes()
        Try
            view.SetCursor(True)
            GetDocumentRecordNotes(False)
            If preUpdateDocumentNotes = lastDocumentNotes Then
                UpdateDocumentRecordNotes()
            Else
                Clipboard.SetText(view.DocumentRecordNotes.Trim)
                GetDocumentRecordNotes(True)
                view.SetCursor(False)
                message.Show(My.Resources.UnableToSaveDocumentNotes, True)
            End If
            view.ScrollToEndInNotesElement()
        Catch ex As IndexOutOfRangeException
            view.SetCursor(False)
            message.Show(String.Format(CultureInfo.CurrentCulture,
                                              My.Resources.ResourceManager.GetString(
                                                  "DocumentRecordMayHaveBeenDeleted"),
                                              ex.Message), True)
            ClearDocumentRecord()
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub SaveSelectedDocumentPdfOrTextAs()
        Dim targetExtension As String
        Dim targetFilePath As String
        cachePathName = New CacheFilePathName(view.DocumentRecordId)
        Dim pdfInfo As New PdfFileInfo(cachePathName.Pdf)
        Dim pdfInfoProperties As New PdfInformationPropertiesReader(pdfInfo.FullName)
        If view.TextElementSelectedText Is Nothing Then
            targetExtension = "pdf"
        Else
            targetExtension = "txt"
        End If
        Using fileSelect As IFileSelectDisplayService = New FileSelectDisplayService
            fileSelect.FileName = pdfInfoProperties.Title
            fileSelect.Filter = targetExtension
            targetFilePath = fileSelect.ShowSave()
        End Using
        If targetFilePath.Length > 0 Then
            view.SetCursor(True)
            If view.TextElementSelectedText Is Nothing Then
                pdfInfo.CopyTo(targetFilePath)
            Else
                view.TextElementSelectedText.WriteToFile(targetFilePath)
            End If
            view.SetCursor(False)
            message.Show(String.Format(
                                CultureInfo.CurrentCulture,
                                My.Resources.ResourceManager.GetString("FileSaved"),
                                targetFilePath), False)
        End If
    End Sub

    Public Sub PrintTextForSelectedDocument()
        view.TextElementSelectedText.Print()
    End Sub

    Public Sub PrintPreviewTextForSelectedDocument()
        view.TextElementSelectedText.PrintPreview(My.Settings.MainSize)
    End Sub

    Public Sub SelectAllSearchResults()
        view.SelectDeselectAllSearchResults(SelectionState.SelectAll)
    End Sub

    Public Sub DeselectAllSearchResults()
        view.SelectDeselectAllSearchResults(SelectionState.DeselectAll)
    End Sub

    Public Sub SetCategoryOnSelectedSearchResults()
        Dim newCategory As String = setCategory.Show
        If newCategory IsNot Nothing Then
            Try
                view.SetCursor(True)
                Dim processPresenter As _
                    New MainSelectedSearchResultsProcessPresenter(view,
                                                                  SelectedDocumentsFunction.SetClearCategory,
                                                                  newCategory)
                processPresenter.ProcessSelectedSearchResults()
                view.SetCursor(False)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        End If
    End Sub

    Public Sub DeleteSelectedSearchResults()
        If question.Show(My.Resources.DeleteSelectedDocuments,
                                False) = DialogResult.Yes Then
            Try
                view.SetCursor(True)
                Dim processPresenter As _
                    New MainSelectedSearchResultsProcessPresenter(view,
                                                                  SelectedDocumentsFunction.Delete,
                                                                  Nothing)
                processPresenter.ProcessSelectedSearchResults()
                view.SetCursor(False)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        End If
    End Sub

    Public Sub ExportSelectedSearchResults()
        folderBrowser.Description = My.Resources.SelectExportFolder
        Dim exportFolder As String = folderBrowser.Show
        If exportFolder IsNot Nothing Then
            Dim processPresenter As MainSelectedSearchResultsProcessPresenter = Nothing
            Try
                view.SetCursor(True)
                processPresenter =
                    New MainSelectedSearchResultsProcessPresenter(view,
                                                                  SelectedDocumentsFunction.Export,
                                                                  exportFolder)
                processPresenter.ProcessSelectedSearchResults()
                view.SetCursor(False)
                message.Show(String.Format(CultureInfo.CurrentCulture,
                                           My.Resources.ResourceManager.GetString(
                                           "SelectedFilesHaveBeenExported"),
                                           processPresenter.ExportFolderPath),
                             False)
                view.SetCursor(False)
            Catch ex As InvalidOperationException
                view.SetCursor(False)
                message.Show(String.Format(CultureInfo.CurrentCulture,
                                                  My.Resources.ResourceManager.GetString(
                                                      "ExportDocumentRecordMayHaveBeenDeleted"),
                                                  ex.Message,
                                                  processPresenter.IdBeingProcessed), True)
            Catch ex As IndexOutOfRangeException
                view.SetCursor(False)
                message.Show(String.Format(CultureInfo.CurrentCulture,
                                                  My.Resources.ResourceManager.GetString(
                                                      "ExportDocumentRecordMayHaveBeenDeleted"),
                                                  ex.Message,
                                                  processPresenter.IdBeingProcessed), True)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(String.Format(CultureInfo.CurrentCulture,
                                                  My.Resources.ResourceManager.GetString(
                                                      "DocumentIdException"),
                                                  ex.Message,
                                                  processPresenter.IdBeingProcessed), True)
            End Try
        End If
    End Sub

    Public Sub RestoreSelectedDocumentNotes()
        view.DocumentRecordPanelSelectedTab = 0
        view.DocumentRecordNotes = lastDocumentNotes
        lastDocumentNotes = view.DocumentRecordNotes
        view.ScrollToEndInNotesElement()
    End Sub

    Public Sub AppendDateTimeUserNameToSelectedDocumentNotes()
        view.ScrollToEndInNotesElement()
        view.DocumentRecordNotes =
            view.DocumentRecordNotes.AppendDateTimeAndTextToString(My.Settings.Username)
        view.ScrollToEndInNotesElement()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
        "CA1726:UsePreferredTerms", MessageId:="Flag")>
    Public Sub SetFlagStateOnSelectedDocument()
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                model.UpdateFlagStateById(view.DocumentRecordId,
                                          view.DocumentRecordFlagState)
            End Using
            view.SetCursor(False)
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub RefreshSearchResults()
        refreshFlag = True
        SearchOptionSelected()
    End Sub

    Public Sub ReloadDocumentPreview()
        view.SetCursor(True)
        Task.Run(Sub() GetDocumentPreview())
        view.SetCursor(False)
    End Sub
#End Region

#Region "View Search Options Members"
    Public Sub SearchOptionSelected()
        Dim lastSelectedId As Integer = view.DocumentRecordId
        If view.SelectedSearchOption = 0 Then
            GetSearchResultsByString(True)
        ElseIf view.SelectedSearchOption = 1 Then
            GetSearchResultsByAuthor()
        ElseIf view.SelectedSearchOption = 2 Then
            GetSearchResultsBySubject()
        ElseIf view.SelectedSearchOption = 3 Then
            GetSearchResultsByAuthorAndSubject()
        ElseIf view.SelectedSearchOption = 4 Then
            GetSearchResultsByCategory()
        ElseIf view.SelectedSearchOption = 5 Then
            GetSearchResultsByDateAdded()
        ElseIf view.SelectedSearchOption = 6 Then
            If refreshFlag Then
                QueryAllOrFlaggedDocumentRecords()
            Else
                ShowTotalAndFlaggedRecordCounts()
            End If
        End If
        If refreshFlag Then
            view.SelectSearchResultRowById(lastSelectedId)
        End If
        refreshFlag = False
    End Sub

    Public Sub GetSearchStringHistory()
        view.SearchStringHistory = searchStringHistory.ToArray(False)
    End Sub

    Public Sub SearchStringTextChanged()
        view.SearchString = view.SearchString.TrimStart
        If view.SearchString.Length > 0 Then
            If view.SearchString.VerifyProperUsageOfQueryOperators Then
                view.SearchStringErrorProviderMessage = Nothing
                view.SearchEnabled = True
            Else
                view.SearchStringErrorProviderMessage =
                    My.Resources.SearchStringImproperUsageOfQueryOperators
                view.SearchEnabled = False
            End If
        Else
            view.SearchStringErrorProviderMessage = Nothing
            view.SearchEnabled = False
        End If
    End Sub

    Public Sub GetSearchResultsByString(ByVal useLastSearchString As Boolean)
        If useLastSearchString Then
            view.SearchString = lastSearchString
        End If
        If view.SearchString.Length > 0 Then
            Try
                view.SetCursor(True)
                ' Need to capture view.SearchString into a new string and set
                ' view.SearchString to that string after the query to prevent
                ' the Search String combox box from selecting the first string
                ' in the drop down list that contains the string in the text
                ' box after the query.
                Dim currentSearchString As String = view.SearchString
                Using model As IDocumentRepository = New DocumentRepository
                    FillSearchResults(model.GetAllRecordsBySearchString(view.SearchString))
                End Using
                view.SearchString = currentSearchString
                If view.SearchResultsRowCount > 0 Then
                    searchStringHistory.Add(view.SearchString)
                End If
                lastSearchString = view.SearchString
                view.SearchEnabled = False
                view.SetCursor(False)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        Else
            ResetSearchResults()
        End If
        GetSearchStringHistory()
    End Sub

    Public Sub GetSearchResultsByAuthor()
        If view.Author IsNot Nothing Then
            Try
                view.SetCursor(True)
                Using model As IDocumentRepository = New DocumentRepository
                    FillSearchResults(model.GetAllRecordsByAuthor(view.Author))
                End Using
                view.SetCursor(False)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        Else
            ResetSearchResults()
        End If
    End Sub

    Public Sub GetSearchResultsBySubject()
        If view.Subject IsNot Nothing Then
            Try
                view.SetCursor(True)
                Using model As IDocumentRepository = New DocumentRepository
                    FillSearchResults(model.GetAllRecordsBySubject(view.Subject))
                End Using
                view.SetCursor(False)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        Else
            ResetSearchResults()
        End If
    End Sub

    Public Sub ClearSubjectPairedSelection()
        If Not view.AuthorPaired Is Nothing Then
            If Not view.AuthorPaired = lastAuthorPairSelected Then
                view.SubjectPaired = Nothing
            End If
            lastAuthorPairSelected = view.AuthorPaired
            view.SubjectPaired = Nothing
        End If
    End Sub

    Public Sub GetSearchResultsByAuthorAndSubject()
        If view.SubjectPaired IsNot Nothing Then
            Try
                view.SetCursor(True)
                Using model As IDocumentRepository = New DocumentRepository
                    FillSearchResults(
                        model.GetAllRecordsByAuthorAndSubject(view.AuthorPaired,
                                                              view.SubjectPaired))
                End Using
                view.SetCursor(False)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        Else
            ResetSearchResults()
        End If
    End Sub

    Public Sub GetSearchResultsByCategory()
        If view.Category IsNot Nothing Then
            Try
                view.SetCursor(True)
                Using model As IDocumentRepository = New DocumentRepository
                    FillSearchResults(model.GetAllRecordsByCategory(view.Category))
                End Using
                view.SetCursor(False)
            Catch ex As OracleException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        Else
            ResetSearchResults()
        End If
    End Sub

    Public Sub GetSearchResultsByDateAdded()
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                FillSearchResults(model.GetAllRecordsByDateAdded(view.SearchDate))
            End Using
            view.SetCursor(False)
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub QueryAllOrFlaggedDocumentRecords()
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                If view.FlaggedDocumentsOnly Then
                    FillSearchResults(model.GetAllFlaggedRecords)
                Else
                    If ApplicationPolicy.DisableQueryAllDocuments = False Then
                        FillSearchResults(model.GetAllRecords)
                    Else
                        view.SetCursor(False)
                        message.Show(My.Resources.FeatureDisabledByPolicy,
                                            False)
                        Exit Sub
                    End If
                End If
            End Using
            view.DBDocumentRecordsCountMessage = Nothing
            view.QueryDocumentsEnabled = False
            view.SetCursor(False)
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub ShowTotalAndFlaggedRecordCounts()
        ResetSearchResults()
        Try
            view.SetCursor(True)
            Using modelInstance1 As IDocumentRepository = New DocumentRepository
                Dim totalCount As Integer = modelInstance1.GetTotalRecordsCount
                Using modelInstance2 As IDocumentRepository = New DocumentRepository
                    Dim flaggedCount As Integer = modelInstance2.GetFlaggedRecordsCount
                    view.DBDocumentRecordsCountMessage =
                        String.Format(CultureInfo.CurrentCulture,
                                      My.Resources.ResourceManager.GetString(
                                          "DocumentRecordsCountMessage"),
                                      totalCount,
                                      flaggedCount)
                End Using
            End Using
            view.QueryDocumentsEnabled = True
            view.SetCursor(False)
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub
#End Region

#Region "View Search Results Members"
    Public Sub SortSearchResults()
        searchResultsSortParameters.SortedColumn = view.SearchResultsSortedColumn
        searchResultsSortParameters.SortOrder = view.SearchResultsSortOrder
    End Sub

    Public Sub SelectSearchResult()
        If view.DocumentRecordId > 0 Then
            Dim tasks(6) As Task
            tasks(1) = Task.Run(Sub() GetDocumentRecordPdf())
            tasks(2) = Task.Run(Sub() GetDocumentRecordNotes(True))
            tasks(3) = Task.Run(Sub() GetDocumentRecordKeywords())
            tasks(4) = Task.Run(Sub() GetDocumentRecordFlagState())
            Try
                tasks(1).Wait()
            Catch ex As AggregateException
                For Each e In ex.InnerExceptions
                    Dim message As String
                    If e.GetType.Name = "InvalidOperationException" Then
                        message = String.Format(CultureInfo.CurrentCulture,
                                                My.Resources.ResourceManager.GetString(
                                                    "DocumentRecordMayHaveBeenDeleted"),
                                                e.Message)
                    Else
                        message = e.Message
                    End If
                    Me.message.Show(message, True)
                Next
                ClearDocumentRecord()
                Exit Sub
            End Try
            tasks(5) = Task.Run(Sub() GetDocumentPreview())
            tasks(6) = Task.Run(Sub() GetDocumentText())
            Try
                tasks(2).Wait()
            Catch ex As AggregateException
                For Each e In ex.InnerExceptions
                    message.Show(e.Message, True)
                Next
                ClearDocumentRecord()
                Exit Sub
            End Try
            view.DocumentRecordPanelEnabled = True
        Else
            ClearDocumentRecord()
        End If
    End Sub

    Private Sub FillSearchResults(ByVal dataTable As DataTable)
        view.SearchResultsExpanded = False
        view.SearchResultsEnabled = False
        view.SearchResults = dataTable
        view.SortSearchResults(searchResultsSortParameters.SortColumnIndex,
                               searchResultsSortParameters.SortDirection)
        If view.SearchResultsRowCount > 0 Then
            If My.Settings.SearchResultsSelectLastRow Then
                view.SelectSearchResultsLastRow()
            End If
        End If
    End Sub

    Private Sub ResetSearchResults()
        If view.SearchResults IsNot Nothing Then
            view.SearchResults.Reset()
        End If
        view.ResetSearchResultsHeader()
    End Sub
#End Region

#Region "View Selected Document Members"
    Public Sub NotesTextChanged()
        view.DocumentRecordNotes = view.DocumentRecordNotes.TrimStart
        If view.DocumentRecordNotes <> lastDocumentNotes Then ' IsNot would not work here.
            view.DocumentRecordNotesChanged = True
        Else
            view.DocumentRecordNotesChanged = False
        End If
    End Sub

    Private Sub GetDocumentRecordPdf()
        Dim cached As Boolean = False
        cachePathName = New CacheFilePathName(view.DocumentRecordId)
        Dim pdfInfo As New PdfFileInfo(cachePathName.Pdf)
        If pdfInfo.Exists Then
            If pdfInfo.ComputeHash = fileHashes.GetItem(pdfInfo.FullName) Then
                cached = True
            End If
        End If
        If cached = False Then
            Using model As IDocumentRepository = New DocumentRepository
                model.GetPdfById(view.DocumentRecordId, pdfInfo.FullName)
            End Using
            fileHashes.SetItem(pdfInfo.FullName, pdfInfo.ComputeHash)
        End If
    End Sub

    Private Sub GetDocumentRecordNotes(ByVal updateView As Boolean)
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                Dim dataTableNotes As DataTable =
                    model.GetNotesById(view.DocumentRecordId)
                Dim notes As String =
                    Convert.ToString(dataTableNotes.Rows(0)("doc_notes"),
                                     CultureInfo.CurrentCulture)
                If updateView Then
                    view.DocumentRecordNotes = notes
                    lastDocumentNotes = view.DocumentRecordNotes
                    view.DocumentRecordNotesChanged = False
                Else
                    preUpdateDocumentNotes = notes
                End If
            End Using
            view.SetCursor(False)
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Private Sub GetDocumentRecordKeywords()
        Using model As IDocumentRepository = New DocumentRepository
            Dim dataTableKeywords As DataTable =
                model.GetKeywordsById(view.DocumentRecordId)
            view.DocumentRecordKeywords =
                Convert.ToString(dataTableKeywords.Rows(0)("doc_keywords"),
                                 CultureInfo.CurrentCulture)
        End Using
    End Sub

    Private Sub GetDocumentRecordFlagState()
        Using model As IDocumentRepository = New DocumentRepository
            Dim dataTableFlagState As DataTable =
                model.GetFlagStateById(view.DocumentRecordId)
            view.DocumentRecordFlagState =
                Convert.ToInt32(dataTableFlagState.Rows(0)("doc_flag"),
                                CultureInfo.CurrentCulture)
        End Using
    End Sub

    Private Sub GetDocumentPreview()
        Dim cached As Boolean = False
        cachePathName = New CacheFilePathName(view.DocumentRecordId)
        Dim pdfInfo As New PdfFileInfo(cachePathName.Pdf)
        Dim imageFile As New ImageFileInfo(cachePathName.PdfPreview)
        If imageFile.Exists Then
            If imageFile.ComputeHash = fileHashes.GetItem(imageFile.FullName) Then
                cached = True
            End If
        End If
        If cached = False Then
            pdfInfo.GetPreviewImageToFile(My.Settings.PreviewImageResolution)
            fileHashes.SetItem(imageFile.FullName, imageFile.ComputeHash)
        End If
        view.DocumentPreview = imageFile.ToImage
    End Sub

    Private Sub GetDocumentText()
        cachePathName = New CacheFilePathName(view.DocumentRecordId)
        Dim pdfInfo As New PdfFileInfo(cachePathName.Pdf)
        view.DocumentText = pdfInfo.GetText
    End Sub

    Private Sub UpdateDocumentRecordNotes()
        Try
            view.SetCursor(True)
            view.DocumentRecordNotes = view.DocumentRecordNotes.Trim
            Using model As IDocumentRepository = New DocumentRepository
                model.UpdateNotesById(view.DocumentRecordId,
                                      view.DocumentRecordNotes)
            End Using
            lastDocumentNotes = view.DocumentRecordNotes
            view.SetCursor(False)
        Catch ex As OracleException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Private Sub ClearDocumentRecord()
        lastDocumentNotes = Nothing
        view.DocumentRecordFlagState = Nothing
        view.DocumentRecordPanelEnabled = False
        view.DocumentRecordNotes = Nothing
        view.DocumentRecordKeywords = Nothing
        view.DocumentPreview = Nothing
        view.DocumentText = Nothing
    End Sub
#End Region

#Region "View Timer Members"
    Public Async Sub UploadAsync()
        If UploadService.Instance.CanUploadCycleStart Then
            If uploadDirInfo.ContainsFiles Or
                uploadStagingDirInfo.ContainsFiles Then
                Try
                    view.UploadRunningVisible = True
                    Application.DoEvents()
                    Using uploadTask As Task = Task.Run(Sub() UploadService.Instance.ExecuteUploadCycle())
                        Await uploadTask
                    End Using
                Catch ex As InvalidOperationException
                Finally
                    view.UploadRunningVisible = False
                    If uploadDirInfo.ContainsFiles("*.delete") Then
                        view.UploadFolderErrorVisible = True
                    Else
                        view.UploadFolderErrorVisible = False
                    End If
                    If uploadStagingDirInfo.ContainsFiles Then
                        view.UploadStagingFolderErrorVisible = True
                    Else
                        view.UploadStagingFolderErrorVisible = False
                    End If
                    Application.DoEvents()
                End Try
            End If
        End If
    End Sub

    Public Sub FlaggedDocumentsCheck()
        Try
            Using model As IDocumentRepository = New DocumentRepository
                Dim flaggedCount As Integer = model.GetFlaggedRecordsCount
                If flaggedCount > 0 Then
                    view.FlaggedDocumentsExistVisible = True
                Else
                    view.FlaggedDocumentsExistVisible = False
                End If
            End Using
            Application.DoEvents()
        Catch ex As OracleException
            view.FlaggedDocumentsCheckTimerEnabled = False
            message.Show(ex.Message, True)
            view.FlaggedDocumentsCheckTimerEnabled = True
        End Try
    End Sub
#End Region

    Public Function ViewClosingPrompt() As Boolean
        Dim result As DialogResult = question.Show(
            My.Resources.DocumentNotesModified, True)
        If result = Windows.Forms.DialogResult.Yes Then
            UpdateDocumentRecordNotes()
            Return True
        ElseIf result = DialogResult.No Then
            Return True
        Else
            Return False
        End If
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                folderBrowser.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
