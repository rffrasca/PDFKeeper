' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2023 Robert F. Frasca
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

Partial Class MainView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainView))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileAddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileOpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileSaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileBurstToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.FilePrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilePrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.FileExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditUndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditCutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditCopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditPasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditSelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditRestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditAppendDateTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditAppendTextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditFlagDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsFindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DocumentsSelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsSelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsSelectNoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DocumentsSetTitleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsSetAuthorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsSetCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsSetTaxYearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.DocumentsDeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewSetPreviewPixelDensityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewToolBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewStatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsUploadProfilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsMoveDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpAboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HorizontalSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.SearchResultsPanel = New System.Windows.Forms.Panel()
        Me.DocumentsDataGridView = New System.Windows.Forms.DataGridView()
        Me.SelectionColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MainViewModelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.VerticalSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.DocumentDataTabControl = New System.Windows.Forms.TabControl()
        Me.NotesTabPage = New System.Windows.Forms.TabPage()
        Me.NotesTextBox = New System.Windows.Forms.TextBox()
        Me.KeywordsTabPage = New System.Windows.Forms.TabPage()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.TextTabPage = New System.Windows.Forms.TabPage()
        Me.TextTextBox = New System.Windows.Forms.TextBox()
        Me.SearchTermSnippetsTabPage = New System.Windows.Forms.TabPage()
        Me.SearchTermSnippetsTextBox = New System.Windows.Forms.TextBox()
        Me.PreviewTabControl = New System.Windows.Forms.TabControl()
        Me.PreviewTabPage = New System.Windows.Forms.TabPage()
        Me.PreviewPanel = New System.Windows.Forms.Panel()
        Me.PreviewPictureBox = New System.Windows.Forms.PictureBox()
        Me.UploadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.DocumentsLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.DocumentsCountLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStripFillerLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.RefreshingDocumentsImageLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FlagImageLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadRunningImageLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadRejectedImageLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.FileAddToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileOpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileSaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileBurstToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FilePrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolBarToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditUndoToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditCutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditCopyToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditPasteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditRestoreToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditAppendDateTimeToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.EditAppendTextToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolBarToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DocumentsFindToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DocumentsDeleteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolBarToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolsOptionsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolsUploadProfilesToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolBarToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpContentsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.UpdateCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CheckForFlaggedDocumentsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DocumentsListTimedRefreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.CheckForDocumentsListChangesTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DocumentsSetSubjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip.SuspendLayout()
        CType(Me.HorizontalSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HorizontalSplitContainer.Panel1.SuspendLayout()
        Me.HorizontalSplitContainer.Panel2.SuspendLayout()
        Me.HorizontalSplitContainer.SuspendLayout()
        Me.SearchResultsPanel.SuspendLayout()
        CType(Me.DocumentsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MainViewModelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VerticalSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VerticalSplitContainer.Panel1.SuspendLayout()
        Me.VerticalSplitContainer.Panel2.SuspendLayout()
        Me.VerticalSplitContainer.SuspendLayout()
        Me.DocumentDataTabControl.SuspendLayout()
        Me.NotesTabPage.SuspendLayout()
        Me.KeywordsTabPage.SuspendLayout()
        Me.TextTabPage.SuspendLayout()
        Me.SearchTermSnippetsTabPage.SuspendLayout()
        Me.PreviewTabControl.SuspendLayout()
        Me.PreviewTabPage.SuspendLayout()
        Me.PreviewPanel.SuspendLayout()
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.DocumentsToolStripMenuItem, Me.ViewToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip, "MenuStrip")
        Me.MenuStrip.Name = "MenuStrip"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAddToolStripMenuItem, Me.FileToolStripSeparator1, Me.FileOpenToolStripMenuItem, Me.FileToolStripSeparator2, Me.FileSaveToolStripMenuItem, Me.FileSaveAsToolStripMenuItem, Me.FileToolStripSeparator3, Me.FileBurstToolStripMenuItem, Me.FileToolStripSeparator4, Me.FilePrintToolStripMenuItem, Me.FilePrintPreviewToolStripMenuItem, Me.FileToolStripSeparator5, Me.FileExportToolStripMenuItem, Me.FileToolStripSeparator6, Me.FileExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'FileAddToolStripMenuItem
        '
        resources.ApplyResources(Me.FileAddToolStripMenuItem, "FileAddToolStripMenuItem")
        Me.FileAddToolStripMenuItem.Name = "FileAddToolStripMenuItem"
        Me.FileAddToolStripMenuItem.Tag = ""
        '
        'FileToolStripSeparator1
        '
        Me.FileToolStripSeparator1.Name = "FileToolStripSeparator1"
        resources.ApplyResources(Me.FileToolStripSeparator1, "FileToolStripSeparator1")
        '
        'FileOpenToolStripMenuItem
        '
        resources.ApplyResources(Me.FileOpenToolStripMenuItem, "FileOpenToolStripMenuItem")
        Me.FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem"
        '
        'FileToolStripSeparator2
        '
        Me.FileToolStripSeparator2.Name = "FileToolStripSeparator2"
        resources.ApplyResources(Me.FileToolStripSeparator2, "FileToolStripSeparator2")
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
        'FileToolStripSeparator3
        '
        Me.FileToolStripSeparator3.Name = "FileToolStripSeparator3"
        resources.ApplyResources(Me.FileToolStripSeparator3, "FileToolStripSeparator3")
        '
        'FileBurstToolStripMenuItem
        '
        Me.FileBurstToolStripMenuItem.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.cut_red
        Me.FileBurstToolStripMenuItem.Name = "FileBurstToolStripMenuItem"
        resources.ApplyResources(Me.FileBurstToolStripMenuItem, "FileBurstToolStripMenuItem")
        '
        'FileToolStripSeparator4
        '
        Me.FileToolStripSeparator4.Name = "FileToolStripSeparator4"
        resources.ApplyResources(Me.FileToolStripSeparator4, "FileToolStripSeparator4")
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
        'FileToolStripSeparator5
        '
        Me.FileToolStripSeparator5.Name = "FileToolStripSeparator5"
        resources.ApplyResources(Me.FileToolStripSeparator5, "FileToolStripSeparator5")
        '
        'FileExportToolStripMenuItem
        '
        Me.FileExportToolStripMenuItem.Name = "FileExportToolStripMenuItem"
        resources.ApplyResources(Me.FileExportToolStripMenuItem, "FileExportToolStripMenuItem")
        '
        'FileToolStripSeparator6
        '
        Me.FileToolStripSeparator6.Name = "FileToolStripSeparator6"
        resources.ApplyResources(Me.FileToolStripSeparator6, "FileToolStripSeparator6")
        '
        'FileExitToolStripMenuItem
        '
        Me.FileExitToolStripMenuItem.Name = "FileExitToolStripMenuItem"
        resources.ApplyResources(Me.FileExitToolStripMenuItem, "FileExitToolStripMenuItem")
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditUndoToolStripMenuItem, Me.EditToolStripSeparator1, Me.EditCutToolStripMenuItem, Me.EditCopyToolStripMenuItem, Me.EditPasteToolStripMenuItem, Me.EditToolStripSeparator2, Me.EditSelectAllToolStripMenuItem, Me.EditToolStripSeparator3, Me.EditRestoreToolStripMenuItem, Me.EditToolStripSeparator4, Me.EditAppendDateTimeToolStripMenuItem, Me.EditAppendTextToolStripMenuItem, Me.EditToolStripSeparator5, Me.EditFlagDocumentToolStripMenuItem})
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
        'EditToolStripSeparator1
        '
        Me.EditToolStripSeparator1.Name = "EditToolStripSeparator1"
        resources.ApplyResources(Me.EditToolStripSeparator1, "EditToolStripSeparator1")
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
        'EditToolStripSeparator2
        '
        Me.EditToolStripSeparator2.Name = "EditToolStripSeparator2"
        resources.ApplyResources(Me.EditToolStripSeparator2, "EditToolStripSeparator2")
        '
        'EditSelectAllToolStripMenuItem
        '
        Me.EditSelectAllToolStripMenuItem.Name = "EditSelectAllToolStripMenuItem"
        resources.ApplyResources(Me.EditSelectAllToolStripMenuItem, "EditSelectAllToolStripMenuItem")
        '
        'EditToolStripSeparator3
        '
        Me.EditToolStripSeparator3.Name = "EditToolStripSeparator3"
        resources.ApplyResources(Me.EditToolStripSeparator3, "EditToolStripSeparator3")
        '
        'EditRestoreToolStripMenuItem
        '
        resources.ApplyResources(Me.EditRestoreToolStripMenuItem, "EditRestoreToolStripMenuItem")
        Me.EditRestoreToolStripMenuItem.Name = "EditRestoreToolStripMenuItem"
        '
        'EditToolStripSeparator4
        '
        Me.EditToolStripSeparator4.Name = "EditToolStripSeparator4"
        resources.ApplyResources(Me.EditToolStripSeparator4, "EditToolStripSeparator4")
        '
        'EditAppendDateTimeToolStripMenuItem
        '
        resources.ApplyResources(Me.EditAppendDateTimeToolStripMenuItem, "EditAppendDateTimeToolStripMenuItem")
        Me.EditAppendDateTimeToolStripMenuItem.Name = "EditAppendDateTimeToolStripMenuItem"
        '
        'EditAppendTextToolStripMenuItem
        '
        Me.EditAppendTextToolStripMenuItem.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.page_text
        Me.EditAppendTextToolStripMenuItem.Name = "EditAppendTextToolStripMenuItem"
        resources.ApplyResources(Me.EditAppendTextToolStripMenuItem, "EditAppendTextToolStripMenuItem")
        '
        'EditToolStripSeparator5
        '
        Me.EditToolStripSeparator5.Name = "EditToolStripSeparator5"
        resources.ApplyResources(Me.EditToolStripSeparator5, "EditToolStripSeparator5")
        '
        'EditFlagDocumentToolStripMenuItem
        '
        Me.EditFlagDocumentToolStripMenuItem.CheckOnClick = True
        Me.EditFlagDocumentToolStripMenuItem.Name = "EditFlagDocumentToolStripMenuItem"
        resources.ApplyResources(Me.EditFlagDocumentToolStripMenuItem, "EditFlagDocumentToolStripMenuItem")
        '
        'DocumentsToolStripMenuItem
        '
        Me.DocumentsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocumentsFindToolStripMenuItem, Me.DocumentsToolStripSeparator1, Me.DocumentsSelectToolStripMenuItem, Me.DocumentsToolStripSeparator2, Me.DocumentsSetTitleToolStripMenuItem, Me.DocumentsSetAuthorToolStripMenuItem, Me.DocumentsSetSubjectToolStripMenuItem, Me.DocumentsSetCategoryToolStripMenuItem, Me.DocumentsSetTaxYearToolStripMenuItem, Me.DocumentsToolStripSeparator3, Me.DocumentsDeleteToolStripMenuItem})
        Me.DocumentsToolStripMenuItem.Name = "DocumentsToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsToolStripMenuItem, "DocumentsToolStripMenuItem")
        '
        'DocumentsFindToolStripMenuItem
        '
        resources.ApplyResources(Me.DocumentsFindToolStripMenuItem, "DocumentsFindToolStripMenuItem")
        Me.DocumentsFindToolStripMenuItem.Name = "DocumentsFindToolStripMenuItem"
        '
        'DocumentsToolStripSeparator1
        '
        Me.DocumentsToolStripSeparator1.Name = "DocumentsToolStripSeparator1"
        resources.ApplyResources(Me.DocumentsToolStripSeparator1, "DocumentsToolStripSeparator1")
        '
        'DocumentsSelectToolStripMenuItem
        '
        Me.DocumentsSelectToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocumentsSelectAllToolStripMenuItem, Me.DocumentsSelectNoneToolStripMenuItem})
        Me.DocumentsSelectToolStripMenuItem.Name = "DocumentsSelectToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSelectToolStripMenuItem, "DocumentsSelectToolStripMenuItem")
        '
        'DocumentsSelectAllToolStripMenuItem
        '
        Me.DocumentsSelectAllToolStripMenuItem.Name = "DocumentsSelectAllToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSelectAllToolStripMenuItem, "DocumentsSelectAllToolStripMenuItem")
        '
        'DocumentsSelectNoneToolStripMenuItem
        '
        Me.DocumentsSelectNoneToolStripMenuItem.Name = "DocumentsSelectNoneToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSelectNoneToolStripMenuItem, "DocumentsSelectNoneToolStripMenuItem")
        '
        'DocumentsToolStripSeparator2
        '
        Me.DocumentsToolStripSeparator2.Name = "DocumentsToolStripSeparator2"
        resources.ApplyResources(Me.DocumentsToolStripSeparator2, "DocumentsToolStripSeparator2")
        '
        'DocumentsSetTitleToolStripMenuItem
        '
        Me.DocumentsSetTitleToolStripMenuItem.Name = "DocumentsSetTitleToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSetTitleToolStripMenuItem, "DocumentsSetTitleToolStripMenuItem")
        '
        'DocumentsSetAuthorToolStripMenuItem
        '
        Me.DocumentsSetAuthorToolStripMenuItem.Name = "DocumentsSetAuthorToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSetAuthorToolStripMenuItem, "DocumentsSetAuthorToolStripMenuItem")
        '
        'DocumentsSetCategoryToolStripMenuItem
        '
        Me.DocumentsSetCategoryToolStripMenuItem.Name = "DocumentsSetCategoryToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSetCategoryToolStripMenuItem, "DocumentsSetCategoryToolStripMenuItem")
        '
        'DocumentsSetTaxYearToolStripMenuItem
        '
        Me.DocumentsSetTaxYearToolStripMenuItem.Name = "DocumentsSetTaxYearToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSetTaxYearToolStripMenuItem, "DocumentsSetTaxYearToolStripMenuItem")
        '
        'DocumentsToolStripSeparator3
        '
        Me.DocumentsToolStripSeparator3.Name = "DocumentsToolStripSeparator3"
        resources.ApplyResources(Me.DocumentsToolStripSeparator3, "DocumentsToolStripSeparator3")
        '
        'DocumentsDeleteToolStripMenuItem
        '
        resources.ApplyResources(Me.DocumentsDeleteToolStripMenuItem, "DocumentsDeleteToolStripMenuItem")
        Me.DocumentsDeleteToolStripMenuItem.Name = "DocumentsDeleteToolStripMenuItem"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewSetPreviewPixelDensityToolStripMenuItem, Me.ViewToolStripSeparator, Me.ViewToolBarToolStripMenuItem, Me.ViewStatusBarToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        resources.ApplyResources(Me.ViewToolStripMenuItem, "ViewToolStripMenuItem")
        '
        'ViewSetPreviewPixelDensityToolStripMenuItem
        '
        Me.ViewSetPreviewPixelDensityToolStripMenuItem.Name = "ViewSetPreviewPixelDensityToolStripMenuItem"
        resources.ApplyResources(Me.ViewSetPreviewPixelDensityToolStripMenuItem, "ViewSetPreviewPixelDensityToolStripMenuItem")
        '
        'ViewToolStripSeparator
        '
        Me.ViewToolStripSeparator.Name = "ViewToolStripSeparator"
        resources.ApplyResources(Me.ViewToolStripSeparator, "ViewToolStripSeparator")
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
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsOptionsToolStripMenuItem, Me.ToolsUploadProfilesToolStripMenuItem, Me.ToolsMoveDatabaseToolStripMenuItem})
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
        'ToolsMoveDatabaseToolStripMenuItem
        '
        Me.ToolsMoveDatabaseToolStripMenuItem.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.database
        Me.ToolsMoveDatabaseToolStripMenuItem.Name = "ToolsMoveDatabaseToolStripMenuItem"
        resources.ApplyResources(Me.ToolsMoveDatabaseToolStripMenuItem, "ToolsMoveDatabaseToolStripMenuItem")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpContentsToolStripMenuItem, Me.HelpToolStripSeparator, Me.HelpAboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'HelpContentsToolStripMenuItem
        '
        resources.ApplyResources(Me.HelpContentsToolStripMenuItem, "HelpContentsToolStripMenuItem")
        Me.HelpContentsToolStripMenuItem.Name = "HelpContentsToolStripMenuItem"
        '
        'HelpToolStripSeparator
        '
        Me.HelpToolStripSeparator.Name = "HelpToolStripSeparator"
        resources.ApplyResources(Me.HelpToolStripSeparator, "HelpToolStripSeparator")
        '
        'HelpAboutToolStripMenuItem
        '
        Me.HelpAboutToolStripMenuItem.Name = "HelpAboutToolStripMenuItem"
        resources.ApplyResources(Me.HelpAboutToolStripMenuItem, "HelpAboutToolStripMenuItem")
        '
        'HorizontalSplitContainer
        '
        Me.HorizontalSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.HorizontalSplitContainer, "HorizontalSplitContainer")
        Me.HorizontalSplitContainer.Name = "HorizontalSplitContainer"
        '
        'HorizontalSplitContainer.Panel1
        '
        Me.HorizontalSplitContainer.Panel1.Controls.Add(Me.SearchResultsPanel)
        '
        'HorizontalSplitContainer.Panel2
        '
        Me.HorizontalSplitContainer.Panel2.Controls.Add(Me.VerticalSplitContainer)
        Me.HorizontalSplitContainer.TabStop = False
        '
        'SearchResultsPanel
        '
        Me.SearchResultsPanel.Controls.Add(Me.DocumentsDataGridView)
        resources.ApplyResources(Me.SearchResultsPanel, "SearchResultsPanel")
        Me.SearchResultsPanel.Name = "SearchResultsPanel"
        '
        'DocumentsDataGridView
        '
        Me.DocumentsDataGridView.AllowUserToAddRows = False
        Me.DocumentsDataGridView.AllowUserToDeleteRows = False
        Me.DocumentsDataGridView.AllowUserToResizeRows = False
        Me.DocumentsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DocumentsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DocumentsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DocumentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DocumentsDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelectionColumn})
        Me.DocumentsDataGridView.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.MainViewModelBindingSource, "DocumentsEnabled", True))
        resources.ApplyResources(Me.DocumentsDataGridView, "DocumentsDataGridView")
        Me.DocumentsDataGridView.MultiSelect = False
        Me.DocumentsDataGridView.Name = "DocumentsDataGridView"
        Me.DocumentsDataGridView.RowHeadersVisible = False
        Me.DocumentsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DocumentsDataGridView.StandardTab = True
        '
        'SelectionColumn
        '
        Me.SelectionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        resources.ApplyResources(Me.SelectionColumn, "SelectionColumn")
        Me.SelectionColumn.Name = "SelectionColumn"
        '
        'MainViewModelBindingSource
        '
        Me.MainViewModelBindingSource.DataSource = GetType(PDFKeeper.Core.ViewModels.MainViewModel)
        '
        'VerticalSplitContainer
        '
        resources.ApplyResources(Me.VerticalSplitContainer, "VerticalSplitContainer")
        Me.VerticalSplitContainer.Name = "VerticalSplitContainer"
        '
        'VerticalSplitContainer.Panel1
        '
        Me.VerticalSplitContainer.Panel1.Controls.Add(Me.DocumentDataTabControl)
        '
        'VerticalSplitContainer.Panel2
        '
        Me.VerticalSplitContainer.Panel2.Controls.Add(Me.PreviewTabControl)
        Me.VerticalSplitContainer.TabStop = False
        '
        'DocumentDataTabControl
        '
        Me.DocumentDataTabControl.Controls.Add(Me.NotesTabPage)
        Me.DocumentDataTabControl.Controls.Add(Me.KeywordsTabPage)
        Me.DocumentDataTabControl.Controls.Add(Me.TextTabPage)
        Me.DocumentDataTabControl.Controls.Add(Me.SearchTermSnippetsTabPage)
        Me.DocumentDataTabControl.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.MainViewModelBindingSource, "DocumentDataEnabled", True))
        resources.ApplyResources(Me.DocumentDataTabControl, "DocumentDataTabControl")
        Me.DocumentDataTabControl.Multiline = True
        Me.DocumentDataTabControl.Name = "DocumentDataTabControl"
        Me.DocumentDataTabControl.SelectedIndex = 0
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
        Me.NotesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainViewModelBindingSource, "Notes", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
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
        Me.KeywordsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainViewModelBindingSource, "Keywords", True))
        resources.ApplyResources(Me.KeywordsTextBox, "KeywordsTextBox")
        Me.KeywordsTextBox.Name = "KeywordsTextBox"
        Me.KeywordsTextBox.ReadOnly = True
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
        Me.TextTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainViewModelBindingSource, "Text", True))
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
        Me.SearchTermSnippetsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainViewModelBindingSource, "SearchTermSnippets", True))
        resources.ApplyResources(Me.SearchTermSnippetsTextBox, "SearchTermSnippetsTextBox")
        Me.SearchTermSnippetsTextBox.Name = "SearchTermSnippetsTextBox"
        Me.SearchTermSnippetsTextBox.ReadOnly = True
        '
        'PreviewTabControl
        '
        Me.PreviewTabControl.Controls.Add(Me.PreviewTabPage)
        resources.ApplyResources(Me.PreviewTabControl, "PreviewTabControl")
        Me.PreviewTabControl.Name = "PreviewTabControl"
        Me.PreviewTabControl.SelectedIndex = 0
        Me.PreviewTabControl.TabStop = False
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
        Me.PreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PreviewPanel.Controls.Add(Me.PreviewPictureBox)
        Me.PreviewPanel.Name = "PreviewPanel"
        '
        'PreviewPictureBox
        '
        Me.PreviewPictureBox.DataBindings.Add(New System.Windows.Forms.Binding("Image", Me.MainViewModelBindingSource, "Preview", True))
        resources.ApplyResources(Me.PreviewPictureBox, "PreviewPictureBox")
        Me.PreviewPictureBox.Name = "PreviewPictureBox"
        Me.PreviewPictureBox.TabStop = False
        '
        'UploadTimer
        '
        Me.UploadTimer.Enabled = True
        Me.UploadTimer.Interval = 15000
        '
        'StatusStrip
        '
        Me.StatusStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.Presentation.My.MySettings.Default, "StatusBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocumentsLabel, Me.DocumentsCountLabel, Me.StatusStripFillerLabel, Me.ProgressBar, Me.RefreshingDocumentsImageLabel, Me.FlagImageLabel, Me.UploadRunningImageLabel, Me.UploadRejectedImageLabel})
        resources.ApplyResources(Me.StatusStrip, "StatusStrip")
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.ShowItemToolTips = True
        Me.StatusStrip.Visible = Global.PDFKeeper.Presentation.My.MySettings.Default.StatusBarVisible
        '
        'DocumentsLabel
        '
        Me.DocumentsLabel.Name = "DocumentsLabel"
        resources.ApplyResources(Me.DocumentsLabel, "DocumentsLabel")
        '
        'DocumentsCountLabel
        '
        Me.DocumentsCountLabel.Name = "DocumentsCountLabel"
        resources.ApplyResources(Me.DocumentsCountLabel, "DocumentsCountLabel")
        '
        'StatusStripFillerLabel
        '
        resources.ApplyResources(Me.StatusStripFillerLabel, "StatusStripFillerLabel")
        Me.StatusStripFillerLabel.Name = "StatusStripFillerLabel"
        Me.StatusStripFillerLabel.Spring = True
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        resources.ApplyResources(Me.ProgressBar, "ProgressBar")
        Me.ProgressBar.Step = 1
        '
        'RefreshingDocumentsImageLabel
        '
        resources.ApplyResources(Me.RefreshingDocumentsImageLabel, "RefreshingDocumentsImageLabel")
        Me.RefreshingDocumentsImageLabel.Name = "RefreshingDocumentsImageLabel"
        '
        'FlagImageLabel
        '
        resources.ApplyResources(Me.FlagImageLabel, "FlagImageLabel")
        Me.FlagImageLabel.Name = "FlagImageLabel"
        '
        'UploadRunningImageLabel
        '
        resources.ApplyResources(Me.UploadRunningImageLabel, "UploadRunningImageLabel")
        Me.UploadRunningImageLabel.Name = "UploadRunningImageLabel"
        '
        'UploadRejectedImageLabel
        '
        resources.ApplyResources(Me.UploadRejectedImageLabel, "UploadRejectedImageLabel")
        Me.UploadRejectedImageLabel.Name = "UploadRejectedImageLabel"
        '
        'ToolStrip
        '
        Me.ToolStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.Presentation.My.MySettings.Default, "ToolBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAddToolStripButton, Me.FileOpenToolStripButton, Me.FileSaveToolStripButton, Me.FileBurstToolStripButton, Me.FilePrintToolStripButton, Me.ToolBarToolStripSeparator1, Me.EditUndoToolStripButton, Me.EditCutToolStripButton, Me.EditCopyToolStripButton, Me.EditPasteToolStripButton, Me.EditRestoreToolStripButton, Me.EditAppendDateTimeToolStripButton, Me.EditAppendTextToolStripButton, Me.ToolBarToolStripSeparator2, Me.DocumentsFindToolStripButton, Me.DocumentsDeleteToolStripButton, Me.ToolBarToolStripSeparator3, Me.ToolsOptionsToolStripButton, Me.ToolsUploadProfilesToolStripButton, Me.ToolBarToolStripSeparator4, Me.HelpContentsToolStripButton})
        resources.ApplyResources(Me.ToolStrip, "ToolStrip")
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Visible = Global.PDFKeeper.Presentation.My.MySettings.Default.ToolBarVisible
        '
        'FileAddToolStripButton
        '
        Me.FileAddToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.FileAddToolStripButton, "FileAddToolStripButton")
        Me.FileAddToolStripButton.Name = "FileAddToolStripButton"
        Me.FileAddToolStripButton.Tag = ""
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
        'ToolBarToolStripSeparator1
        '
        Me.ToolBarToolStripSeparator1.Name = "ToolBarToolStripSeparator1"
        resources.ApplyResources(Me.ToolBarToolStripSeparator1, "ToolBarToolStripSeparator1")
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
        'EditAppendDateTimeToolStripButton
        '
        Me.EditAppendDateTimeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.EditAppendDateTimeToolStripButton, "EditAppendDateTimeToolStripButton")
        Me.EditAppendDateTimeToolStripButton.Name = "EditAppendDateTimeToolStripButton"
        '
        'EditAppendTextToolStripButton
        '
        Me.EditAppendTextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.EditAppendTextToolStripButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.page_text
        resources.ApplyResources(Me.EditAppendTextToolStripButton, "EditAppendTextToolStripButton")
        Me.EditAppendTextToolStripButton.Name = "EditAppendTextToolStripButton"
        '
        'ToolBarToolStripSeparator2
        '
        Me.ToolBarToolStripSeparator2.Name = "ToolBarToolStripSeparator2"
        resources.ApplyResources(Me.ToolBarToolStripSeparator2, "ToolBarToolStripSeparator2")
        '
        'DocumentsFindToolStripButton
        '
        Me.DocumentsFindToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.DocumentsFindToolStripButton, "DocumentsFindToolStripButton")
        Me.DocumentsFindToolStripButton.Name = "DocumentsFindToolStripButton"
        '
        'DocumentsDeleteToolStripButton
        '
        Me.DocumentsDeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.DocumentsDeleteToolStripButton, "DocumentsDeleteToolStripButton")
        Me.DocumentsDeleteToolStripButton.Name = "DocumentsDeleteToolStripButton"
        '
        'ToolBarToolStripSeparator3
        '
        Me.ToolBarToolStripSeparator3.Name = "ToolBarToolStripSeparator3"
        resources.ApplyResources(Me.ToolBarToolStripSeparator3, "ToolBarToolStripSeparator3")
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
        'ToolBarToolStripSeparator4
        '
        Me.ToolBarToolStripSeparator4.Name = "ToolBarToolStripSeparator4"
        resources.ApplyResources(Me.ToolBarToolStripSeparator4, "ToolBarToolStripSeparator4")
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
        'CheckForFlaggedDocumentsTimer
        '
        Me.CheckForFlaggedDocumentsTimer.Enabled = True
        Me.CheckForFlaggedDocumentsTimer.Interval = 5000
        '
        'DocumentsListTimedRefreshTimer
        '
        Me.DocumentsListTimedRefreshTimer.Enabled = True
        Me.DocumentsListTimedRefreshTimer.Interval = 60000
        '
        'CheckForDocumentsListChangesTimer
        '
        Me.CheckForDocumentsListChangesTimer.Enabled = True
        Me.CheckForDocumentsListChangesTimer.Interval = 5000
        '
        'DocumentsSetSubjectToolStripMenuItem
        '
        Me.DocumentsSetSubjectToolStripMenuItem.Name = "DocumentsSetSubjectToolStripMenuItem"
        resources.ApplyResources(Me.DocumentsSetSubjectToolStripMenuItem, "DocumentsSetSubjectToolStripMenuItem")
        '
        'MainView
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.HorizontalSplitContainer)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "MainView"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.HorizontalSplitContainer.Panel1.ResumeLayout(False)
        Me.HorizontalSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.HorizontalSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HorizontalSplitContainer.ResumeLayout(False)
        Me.SearchResultsPanel.ResumeLayout(False)
        CType(Me.DocumentsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MainViewModelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VerticalSplitContainer.Panel1.ResumeLayout(False)
        Me.VerticalSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.VerticalSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VerticalSplitContainer.ResumeLayout(False)
        Me.DocumentDataTabControl.ResumeLayout(False)
        Me.NotesTabPage.ResumeLayout(False)
        Me.NotesTabPage.PerformLayout()
        Me.KeywordsTabPage.ResumeLayout(False)
        Me.KeywordsTabPage.PerformLayout()
        Me.TextTabPage.ResumeLayout(False)
        Me.TextTabPage.PerformLayout()
        Me.SearchTermSnippetsTabPage.ResumeLayout(False)
        Me.SearchTermSnippetsTabPage.PerformLayout()
        Me.PreviewTabControl.ResumeLayout(False)
        Me.PreviewTabPage.ResumeLayout(False)
        Me.PreviewPanel.ResumeLayout(False)
        Me.PreviewPanel.PerformLayout()
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents HorizontalSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents FileAddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileOpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileSaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FilePrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilePrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditUndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditCutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditCopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditPasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditSelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditAppendDateTimeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewStatusBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsOptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsUploadProfilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpAboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileAddToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FileOpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FileSaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FilePrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DocumentsDeleteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBarToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditUndoToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditCutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditCopyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditPasteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditAppendDateTimeToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBarToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolsOptionsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolsUploadProfilesToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBarToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpContentsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DocumentsLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DocumentsCountLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ViewSetPreviewPixelDensityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents EditRestoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditRestoreToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UploadTimer As System.Windows.Forms.Timer
    Friend WithEvents UploadRunningImageLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UploadRejectedImageLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStripFillerLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UpdateCheckTimer As System.Windows.Forms.Timer
    Friend WithEvents EditFlagDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlagImageLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SearchResultsPanel As Panel
    Friend WithEvents EditAppendTextToolStripButton As ToolStripButton
    Friend WithEvents DocumentDataTabControl As TabControl
    Friend WithEvents NotesTabPage As TabPage
    Friend WithEvents NotesTextBox As TextBox
    Friend WithEvents KeywordsTabPage As TabPage
    Friend WithEvents KeywordsTextBox As TextBox
    Friend WithEvents TextTabPage As TabPage
    Friend WithEvents DocumentsDataGridView As DataGridView
    Friend WithEvents SelectionColumn As DataGridViewCheckBoxColumn
    Friend WithEvents ToolsMoveDatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents FileBurstToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileBurstToolStripButton As ToolStripButton
    Friend WithEvents SearchTermSnippetsTabPage As TabPage
    Friend WithEvents SearchTermSnippetsTextBox As TextBox
    Friend WithEvents VerticalSplitContainer As SplitContainer
    Friend WithEvents PreviewTabControl As TabControl
    Friend WithEvents PreviewTabPage As TabPage
    Friend WithEvents PreviewPanel As Panel
    Friend WithEvents PreviewPictureBox As PictureBox
    Friend WithEvents MainViewModelBindingSource As BindingSource
    Friend WithEvents TextTextBox As TextBox
    Friend WithEvents DocumentsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsFindToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents DocumentsSelectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsSelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsSelectNoneToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolBarToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents DocumentsFindToolStripButton As ToolStripButton
    Friend WithEvents DocumentsSetCategoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsSetTaxYearToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsDeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckForFlaggedDocumentsTimer As Timer
    Friend WithEvents EditAppendTextToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents RefreshingDocumentsImageLabel As ToolStripStatusLabel
    Friend WithEvents DocumentsListTimedRefreshTimer As Timer
    Friend WithEvents HelpProvider As HelpProvider
    Friend WithEvents CheckForDocumentsListChangesTimer As Timer
    Friend WithEvents DocumentsSetTitleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsSetAuthorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents DocumentsToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents DocumentsSetSubjectToolStripMenuItem As ToolStripMenuItem
End Class
