' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

Imports System.Collections.ObjectModel
Imports System.Diagnostics.CodeAnalysis
Imports PDFKeeper.Core.Application
Imports PDFKeeper.Core.Commands
Imports PDFKeeper.Core.Interop
Imports PDFKeeper.Core.Presenters
Imports PDFKeeper.Core.ViewModels
Imports PDFKeeper.PDFViewer.Services

Public Class MainView
    Private ReadOnly presenter As MainPresenter
    Private ReadOnly viewModel As MainViewModel
    Private ReadOnly dataGridViewSortProperties As DataGridViewSortProperties
    Private ReadOnly helpFile As HelpFile

    ' Message that is sent when the contents of the clipboard have changed.
    Private Const WM_CLIPBOARDUPDATE = &H31D

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        presenter = New MainPresenter(New PdfViewerService, New FolderBrowserDialogService,
                                      New MessageBoxService, New FolderExplorerService,
                                      New SetTitleDialogService, New SetAuthorDialogService,
                                      New SetSubjectDialogService, New SetCategoryDialogService,
                                      New SetTaxYearDialogService, New OpenFileDialogService,
                                      New SaveFileDialogService, New PrintDialogService,
                                      New PrintPreviewDialogService)
        viewModel = presenter.ViewModel
        MainViewModelBindingSource.DataSource = presenter.ViewModel
        dataGridViewSortProperties = New DataGridViewSortProperties
        helpFile = New HelpFile
        HelpProvider.HelpNamespace = helpFile.FileName
        AddEventHandlers()
        AddTags()
    End Sub

    Private Sub AddEventHandlers()
        AddHandler FileAddToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FileAddToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler FileOpenToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FileOpenToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler FileSaveToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FileSaveToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler FileSaveAsToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FileBurstToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FileBurstToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler FilePrintToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FilePrintToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler FilePrintPreviewToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FileExportToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler FileExitToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditUndoToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditUndoToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler EditCutToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditCutToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler EditCopyToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditCopyToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler EditPasteToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditPasteToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler EditSelectAllToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditRestoreToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditRestoreToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler EditAppendDateTimeToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditAppendDateTimeToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler EditAppendTextToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler EditAppendTextToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler EditFlagDocumentToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsFindToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsFindToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsSelectAllToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsSelectNoneToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsSetTitleToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsSetAuthorToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsSetSubjectToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsSetCategoryToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsSetTaxYearToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsDeleteToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler DocumentsDeleteToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler ViewToolStripMenuItem.DropDownOpened, AddressOf ToolStripItem_Click
        AddHandler ViewSetPreviewPixelDensityToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler ViewToolBarToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler ViewStatusBarToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler ToolsOptionsToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler ToolsOptionsToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler ToolsUploadProfilesToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler ToolsUploadProfilesToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler ToolsMoveDatabaseToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler HelpContentsToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler HelpContentsToolStripButton.Click, AddressOf ToolStripItem_Click
        AddHandler HelpAboutToolStripMenuItem.Click, AddressOf ToolStripItem_Click
        AddHandler presenter.LongRunningOperationStarted,
            AddressOf MainView_LongRunningOperationStarted
        AddHandler presenter.LongRunningOperationFinished,
            AddressOf MainView_LongRunningOperationFinished
        AddHandler presenter.CheckedDocumentsProcessed,
            AddressOf MainView_CheckedDocumentsProcessed
        AddHandler presenter.ScrollToEndOfNotesTextRequested,
            AddressOf MainView_ScrollToEndOfNotesTextRequested
        AddHandler presenter.ProgressBarPerformStepRequested,
            AddressOf MainView_ProgressBarPerformStepRequested
        AddHandler viewModel.PropertyChanged, AddressOf MainView_PropertyChanged
    End Sub

    <SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")>
    Private Sub AddTags()
        FileAddToolStripMenuItem.Tag = New DialogShowCommand(New AddPdfView, Nothing)
        FileAddToolStripButton.Tag = New DialogShowCommand(New AddPdfView, Nothing)
        FileOpenToolStripMenuItem.Tag = New OpenPdfCommand(presenter)
        FileOpenToolStripButton.Tag = New OpenPdfCommand(presenter)
        FileSaveToolStripMenuItem.Tag = New SaveCommand(presenter)
        FileSaveToolStripButton.Tag = New SaveCommand(presenter)
        FileSaveAsToolStripMenuItem.Tag = New SaveAsCommand(presenter)
        FileBurstToolStripMenuItem.Tag = New BurstPdfCommand(presenter)
        FileBurstToolStripButton.Tag = New BurstPdfCommand(presenter)
        FilePrintToolStripMenuItem.Tag = New PrintTextCommand(presenter, False)
        FilePrintToolStripButton.Tag = New PrintTextCommand(presenter, False)
        FilePrintPreviewToolStripMenuItem.Tag = New PrintTextCommand(presenter, True)
        FileExportToolStripMenuItem.Tag = New ExportCommand(presenter)
        FileExitToolStripMenuItem.Tag = New ViewCloseCommand(Me)
        EditUndoToolStripMenuItem.Tag = New UndoCommand(Me)
        EditUndoToolStripButton.Tag = New UndoCommand(Me)
        EditCutToolStripMenuItem.Tag = New CutCommand(Me, presenter)
        EditCutToolStripButton.Tag = New CutCommand(Me, presenter)
        EditCopyToolStripMenuItem.Tag = New CopyCommand(Me)
        EditCopyToolStripButton.Tag = New CopyCommand(Me)
        EditPasteToolStripMenuItem.Tag = New PasteCommand(Me)
        EditPasteToolStripButton.Tag = New PasteCommand(Me)
        EditSelectAllToolStripMenuItem.Tag = New SelectAllCommand(Me, presenter)
        EditRestoreToolStripMenuItem.Tag = New RestoreCommand(presenter)
        EditRestoreToolStripButton.Tag = New RestoreCommand(presenter)
        EditAppendDateTimeToolStripMenuItem.Tag = New AppendDateTimeIntoNotesCommand(presenter)
        EditAppendDateTimeToolStripButton.Tag = New AppendDateTimeIntoNotesCommand(presenter)
        EditAppendTextToolStripMenuItem.Tag = New AppendTextFromFileIntoNotes(presenter)
        EditAppendTextToolStripButton.Tag = New AppendTextFromFileIntoNotes(presenter)
        EditFlagDocumentToolStripMenuItem.Tag = New FlagStateToggleCommand(presenter)
        DocumentsFindToolStripMenuItem.Tag = New DialogShowCommand(FindDocumentsView, Nothing)
        DocumentsFindToolStripButton.Tag = New DialogShowCommand(FindDocumentsView, Nothing)
        DocumentsSelectAllToolStripMenuItem.Tag = New SelectAllDocumentsCommand(Me, True)
        DocumentsSelectNoneToolStripMenuItem.Tag = New SelectAllDocumentsCommand(Me, False)
        DocumentsSetTitleToolStripMenuItem.Tag = New SetTitleCommand(presenter)
        DocumentsSetAuthorToolStripMenuItem.Tag = New SetAuthorCommand(presenter)
        DocumentsSetSubjectToolStripMenuItem.Tag = New SetSubjectCommand(presenter)
        DocumentsSetCategoryToolStripMenuItem.Tag = New SetCategoryCommand(presenter)
        DocumentsSetTaxYearToolStripMenuItem.Tag = New SetTaxYearCommand(presenter)
        DocumentsDeleteToolStripMenuItem.Tag = New DeleteCommand(presenter)
        DocumentsDeleteToolStripButton.Tag = New DeleteCommand(presenter)
        ViewToolStripMenuItem.Tag = New ViewCommand(Me)
        ViewSetPreviewPixelDensityToolStripMenuItem.Tag = New DialogShowCommand(
            SetPreviewPixelDensityDialog, New SetPreviewImageCommand(presenter))
        ViewToolBarToolStripMenuItem.Tag = New ToolBarToggleCommand(Me)
        ViewStatusBarToolStripMenuItem.Tag = New StatusBarToggleCommand(Me)
        ToolsOptionsToolStripMenuItem.Tag = New DialogShowCommand(OptionsDialog, Nothing)
        ToolsOptionsToolStripButton.Tag = New DialogShowCommand(OptionsDialog, Nothing)
        ToolsUploadProfilesToolStripMenuItem.Tag = New DialogShowCommand(UploadProfilesView,
                                                                         Nothing)
        ToolsUploadProfilesToolStripButton.Tag = New DialogShowCommand(UploadProfilesView, Nothing)
        ToolsMoveDatabaseToolStripMenuItem.Tag = New MoveDatabaseCommand(presenter)
        HelpContentsToolStripMenuItem.Tag = New HelpFileShowCommand(helpFile, Me)
        HelpContentsToolStripButton.Tag = New HelpFileShowCommand(helpFile, Me)
        HelpAboutToolStripMenuItem.Tag = New DialogShowCommand(AboutBox, Nothing)
    End Sub

    Private Sub MyBaseWndProc(ByRef m As Message)
        If m.Msg.Equals(WM_CLIPBOARDUPDATE) Then
            If My.Computer.Clipboard.ContainsText Then
                If NotesTextBox.Focused Then
                    presenter.SetPasteEnabledState(True)
                End If
            End If
        End If
    End Sub

    Private Sub MainView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NativeMethods.AddClipboardFormatListener(Handle)
        UpdateCheckTimer_Tick(Me, Nothing)
        GetFormState()
        presenter.SetInitialState()
        If My.Settings.FindFlaggedDocumentsOnStartup Then
            presenter.GetListOfFlaggedDocuments()
        End If
    End Sub

    Private Sub GetFormState()
        If Not IsNothing(My.Settings.MainViewLocation) Then
            ' Workaround for rare bug that can cause this form to be positioned off the screen.
            If My.Settings.MainViewLocation.Equals(New Point(-32000, -32000)) Then
                Location = New Point(0, 0)
            Else
                Location = My.Settings.MainViewLocation
            End If
        End If
        If Not IsNothing(My.Settings.MainViewSize) Then
            Size = My.Settings.MainViewSize
        End If
        WindowState = My.Settings.MainViewState
        HorizontalSplitContainer.SplitterDistance = My.Settings.HorizontalSplitterDistance
        VerticalSplitContainer.SplitterDistance = My.Settings.VerticalSplitterDistance
    End Sub

    Private Sub ToolStripItem_Click(sender As Object, e As EventArgs)
        If sender.GetType.Name.Equals("ToolStripMenuItem", StringComparison.Ordinal) Then
            presenter.ExecuteCommand(TryCast(CType(sender, ToolStripMenuItem).Tag, ICommand))
        ElseIf sender.GetType.Name.Equals("ToolStripButton", StringComparison.Ordinal) Then
            presenter.ExecuteCommand(TryCast(CType(sender, ToolStripButton).Tag, ICommand))
        End If
    End Sub

    Private Sub DocumentsDataGridView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DocumentsDataGridView.CellDoubleClick
        ToolStripItem_Click(FileOpenToolStripMenuItem, Nothing)
    End Sub

    Private Sub DocumentsDataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DocumentsDataGridView.CellValueChanged
        presenter.SetCheckedDocumentIds(GetCheckedDocumentIds)
    End Sub

    Private Sub DocumentsDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DocumentsDataGridView.CurrentCellDirtyStateChanged
        If DocumentsDataGridView.IsCurrentCellDirty Then
            DocumentsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DocumentsDataGridView_DataSourceChanged(sender As Object, e As EventArgs) Handles DocumentsDataGridView.DataSourceChanged
        With DocumentsDataGridView
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
                .Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                If .Columns(7).Displayed = True Then
                    .Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If
                .Columns(7).MinimumWidth = .Columns(7).FillWeight + 20
                .Sort(.Columns(dataGridViewSortProperties.SortColumnIndex),
                      dataGridViewSortProperties.SortDirection)
                If My.Settings.SelectLastDocumentRow Then
                    .Rows(.Rows.Count - 1).Selected = True
                    .FirstDisplayedScrollingRowIndex = .RowCount - 1
                End If
            End If
        End With
    End Sub

    Private Sub DocumentsDataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DocumentsDataGridView.RowsAdded
        DocumentsCountLabel.Text = DocumentsDataGridView.Rows.Count
    End Sub

    Private Sub DocumentsDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DocumentsDataGridView.RowsRemoved
        DocumentsDataGridView_RowsAdded(Me, Nothing)
        DocumentsDataGridView_CellValueChanged(Me, Nothing)
    End Sub

    Private Sub DocumentsDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles DocumentsDataGridView.SelectionChanged
        viewModel.CurrentDocumentId = 0 ' No row is selected.
        If DocumentsDataGridView.SelectedRows.Count > 0 Then ' To prevent empty DataGridView.
            viewModel.CurrentDocumentId = DocumentsDataGridView.SelectedRows(0).Cells(1).Value
        End If
        presenter.DocumentSelectionChanged(My.Settings.PreviewPixelDensity)
    End Sub

    Private Sub DocumentsDataGridView_Sorted(sender As Object, e As EventArgs) Handles DocumentsDataGridView.Sorted
        dataGridViewSortProperties.SortedColumn = DocumentsDataGridView.SortedColumn
        dataGridViewSortProperties.SortOrder = DocumentsDataGridView.SortOrder
    End Sub

    Private Sub TextBox_Enter(sender As Object, e As EventArgs) Handles NotesTextBox.GotFocus, NotesTextBox.Enter, TextTextBox.Enter, SearchTermSnippetsTextBox.Enter, KeywordsTextBox.Enter
        Dim textBox = CType(sender, TextBox)
        SetTextBoxFocusedState(textBox, True)
        presenter.SetTextBoxEnterState(textBox.CanUndo)
    End Sub

    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NotesTextBox.KeyPress, TextTextBox.KeyPress, SearchTermSnippetsTextBox.KeyPress, KeywordsTextBox.KeyPress
        TextBoxHelper.SyncSelectedTextWithViewModel(CType(sender, TextBox), Me, viewModel)
        presenter.SetStateForTextBoxSelectedText()
    End Sub

    Private Sub TextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles NotesTextBox.MouseUp, TextTextBox.MouseUp, SearchTermSnippetsTextBox.MouseUp, KeywordsTextBox.MouseUp
        TextBoxHelper.SyncSelectedTextWithViewModel(CType(sender, TextBox), Me, viewModel)
        presenter.SetStateForTextBoxSelectedText()
    End Sub

    Private Sub TextBox_Leave(sender As Object, e As EventArgs) Handles NotesTextBox.Leave, TextTextBox.Leave, SearchTermSnippetsTextBox.Leave, KeywordsTextBox.Leave
        SetTextBoxFocusedState(CType(sender, TextBox), False)
        presenter.SetTextBoxLeaveState()
    End Sub

    Private Sub UploadRejectedImageToolStripStatusLabel_Click(sender As Object, e As EventArgs) Handles UploadRejectedImageLabel.Click
        presenter.ExploreUploadRejected()
    End Sub

    Private Sub UpdateCheckTimer_Tick(sender As Object, e As EventArgs) Handles UpdateCheckTimer.Tick
        AutoUpdater.RunUpdateAsAdmin = False
        AutoUpdater.Start(ApplicationUri.AutoUpdaterConfig.AbsoluteUri)
    End Sub

    Private Async Sub CheckForFlaggedDocumentsTimer_Tick(sender As Object, e As EventArgs) Handles CheckForFlaggedDocumentsTimer.Tick
        CheckForFlaggedDocumentsTimer.Stop()
        Await Task.Run(Sub() presenter.CheckForFlaggedDocuments()).ConfigureAwait(True)
        CheckForFlaggedDocumentsTimer.Start()
    End Sub

    Private Sub CheckForDocumentsListChangesTimer_Tick(sender As Object, e As EventArgs) Handles CheckForDocumentsListChangesTimer.Tick
        CheckForDocumentsListChangesTimer.Stop()
        presenter.CheckForDocumentsListChanges()
        CheckForDocumentsListChangesTimer.Start()
    End Sub

    Private Async Sub UploadTimer_Tick(sender As Object, e As EventArgs) Handles UploadTimer.Tick
        UploadTimer.Stop()
        Await Task.Run(Sub() presenter.ExecuteUploadDirectoryMaintenance()).ConfigureAwait(True)
        Await Task.Run(Sub() presenter.ExecuteUpload()).ConfigureAwait(True)
        presenter.CheckForRejectedPdfFiles()
        UploadTimer.Start()
    End Sub

    Private Sub DocumentsListTimedRefreshTimer_Tick(sender As Object, e As EventArgs) Handles DocumentsListTimedRefreshTimer.Tick
        DocumentsListTimedRefreshTimer.Stop()
        presenter.SetDocumentsListHasChanges()
        DocumentsListTimedRefreshTimer.Start()
    End Sub

    Private Sub MainView_LongRunningOperationStarted(sender As Object, e As EventArgs)
        Cursor = Cursors.WaitCursor
    End Sub

    Private Sub MainView_LongRunningOperationFinished(sender As Object, e As EventArgs)
        Cursor = Cursors.Default
    End Sub

    Private Sub MainView_CheckedDocumentsProcessed(sender As Object, e As EventArgs)
        ToolStripItem_Click(DocumentsSelectNoneToolStripMenuItem, Nothing)
    End Sub

    Private Sub MainView_ScrollToEndOfNotesTextRequested(sender As Object, e As EventArgs)
        NotesTextBox.Select()
        NotesTextBox.Select(NotesTextBox.Text.Length, 0)
        NotesTextBox.ScrollToCaret()
    End Sub

    Private Sub MainView_ProgressBarPerformStepRequested(sender As Object, e As EventArgs)
        ProgressBar.PerformStep()
        Application.DoEvents()
    End Sub

    <SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")>
    Private Sub MainView_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName.Equals("FileOpenMenuEnabled", StringComparison.Ordinal) Then
            FileOpenToolStripMenuItem.Enabled = viewModel.FileOpenMenuEnabled
            FileOpenToolStripButton.Enabled = viewModel.FileOpenMenuEnabled
        ElseIf e.PropertyName.Equals("FileSaveMenuEnabled", StringComparison.Ordinal) Then
            FileSaveToolStripMenuItem.Enabled = viewModel.FileSaveMenuEnabled
            FileSaveToolStripButton.Enabled = viewModel.FileSaveMenuEnabled
        ElseIf e.PropertyName.Equals("FileSaveAsMenuEnabled", StringComparison.Ordinal) Then
            FileSaveAsToolStripMenuItem.Enabled = viewModel.FileSaveAsMenuEnabled
        ElseIf e.PropertyName.Equals("FileBurstMenuEnabled", StringComparison.Ordinal) Then
            FileBurstToolStripMenuItem.Enabled = viewModel.FileBurstMenuEnabled
            FileBurstToolStripButton.Enabled = viewModel.FileBurstMenuEnabled
        ElseIf e.PropertyName.Equals("FilePrintMenuEnabled", StringComparison.Ordinal) Then
            FilePrintToolStripMenuItem.Enabled = viewModel.FilePrintMenuEnabled
            FilePrintToolStripButton.Enabled = viewModel.FilePrintMenuEnabled
        ElseIf e.PropertyName.Equals("FilePrintPreviewMenuEnabled", StringComparison.Ordinal) Then
            FilePrintPreviewToolStripMenuItem.Enabled = viewModel.FilePrintPreviewMenuEnabled
        ElseIf e.PropertyName.Equals("FileExportMenuEnabled", StringComparison.Ordinal) Then
            FileExportToolStripMenuItem.Enabled = viewModel.FileExportMenuEnabled
        ElseIf e.PropertyName.Equals("EditUndoMenuEnabled", StringComparison.Ordinal) Then
            EditUndoToolStripMenuItem.Enabled = viewModel.EditUndoMenuEnabled
            EditUndoToolStripButton.Enabled = viewModel.EditUndoMenuEnabled
        ElseIf e.PropertyName.Equals("EditCutMenuEnabled", StringComparison.Ordinal) Then
            EditCutToolStripMenuItem.Enabled = viewModel.EditCutMenuEnabled
            EditCutToolStripButton.Enabled = viewModel.EditCutMenuEnabled
        ElseIf e.PropertyName.Equals("EditCopyMenuEnabled", StringComparison.Ordinal) Then
            EditCopyToolStripMenuItem.Enabled = viewModel.EditCopyMenuEnabled
            EditCopyToolStripButton.Enabled = viewModel.EditCopyMenuEnabled
        ElseIf e.PropertyName.Equals("EditPasteMenuEnabled", StringComparison.Ordinal) Then
            EditPasteToolStripMenuItem.Enabled = viewModel.EditPasteMenuEnabled
            EditPasteToolStripButton.Enabled = viewModel.EditPasteMenuEnabled
        ElseIf e.PropertyName.Equals("EditSelectAllMenuEnabled", StringComparison.Ordinal) Then
            EditSelectAllToolStripMenuItem.Enabled = viewModel.EditSelectAllMenuEnabled
        ElseIf e.PropertyName.Equals("EditRestoreMenuEnabled", StringComparison.Ordinal) Then
            EditRestoreToolStripMenuItem.Enabled = viewModel.EditRestoreMenuEnabled
            EditRestoreToolStripButton.Enabled = viewModel.EditRestoreMenuEnabled
        ElseIf e.PropertyName.Equals("EditAppendDateTimeMenuEnabled",
                                     StringComparison.Ordinal) Then
            EditAppendDateTimeToolStripMenuItem.Enabled = viewModel.EditAppendDateTimeMenuEnabled
            EditAppendDateTimeToolStripButton.Enabled = viewModel.EditAppendDateTimeMenuEnabled
        ElseIf e.PropertyName.Equals("EditAppendTextMenuEnabled", StringComparison.Ordinal) Then
            EditAppendTextToolStripMenuItem.Enabled = viewModel.EditAppendTextMenuEnabled
            EditAppendTextToolStripButton.Enabled = viewModel.EditAppendTextMenuEnabled
        ElseIf e.PropertyName.Equals("EditFlagDocumentMenuEnabled", StringComparison.Ordinal) Then
            EditFlagDocumentToolStripMenuItem.Enabled = viewModel.EditFlagDocumentMenuEnabled
        ElseIf e.PropertyName.Equals("EditFlagDocumentMenuChecked", StringComparison.Ordinal) Then
            EditFlagDocumentToolStripMenuItem.Checked = viewModel.EditFlagDocumentMenuChecked
        ElseIf e.PropertyName.Equals("DocumentsFindMenuEnabled", StringComparison.Ordinal) Then
            DocumentsFindToolStripMenuItem.Enabled = viewModel.DocumentsFindMenuEnabled
            DocumentsFindToolStripButton.Enabled = viewModel.DocumentsFindMenuEnabled
        ElseIf e.PropertyName.Equals("DocumentsSelectMenuEnabled", StringComparison.Ordinal) Then
            DocumentsSelectToolStripMenuItem.Enabled = viewModel.DocumentsSelectMenuEnabled
        ElseIf e.PropertyName.Equals("DocumentsSetTitleMenuEnabled", StringComparison.Ordinal) Then
            DocumentsSetTitleToolStripMenuItem.Enabled = viewModel.DocumentsSetTitleMenuEnabled
        ElseIf e.PropertyName.Equals("DocumentsSetAuthorMenuEnabled",
                                     StringComparison.Ordinal) Then
            DocumentsSetAuthorToolStripMenuItem.Enabled = viewModel.DocumentsSetAuthorMenuEnabled
        ElseIf e.PropertyName.Equals("DocumentsSetSubjectMenuEnabled",
                                     StringComparison.Ordinal) Then
            DocumentsSetSubjectToolStripMenuItem.Enabled = viewModel.DocumentsSetSubjectMenuEnabled
        ElseIf e.PropertyName.Equals("DocumentsSetCategoryMenuEnabled",
                                     StringComparison.Ordinal) Then
            DocumentsSetCategoryToolStripMenuItem.Enabled =
                viewModel.DocumentsSetCategoryMenuEnabled
        ElseIf e.PropertyName.Equals("DocumentsSetTaxYearMenuEnabled",
                                     StringComparison.Ordinal) Then
            DocumentsSetTaxYearToolStripMenuItem.Enabled = viewModel.DocumentsSetTaxYearMenuEnabled
        ElseIf e.PropertyName.Equals("DocumentsDeleteMenuEnabled", StringComparison.Ordinal) Then
            DocumentsDeleteToolStripMenuItem.Enabled = viewModel.DocumentsDeleteMenuEnabled
            DocumentsDeleteToolStripButton.Enabled = viewModel.DocumentsDeleteMenuEnabled
        ElseIf e.PropertyName.Equals("ViewSetPreviewPixelDensityMenuEnabled",
                                     StringComparison.Ordinal) Then
            ViewSetPreviewPixelDensityToolStripMenuItem.Enabled =
                viewModel.ViewSetPreviewPixelDensityMenuEnabled
        ElseIf e.PropertyName.Equals("ToolsMoveDatabaseMenuVisible", StringComparison.Ordinal) Then
            ToolsMoveDatabaseToolStripMenuItem.Visible = viewModel.ToolsMoveDatabaseMenuVisible
        ElseIf e.PropertyName.Equals("Documents", StringComparison.Ordinal) Then
            DocumentsDataGridView.DataSource = viewModel.Documents
        ElseIf e.PropertyName.Equals("CurrentDocumentId", StringComparison.Ordinal) Then
            If viewModel.CurrentDocumentId > 0 Then
                For Each row In DocumentsDataGridView.Rows
                    If row.Cells(1).Value = viewModel.CurrentDocumentId Then
                        DocumentsDataGridView.CurrentCell = DocumentsDataGridView.Rows(row.Index).Cells(1)
                    End If
                Next
            End If
        ElseIf e.PropertyName.Equals("Notes", StringComparison.Ordinal) Then
            NotesTextBox.Text = NotesTextBox.Text.TrimStart
            presenter.OnNotesTextChanged(NotesTextBox.CanUndo)
        ElseIf e.PropertyName.Equals("ProgressBarVisible", StringComparison.Ordinal) Then
            ProgressBar.Visible = viewModel.ProgressBarVisible
        ElseIf e.PropertyName.Equals("ProgressBarMaximum", StringComparison.Ordinal) Then
            ProgressBar.Maximum = viewModel.ProgressBarMaximum
        ElseIf e.PropertyName.Equals("RefreshingDocumentsImageVisible",
                                     StringComparison.Ordinal) Then
            RefreshingDocumentsImageLabel.Visible = viewModel.RefreshingDocumentsImageVisible
            Application.DoEvents()
        ElseIf e.PropertyName.Equals("FlagImageVisible", StringComparison.Ordinal) Then
            FlagImageLabel.Visible = viewModel.FlagImageVisible
            Application.DoEvents()
        ElseIf e.PropertyName.Equals("UploadRunningImageVisible", StringComparison.Ordinal) Then
            UploadRunningImageLabel.Visible = viewModel.UploadRunningImageVisible
            Application.DoEvents()
        ElseIf e.PropertyName.Equals("UploadRejectedImageVisible", StringComparison.Ordinal) Then
            UploadRejectedImageLabel.Visible = viewModel.UploadRejectedImageVisible
            Application.DoEvents()
        End If
    End Sub

    Private Sub MainView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If viewModel.NotesChanged Then
            presenter.SaveNotesPromptBeforeClosing()
        End If
        e.Cancel = presenter.CancelViewClosing
        presenter.WaitForUploadToFinish()
        NativeMethods.RemoveClipboardFormatListener(Handle)
        SetFormState()
    End Sub

    Private Sub SetFormState()
        My.Settings.HorizontalSplitterDistance = HorizontalSplitContainer.SplitterDistance
        My.Settings.VerticalSplitterDistance = VerticalSplitContainer.SplitterDistance
        My.Settings.MainViewLocation = Location
        If WindowState.Equals(FormWindowState.Normal) Then
            My.Settings.MainViewSize = Size
        End If
        My.Settings.MainViewState = WindowState
    End Sub

    Private Function GetCheckedDocumentIds() As Collection(Of Integer)
        Dim ids = New Collection(Of Integer)
        For Each row In DocumentsDataGridView.Rows
            If row.Cells(0).Value = True Then
                ids.Add(row.Cells(1).Value)
            End If
        Next
        Return ids
    End Function

    ''' <summary>
    ''' Sets the text box focused state in the ViewModel.
    ''' </summary>
    ''' <param name="textBox">The TextBox object.</param>
    ''' <param name="enabled">True or False to set focus to enabled.</param>
    Private Sub SetTextBoxFocusedState(textBox As TextBox, enabled As Boolean)
        If textBox.Equals(NotesTextBox) Then
            viewModel.NotesFocused = enabled
        ElseIf textBox.Equals(KeywordsTextBox) Then
            viewModel.KeywordsFocused = enabled
        ElseIf textBox.Equals(TextTextBox) Then
            viewModel.TextFocused = enabled
        ElseIf textBox.Equals(SearchTermSnippetsTextBox) Then
            viewModel.SearchTermSnippetsFocused = enabled
        End If
    End Sub
End Class
