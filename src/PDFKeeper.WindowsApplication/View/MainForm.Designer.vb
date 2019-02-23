'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
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
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileNewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ViewToggleRightPanelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewRefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewSetPreviewImageResolutionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewToolbarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewStatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolsUploadFoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpAboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchStringErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.SearchResultsDataGridView = New System.Windows.Forms.DataGridView()
        Me.SelectionColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SearchPanel = New System.Windows.Forms.Panel()
        Me.SearchOptionsTabControl = New System.Windows.Forms.TabControl()
        Me.SearchByStringTabPage = New System.Windows.Forms.TabPage()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.SearchStringComboBox = New System.Windows.Forms.ComboBox()
        Me.SearchByAuthorTabPage = New System.Windows.Forms.TabPage()
        Me.Author1ComboBox = New System.Windows.Forms.ComboBox()
        Me.SearchBySubjectTabPage = New System.Windows.Forms.TabPage()
        Me.Subject1ComboBox = New System.Windows.Forms.ComboBox()
        Me.SearchByAuthorAndSubjectTabPage = New System.Windows.Forms.TabPage()
        Me.Subject2ComboBox = New System.Windows.Forms.ComboBox()
        Me.Author2ComboBox = New System.Windows.Forms.ComboBox()
        Me.SearchByDateAddedTabPage = New System.Windows.Forms.TabPage()
        Me.SearchDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.AllFlaggedDocumentsTabPage = New System.Windows.Forms.TabPage()
        Me.FlaggedDocumentsOnlyCheckBox = New System.Windows.Forms.CheckBox()
        Me.QueryDocumentsButton = New System.Windows.Forms.Button()
        Me.DBDocumentRecordsCountLabel = New System.Windows.Forms.Label()
        Me.RightTabControl = New System.Windows.Forms.TabControl()
        Me.NotesTabPage = New System.Windows.Forms.TabPage()
        Me.NotesTextBox = New System.Windows.Forms.TextBox()
        Me.KeywordsTabPage = New System.Windows.Forms.TabPage()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.PreviewTabPage = New System.Windows.Forms.TabPage()
        Me.PreviewPanel = New System.Windows.Forms.Panel()
        Me.PreviewPictureBox = New System.Windows.Forms.PictureBox()
        Me.TextTabPage = New System.Windows.Forms.TabPage()
        Me.TextTextBox = New System.Windows.Forms.TextBox()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.TotalRecordsToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TotalRecordsCountToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.DeleteExportToolStripProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.FillerToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadRunningToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadFolderErrorToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UploadStagingFolderErrorToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.FileNewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileOpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FileSaveToolStripButton = New System.Windows.Forms.ToolStripButton()
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
        Me.ViewToggleRightPanelToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ViewRefreshToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolsOptionsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolsUploadFoldersToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpContentsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.UploadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.AutoUpdateCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip.SuspendLayout()
        CType(Me.SearchStringErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        CType(Me.SearchResultsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SearchPanel.SuspendLayout()
        Me.SearchOptionsTabControl.SuspendLayout()
        Me.SearchByStringTabPage.SuspendLayout()
        Me.SearchByAuthorTabPage.SuspendLayout()
        Me.SearchBySubjectTabPage.SuspendLayout()
        Me.SearchByAuthorAndSubjectTabPage.SuspendLayout()
        Me.SearchByDateAddedTabPage.SuspendLayout()
        Me.AllFlaggedDocumentsTabPage.SuspendLayout()
        Me.RightTabControl.SuspendLayout()
        Me.NotesTabPage.SuspendLayout()
        Me.KeywordsTabPage.SuspendLayout()
        Me.PreviewTabPage.SuspendLayout()
        Me.PreviewPanel.SuspendLayout()
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TextTabPage.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip, "MenuStrip")
        Me.MenuStrip.Name = "MenuStrip"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileNewToolStripMenuItem, Me.ToolStripSeparator1, Me.FileOpenToolStripMenuItem, Me.ToolStripSeparator2, Me.FileSaveToolStripMenuItem, Me.FileSaveAsToolStripMenuItem, Me.ToolStripSeparator4, Me.FilePrintToolStripMenuItem, Me.FilePrintPreviewToolStripMenuItem, Me.ToolStripSeparator3, Me.FileSelectToolStripMenuItem, Me.FileDeleteToolStripMenuItem, Me.FileExportToolStripMenuItem, Me.ToolStripSeparator6, Me.FileExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'FileNewToolStripMenuItem
        '
        resources.ApplyResources(Me.FileNewToolStripMenuItem, "FileNewToolStripMenuItem")
        Me.FileNewToolStripMenuItem.Name = "FileNewToolStripMenuItem"
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
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewToggleRightPanelToolStripMenuItem, Me.ToolStripSeparator23, Me.ViewRefreshToolStripMenuItem, Me.ToolStripSeparator24, Me.ViewSetPreviewImageResolutionToolStripMenuItem, Me.ToolStripSeparator10, Me.ViewToolbarToolStripMenuItem, Me.ViewStatusBarToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        resources.ApplyResources(Me.ViewToolStripMenuItem, "ViewToolStripMenuItem")
        '
        'ViewToggleRightPanelToolStripMenuItem
        '
        resources.ApplyResources(Me.ViewToggleRightPanelToolStripMenuItem, "ViewToggleRightPanelToolStripMenuItem")
        Me.ViewToggleRightPanelToolStripMenuItem.Name = "ViewToggleRightPanelToolStripMenuItem"
        '
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        resources.ApplyResources(Me.ToolStripSeparator23, "ToolStripSeparator23")
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
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsOptionsToolStripMenuItem, Me.ToolStripSeparator11, Me.ToolsUploadFoldersToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        '
        'ToolsOptionsToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsOptionsToolStripMenuItem, "ToolsOptionsToolStripMenuItem")
        Me.ToolsOptionsToolStripMenuItem.Name = "ToolsOptionsToolStripMenuItem"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        resources.ApplyResources(Me.ToolStripSeparator11, "ToolStripSeparator11")
        '
        'ToolsUploadFoldersToolStripMenuItem
        '
        resources.ApplyResources(Me.ToolsUploadFoldersToolStripMenuItem, "ToolsUploadFoldersToolStripMenuItem")
        Me.ToolsUploadFoldersToolStripMenuItem.Name = "ToolsUploadFoldersToolStripMenuItem"
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
        'SearchStringErrorProvider
        '
        Me.SearchStringErrorProvider.ContainerControl = Me
        '
        'SplitContainer
        '
        Me.SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.SplitContainer, "SplitContainer")
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.SearchResultsDataGridView)
        Me.SplitContainer.Panel1.Controls.Add(Me.SearchPanel)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.RightTabControl)
        Me.SplitContainer.TabStop = False
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
        'SearchPanel
        '
        Me.SearchPanel.BackColor = System.Drawing.SystemColors.Window
        Me.SearchPanel.Controls.Add(Me.SearchOptionsTabControl)
        resources.ApplyResources(Me.SearchPanel, "SearchPanel")
        Me.SearchPanel.Name = "SearchPanel"
        '
        'SearchOptionsTabControl
        '
        Me.SearchOptionsTabControl.Controls.Add(Me.SearchByStringTabPage)
        Me.SearchOptionsTabControl.Controls.Add(Me.SearchByAuthorTabPage)
        Me.SearchOptionsTabControl.Controls.Add(Me.SearchBySubjectTabPage)
        Me.SearchOptionsTabControl.Controls.Add(Me.SearchByAuthorAndSubjectTabPage)
        Me.SearchOptionsTabControl.Controls.Add(Me.SearchByDateAddedTabPage)
        Me.SearchOptionsTabControl.Controls.Add(Me.AllFlaggedDocumentsTabPage)
        resources.ApplyResources(Me.SearchOptionsTabControl, "SearchOptionsTabControl")
        Me.SearchOptionsTabControl.Name = "SearchOptionsTabControl"
        Me.SearchOptionsTabControl.SelectedIndex = 0
        '
        'SearchByStringTabPage
        '
        Me.SearchByStringTabPage.Controls.Add(Me.SearchButton)
        Me.SearchByStringTabPage.Controls.Add(Me.SearchStringComboBox)
        resources.ApplyResources(Me.SearchByStringTabPage, "SearchByStringTabPage")
        Me.SearchByStringTabPage.Name = "SearchByStringTabPage"
        Me.SearchByStringTabPage.UseVisualStyleBackColor = True
        '
        'SearchButton
        '
        resources.ApplyResources(Me.SearchButton, "SearchButton")
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'SearchStringComboBox
        '
        resources.ApplyResources(Me.SearchStringComboBox, "SearchStringComboBox")
        Me.SearchStringComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SearchStringComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SearchStringComboBox.FormattingEnabled = True
        Me.SearchStringComboBox.Name = "SearchStringComboBox"
        Me.SearchStringComboBox.Sorted = True
        '
        'SearchByAuthorTabPage
        '
        Me.SearchByAuthorTabPage.Controls.Add(Me.Author1ComboBox)
        resources.ApplyResources(Me.SearchByAuthorTabPage, "SearchByAuthorTabPage")
        Me.SearchByAuthorTabPage.Name = "SearchByAuthorTabPage"
        Me.SearchByAuthorTabPage.UseVisualStyleBackColor = True
        '
        'Author1ComboBox
        '
        resources.ApplyResources(Me.Author1ComboBox, "Author1ComboBox")
        Me.Author1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Author1ComboBox.FormattingEnabled = True
        Me.Author1ComboBox.Name = "Author1ComboBox"
        Me.Author1ComboBox.Sorted = True
        '
        'SearchBySubjectTabPage
        '
        Me.SearchBySubjectTabPage.Controls.Add(Me.Subject1ComboBox)
        resources.ApplyResources(Me.SearchBySubjectTabPage, "SearchBySubjectTabPage")
        Me.SearchBySubjectTabPage.Name = "SearchBySubjectTabPage"
        Me.SearchBySubjectTabPage.UseVisualStyleBackColor = True
        '
        'Subject1ComboBox
        '
        resources.ApplyResources(Me.Subject1ComboBox, "Subject1ComboBox")
        Me.Subject1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Subject1ComboBox.FormattingEnabled = True
        Me.Subject1ComboBox.Name = "Subject1ComboBox"
        Me.Subject1ComboBox.Sorted = True
        '
        'SearchByAuthorAndSubjectTabPage
        '
        Me.SearchByAuthorAndSubjectTabPage.Controls.Add(Me.Subject2ComboBox)
        Me.SearchByAuthorAndSubjectTabPage.Controls.Add(Me.Author2ComboBox)
        resources.ApplyResources(Me.SearchByAuthorAndSubjectTabPage, "SearchByAuthorAndSubjectTabPage")
        Me.SearchByAuthorAndSubjectTabPage.Name = "SearchByAuthorAndSubjectTabPage"
        Me.SearchByAuthorAndSubjectTabPage.UseVisualStyleBackColor = True
        '
        'Subject2ComboBox
        '
        resources.ApplyResources(Me.Subject2ComboBox, "Subject2ComboBox")
        Me.Subject2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Subject2ComboBox.FormattingEnabled = True
        Me.Subject2ComboBox.Name = "Subject2ComboBox"
        Me.Subject2ComboBox.Sorted = True
        '
        'Author2ComboBox
        '
        resources.ApplyResources(Me.Author2ComboBox, "Author2ComboBox")
        Me.Author2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Author2ComboBox.FormattingEnabled = True
        Me.Author2ComboBox.Name = "Author2ComboBox"
        Me.Author2ComboBox.Sorted = True
        '
        'SearchByDateAddedTabPage
        '
        Me.SearchByDateAddedTabPage.Controls.Add(Me.SearchDateTimePicker)
        resources.ApplyResources(Me.SearchByDateAddedTabPage, "SearchByDateAddedTabPage")
        Me.SearchByDateAddedTabPage.Name = "SearchByDateAddedTabPage"
        Me.SearchByDateAddedTabPage.UseVisualStyleBackColor = True
        '
        'SearchDateTimePicker
        '
        resources.ApplyResources(Me.SearchDateTimePicker, "SearchDateTimePicker")
        Me.SearchDateTimePicker.Name = "SearchDateTimePicker"
        '
        'AllFlaggedDocumentsTabPage
        '
        Me.AllFlaggedDocumentsTabPage.Controls.Add(Me.FlaggedDocumentsOnlyCheckBox)
        Me.AllFlaggedDocumentsTabPage.Controls.Add(Me.QueryDocumentsButton)
        Me.AllFlaggedDocumentsTabPage.Controls.Add(Me.DBDocumentRecordsCountLabel)
        resources.ApplyResources(Me.AllFlaggedDocumentsTabPage, "AllFlaggedDocumentsTabPage")
        Me.AllFlaggedDocumentsTabPage.Name = "AllFlaggedDocumentsTabPage"
        Me.AllFlaggedDocumentsTabPage.UseVisualStyleBackColor = True
        '
        'FlaggedDocumentsOnlyCheckBox
        '
        resources.ApplyResources(Me.FlaggedDocumentsOnlyCheckBox, "FlaggedDocumentsOnlyCheckBox")
        Me.FlaggedDocumentsOnlyCheckBox.Name = "FlaggedDocumentsOnlyCheckBox"
        Me.FlaggedDocumentsOnlyCheckBox.UseVisualStyleBackColor = True
        '
        'QueryDocumentsButton
        '
        resources.ApplyResources(Me.QueryDocumentsButton, "QueryDocumentsButton")
        Me.QueryDocumentsButton.Name = "QueryDocumentsButton"
        Me.HelpProvider.SetShowHelp(Me.QueryDocumentsButton, CType(resources.GetObject("QueryDocumentsButton.ShowHelp"), Boolean))
        Me.QueryDocumentsButton.UseVisualStyleBackColor = True
        '
        'DBDocumentRecordsCountLabel
        '
        resources.ApplyResources(Me.DBDocumentRecordsCountLabel, "DBDocumentRecordsCountLabel")
        Me.DBDocumentRecordsCountLabel.Name = "DBDocumentRecordsCountLabel"
        '
        'RightTabControl
        '
        Me.RightTabControl.Controls.Add(Me.NotesTabPage)
        Me.RightTabControl.Controls.Add(Me.KeywordsTabPage)
        Me.RightTabControl.Controls.Add(Me.PreviewTabPage)
        Me.RightTabControl.Controls.Add(Me.TextTabPage)
        resources.ApplyResources(Me.RightTabControl, "RightTabControl")
        Me.RightTabControl.Name = "RightTabControl"
        Me.RightTabControl.SelectedIndex = 0
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
        'StatusStrip
        '
        Me.StatusStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "MainStatusBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TotalRecordsToolStripStatusLabel, Me.TotalRecordsCountToolStripStatusLabel, Me.DeleteExportToolStripProgressBar, Me.FillerToolStripStatusLabel, Me.UploadRunningToolStripStatusLabel, Me.UploadFolderErrorToolStripStatusLabel, Me.UploadStagingFolderErrorToolStripStatusLabel})
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
        'DeleteExportToolStripProgressBar
        '
        Me.DeleteExportToolStripProgressBar.Name = "DeleteExportToolStripProgressBar"
        resources.ApplyResources(Me.DeleteExportToolStripProgressBar, "DeleteExportToolStripProgressBar")
        Me.DeleteExportToolStripProgressBar.Step = 1
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
        'ToolStrip
        '
        Me.ToolStrip.DataBindings.Add(New System.Windows.Forms.Binding("Visible", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "MainToolBarVisible", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileNewToolStripButton, Me.FileOpenToolStripButton, Me.FileSaveToolStripButton, Me.FilePrintToolStripButton, Me.FileDeleteToolStripButton, Me.ToolStripSeparator17, Me.EditUndoToolStripButton, Me.EditCutToolStripButton, Me.EditCopyToolStripButton, Me.EditPasteToolStripButton, Me.EditRestoreToolStripButton, Me.EditDateTimeToolStripButton, Me.ToolStripSeparator20, Me.ViewToggleRightPanelToolStripButton, Me.ViewRefreshToolStripButton, Me.ToolStripSeparator21, Me.ToolsOptionsToolStripButton, Me.ToolsUploadFoldersToolStripButton, Me.ToolStripSeparator22, Me.HelpContentsToolStripButton})
        resources.ApplyResources(Me.ToolStrip, "ToolStrip")
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Visible = Global.PDFKeeper.WindowsApplication.My.MySettings.Default.MainToolBarVisible
        '
        'FileNewToolStripButton
        '
        Me.FileNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.FileNewToolStripButton, "FileNewToolStripButton")
        Me.FileNewToolStripButton.Name = "FileNewToolStripButton"
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
        'ViewToggleRightPanelToolStripButton
        '
        Me.ViewToggleRightPanelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ViewToggleRightPanelToolStripButton, "ViewToggleRightPanelToolStripButton")
        Me.ViewToggleRightPanelToolStripButton.Name = "ViewToggleRightPanelToolStripButton"
        '
        'ViewRefreshToolStripButton
        '
        Me.ViewRefreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ViewRefreshToolStripButton, "ViewRefreshToolStripButton")
        Me.ViewRefreshToolStripButton.Name = "ViewRefreshToolStripButton"
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
        'ToolsUploadFoldersToolStripButton
        '
        Me.ToolsUploadFoldersToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolsUploadFoldersToolStripButton, "ToolsUploadFoldersToolStripButton")
        Me.ToolsUploadFoldersToolStripButton.Name = "ToolsUploadFoldersToolStripButton"
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
        'UploadTimer
        '
        Me.UploadTimer.Enabled = True
        Me.UploadTimer.Interval = 5000
        '
        'HelpProvider
        '
        resources.ApplyResources(Me.HelpProvider, "HelpProvider")
        '
        'AutoUpdateCheckTimer
        '
        Me.AutoUpdateCheckTimer.Enabled = True
        Me.AutoUpdateCheckTimer.Interval = 1800000
        '
        'MainForm
        '
        Me.AcceptButton = Me.SearchButton
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
        CType(Me.SearchStringErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        CType(Me.SearchResultsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SearchPanel.ResumeLayout(False)
        Me.SearchOptionsTabControl.ResumeLayout(False)
        Me.SearchByStringTabPage.ResumeLayout(False)
        Me.SearchByAuthorTabPage.ResumeLayout(False)
        Me.SearchBySubjectTabPage.ResumeLayout(False)
        Me.SearchByAuthorAndSubjectTabPage.ResumeLayout(False)
        Me.SearchByDateAddedTabPage.ResumeLayout(False)
        Me.AllFlaggedDocumentsTabPage.ResumeLayout(False)
        Me.AllFlaggedDocumentsTabPage.PerformLayout()
        Me.RightTabControl.ResumeLayout(False)
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
    Friend WithEvents RightTabControl As System.Windows.Forms.TabControl
    Friend WithEvents NotesTabPage As System.Windows.Forms.TabPage
    Friend WithEvents KeywordsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents PreviewTabPage As System.Windows.Forms.TabPage
    Friend WithEvents TextTabPage As System.Windows.Forms.TabPage
    Friend WithEvents NotesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents KeywordsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TextTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FileNewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents ToolsUploadFoldersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpAboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileNewToolStripButton As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents ToolsUploadFoldersToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpContentsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SearchStringErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents SearchResultsDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents SearchPanel As System.Windows.Forms.Panel
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents SearchOptionsTabControl As System.Windows.Forms.TabControl
    Friend WithEvents SearchByStringTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SearchStringComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SearchByAuthorTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SearchByAuthorAndSubjectTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SearchByDateAddedTabPage As System.Windows.Forms.TabPage
    Friend WithEvents TotalRecordsToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TotalRecordsCountToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SearchDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ViewToggleRightPanelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator23 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ViewToggleRightPanelToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Author1ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Author2ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Subject2ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents PreviewPanel As System.Windows.Forms.Panel
    Friend WithEvents PreviewPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents ViewSetPreviewImageResolutionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectionColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DeleteExportToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents AllFlaggedDocumentsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SearchBySubjectTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Subject1ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents QueryDocumentsButton As System.Windows.Forms.Button
    Friend WithEvents DBDocumentRecordsCountLabel As System.Windows.Forms.Label
    Friend WithEvents EditRestoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditRestoreToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UploadTimer As System.Windows.Forms.Timer
    Friend WithEvents UploadRunningToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UploadFolderErrorToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents UploadStagingFolderErrorToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FillerToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents HelpProvider As System.Windows.Forms.HelpProvider
    Friend WithEvents AutoUpdateCheckTimer As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditFlagDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlaggedDocumentsOnlyCheckBox As System.Windows.Forms.CheckBox
End Class
