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
Imports System.Drawing.Imaging

Public Class MainForm
    Implements IMainWindowStateView, IToolStripStateView, IMainView
    Private ReadOnly windowStatePresenter As MainWindowStatePresenter
    Private ReadOnly toolStripStatePresenter As MainToolStripStatePresenter
    Private ReadOnly commonPresenter As CommonPresenter
    Private ReadOnly presenter As MainPresenter
    Private ReadOnly help As IHelpDisplayService = New HelpDisplayService
    Private m_DocumentRecordNotesChanged As String
    ' Message that is sent when the contents of the clipboard have changed.
    Private Const WM_CLIPBOARDUPDATE As Integer = &H31D

    Public Sub New()
        InitializeComponent()
        windowStatePresenter = New MainWindowStatePresenter(Me)
        toolStripStatePresenter = New MainToolStripStatePresenter(Me)
        commonPresenter = New CommonPresenter(Me)
        presenter = New MainPresenter(Me)
        HelpProvider.HelpNamespace = help.Name
        presenter.ApplyPolicy()
        WindowsApplication.ProductUpdate.Start() ' Also called on a timer every 30 minutes.
    End Sub

#Region "Interface Members (IMainWindowStateView)"
    Public Property ViewLocation As Point Implements IMainWindowStateView.ViewLocation
        Get
            Return Me.Location
        End Get
        Set(value As Point)
            Me.Location = value
        End Set
    End Property

    Public Property ViewSize As Size Implements IMainWindowStateView.ViewSize
        Get
            Return Me.Size
        End Get
        Set(value As Size)
            Me.Size = value
        End Set
    End Property

    Public ReadOnly Property ViewRestoreBoundsSize As Size Implements IMainWindowStateView.ViewRestoreBoundsSize
        Get
            Return Me.RestoreBounds.Size
        End Get
    End Property

    Public Property FormWindowState As FormWindowState Implements IMainWindowStateView.ViewWindowState
        Get
            Return Me.WindowState
        End Get
        Set(value As FormWindowState)
            Me.WindowState = value
        End Set
    End Property

    Public Property ViewSplitterDistance As Integer Implements IMainWindowStateView.ViewSplitterDistance
        Get
            Return SplitContainer.SplitterDistance
        End Get
        Set(value As Integer)
            SplitContainer.SplitterDistance = value
        End Set
    End Property
#End Region

#Region "Interface Members (IToolStripStateView)"
    Public Sub SetToolStripItemsState(itemShortName As String, enabled As Boolean) Implements IToolStripStateView.SetToolStripItemsState
        Dim menuStripResults = MenuStrip.Items.Find(itemShortName & "ToolStripMenuItem", True).ToList
        Dim toolBarResults = ToolStrip.Items.Find(itemShortName & "ToolStripButton", True).ToList
        For Each item As ToolStripItem In menuStripResults
            item.Enabled = enabled
        Next
        For Each item As ToolStripItem In toolBarResults
            item.Enabled = enabled
        Next
    End Sub
#End Region

