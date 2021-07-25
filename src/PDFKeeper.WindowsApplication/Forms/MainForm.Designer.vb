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
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                commonPresenter.Dispose()
                presenter.Dispose()
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileAddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileOpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileSaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.FilePrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilePrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileSelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSelectNoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSetCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSetTaxYearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileDeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditUndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditCutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditCopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditPasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditSelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditRestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditDateTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditFlagDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewRefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewSetPreviewImageResolutionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewToolbarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewStatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertTextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsMoveDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolsManageUploadFolderConfigurationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpAboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchTextErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.SelectedDocumentTabControl = New System.Windows.Forms.TabControl()
        Me.NotesTabPage = New System.Windows.Forms.TabPage()
        Me.NotesTextBox = New System.Windows.Forms.TextBox()
        Me.KeywordsTabPage = New System.Windows.Forms.TabPage()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.PreviewTabPage = New System.Windows.Forms.TabPage()
        Me.PreviewPanel = New System.Windows.Forms.Panel()
        Me.PreviewPictureBox = New System.Windows.Forms.PictureBox()
        Me.TextTabPage = New System.Windows.Forms.TabPage()
        Me.TextTextBox = New System.Windows.Forms.TextBox()
        Me.SearchGroupBox = New System.Windows.Forms.GroupBox()
        Me.TaxYearGroupComboBox = New System.Windows.Forms.ComboBox()
        Me.TaxYearLabel = New System.Windows.Forms.Label()
        Me.DateLabel = New System.Windows.Forms.Label()
        Me.TextLabel = New System.Windows.Forms.Label()
        Me.SearchFunctionsListBox = New System.Windows.Forms.ListBox()
        Me.SearchBySelectionsButton = New System.Windows.Forms.Button()
        Me.SearchByTextButton = New System.Windows.Forms.Button()
        Me.SearchDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.ClearSelectionsButton = New System.Windows.Forms.Button()
        Me.SubjectGroupComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryGroupComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryLabel = New System.Windows.Forms.Label()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.AuthorGroupComboBox = New System.Windows.Forms.ComboBox()
        Me.AuthorLabel = New System.Windows.Forms.Label()
        Me.SearchTextComboBox = New System.Windows.Forms.ComboBox()
        Me.SearchResultsPanel = New System.Windows.Forms.Panel()
        Me.SearchResultsDataGridView = New System.Windows.Forms.DataGridView()
        Me.SelectionColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UploadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.TotalRecordsToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TotalRecordsCountToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SelectedDocumentsProcessToolStripProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.FillerToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadRunningToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadFolderErrorToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadStagingFolderErrorToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FlaggedDocumentsExistToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.FileAddToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileOpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileSaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FilePrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileDeleteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditUndoToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditCutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditCopyToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditPasteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditRestoreToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditDateTimeToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewRefreshToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.InsertTextToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolsOptionsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolsManageUploadFolderConfigurationsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpContentsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AutoUpdateCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FlaggedDocumentsCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip.SuspendLayout()
        CType(Me.SearchTextErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SelectedDocumentTabControl.SuspendLayout()
        Me.NotesTabPage.SuspendLayout()
        Me.KeywordsTabPage.SuspendLayout()
        Me.PreviewTabPage.SuspendLayout()
        Me.PreviewPanel.SuspendLayout()
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TextTabPage.SuspendLayout()
        Me.SearchGroupBox.SuspendLayout()
        Me.SearchResultsPanel.SuspendLayout()
        CType(Me.SearchResultsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.InsertToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip, "MenuStrip")
        Me.MenuStrip.Name = "MenuStrip"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAddToolStripMenuItem, Me.ToolStripSeparator1, Me.FileOpenToolStripMenuItem, Me.ToolStripSeparator2, Me.FileSaveToolStripMenuItem, Me.FileSaveAsToolStripMenuItem, Me.ToolStripSeparator4, Me.FilePrintToolStripMenuItem, Me.FilePrintPreviewToolStripMenuItem, Me.ToolStripSeparator3, Me.FileSelectToolStripMenuItem, Me.FileSetCategoryToolStripMenuItem, Me.FileSetTaxYearToolStripMenuItem, Me.FileDeleteToolStripMenuItem, Me.FileExportToolStripMenuItem, Me.ToolStripSeparator6, Me.FileExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'FileAddToolStripMenuItem
        '
        resources.ApplyResources(Me.FileAddToolStripMenuItem, "FileAddToolStripMenuItem")
        Me.FileAddToolStripMenuItem.Name = "FileAddToolStripMenuItem"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'FileOpenToolStripMenuItem
        '
        resources.ApplyResources(Me.FileOpenToolStripMenuItem, "FileOpenToolStripMenuItem")
        Me.FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'FileSaveToolStripMenuItem
        '
        resources.ApplyResources(Me.FileSaveToolStripMenuItem, "FileSaveToolStripMenuItem")
        Me.FileSaveToolStripMenuItem.Name = "FileSaveToolStripMenuItem"
        '
        'FileSaveAsToolStripMenuItem
        '
        Me.FileSaveAsToolStripMenuItem.Name = "FileSaveAsToolStripMenuItem"
        resources.ApplyResources(Me.FileSaveAsToolStripMenuItem, "FileSaveAsToolStripMenuItem")
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'FilePrintToolStripMenuItem
        '
        resources.ApplyResources(Me.FilePrintToolStripMenuItem, "FilePrintToolStripMenuItem")
        Me.FilePrintToolStripMenuItem.Name = "FilePrintToolStripMenuItem"
        '
        'FilePrintPreviewToolStripMenuItem
        '
        Me.FilePrintPreviewToolStripMenuItem.Name = "FilePrintPreviewToolStripMenuItem"
        resources.ApplyResources(Me.FilePrintPreviewToolStripMenuItem, "FilePrintPreviewToolStripMenuItem")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'FileSelectToolStripMenuItem
        '
        Me.FileSelectToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileSelectAllToolStripMenuItem, Me.FileSelectNoneToolStripMenuItem})
        Me.FileSelectToolStripMenuItem.Name = "FileSelectToolStripMenuItem"
        resources.ApplyResources(Me.FileSelectToolStripMenuItem, "FileSelectToolStripMenuItem")
        '
        'FileSelectAllToolStripMenuItem
        '
        Me.FileSelectAllToolStripMenuItem.Name = "FileSelectAllToolStripMenuItem"
        resources.ApplyResources(Me.FileSelectAllToolStripMenuItem, "FileSelectAllToolStripMenuItem")
        '
        'FileSelectNoneToolStripMenuItem
        '
        Me.FileSelectNoneToolStripMenuItem.Name = "FileSelectNoneToolStripMenuItem"
        resources.ApplyResources(Me.FileSelectNoneToolStripMenuItem, "FileSelectNoneToolStripMenuItem")
        '
        'FileSetCategoryToolStripMenuItem
        '
        Me.FileSetCategoryToolStripMenuItem.Name = "FileSetCategoryToolStripMenuItem"
        resources.ApplyResources(Me.FileSetCategoryToolStripMenuItem, "FileSetCategoryToolStripMenuItem")
        '
        'FileSetTaxYearToolStripMenuItem
        '
        Me.FileSetTaxYearToolStripMenuItem.Name = "FileSetTaxYearToolStripMenuItem"
        resources.ApplyResources(Me.FileSetTaxYearToolStripMenuItem, "FileSetTaxYearToolStripMenuItem")
        '
        'FileDeleteToolStripMenuItem
        '
        resources.ApplyResources(Me.FileDeleteToolStripMenuItem, "FileDeleteToolStripMenuItem")
        Me.FileDeleteToolStripMenuItem.Name = "FileDeleteToolStripMenuItem"
        '
        'FileExportToolStripMenuItem
        '
        Me.FileExportToolStripMenuItem.Name = "FileExportToolStripMenuItem"
        resources.ApplyResources(Me.FileExportToolStripMenuItem, "FileExportToolStripMenuItem")
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        resources.ApplyResources(Me.ToolStripSeparator6, "ToolStripSeparator6")
        '
        'FileExitToolStripMenuItem
        '
        Me.FileExitToolStripMenuItem.Name = "FileExitToolStripMenuItem"
        resources.ApplyResources(Me.FileExitToolStripMenuItem, "FileExitToolStripMenuItem")
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditUndoToolStripMenuItem, Me.ToolStripSeparator7, Me.EditCutToolStripMenuItem, Me.EditCopyToolStripMenuItem, Me.EditPasteToolStripMenuItem, Me.ToolStripSeparator8, Me.EditSelectAllToolStripMenuItem, Me.ToolStripSeparator9, Me.EditRestoreToolStripMenuItem, Me.ToolStripSeparator5, Me.EditDateTimeToolStripMenuItem, Me.ToolStripSeparator13, Me.EditFlagDocumentToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        '
        'EditUndoToolStripMenuItem
        '
        resources.ApplyResources(Me.EditUndoToolStripMenuItem, "EditUndoToolStripMenuItem")
        Me.EditUndoToolStripMenuItem.Name = "EditUndoToolStripMenuItem"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        '
        'EditCutToolStripMenuItem
        '
        resources.ApplyResources(Me.EditCutToolStripMenuItem, "EditCutToolStripMenuItem")
        Me.EditCutToolStripMenuItem.Name = "EditCutToolStripMenuItem"
        '
        'EditCopyToolStripMenuItem
        '
        resources.ApplyResources(Me.EditCopyToolStripMenuItem, "EditCopyToolStripMenuItem")
        Me.EditCopyToolStripMenuItem.Name = "EditCopyToolStripMenuItem"
        '
        'EditPasteToolStripMenuItem
        '
        resources.ApplyResources(Me.EditPasteToolStripMenuItem, "EditPasteToolStripMenuItem")
        Me.EditPasteToolStripMenuItem.Name = "EditPasteToolStripMenuItem"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        resources.ApplyResources(Me.ToolStripSeparator8, "ToolStripSeparator8")
        '
        'EditSelectAllToolStripMenuItem
        '
        Me.EditSelectAllToolStripMenuItem.Name = "EditSelectAllToolStripMenuItem"
        resources.ApplyResources(Me.EditSelectAllToolStripMenuItem, "EditSelectAllToolStripMenuItem")
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        resources.ApplyResources(Me.ToolStripSeparator9, "ToolStripSeparator9")
        '
        'EditRestoreToolStripMenuItem
        '
        resources.ApplyResources(Me.EditRestoreToolStripMenuItem, "EditRestoreToolStripMenuItem")
        Me.EditRestoreToolStripMenuItem.Name = "EditRestoreToolStripMenuItem"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        '
        'EditDateTimeToolStripMenuItem
        '
        resources.ApplyResources(Me.EditDateTimeToolStripMenuItem, "EditDateTimeToolStripMenuItem")
        Me.EditDateTimeToolStripMenuItem.Name = "EditDateTimeToolStripMenuItem"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        resources.ApplyResources(Me.ToolStripSeparator13, "ToolStripSeparator13")
        '
        'EditFlagDocumentToolStripMenuItem
        '
        Me.EditFlagDocumentToolStripMenuItem.CheckOnClick = True
        Me.EditFlagDocumentToolStripMenuItem.Name = "EditFlagDocumentToolStripMenuItem"
        resources.ApplyResources(Me.EditFlagDocumentToolStripMenuItem, "EditFlagDocumentToolStripMenuItem")
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewRefreshToolStripMenuItem, Me.ToolStripSeparator24, Me.ViewSetPreviewImageResolutionToolStripMenuItem, Me.ToolStripSeparator10, Me.ViewToolbarToolStripMenuItem, Me.ViewStatusBarToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        resources.ApplyResources(Me.ViewToolStripMenuItem, "ViewToolStripMenuItem")
        '
        'ViewRefreshToolStripMenuItem
        '
        resources.ApplyResources(Me.ViewRefreshToolStripMenuItem, "ViewRefreshToolStripMenuItem")
        Me.ViewRefreshToolStripMenuItem.Name = "ViewRefreshToolStripMenuItem"
        '
        'ToolStripSeparator24
        '
        Me.ToolStripSeparator24.Name = "ToolStripSeparator24"
        resources.ApplyResources(Me.ToolStripSeparator24, "ToolStripSeparator24")
        '
        'ViewSetPreviewImageResolutionToolStripMenuItem
        '
        Me.ViewSetPreviewImageResolutionToolStripMenuItem.Name = "ViewSetPreviewImageResolutionToolStripMenuItem"
        resources.ApplyResources(Me.ViewSetPreviewImageResolutionToolStripMenuItem, "ViewSetPreviewImageResolutionToolStripMenuItem")
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        resources.ApplyResources(Me.ToolStripSeparator10, "ToolStripSeparator10")
        '
        'ViewToolbarToolStripMenuItem
        '
        Me.ViewToolbarToolStripMenuItem.Checked = True
        Me.ViewToolbarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ViewToolbarToolStripMenuItem.Name = "ViewToolbarToolStripMenuItem"
        resources.ApplyResources(Me.ViewToolbarToolStripMenuItem, "ViewToolbarToolStripMenuItem")
        '
        'ViewStatusBarToolStripMenuItem
        '
        Me.ViewStatusBarToolStripMenuItem.Checked = True
        Me.ViewStatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ViewStatusBarToolStripMenuItem.Name = "ViewStatusBarToolStripMenuItem"
        resources.ApplyResources(Me.ViewStatusBarToolStripMenuItem, "ViewStatusBarToolStripMenuItem")
        '
        'InsertToolStripMenuItem
        '
        Me.InsertToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InsertTextToolStripMenuItem})
        Me.InsertToolStripMenuItem.Name = "InsertToolStripMenuItem"
        resources.ApplyResources(Me.InsertToolStripMenuItem, "InsertToolStripMenuItem")
        '
        'InsertTextToolStripMenuItem
        '
        Me.InsertTextToolStripMenuItem.Image = Global.PDFKeeper.WindowsApplication.My.Resources.Resources.page_text
        Me.InsertTextToolStripMenuItem.Name = "InsertTextToolStripMenuItem"
        resources.ApplyResources(Me.InsertTextToolStripMenuItem, "InsertTextToolStripMenuItem")
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsOptionsToolStripMenuItem, Me.ToolsManageUploadFolderConfigurationsToolStripMenuItem, Me.ToolStripSeparator11, Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem, Me.ToolsMoveDatabaseToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        '
        'ToolsOptionsToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsOptionsToolStripMenuItem, "ToolsOptionsToolStripMenuItem")
        Me.ToolsOptionsToolStripMenuItem.Name = "ToolsOptionsToolStripMenuItem"
        '
        'ToolsMoveDatabaseToolStripMenuItem
        '
        Me.ToolsMoveDatabaseToolStripMenuItem.Image = Global.PDFKeeper.WindowsApplication.My.Resources.Resources.database
        Me.ToolsMoveDatabaseToolStripMenuItem.Name = "ToolsMoveDatabaseToolStripMenuItem"
        resources.ApplyResources(Me.ToolsMoveDatabaseToolStripMenuItem, "ToolsMoveDatabaseToolStripMenuItem")
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        resources.ApplyResources(Me.ToolStripSeparator11, "ToolStripSeparator11")
        '
        'ToolsManageUploadFolderConfigurationsToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsManageUploadFolderConfigurationsToolStripMenuItem, "ToolsManageUploadFolderConfigurationsToolStripMenuItem")
        Me.ToolsManageUploadFolderConfigurationsToolStripMenuItem.Name = "ToolsManageUploadFolderConfigurationsToolStripMenuItem"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpContentsToolStripMenuItem, Me.ToolStripSeparator12, Me.HelpAboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'HelpContentsToolStripMenuItem
        '
        resources.ApplyResources(Me.HelpContentsToolStripMenuItem, "HelpContentsToolStripMenuItem")
        Me.HelpContentsToolStripMenuItem.Name = "HelpContentsToolStripMenuItem"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        resources.ApplyResources(Me.ToolStripSeparator12, "ToolStripSeparator12")
        '
        'HelpAboutToolStripMenuItem
        '
        Me.HelpAboutToolStripMenuItem.Name = "HelpAboutToolStripMenuItem"
        resources.ApplyResources(Me.HelpAboutToolStripMenuItem, "HelpAboutToolStripMenuItem")
        '
        'SearchTextErrorProvider
        '
        Me.SearchTextErrorProvider.ContainerControl = Me
        '
        'SplitContainer
        '
        Me.SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.SplitContainer, "SplitContainer")
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.SelectedDocumentTabControl)
        Me.SplitContainer.Panel1.Controls.Add(Me.SearchGroupBox)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.SearchResultsPanel)
        Me.SplitContainer.TabStop = False
        '
        'SelectedDocumentTabControl
        '
        Me.SelectedDocumentTabControl.Controls.Add(Me.NotesTabPage)
        Me.SelectedDocumentTabControl.Controls.Add(Me.KeywordsTabPage)
        Me.SelectedDocumentTabControl.Controls.Add(Me.PreviewTabPage)
        Me.SelectedDocumentTabControl.Controls.Add(Me.TextTabPage)
        resources.ApplyResources(Me.SelectedDocumentTabControl, "SelectedDocumentTabControl")
        Me.SelectedDocumentTabControl.Name = "SelectedDocumentTabControl"
        Me.SelectedDocumentTabControl.SelectedIndex = 0
        '
        'NotesTabPage
        '
        Me.NotesTabPage.Controls.Add(Me.NotesTextBox)
        resources.ApplyResources(Me.NotesTabPage, "NotesTabPage")
        Me.NotesTabPage.Name = "NotesTabPage"
        Me.NotesTabPage.UseVisualStyleBackColor = True
        '
        'NotesTextBox
        '
        Me.NotesTextBox.AcceptsReturn = True
        resources.ApplyResources(Me.NotesTextBox, "NotesTextBox")
        Me.NotesTextBox.Name = "NotesTextBox"
        '
        'KeywordsTabPage
        '
        Me.KeywordsTabPage.Controls.Add(Me.KeywordsTextBox)
        resources.ApplyResources(Me.KeywordsTabPage, "KeywordsTabPage")
        Me.KeywordsTabPage.Name = "KeywordsTabPage"
        Me.KeywordsTabPage.UseVisualStyleBackColor = True
        '
        'KeywordsTextBox
        '
        resources.ApplyResources(Me.KeywordsTextBox, "KeywordsTextBox")
        Me.KeywordsTextBox.Name = "KeywordsTextBox"
        Me.KeywordsTextBox.ReadOnly = True
        '
        'PreviewTabPage
        '
        Me.PreviewTabPage.Controls.Add(Me.PreviewPanel)
        resources.ApplyResources(Me.PreviewTabPage, "PreviewTabPage")
        Me.PreviewTabPage.Name = "PreviewTabPage"
        Me.PreviewTabPage.UseVisualStyleBackColor = True
        '
        'PreviewPanel
        '
        resources.ApplyResources(Me.PreviewPanel, "PreviewPanel")
        Me.PreviewPanel.Controls.Add(Me.PreviewPictureBox)
        Me.PreviewPanel.Name = "PreviewPanel"
        '
        'PreviewPictureBox
        '
        resources.ApplyResources(Me.PreviewPictureBox, "PreviewPictureBox")
        Me.PreviewPictureBox.Name = "PreviewPictureBox"
        Me.PreviewPictureBox.TabStop = False
        '
        'TextTabPage
        '
        Me.TextTabPage.Controls.Add(Me.TextTextBox)
        resources.ApplyResources(Me.TextTabPage, "TextTabPage")
        Me.TextTabPage.Name = "TextTabPage"
        Me.TextTabPage.UseVisualStyleBackColor = True
        '
        'TextTextBox
        '
        resources.ApplyResources(Me.TextTextBox, "TextTextBox")
        Me.TextTextBox.Name = "TextTextBox"
        Me.TextTextBox.ReadOnly = True
        '
        'SearchGroupBox
        '
        Me.SearchGroupBox.Controls.Add(Me.TaxYearGroupComboBox)
        Me.SearchGroupBox.Controls.Add(Me.TaxYearLabel)
        Me.SearchGroupBox.Controls.Add(Me.DateLabel)
        Me.SearchGroupBox.Controls.Add(Me.TextLabel)
        Me.SearchGroupBox.Controls.Add(Me.SearchFunctionsListBox)
        Me.SearchGroupBox.Controls.Add(Me.SearchBySelectionsButton)
        Me.SearchGroupBox.Controls.Add(Me.SearchByTextButton)
        Me.SearchGroupBox.Controls.Add(Me.SearchDateTimePicker)
        Me.SearchGroupBox.Controls.Add(Me.ClearSelectionsButton)
        Me.SearchGroupBox.Controls.Add(Me.SubjectGroupComboBox)
        Me.SearchGroupBox.Controls.Add(Me.CategoryGroupComboBox)
        Me.SearchGroupBox.Controls.Add(Me.CategoryLabel)
        Me.SearchGroupBox.Controls.Add(Me.SubjectLabel)
        Me.SearchGroupBox.Controls.Add(Me.AuthorGroupComboBox)
        Me.SearchGroupBox.Controls.Add(Me.AuthorLabel)
        Me.SearchGroupBox.Controls.Add(Me.SearchTextComboBox)
        resources.ApplyResources(Me.SearchGroupBox, "SearchGroupBox")
        Me.SearchGroupBox.Name = "SearchGroupBox"
        Me.SearchGroupBox.TabStop = False
        '
        'TaxYearGroupComboBox
        '
        resources.ApplyResources(Me.TaxYearGroupComboBox, "TaxYearGroupComboBox")
        Me.TaxYearGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TaxYearGroupComboBox.FormattingEnabled = True
        Me.TaxYearGroupComboBox.Name = "TaxYearGroupComboBox"
        Me.TaxYearGroupComboBox.Sorted = True
        '
        'TaxYearLabel
        '
        resources.ApplyResources(Me.TaxYearLabel, "TaxYearLabel")
        Me.TaxYearLabel.Name = "TaxYearLabel"
        '
        'DateLabel
        '
        resources.ApplyResources(Me.DateLabel, "DateLabel")
        Me.DateLabel.Name = "DateLabel"
        '
        'TextLabel
        '
        resources.ApplyResources(Me.TextLabel, "TextLabel")
        Me.TextLabel.Name = "TextLabel"
        '
        'SearchFunctionsListBox
        '
        resources.ApplyResources(Me.SearchFunctionsListBox, "SearchFunctionsListBox")
        Me.SearchFunctionsListBox.FormattingEnabled = True
        Me.SearchFunctionsListBox.Items.AddRange(New Object() {resources.GetString("SearchFunctionsListBox.Items"), resources.GetString("SearchFunctionsListBox.Items1"), resources.GetString("SearchFunctionsListBox.Items2"), resources.GetString("SearchFunctionsListBox.Items3"), resources.GetString("SearchFunctionsListBox.Items4")})
        Me.SearchFunctionsListBox.Name = "SearchFunctionsListBox"
        '
        'SearchBySelectionsButton
        '
        resources.ApplyResources(Me.SearchBySelectionsButton, "SearchBySelectionsButton")
        Me.SearchBySelectionsButton.Name = "SearchBySelectionsButton"
        Me.SearchBySelectionsButton.UseVisualStyleBackColor = True
        '
        'SearchByTextButton
        '
        resources.ApplyResources(Me.SearchByTextButton, "SearchByTextButton")
        Me.SearchByTextButton.Name = "SearchByTextButton"
        Me.SearchByTextButton.UseVisualStyleBackColor = True
        '
        'SearchDateTimePicker
        '
        resources.ApplyResources(Me.SearchDateTimePicker, "SearchDateTimePicker")
        Me.SearchDateTimePicker.Name = "SearchDateTimePicker"
        '
        'ClearSelectionsButton
        '
        resources.ApplyResources(Me.ClearSelectionsButton, "ClearSelectionsButton")
        Me.ClearSelectionsButton.Name = "ClearSelectionsButton"
        Me.ClearSelectionsButton.UseVisualStyleBackColor = True
        '
        'SubjectGroupComboBox
        '
        resources.ApplyResources(Me.SubjectGroupComboBox, "SubjectGroupComboBox")
        Me.SubjectGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SubjectGroupComboBox.FormattingEnabled = True
        Me.SubjectGroupComboBox.Name = "SubjectGroupComboBox"
        Me.SubjectGroupComboBox.Sorted = True
        '
        'CategoryGroupComboBox
        '
        resources.ApplyResources(Me.CategoryGroupComboBox, "CategoryGroupComboBox")
        Me.CategoryGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CategoryGroupComboBox.FormattingEnabled = True
        Me.CategoryGroupComboBox.Name = "CategoryGroupComboBox"
        Me.CategoryGroupComboBox.Sorted = True
        '
        'CategoryLabel
        '
        resources.ApplyResources(Me.CategoryLabel, "CategoryLabel")
        Me.CategoryLabel.Name = "CategoryLabel"
        '
        'SubjectLabel
        '
        resources.ApplyResources(Me.SubjectLabel, "SubjectLabel")
        Me.SubjectLabel.Name = "SubjectLabel"
        '
        'AuthorGroupComboBox
        '
        resources.ApplyResources(Me.AuthorGroupComboBox, "AuthorGroupComboBox")
        Me.AuthorGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AuthorGroupComboBox.FormattingEnabled = True
        Me.AuthorGroupComboBox.Name = "AuthorGroupComboBox"
        Me.AuthorGroupComboBox.Sorted = True
        '
        'AuthorLabel
        '
        resources.ApplyResources(Me.AuthorLabel, "AuthorLabel")
        Me.AuthorLabel.Name = "AuthorLabel"
        '
        'SearchTextComboBox
        '
        resources.ApplyResources(Me.SearchTextComboBox, "SearchTextComboBox")
        Me.SearchTextComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SearchTextComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SearchTextComboBox.FormattingEnabled = True
        Me.SearchTextComboBox.Name = "SearchTextComboBox"
        Me.SearchTextComboBox.Sorted = True
        '
        'SearchResultsPanel
        '
        Me.SearchResultsPanel.Controls.Add(Me.SearchResultsDataGridView)
        resources.ApplyResources(Me.SearchResultsPanel, "SearchResultsPanel")
        Me.SearchResultsPanel.Name = "SearchResultsPanel"
        '
        'SearchResultsDataGridView
        '
        Me.SearchResultsDataGridView.AllowUserToAddRows = False
        Me.SearchResultsDataGridView.AllowUserToDeleteRows = False
        Me.SearchResultsDataGridView.AllowUserToResizeRows = False
        Me.SearchResultsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.SearchResultsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window
        Me.SearchResultsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SearchResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SearchResultsDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelectionColumn})
        resources.ApplyResources(Me.SearchResultsDataGridView, "SearchResultsDataGridView")
        Me.SearchResultsDataGridView.MultiSelect = False
        Me.SearchResultsDataGridView.Name = "SearchResultsDataGridView"
        Me.SearchResultsDataGridView.RowHeadersVisible = False
        Me.SearchResultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SearchResultsDataGridView.StandardTab = True
        '
        'SelectionColumn
        '
        Me.SelectionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        resources.ApplyResources(Me.SelectionColumn, "SelectionColumn")
        Me.SelectionColumn.Name = "SelectionColumn"
        '
        'UploadTimer
        '
        Me.UploadTimer.Enabled = True
        Me.UploadTimer.Interval = 5000
        '
        'StatusStrip
        '
        Me.StatusStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "MainStatusBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TotalRecordsToolStripStatusLabel, Me.TotalRecordsCountToolStripStatusLabel, Me.SelectedDocumentsProcessToolStripProgressBar, Me.FillerToolStripStatusLabel, Me.UploadRunningToolStripStatusLabel, Me.UploadFolderErrorToolStripStatusLabel, Me.UploadStagingFolderErrorToolStripStatusLabel, Me.FlaggedDocumentsExistToolStripStatusLabel})
        resources.ApplyResources(Me.StatusStrip, "StatusStrip")
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.ShowItemToolTips = True
        Me.StatusStrip.Visible = Global.PDFKeeper.WindowsApplication.My.MySettings.Default.MainStatusBarVisible
        '
        'TotalRecordsToolStripStatusLabel
        '
        Me.TotalRecordsToolStripStatusLabel.Name = "TotalRecordsToolStripStatusLabel"
        resources.ApplyResources(Me.TotalRecordsToolStripStatusLabel, "TotalRecordsToolStripStatusLabel")
        '
        'TotalRecordsCountToolStripStatusLabel
        '
        Me.TotalRecordsCountToolStripStatusLabel.Name = "TotalRecordsCountToolStripStatusLabel"
        resources.ApplyResources(Me.TotalRecordsCountToolStripStatusLabel, "TotalRecordsCountToolStripStatusLabel")
        '
        'SelectedDocumentsProcessToolStripProgressBar
        '
        Me.SelectedDocumentsProcessToolStripProgressBar.Name = "SelectedDocumentsProcessToolStripProgressBar"
        resources.ApplyResources(Me.SelectedDocumentsProcessToolStripProgressBar, "SelectedDocumentsProcessToolStripProgressBar")
        Me.SelectedDocumentsProcessToolStripProgressBar.Step = 1
        '
        'FillerToolStripStatusLabel
        '
        resources.ApplyResources(Me.FillerToolStripStatusLabel, "FillerToolStripStatusLabel")
        Me.FillerToolStripStatusLabel.Name = "FillerToolStripStatusLabel"
        Me.FillerToolStripStatusLabel.Spring = True
        '
        'UploadRunningToolStripStatusLabel
        '
        resources.ApplyResources(Me.UploadRunningToolStripStatusLabel, "UploadRunningToolStripStatusLabel")
        Me.UploadRunningToolStripStatusLabel.Name = "UploadRunningToolStripStatusLabel"
        '
        'UploadFolderErrorToolStripStatusLabel
        '
        resources.ApplyResources(Me.UploadFolderErrorToolStripStatusLabel, "UploadFolderErrorToolStripStatusLabel")
        Me.UploadFolderErrorToolStripStatusLabel.Name = "UploadFolderErrorToolStripStatusLabel"
        '
        'UploadStagingFolderErrorToolStripStatusLabel
        '
        resources.ApplyResources(Me.UploadStagingFolderErrorToolStripStatusLabel, "UploadStagingFolderErrorToolStripStatusLabel")
        Me.UploadStagingFolderErrorToolStripStatusLabel.Name = "UploadStagingFolderErrorToolStripStatusLabel"
        '
        'FlaggedDocumentsExistToolStripStatusLabel
        '
        resources.ApplyResources(Me.FlaggedDocumentsExistToolStripStatusLabel, "FlaggedDocumentsExistToolStripStatusLabel")
        Me.FlaggedDocumentsExistToolStripStatusLabel.Name = "FlaggedDocumentsExistToolStripStatusLabel"
        '
        'ToolStrip
        '
        Me.ToolStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "MainToolBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAddToolStripButton, Me.FileOpenToolStripButton, Me.FileSaveToolStripButton, Me.FilePrintToolStripButton, Me.FileDeleteToolStripButton, Me.ToolStripSeparator17, Me.EditUndoToolStripButton, Me.EditCutToolStripButton, Me.EditCopyToolStripButton, Me.EditPasteToolStripButton, Me.EditRestoreToolStripButton, Me.EditDateTimeToolStripButton, Me.ToolStripSeparator20, Me.ViewRefreshToolStripButton, Me.ToolStripSeparator14, Me.InsertTextToolStripButton, Me.ToolStripSeparator21, Me.ToolsOptionsToolStripButton, Me.ToolsManageUploadFolderConfigurationsToolStripButton, Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton, Me.ToolStripSeparator22, Me.HelpContentsToolStripButton})
        resources.ApplyResources(Me.ToolStrip, "ToolStrip")
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Visible = Global.PDFKeeper.WindowsApplication.My.MySettings.Default.MainToolBarVisible
        '
        'FileAddToolStripButton
        '
        Me.FileAddToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.FileAddToolStripButton, "FileAddToolStripButton")
        Me.FileAddToolStripButton.Name = "FileAddToolStripButton"
        '
        'FileOpenToolStripButton
        '
        Me.FileOpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.FileOpenToolStripButton, "FileOpenToolStripButton")
        Me.FileOpenToolStripButton.Name = "FileOpenToolStripButton"
        '
        'FileSaveToolStripButton
        '
        Me.FileSaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.FileSaveToolStripButton, "FileSaveToolStripButton")
        Me.FileSaveToolStripButton.Name = "FileSaveToolStripButton"
        '
        'FilePrintToolStripButton
        '
        Me.FilePrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.FilePrintToolStripButton, "FilePrintToolStripButton")
        Me.FilePrintToolStripButton.Name = "FilePrintToolStripButton"
        '
        'FileDeleteToolStripButton
        '
        Me.FileDeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.FileDeleteToolStripButton, "FileDeleteToolStripButton")
        Me.FileDeleteToolStripButton.Name = "FileDeleteToolStripButton"
        '
        'ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton
        '
        Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton.Image = Global.PDFKeeper.WindowsApplication.My.Resources.Resources.table
        resources.ApplyResources(Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton, "ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton")
        Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton.Name = "ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        resources.ApplyResources(Me.ToolStripSeparator17, "ToolStripSeparator17")
        '
        'EditUndoToolStripButton
        '
        Me.EditUndoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EditUndoToolStripButton, "EditUndoToolStripButton")
        Me.EditUndoToolStripButton.Name = "EditUndoToolStripButton"
        '
        'EditCutToolStripButton
        '
        Me.EditCutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EditCutToolStripButton, "EditCutToolStripButton")
        Me.EditCutToolStripButton.Name = "EditCutToolStripButton"
        '
        'EditCopyToolStripButton
        '
        Me.EditCopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EditCopyToolStripButton, "EditCopyToolStripButton")
        Me.EditCopyToolStripButton.Name = "EditCopyToolStripButton"
        '
        'EditPasteToolStripButton
        '
        Me.EditPasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EditPasteToolStripButton, "EditPasteToolStripButton")
        Me.EditPasteToolStripButton.Name = "EditPasteToolStripButton"
        '
        'EditRestoreToolStripButton
        '
        Me.EditRestoreToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EditRestoreToolStripButton, "EditRestoreToolStripButton")
        Me.EditRestoreToolStripButton.Name = "EditRestoreToolStripButton"
        '
        'EditDateTimeToolStripButton
        '
        Me.EditDateTimeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EditDateTimeToolStripButton, "EditDateTimeToolStripButton")
        Me.EditDateTimeToolStripButton.Name = "EditDateTimeToolStripButton"
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        resources.ApplyResources(Me.ToolStripSeparator20, "ToolStripSeparator20")
        '
        'ViewRefreshToolStripButton
        '
        Me.ViewRefreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ViewRefreshToolStripButton, "ViewRefreshToolStripButton")
        Me.ViewRefreshToolStripButton.Name = "ViewRefreshToolStripButton"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        resources.ApplyResources(Me.ToolStripSeparator14, "ToolStripSeparator14")
        '
        'InsertTextToolStripButton
        '
        Me.InsertTextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.InsertTextToolStripButton.Image = Global.PDFKeeper.WindowsApplication.My.Resources.Resources.page_text
        resources.ApplyResources(Me.InsertTextToolStripButton, "InsertTextToolStripButton")
        Me.InsertTextToolStripButton.Name = "InsertTextToolStripButton"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        resources.ApplyResources(Me.ToolStripSeparator21, "ToolStripSeparator21")
        '
        'ToolsOptionsToolStripButton
        '
        Me.ToolsOptionsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolsOptionsToolStripButton, "ToolsOptionsToolStripButton")
        Me.ToolsOptionsToolStripButton.Name = "ToolsOptionsToolStripButton"
        '
        'ToolsManageUploadFolderConfigurationsToolStripButton
        '
        Me.ToolsManageUploadFolderConfigurationsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolsManageUploadFolderConfigurationsToolStripButton, "ToolsManageUploadFolderConfigurationsToolStripButton")
        Me.ToolsManageUploadFolderConfigurationsToolStripButton.Name = "ToolsManageUploadFolderConfigurationsToolStripButton"
        '
        'ToolStripSeparator22
        '
        Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
        resources.ApplyResources(Me.ToolStripSeparator22, "ToolStripSeparator22")
        '
        'HelpContentsToolStripButton
        '
        Me.HelpContentsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.HelpContentsToolStripButton, "HelpContentsToolStripButton")
        Me.HelpContentsToolStripButton.Name = "HelpContentsToolStripButton"
        '
        'AutoUpdateCheckTimer
        '
        Me.AutoUpdateCheckTimer.Enabled = True
        Me.AutoUpdateCheckTimer.Interval = 1800000
        '
        'FlaggedDocumentsCheckTimer
        '
        Me.FlaggedDocumentsCheckTimer.Enabled = True
        Me.FlaggedDocumentsCheckTimer.Interval = 10000
        '
        'ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem, "ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem")
        Me.ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem.Name = "ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem"
        '
        'MainForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "MainForm"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        CType(Me.SearchTextErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.SelectedDocumentTabControl.ResumeLayout(False)
        Me.NotesTabPage.ResumeLayout(False)
        Me.NotesTabPage.PerformLayout()
        Me.KeywordsTabPage.ResumeLayout(False)
        Me.KeywordsTabPage.PerformLayout()
        Me.PreviewTabPage.ResumeLayout(False)
        Me.PreviewPanel.ResumeLayout(False)
        Me.PreviewPanel.PerformLayout()
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TextTabPage.ResumeLayout(False)
        Me.TextTabPage.PerformLayout()
        Me.SearchGroupBox.ResumeLayout(False)
        Me.SearchGroupBox.PerformLayout()
        Me.SearchResultsPanel.ResumeLayout(False)
        CType(Me.SearchResultsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents FileAddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileOpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileSaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FilePrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilePrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSelectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileDeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileSelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSelectNoneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditUndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditCutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditCopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditPasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditSelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditDateTimeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolbarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewStatusBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewRefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsOptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolsManageUploadFolderConfigurationsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpAboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileAddToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FileOpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FileSaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FilePrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FileDeleteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditUndoToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditCutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditCopyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditPasteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditDateTimeToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ViewRefreshToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolsOptionsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolsManageUploadFolderConfigurationsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpContentsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SearchTextErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TotalRecordsToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TotalRecordsCountToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ViewSetPreviewImageResolutionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectedDocumentsProcessToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents EditRestoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditRestoreToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UploadTimer As System.Windows.Forms.Timer
    Friend WithEvents UploadRunningToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UploadFolderErrorToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UploadStagingFolderErrorToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FillerToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents AutoUpdateCheckTimer As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditFlagDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSetCategoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlaggedDocumentsExistToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FlaggedDocumentsCheckTimer As System.Windows.Forms.Timer
    Friend WithEvents SearchResultsPanel As Panel
    Friend WithEvents InsertToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InsertTextToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents InsertTextToolStripButton As ToolStripButton
    Friend WithEvents SelectedDocumentTabControl As TabControl
    Friend WithEvents NotesTabPage As TabPage
    Friend WithEvents NotesTextBox As TextBox
    Friend WithEvents KeywordsTabPage As TabPage
    Friend WithEvents KeywordsTextBox As TextBox
    Friend WithEvents PreviewTabPage As TabPage
    Friend WithEvents PreviewPanel As Panel
    Friend WithEvents PreviewPictureBox As PictureBox
    Friend WithEvents TextTabPage As TabPage
    Friend WithEvents TextTextBox As TextBox
    Friend WithEvents SearchGroupBox As GroupBox
    Friend WithEvents DateLabel As Label
    Friend WithEvents TextLabel As Label
    Friend WithEvents SearchFunctionsListBox As ListBox
    Friend WithEvents SearchBySelectionsButton As Button
    Friend WithEvents SearchByTextButton As Button
    Friend WithEvents SearchDateTimePicker As DateTimePicker
    Friend WithEvents ClearSelectionsButton As Button
    Friend WithEvents SubjectGroupComboBox As ComboBox
    Friend WithEvents CategoryGroupComboBox As ComboBox
    Friend WithEvents CategoryLabel As Label
    Friend WithEvents SubjectLabel As Label
    Friend WithEvents AuthorGroupComboBox As ComboBox
    Friend WithEvents AuthorLabel As Label
    Friend WithEvents SearchTextComboBox As ComboBox
    Friend WithEvents SearchResultsDataGridView As DataGridView
    Friend WithEvents SelectionColumn As DataGridViewCheckBoxColumn
    Friend WithEvents TaxYearGroupComboBox As ComboBox
    Friend WithEvents TaxYearLabel As Label
    Friend WithEvents FileSetTaxYearToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsUpdatePdfTextAnnotAndTextInDbToolStripButton As ToolStripButton
    Friend WithEvents HelpProvider As HelpProvider
    Friend WithEvents ToolsMoveDatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsUpdatePdfTextAnnotAndTextInDbToolStripMenuItem As ToolStripMenuItem
End Class
