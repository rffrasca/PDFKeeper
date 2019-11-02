'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", _
    "CA1506:AvoidExcessiveClassCoupling")> _
Public Class MainForm
    Implements IViewWindowState, IViewToolStripState, IMainView
    Private unboundSettings As MainViewWindowState
    Private toolStripState As MainViewToolStripState
    Private commonPresenter As CommonViewPresenter
    Private presenter As MainViewPresenter
    Private help As New HelpFile
    Private updateCheck As ICommand = New ProductUpdateCheckCommand
    Private m_DocumentRecordNotesChanged As String
    ' Message that is sent when the contents of the clipboard have changed.
    Private Const WM_CLIPBOARDUPDATE As Integer = &H31D

    Public Sub New()
        InitializeComponent()
        unboundSettings = New MainViewWindowState(Me)
        toolStripState = New MainViewToolStripState(Me)
        commonPresenter = New CommonViewPresenter(Me)
        presenter = New MainViewPresenter(Me)
        HelpProvider.HelpNamespace = help.Name
        updateCheck.Execute()   ' Also called on a timer every 30 minutes.
    End Sub

#Region "Interface Members (IViewWindowState)"
    Public Property ViewLocation As Point Implements IViewWindowState.ViewLocation
        Get
            Return Me.Location
        End Get
        Set(value As Point)
            Me.Location = value
        End Set
    End Property

    Public Property ViewSize As Size Implements IViewWindowState.ViewSize
        Get
            Return Me.Size
        End Get
        Set(value As Size)
            Me.Size = value
        End Set
    End Property

    Public ReadOnly Property ViewRestoreBoundsSize As Size Implements IViewWindowState.ViewRestoreBoundsSize
        Get
            Return Me.RestoreBounds.Size
        End Get
    End Property

    Public Property FormWindowState As FormWindowState Implements IViewWindowState.ViewWindowState
        Get
            Return Me.WindowState
        End Get
        Set(value As FormWindowState)
            Me.WindowState = value
        End Set
    End Property

    Public Property ViewSplitterDistance As Integer Implements IViewWindowState.ViewSplitterDistance
        Get
            Return SplitContainer.SplitterDistance
        End Get
        Set(value As Integer)
            SplitContainer.SplitterDistance = value
        End Set
    End Property
#End Region