#Region "Interface Members (IMainView)"
    Public ReadOnly Property SearchFunctionsEnabled As Boolean Implements IMainView.SearchFunctionsEnabled
        Get
            Return SearchFunctionsListBox.Enabled
        End Get
    End Property

    Public ReadOnly Property SelectedSearchFunction As Integer Implements IMainView.SelectedSearchFunction
        Get
            Return SearchFunctionsListBox.SelectedIndex
        End Get
    End Property

    Public Property SearchTextControlEnabled As Boolean Implements IMainView.SearchTextControlEnabled
        Get
            Return SearchTextComboBox.Enabled
        End Get
        Set(value As Boolean)
            SearchTextComboBox.Enabled = value
        End Set
    End Property

    Public Property SearchTextHistory As Object Implements IMainView.SearchTextHistory
        Get
            Return SearchTextComboBox.Items
        End Get
        Set(value As Object)
            SearchTextComboBox.Items.Clear()  ' Need to clear twice to work around duplicates
            SearchTextComboBox.Items.Clear()  ' from being displayed in the drop down.
            SearchTextComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property SearchText As String Implements IMainView.SearchText
        Get
            Return SearchTextComboBox.Text
        End Get
        Set(value As String)
            SearchTextComboBox.Text = value
        End Set
    End Property

    Public Property SearchTextErrorProviderMessage As String Implements IMainView.SearchTextErrorProviderMessage
        Get
            Return SearchTextErrorProvider.GetError(SearchTextComboBox)
        End Get
        Set(value As String)
            If value Is Nothing Then
                SearchTextErrorProvider.Clear()
            Else
                SearchTextErrorProvider.SetError(SearchTextComboBox, value)
            End If
        End Set
    End Property

    Public Property SearchEnabled As Boolean Implements IMainView.SearchEnabled
        Get
            Return SearchByTextButton.Enabled
        End Get
        Set(value As Boolean)
            SearchByTextButton.Enabled = value
        End Set
    End Property

    Public Property AuthorEnabled As Boolean Implements IMainView.AuthorEnabled
        Get
            Return AuthorGroupComboBox.Enabled
        End Get
        Set(value As Boolean)
            AuthorGroupComboBox.Enabled = value
        End Set
    End Property

    Public Property AuthorsGroup As DataTable Implements ICommonView.AuthorsGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            AuthorGroupComboBox.DataSource = value
            AuthorGroupComboBox.DisplayMember = "doc_author"
        End Set
    End Property

    Public Property AuthorGroup As String Implements ICommonView.AuthorGroup
        Get
            If Not AuthorGroupComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(AuthorGroupComboBox.SelectedItem("doc_author"),
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            AuthorGroupComboBox.SelectedItem = value
        End Set
    End Property

    Public Property SubjectEnabled As Boolean Implements IMainView.SubjectEnabled
        Get
            Return SubjectGroupComboBox.Enabled
        End Get
        Set(value As Boolean)
            SubjectGroupComboBox.Enabled = value
        End Set
    End Property

    Public Property SubjectsGroup As DataTable Implements ICommonView.SubjectsGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            SubjectGroupComboBox.DataSource = value
            SubjectGroupComboBox.DisplayMember = "doc_subject"
        End Set
    End Property

    Public Property SubjectGroup As String Implements ICommonView.SubjectGroup
        Get
            If Not SubjectGroupComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(SubjectGroupComboBox.SelectedItem("doc_subject"),
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            SubjectGroupComboBox.SelectedItem = value
        End Set
    End Property

    Public Property CategoryEnabled As Boolean Implements IMainView.CategoryEnabled
        Get
            Return CategoryGroupComboBox.Enabled
        End Get
        Set(value As Boolean)
            CategoryGroupComboBox.Enabled = value
        End Set
    End Property

    Public Property CategoriesGroup As DataTable Implements ICommonView.CategoriesGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            CategoryGroupComboBox.DataSource = value
            CategoryGroupComboBox.DisplayMember = "doc_category"
        End Set
    End Property

    Public Property CategoryGroup As String Implements ICommonView.CategoryGroup
        Get
            If Not CategoryGroupComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(CategoryGroupComboBox.SelectedItem("doc_category"),
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            CategoryGroupComboBox.SelectedItem = value
        End Set
    End Property

    Public Property ClearSelectionsEnabled As Boolean Implements IMainView.ClearSelectionsEnabled
        Get
            Return ClearSelectionsButton.Enabled
        End Get
        Set(value As Boolean)
            ClearSelectionsButton.Enabled = value
        End Set
    End Property

    Public Property SearchBySelectionsEnabled As Boolean Implements IMainView.SearchBySelectionsEnabled
        Get
            Return SearchBySelectionsButton.Enabled
        End Get
        Set(value As Boolean)
            SearchBySelectionsButton.Enabled = value
        End Set
    End Property

    Public Property SearchDatePickerEnabled As Boolean Implements IMainView.SearchDatePickerEnabled
        Get
            Return SearchDateTimePicker.Enabled
        End Get
        Set(value As Boolean)
            SearchDateTimePicker.Enabled = value
        End Set
    End Property

    Public ReadOnly Property SearchDate As String Implements IMainView.SearchDate
        Get
            Return SearchDateTimePicker.Value.ToString("yyyy-MM-dd",
                                                       CultureInfo.CurrentCulture)
        End Get
    End Property

    Public Property SearchResultsEnabled As Boolean Implements IMainView.SearchResultsEnabled
        Get
            Return SearchResultsDataGridView.Enabled
        End Get
        Set(value As Boolean)
            SearchResultsDataGridView.Enabled = value
        End Set
    End Property

    Public Property SearchResults As DataTable Implements IMainView.SearchResults
        Get
            Return SearchResultsDataGridView.DataSource
        End Get
        Set(value As DataTable)
            SearchResultsDataGridView.DataSource = value
        End Set
    End Property

    Public ReadOnly Property SearchResultsSortedColumn As DataGridViewColumn Implements IMainView.SearchResultsSortedColumn
        Get
            Return SearchResultsDataGridView.SortedColumn
        End Get
    End Property

    Public ReadOnly Property SearchResultsSortOrder As SortOrder Implements IMainView.SearchResultsSortOrder
        Get
            Return SearchResultsDataGridView.SortOrder
        End Get
    End Property

    Public ReadOnly Property SearchResultsRowCount As Integer Implements IMainView.SearchResultsRowCount
        Get
            Return SearchResultsDataGridView.RowCount
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultsIds As Object Implements IMainView.SelectedSearchResultsIds
        Get
            Return GetSelectedSearchResultsIds.ToArray(True)
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultsIdsCount As Integer Implements IMainView.SelectedSearchResultsIdsCount
        Get
            Return GetSelectedSearchResultsIds.Count
        End Get
    End Property

    Public ReadOnly Property DocumentRecordId As Integer Implements IMainView.DocumentRecordId
        Get
            ' Filter out null selections that occur when the DataGridView is filled.
            If SearchResultsDataGridView.SelectedRows.Count > 0 Then
                Return SearchResultsDataGridView.SelectedRows(0).Cells(1).Value
            End If
            Return Nothing  ' Will return 0 for a null selection.
        End Get
    End Property

    Public Property DocumentRecordFlagState As Integer Implements IMainView.DocumentRecordFlagState
        Get
            If EditFlagDocumentToolStripMenuItem.Checked Then
                Return 1
            Else
                Return 0
            End If
        End Get
        Set(value As Integer)
            If value = 1 Then
                EditFlagDocumentToolStripMenuItem.Checked = True
            Else
                EditFlagDocumentToolStripMenuItem.Checked = False
            End If
        End Set
    End Property

    Public Property DocumentRecordPanelEnabled As Boolean Implements IMainView.DocumentRecordPanelEnabled
        Get
            Return SelectedDocumentTabControl.Enabled
        End Get
        Set(value As Boolean)
            toolStripStatePresenter.SetDocumentSelectedState(value)
            SelectedDocumentTabControl.Enabled = value
        End Set
    End Property

    Public Property DocumentRecordPanelSelectedTab As Integer Implements IMainView.DocumentRecordPanelSelectedTab
        Get
            Return SelectedDocumentTabControl.SelectedIndex
        End Get
        Set(value As Integer)
            SelectedDocumentTabControl.SelectedIndex = value
        End Set
    End Property

    Public ReadOnly Property TextElementSelectedText As String Implements IMainView.TextElementSelectedText
        Get
            Dim text As String = Nothing
            If NotesTextBox.Focused Then
                If NotesTextBox.Text.Length > 0 Then
                    text = NotesTextBox.Text
                End If
            ElseIf TextTextBox.Focused Then
                If TextTextBox.Text.Length > 0 Then
                    text = TextTextBox.Text
                End If
            End If
            Return text
        End Get
    End Property

    Public Property DocumentRecordNotes As String Implements IMainView.DocumentRecordNotes
        Get
            Return NotesTextBox.Text
        End Get
        Set(value As String)
            NotesTextBox.Text = value
        End Set
    End Property

    Public Property DocumentRecordNotesChanged As Boolean Implements IMainView.DocumentRecordNotesChanged
        Get
            Return m_DocumentRecordNotesChanged
        End Get
        Set(value As Boolean)
            m_DocumentRecordNotesChanged = value
            Dim controlEnabled As Boolean = False
            If m_DocumentRecordNotesChanged = False Then
                controlEnabled = True
                toolStripStatePresenter.SetSearchResultsSelectedState(GetSelectedSearchResultsIds.Count)
            End If
            ' The "If" check is needed to prevent user from having to check/uncheck checkbox in
            ' SearchResultsDataGridView when Document Notes length > 0.
            If NotesTextBox.Focused Then
                SearchGroupBox.Enabled = controlEnabled
                SearchResultsDataGridView.Enabled = controlEnabled
            End If
            If DocumentRecordId > 0 Then
                toolStripStatePresenter.SetNotesTextBoxChangedState(m_DocumentRecordNotesChanged,
                                                                    NotesTextBox.CanUndo)
            End If
        End Set
    End Property

    Public Property DocumentRecordKeywords As String Implements IMainView.DocumentRecordKeywords
        Get
            Return KeywordsTextBox.Text
        End Get
        Set(value As String)
            KeywordsTextBox.Text = value
        End Set
    End Property

    Public Property DocumentPreview As Image Implements IMainView.DocumentPreview
        Get
            Return PreviewPictureBox.Image
        End Get
        Set(value As Image)
            ' Dispose the PictureBox first to release memory before loading image.
            If Not PreviewPictureBox.Image Is Nothing Then
                PreviewPictureBox.Image.Dispose()
            End If
            Dim imageLock As New Object
            SyncLock imageLock  ' Required to prevent a rare InvalidOperationException.
                PreviewPictureBox.Image = value
            End SyncLock
        End Set
    End Property

    Public Property DocumentText As String Implements IMainView.DocumentText
        Get
            Return TextTextBox.Text
        End Get
        Set(value As String)
            TextTextBox.Text = value
            If TextTextBox.Text.Length > 0 Then
                TextTextBox.Text = TextTextBox.Text.Trim
            End If
        End Set
    End Property

    Public Property DeleteExportProgressVisible As Boolean Implements IMainView.DeleteExportProgressVisible
        Get
            Return DeleteExportToolStripProgressBar.Visible
        End Get
        Set(value As Boolean)
            DeleteExportToolStripProgressBar.Visible = value    ' Must be performed first.
            If value Then
                DeleteExportToolStripProgressBar.Value = 0
            End If
        End Set
    End Property

    Public Property DeleteExportProgressMaximum As Integer Implements IMainView.DeleteExportProgressMaximum
        Get
            Return DeleteExportToolStripProgressBar.Maximum
        End Get
        Set(value As Integer)
            DeleteExportToolStripProgressBar.Maximum = value
        End Set
    End Property

    Public Property UploadRunningVisible As Boolean Implements IMainView.UploadRunningVisible
        Get
            Return UploadRunningToolStripStatusLabel.Visible
        End Get
        Set(value As Boolean)
            UploadRunningToolStripStatusLabel.Visible = value
        End Set
    End Property

    Public Property UploadFolderErrorVisible As Boolean Implements IMainView.UploadFolderErrorVisible
        Get
            Return UploadFolderErrorToolStripStatusLabel.Visible
        End Get
        Set(value As Boolean)
            UploadFolderErrorToolStripStatusLabel.Visible = value
        End Set
    End Property

    Public Property UploadStagingFolderErrorVisible As Boolean Implements IMainView.UploadStagingFolderErrorVisible
        Get
            Return UploadStagingFolderErrorToolStripStatusLabel.Visible
        End Get
        Set(value As Boolean)
            UploadStagingFolderErrorToolStripStatusLabel.Visible = value
        End Set
    End Property

    Public Property FlaggedDocumentsExistVisible As Boolean Implements IMainView.FlaggedDocumentsExistVisible
        Get
            Return FlaggedDocumentsExistToolStripStatusLabel.Visible
        End Get
        Set(value As Boolean)
            FlaggedDocumentsExistToolStripStatusLabel.Visible = value
        End Set
    End Property

    Public Property FlaggedDocumentsCheckTimerEnabled As Boolean Implements IMainView.FlaggedDocumentsCheckTimerEnabled
        Get
            Return FlaggedDocumentsCheckTimer.Enabled
        End Get
        Set(value As Boolean)
            FlaggedDocumentsCheckTimer.Enabled = value
        End Set
    End Property

    Public ReadOnly Property ActiveElement As String Implements ICommonView.ActiveElement
        Get
            Return SplitContainer.ActiveControl.Name
        End Get
    End Property

    Public Property AuthorsPaired As DataTable Implements ICommonView.AuthorsPaired
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property AuthorPaired As String Implements ICommonView.AuthorPaired
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property SubjectsPaired As DataTable Implements ICommonView.SubjectsPaired
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property SubjectPaired As String Implements ICommonView.SubjectPaired
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property Categories As DataTable Implements ICommonView.Categories
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property Category As String Implements ICommonView.Category
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public Sub RemoveAllDocumentsFromSearchFunctions() Implements IMainView.RemoveAllDocumentsFromSearchFunctions
        SearchFunctionsListBox.Items.RemoveAt(4)
    End Sub

    Public Sub SelectSearchResultsLastRow() Implements IMainView.SelectSearchResultsLastRow
        SearchResultsDataGridView.Rows(SearchResultsDataGridView.Rows.Count - 1).Selected = True
        SearchResultsDataGridView.FirstDisplayedScrollingRowIndex = SearchResultsDataGridView.RowCount - 1
    End Sub

    Public Sub SelectSearchResultRowById(id As Integer) Implements IMainView.SelectSearchResultRowById
        For Each row As DataGridViewRow In SearchResultsDataGridView.Rows
            If row.Cells(1).Value = id Then
                row.Selected = True
                SearchResultsDataGridView.FirstDisplayedScrollingRowIndex = row.Index
            End If
        Next
    End Sub

    Public Sub SelectDeselectAllSearchResults(selectionState As SelectionState) Implements IMainView.SelectDeselectAllSearchResults
        For Each row As DataGridViewRow In SearchResultsDataGridView.Rows
            If selectionState = SelectionState.SelectAll Then
                row.Cells(0).Value = True
            ElseIf selectionState = SelectionState.DeselectAll Then
                row.Cells(0).Value = False
            End If
        Next
        SearchResultsDataGridView.RefreshEdit()
    End Sub

    Public Sub SortSearchResults(sortColumnIndex As Integer, sortDirection As ListSortDirection) Implements IMainView.SortSearchResults
        SearchResultsDataGridView.Sort(SearchResultsDataGridView.Columns(sortColumnIndex), sortDirection)
    End Sub

    Public Sub RefreshSearchResults() Implements IMainView.RefreshSearchResults
        ViewRefreshToolStrip_Click(Me, Nothing)
    End Sub

    Public Sub ResetSearchResultsHeader() Implements IMainView.ResetSearchResultsHeader
        SearchResultsDataGridView.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Public Sub ScrollToEndInNotesElement() Implements IMainView.ScrollToEndInNotesElement
        NotesTextBox.Select()
        NotesTextBox.Select(NotesTextBox.Text.Length, 0)
        NotesTextBox.ScrollToCaret()
    End Sub

    Public Sub DeleteExportProgressPerformStep() Implements IMainView.DeleteExportProgressPerformStep
        DeleteExportToolStripProgressBar.PerformStep()
    End Sub

    Public Sub SetCursor(wait As Boolean) Implements ICommonView.SetCursor
        If wait Then
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        windowStatePresenter.GetState()
        NativeMethods.AddClipboardFormatListener(Me.Handle)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = WM_CLIPBOARDUPDATE Then
            If My.Computer.Clipboard.ContainsText Then
                If NotesTextBox.Focused Then
                    toolStripStatePresenter.SetPasteState(True)
                End If
            End If
        End If
    End Sub

#Region "ToolStrip Events"
    Private Sub ToolStrip_VisibleChanged(sender As Object, e As EventArgs) Handles ToolStrip.VisibleChanged
        ViewToolbarToolStripMenuItem.Checked = ToolStrip.Visible
    End Sub

    Private Sub FileNewToolStrip_Click(sender As Object, e As EventArgs) Handles FileNewToolStripMenuItem.Click,
                                                                                 FileNewToolStripButton.Click
        AddPdfDocumentsDialog.ShowDialog()
    End Sub

    Private Sub FileOpenToolStrip_Click(sender As Object, e As EventArgs) Handles FileOpenToolStripMenuItem.Click,
                                                                                  FileOpenToolStripButton.Click
        presenter.OpenSelectedDocumentPdf()
    End Sub

    Private Sub FileSaveToolStrip_Click(sender As Object, e As EventArgs) Handles FileSaveToolStripMenuItem.Click,
                                                                                  FileSaveToolStripButton.Click
        presenter.SaveSelectedDocumentNotes()
        presenter.NotesTextChanged()
        toolStripStatePresenter.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly,
                                                             NotesTextBox.TextLength,
                                                             NotesTextBox.SelectionLength)
    End Sub

    Private Sub FileSaveAsToolStrip_Click(sender As Object, e As EventArgs) Handles FileSaveAsToolStripMenuItem.Click
        presenter.SaveSelectedDocumentPdfOrTextAs()
    End Sub

    Private Sub FilePrintToolStrip_Click(sender As Object, e As EventArgs) Handles FilePrintToolStripMenuItem.Click,
                                                                                   FilePrintToolStripButton.Click
        presenter.PrintTextForSelectedDocument()
    End Sub

    Private Sub FilePrintPreviewToolStrip_Click(sender As Object, e As EventArgs) Handles FilePrintPreviewToolStripMenuItem.Click
        presenter.PrintPreviewTextForSelectedDocument()
    End Sub

    Private Sub FileSelectAllToolStrip_Click(sender As Object, e As EventArgs) Handles FileSelectAllToolStripMenuItem.Click
        presenter.SelectAllSearchResults()
    End Sub

    Private Sub FileSelectNoneToolStrip_Click(sender As Object, e As EventArgs) Handles FileSelectNoneToolStripMenuItem.Click
        presenter.DeselectAllSearchResults()
    End Sub

    Private Sub FileSetCategoryToolStrip_Click(sender As Object, e As EventArgs) Handles FileSetCategoryToolStripMenuItem.Click
        presenter.SetCategoryOnSelectedSearchResults()
    End Sub

    Private Sub FileDeleteToolStrip_Click(sender As Object, e As EventArgs) Handles FileDeleteToolStripMenuItem.Click,
                                                                                    FileDeleteToolStripButton.Click
        presenter.DeleteSelectedSearchResults()
    End Sub

    Private Sub FileExportToolStrip_Click(sender As Object, e As EventArgs) Handles FileExportToolStripMenuItem.Click
        presenter.ExportSelectedSearchResults()
    End Sub

    Private Sub FileExitToolStrip_Click(sender As Object, e As EventArgs) Handles FileExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub EditUndoToolStrip_Click(sender As Object, e As EventArgs) Handles EditUndoToolStripMenuItem.Click,
                                                                                  EditUndoToolStripButton.Click
        NotesTextBox.Undo()
    End Sub

    Private Sub EditCutToolStrip_Click(sender As Object, e As EventArgs) Handles EditCutToolStripMenuItem.Click,
                                                                                 EditCutToolStripButton.Click
        NotesTextBox.Cut()
    End Sub

    Private Sub EditCopyToolStrip_Click(sender As Object, e As EventArgs) Handles EditCopyToolStripMenuItem.Click,
                                                                                  EditCopyToolStripButton.Click
        GetTextBoxWithInputFocus.Copy()
    End Sub

    Private Sub EditPasteToolStrip_Click(sender As Object, e As EventArgs) Handles EditPasteToolStripMenuItem.Click,
                                                                                   EditPasteToolStripButton.Click
        NotesTextBox.Paste()
    End Sub

    Private Sub EditSelectAllToolStrip_Click(sender As Object, e As EventArgs) Handles EditSelectAllToolStripMenuItem.Click
        GetTextBoxWithInputFocus.SelectAll()
        toolStripStatePresenter.SetTextBoxTextSelectionState(GetTextBoxWithInputFocus.ReadOnly,
                                                             GetTextBoxWithInputFocus.TextLength,
                                                             GetTextBoxWithInputFocus.SelectionLength)
    End Sub

    Private Sub EditRestoreToolStrip_Click(sender As Object, e As EventArgs) Handles EditRestoreToolStripMenuItem.Click,
                                                                                     EditRestoreToolStripButton.Click
        presenter.RestoreSelectedDocumentNotes()
        presenter.NotesTextChanged()
    End Sub

    Private Sub EditDateTimeToolStrip_Click(sender As Object, e As EventArgs) Handles EditDateTimeToolStripMenuItem.Click,
                                                                                      EditDateTimeToolStripButton.Click
        presenter.AppendDateTimeUserNameToSelectedDocumentNotes()
    End Sub

    Private Sub EditFlagDocumentToolStrip_Click(sender As Object, e As EventArgs) Handles EditFlagDocumentToolStripMenuItem.Click
        presenter.SetFlagStateOnSelectedDocument()
    End Sub

    Private Sub ViewRefreshToolStrip_Click(sender As Object, e As EventArgs) Handles ViewRefreshToolStripMenuItem.Click,
                                                                                     ViewRefreshToolStripButton.Click
        presenter.RefreshSearchResults()
    End Sub

    Private Sub ViewSetPreviewImageResolutionToolStrip_Click(sender As Object, e As EventArgs) Handles ViewSetPreviewImageResolutionToolStripMenuItem.Click
        PreviewImageResolutionDialog.ShowDialog()
        presenter.ReloadDocumentPreview()
    End Sub

    Private Sub ViewToolBarToolStrip_Click(sender As Object, e As EventArgs) Handles ViewToolbarToolStripMenuItem.Click
        If ViewToolbarToolStripMenuItem.Checked Then
            ViewToolbarToolStripMenuItem.Checked = False
        Else
            ViewToolbarToolStripMenuItem.Checked = True
        End If
        ToolStrip.Visible = ViewToolbarToolStripMenuItem.Checked
    End Sub

    Private Sub ViewStatusBarToolStrip_Click(sender As Object, e As EventArgs) Handles ViewStatusBarToolStripMenuItem.Click
        If ViewStatusBarToolStripMenuItem.Checked Then
            ViewStatusBarToolStripMenuItem.Checked = False
        Else
            ViewStatusBarToolStripMenuItem.Checked = True
        End If
        StatusStrip.Visible = ViewStatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub InsertTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertTextToolStripMenuItem.Click,
                                                                                            InsertTextToolStripButton.Click
        presenter.InsertTextFromFileIntoSelectedDocumentNotes()
    End Sub

    Private Sub ToolsOptionsToolStripButton_Click(sender As Object, e As EventArgs) Handles ToolsOptionsToolStripButton.Click,
                                                                                            ToolsOptionsToolStripMenuItem.Click
        OptionsDialog.ShowDialog()
    End Sub

    Private Sub ToolsManageUploadFoldersToolStrip_Click(sender As Object, e As EventArgs) Handles ToolsManageUploadFolderConfigurationsToolStripMenuItem.Click,
                                                                                                  ToolsManageUploadFolderConfigurationsToolStripButton.Click
        ManageUploadFolderConfigurationsDialog.ShowDialog()
    End Sub

    Private Sub HelpContentsToolStrip_Click(sender As Object, e As EventArgs) Handles HelpContentsToolStripMenuItem.Click,
                                                                                      HelpContentsToolStripButton.Click
        help.Show(Me, "Using PDFKeeper.html")
    End Sub

    Private Sub HelpAboutToolStrip_Click(sender As Object, e As EventArgs) Handles HelpAboutToolStripMenuItem.Click
        AboutBox.ShowDialog()
    End Sub
#End Region

#Region "Search Functions Events and Members"
    Private Sub SearchFunctionsListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SearchFunctionsListBox.SelectedIndexChanged
        toolStripStatePresenter.SetPreSearchState()
        presenter.SearchFunctionSelected()
    End Sub

    Private Sub SearchStringComboBox_Enter(sender As Object, e As EventArgs) Handles SearchTextComboBox.Enter
        presenter.GetSearchStringHistory()
    End Sub

    Private Sub SearchStringComboBox_TextChanged(sender As Object, e As EventArgs) Handles SearchTextComboBox.TextChanged
        toolStripStatePresenter.SetPreSearchState()
        presenter.SearchTextChanged()
    End Sub

    Private Sub SearchByTextButton_Click(sender As Object, e As EventArgs) Handles SearchByTextButton.Click
        presenter.GetSearchResultsByString(False)
    End Sub

    Private Sub AuthorGroupComboBox_DropDown(sender As Object, e As EventArgs) Handles AuthorGroupComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub AuthorGroupComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles AuthorGroupComboBox.DropDownClosed
        toolStripStatePresenter.SetPreSearchState()
        presenter.SetSearchBySelectionsButtonsState()
    End Sub

    Private Sub AuthorGroupComboBox_Enter(sender As Object, e As EventArgs) Handles AuthorGroupComboBox.Enter
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub AuthorComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles AuthorGroupComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            AuthorGroupComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub AuthorGroupComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles AuthorGroupComboBox.KeyUp
        toolStripStatePresenter.SetPreSearchState()
        presenter.SetSearchBySelectionsButtonsState()
    End Sub

    Private Sub AuthorGroupComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles AuthorGroupComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not AuthorGroupComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub SubjectGroupComboBox_DropDown(sender As Object, e As EventArgs) Handles SubjectGroupComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub SubjectGroupComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles SubjectGroupComboBox.DropDownClosed
        toolStripStatePresenter.SetPreSearchState()
        presenter.SetSearchBySelectionsButtonsState()
    End Sub

    Private Sub SubjectGroupComboBox_Enter(sender As Object, e As EventArgs) Handles SubjectGroupComboBox.Enter
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub SubjectGroupComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SubjectGroupComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            SubjectGroupComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub SubjectGroupComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles SubjectGroupComboBox.KeyUp
        toolStripStatePresenter.SetPreSearchState()
        presenter.SetSearchBySelectionsButtonsState()
    End Sub

    Private Sub SubjectGroupComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles SubjectGroupComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not SubjectGroupComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub CategoryGroupComboBox_DropDown(sender As Object, e As EventArgs) Handles CategoryGroupComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub CategoryGroupComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles CategoryGroupComboBox.DropDownClosed
        toolStripStatePresenter.SetPreSearchState()
        presenter.SetSearchBySelectionsButtonsState()
    End Sub

    Private Sub CategoryGroupComboBox_Enter(sender As Object, e As EventArgs) Handles CategoryGroupComboBox.Enter
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub CategoryGroupComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CategoryGroupComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            CategoryGroupComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub CategoryGroupComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles CategoryGroupComboBox.KeyUp
        toolStripStatePresenter.SetPreSearchState()
        presenter.SetSearchBySelectionsButtonsState()
    End Sub

    Private Sub CategoryGroupComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles CategoryGroupComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not CategoryGroupComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub ClearSelectionsButton_Click(sender As Object, e As EventArgs) Handles ClearSelectionsButton.Click
        toolStripStatePresenter.SetPreSearchState()
        presenter.ClearSearchSelections()
        presenter.ClearSearchSelections()
    End Sub

    Private Sub SearchBySelectionsButton_Click(sender As Object, e As EventArgs) Handles SearchBySelectionsButton.Click
        presenter.GetSearchResultsBySearchSelection()
    End Sub

    Private Sub SearchDateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles SearchDateTimePicker.ValueChanged
        presenter.GetDocumentRecordsByDateAdded()
    End Sub
#End Region

#Region "Search Results Events and Members"
    Private Sub SearchResultsDataGridView_DataSourceChanged(sender As Object, e As EventArgs) Handles SearchResultsDataGridView.DataSourceChanged
        With SearchResultsDataGridView
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
            .Columns(6).HeaderCell.Value = My.Resources.Added
            .Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(6).ReadOnly = True
        End With
        If SearchResultsDataGridView.RowCount > 0 Then
            SearchResultsDataGridView.Enabled = True
            SearchResultsDataGridView.Columns(6).AutoSizeMode =
                DataGridViewAutoSizeColumnMode.DisplayedCells
            If SearchResultsDataGridView.Columns(6).Displayed = True Then
                SearchResultsDataGridView.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            SearchResultsDataGridView.Columns(6).MinimumWidth = SearchResultsDataGridView.Columns(6).FillWeight + 20
        End If
        toolStripStatePresenter.SetPostSearchState()
    End Sub

    Private Sub SearchResultsDataGridView_Sorted(sender As Object, e As EventArgs) Handles SearchResultsDataGridView.Sorted
        presenter.SortSearchResults()
    End Sub

    Private Sub SearchResultsDataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles SearchResultsDataGridView.RowsAdded
        TotalRecordsCountToolStripStatusLabel.Text = SearchResultsDataGridView.RowCount
        toolStripStatePresenter.SetSearchResultsRowCountChangedState(SearchResultsDataGridView.RowCount)
    End Sub

    Private Sub SearchResultsDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles SearchResultsDataGridView.RowsRemoved
        TotalRecordsCountToolStripStatusLabel.Text = SearchResultsDataGridView.RowCount
        toolStripStatePresenter.SetSearchResultsRowCountChangedState(SearchResultsDataGridView.RowCount)
        toolStripStatePresenter.SetSearchResultsSelectedState(GetSelectedSearchResultsIds.Count)
    End Sub

    Private Sub SearchResultsDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles SearchResultsDataGridView.SelectionChanged
        presenter.SelectSearchResult()
    End Sub

    Private Sub SearchResultsDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles SearchResultsDataGridView.CurrentCellDirtyStateChanged
        If SearchResultsDataGridView.IsCurrentCellDirty Then
            SearchResultsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub SearchResultsDataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles SearchResultsDataGridView.CellValueChanged
        toolStripStatePresenter.SetSearchResultsSelectedState(GetSelectedSearchResultsIds.Count)
    End Sub

    Private Sub SearchResultsDataGridView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles SearchResultsDataGridView.CellDoubleClick
        presenter.OpenSelectedDocumentPdf()
    End Sub

    Private Function GetSelectedSearchResultsIds() As GenericList(Of String)
        Dim results As New GenericList(Of String)
        For Each row As DataGridViewRow In SearchResultsDataGridView.Rows
            If row.Cells(0).Value = True Then
                results.Add(row.Cells(1).Value)
            End If
        Next
        Return results
    End Function
#End Region

#Region "Selected Document Events and Members"
    Private Sub NotesTextBox_Enter(sender As Object, e As EventArgs) Handles NotesTextBox.Enter,
                                                                             NotesTextBox.GotFocus
        toolStripStatePresenter.SetTextBoxEnterState(NotesTextBox.ReadOnly, NotesTextBox.TextLength)
        If NotesTextBox.TextLength > 0 Then
            toolStripStatePresenter.SetTextBoxPrintableState(True)
        End If
        If My.Computer.Clipboard.ContainsText Then
            toolStripStatePresenter.SetPasteState(True)
        End If
        toolStripStatePresenter.SetNotesTextBoxChangedState(m_DocumentRecordNotesChanged,
                                                            NotesTextBox.CanUndo)
    End Sub

    Private Sub NotesTextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles NotesTextBox.MouseUp
        toolStripStatePresenter.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly,
                                                             NotesTextBox.TextLength,
                                                             NotesTextBox.SelectionLength)
    End Sub

    Private Sub NotesTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NotesTextBox.KeyPress
        toolStripStatePresenter.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly,
                                                             NotesTextBox.TextLength,
                                                             NotesTextBox.SelectionLength)
    End Sub

    Private Sub NotesTextBox_TextChanged(sender As Object, e As EventArgs) Handles NotesTextBox.TextChanged
        presenter.NotesTextChanged()
        toolStripStatePresenter.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly,
                                                             NotesTextBox.TextLength,
                                                             NotesTextBox.SelectionLength)
    End Sub

    Private Sub NotesTextBox_Leave(sender As Object, e As EventArgs) Handles NotesTextBox.Leave
        toolStripStatePresenter.SetTextBoxLeaveState()
        toolStripStatePresenter.SetTextBoxPrintableState(False)
    End Sub

    Private Sub KeywordsTextBox_Enter(sender As Object, e As EventArgs) Handles KeywordsTextBox.Enter
        toolStripStatePresenter.SetTextBoxEnterState(KeywordsTextBox.ReadOnly,
                                                     KeywordsTextBox.TextLength)
        toolStripStatePresenter.SetTextBoxPrintableState(False)
    End Sub

    Private Sub KeywordsTextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles KeywordsTextBox.MouseUp
        toolStripStatePresenter.SetTextBoxTextSelectionState(KeywordsTextBox.ReadOnly,
                                                             KeywordsTextBox.TextLength,
                                                             KeywordsTextBox.SelectionLength)
    End Sub

    Private Sub KeywordsTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KeywordsTextBox.KeyPress
        toolStripStatePresenter.SetTextBoxTextSelectionState(KeywordsTextBox.ReadOnly,
                                                             KeywordsTextBox.TextLength,
                                                             KeywordsTextBox.SelectionLength)
    End Sub

    Private Sub KeywordsTextBox_Leave(sender As Object, e As EventArgs) Handles KeywordsTextBox.Leave
        toolStripStatePresenter.SetTextBoxLeaveState()
    End Sub

    Private Sub PreviewPictureBox_DoubleClick(sender As Object, e As EventArgs) Handles PreviewPictureBox.DoubleClick
        PreviewImageResolutionDialog.ShowDialog()
        presenter.ReloadDocumentPreview()
    End Sub

    Private Sub TextTextBox_Enter(sender As Object, e As EventArgs) Handles TextTextBox.Enter
        toolStripStatePresenter.SetTextBoxEnterState(TextTextBox.ReadOnly, TextTextBox.TextLength)
        If TextTextBox.TextLength > 0 Then
            toolStripStatePresenter.SetTextBoxPrintableState(True)
        End If
    End Sub

    Private Sub TextTextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles TextTextBox.MouseUp
        toolStripStatePresenter.SetTextBoxTextSelectionState(TextTextBox.ReadOnly,
                                                             TextTextBox.TextLength,
                                                             TextTextBox.SelectionLength)
    End Sub

    Private Sub TextTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextTextBox.KeyPress
        toolStripStatePresenter.SetTextBoxTextSelectionState(TextTextBox.ReadOnly,
                                                             TextTextBox.TextLength,
                                                             TextTextBox.SelectionLength)
    End Sub

    Private Sub TextTextBox_Leave(sender As Object, e As EventArgs) Handles TextTextBox.Leave
        toolStripStatePresenter.SetTextBoxLeaveState()
        toolStripStatePresenter.SetTextBoxPrintableState(False)
    End Sub

    Private Function GetTextBoxWithInputFocus() As TextBox
        Dim textBox As TextBox = Nothing
        If NotesTextBox.Focused Then
            textBox = NotesTextBox
        ElseIf KeywordsTextBox.Focused Then
            textBox = KeywordsTextBox
        ElseIf TextTextBox.Focused Then
            textBox = TextTextBox
        End If
        Return textBox
    End Function
