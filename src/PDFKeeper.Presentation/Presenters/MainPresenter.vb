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
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.FileIO
Imports PDFKeeper.Infrastructure
Imports PDFKeeper.Services

Public Class MainPresenter
    Implements IDisposable
    Private ReadOnly view As IMainView
    Private ReadOnly authorListSvc As IAuthorListService
    Private ReadOnly subjectListSvc As ISubjectListService
    Private ReadOnly categoryListSvc As ICategoryListService
    Private ReadOnly taxYearListSvc As ITaxYearListService
    Private ReadOnly documentSvc As IDocumentService
    Private ReadOnly documentListSvc As IDocumentListService
    Private ReadOnly fileCacheSvc As IFileCacheService
    Private ReadOnly searchTermHistorySvc As ISearchTermHistoryService
    Private ReadOnly pdfSvc As IPdfService
    Private ReadOnly uploadSvc As IUploadService
    Private ReadOnly help As New HelpFile
    Private ReadOnly commonDialogs As New CommonDialogs
    Private ReadOnly dgvSortProperties As New DataGridViewSortProperties
    Private toolStripStateManager As ToolStripStateManager
    Private viewInstance As Form
    Private previousSearchTerm As String = String.Empty
    Private findBySelectionsPerformed As Boolean
    Private selectedDocument As DocumentModel
    Private textToPrint As String = String.Empty
    Private previousNotes As String
    Private preUpdateNotes As String
    Private disposedValue As Boolean
    ' Message that is sent when the contents of the clipboard have changed.
    Private Const WM_CLIPBOARDUPDATE = &H31D

    Public Sub New(ByVal view As IMainView, ByVal authorListSvc As IAuthorListService,
                   ByVal subjectListSvc As ISubjectListService, ByVal categoryListSvc As ICategoryListService,
                   ByVal taxYearListSvc As ITaxYearListService, ByVal documentSvc As IDocumentService,
                   ByVal documentListSvc As IDocumentListService, ByVal fileCacheSvc As IFileCacheService,
                   ByVal searchTermHistorySvc As ISearchTermHistoryService, ByVal pdfSvc As IPdfService,
                   ByVal uploadSvc As IUploadService)
        Me.view = view
        Me.authorListSvc = authorListSvc
        Me.subjectListSvc = subjectListSvc
        Me.categoryListSvc = categoryListSvc
        Me.taxYearListSvc = taxYearListSvc
        Me.documentSvc = documentSvc
        Me.documentListSvc = documentListSvc
        Me.fileCacheSvc = fileCacheSvc
        Me.searchTermHistorySvc = searchTermHistorySvc
        Me.pdfSvc = pdfSvc
        Me.uploadSvc = uploadSvc
    End Sub

    Public Sub MyBaseWndProc(ByRef m As Message)
        If m.Msg = WM_CLIPBOARDUPDATE Then
            If My.Computer.Clipboard.ContainsText Then
                If view.NotesFocused Then
                    toolStripStateManager.EnableEditPaste(True)
                End If
            End If
        End If
    End Sub

    Public Sub NotifyPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = "")
        If propertyName = "NotesChanged" Then
            OnNotesChanged()
        End If
    End Sub

    Friend Sub MainForm_Load(sender As Object, e As EventArgs)
        viewInstance = CType(sender, Form)
        NativeMethods.AddClipboardFormatListener(viewInstance.Handle)
        UpdateCheckTimer_Tick(Me, Nothing)
        SetInitialFormState()
        toolStripStateManager = New ToolStripStateManager(viewInstance)
        StartupActions()
    End Sub

    Friend Sub ToolStrip_VisibleChanged(sender As Object, e As EventArgs)
        view.ViewToolBarMenuItemChecked = view.ToolStripVisible
    End Sub

    Friend Sub FileAddToolStrip_Click(sender As Object, e As EventArgs)
        Using AddPdfDialog
            AddPdfDialog.ShowDialog()
        End Using
    End Sub

    Friend Sub FileOpenToolStrip_Click(sender As Object, e As EventArgs)
        If view.CheckedDocumentIdItems().Count > 0 Then
            ProcessCheckedDocuments(CheckedDocumentsAction.Open, Nothing, Nothing)
        Else
            DocumentListDataGridView_CellDoubleClick(Me, Nothing)
        End If
    End Sub

    Friend Sub FileSaveToolStrip_Click(sender As Object, e As EventArgs)
        With view
            preUpdateNotes = selectedDocument.Notes
            If preUpdateNotes = previousNotes Then
                UpdateSelectedDocument()
                .Notes = selectedDocument.Notes
                previousNotes = .Notes
            Else
                Clipboard.SetText(.Notes.Trim)
                DocumentListDataGridView_SelectionChanged(Me, Nothing)
                commonDialogs.ShowMessageBox(My.Resources.UnableToSaveNotes, True)
            End If
            .ScrollToEndOfNotesText()
            NotesTextBox_TextChanged(Me, Nothing)
            toolStripStateManager.SetStateForTextBoxSelectedText(.NotesReadOnly, .Notes.Length, .NotesSelectionLength)
        End With
    End Sub

    Friend Sub FileSaveAsToolStrip_Click(sender As Object, e As EventArgs)
        Dim text = view.TextFromFocusedTextBox
        Dim filter As String
        If text Is Nothing Then
            filter = My.Resources.PdfFilter
        Else
            filter = My.Resources.TextFilter
        End If
        Dim targetPath = view.ShowSaveFileDialog(filter, selectedDocument.Title)
        If targetPath IsNot Nothing Then
            viewInstance.Cursor = Cursors.WaitCursor
            If text Is Nothing Then
                Dim pdfFile = New PdfFile(fileCacheSvc.GetCachedPdfFullName(view.SelectedDocumentId))
                pdfFile.CopyTo(targetPath)
            Else
                text.WriteToFile(targetPath)
            End If
            viewInstance.Cursor = Cursors.Default
        End If
    End Sub

    Friend Sub FileBurstToolStrip_Click(sender As Object, e As EventArgs)
        Dim pdfFile = New PdfFile(fileCacheSvc.GetCachedPdfFullName(view.SelectedDocumentId))
        Dim selectedPath = view.ShowFolderBrowserDialog(String.Format(CultureInfo.CurrentCulture,
                                                                      My.Resources.ResourceManager.GetString(
                                                                      "SelectBurstFolder", CultureInfo.CurrentCulture),
                                                                      String.Concat(pdfFile.FileNameWithoutExtension,
                                                                                    "_")))
        If selectedPath IsNot Nothing Then
            viewInstance.Cursor = Cursors.WaitCursor
            pdfFile.Split(selectedPath)
            viewInstance.Cursor = Cursors.Default
        End If
    End Sub

    Friend Sub FilePrintToolStrip_Click(sender As Object, e As EventArgs)
        view.PrintSelectectedTextBoxText(False)
    End Sub

    Friend Sub FilePrintPreviewToolStrip_Click(sender As Object, e As EventArgs)
        view.PrintSelectectedTextBoxText(True)
    End Sub

    Friend Sub FileSelectAllToolStrip_Click(sender As Object, e As EventArgs)
        view.SelectAllDocumentCheckBoxes(True)
    End Sub

    Friend Sub FileSelectNoneToolStrip_Click(sender As Object, e As EventArgs)
        view.SelectAllDocumentCheckBoxes(False)
    End Sub

    Friend Sub FileSetCategoryToolStrip_Click(sender As Object, e As EventArgs)
        Using SetCategoryDialog
            If SetCategoryDialog.ShowDialog = DialogResult.OK Then
                Dim category = SetCategoryDialog.Category
                If category IsNot Nothing Then
                    ProcessCheckedDocuments(CheckedDocumentsAction.SetCategory, category, Nothing)
                End If
            End If
        End Using
    End Sub

    Friend Sub FileSetTaxYearToolStrip_Click(sender As Object, e As EventArgs)
        Using SetTaxYearDialog
            If SetTaxYearDialog.ShowDialog = DialogResult.OK Then
                Dim taxYear = SetTaxYearDialog.TaxYear
                If taxYear IsNot Nothing Then
                    ProcessCheckedDocuments(CheckedDocumentsAction.SetTaxYear, taxYear, Nothing)
                End If
            End If
        End Using
    End Sub

    Friend Sub FileDeleteToolStrip_Click(sender As Object, e As EventArgs)
        If commonDialogs.ShowQuestionMessageBox(My.Resources.DeleteSelectedDocuments, False) = DialogResult.Yes Then
            ProcessCheckedDocuments(CheckedDocumentsAction.Delete, Nothing, Nothing)
        End If
    End Sub

    Friend Sub FileExportToolStrip_Click(sender As Object, e As EventArgs)
        Dim exportPath = view.ShowFolderBrowserDialog(My.Resources.SelectExportFolder)
        If exportPath IsNot Nothing Then
            ProcessCheckedDocuments(CheckedDocumentsAction.Export, exportPath, Nothing)
        End If
    End Sub

    Friend Sub EditUndoToolStrip_Click(sender As Object, e As EventArgs)
        If sender.GetType.Name.Contains("ToolStripMenuItem") Then
            TryCast(CType(sender, ToolStripMenuItem).Tag, TextBox).Undo()
        Else
            TryCast(CType(sender, ToolStripButton).Tag, TextBox).Undo()
        End If
    End Sub

    Friend Sub EditCutToolStrip_Click(sender As Object, e As EventArgs)
        If sender.GetType.Name.Contains("ToolStripMenuItem") Then
            TryCast(CType(sender, ToolStripMenuItem).Tag, TextBox).Cut()
        Else
            TryCast(CType(sender, ToolStripButton).Tag, TextBox).Cut()
        End If
    End Sub

    Friend Sub EditCopyToolStrip_Click(sender As Object, e As EventArgs)
        If sender.GetType.Name.Contains("ToolStripMenuItem") Then
            TryCast(CType(sender, ToolStripMenuItem).Tag, TextBoxFocusedChecker).TextBoxWithFocus().Copy()
        Else
            TryCast(CType(sender, ToolStripButton).Tag, TextBoxFocusedChecker).TextBoxWithFocus().Copy()
        End If
    End Sub

    Friend Sub EditPasteToolStrip_Click(sender As Object, e As EventArgs)
        If sender.GetType.Name.Contains("ToolStripMenuItem") Then
            TryCast(CType(sender, ToolStripMenuItem).Tag, TextBox).Paste()
        Else
            TryCast(CType(sender, ToolStripButton).Tag, TextBox).Paste()
        End If
    End Sub

    Friend Sub EditSelectAllToolStrip_Click(sender As Object, e As EventArgs)
        Dim textBox = TryCast(CType(sender, ToolStripMenuItem).Tag, TextBoxFocusedChecker).TextBoxWithFocus
        textBox.SelectAll()
        toolStripStateManager.SetStateForTextBoxSelectedText(textBox.ReadOnly, textBox.Text.Length,
                                                             textBox.SelectionLength)
    End Sub

    Friend Sub EditRestoreToolStrip_Click(sender As Object, e As EventArgs)
        With view
            .DocumentTabControlSelectedIndex = 0
            .Notes = previousNotes
            previousNotes = .Notes
            .ScrollToEndOfNotesText()
            NotesTextBox_TextChanged(Me, Nothing)
        End With
    End Sub

    Friend Sub EditDateTimeToolStrip_Click(sender As Object, e As EventArgs)
        With view
            .ScrollToEndOfNotesText()
            .Notes = .Notes.AppendDateTimeAndText(DbSession.UserName)
            .ScrollToEndOfNotesText()
        End With
    End Sub

    Friend Sub EditFlagDocumentToolStrip_Click(sender As Object, e As EventArgs)
        UpdateSelectedDocument()
        TriggerDocumentListRefresh(False)
    End Sub

    Friend Sub ViewToolStripMenuItem_DropDownOpened(sender As Object, e As EventArgs)
        view.ViewToolBarMenuItemChecked = view.ToolStripVisible
        view.ViewStatusBarMenuItemChecked = view.StatusStripVisible
    End Sub

    Friend Sub ViewRefreshToolStrip_Click(sender As Object, e As EventArgs)
        With view
            Dim previousSelectedId = .SelectedDocumentId
            If .DocumentRetrievalChoiceSelectedIndex = 0 Then      'Find Documents Search Term
                FindDocumentsBySearchTerm(True)
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 1 Then  'Find Documents by Selections
                FindBySelectionsButton_Click(Me, Nothing)
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 2 Then  'Find Documents by Date Added
                DateAddedDateTimePicker_ValueChanged(Me, Nothing)
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 3 Then  'List Flagged Documents
                ListFlaggedDocuments()
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 4 Then  'List All Documents
                ListAllDocuments()
            End If
            .SelectDocument(previousSelectedId)
        End With
    End Sub

    Friend Sub ViewSetPreviewPixelDensityToolStrip_Click(sender As Object, e As EventArgs)
        PreviewPictureBox_DoubleClick(Me, Nothing)
    End Sub

    Friend Sub ViewToolBarToolStrip_Click(sender As Object, e As EventArgs)
        With view
            If .ViewToolBarMenuItemChecked Then
                .ViewToolBarMenuItemChecked = False
            Else
                .ViewToolBarMenuItemChecked = True
            End If
            .ToolStripVisible = .ViewToolBarMenuItemChecked
        End With
    End Sub

    Friend Sub ViewStatusBarToolStrip_Click(sender As Object, e As EventArgs)
        With view
            If .ViewStatusBarMenuItemChecked Then
                .ViewStatusBarMenuItemChecked = False
            Else
                .ViewStatusBarMenuItemChecked = True
            End If
            .StatusStripVisible = .ViewStatusBarMenuItemChecked
        End With
    End Sub

    Friend Sub InsertTextToolStrip_Click(sender As Object, e As EventArgs)
        Dim textFile = view.ShowOpenTextFileDialog
        If textFile IsNot Nothing Then
            With view
                .ScrollToEndOfNotesText()
                .Notes = .Notes.AppendTextFile(textFile)
                .ScrollToEndOfNotesText()
            End With
            If commonDialogs.ShowQuestionMessageBox(String.Format(CultureInfo.CurrentCulture,
                                                                  My.Resources.ResourceManager.GetString(
                                                                  "DeleteFileToRecycleBin",
                                                                  CultureInfo.CurrentCulture), textFile),
                                                    False) = DialogResult.Yes Then
                Dim textFileInfo = New FileInfo(textFile)
                textFileInfo.DeleteToRecycleBin
            End If
        End If
    End Sub

    Friend Sub ToolsOptionsToolStrip_Click(sender As Object, e As EventArgs)
        OptionsDialog.ShowDialog()
    End Sub

    Friend Sub ToolsUploadProfilesToolStrip_Click(sender As Object, e As EventArgs)
        UploadProfilesDialog.ShowDialog()
    End Sub

    Friend Sub ToolsUpdatePdfTextColumnsToolStrip_Click(sender As Object, e As EventArgs)
        Dim result = commonDialogs.ShowQuestionMessageBox(My.Resources.OcrImageDataPages, True)
        If result = DialogResult.Yes Or result = DialogResult.No Then
            Dim ocrImageDataPages = False
            If result = DialogResult.Yes Then
                ocrImageDataPages = True
            End If
            ProcessCheckedDocuments(CheckedDocumentsAction.Update, Nothing, ocrImageDataPages)
        End If
    End Sub

    Friend Sub ToolsMoveDatabaseToolStrip_Click(sender As Object, e As EventArgs)
        Dim destFolderPath = view.ShowFolderBrowserDialog(My.Resources.SelectNewDatabaseFolderLocation)
        If destFolderPath IsNot Nothing Then
            Try
                viewInstance.Cursor = Cursors.WaitCursor
                WaitForUploadToFinish()
                Dim command As ICommand = New MoveSqliteDatabaseCommand(destFolderPath)
                command.Execute()
            Catch ex As IOException
                commonDialogs.ShowMessageBox(ex.Message, True)
            Finally
                viewInstance.Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Friend Sub HelpContentsToolStrip_Click(sender As Object, e As EventArgs)
        help.Show("Using PDFKeeper.html", viewInstance)
    End Sub

    Friend Sub HelpAboutToolStrip_Click(sender As Object, e As EventArgs)
        AboutBox.ShowDialog()
    End Sub

    Friend Sub DocumentRetrievalChoicesListBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        toolStripStateManager.EnableViewRefresh(False)
        With view
            .SearchTermEnabled = False
            .AuthorEnabled = False
            .SubjectEnabled = False
            .CategoryEnabled = False
            .TaxYearEnabled = False
            .ClearSelectionsEnabled = False
            .FindBySelectionsEnabled = False
            .DateAddedEnabled = False
            If .DocumentRetrievalChoiceSelectedIndex = 0 Then      ' Find Documents by Search Term
                SearchTermComboBox_TextChanged(Me, Nothing)
                FindDocumentsBySearchTerm(True)
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 1 Then  ' Find Documents by Selections
                Dim captureFilterBySelectionsPerformed = findBySelectionsPerformed
                .AuthorEnabled = True
                .SubjectEnabled = True
                .CategoryEnabled = True
                .TaxYearEnabled = True
                ComboBox_DropDownClosed(Me, Nothing)
                findBySelectionsPerformed = captureFilterBySelectionsPerformed
                If findBySelectionsPerformed Then
                    FindBySelectionsButton_Click(Me, Nothing)
                End If
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 2 Then  ' Find Documents by Date Added
                .DateAddedEnabled = True
                DateAddedDateTimePicker_ValueChanged(Me, Nothing)
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 3 Then  ' List Flagged Documents
                ListFlaggedDocuments()
            ElseIf .DocumentRetrievalChoiceSelectedIndex = 4 Then  ' List All Documents
                ListAllDocuments()
            End If
        End With
    End Sub

    Friend Sub SearchTermComboBox_Enter(sender As Object, e As EventArgs)
        view.SearchTermItems = searchTermHistorySvc.ListHistory
    End Sub

    Friend Sub SearchTermComboBox_TextChanged(sender As Object, e As EventArgs)
        toolStripStateManager.EnableViewRefresh(False)
        view.SetErrorProviderMessage(Nothing)
        view.SearchTerm = view.SearchTerm.TrimStart
        If view.SearchTerm.Trim.Length > 0 Then
            view.FindBySearchTermEnabled = True
        Else
            view.FindBySearchTermEnabled = False
        End If
    End Sub

    Friend Sub FindBySearchTermButton_Click(sender As Object, e As EventArgs)
        FindDocumentsBySearchTerm(False)
    End Sub

    Friend Sub AuthorComboBox_Enter(sender As Object, e As EventArgs)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            With view
                Dim currentItem = .Author
                If .Subject Is Nothing And .Category Is Nothing And .TaxYear Is Nothing Then
                    .AuthorItems = authorListSvc.ListAuthors
                Else
                    Dim model = New AuthorFilterModel
                    model.Subject = .Subject
                    model.Category = .Category
                    model.TaxYear = .TaxYear
                    .AuthorItems = authorListSvc.ListAuthors(model)
                End If
                .Author = currentItem
            End With
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub SubjectComboBox_Enter(sender As Object, e As EventArgs)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            With view
                Dim currentItem = .Subject
                If .Author Is Nothing And .Category Is Nothing And .TaxYear Is Nothing Then
                    .SubjectItems = subjectListSvc.ListSubjects
                Else
                    Dim model = New SubjectFilterModel
                    model.Author = .Author
                    model.Category = .Category
                    model.TaxYear = .TaxYear
                    .SubjectItems = subjectListSvc.ListSubjects(model)
                End If
                .Subject = currentItem
            End With
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub CategoryComboBox_Enter(sender As Object, e As EventArgs)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            With view
                Dim currentItem = .Category
                If .Author Is Nothing And .Subject Is Nothing And .TaxYear Is Nothing Then
                    .CategoryItems = categoryListSvc.ListCategories
                Else
                    Dim model = New CategoryFilterModel
                    model.Author = .Author
                    model.Subject = .Subject
                    model.TaxYear = .TaxYear
                    .CategoryItems = categoryListSvc.ListCategories(model)
                End If
                .Category = currentItem
            End With
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub TaxYearComboBox_Enter(sender As Object, e As EventArgs)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            With view
                Dim currentItem = .TaxYear
                If .Author Is Nothing And .Subject Is Nothing And .Category Is Nothing Then
                    .TaxYearItems = taxYearListSvc.ListTaxYears
                Else
                    Dim model = New TaxYearFilterModel
                    model.Author = .Author
                    model.Subject = .Subject
                    model.Category = .Category
                    .TaxYearItems = taxYearListSvc.ListTaxYears(model)
                End If
                .TaxYear = currentItem
            End With
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub ComboBox_DropDownClosed(sender As Object, e As EventArgs)
        Dim enabled As Boolean
        toolStripStateManager.EnableViewRefresh(False)
        With view
            If .Author IsNot Nothing Or .Subject IsNot Nothing Or .Category IsNot Nothing Or .TaxYear IsNot Nothing Then
                enabled = True
            End If
            .ClearSelectionsEnabled = enabled
            .FindBySelectionsEnabled = enabled
        End With
        findBySelectionsPerformed = False
        ResetDocumentListDataSource()
    End Sub

    Friend Sub ComboBox_KeyDown(sender As Object, e As KeyEventArgs)
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            CType(sender, ComboBox).DroppedDown = True
        End If
    End Sub

    Friend Sub ComboBox_MouseWheel(sender As Object, e As MouseEventArgs)
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not CType(sender, ComboBox).DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Friend Sub ClearSelectionsButton_Click(sender As Object, e As EventArgs)
        toolStripStateManager.EnableViewRefresh(False)
        With view
            .Author = Nothing
            .Subject = Nothing
            .Category = Nothing
            .TaxYear = Nothing
            .ClearSelectionsEnabled = False
            .FindBySelectionsEnabled = False
        End With
        findBySelectionsPerformed = False
        ResetDocumentListDataSource()
    End Sub

    Friend Sub FindBySelectionsButton_Click(sender As Object, e As EventArgs)
        With view
            If .ClearSelectionsEnabled Then
                Try
                    viewInstance.Cursor = Cursors.WaitCursor
                    Dim model = New FindSelectionsFilterModel
                    model.Author = .Author
                    model.Subject = .Subject
                    model.Category = .Category
                    model.TaxYear = .TaxYear
                    .DocumentList = documentListSvc.ListDocumentsBySelections(model)
                    findBySelectionsPerformed = True
                    .FindBySelectionsEnabled = False
                Catch ex As DbException
                    commonDialogs.ShowMessageBox(ex.Message, True)
                Finally
                    viewInstance.Cursor = Cursors.Default
                End Try
            Else
                findBySelectionsPerformed = False
                ResetDocumentListDataSource()
            End If
        End With
    End Sub

    Friend Sub DateAddedDateTimePicker_ValueChanged(sender As Object, e As EventArgs)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            view.DocumentList = documentListSvc.ListDocumentsByDateAdded(view.DateAdded)
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub DocumentListDataGridView_DataSourceChanged(sender As Object, e As EventArgs)
        With CType(sender, DataGridView)
            .Enabled = False
            .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            .Columns(1).HeaderCell.Value = My.Resources.ID
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(1).ReadOnly = True
            .Columns(2).HeaderCell.Value = My.Resources.Title
            .Columns(2).ReadOnly = True
            .Columns(3).HeaderCell.Value = My.Resources.Author
            .Columns(3).ReadOnly = True
            .Columns(4).HeaderCell.Value = My.Resources.Subject
            .Columns(4).ReadOnly = True
            .Columns(5).HeaderCell.Value = My.Resources.Category
            .Columns(5).ReadOnly = True
            .Columns(6).HeaderCell.Value = My.Resources.TaxYear
            .Columns(6).ReadOnly = True
            .Columns(7).HeaderCell.Value = My.Resources.Added
            .Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(7).ReadOnly = True
            If .RowCount > 0 Then
                .Enabled = True
                .Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                If .Columns(7).Displayed = True Then
                    .Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If
                .Columns(7).MinimumWidth = .Columns(7).FillWeight + 20
                .Sort(.Columns(dgvSortProperties.SortColumnIndex), dgvSortProperties.SortDirection)
                If My.Settings.SelectLastDocumentRow Then
                    .Rows(.Rows.Count - 1).Selected = True
                    .FirstDisplayedScrollingRowIndex = .RowCount - 1
                End If
            End If
        End With
        toolStripStateManager.EnableViewRefresh(True)
    End Sub

    Friend Sub DocumentListDataGridView_Sorted(sender As Object, e As EventArgs)
        Dim control = CType(sender, DataGridView)
        dgvSortProperties.SortedColumn = control.SortedColumn
        dgvSortProperties.SortOrder = control.SortOrder
    End Sub

    Friend Sub DocumentListDataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        view.DocumentListCountStatusText = view.DocumentListRowCount
        toolStripStateManager.SetStateForViewRowCount(view.DocumentListRowCount)
    End Sub

    Friend Sub DocumentListDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        DocumentListDataGridView_RowsAdded(Me, Nothing)
        toolStripStateManager.SetStateForViewCheckedRowCount(view.CheckedDocumentIdItems.Count)
    End Sub

    Friend Sub DocumentListDataGridView_SelectionChanged(sender As Object, e As EventArgs)
        With view
            ' Filter out null selections that occur when the DataGridView is filled.
            If .SelectedDocumentId > 0 Then
                Try
                    viewInstance.Cursor = Cursors.WaitCursor
                    If .DocumentRetrievalChoiceSelectedIndex = 0 Then
                        selectedDocument = documentSvc.ReadDocument(.SelectedDocumentId, .SearchTerm)
                    Else
                        selectedDocument = documentSvc.ReadDocument(.SelectedDocumentId, Nothing)
                    End If
                    Dim cachePdfTask = Task.Run(Sub() fileCacheSvc.AddPdfToCache(.SelectedDocumentId,
                                                                                 selectedDocument.Pdf))
                    .FlagState = selectedDocument.Flag
                    .Notes = selectedDocument.Notes
                    previousNotes = .Notes
                    .NotesChanged = False
                    .Keywords = selectedDocument.Keywords
                    .Text = selectedDocument.Text
                    .SearchTermSnippets = selectedDocument.SearchTermSnippets
                    cachePdfTask.Wait()
                    SetPreviewImage()
                    .DocumentTabControlEnabled = True
                Catch ex As DbException
                    commonDialogs.ShowMessageBox(ex.Message, True)
                Finally
                    viewInstance.Cursor = Cursors.Default
                End Try
                toolStripStateManager.SetStateForDocumentSelected(.DocumentTabControlEnabled)
            Else
                ClearSelectedDocument()
            End If
        End With
    End Sub

    Friend Sub DocumentListDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs)
        Dim control = CType(sender, DataGridView)
        If control.IsCurrentCellDirty Then
            control.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Friend Sub DocumentListDataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        toolStripStateManager.SetStateForViewCheckedRowCount(view.CheckedDocumentIdItems.Count)
    End Sub

    Friend Sub DocumentListDataGridView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        pdfSvc.ShowPdf(fileCacheSvc.GetCachedPdfFullName(view.SelectedDocumentId),
                       My.Settings.ShowPdfWithDefaultApplication)
    End Sub

    Friend Sub NotesTextBox_TextChanged(sender As Object, e As EventArgs)
        With view
            .Notes = .Notes.TrimStart
            If .Notes <> previousNotes Then
                .NotesChanged = True
            Else
                .NotesChanged = False
            End If
            toolStripStateManager.SetStateForTextBoxSelectedText(.NotesReadOnly, .Notes.Length, .NotesSelectionLength)
        End With
    End Sub

    Friend Sub TextBox_Enter(sender As Object, e As EventArgs)
        Dim textBox = CType(sender, TextBox)
        Dim printable = False
        If textBox.Name = "NotesTextBox" Or textBox.Name = "TextTextBox" Or
            textBox.Name = "SearchTermSnippetsTextBox" Then
            If textBox.Text.Length > 0 Then
                printable = True
            End If
        End If
        toolStripStateManager.SetStateForTextBoxEnter(textBox, printable)
        If textBox.Name = "NotesTextBox" Then
            If My.Computer.Clipboard.ContainsText Then
                toolStripStateManager.EnableEditPaste(True)
            End If
            toolStripStateManager.SetStateForNotesChanged(view.NotesChanged, textBox.CanUndo)
        End If
    End Sub

    Friend Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim textBox = CType(sender, TextBox)
        toolStripStateManager.SetStateForTextBoxSelectedText(textBox.ReadOnly, textBox.Text.Length,
                                                             textBox.SelectionLength)
    End Sub

    Friend Sub TextBox_MouseUp(sender As Object, e As MouseEventArgs)
        Dim textBox = CType(sender, TextBox)
        toolStripStateManager.SetStateForTextBoxSelectedText(textBox.ReadOnly, textBox.Text.Length,
                                                             textBox.SelectionLength)
    End Sub

    Friend Sub TextBox_Leave(sender As Object, e As EventArgs)
        toolStripStateManager.SetStateForTextBoxLeave()
    End Sub

    Friend Sub PreviewPictureBox_DoubleClick(sender As Object, e As EventArgs)
        SetPreviewPixelDensityDialog.ShowDialog()
        Task.Run(Sub() SetPreviewImage())
    End Sub

    Friend Sub StatusStrip_VisibleChanged(sender As Object, e As EventArgs)
        view.ViewStatusBarMenuItemChecked = view.StatusStripVisible
    End Sub

    Friend Sub UploadRejectedImageToolStripStatusLabel_Click(sender As Object, e As EventArgs)
        Process.Start(AppFolders.GetPath(AppFolders.AppFolder.UploadRejected))
    End Sub

    Friend Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        If textToPrint.Length = 0 Then
            textToPrint = view.DocumentTabControlSelectedTabTextBoxText
        End If
        Using font = New Font("Lucida Console", 10)
            Dim charactersOnPage = 0
            Dim linesPerPage = 0
            e.Graphics.MeasureString(textToPrint, font, e.MarginBounds.Size, StringFormat.GenericTypographic,
                                     charactersOnPage, linesPerPage)
            e.Graphics.DrawString(textToPrint, font, Brushes.Black, e.MarginBounds, StringFormat.GenericTypographic)
            textToPrint = textToPrint.Substring(charactersOnPage)
            e.HasMorePages = textToPrint.Length > 0
        End Using
    End Sub

    Friend Sub UpdateCheckTimer_Tick(sender As Object, e As EventArgs)
        AutoUpdater.Start(AppProperties.AutoUpdaterConfigUri.AbsoluteUri)
    End Sub

    Friend Async Sub FlaggedDocumentCheckTimer_Tick(sender As Object, e As EventArgs)
        Dim timer = CType(sender, Timer)
        Try
            timer.Stop()
            Dim count = Await Task.Run(
                Function() documentListSvc.ListFlaggedDocuments.Rows.Count).ConfigureAwait(True)
            If count > 0 Then
                view.FlagImageVisible = True
            Else
                view.FlagImageVisible = False
            End If
            Application.DoEvents()
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            timer.Start()
        End Try
    End Sub

    Friend Async Sub UploadFolderMaintenanceTimer_Tick(sender As Object, e As EventArgs)
        Dim timer = CType(sender, Timer)
        timer.Stop()
        Await Task.Run(Sub() uploadSvc.ExecuteUploadFolderMaintenance()).ConfigureAwait(True)
        timer.Start()
    End Sub

    Friend Async Sub UploadTimer_Tick(sender As Object, e As EventArgs)
        Dim timer = CType(sender, Timer)
        If uploadSvc.PdfFilesToUploadExist Then
            Try
                view.UploadRunningImageVisible = True
                Application.DoEvents()
                timer.Stop()
                Await Task.Run(Sub() uploadSvc.ExecuteUpload()).ConfigureAwait(True)
            Catch ex As DbException
                commonDialogs.ShowMessageBox(ex.Message, True)
            Finally
                If uploadSvc.DocumentsUploaded Then
                    TriggerDocumentListRefresh(True)
                End If
                timer.Start()
                view.UploadRunningImageVisible = False
                Application.DoEvents()
            End Try
        End If
    End Sub

    Friend Sub UploadRejectedFilesCheckTimer_Tick(sender As Object, e As EventArgs)
        Dim timer = CType(sender, Timer)
        timer.Stop()
        If uploadSvc.RejectedPdfFilesExist Then
            view.UploadRejectedImageVisible = True
        Else
            view.UploadRejectedImageVisible = False
        End If
        Application.DoEvents()
        timer.Start()
    End Sub

    Friend Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs)
        WaitForUploadToFinish()
        If view.NotesChanged Then
            view.DocumentTabControlSelectedIndex = 0
            view.ScrollToEndOfNotesText()
            Dim result = commonDialogs.ShowQuestionMessageBox(My.Resources.NotesModified, True)
            If result = DialogResult.Yes Then
                UpdateSelectedDocument()
            ElseIf result = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
        NativeMethods.RemoveClipboardFormatListener(viewInstance.Handle)
        SaveFormState()
    End Sub

    Private Enum CheckedDocumentsAction
        Open
        SetCategory
        SetTaxYear
        Delete
        Export
        Update
    End Enum

    Private Sub SetInitialFormState()
        With viewInstance
            If Not IsNothing(My.Settings.MainLocation) Then
                ' Workaround added for an occasional bug that can cause MainForm to be positioned off the screen.
                If My.Settings.MainLocation = New System.Drawing.Point(-32000, -32000) Then
                    .Location = New System.Drawing.Point(0, 0)
                Else
                    .Location = My.Settings.MainLocation
                End If
            End If
            If Not IsNothing(My.Settings.MainSize) Then
                .Size = My.Settings.MainSize
            End If
            .WindowState = My.Settings.MainWindowState
        End With
        view.SplitterDistance = My.Settings.MainSplitterDistance
    End Sub

    Private Sub StartupActions()
        If DbSession.Platform <> DbSession.DbPlatform.Sqlite Then
            If AppPolicies.GetValue(AppPolicies.AppPolicy.RemoveListAllDocuments) = 1 Then
                view.RemoveListAllDocumentsChoice()
            End If
        End If
        If My.Settings.ListFlaggedDocumentsOnStartup Then
            view.DocumentRetrievalChoiceSelectedIndex = 3
        End If
    End Sub

    Private Sub OnNotesChanged()
        Dim controlEnabled = False
        With view
            If .NotesChanged = False Then
                controlEnabled = True
                toolStripStateManager.SetStateForViewCheckedRowCount(.CheckedDocumentIdItems.Count)
            End If
            ' The "If" check is needed to prevent user from having to check/uncheck checkbox in
            ' DocumentListDataGridView when Document Notes length > 0.
            If .NotesFocused Then
                .DocumentRetrievalGroupEnabled = controlEnabled
                .DocumentListViewEnabled = controlEnabled
            End If
            If .SelectedDocumentId > 0 Then
                toolStripStateManager.SetStateForNotesChanged(.NotesChanged, .NotesCanUndo)
            End If
        End With
    End Sub

    Private Sub FindDocumentsBySearchTerm(ByVal usePreviousSearchTerm As Boolean)
        With view
            If usePreviousSearchTerm Then
                .SearchTerm = previousSearchTerm
            End If
            .SearchTermEnabled = True
            If .SearchTerm.Trim.Length > 0 Then
                Try
                    viewInstance.Cursor = Cursors.WaitCursor
                    ' Need to capture SearchTermComboBox.Text into a string and then set SearchTermComboBox.Text to that
                    ' string after the query to prevent SearchTermComboBox from selecting the first item in the drop down
                    ' list that contains the text in the text box after the query.
                    Dim currentSearchTerm = .SearchTerm
                    .DocumentList = documentListSvc.ListDocumentsBySearchTerm(.SearchTerm)
                    .SearchTerm = currentSearchTerm
                    If .DocumentListRowCount > 0 Then
                        searchTermHistorySvc.AddToHistory(.SearchTerm)
                    End If
                    previousSearchTerm = .SearchTerm
                    .FindBySearchTermEnabled = False
                    .SetErrorProviderMessage(Nothing)
                Catch ex As FormatException
                    .SetErrorProviderMessage(My.Resources.ImproperUsageOfQueryOperators)
                    .FindBySearchTermEnabled = False
                Catch ex As DbException
                    commonDialogs.ShowMessageBox(ex.Message, True)
                Finally
                    viewInstance.Cursor = Cursors.Default
                End Try
            Else
                ResetDocumentListDataSource()
            End If
            .SearchTermItems = searchTermHistorySvc.ListHistory
        End With
    End Sub

    Private Sub ListFlaggedDocuments()
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            view.DocumentList = documentListSvc.ListFlaggedDocuments
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ListAllDocuments()
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            view.DocumentList = documentListSvc.ListAllDocuments
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TriggerDocumentListRefresh(ByVal postUpload As Boolean)
        If view.DocumentRetrievalChoicesListEnabled Then
            If postUpload Then
                If view.CheckedDocumentIdItems.Count = 0 Then
                    ViewRefreshToolStrip_Click(Me, Nothing)
                End If
            Else
                If view.DocumentRetrievalChoiceSelectedIndex = 3 And view.FlagState = 0 Then
                    If commonDialogs.ShowQuestionMessageBox(My.Resources.RefreshDocuments,
                                                            False) = DialogResult.Yes Then
                        ViewRefreshToolStrip_Click(Me, Nothing)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ResetDocumentListDataSource()
        If view.DocumentList IsNot Nothing Then
            view.DocumentList.Reset()
        End If
        view.DocumentListColumn0AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub UpdateSelectedDocument()
        With view
            selectedDocument.Notes = .Notes.Trim
            selectedDocument.Flag = .FlagState
            Try
                viewInstance.Cursor = Cursors.WaitCursor
                documentSvc.UpdateDocument(.SelectedDocumentId, selectedDocument)
            Catch ex As IndexOutOfRangeException
                commonDialogs.ShowMessageBox(String.Format(CultureInfo.CurrentCulture,
                                                           My.Resources.ResourceManager.GetString(
                                                           "DocumentRecordMayHaveBeenDeleted",
                                                           CultureInfo.CurrentCulture), ex.Message), True)
                ClearSelectedDocument()
            Catch ex As DbException
                commonDialogs.ShowMessageBox(ex.Message, True)
            Finally
                viewInstance.Cursor = Cursors.Default
            End Try
        End With
    End Sub

    Private Sub ClearSelectedDocument()
        previousNotes = Nothing
        With view
            .FlagState = Nothing
            .DocumentTabControlEnabled = False
            .Notes = Nothing
            .Keywords = Nothing
            .Preview = Nothing
            .Text = Nothing
        End With
        toolStripStateManager.SetStateForDocumentSelected(False)
    End Sub

    Private Sub SetPreviewImage()
        fileCacheSvc.CreatePreviewInCache(view.SelectedDocumentId, My.Settings.PreviewPixelDensity)
        view.Preview = fileCacheSvc.GetPreviewFromCache(view.SelectedDocumentId, My.Settings.PreviewPixelDensity)
    End Sub

    Private Sub ProcessCheckedDocuments(ByVal action As CheckedDocumentsAction, ByVal strParam As String,
                                        ByVal boolParam As Boolean)
        Dim openMaximum = 12
        view.ProgressBarVisible = True
        view.ProgressBarMaximum = view.CheckedDocumentIdItems.Count
        If action = CheckedDocumentsAction.Open Then
            If view.CheckedDocumentIdItems.Count > openMaximum Then
                view.ProgressBarMaximum = openMaximum
                commonDialogs.ShowMessageBox(String.Format(CultureInfo.CurrentCulture,
                                                           My.Resources.ResourceManager.GetString(
                                                           "OpenCheckedDocumentsMaximumReached",
                                                           CultureInfo.CurrentCulture), openMaximum), False)
            End If
        End If
        If action = CheckedDocumentsAction.Export Then
            strParam = Path.Combine(strParam, String.Concat(My.Application.Info.ProductName, "-",
                                                            My.Resources.Export, "_",
                                                            DateTime.Now.ToString("yyyy-MM-dd_HH.mm",
                                                                                  CultureInfo.CurrentCulture)))
            Directory.CreateDirectory(strParam)
        End If
        Dim count = 0   ' Used by Open only.
        For Each id In view.CheckedDocumentIdItems()
            Try
                viewInstance.Cursor = Cursors.WaitCursor
                If action = CheckedDocumentsAction.Open Then
                    count += 1
                    If count <= openMaximum Then
                        OpenDocument(id)
                    End If
                ElseIf action = CheckedDocumentsAction.SetCategory Then
                    UpdateDocument(id, CheckedDocumentsAction.SetCategory, strParam)
                ElseIf action = CheckedDocumentsAction.SetTaxYear Then
                    UpdateDocument(id, CheckedDocumentsAction.SetTaxYear, strParam)
                ElseIf action = CheckedDocumentsAction.Delete Then
                    documentSvc.DeleteDocument(id)
                ElseIf action = CheckedDocumentsAction.Export Then
                    ExportDocument(id, strParam)
                ElseIf action = CheckedDocumentsAction.Update Then
                    UpdatePdfTextColumns(id, boolParam)
                End If
            Catch ex As InvalidOperationException
                commonDialogs.ShowMessageBox(String.Format(CultureInfo.CurrentCulture,
                                                           My.Resources.ResourceManager.GetString(
                                                           "DocumentMayHaveBeenDeletedException",
                                                           CultureInfo.CurrentCulture), ex.Message, id), True)
            Catch ex As IndexOutOfRangeException
                commonDialogs.ShowMessageBox(String.Format(CultureInfo.CurrentCulture,
                                                           My.Resources.ResourceManager.GetString(
                                                           "DocumentMayHaveBeenDeletedException",
                                                           CultureInfo.CurrentCulture), ex.Message, id), True)
            Catch ex As DbException
                commonDialogs.ShowMessageBox(String.Format(CultureInfo.CurrentCulture,
                                                           My.Resources.ResourceManager.GetString(
                                                           "DocumentDatabaseException", CultureInfo.CurrentCulture),
                                                           ex.Message, id), True)
            Finally
                viewInstance.Cursor = Cursors.Default
            End Try
            view.PerformStepOnProgressBar()
            Application.DoEvents()
        Next
        view.ProgressBarVisible = False
        If action = CheckedDocumentsAction.SetCategory Or CheckedDocumentsAction.SetTaxYear Or
            CheckedDocumentsAction.Delete Then
            ViewRefreshToolStrip_Click(Me, Nothing)
        Else
            FileSelectNoneToolStrip_Click(Me, Nothing)
        End If
    End Sub

    Private Sub OpenDocument(ByVal id As Integer)
        Dim document = documentSvc.ReadDocument(id, Nothing)
        fileCacheSvc.AddPdfToCache(id, document.Pdf)
        pdfSvc.ShowPdf(fileCacheSvc.GetCachedPdfFullName(id), My.Settings.ShowPdfWithDefaultApplication)
    End Sub

    Private Sub UpdateDocument(ByVal id As Integer, ByVal action As CheckedDocumentsAction, ByVal value As String)
        Dim document = documentSvc.ReadDocument(id, Nothing)
        If action = CheckedDocumentsAction.SetCategory Then
            document.Category = value
        ElseIf action = CheckedDocumentsAction.SetTaxYear Then
            document.TaxYear = value
        End If
        documentSvc.UpdateDocument(id, document)
    End Sub

    Private Sub ExportDocument(ByVal id As Integer, ByVal exportPath As String)
        Dim document = documentSvc.ReadDocument(id, Nothing)
        Dim authorPath = Path.Combine(exportPath, document.Author)
        Dim subjectPath = Path.Combine(authorPath, document.Subject)
        Directory.CreateDirectory(authorPath)
        Directory.CreateDirectory(subjectPath)
        Dim pdfPath = Path.Combine(subjectPath, String.Concat("[", id, "]", document.Title, ".pdf"))
        document.Pdf.ToFile(pdfPath)
        Dim pdfInfo = pdfSvc.ReadPdfInfo(pdfPath, Nothing)
        With pdfInfo
            If .Title <> document.Title Or .Author <> document.Author Or .Subject <> document.Subject Or
                    .Keywords <> document.Keywords Then
                Dim tempPdfPath = Path.Combine(AppFolders.GetPath(AppFolders.AppFolder.Temp),
                                               Path.GetFileName(pdfPath))
                .Title = document.Title
                .Author = document.Author
                .Subject = document.Subject
                .Keywords = document.Keywords
                pdfSvc.WritePdfWithInfo(pdfPath, Nothing, tempPdfPath, pdfInfo)
                'TODO: Overwrite parameter was added to File.Move in .NET5
                IO.File.Delete(pdfPath)
                IO.File.Move(tempPdfPath, pdfPath)
            End If
        End With
        Dim pdfInfoExt = New PdfInfoExtModel
        With pdfInfoExt
            .Notes = document.Notes
            .Category = document.Category
            .TaxYear = document.TaxYear
            .Flag = document.Flag
            pdfSvc.WritePdfInfoExt(pdfPath, pdfInfoExt)
        End With
    End Sub

    Private Sub UpdatePdfTextColumns(ByVal id As Integer, ByVal ocrImageDataPages As Boolean)
        Dim document = documentSvc.ReadDocument(id, Nothing)
        fileCacheSvc.AddPdfToCache(id, document.Pdf)
        Dim cachedPdfPath = fileCacheSvc.GetCachedPdfFullName(id)
        Dim pdf = New PdfFile(cachedPdfPath)
        document.TextAnnotations = pdf.GetTextAnnot
        document.Text = pdf.GetText(ocrImageDataPages)
        documentSvc.UpdateDocument(id, document)
        fileCacheSvc.DeletePdfFromCache(id)
    End Sub

    Private Sub WaitForUploadToFinish()
        Do While view.UploadRunningImageVisible
            Threading.Thread.Sleep(5000)
        Loop
    End Sub

    Private Sub SaveFormState()
        With viewInstance
            My.Settings.MainSplitterDistance = view.SplitterDistance
            My.Settings.MainLocation = .Location
            If .WindowState = FormWindowState.Normal Then
                My.Settings.MainSize = .Size
            Else
                My.Settings.MainSize = .Size
            End If
            My.Settings.MainWindowState = .WindowState
        End With
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                toolStripStateManager.Dispose()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
