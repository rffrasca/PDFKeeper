'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
    Private ReadOnly view As IMainView
    Private ReadOnly uploadDirInfo As New DirectoryInfo(UserProfile.UploadPath)
    Private ReadOnly uploadStagingDirInfo As _
        New DirectoryInfo(UserProfile.UploadStagingPath)
    Private ReadOnly searchTextHistory As New GenericList(Of String)
    Private ReadOnly message As IMessageDisplayService =
        New MessageDisplayService
    Private ReadOnly question As IQuestionDisplayService =
        New QuestionDisplayService
    Private ReadOnly fileSelect As IFileSelectDisplayService =
        New FileSelectDisplayService
    Private ReadOnly folderBrowser As IFolderBrowserDisplayService =
        New FolderBrowserDisplayService
    Private ReadOnly setCategory As ISetCategoryView =
        New SetCategoryHelper
    Private ReadOnly setTaxYear As ISetTaxYearView =
        New SetTaxYearHelper
    Private ReadOnly pdfViewer As IPdfViewerService = New PdfViewerService
    Private ReadOnly searchResultsSortParameters As New DataGridViewSortParameters
    Private ReadOnly fileHashes As New GenericDictionaryList(Of String, String)
    Private cachePathName As CacheFilePathName
    Private searchBySelectionPerformed As Boolean
    Private refreshFlag As Boolean
    Private lastSearchText As String = String.Empty
    Private lastDocumentNotes As String
    Private preUpdateDocumentNotes As String

    Public Sub New(view As IMainView)
        Me.view = view
    End Sub

    Public Sub ApplyPolicy()    ' Policies do not apply when using SQLite.
        If DbInstanceProperties.Platform IsNot
            DatabasePlatform.Sqlite.ToString Then
            If ApplicationPolicy.DisableQueryAllDocuments Then
                view.RemoveAllDocumentsFromSearchFunctions()
            End If
        End If
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
                                                  "DocumentRecordMayHaveBeenDeleted",
                                                  CultureInfo.CurrentCulture),
                                              ex.Message), True)
            ClearDocumentRecord()
        Catch ex As CustomDbException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub SaveSelectedDocumentPdfOrTextAs()
        Dim targetExtension As String
        Dim targetFilePath As String
        cachePathName = New CacheFilePathName(view.DocumentRecordId)
        Dim pdfFile As New PdfFile(cachePathName.Pdf)
        If view.TextElementSelectedText Is Nothing Then
            targetExtension = "pdf"
        Else
            targetExtension = "txt"
        End If
        Using fileSelect As IFileSelectDisplayService = New FileSelectDisplayService
            Using model As IDocumentRepository = New DocumentRepository
                fileSelect.FileName = model.GetTitleById(view.DocumentRecordId)
            End Using
            fileSelect.Filter = targetExtension
            targetFilePath = fileSelect.ShowSave()
        End Using
        If targetFilePath.Length > 0 Then
            view.SetCursor(True)
            If view.TextElementSelectedText Is Nothing Then
                pdfFile.CopyTo(targetFilePath)
            Else
                view.TextElementSelectedText.WriteToFile(targetFilePath)
            End If
            view.SetCursor(False)
            message.Show(String.Format(
                                CultureInfo.CurrentCulture,
                                My.Resources.ResourceManager.GetString("FileSaved",
                                                                       CultureInfo.CurrentCulture),
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
        view.SetCursor(True)
        view.SelectDeselectAllSearchResults(SelectionState.SelectAll)
        view.SetCursor(False)
    End Sub

    Public Sub DeselectAllSearchResults()
        view.SetCursor(True)
        view.SelectDeselectAllSearchResults(SelectionState.DeselectAll)
        view.SetCursor(False)
    End Sub

    Public Sub SetCategoryOnSelectedSearchResults()
        Dim newCategory As String = setCategory.Show
        If newCategory IsNot Nothing Then
            Dim processPresenter As _
                New MainSelectedSearchResultsProcessPresenter(view,
                                                              SelectedDocumentsAction.SetCategory,
                                                              newCategory)
            ProcessSelectedSearchResults(processPresenter)
        End If
    End Sub

    Public Sub SetTaxYearOnSelectedSearchResults()
        Dim newTaxYear As String = setTaxYear.Show
        If newTaxYear IsNot Nothing Then
            Dim processPresenter As _
                New MainSelectedSearchResultsProcessPresenter(view,
                                                              SelectedDocumentsAction.SetTaxYear,
                                                              newTaxYear)
            ProcessSelectedSearchResults(processPresenter)
        End If
    End Sub

    Public Sub DeleteSelectedSearchResults()
        If question.Show(My.Resources.DeleteSelectedDocuments,
                                False) = DialogResult.Yes Then
            Dim processPresenter As _
                New MainSelectedSearchResultsProcessPresenter(view,
                                                              SelectedDocumentsAction.Delete,
                                                              Nothing)
            ProcessSelectedSearchResults(processPresenter)
        End If
    End Sub

    Public Sub ExportSelectedSearchResults()
        folderBrowser.Description = My.Resources.SelectExportFolder
        Dim exportFolder As String = folderBrowser.Show
        If exportFolder IsNot Nothing Then
            Dim processPresenter As _
                New MainSelectedSearchResultsProcessPresenter(view,
                                                              SelectedDocumentsAction.Export,
                                                              exportFolder)
            ProcessSelectedSearchResults(processPresenter)
            message.Show(String.Format(CultureInfo.CurrentCulture,
                                       My.Resources.ResourceManager.GetString(
                                       "SelectedFilesHaveBeenExported",
                                       CultureInfo.CurrentCulture),
                                       processPresenter.ExportFolderPath), False)
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
            view.DocumentRecordNotes.AppendDateTimeAndText(
            UserIdentification.AccountName)
        view.ScrollToEndInNotesElement()
    End Sub

    Public Sub SetFlagStateOnSelectedDocument()
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                model.UpdateFlagStateById(view.DocumentRecordId,
                                          view.DocumentRecordFlagState)
            End Using
            view.SetCursor(False)
            TriggerSearchResultsRefresh(False)
        Catch ex As CustomDbException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Public Sub RefreshSearchResults()
        refreshFlag = True
        Dim lastSelectedId As Integer = view.DocumentRecordId
        If view.SelectedSearchFunction = 0 Then     ' Documents by Text
            GetSearchResultsByString(True)
        ElseIf view.SelectedSearchFunction = 1 Then ' Documents by Selections
            GetSearchResultsBySearchSelection()
        ElseIf view.SelectedSearchFunction = 2 Then ' Documents by Date Added
            GetDocumentRecordsByDateAdded()
        ElseIf view.SelectedSearchFunction = 3 Then ' Flagged Documents
            GetFlaggedDocumentRecords()
        ElseIf view.SelectedSearchFunction = 4 Then ' All Documents
            GetAllDocumentRecords()
        End If
        If refreshFlag Then
            view.SelectSearchResultRowById(lastSelectedId)
        End If
        refreshFlag = False
    End Sub

    Public Sub ReloadDocumentPreview()
        view.SetCursor(True)
        Task.Run(Sub() GetDocumentPreview())
        view.SetCursor(False)
    End Sub

    Public Sub InsertTextFromFileIntoSelectedDocumentNotes()
        fileSelect.Filter = "txt"
        Dim textFile As String = fileSelect.ShowOpen
        If textFile.Length > 0 Then

            With view
                view.SetCursor(True)
                .ScrollToEndInNotesElement()
                .DocumentRecordNotes =
                    .DocumentRecordNotes.AppendText(IO.File.ReadAllText(textFile).Trim)
                .ScrollToEndInNotesElement()
                .SetCursor(False)
                If question.Show(String.Format(CultureInfo.CurrentCulture,
                                               My.Resources.ResourceManager.GetString(
                                               "DeleteFileToRecycleBin",
                                               CultureInfo.CurrentCulture),
                                               textFile),
                                 False) = DialogResult.Yes Then
                    .SetCursor(True)
                    Dim textFileInfo As New FileInfo(textFile)
                    textFileInfo.DeleteToRecycleBin
                    .SetCursor(False)
                End If
            End With
        End If
    End Sub

    Public Sub UpdatePdfTextAnnotAndTextInDbForSelectedSearchResults()
        If question.Show(My.Resources.UpdateSelectedDocuments,
                         False) = DialogResult.Yes Then
            Dim processPresenter As _
                New MainSelectedSearchResultsProcessPresenter(view,
                                                              SelectedDocumentsAction.Update,
                                                              Nothing)
            ProcessSelectedSearchResults(processPresenter)
        End If
    End Sub

    Public Sub MoveLocalDatabase()
        folderBrowser.Description = My.Resources.SelectNewDatabaseFolderLocation
        Dim newDatabaseFolder As String = folderBrowser.Show
        If newDatabaseFolder IsNot Nothing Then
            view.SetCursor(True)
            UploadService.Instance.CanUploadCycleStart = False
            Try
                IO.File.Move(UserProfile.LocalDatabasePath, IO.Path.Combine(newDatabaseFolder,
                                                                            String.Concat(Application.ProductName,
                                                                                          ".sqlite")))
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Robert F. Frasca\PDFKeeper",
                                              "LocalDatabasePath",
                                              newDatabaseFolder)
                view.SetCursor(False)
            Catch ex As IOException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            Finally
                UploadService.Instance.CanUploadCycleStart = True
            End Try
        End If
    End Sub

    Private Sub TriggerSearchResultsRefresh(ByVal postUpload As Boolean)
        If view.SearchFunctionsEnabled Then
            If postUpload Then
                RefreshSearchResults()
            Else
                If view.SelectedSearchFunction = 3 And
                        view.DocumentRecordFlagState = 0 Then
                    If question.Show(My.Resources.RefreshSearchResults,
                                     False) = DialogResult.Yes Then
                        RefreshSearchResults()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ProcessSelectedSearchResults(ByVal processPresenter As MainSelectedSearchResultsProcessPresenter)
        Try
            view.SetCursor(True)
            processPresenter.ProcessSelectedSearchResults()
            view.SetCursor(False)
        Catch ex As InvalidOperationException
            view.SetCursor(False)
            message.Show(String.Format(CultureInfo.CurrentCulture,
                                       My.Resources.ResourceManager.GetString(
                                       "ProcessDocumentRecordMayHaveBeenDeleted",
                                       CultureInfo.CurrentCulture),
                                       ex.Message,
                                       processPresenter.IdBeingProcessed), True)
        Catch ex As IndexOutOfRangeException
            view.SetCursor(False)
            message.Show(String.Format(CultureInfo.CurrentCulture,
                                       My.Resources.ResourceManager.GetString(
                                       "ProcessDocumentRecordMayHaveBeenDeleted",
                                       CultureInfo.CurrentCulture),
                                       ex.Message,
                                       processPresenter.IdBeingProcessed), True)
        Catch ex As CustomDbException
            view.SetCursor(False)
            message.Show(String.Format(CultureInfo.CurrentCulture,
                                       My.Resources.ResourceManager.GetString(
                                       "DocumentIdException",
                                       CultureInfo.CurrentCulture),
                                       ex.Message,
                                       processPresenter.IdBeingProcessed), True)
        End Try
    End Sub

#End Region

#Region "View Search Functions Members"
    Public Sub SearchFunctionSelected()
        With view
            .SearchTextControlEnabled = False
            .AuthorEnabled = False
            .SubjectEnabled = False
            .CategoryEnabled = False
            .TaxYearEnabled = False
            .ClearSelectionsEnabled = False
            .SearchBySelectionsEnabled = False
            .SearchDatePickerEnabled = False
        End With
        If view.SelectedSearchFunction = 0 Then     ' Documents by Text
            SearchTextChanged()
            GetSearchResultsByString(True)
        ElseIf view.SelectedSearchFunction = 1 Then ' Documents by Selections
            Dim capturesearchBySelectionPerformed As Boolean =
                searchBySelectionPerformed
            With view
                .AuthorEnabled = True
                .SubjectEnabled = True
                .CategoryEnabled = True
                .TaxYearEnabled = True
            End With
            SetSearchBySelectionsButtonsState()
            searchBySelectionPerformed = capturesearchBySelectionPerformed
            If searchBySelectionPerformed Then
                GetSearchResultsBySearchSelection()
            End If
        ElseIf view.SelectedSearchFunction = 2 Then ' Documents by Date Added
            view.SearchDatePickerEnabled = True
            GetDocumentRecordsByDateAdded()
        ElseIf view.SelectedSearchFunction = 3 Then ' Flagged Documents
            GetFlaggedDocumentRecords()
        ElseIf view.SelectedSearchFunction = 4 Then ' All Documents
            GetAllDocumentRecords()
        End If
    End Sub

    Public Sub GetSearchStringHistory()
        view.SearchTextHistory = searchTextHistory.ToArray(False)
    End Sub

    Public Sub SearchTextChanged()
        view.SearchText = view.SearchText.TrimStart
        If view.SearchText.Length > 0 Then
            If QueryOperatorChecker.IsSyntaxCorrect(DbInstanceProperties.Platform,
                                                    view.SearchText) Then
                view.SearchTextErrorProviderMessage = Nothing
                view.SearchEnabled = True
            Else
                view.SearchTextErrorProviderMessage =
                    My.Resources.SearchTextImproperUsageOfQueryOperators
                view.SearchEnabled = False
            End If
        Else
            view.SearchTextErrorProviderMessage = Nothing
            view.SearchEnabled = False
        End If
    End Sub

    Public Sub GetSearchResultsByString(ByVal useLastSearchString As Boolean)
        If useLastSearchString Then
            view.SearchText = lastSearchText
        End If
        view.SearchTextControlEnabled = True
        If view.SearchText.Length > 0 Then
            Try
                view.SetCursor(True)
                ' Need to capture view.SearchText into a new string and set
                ' view.SearchText to that string after the query to prevent
                ' the Search Text combox box from selecting the first item
                ' in the drop down list that contains the text in the text
                ' box after the query.
                Dim currentSearchString As String = view.SearchText
                Using model As IDocumentRepository = New DocumentRepository
                    FillSearchResults(model.GetAllRecordsBySearchText(view.SearchText))
                End Using
                view.SearchText = currentSearchString
                If view.SearchResultsRowCount > 0 Then
                    searchTextHistory.Add(view.SearchText)
                End If
                lastSearchText = view.SearchText
                view.SearchEnabled = False
                view.SetCursor(False)
            Catch ex As CustomDbException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        Else
            ResetSearchResults()
        End If
        GetSearchStringHistory()
    End Sub

    Public Sub SetSearchBySelectionsButtonsState()
        Dim enabled As Boolean = False
        If view.AuthorGroup IsNot Nothing Or
            view.SubjectGroup IsNot Nothing Or
            view.CategoryGroup IsNot Nothing Or
            view.TaxYearGroup IsNot Nothing Then
            enabled = True
        End If
        view.ClearSelectionsEnabled = enabled
        view.SearchBySelectionsEnabled = enabled
        searchBySelectionPerformed = False
        ResetSearchResults()
    End Sub

    Public Sub ClearSearchSelections()
        With view
            .AuthorGroup = Nothing
            .SubjectGroup = Nothing
            .CategoryGroup = Nothing
            .TaxYearGroup = Nothing
            .ClearSelectionsEnabled = False
            .SearchBySelectionsEnabled = False
        End With
        searchBySelectionPerformed = False
        ResetSearchResults()
    End Sub

    Public Sub GetSearchResultsBySearchSelection()
        If view.ClearSelectionsEnabled Then
            Try
                view.SetCursor(True)
                Using model As IDocumentRepository = New DocumentRepository
                    FillSearchResults(
                        model.GetAllRecordsByAuthorSubjectCategoryAndTaxYear(view.AuthorGroup,
                                                                             view.SubjectGroup,
                                                                             view.CategoryGroup,
                                                                             view.TaxYearGroup))
                End Using
                view.SetCursor(False)
                searchBySelectionPerformed = True
                view.SearchBySelectionsEnabled = False
            Catch ex As CustomDbException
                view.SetCursor(False)
                message.Show(ex.Message, True)
            End Try
        Else
            searchBySelectionPerformed = False
            ResetSearchResults()
        End If
    End Sub

    Public Sub GetDocumentRecordsByDateAdded()
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                FillSearchResults(model.GetAllRecordsByDateAdded(view.SearchDate))
            End Using
            view.SetCursor(False)
        Catch ex As CustomDbException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Private Sub GetFlaggedDocumentRecords()
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                FillSearchResults(model.GetAllFlaggedRecords)
            End Using
            view.SetCursor(False)
        Catch ex As CustomDbException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Private Sub GetAllDocumentRecords()
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                FillSearchResults(model.GetAllRecords)
            End Using
            view.SetCursor(False)
        Catch ex As CustomDbException
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
                                                    "DocumentRecordMayHaveBeenDeleted",
                                                    CultureInfo.CurrentCulture),
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
                tasks(5).Wait()
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
        Dim pdfFile As New PdfFile(cachePathName.Pdf)
        If pdfFile.Exists Then
            If pdfFile.ComputeHash = fileHashes.GetItem(pdfFile.FullName) Then
                cached = True
            End If
        End If
        If cached = False Then
            Using model As IDocumentRepository = New DocumentRepository
                model.GetPdfById(view.DocumentRecordId, pdfFile.FullName)
            End Using
            fileHashes.SetItem(pdfFile.FullName, pdfFile.ComputeHash)
        End If
    End Sub

    Private Sub GetDocumentRecordNotes(ByVal updateView As Boolean)
        Try
            view.SetCursor(True)
            Using model As IDocumentRepository = New DocumentRepository
                Dim notes As String = model.GetNotesById(view.DocumentRecordId)
                If updateView Then
                    view.DocumentRecordNotes = notes
                    lastDocumentNotes = view.DocumentRecordNotes
                    view.DocumentRecordNotesChanged = False
                Else
                    preUpdateDocumentNotes = notes
                End If
            End Using
            view.SetCursor(False)
        Catch ex As CustomDbException
            view.SetCursor(False)
            message.Show(ex.Message, True)
        End Try
    End Sub

    Private Sub GetDocumentRecordKeywords()
        Using model As IDocumentRepository = New DocumentRepository
            view.DocumentRecordKeywords = model.GetKeywordsById(view.DocumentRecordId)
        End Using
    End Sub

    Private Sub GetDocumentRecordFlagState()
        Using model As IDocumentRepository = New DocumentRepository
            view.DocumentRecordFlagState = model.GetFlagStateById(view.DocumentRecordId)
        End Using
    End Sub

    Private Sub GetDocumentPreview()
        Dim cached As Boolean = False
        cachePathName = New CacheFilePathName(view.DocumentRecordId)
        Dim pdfFile As New PdfFile(cachePathName.Pdf)
        Dim imageFile As New ImageFile(cachePathName.PdfPreview)
        If imageFile.Exists Then
            If imageFile.ComputeHash = fileHashes.GetItem(imageFile.FullName) Then
                cached = True
            End If
        End If
        If cached = False Then
            pdfFile.GeneratePreviewImageFile(My.Settings.PreviewImageResolution)
            fileHashes.SetItem(imageFile.FullName, imageFile.ComputeHash)
        End If
        view.DocumentPreview = imageFile.ToImage
    End Sub

    Private Sub GetDocumentText()
        Using model As IDocumentRepository = New DocumentRepository
            view.DocumentText = model.GetTextById(view.DocumentRecordId)
        End Using
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
        Catch ex As CustomDbException
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
                    Dim pdfFilesToUpload As Boolean = False
                    If uploadDirInfo.ContainsFilesMatchingSearchPattern("*.pdf") Or
                        uploadStagingDirInfo.ContainsFilesMatchingSearchPattern("*.pdf") Then
                        pdfFilesToUpload = True
                        view.UploadRunningVisible = True
                    End If
                    view.UploadFolderErrorVisible = False
                    view.UploadStagingFolderErrorVisible = False
                    Application.DoEvents()
                    Using uploadTask As Task = Task.Run(Sub() UploadService.Instance.ExecuteUploadCycle())
                        Await uploadTask.ConfigureAwait(True)
                    End Using
                    If pdfFilesToUpload Then
                        TriggerSearchResultsRefresh(True)
                    End If
                Catch ex As InvalidOperationException
                Finally
                    view.UploadRunningVisible = False
                    If uploadDirInfo.ContainsFilesExcludingSearchPattern("*.delete") Then
                        view.UploadFolderErrorVisible = True
                    End If
                    If uploadStagingDirInfo.ContainsFiles Then
                        view.UploadStagingFolderErrorVisible = True
                    End If
                    Application.DoEvents()
                End Try
            Else
                view.UploadFolderErrorVisible = False
                view.UploadStagingFolderErrorVisible = False
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
        Catch ex As CustomDbException
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
                fileSelect.Dispose()
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