#End Region

#Region "Timer Events"
    Private Sub UploadTimer_Tick(sender As Object, e As EventArgs) Handles UploadTimer.Tick
        presenter.UploadAsync()
    End Sub

    Private Sub AutoUpdateCheckTimer_Tick(sender As Object, e As EventArgs) Handles AutoUpdateCheckTimer.Tick
        ProductUpdate.Start()
    End Sub

    Private Sub FlaggedDocumentsCheckTimer_Tick(sender As Object, e As EventArgs) Handles FlaggedDocumentsCheckTimer.Tick
        presenter.FlaggedDocumentsCheck()
    End Sub
#End Region

#Region "StatusStrip Events"
    Private Sub StatusStrip_VisibleChanged(sender As Object, e As EventArgs) Handles StatusStrip.VisibleChanged
        ViewStatusBarToolStripMenuItem.Checked = StatusStrip.Visible
    End Sub

    Private Sub UploadFolderErrorToolStripStatusLabel_Click(sender As Object, e As EventArgs) Handles UploadFolderErrorToolStripStatusLabel.Click
        Dim dirInfo As New DirectoryInfo(UserProfile.UploadPath)
        dirInfo.Explore()
    End Sub

    Private Sub UploadStagingFolderErrorToolStripStatusLabel_Click(sender As Object, e As EventArgs) Handles UploadStagingFolderErrorToolStripStatusLabel.Click
        Dim dirInfo As New DirectoryInfo(UserProfile.UploadStagingPath)
        dirInfo.Explore()
    End Sub
#End Region

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If FileSaveToolStripMenuItem.Enabled Then
            SelectedDocumentTabControl.SelectedIndex = 0
            NotesTextBox.Select()
            If presenter.ViewClosingPrompt = False Then
                e.Cancel = True
            End If
        End If
        windowStatePresenter.SetState()
        NativeMethods.RemoveClipboardFormatListener(Me.Handle)
        UserProfile.DeleteUploadShortcut()
        UserProfile.DeleteCachedFiles()
    End Sub
End Class
