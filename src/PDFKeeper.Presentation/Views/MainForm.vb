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
Imports System.Collections.ObjectModel
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.Infrastructure
Imports PDFKeeper.Services

Public Class MainForm
    Implements IMainView, INotifyPropertyChanged
    Private ReadOnly presenter As MainPresenter
    Private ReadOnly help As New HelpFile
    Private _NotesChanged As Boolean

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        InitializeComponent()
        Dim repository = DocumentRepositoryFactory.Repository
        presenter = New MainPresenter(Me, New AuthorListService(repository), New SubjectListService(repository),
                                      New CategoryListService(repository), New TaxYearListService(repository),
                                      New DocumentService(repository), New DocumentListService(repository),
                                      New DocumentUtilService(repository), New FileCacheService,
                                      New SearchTermHistoryService, New PdfService,
                                      New UploadService(
                                      New XmlRepository(Of UploadProfileModel)(
                                      AppFolders.GetPath(AppFolders.AppFolder.UploadProfiles)),
                                      New DocumentService(repository),
                                      New UploadProfileService(New XmlRepository(Of UploadProfileModel)(
                                      AppFolders.GetPath(AppFolders.AppFolder.UploadProfiles)))))
        HelpProvider.HelpNamespace = help.FileName
        SetToolStripItemTags()
        AddHandlers()
    End Sub

    Public Property ViewToolBarMenuItemChecked As Boolean Implements IMainView.ViewToolBarMenuItemChecked
        Get
            Return ViewToolBarToolStripMenuItem.Checked
        End Get
        Set(value As Boolean)
            ViewToolBarToolStripMenuItem.Checked = value
        End Set
    End Property

    Public Property ViewStatusBarMenuItemChecked As Boolean Implements IMainView.ViewStatusBarMenuItemChecked
        Get
            Return ViewStatusBarToolStripMenuItem.Checked
        End Get
        Set(value As Boolean)
            ViewStatusBarToolStripMenuItem.Checked = value
        End Set
    End Property

    Public Property ToolStripVisible As Boolean Implements IMainView.ToolStripVisible
        Get
            Return ToolStrip.Visible
        End Get
        Set(value As Boolean)
            ToolStrip.Visible = value
        End Set
    End Property

    Public Property SplitterDistance As Integer Implements IMainView.SplitterDistance
        Get
            Return SplitContainer.SplitterDistance
        End Get
        Set(value As Integer)
            SplitContainer.SplitterDistance = value
        End Set
    End Property

    Public Property DocumentRetrievalGroupEnabled As Boolean Implements IMainView.DocumentRetrievalGroupEnabled
        Get
            Return DocumentRetrievalGroupBox.Enabled
        End Get
        Set(value As Boolean)
            DocumentRetrievalGroupBox.Enabled = value
        End Set
    End Property

    Public ReadOnly Property DocumentRetrievalChoicesListEnabled As Boolean Implements IMainView.DocumentRetrievalChoicesListEnabled
        Get
            Return DocumentRetrievalChoicesListBox.Enabled
        End Get
    End Property

    Public Property DocumentRetrievalChoiceSelectedIndex As Integer Implements IMainView.DocumentRetrievalChoiceSelectedIndex
        Get
            Return DocumentRetrievalChoicesListBox.SelectedIndex
        End Get
        Set(value As Integer)
            DocumentRetrievalChoicesListBox.SelectedIndex = value
        End Set
    End Property

    Public Property SearchTerm As String Implements IMainView.SearchTerm
        Get
            Return SearchTermComboBox.Text
        End Get
        Set(value As String)
            SearchTermComboBox.Text = value
        End Set
    End Property

    Public Property SearchTermItems As Object Implements IMainView.SearchTermItems
        Get
            Return SearchTermComboBox.Items
        End Get
        Set(value As Object)
            SearchTermComboBox.Items.Clear()  ' Clearing twice will prevent duplicate items in the drop down list.
            SearchTermComboBox.Items.Clear()
            SearchTermComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property SearchTermEnabled As Boolean Implements IMainView.SearchTermEnabled
        Get
            Return SearchTermComboBox.Enabled
        End Get
        Set(value As Boolean)
            SearchTermComboBox.Enabled = value
        End Set
    End Property

    Public Property FindBySearchTermEnabled As Boolean Implements IMainView.FindBySearchTermEnabled
        Get
            Return FindBySearchTermButton.Enabled
        End Get
        Set(value As Boolean)
            FindBySearchTermButton.Enabled = value
        End Set
    End Property

    Public Property Author As String Implements IViewCommon.Author
        Get
            If AuthorComboBox.SelectedItem IsNot Nothing Then
                Return Convert.ToString(AuthorComboBox.SelectedItem(AuthorComboBox.DisplayMember),
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            AuthorComboBox.SelectedItem = value
        End Set
    End Property

    Public Property AuthorItems As DataTable Implements IViewCommon.AuthorItems
        Get
            Return AuthorComboBox.DataSource
        End Get
        Set(value As DataTable)
            AuthorComboBox.DataSource = value
            AuthorComboBox.DisplayMember = AuthorComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property AuthorEnabled As Boolean Implements IMainView.AuthorEnabled
        Get
            Return AuthorComboBox.Enabled
        End Get
        Set(value As Boolean)
            AuthorComboBox.Enabled = value
        End Set
    End Property

    Public Property Subject As String Implements IViewCommon.Subject
        Get
            If SubjectComboBox.SelectedItem IsNot Nothing Then
                Return Convert.ToString(SubjectComboBox.SelectedItem(SubjectComboBox.DisplayMember),
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            SubjectComboBox.SelectedItem = value
        End Set
    End Property

    Public Property SubjectItems As DataTable Implements IViewCommon.SubjectItems
        Get
            Return SubjectComboBox.DataSource
        End Get
        Set(value As DataTable)
            SubjectComboBox.DataSource = value
            SubjectComboBox.DisplayMember = SubjectComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property SubjectEnabled As Boolean Implements IMainView.SubjectEnabled
        Get
            Return SubjectComboBox.Enabled
        End Get
        Set(value As Boolean)
            SubjectComboBox.Enabled = value
        End Set
    End Property

    Public Property Category As String Implements IViewCommon.Category
        Get
            If CategoryComboBox.SelectedItem IsNot Nothing Then
                Return Convert.ToString(CategoryComboBox.SelectedItem(CategoryComboBox.DisplayMember),
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            CategoryComboBox.SelectedItem = value
        End Set
    End Property

    Public Property CategoryItems As DataTable Implements IViewCommon.CategoryItems
        Get
            Return CategoryComboBox.DataSource
        End Get
        Set(value As DataTable)
            CategoryComboBox.DataSource = value
            CategoryComboBox.DisplayMember = CategoryComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property CategoryEnabled As Boolean Implements IMainView.CategoryEnabled
        Get
            Return CategoryComboBox.Enabled
        End Get
        Set(value As Boolean)
            CategoryComboBox.Enabled = value
        End Set
    End Property

    Public Property TaxYear As String Implements IViewCommon.TaxYear
        Get
            If TaxYearComboBox.SelectedItem IsNot Nothing Then
                Return Convert.ToString(TaxYearComboBox.SelectedItem(TaxYearComboBox.DisplayMember),
                                        CultureInfo.CurrentCulture)
            End If
            Return Nothing
        End Get
        Set(value As String)
            TaxYearComboBox.SelectedItem = value
        End Set
    End Property

    Public Property TaxYearItems As DataTable Implements IViewCommon.TaxYearItems
        Get
            Return TaxYearComboBox.DataSource
        End Get
        Set(value As DataTable)
            TaxYearComboBox.DataSource = value
            TaxYearComboBox.DisplayMember = TaxYearComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Public Property TaxYearEnabled As Boolean Implements IMainView.TaxYearEnabled
        Get
            Return TaxYearComboBox.Enabled
        End Get
        Set(value As Boolean)
            TaxYearComboBox.Enabled = value
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

    Public Property FindBySelectionsEnabled As Boolean Implements IMainView.FindBySelectionsEnabled
        Get
            Return FindBySelectionsButton.Enabled
        End Get
        Set(value As Boolean)
            FindBySelectionsButton.Enabled = value
        End Set
    End Property

    Public ReadOnly Property DateAdded As String Implements IMainView.DateAdded
        Get
            Return DateAddedDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture)
        End Get
    End Property

    Public Property DateAddedEnabled As Boolean Implements IMainView.DateAddedEnabled
        Get
            Return DateAddedDateTimePicker.Enabled
        End Get
        Set(value As Boolean)
            DateAddedDateTimePicker.Enabled = value
        End Set
    End Property

    Public Property DocumentList As DataTable Implements IMainView.DocumentList
        Get
            Return DocumentListDataGridView.DataSource
        End Get
        Set(value As DataTable)
            DocumentListDataGridView.DataSource = value
        End Set
    End Property

    Public Property DocumentListViewEnabled As Boolean Implements IMainView.DocumentListViewEnabled
        Get
            Return DocumentListDataGridView.Enabled
        End Get
        Set(value As Boolean)
            DocumentListDataGridView.Enabled = value
        End Set
    End Property

    Public ReadOnly Property DocumentListRowCount As Integer Implements IMainView.DocumentListRowCount
        Get
            Return DocumentListDataGridView.RowCount
        End Get
    End Property

    Public Property DocumentListColumn0AutoSizeMode As DataGridViewAutoSizeColumnMode Implements IMainView.DocumentListColumn0AutoSizeMode
        Get
            Return DocumentListDataGridView.Columns(0).AutoSizeMode
        End Get
        Set(value As DataGridViewAutoSizeColumnMode)
            DocumentListDataGridView.Columns(0).AutoSizeMode = value
        End Set
    End Property

    Public ReadOnly Property CheckedDocumentIdItems As Collection(Of Integer) Implements IMainView.CheckedDocumentIdItems
        Get
            Dim items = New Collection(Of Integer)
            For Each row In DocumentListDataGridView.Rows
                If row.Cells(0).Value = True Then
                    items.Add(row.Cells(1).Value)
                End If
            Next
            Return items
        End Get
    End Property

    Public ReadOnly Property SelectedDocumentId As Integer Implements IMainView.SelectedDocumentId
        Get
            ' Filter out null selections that occur when the DataGridView is filled.
            If DocumentListDataGridView.SelectedRows.Count > 0 Then
                Return DocumentListDataGridView.SelectedRows(0).Cells(1).Value
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Property FlagState As Integer Implements IMainView.FlagState
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

    Public Property DocumentTabControlSelectedIndex As Integer Implements IMainView.DocumentTabControlSelectedIndex
        Get
            Return DocumentTabControl.SelectedIndex
        End Get
        Set(value As Integer)
            DocumentTabControl.SelectedIndex = value
        End Set
    End Property

    Public Property DocumentTabControlEnabled As Boolean Implements IMainView.DocumentTabControlEnabled
        Get
            Return DocumentTabControl.Enabled
        End Get
        Set(value As Boolean)
            DocumentTabControl.Enabled = value
        End Set
    End Property

    Public ReadOnly Property DocumentTabControlSelectedTabTextBoxText As String Implements IMainView.DocumentTabControlSelectedTabTextBoxText
        Get
            Dim text As String = Nothing
            If DocumentTabControl.SelectedIndex = 0 Then
                If NotesTextBox.Text.Length > 0 Then
                    text = NotesTextBox.Text
                End If
            ElseIf DocumentTabControl.SelectedIndex = 3 Then
                If TextTextBox.Text.Length > 0 Then
                    text = TextTextBox.Text
                End If
            ElseIf DocumentTabControl.SelectedIndex = 4 Then
                If SearchTermSnippetsTextBox.Text.Length > 0 Then
                    text = SearchTermSnippetsTextBox.Text
                End If
            End If
            Return text
        End Get
    End Property

    Public ReadOnly Property TextFromFocusedTextBox As String Implements IMainView.TextFromFocusedTextBox
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
            ElseIf SearchTermSnippetsTextBox.Focused Then
                If SearchTermSnippetsTextBox.Text.Length > 0 Then
                    text = SearchTermSnippetsTextBox.Text
                End If
            End If
            Return text
        End Get
    End Property

    Public Property Notes As String Implements IMainView.Notes
        Get
            Return NotesTextBox.Text
        End Get
        Set(value As String)
            NotesTextBox.Text = value
        End Set
    End Property

    Public ReadOnly Property NotesSelectionLength As Integer Implements IMainView.NotesSelectionLength
        Get
            Return NotesTextBox.SelectionLength
        End Get
    End Property

    Public Property NotesChanged As Boolean Implements IMainView.NotesChanged
        Get
            Return _NotesChanged
        End Get
        Set(value As Boolean)
            _NotesChanged = value
            presenter.NotifyPropertyChanged()
        End Set
    End Property

    Public ReadOnly Property NotesFocused As Boolean Implements IMainView.NotesFocused
        Get
            Return NotesTextBox.Focused
        End Get
    End Property

    Public ReadOnly Property NotesCanUndo As Boolean Implements IMainView.NotesCanUndo
        Get
            Return NotesTextBox.CanUndo
        End Get
    End Property

    Public ReadOnly Property NotesReadOnly As Boolean Implements IMainView.NotesReadOnly
        Get
            Return NotesTextBox.ReadOnly
        End Get
    End Property

    Public Property Keywords As String Implements IMainView.Keywords
        Get
            Return KeywordsTextBox.Text
        End Get
        Set(value As String)
            KeywordsTextBox.Text = value
        End Set
    End Property

    Public Property Preview As Image Implements IMainView.Preview
        Get
            Return PreviewPictureBox.Image
        End Get
        Set(value As Image)
            ' Dispose the PictureBox first to release memory before loading image.
            If PreviewPictureBox.Image IsNot Nothing Then
                PreviewPictureBox.Image.Dispose()
            End If
            Dim imageLock = New Object
            SyncLock imageLock  ' Required to prevent a rare InvalidOperationException.
                PreviewPictureBox.Image = value
            End SyncLock
        End Set
    End Property

    Private Property IMainView_Text As String Implements IMainView.Text
        Get
            Return TextTextBox.Text
        End Get
        Set(value As String)
            TextTextBox.Text = value
        End Set
    End Property

    Public Property SearchTermSnippets As String Implements IMainView.SearchTermSnippets
        Get
            Return SearchTermSnippetsTextBox.Text
        End Get
        Set(value As String)
            SearchTermSnippetsTextBox.Text = value
        End Set
    End Property

    Public Property StatusStripVisible As Boolean Implements IMainView.StatusStripVisible
        Get
            Return StatusStrip.Visible
        End Get
        Set(value As Boolean)
            StatusStrip.Visible = value
        End Set
    End Property

    Public Property DocumentListCountStatusText As String Implements IMainView.DocumentListCountStatusText
        Get
            Return DocumentsCountToolStripStatusLabel.Text
        End Get
        Set(value As String)
            DocumentsCountToolStripStatusLabel.Text = value
        End Set
    End Property

    Public Property ProgressBarVisible As Boolean Implements IMainView.ProgressBarVisible
        Get
            Return ToolStripProgressBar.Visible
        End Get
        Set(value As Boolean)
            ToolStripProgressBar.Visible = value    ' Must be performed first.
            If value Then
                ToolStripProgressBar.Value = 0
            End If
        End Set
    End Property

    Public Property ProgressBarMaximum As Integer Implements IMainView.ProgressBarMaximum
        Get
            Return ToolStripProgressBar.Maximum
        End Get
        Set(value As Integer)
            ToolStripProgressBar.Maximum = value
        End Set
    End Property

    Public Property FlagImageVisible As Boolean Implements IMainView.FlagImageVisible
        Get
            Return FlagImageToolStripStatusLabel.Visible
        End Get
        Set(value As Boolean)
            FlagImageToolStripStatusLabel.Visible = value
        End Set
    End Property

    Public Property UploadRunningImageVisible As Boolean Implements IMainView.UploadRunningImageVisible
        Get
            Return UploadRunningImageToolStripStatusLabel.Visible
        End Get
        Set(value As Boolean)
            UploadRunningImageToolStripStatusLabel.Visible = value
        End Set
    End Property

    Public Property UploadRejectedImageVisible As Boolean Implements IMainView.UploadRejectedImageVisible
        Get
            Return UploadRejectedImageToolStripStatusLabel.Visible
        End Get
        Set(value As Boolean)
            UploadRejectedImageToolStripStatusLabel.Visible = value
        End Set
    End Property

    Public Sub RemoveListAllDocumentsChoice() Implements IMainView.RemoveListAllDocumentsChoice
        DocumentRetrievalChoicesListBox.Items.RemoveAt(4)
    End Sub

    Public Sub SelectAllDocumentCheckBoxes(check As Boolean) Implements IMainView.SelectAllDocumentCheckBoxes
        Me.Cursor = Cursors.WaitCursor
        For Each row In DocumentListDataGridView.Rows
            row.Cells(0).Value = check
        Next
        DocumentListDataGridView.RefreshEdit()
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub SelectDocument(id As Integer) Implements IMainView.SelectDocument
        For Each row In DocumentListDataGridView.Rows
            If row.Cells(1).Value = id Then
                row.Selected = True
                DocumentListDataGridView.FirstDisplayedScrollingRowIndex = row.Index
            End If
        Next
    End Sub

    Public Sub ScrollToEndOfNotesText() Implements IMainView.ScrollToEndOfNotesText
        NotesTextBox.Select()
        NotesTextBox.Select(NotesTextBox.Text.Length, 0)
        NotesTextBox.ScrollToCaret()
    End Sub

    Public Sub PerformStepOnProgressBar() Implements IMainView.PerformStepOnProgressBar
        ToolStripProgressBar.PerformStep()
    End Sub

    Public Sub SetErrorProviderMessage(message As String) Implements IMainView.SetErrorProviderMessage
        If message Is Nothing Then
            ErrorProvider.Clear()
        Else
            ErrorProvider.SetError(SearchTermComboBox, message)
        End If
    End Sub

    Public Function ShowOpenTextFileDialog() As String Implements IMainView.ShowOpenTextFileDialog
        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            Return OpenFileDialog.FileName
        Else
            Return Nothing
        End If
    End Function

    Public Function ShowSaveFileDialog(filter As String, fileNamePrefill As String) As String Implements IMainView.ShowSaveFileDialog
        SaveFileDialog.Filter = filter
        SaveFileDialog.FileName = fileNamePrefill
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            Return SaveFileDialog.FileName
        Else
            Return Nothing
        End If
    End Function

    Public Function ShowFolderBrowserDialog(description As String) As String Implements IMainView.ShowFolderBrowserDialog
        FolderBrowserDialog.Description = description
        If FolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            Return FolderBrowserDialog.SelectedPath
        Else
            Return Nothing
        End If
    End Function

    Public Sub PrintSelectectedTextBoxText(usePreview As Boolean) Implements IMainView.PrintSelectectedTextBoxText
        If usePreview Then
            PrintPreviewDialog.ClientSize = Me.Size
            PrintPreviewDialog.ShowDialog()
        Else
            If PrintDialog.ShowDialog() = DialogResult.OK Then
                PrintDocument.Print()
            End If
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        presenter.MyBaseWndProc(m)
        MyBase.WndProc(m)
    End Sub

    Private Sub SetToolStripItemTags()
        Dim textBoxes = New Collection(Of TextBox) From {
            NotesTextBox,
            KeywordsTextBox,
            TextTextBox,
            SearchTermSnippetsTextBox
        }
        EditUndoToolStripMenuItem.Tag = NotesTextBox
        EditUndoToolStripButton.Tag = NotesTextBox
        EditCutToolStripMenuItem.Tag = NotesTextBox
        EditCutToolStripButton.Tag = NotesTextBox
        EditCopyToolStripMenuItem.Tag = New TextBoxFocusedChecker(textBoxes)
        EditCopyToolStripButton.Tag = New TextBoxFocusedChecker(textBoxes)
        EditPasteToolStripMenuItem.Tag = NotesTextBox
        EditPasteToolStripButton.Tag = NotesTextBox
        EditSelectAllToolStripMenuItem.Tag = New TextBoxFocusedChecker(textBoxes)
    End Sub

    Private Sub AddHandlers()
        AddHandler MyBase.Load, AddressOf presenter.MainForm_Load
        AddHandler ToolStrip.VisibleChanged, AddressOf presenter.ToolStrip_VisibleChanged
        AddHandler FileAddToolStripMenuItem.Click, AddressOf presenter.FileAddToolStrip_Click
        AddHandler FileAddToolStripButton.Click, AddressOf presenter.FileAddToolStrip_Click
        AddHandler FileOpenToolStripMenuItem.Click, AddressOf presenter.FileOpenToolStrip_Click
        AddHandler FileOpenToolStripButton.Click, AddressOf presenter.FileOpenToolStrip_Click
        AddHandler FileSaveToolStripMenuItem.Click, AddressOf presenter.FileSaveToolStrip_Click
        AddHandler FileSaveToolStripButton.Click, AddressOf presenter.FileSaveToolStrip_Click
        AddHandler FileSaveAsToolStripMenuItem.Click, AddressOf presenter.FileSaveAsToolStrip_Click
        AddHandler FileBurstToolStripMenuItem.Click, AddressOf presenter.FileBurstToolStrip_Click
        AddHandler FileBurstToolStripButton.Click, AddressOf presenter.FileBurstToolStrip_Click
        AddHandler FilePrintToolStripMenuItem.Click, AddressOf presenter.FilePrintToolStrip_Click
        AddHandler FilePrintToolStripButton.Click, AddressOf presenter.FilePrintToolStrip_Click
        AddHandler FilePrintPreviewToolStripMenuItem.Click, AddressOf presenter.FilePrintPreviewToolStrip_Click
        AddHandler FileSelectAllToolStripMenuItem.Click, AddressOf presenter.FileSelectAllToolStrip_Click
        AddHandler FileSelectNoneToolStripMenuItem.Click, AddressOf presenter.FileSelectNoneToolStrip_Click
        AddHandler FileSetCategoryToolStripMenuItem.Click, AddressOf presenter.FileSetCategoryToolStrip_Click
        AddHandler FileSetTaxYearToolStripMenuItem.Click, AddressOf presenter.FileSetTaxYearToolStrip_Click
        AddHandler FileDeleteToolStripMenuItem.Click, AddressOf presenter.FileDeleteToolStrip_Click
        AddHandler FileDeleteToolStripButton.Click, AddressOf presenter.FileDeleteToolStrip_Click
        AddHandler FileExportToolStripMenuItem.Click, AddressOf presenter.FileExportToolStrip_Click
        AddHandler FileExitToolStripMenuItem.Click, AddressOf Close
        AddHandler EditUndoToolStripMenuItem.Click, AddressOf presenter.EditUndoToolStrip_Click
        AddHandler EditUndoToolStripButton.Click, AddressOf presenter.EditUndoToolStrip_Click
        AddHandler EditCutToolStripMenuItem.Click, AddressOf presenter.EditCutToolStrip_Click
        AddHandler EditCutToolStripButton.Click, AddressOf presenter.EditCutToolStrip_Click
        AddHandler EditCopyToolStripMenuItem.Click, AddressOf presenter.EditCopyToolStrip_Click
        AddHandler EditCopyToolStripButton.Click, AddressOf presenter.EditCopyToolStrip_Click
        AddHandler EditPasteToolStripMenuItem.Click, AddressOf presenter.EditPasteToolStrip_Click
        AddHandler EditPasteToolStripButton.Click, AddressOf presenter.EditPasteToolStrip_Click
        AddHandler EditSelectAllToolStripMenuItem.Click, AddressOf presenter.EditSelectAllToolStrip_Click
        AddHandler EditRestoreToolStripMenuItem.Click, AddressOf presenter.EditRestoreToolStrip_Click
        AddHandler EditRestoreToolStripButton.Click, AddressOf presenter.EditRestoreToolStrip_Click
        AddHandler EditDateTimeToolStripMenuItem.Click, AddressOf presenter.EditDateTimeToolStrip_Click
        AddHandler EditDateTimeToolStripButton.Click, AddressOf presenter.EditDateTimeToolStrip_Click
        AddHandler EditFlagDocumentToolStripMenuItem.Click, AddressOf presenter.EditFlagDocumentToolStrip_Click
        AddHandler ViewToolStripMenuItem.DropDownOpened, AddressOf presenter.ViewToolStripMenuItem_DropDownOpened
        AddHandler ViewRefreshToolStripMenuItem.Click, AddressOf presenter.ViewRefreshToolStrip_Click
        AddHandler ViewRefreshToolStripButton.Click, AddressOf presenter.ViewRefreshToolStrip_Click
        AddHandler ViewSetPreviewPixelDensityToolStripMenuItem.Click,
            AddressOf presenter.ViewSetPreviewPixelDensityToolStrip_Click
        AddHandler ViewToolBarToolStripMenuItem.Click, AddressOf presenter.ViewToolBarToolStrip_Click
        AddHandler ViewStatusBarToolStripMenuItem.Click, AddressOf presenter.ViewStatusBarToolStrip_Click
        AddHandler InsertTextToolStripMenuItem.Click, AddressOf presenter.InsertTextToolStrip_Click
        AddHandler InsertTextToolStripButton.Click, AddressOf presenter.InsertTextToolStrip_Click
        AddHandler ToolsOptionsToolStripButton.Click, AddressOf presenter.ToolsOptionsToolStrip_Click
        AddHandler ToolsOptionsToolStripMenuItem.Click, AddressOf presenter.ToolsOptionsToolStrip_Click
        AddHandler ToolsUploadProfilesToolStripMenuItem.Click, AddressOf presenter.ToolsUploadProfilesToolStrip_Click
        AddHandler ToolsUploadProfilesToolStripButton.Click, AddressOf presenter.ToolsUploadProfilesToolStrip_Click
        AddHandler ToolsUpdatePdfTextColumnsToolStripMenuItem.Click,
            AddressOf presenter.ToolsUpdatePdfTextColumnsToolStrip_Click
        AddHandler ToolsUpdatePdfTextColumnsToolStripButton.Click,
            AddressOf presenter.ToolsUpdatePdfTextColumnsToolStrip_Click
        AddHandler ToolsMoveDatabaseToolStripMenuItem.Click, AddressOf presenter.ToolsMoveDatabaseToolStrip_Click
        AddHandler ToolsRebuildFullTextSearchIndexToolStripMenuItem.Click,
            AddressOf presenter.ToolsRebuildFullTextSearchIndexToolStrip_Click
        AddHandler HelpContentsToolStripMenuItem.Click, AddressOf presenter.HelpContentsToolStrip_Click
        AddHandler HelpContentsToolStripButton.Click, AddressOf presenter.HelpContentsToolStrip_Click
        AddHandler HelpAboutToolStripMenuItem.Click, AddressOf presenter.HelpAboutToolStrip_Click
        AddHandler DocumentRetrievalChoicesListBox.SelectedIndexChanged,
            AddressOf presenter.DocumentRetrievalChoicesListBox_SelectedIndexChanged
        AddHandler SearchTermComboBox.Enter, AddressOf presenter.SearchTermComboBox_Enter
        AddHandler SearchTermComboBox.TextChanged, AddressOf presenter.SearchTermComboBox_TextChanged
        AddHandler FindBySearchTermButton.Click, AddressOf presenter.FindBySearchTermButton_Click
        AddHandler AuthorComboBox.Enter, AddressOf presenter.AuthorComboBox_Enter
        AddHandler SubjectComboBox.Enter, AddressOf presenter.SubjectComboBox_Enter
        AddHandler CategoryComboBox.Enter, AddressOf presenter.CategoryComboBox_Enter
        AddHandler TaxYearComboBox.Enter, AddressOf presenter.TaxYearComboBox_Enter
        AddHandler AuthorComboBox.DropDownClosed, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler SubjectComboBox.DropDownClosed, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler CategoryComboBox.DropDownClosed, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler TaxYearComboBox.DropDownClosed, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler AuthorComboBox.KeyUp, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler SubjectComboBox.KeyUp, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler CategoryComboBox.KeyUp, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler TaxYearComboBox.KeyUp, AddressOf presenter.ComboBox_DropDownClosed
        AddHandler AuthorComboBox.KeyDown, AddressOf presenter.ComboBox_KeyDown
        AddHandler SubjectComboBox.KeyDown, AddressOf presenter.ComboBox_KeyDown
        AddHandler CategoryComboBox.KeyDown, AddressOf presenter.ComboBox_KeyDown
        AddHandler TaxYearComboBox.KeyDown, AddressOf presenter.ComboBox_KeyDown
        AddHandler AuthorComboBox.MouseWheel, AddressOf presenter.ComboBox_MouseWheel
        AddHandler SubjectComboBox.MouseWheel, AddressOf presenter.ComboBox_MouseWheel
        AddHandler CategoryComboBox.MouseWheel, AddressOf presenter.ComboBox_MouseWheel
        AddHandler TaxYearComboBox.MouseWheel, AddressOf presenter.ComboBox_MouseWheel
        AddHandler ClearSelectionsButton.Click, AddressOf presenter.ClearSelectionsButton_Click
        AddHandler FindBySelectionsButton.Click, AddressOf presenter.FindBySelectionsButton_Click
        AddHandler DateAddedDateTimePicker.ValueChanged, AddressOf presenter.DateAddedDateTimePicker_ValueChanged
        AddHandler DocumentListDataGridView.DataSourceChanged,
            AddressOf presenter.DocumentListDataGridView_DataSourceChanged
        AddHandler DocumentListDataGridView.Sorted, AddressOf presenter.DocumentListDataGridView_Sorted
        AddHandler DocumentListDataGridView.RowsAdded, AddressOf presenter.DocumentListDataGridView_RowsAdded
        AddHandler DocumentListDataGridView.RowsRemoved, AddressOf presenter.DocumentListDataGridView_RowsRemoved
        AddHandler DocumentListDataGridView.SelectionChanged,
            AddressOf presenter.DocumentListDataGridView_SelectionChanged
        AddHandler DocumentListDataGridView.CurrentCellDirtyStateChanged,
            AddressOf presenter.DocumentListDataGridView_CurrentCellDirtyStateChanged
        AddHandler DocumentListDataGridView.CellValueChanged,
            AddressOf presenter.DocumentListDataGridView_CellValueChanged
        AddHandler DocumentListDataGridView.CellDoubleClick,
            AddressOf presenter.DocumentListDataGridView_CellDoubleClick
        AddHandler NotesTextBox.TextChanged, AddressOf presenter.NotesTextBox_TextChanged
        AddHandler NotesTextBox.Enter, AddressOf presenter.TextBox_Enter
        AddHandler NotesTextBox.GotFocus, AddressOf presenter.TextBox_Enter
        AddHandler KeywordsTextBox.Enter, AddressOf presenter.TextBox_Enter
        AddHandler TextTextBox.Enter, AddressOf presenter.TextBox_Enter
        AddHandler SearchTermSnippetsTextBox.Enter, AddressOf presenter.TextBox_Enter
        AddHandler NotesTextBox.KeyPress, AddressOf presenter.TextBox_KeyPress
        AddHandler KeywordsTextBox.KeyPress, AddressOf presenter.TextBox_KeyPress
        AddHandler TextTextBox.KeyPress, AddressOf presenter.TextBox_KeyPress
        AddHandler SearchTermSnippetsTextBox.KeyPress, AddressOf presenter.TextBox_KeyPress
        AddHandler NotesTextBox.MouseUp, AddressOf presenter.TextBox_MouseUp
        AddHandler KeywordsTextBox.MouseUp, AddressOf presenter.TextBox_MouseUp
        AddHandler TextTextBox.MouseUp, AddressOf presenter.TextBox_MouseUp
        AddHandler SearchTermSnippetsTextBox.MouseUp, AddressOf presenter.TextBox_MouseUp
        AddHandler NotesTextBox.Leave, AddressOf presenter.TextBox_Leave
        AddHandler KeywordsTextBox.Leave, AddressOf presenter.TextBox_Leave
        AddHandler TextTextBox.Leave, AddressOf presenter.TextBox_Leave
        AddHandler SearchTermSnippetsTextBox.Leave, AddressOf presenter.TextBox_Leave
        AddHandler PreviewPictureBox.DoubleClick, AddressOf presenter.PreviewPictureBox_DoubleClick
        AddHandler StatusStrip.VisibleChanged, AddressOf presenter.StatusStrip_VisibleChanged
        AddHandler UploadRejectedImageToolStripStatusLabel.Click,
            AddressOf presenter.UploadRejectedImageToolStripStatusLabel_Click
        AddHandler PrintDocument.PrintPage, AddressOf presenter.PrintDocument_PrintPage
        AddHandler UpdateCheckTimer.Tick, AddressOf presenter.UpdateCheckTimer_Tick
        AddHandler FlaggedDocumentCheckTimer.Tick, AddressOf presenter.FlaggedDocumentCheckTimer_Tick
        AddHandler UploadFolderMaintenanceTimer.Tick, AddressOf presenter.UploadFolderMaintenanceTimer_Tick
        AddHandler UploadTimer.Tick, AddressOf presenter.UploadTimer_Tick
        AddHandler UploadRejectedFilesCheckTimer.Tick, AddressOf presenter.UploadRejectedFilesCheckTimer_Tick
        AddHandler MyBase.FormClosing, AddressOf presenter.MainForm_FormClosing
    End Sub
End Class