#Region "Interface Members (IViewToolStripState)"
    Public Sub SetToolStripItemsState(itemShortName As String, enabled As Boolean) Implements IViewToolStripState.SetToolStripItemsState
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
    Public ReadOnly Property SelectedSearchOption As Integer Implements IMainView.SelectedSearchOption
        Get
            Return SearchOptionsTabControl.SelectedIndex
        End Get
    End Property

    Public Property SearchStringHistory As Object Implements IMainView.SearchStringHistory
        Get
            Return SearchStringComboBox.Items
        End Get
        Set(value As Object)
            SearchStringComboBox.Items.Clear()  ' Need to clear twice to work around duplicates
            SearchStringComboBox.Items.Clear()  ' from being displayed in the drop down.
            SearchStringComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property SearchString As String Implements IMainView.SearchString
        Get
            Return SearchStringComboBox.Text
        End Get
        Set(value As String)
            SearchStringComboBox.Text = value
        End Set
    End Property

    Public Property SearchStringErrorProviderMessage As String Implements IMainView.SearchStringErrorProviderMessage
        Get
            Return SearchStringErrorProvider.GetError(SearchStringComboBox)
        End Get
        Set(value As String)
            If value Is Nothing Then
                SearchStringErrorProvider.Clear()
            Else
                SearchStringErrorProvider.SetError(SearchStringComboBox, value)
            End If
        End Set
    End Property

    Public Property SearchEnabled As Boolean Implements IMainView.SearchEnabled
        Get
            Return SearchButton.Enabled
        End Get
        Set(value As Boolean)
            SearchButton.Enabled = value
        End Set
    End Property

    Public Property Authors As DataTable Implements ICommonView.Authors
        Get
            Return AuthorComboBox.DataSource
        End Get
        Set(value As DataTable)
            AuthorComboBox.DataSource = value
            AuthorComboBox.DisplayMember = "doc_author"
        End Set
    End Property

    Public Property Author As String Implements ICommonView.Author
        Get
            If Not AuthorComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(AuthorComboBox.SelectedItem("doc_author"), _
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property Subjects As DataTable Implements ICommonView.Subjects
        Get
            Return SubjectComboBox.DataSource
        End Get
        Set(value As DataTable)
            SubjectComboBox.DataSource = value
            SubjectComboBox.DisplayMember = "doc_subject"
        End Set
    End Property

    Public Property Subject As String Implements ICommonView.Subject
        Get
            If Not SubjectComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(SubjectComboBox.SelectedItem("doc_subject"), _
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property AuthorsPaired As DataTable Implements ICommonView.AuthorsPaired
        Get
            Return AuthorPairedComboBox.DataSource
        End Get
        Set(value As DataTable)
            AuthorPairedComboBox.DataSource = value
            AuthorPairedComboBox.DisplayMember = "doc_author"
        End Set
    End Property

    Public Property AuthorPaired As String Implements ICommonView.AuthorPaired
        Get
            If Not AuthorPairedComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(AuthorPairedComboBox.SelectedItem("doc_author"), _
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public Property SubjectsPaired As DataTable Implements ICommonView.SubjectsPaired
        Get
            Return SubjectPairedComboBox.DataSource
        End Get
        Set(value As DataTable)
            SubjectPairedComboBox.DataSource = value
            SubjectPairedComboBox.DisplayMember = "doc_subject"
        End Set
    End Property

    Public Property SubjectPaired As String Implements ICommonView.SubjectPaired
        Get
            If Not SubjectPairedComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(SubjectPairedComboBox.SelectedItem("doc_subject"), _
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            SubjectPairedComboBox.SelectedItem = value
            SearchOptionsTabControl_Selected(Me, Nothing)
        End Set
    End Property

    Public Property Categories As DataTable Implements ICommonView.Categories
        Get
            Return CategoryComboBox.DataSource
        End Get
        Set(value As DataTable)
            CategoryComboBox.DataSource = value
            CategoryComboBox.DisplayMember = "doc_category"
        End Set
    End Property

    Public Property Category As String Implements ICommonView.Category
        Get
            If Not CategoryComboBox.SelectedItem Is Nothing Then
                Return Convert.ToString(CategoryComboBox.SelectedItem("doc_category"), _
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException
        End Set
    End Property

    Public ReadOnly Property SearchDate As String Implements IMainView.SearchDate
        Get
            Return SearchDateTimePicker.Value.ToString("yyyy-MM-dd", _
                                                       CultureInfo.CurrentCulture)
        End Get
    End Property

    Public Property DBDocumentRecordsCountMessage As String Implements IMainView.DBDocumentRecordsCountMessage
        Get
            Return DBDocumentRecordsCountLabel.Text
        End Get
        Set(value As String)
            DBDocumentRecordsCountLabel.Text = value
        End Set
    End Property

    Public ReadOnly Property FlaggedDocumentsOnly As Boolean Implements IMainView.FlaggedDocumentsOnly
        Get
            Return FlaggedDocumentsOnlyCheckBox.Checked
        End Get
    End Property

    Public Property QueryDocumentsEnabled As Boolean Implements IMainView.QueryDocumentsEnabled
        Get
            Return QueryDocumentsButton.Enabled
        End Get
        Set(value As Boolean)
            QueryDocumentsButton.Enabled = value
        End Set
    End Property

    Public Property SearchResultsExpanded As Boolean Implements IMainView.SearchResultsExpanded
        Get
            Return SplitContainer.Panel2Collapsed
        End Get
        Set(value As Boolean)
            SplitContainer.Panel2Collapsed = value
        End Set
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
            Return RightTabControl.Enabled
        End Get
        Set(value As Boolean)
            toolStripState.SetDocumentSelectedState(value)
            RightTabControl.Enabled = value
        End Set
    End Property

    Public Property DocumentRecordPanelSelectedTab As Integer Implements IMainView.DocumentRecordPanelSelectedTab
        Get
            Return RightTabControl.SelectedIndex
        End Get
        Set(value As Integer)
            RightTabControl.SelectedIndex = value
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
                toolStripState.SetSearchResultsSelectedState(GetSelectedSearchResultsIds.Count)
            End If
            ' The "If" check is needed to prevent user from having to check/uncheck checkbox in
            ' SearchResultsDataGridView when Document Notes length > 0.
            If NotesTextBox.Focused Then
                SearchOptionsTabControl.Enabled = controlEnabled
                SearchResultsDataGridView.Enabled = controlEnabled
            End If
            toolStripState.SetNotesTextBoxChangedState(m_DocumentRecordNotesChanged, _
                                                       NotesTextBox.CanUndo)
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
            PreviewPictureBox.Image = value
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

    Public Sub SelectSearchResultsLastRow() Implements IMainView.SelectSearchResultsLastRow
        SearchResultsDataGridView.Rows(SearchResultsDataGridView.Rows.Count - 1).Selected = True
        SearchResultsDataGridView.FirstDisplayedScrollingRowIndex = SearchResultsDataGridView.RowCount - 1
    End Sub

    Public Sub SelectDeselectAllSearchResults(selectionState As SelectionState) Implements IMainView.SelectDeselectAllSearchResults
        For Each row As DataGridViewRow In SearchResultsDataGridView.Rows
            If selectionState = Enums.SelectionState.SelectAll Then
                row.Cells(0).Value = True
            ElseIf selectionState = Enums.SelectionState.DeselectAll Then
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

    Public Sub OnLongRunningOperationStarted() Implements ICommonView.OnLongRunningOperationStarted
        Me.Cursor = Cursors.WaitCursor
    End Sub

    Public Sub OnLongRunningOperationFinished() Implements ICommonView.OnLongRunningOperationFinished
        Me.Cursor = Cursors.Default
    End Sub
#End Region

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        unboundSettings.GetState()
        SearchStringComboBox.Select()
        NativeMethods.AddClipboardFormatListener(Me.Handle)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = WM_CLIPBOARDUPDATE Then
            If My.Computer.Clipboard.ContainsText Then
                If NotesTextBox.Focused Then
                    toolStripState.SetPasteState(True)
                End If
            End If
        End If
    End Sub

#Region "ToolStrip Events"
    Private Sub ToolStrip_VisibleChanged(sender As Object, e As EventArgs) Handles ToolStrip.VisibleChanged
        ViewToolbarToolStripMenuItem.Checked = ToolStrip.Visible
    End Sub

    Private Sub FileNewToolStrip_Click(sender As Object, e As EventArgs) Handles FileNewToolStripMenuItem.Click, _
                                                                                 FileNewToolStripButton.Click
        AddPdfDocumentsDialog.ShowDialog()
    End Sub

    Private Sub FileOpenToolStrip_Click(sender As Object, e As EventArgs) Handles FileOpenToolStripMenuItem.Click, _
                                                                                  FileOpenToolStripButton.Click
        presenter.OpenSelectedDocumentPdf()
    End Sub

    Private Sub FileSaveToolStrip_Click(sender As Object, e As EventArgs) Handles FileSaveToolStripMenuItem.Click, _
                                                                                  FileSaveToolStripButton.Click
        presenter.SaveSelectedDocumentNotes()
        presenter.NotesTextChanged()
        toolStripState.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly, _
                                                    NotesTextBox.TextLength, _
                                                    NotesTextBox.SelectionLength)
    End Sub

    Private Sub FileSaveAsToolStrip_Click(sender As Object, e As EventArgs) Handles FileSaveAsToolStripMenuItem.Click
        presenter.SaveSelectedDocumentPdfOrTextAs()
    End Sub

    Private Sub FilePrintToolStrip_Click(sender As Object, e As EventArgs) Handles FilePrintToolStripMenuItem.Click, _
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

    Private Sub FileDeleteToolStrip_Click(sender As Object, e As EventArgs) Handles FileDeleteToolStripMenuItem.Click, _
                                                                                    FileDeleteToolStripButton.Click
        presenter.DeleteSelectedSearchResults()
    End Sub

    Private Sub FileExportToolStrip_Click(sender As Object, e As EventArgs) Handles FileExportToolStripMenuItem.Click
        presenter.ExportSelectedSearchResults()
    End Sub

    Private Sub FileExitToolStrip_Click(sender As Object, e As EventArgs) Handles FileExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub EditUndoToolStrip_Click(sender As Object, e As EventArgs) Handles EditUndoToolStripMenuItem.Click, _
                                                                                  EditUndoToolStripButton.Click
        NotesTextBox.Undo()
    End Sub

    Private Sub EditCutToolStrip_Click(sender As Object, e As EventArgs) Handles EditCutToolStripMenuItem.Click, _
                                                                                 EditCutToolStripButton.Click
        NotesTextBox.Cut()
    End Sub

    Private Sub EditCopyToolStrip_Click(sender As Object, e As EventArgs) Handles EditCopyToolStripMenuItem.Click, _
                                                                                  EditCopyToolStripButton.Click
        GetTextBoxWithInputFocus.Copy()
    End Sub

    Private Sub EditPasteToolStrip_Click(sender As Object, e As EventArgs) Handles EditPasteToolStripMenuItem.Click, _
                                                                                   EditPasteToolStripButton.Click
        NotesTextBox.Paste()
    End Sub

    Private Sub EditSelectAllToolStrip_Click(sender As Object, e As EventArgs) Handles EditSelectAllToolStripMenuItem.Click
        GetTextBoxWithInputFocus.SelectAll()
        toolStripState.SetTextBoxTextSelectionState(GetTextBoxWithInputFocus.ReadOnly, _
                                                    GetTextBoxWithInputFocus.TextLength, _
                                                    GetTextBoxWithInputFocus.SelectionLength)
    End Sub

    Private Sub EditRestoreToolStrip_Click(sender As Object, e As EventArgs) Handles EditRestoreToolStripMenuItem.Click, _
                                                                                     EditRestoreToolStripButton.Click
        presenter.RestoreSelectedDocumentNotes()
        presenter.NotesTextChanged()
    End Sub

    Private Sub EditDateTimeToolStrip_Click(sender As Object, e As EventArgs) Handles EditDateTimeToolStripMenuItem.Click, _
                                                                                      EditDateTimeToolStripButton.Click
        presenter.AppendDateTimeUserNameToSelectedDocumentNotes()
    End Sub

    Private Sub EditFlagDocumentToolStrip_Click(sender As Object, e As EventArgs) Handles EditFlagDocumentToolStripMenuItem.Click
        presenter.SetFlagStateOnSelectedDocument()
    End Sub

    Private Sub ViewToggleRightPanelToolStrip_Click(sender As Object, e As EventArgs) Handles ViewToggleRightPanelToolStripMenuItem.Click, _
                                                                                              ViewToggleRightPanelToolStripButton.Click
        SearchResultsDataGridView.Columns(5).AutoSizeMode = _
            DataGridViewAutoSizeColumnMode.DisplayedCells
        If SplitContainer.Panel2Collapsed Then
            SplitContainer.Panel2Collapsed = False
        Else
            SplitContainer.Panel2Collapsed = True
            SearchResultsDataGridView.Columns(5).AutoSizeMode = _
                DataGridViewAutoSizeColumnMode.Fill
        End If
    End Sub

    Private Sub ViewRefreshToolStrip_Click(sender As Object, e As EventArgs) Handles ViewRefreshToolStripMenuItem.Click, _
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

    Private Sub ToolsOptionsToolStripButton_Click(sender As Object, e As EventArgs) Handles ToolsOptionsToolStripButton.Click, _
                                                                                            ToolsOptionsToolStripMenuItem.Click
        OptionsDialog.ShowDialog()
    End Sub

    Private Sub ToolsManageUploadFoldersToolStrip_Click(sender As Object, e As EventArgs) Handles ToolsManageUploadFolderConfigurationsToolStripMenuItem.Click, _
                                                                                                  ToolsManageUploadFolderConfigurationsToolStripButton.Click
        ManageUploadFolderConfigurationsDialog.ShowDialog()
    End Sub

    Private Sub HelpContentsToolStrip_Click(sender As Object, e As EventArgs) Handles HelpContentsToolStripMenuItem.Click, _
                                                                                      HelpContentsToolStripButton.Click
        help.Show(Me, "Using PDFKeeper.html")
    End Sub

    Private Sub HelpAboutToolStrip_Click(sender As Object, e As EventArgs) Handles HelpAboutToolStripMenuItem.Click
        AboutBox.ShowDialog()
    End Sub
#End Region

#Region "Search Options Events and Members"
    Private Sub SearchOptionsTabControl_Selected(sender As Object, e As TabControlEventArgs) Handles SearchOptionsTabControl.Selected
        toolStripState.SetPreSearchState()
        SplitContainer.Panel2Collapsed = False
        presenter.SearchOptionSelected()
    End Sub

    Private Sub SearchStringComboBox_Enter(sender As Object, e As EventArgs) Handles SearchStringComboBox.Enter
        presenter.GetSearchStringHistory()
    End Sub

    Private Sub SearchStringComboBox_TextChanged(sender As Object, e As EventArgs) Handles SearchStringComboBox.TextChanged
        toolStripState.SetPreSearchState()
        presenter.SearchStringTextChanged()
    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        presenter.GetSearchResultsByString(False)
    End Sub

    Private Sub AuthorComboBox_DropDown(sender As Object, e As EventArgs) Handles AuthorComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub AuthorComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles AuthorComboBox.DropDownClosed
        presenter.GetSearchResultsByAuthor()
    End Sub

    Private Sub AuthorComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles AuthorComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            AuthorComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub AuthorComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles AuthorComboBox.KeyUp
        ' Pressing the up arrow when ComboBox has focus will select the previous Author in the
        ' collection when the drop down is in the closed position.  When drop down is in the
        ' open position, the up arrow will move selector up the list.
        If e.KeyCode = 38 Then
            If AuthorComboBox.DroppedDown = False Then
                presenter.GetSearchResultsByAuthor()
            End If
        End If
    End Sub

    Private Sub AuthorComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles AuthorComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not AuthorComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub SubjectComboBox_DropDown(sender As Object, e As EventArgs) Handles SubjectComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub SubjectComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles SubjectComboBox.DropDownClosed
        presenter.GetSearchResultsBySubject()
    End Sub

    Private Sub SubjectComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SubjectComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            SubjectComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub SubjectComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles SubjectComboBox.KeyUp
        ' Pressing the up arrow when ComboBox has focus will select the previous Subject in the
        ' collection when the drop down is in the closed position.  When drop down is in the
        ' open position, the up arrow will move selector up the list.
        If e.KeyCode = 38 Then
            If SubjectComboBox.DroppedDown = False Then
                presenter.GetSearchResultsBySubject()
            End If
        End If
    End Sub

    Private Sub SubjectComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles SubjectComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not SubjectComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub AuthorPairedComboBox_DropDown(sender As Object, e As EventArgs) Handles AuthorPairedComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub AuthorPairedComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles AuthorPairedComboBox.DropDownClosed
        presenter.ClearSubjectPairedSelection()
        presenter.GetSearchResultsByAuthorAndSubject()
    End Sub

    Private Sub AuthorPairedComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles AuthorPairedComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            AuthorPairedComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub AuthorPairedComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles AuthorPairedComboBox.KeyUp
        ' Pressing the up arrow when ComboBox has focus will select the previous Author in the
        ' collection when the drop down is in the closed position.  When drop down is in the
        ' open position, the up arrow will move selector up the list.
        If e.KeyCode = 38 Then
            If AuthorPairedComboBox.DroppedDown = False Then
                presenter.ClearSubjectPairedSelection()
                presenter.GetSearchResultsByAuthorAndSubject()
            End If
        End If
    End Sub

    Private Sub AuthorPairedComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles AuthorPairedComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not AuthorPairedComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub SubjectPairedComboBox_DropDown(sender As Object, e As EventArgs) Handles SubjectPairedComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub SubjectPairedComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles SubjectPairedComboBox.DropDownClosed
        presenter.GetSearchResultsByAuthorAndSubject()
    End Sub

    Private Sub SubjectPairedComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SubjectPairedComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            SubjectPairedComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub SubjectPairedComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles SubjectPairedComboBox.KeyUp
        ' Pressing the up arrow when ComboBox has focus will select the previous Subject in the
        ' collection when the drop down is in the closed position.  When drop down is in the
        ' open position, the up arrow will move selector up the list.
        If e.KeyCode = 38 Then
            If SubjectPairedComboBox.DroppedDown = False Then
                presenter.GetSearchResultsByAuthorAndSubject()
            End If
        End If
    End Sub

    Private Sub SubjectPairedComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles SubjectPairedComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not SubjectPairedComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub CategoryComboBox_DropDown(sender As Object, e As EventArgs) Handles CategoryComboBox.DropDown
        commonPresenter.GetColumnItemsByGroup()
    End Sub

    Private Sub CategoryComboBox_DropDownClosed(sender As Object, e As EventArgs) Handles CategoryComboBox.DropDownClosed
        presenter.GetSearchResultsByCategory()
    End Sub

    Private Sub CategoryComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CategoryComboBox.KeyDown
        ' ComboBox will only drop down when the down arrow is pressed.
        If e.KeyCode = 40 Then
            CategoryComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub CategoryComboBox_KeyUp(sender As Object, e As KeyEventArgs) Handles CategoryComboBox.KeyUp
        ' Pressing the up arrow when ComboBox has focus will select the previous Category in the
        ' collection when the drop down is in the closed position.  When drop down is in the
        ' open position, the up arrow will move selector up the list.
        If e.KeyCode = 38 Then
            If CategoryComboBox.DroppedDown = False Then
                presenter.GetSearchResultsByCategory()
            End If
        End If
    End Sub

    Private Sub CategoryComboBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles CategoryComboBox.MouseWheel
        ' This will prevent mouse wheel scrolling while drop down is closed.
        If Not CategoryComboBox.DroppedDown Then
            Dim handledMouseEventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
            handledMouseEventArgs.Handled = True
        End If
    End Sub

    Private Sub SearchDateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles SearchDateTimePicker.ValueChanged
        presenter.GetSearchResultsByDateAdded()
    End Sub

    Private Sub FlaggedDocumentsOnlyCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FlaggedDocumentsOnlyCheckBox.CheckedChanged
        SearchOptionsTabControl_Selected(Me, Nothing)
    End Sub

    Private Sub QueryDocumentsButton_Click(sender As Object, e As EventArgs) Handles QueryDocumentsButton.Click
        presenter.QueryAllOrFlaggedDocumentRecords()
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
            SearchResultsDataGridView.Columns(6).AutoSizeMode = _
                DataGridViewAutoSizeColumnMode.DisplayedCells
            If SearchResultsDataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Displayed) > _
                SearchResultsDataGridView.Size.Width Then
                SplitContainer.Panel2Collapsed = True
            End If
            If SearchResultsDataGridView.Columns(6).Displayed = True Then
                SearchResultsDataGridView.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            SearchResultsDataGridView.Columns(6).MinimumWidth = SearchResultsDataGridView.Columns(6).FillWeight + 20
        End If
        toolStripState.SetPostSearchState()
        SearchResultsDataGridView.Focus()
    End Sub

    Private Sub SearchResultsDataGridView_Sorted(sender As Object, e As EventArgs) Handles SearchResultsDataGridView.Sorted
        presenter.SortSearchResults()
    End Sub

    Private Sub SearchResultsDataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles SearchResultsDataGridView.RowsAdded
        TotalRecordsCountToolStripStatusLabel.Text = SearchResultsDataGridView.RowCount
        toolStripState.SetSearchResultsRowCountChangedState(SearchResultsDataGridView.RowCount, _
                                                            SplitContainer.Panel2Collapsed)
    End Sub

    Private Sub SearchResultsDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles SearchResultsDataGridView.RowsRemoved
        TotalRecordsCountToolStripStatusLabel.Text = SearchResultsDataGridView.RowCount
        toolStripState.SetSearchResultsRowCountChangedState(SearchResultsDataGridView.RowCount, _
                                                            SplitContainer.Panel2Collapsed)
        toolStripState.SetSearchResultsSelectedState(GetSelectedSearchResultsIds.Count)
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
        toolStripState.SetSearchResultsSelectedState(GetSelectedSearchResultsIds.Count)
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
    Private Sub NotesTextBox_Enter(sender As Object, e As EventArgs) Handles NotesTextBox.Enter, _
                                                                             NotesTextBox.GotFocus
        toolStripState.SetTextBoxEnterState(NotesTextBox.ReadOnly, NotesTextBox.TextLength)
        If NotesTextBox.TextLength > 0 Then
            toolStripState.SetTextBoxPrintableState(True)
        End If
        If My.Computer.Clipboard.ContainsText Then
            toolStripState.SetPasteState(True)
        End If
        toolStripState.SetNotesTextBoxChangedState(m_DocumentRecordNotesChanged, _
                                                   NotesTextBox.CanUndo)
    End Sub

    Private Sub NotesTextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles NotesTextBox.MouseUp
        toolStripState.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly, _
                                                    NotesTextBox.TextLength, _
                                                    NotesTextBox.SelectionLength)
    End Sub

    Private Sub NotesTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NotesTextBox.KeyPress
        toolStripState.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly, _
                                                             NotesTextBox.TextLength, _
                                                             NotesTextBox.SelectionLength)
    End Sub

    Private Sub NotesTextBox_TextChanged(sender As Object, e As EventArgs) Handles NotesTextBox.TextChanged
        presenter.NotesTextChanged()
        toolStripState.SetTextBoxTextSelectionState(NotesTextBox.ReadOnly, _
                                                    NotesTextBox.TextLength, _
                                                    NotesTextBox.SelectionLength)
    End Sub

    Private Sub NotesTextBox_Leave(sender As Object, e As EventArgs) Handles NotesTextBox.Leave
        toolStripState.SetTextBoxLeaveState()
        toolStripState.SetTextBoxPrintableState(False)
    End Sub

    Private Sub KeywordsTextBox_Enter(sender As Object, e As EventArgs) Handles KeywordsTextBox.Enter
        toolStripState.SetTextBoxEnterState(KeywordsTextBox.ReadOnly, _
                                            KeywordsTextBox.TextLength)
        toolStripState.SetTextBoxPrintableState(False)
    End Sub

    Private Sub KeywordsTextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles KeywordsTextBox.MouseUp
        toolStripState.SetTextBoxTextSelectionState(KeywordsTextBox.ReadOnly, _
                                                    KeywordsTextBox.TextLength, _
                                                    KeywordsTextBox.SelectionLength)
    End Sub

    Private Sub KeywordsTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KeywordsTextBox.KeyPress
        toolStripState.SetTextBoxTextSelectionState(KeywordsTextBox.ReadOnly, _
                                                    KeywordsTextBox.TextLength, _
                                                    KeywordsTextBox.SelectionLength)
    End Sub

    Private Sub KeywordsTextBox_Leave(sender As Object, e As EventArgs) Handles KeywordsTextBox.Leave
        toolStripState.SetTextBoxLeaveState()
    End Sub

    Private Sub PreviewPictureBox_DoubleClick(sender As Object, e As EventArgs) Handles PreviewPictureBox.DoubleClick
        PreviewImageResolutionDialog.ShowDialog()
        presenter.ReloadDocumentPreview()
    End Sub

    Private Sub TextTextBox_Enter(sender As Object, e As EventArgs) Handles TextTextBox.Enter
        toolStripState.SetTextBoxEnterState(TextTextBox.ReadOnly, TextTextBox.TextLength)
        If TextTextBox.TextLength > 0 Then
            toolStripState.SetTextBoxPrintableState(True)
        End If
    End Sub

    Private Sub TextTextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles TextTextBox.MouseUp
        toolStripState.SetTextBoxTextSelectionState(TextTextBox.ReadOnly, _
                                                    TextTextBox.TextLength, _
                                                    TextTextBox.SelectionLength)
    End Sub

    Private Sub TextTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextTextBox.KeyPress
        toolStripState.SetTextBoxTextSelectionState(TextTextBox.ReadOnly, _
                                                    TextTextBox.TextLength, _
                                                    TextTextBox.SelectionLength)
    End Sub

    Private Sub TextTextBox_Leave(sender As Object, e As EventArgs) Handles TextTextBox.Leave
        toolStripState.SetTextBoxLeaveState()
        toolStripState.SetTextBoxPrintableState(False)
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
        updateCheck.Execute()
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
            RightTabControl.SelectedIndex = 0
            NotesTextBox.Select()
            If presenter.ViewClosingPrompt = False Then
                e.Cancel = True
            End If
        End If
        NativeMethods.RemoveClipboardFormatListener(Me.Handle)
    End Sub
End Class
