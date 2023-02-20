'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
                components.Dispose()
                presenter.Dispose()
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
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileBurstToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ViewSetPreviewPixelDensityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewToolBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewStatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertTextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsUploadProfilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolsUpdatePdfTextColumnsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsMoveDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpAboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.DocumentTabControl = New System.Windows.Forms.TabControl()
        Me.NotesTabPage = New System.Windows.Forms.TabPage()
        Me.NotesTextBox = New System.Windows.Forms.TextBox()
        Me.KeywordsTabPage = New System.Windows.Forms.TabPage()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.PreviewTabPage = New System.Windows.Forms.TabPage()
        Me.PreviewPanel = New System.Windows.Forms.Panel()
        Me.PreviewPictureBox = New System.Windows.Forms.PictureBox()
        Me.TextTabPage = New System.Windows.Forms.TabPage()
        Me.TextTextBox = New System.Windows.Forms.TextBox()
        Me.SearchTermSnippetsTabPage = New System.Windows.Forms.TabPage()
        Me.SearchTermSnippetsTextBox = New System.Windows.Forms.TextBox()
        Me.DocumentRetrievalGroupBox = New System.Windows.Forms.GroupBox()
        Me.TaxYearComboBox = New System.Windows.Forms.ComboBox()
        Me.TaxYearLabel = New System.Windows.Forms.Label()
        Me.DateLabel = New System.Windows.Forms.Label()
        Me.SearchTermLabel = New System.Windows.Forms.Label()
        Me.DocumentRetrievalChoicesListBox = New System.Windows.Forms.ListBox()
        Me.FindBySelectionsButton = New System.Windows.Forms.Button()
        Me.FindBySearchTermButton = New System.Windows.Forms.Button()
        Me.DateAddedDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.ClearSelectionsButton = New System.Windows.Forms.Button()
        Me.SubjectComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryLabel = New System.Windows.Forms.Label()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.AuthorComboBox = New System.Windows.Forms.ComboBox()
        Me.AuthorLabel = New System.Windows.Forms.Label()
        Me.SearchTermComboBox = New System.Windows.Forms.ComboBox()
        Me.SearchResultsPanel = New System.Windows.Forms.Panel()
        Me.DocumentListDataGridView = New System.Windows.Forms.DataGridView()
        Me.SelectionColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UploadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.DocumentsToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.DocumentsCountToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FillerToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.UploadRunningImageToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadRejectedImageToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FlagImageToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.FileAddToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileOpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileSaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileBurstToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FilePrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileDeleteToolStripButton = New System.Windows.Forms.ToolStripButton()
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
        Me.ToolsUploadProfilesToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolsUpdatePdfTextColumnsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpContentsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.UpdateCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FlaggedDocumentCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.PrintDialog = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog = New System.Windows.Forms.PrintPreviewDialog()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.UploadFolderMaintenanceTimer = New System.Windows.Forms.Timer(Me.components)
        Me.UploadRejectedFilesCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.DocumentTabControl.SuspendLayout()
        Me.NotesTabPage.SuspendLayout()
        Me.KeywordsTabPage.SuspendLayout()
        Me.PreviewTabPage.SuspendLayout()
        Me.PreviewPanel.SuspendLayout()
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TextTabPage.SuspendLayout()
        Me.SearchTermSnippetsTabPage.SuspendLayout()
        Me.DocumentRetrievalGroupBox.SuspendLayout()
        Me.SearchResultsPanel.SuspendLayout()
        CType(Me.DocumentListDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAddToolStripMenuItem, Me.ToolStripSeparator1, Me.FileOpenToolStripMenuItem, Me.ToolStripSeparator2, Me.FileSaveToolStripMenuItem, Me.FileSaveAsToolStripMenuItem, Me.ToolStripSeparator15, Me.FileBurstToolStripMenuItem, Me.ToolStripSeparator4, Me.FilePrintToolStripMenuItem, Me.FilePrintPreviewToolStripMenuItem, Me.ToolStripSeparator3, Me.FileSelectToolStripMenuItem, Me.FileSetCategoryToolStripMenuItem, Me.FileSetTaxYearToolStripMenuItem, Me.FileDeleteToolStripMenuItem, Me.FileExportToolStripMenuItem, Me.ToolStripSeparator6, Me.FileExitToolStripMenuItem})
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
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        resources.ApplyResources(Me.ToolStripSeparator15, "ToolStripSeparator15")
        '
        'FileBurstToolStripMenuItem
        '
        Me.FileBurstToolStripMenuItem.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.cut_red
        Me.FileBurstToolStripMenuItem.Name = "FileBurstToolStripMenuItem"
        resources.ApplyResources(Me.FileBurstToolStripMenuItem, "FileBurstToolStripMenuItem")
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
        Me.EditToolStripMenuItem.Tag = ""
        '
        'EditUndoToolStripMenuItem
        '
        resources.ApplyResources(Me.EditUndoToolStripMenuItem, "EditUndoToolStripMenuItem")
        Me.EditUndoToolStripMenuItem.Name = "EditUndoToolStripMenuItem"
        Me.EditUndoToolStripMenuItem.Tag = ""
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
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewRefreshToolStripMenuItem, Me.ToolStripSeparator24, Me.ViewSetPreviewPixelDensityToolStripMenuItem, Me.ToolStripSeparator10, Me.ViewToolBarToolStripMenuItem, Me.ViewStatusBarToolStripMenuItem})
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
        'ViewSetPreviewPixelDensityToolStripMenuItem
        '
        Me.ViewSetPreviewPixelDensityToolStripMenuItem.Name = "ViewSetPreviewPixelDensityToolStripMenuItem"
        resources.ApplyResources(Me.ViewSetPreviewPixelDensityToolStripMenuItem, "ViewSetPreviewPixelDensityToolStripMenuItem")
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        resources.ApplyResources(Me.ToolStripSeparator10, "ToolStripSeparator10")
        '
        'ViewToolBarToolStripMenuItem
        '
        Me.ViewToolBarToolStripMenuItem.Checked = True
        Me.ViewToolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ViewToolBarToolStripMenuItem.Name = "ViewToolBarToolStripMenuItem"
        resources.ApplyResources(Me.ViewToolBarToolStripMenuItem, "ViewToolBarToolStripMenuItem")
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
        Me.InsertTextToolStripMenuItem.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.page_text
        Me.InsertTextToolStripMenuItem.Name = "InsertTextToolStripMenuItem"
        resources.ApplyResources(Me.InsertTextToolStripMenuItem, "InsertTextToolStripMenuItem")
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsOptionsToolStripMenuItem, Me.ToolsUploadProfilesToolStripMenuItem, Me.ToolStripSeparator11, Me.ToolsUpdatePdfTextColumnsToolStripMenuItem, Me.ToolsMoveDatabaseToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        '
        'ToolsOptionsToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsOptionsToolStripMenuItem, "ToolsOptionsToolStripMenuItem")
        Me.ToolsOptionsToolStripMenuItem.Name = "ToolsOptionsToolStripMenuItem"
        '
        'ToolsUploadProfilesToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsUploadProfilesToolStripMenuItem, "ToolsUploadProfilesToolStripMenuItem")
        Me.ToolsUploadProfilesToolStripMenuItem.Name = "ToolsUploadProfilesToolStripMenuItem"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        resources.ApplyResources(Me.ToolStripSeparator11, "ToolStripSeparator11")
        '
        'ToolsUpdatePdfTextColumnsToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsUpdatePdfTextColumnsToolStripMenuItem, "ToolsUpdatePdfTextColumnsToolStripMenuItem")
        Me.ToolsUpdatePdfTextColumnsToolStripMenuItem.Name = "ToolsUpdatePdfTextColumnsToolStripMenuItem"
        '
        'ToolsMoveDatabaseToolStripMenuItem
        '
        Me.ToolsMoveDatabaseToolStripMenuItem.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.database
        Me.ToolsMoveDatabaseToolStripMenuItem.Name = "ToolsMoveDatabaseToolStripMenuItem"
        resources.ApplyResources(Me.ToolsMoveDatabaseToolStripMenuItem, "ToolsMoveDatabaseToolStripMenuItem")
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
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'SplitContainer
        '
        Me.SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.SplitContainer, "SplitContainer")
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.DocumentTabControl)
        Me.SplitContainer.Panel1.Controls.Add(Me.DocumentRetrievalGroupBox)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.SearchResultsPanel)
        Me.SplitContainer.TabStop = False
        '
        'DocumentTabControl
        '
        Me.DocumentTabControl.Controls.Add(Me.NotesTabPage)
        Me.DocumentTabControl.Controls.Add(Me.KeywordsTabPage)
        Me.DocumentTabControl.Controls.Add(Me.PreviewTabPage)
        Me.DocumentTabControl.Controls.Add(Me.TextTabPage)
        Me.DocumentTabControl.Controls.Add(Me.SearchTermSnippetsTabPage)
        resources.ApplyResources(Me.DocumentTabControl, "DocumentTabControl")
        Me.DocumentTabControl.Name = "DocumentTabControl"
        Me.DocumentTabControl.SelectedIndex = 0
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
        'SearchTermSnippetsTabPage
        '
        Me.SearchTermSnippetsTabPage.Controls.Add(Me.SearchTermSnippetsTextBox)
        resources.ApplyResources(Me.SearchTermSnippetsTabPage, "SearchTermSnippetsTabPage")
        Me.SearchTermSnippetsTabPage.Name = "SearchTermSnippetsTabPage"
        Me.SearchTermSnippetsTabPage.UseVisualStyleBackColor = True
        '
        'SearchTermSnippetsTextBox
        '
        resources.ApplyResources(Me.SearchTermSnippetsTextBox, "SearchTermSnippetsTextBox")
        Me.SearchTermSnippetsTextBox.Name = "SearchTermSnippetsTextBox"
        Me.SearchTermSnippetsTextBox.ReadOnly = True
        Me.HelpProvider.SetShowHelp(Me.SearchTermSnippetsTextBox, CType(resources.GetObject("SearchTermSnippetsTextBox.ShowHelp"), Boolean))
        '
        'DocumentRetrievalGroupBox
        '
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.TaxYearComboBox)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.TaxYearLabel)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.DateLabel)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.SearchTermLabel)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.DocumentRetrievalChoicesListBox)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.FindBySelectionsButton)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.FindBySearchTermButton)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.DateAddedDateTimePicker)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.ClearSelectionsButton)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.SubjectComboBox)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.CategoryComboBox)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.CategoryLabel)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.SubjectLabel)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.AuthorComboBox)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.AuthorLabel)
        Me.DocumentRetrievalGroupBox.Controls.Add(Me.SearchTermComboBox)
        resources.ApplyResources(Me.DocumentRetrievalGroupBox, "DocumentRetrievalGroupBox")
        Me.DocumentRetrievalGroupBox.Name = "DocumentRetrievalGroupBox"
        Me.DocumentRetrievalGroupBox.TabStop = False
        '
        'TaxYearComboBox
        '
        resources.ApplyResources(Me.TaxYearComboBox, "TaxYearComboBox")
        Me.TaxYearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TaxYearComboBox.FormattingEnabled = True
        Me.TaxYearComboBox.Name = "TaxYearComboBox"
        Me.TaxYearComboBox.Sorted = True
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
        'SearchTermLabel
        '
        resources.ApplyResources(Me.SearchTermLabel, "SearchTermLabel")
        Me.SearchTermLabel.Name = "SearchTermLabel"
        '
        'DocumentRetrievalChoicesListBox
        '
        resources.ApplyResources(Me.DocumentRetrievalChoicesListBox, "DocumentRetrievalChoicesListBox")
        Me.DocumentRetrievalChoicesListBox.FormattingEnabled = True
        Me.DocumentRetrievalChoicesListBox.Items.AddRange(New Object() {resources.GetString("DocumentRetrievalChoicesListBox.Items"), resources.GetString("DocumentRetrievalChoicesListBox.Items1"), resources.GetString("DocumentRetrievalChoicesListBox.Items2"), resources.GetString("DocumentRetrievalChoicesListBox.Items3"), resources.GetString("DocumentRetrievalChoicesListBox.Items4")})
        Me.DocumentRetrievalChoicesListBox.Name = "DocumentRetrievalChoicesListBox"
        '
        'FindBySelectionsButton
        '
        resources.ApplyResources(Me.FindBySelectionsButton, "FindBySelectionsButton")
        Me.FindBySelectionsButton.Name = "FindBySelectionsButton"
        Me.FindBySelectionsButton.UseVisualStyleBackColor = True
        '
        'FindBySearchTermButton
        '
        resources.ApplyResources(Me.FindBySearchTermButton, "FindBySearchTermButton")
        Me.FindBySearchTermButton.Name = "FindBySearchTermButton"
        Me.HelpProvider.SetShowHelp(Me.FindBySearchTermButton, CType(resources.GetObject("FindBySearchTermButton.ShowHelp"), Boolean))
        Me.FindBySearchTermButton.UseVisualStyleBackColor = True
        '
        'DateAddedDateTimePicker
        '
        resources.ApplyResources(Me.DateAddedDateTimePicker, "DateAddedDateTimePicker")
        Me.DateAddedDateTimePicker.Name = "DateAddedDateTimePicker"
        '
        'ClearSelectionsButton
        '
        resources.ApplyResources(Me.ClearSelectionsButton, "ClearSelectionsButton")
        Me.ClearSelectionsButton.Name = "ClearSelectionsButton"
        Me.ClearSelectionsButton.UseVisualStyleBackColor = True
        '
        'SubjectComboBox
        '
        resources.ApplyResources(Me.SubjectComboBox, "SubjectComboBox")
        Me.SubjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SubjectComboBox.FormattingEnabled = True
        Me.SubjectComboBox.Name = "SubjectComboBox"
        Me.SubjectComboBox.Sorted = True
        '
        'CategoryComboBox
        '
        resources.ApplyResources(Me.CategoryComboBox, "CategoryComboBox")
        Me.CategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CategoryComboBox.FormattingEnabled = True
        Me.CategoryComboBox.Name = "CategoryComboBox"
        Me.CategoryComboBox.Sorted = True
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
        'AuthorComboBox
        '
        resources.ApplyResources(Me.AuthorComboBox, "AuthorComboBox")
        Me.AuthorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AuthorComboBox.FormattingEnabled = True
        Me.AuthorComboBox.Name = "AuthorComboBox"
        Me.AuthorComboBox.Sorted = True
        '
        'AuthorLabel
        '
        resources.ApplyResources(Me.AuthorLabel, "AuthorLabel")
        Me.AuthorLabel.Name = "AuthorLabel"
        '
        'SearchTermComboBox
        '
        resources.ApplyResources(Me.SearchTermComboBox, "SearchTermComboBox")
        Me.SearchTermComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SearchTermComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SearchTermComboBox.FormattingEnabled = True
        Me.SearchTermComboBox.Name = "SearchTermComboBox"
        Me.SearchTermComboBox.Sorted = True
        '
        'SearchResultsPanel
        '
        Me.SearchResultsPanel.Controls.Add(Me.DocumentListDataGridView)
        resources.ApplyResources(Me.SearchResultsPanel, "SearchResultsPanel")
        Me.SearchResultsPanel.Name = "SearchResultsPanel"
        '
        'DocumentListDataGridView
        '
        Me.DocumentListDataGridView.AllowUserToAddRows = False
        Me.DocumentListDataGridView.AllowUserToDeleteRows = False
        Me.DocumentListDataGridView.AllowUserToResizeRows = False
        Me.DocumentListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DocumentListDataGridView.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DocumentListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DocumentListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DocumentListDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelectionColumn})
        resources.ApplyResources(Me.DocumentListDataGridView, "DocumentListDataGridView")
        Me.DocumentListDataGridView.MultiSelect = False
        Me.DocumentListDataGridView.Name = "DocumentListDataGridView"
        Me.DocumentListDataGridView.RowHeadersVisible = False
        Me.DocumentListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DocumentListDataGridView.StandardTab = True
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
        Me.UploadTimer.Interval = 15000
        '
        'StatusStrip
        '
        Me.StatusStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.Presentation.My.MySettings.Default, "MainStatusBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocumentsToolStripStatusLabel, Me.DocumentsCountToolStripStatusLabel, Me.FillerToolStripStatusLabel, Me.ToolStripProgressBar, Me.UploadRunningImageToolStripStatusLabel, Me.UploadRejectedImageToolStripStatusLabel, Me.FlagImageToolStripStatusLabel})
        resources.ApplyResources(Me.StatusStrip, "StatusStrip")
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.ShowItemToolTips = True
        Me.StatusStrip.Visible = Global.PDFKeeper.Presentation.My.MySettings.Default.MainStatusBarVisible
        '
        'DocumentsToolStripStatusLabel
        '
        Me.DocumentsToolStripStatusLabel.Name = "DocumentsToolStripStatusLabel"
        resources.ApplyResources(Me.DocumentsToolStripStatusLabel, "DocumentsToolStripStatusLabel")
        '
        'DocumentsCountToolStripStatusLabel
        '
        Me.DocumentsCountToolStripStatusLabel.Name = "DocumentsCountToolStripStatusLabel"
        resources.ApplyResources(Me.DocumentsCountToolStripStatusLabel, "DocumentsCountToolStripStatusLabel")
        '
        'FillerToolStripStatusLabel
        '
        resources.ApplyResources(Me.FillerToolStripStatusLabel, "FillerToolStripStatusLabel")
        Me.FillerToolStripStatusLabel.Name = "FillerToolStripStatusLabel"
        Me.FillerToolStripStatusLabel.Spring = True
        '
        'ToolStripProgressBar
        '
        Me.ToolStripProgressBar.Name = "ToolStripProgressBar"
        resources.ApplyResources(Me.ToolStripProgressBar, "ToolStripProgressBar")
        Me.ToolStripProgressBar.Step = 1
        '
        'UploadRunningImageToolStripStatusLabel
        '
        resources.ApplyResources(Me.UploadRunningImageToolStripStatusLabel, "UploadRunningImageToolStripStatusLabel")
        Me.UploadRunningImageToolStripStatusLabel.Name = "UploadRunningImageToolStripStatusLabel"
        '
        'UploadRejectedImageToolStripStatusLabel
        '
        resources.ApplyResources(Me.UploadRejectedImageToolStripStatusLabel, "UploadRejectedImageToolStripStatusLabel")
        Me.UploadRejectedImageToolStripStatusLabel.Name = "UploadRejectedImageToolStripStatusLabel"
        '
        'FlagImageToolStripStatusLabel
        '
        resources.ApplyResources(Me.FlagImageToolStripStatusLabel, "FlagImageToolStripStatusLabel")
        Me.FlagImageToolStripStatusLabel.Name = "FlagImageToolStripStatusLabel"
        '
        'ToolStrip
        '
        Me.ToolStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.Presentation.My.MySettings.Default, "MainToolBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAddToolStripButton, Me.FileOpenToolStripButton, Me.FileSaveToolStripButton, Me.FileBurstToolStripButton, Me.FilePrintToolStripButton, Me.FileDeleteToolStripButton, Me.ToolStripSeparator17, Me.EditUndoToolStripButton, Me.EditCutToolStripButton, Me.EditCopyToolStripButton, Me.EditPasteToolStripButton, Me.EditRestoreToolStripButton, Me.EditDateTimeToolStripButton, Me.ToolStripSeparator20, Me.ViewRefreshToolStripButton, Me.ToolStripSeparator14, Me.InsertTextToolStripButton, Me.ToolStripSeparator21, Me.ToolsOptionsToolStripButton, Me.ToolsUploadProfilesToolStripButton, Me.ToolsUpdatePdfTextColumnsToolStripButton, Me.ToolStripSeparator22, Me.HelpContentsToolStripButton})
        resources.ApplyResources(Me.ToolStrip, "ToolStrip")
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Visible = Global.PDFKeeper.Presentation.My.MySettings.Default.MainToolBarVisible
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
        'FileBurstToolStripButton
        '
        Me.FileBurstToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FileBurstToolStripButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.cut_red
        resources.ApplyResources(Me.FileBurstToolStripButton, "FileBurstToolStripButton")
        Me.FileBurstToolStripButton.Name = "FileBurstToolStripButton"
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
        Me.InsertTextToolStripButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.page_text
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
        'ToolsUploadProfilesToolStripButton
        '
        Me.ToolsUploadProfilesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolsUploadProfilesToolStripButton, "ToolsUploadProfilesToolStripButton")
        Me.ToolsUploadProfilesToolStripButton.Name = "ToolsUploadProfilesToolStripButton"
        '
        'ToolsUpdatePdfTextColumnsToolStripButton
        '
        Me.ToolsUpdatePdfTextColumnsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolsUpdatePdfTextColumnsToolStripButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.table
        resources.ApplyResources(Me.ToolsUpdatePdfTextColumnsToolStripButton, "ToolsUpdatePdfTextColumnsToolStripButton")
        Me.ToolsUpdatePdfTextColumnsToolStripButton.Name = "ToolsUpdatePdfTextColumnsToolStripButton"
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
        'UpdateCheckTimer
        '
        Me.UpdateCheckTimer.Enabled = True
        Me.UpdateCheckTimer.Interval = 3600000
        '
        'FlaggedDocumentCheckTimer
        '
        Me.FlaggedDocumentCheckTimer.Enabled = True
        Me.FlaggedDocumentCheckTimer.Interval = 5000
        '
        'PrintDialog
        '
        Me.PrintDialog.Document = Me.PrintDocument
        Me.PrintDialog.UseEXDialog = True
        '
        'PrintPreviewDialog
        '
        resources.ApplyResources(Me.PrintPreviewDialog, "PrintPreviewDialog")
        Me.PrintPreviewDialog.Document = Me.PrintDocument
        Me.PrintPreviewDialog.Name = "PrintPreviewDialog"
        Me.PrintPreviewDialog.ShowIcon = False
        Me.PrintPreviewDialog.UseAntiAlias = True
        '
        'OpenFileDialog
        '
        resources.ApplyResources(Me.OpenFileDialog, "OpenFileDialog")
        '
        'UploadFolderMaintenanceTimer
        '
        Me.UploadFolderMaintenanceTimer.Enabled = True
        Me.UploadFolderMaintenanceTimer.Interval = 30000
        '
        'UploadRejectedFilesCheckTimer
        '
        Me.UploadRejectedFilesCheckTimer.Enabled = True
        Me.UploadRejectedFilesCheckTimer.Interval = 30000
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
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.DocumentTabControl.ResumeLayout(False)
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
        Me.SearchTermSnippetsTabPage.ResumeLayout(False)
        Me.SearchTermSnippetsTabPage.PerformLayout()
        Me.DocumentRetrievalGroupBox.ResumeLayout(False)
        Me.DocumentRetrievalGroupBox.PerformLayout()
        Me.SearchResultsPanel.ResumeLayout(False)
        CType(Me.DocumentListDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ViewToolBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewStatusBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewRefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsOptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolsUploadProfilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents ToolsUploadProfilesToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpContentsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents DocumentsToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DocumentsCountToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ViewSetPreviewPixelDensityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents EditRestoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditRestoreToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UploadTimer As System.Windows.Forms.Timer
    Friend WithEvents UploadRunningImageToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UploadRejectedImageToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FillerToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UpdateCheckTimer As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditFlagDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSetCategoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlagImageToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FlaggedDocumentCheckTimer As System.Windows.Forms.Timer
    Friend WithEvents SearchResultsPanel As Panel
    Friend WithEvents InsertToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InsertTextToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents InsertTextToolStripButton As ToolStripButton
    Friend WithEvents DocumentTabControl As TabControl
    Friend WithEvents NotesTabPage As TabPage
    Friend WithEvents NotesTextBox As TextBox
    Friend WithEvents KeywordsTabPage As TabPage
    Friend WithEvents KeywordsTextBox As TextBox
    Friend WithEvents PreviewTabPage As TabPage
    Friend WithEvents PreviewPanel As Panel
    Friend WithEvents PreviewPictureBox As PictureBox
    Friend WithEvents TextTabPage As TabPage
    Friend WithEvents TextTextBox As TextBox
    Friend WithEvents DocumentRetrievalGroupBox As GroupBox
    Friend WithEvents DateLabel As Label
    Friend WithEvents SearchTermLabel As Label
    Friend WithEvents DocumentRetrievalChoicesListBox As ListBox
    Friend WithEvents FindBySelectionsButton As Button
    Friend WithEvents FindBySearchTermButton As Button
    Friend WithEvents DateAddedDateTimePicker As DateTimePicker
    Friend WithEvents ClearSelectionsButton As Button
    Friend WithEvents SubjectComboBox As ComboBox
    Friend WithEvents CategoryComboBox As ComboBox
    Friend WithEvents CategoryLabel As Label
    Friend WithEvents SubjectLabel As Label
    Friend WithEvents AuthorComboBox As ComboBox
    Friend WithEvents AuthorLabel As Label
    Friend WithEvents SearchTermComboBox As ComboBox
    Friend WithEvents DocumentListDataGridView As DataGridView
    Friend WithEvents SelectionColumn As DataGridViewCheckBoxColumn
    Friend WithEvents TaxYearComboBox As ComboBox
    Friend WithEvents TaxYearLabel As Label
    Friend WithEvents FileSetTaxYearToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsUpdatePdfTextColumnsToolStripButton As ToolStripButton
    Friend WithEvents HelpProvider As HelpProvider
    Friend WithEvents ToolsMoveDatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsUpdatePdfTextColumnsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog As SaveFileDialog
    Friend WithEvents PrintDialog As PrintDialog
    Friend WithEvents PrintPreviewDialog As PrintPreviewDialog
    Friend WithEvents PrintDocument As PrintDocument
    Friend WithEvents FolderBrowserDialog As FolderBrowserDialog
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents UploadFolderMaintenanceTimer As Timer
    Friend WithEvents UploadRejectedFilesCheckTimer As Timer
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents FileBurstToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileBurstToolStripButton As ToolStripButton
    Friend WithEvents SearchTermSnippetsTabPage As TabPage
    Friend WithEvents SearchTermSnippetsTextBox As TextBox
End Class
