'******************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Partial Class MainForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.menuStrip = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.savePDFtoDiskToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.printDocumentNotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.checkAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.uncheckAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.deleteCheckedDocumentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.viewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.refreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.informationPropertiesEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.uploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.folderWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.informationPropertiesEditorWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.selectFolderInfoPropEditWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
		Me.enableInfoPropEditWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.disableInfoPropEditWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.uploadWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.selectFolderUploadWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
		Me.enableUploadWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.disableUploadWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
		Me.configurationUploadWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
		Me.windowsExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.contentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
		Me.checkNewerVersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.labelSearchText = New System.Windows.Forms.Label
		Me.errorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.buttonSearch = New System.Windows.Forms.Button
		Me.listViewDocs = New System.Windows.Forms.ListView
		Me.columnHeaderID = New System.Windows.Forms.ColumnHeader(CType(resources.GetObject("listViewDocs.Columns"),Integer))
		Me.columnHeaderTitle = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderAuthor = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderSubject = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderAdded = New System.Windows.Forms.ColumnHeader
		Me.statusStrip = New System.Windows.Forms.StatusStrip
		Me.toolStripStatusLabelListCount = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelUpdateStatus = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelUpload = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelUploaded = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelSkipped = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelInfoPropEditWatcher = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelUploadWatcher = New System.Windows.Forms.ToolStripStatusLabel
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.backgroundWorkerUpdateCheck = New System.ComponentModel.BackgroundWorker
		Me.comboBoxSearchText = New System.Windows.Forms.ComboBox
		Me.tabControlDocNotesKeywords = New System.Windows.Forms.TabControl
		Me.tabPageDocumentNotes = New System.Windows.Forms.TabPage
		Me.buttonDocumentNotesRevert = New System.Windows.Forms.Button
		Me.buttonDocumentNotesUpdate = New System.Windows.Forms.Button
		Me.textBoxDocumentNotes = New System.Windows.Forms.TextBox
		Me.tabPageDocumentKeywords = New System.Windows.Forms.TabPage
		Me.textBoxDocumentKeywords = New System.Windows.Forms.TextBox
		Me.imageList = New System.Windows.Forms.ImageList(Me.components)
		Me.printDocumentNotes = New System.Drawing.Printing.PrintDocument
		Me.printDialog = New System.Windows.Forms.PrintDialog
		Me.backgroundWorkerUpload = New System.ComponentModel.BackgroundWorker
		Me.processExplorer = New System.Diagnostics.Process
		Me.processHelp = New System.Diagnostics.Process
		Me.processPdfViewer = New System.Diagnostics.Process
		Me.fileSystemWatcherEditor = New System.IO.FileSystemWatcher
		Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
		Me.timerUpload = New System.Windows.Forms.Timer(Me.components)
		Me.processNotepad = New System.Diagnostics.Process
		Me.fileSystemWatcherUpload = New System.IO.FileSystemWatcher
		Me.menuStrip.SuspendLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).BeginInit
		Me.statusStrip.SuspendLayout
		Me.tabControlDocNotesKeywords.SuspendLayout
		Me.tabPageDocumentNotes.SuspendLayout
		Me.tabPageDocumentKeywords.SuspendLayout
		CType(Me.fileSystemWatcherEditor,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.fileSystemWatcherUpload,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'menuStrip
		'
		Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.viewToolStripMenuItem, Me.toolsToolStripMenuItem, Me.helpToolStripMenuItem})
		resources.ApplyResources(Me.menuStrip, "menuStrip")
		Me.menuStrip.Name = "menuStrip"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.savePDFtoDiskToolStripMenuItem, Me.toolStripSeparator1, Me.printDocumentNotesToolStripMenuItem, Me.toolStripSeparator2, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		resources.ApplyResources(Me.fileToolStripMenuItem, "fileToolStripMenuItem")
		'
		'savePDFtoDiskToolStripMenuItem
		'
		resources.ApplyResources(Me.savePDFtoDiskToolStripMenuItem, "savePDFtoDiskToolStripMenuItem")
		Me.savePDFtoDiskToolStripMenuItem.Name = "savePDFtoDiskToolStripMenuItem"
		AddHandler Me.savePDFtoDiskToolStripMenuItem.Click, AddressOf Me.SavePDFtoDiskToolStripMenuItemClick
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		resources.ApplyResources(Me.toolStripSeparator1, "toolStripSeparator1")
		'
		'printDocumentNotesToolStripMenuItem
		'
		resources.ApplyResources(Me.printDocumentNotesToolStripMenuItem, "printDocumentNotesToolStripMenuItem")
		Me.printDocumentNotesToolStripMenuItem.Name = "printDocumentNotesToolStripMenuItem"
		AddHandler Me.printDocumentNotesToolStripMenuItem.Click, AddressOf Me.PrintDocumentNotesToolStripMenuItemClick
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		resources.ApplyResources(Me.toolStripSeparator2, "toolStripSeparator2")
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		resources.ApplyResources(Me.exitToolStripMenuItem, "exitToolStripMenuItem")
		AddHandler Me.exitToolStripMenuItem.Click, AddressOf Me.ExitToolStripMenuItemClick
		'
		'editToolStripMenuItem
		'
		Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.insertDateTimeIntoDocumentNotesToolStripMenuItem, Me.toolStripSeparator4, Me.checkAllToolStripMenuItem, Me.uncheckAllToolStripMenuItem, Me.deleteCheckedDocumentsToolStripMenuItem})
		Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
		resources.ApplyResources(Me.editToolStripMenuItem, "editToolStripMenuItem")
		'
		'insertDateTimeIntoDocumentNotesToolStripMenuItem
		'
		resources.ApplyResources(Me.insertDateTimeIntoDocumentNotesToolStripMenuItem, "insertDateTimeIntoDocumentNotesToolStripMenuItem")
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Name = "insertDateTimeIntoDocumentNotesToolStripMenuItem"
		AddHandler Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Click, AddressOf Me.InsertDateTimeIntoDocumentNotesToolStripMenuItemClick
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		resources.ApplyResources(Me.toolStripSeparator4, "toolStripSeparator4")
		'
		'checkAllToolStripMenuItem
		'
		resources.ApplyResources(Me.checkAllToolStripMenuItem, "checkAllToolStripMenuItem")
		Me.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem"
		AddHandler Me.checkAllToolStripMenuItem.Click, AddressOf Me.CheckAllToolStripMenuItemClick
		'
		'uncheckAllToolStripMenuItem
		'
		resources.ApplyResources(Me.uncheckAllToolStripMenuItem, "uncheckAllToolStripMenuItem")
		Me.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem"
		AddHandler Me.uncheckAllToolStripMenuItem.Click, AddressOf Me.UncheckAllToolStripMenuItemClick
		'
		'deleteCheckedDocumentsToolStripMenuItem
		'
		resources.ApplyResources(Me.deleteCheckedDocumentsToolStripMenuItem, "deleteCheckedDocumentsToolStripMenuItem")
		Me.deleteCheckedDocumentsToolStripMenuItem.Name = "deleteCheckedDocumentsToolStripMenuItem"
		AddHandler Me.deleteCheckedDocumentsToolStripMenuItem.Click, AddressOf Me.DeleteCheckedDocumentsToolStripMenuItemClick
		'
		'viewToolStripMenuItem
		'
		Me.viewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.refreshToolStripMenuItem})
		Me.viewToolStripMenuItem.Name = "viewToolStripMenuItem"
		resources.ApplyResources(Me.viewToolStripMenuItem, "viewToolStripMenuItem")
		'
		'refreshToolStripMenuItem
		'
		resources.ApplyResources(Me.refreshToolStripMenuItem, "refreshToolStripMenuItem")
		Me.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem"
		AddHandler Me.refreshToolStripMenuItem.Click, AddressOf Me.ButtonSearchClick
		'
		'toolsToolStripMenuItem
		'
		Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.informationPropertiesEditorToolStripMenuItem, Me.uploadToolStripMenuItem, Me.toolStripSeparator3, Me.folderWatcherToolStripMenuItem, Me.toolStripSeparator7, Me.windowsExplorerToolStripMenuItem})
		Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
		resources.ApplyResources(Me.toolsToolStripMenuItem, "toolsToolStripMenuItem")
		AddHandler Me.toolsToolStripMenuItem.DropDownOpening, AddressOf Me.ToolsToolStripMenuItemClick
		AddHandler Me.toolsToolStripMenuItem.Click, AddressOf Me.ToolsToolStripMenuItemClick
		'
		'informationPropertiesEditorToolStripMenuItem
		'
		Me.informationPropertiesEditorToolStripMenuItem.Name = "informationPropertiesEditorToolStripMenuItem"
		resources.ApplyResources(Me.informationPropertiesEditorToolStripMenuItem, "informationPropertiesEditorToolStripMenuItem")
		AddHandler Me.informationPropertiesEditorToolStripMenuItem.Click, AddressOf Me.InformationPropertiesEditorToolStripMenuItemClick
		'
		'uploadToolStripMenuItem
		'
		resources.ApplyResources(Me.uploadToolStripMenuItem, "uploadToolStripMenuItem")
		Me.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem"
		AddHandler Me.uploadToolStripMenuItem.Click, AddressOf Me.UploadToolStripMenuItemClick
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		resources.ApplyResources(Me.toolStripSeparator3, "toolStripSeparator3")
		'
		'folderWatcherToolStripMenuItem
		'
		Me.folderWatcherToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.informationPropertiesEditorWatcherToolStripMenuItem, Me.uploadWatcherToolStripMenuItem})
		resources.ApplyResources(Me.folderWatcherToolStripMenuItem, "folderWatcherToolStripMenuItem")
		Me.folderWatcherToolStripMenuItem.Name = "folderWatcherToolStripMenuItem"
		'
		'informationPropertiesEditorWatcherToolStripMenuItem
		'
		Me.informationPropertiesEditorWatcherToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.selectFolderInfoPropEditWatcherToolStripMenuItem, Me.toolStripSeparator8, Me.enableInfoPropEditWatcherToolStripMenuItem, Me.disableInfoPropEditWatcherToolStripMenuItem})
		Me.informationPropertiesEditorWatcherToolStripMenuItem.Name = "informationPropertiesEditorWatcherToolStripMenuItem"
		resources.ApplyResources(Me.informationPropertiesEditorWatcherToolStripMenuItem, "informationPropertiesEditorWatcherToolStripMenuItem")
		AddHandler Me.informationPropertiesEditorWatcherToolStripMenuItem.DropDownOpening, AddressOf Me.InformationPropertiesEditorWatcherToolStripMenuItemClick
		AddHandler Me.informationPropertiesEditorWatcherToolStripMenuItem.Click, AddressOf Me.InformationPropertiesEditorWatcherToolStripMenuItemClick
		'
		'selectFolderInfoPropEditWatcherToolStripMenuItem
		'
		resources.ApplyResources(Me.selectFolderInfoPropEditWatcherToolStripMenuItem, "selectFolderInfoPropEditWatcherToolStripMenuItem")
		Me.selectFolderInfoPropEditWatcherToolStripMenuItem.Name = "selectFolderInfoPropEditWatcherToolStripMenuItem"
		AddHandler Me.selectFolderInfoPropEditWatcherToolStripMenuItem.Click, AddressOf Me.SelectFolderInfoPropEditWatcherToolStripMenuItemClick
		'
		'toolStripSeparator8
		'
		Me.toolStripSeparator8.Name = "toolStripSeparator8"
		resources.ApplyResources(Me.toolStripSeparator8, "toolStripSeparator8")
		'
		'enableInfoPropEditWatcherToolStripMenuItem
		'
		resources.ApplyResources(Me.enableInfoPropEditWatcherToolStripMenuItem, "enableInfoPropEditWatcherToolStripMenuItem")
		Me.enableInfoPropEditWatcherToolStripMenuItem.Name = "enableInfoPropEditWatcherToolStripMenuItem"
		AddHandler Me.enableInfoPropEditWatcherToolStripMenuItem.Click, AddressOf Me.EnableInfoPropEditWatcherToolStripMenuItemClick
		'
		'disableInfoPropEditWatcherToolStripMenuItem
		'
		resources.ApplyResources(Me.disableInfoPropEditWatcherToolStripMenuItem, "disableInfoPropEditWatcherToolStripMenuItem")
		Me.disableInfoPropEditWatcherToolStripMenuItem.Name = "disableInfoPropEditWatcherToolStripMenuItem"
		AddHandler Me.disableInfoPropEditWatcherToolStripMenuItem.Click, AddressOf Me.DisableInfoPropEditWatcherToolStripMenuItemClick
		'
		'uploadWatcherToolStripMenuItem
		'
		Me.uploadWatcherToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.selectFolderUploadWatcherToolStripMenuItem, Me.toolStripSeparator9, Me.enableUploadWatcherToolStripMenuItem, Me.disableUploadWatcherToolStripMenuItem, Me.toolStripSeparator10, Me.configurationUploadWatcherToolStripMenuItem})
		Me.uploadWatcherToolStripMenuItem.Name = "uploadWatcherToolStripMenuItem"
		resources.ApplyResources(Me.uploadWatcherToolStripMenuItem, "uploadWatcherToolStripMenuItem")
		AddHandler Me.uploadWatcherToolStripMenuItem.DropDownOpening, AddressOf Me.UploadWatcherToolStripMenuItemClick
		AddHandler Me.uploadWatcherToolStripMenuItem.Click, AddressOf Me.UploadWatcherToolStripMenuItemClick
		'
		'selectFolderUploadWatcherToolStripMenuItem
		'
		resources.ApplyResources(Me.selectFolderUploadWatcherToolStripMenuItem, "selectFolderUploadWatcherToolStripMenuItem")
		Me.selectFolderUploadWatcherToolStripMenuItem.Name = "selectFolderUploadWatcherToolStripMenuItem"
		AddHandler Me.selectFolderUploadWatcherToolStripMenuItem.Click, AddressOf Me.SelectFolderUploadWatcherToolStripMenuItemClick
		'
		'toolStripSeparator9
		'
		Me.toolStripSeparator9.Name = "toolStripSeparator9"
		resources.ApplyResources(Me.toolStripSeparator9, "toolStripSeparator9")
		'
		'enableUploadWatcherToolStripMenuItem
		'
		resources.ApplyResources(Me.enableUploadWatcherToolStripMenuItem, "enableUploadWatcherToolStripMenuItem")
		Me.enableUploadWatcherToolStripMenuItem.Name = "enableUploadWatcherToolStripMenuItem"
		AddHandler Me.enableUploadWatcherToolStripMenuItem.Click, AddressOf Me.EnableUploadWatcherToolStripMenuItemClick
		'
		'disableUploadWatcherToolStripMenuItem
		'
		resources.ApplyResources(Me.disableUploadWatcherToolStripMenuItem, "disableUploadWatcherToolStripMenuItem")
		Me.disableUploadWatcherToolStripMenuItem.Name = "disableUploadWatcherToolStripMenuItem"
		AddHandler Me.disableUploadWatcherToolStripMenuItem.Click, AddressOf Me.DisableUploadWatcherToolStripMenuItemClick
		'
		'toolStripSeparator10
		'
		Me.toolStripSeparator10.Name = "toolStripSeparator10"
		resources.ApplyResources(Me.toolStripSeparator10, "toolStripSeparator10")
		'
		'configurationUploadWatcherToolStripMenuItem
		'
		resources.ApplyResources(Me.configurationUploadWatcherToolStripMenuItem, "configurationUploadWatcherToolStripMenuItem")
		Me.configurationUploadWatcherToolStripMenuItem.Name = "configurationUploadWatcherToolStripMenuItem"
		AddHandler Me.configurationUploadWatcherToolStripMenuItem.Click, AddressOf Me.ConfigurationUploadWatcherToolStripMenuItemClick
		'
		'toolStripSeparator7
		'
		Me.toolStripSeparator7.Name = "toolStripSeparator7"
		resources.ApplyResources(Me.toolStripSeparator7, "toolStripSeparator7")
		'
		'windowsExplorerToolStripMenuItem
		'
		Me.windowsExplorerToolStripMenuItem.Name = "windowsExplorerToolStripMenuItem"
		resources.ApplyResources(Me.windowsExplorerToolStripMenuItem, "windowsExplorerToolStripMenuItem")
		AddHandler Me.windowsExplorerToolStripMenuItem.Click, AddressOf Me.WindowsExplorerToolStripMenuItemClick
		'
		'helpToolStripMenuItem
		'
		Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.contentsToolStripMenuItem, Me.toolStripSeparator6, Me.checkNewerVersionToolStripMenuItem, Me.toolStripSeparator5, Me.aboutToolStripMenuItem})
		Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
		resources.ApplyResources(Me.helpToolStripMenuItem, "helpToolStripMenuItem")
		AddHandler Me.helpToolStripMenuItem.Click, AddressOf Me.HelpToolStripMenuItemClick
		'
		'contentsToolStripMenuItem
		'
		resources.ApplyResources(Me.contentsToolStripMenuItem, "contentsToolStripMenuItem")
		Me.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem"
		AddHandler Me.contentsToolStripMenuItem.Click, AddressOf Me.ContentsToolStripMenuItemClick
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		resources.ApplyResources(Me.toolStripSeparator6, "toolStripSeparator6")
		'
		'checkNewerVersionToolStripMenuItem
		'
		Me.checkNewerVersionToolStripMenuItem.Checked = true
		Me.checkNewerVersionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkNewerVersionToolStripMenuItem.Name = "checkNewerVersionToolStripMenuItem"
		resources.ApplyResources(Me.checkNewerVersionToolStripMenuItem, "checkNewerVersionToolStripMenuItem")
		AddHandler Me.checkNewerVersionToolStripMenuItem.Click, AddressOf Me.CheckNewerVersionToolStripMenuItemClick
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		resources.ApplyResources(Me.toolStripSeparator5, "toolStripSeparator5")
		'
		'aboutToolStripMenuItem
		'
		Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
		resources.ApplyResources(Me.aboutToolStripMenuItem, "aboutToolStripMenuItem")
		AddHandler Me.aboutToolStripMenuItem.Click, AddressOf Me.AboutToolStripMenuItemClick
		'
		'labelSearchText
		'
		resources.ApplyResources(Me.labelSearchText, "labelSearchText")
		Me.labelSearchText.Name = "labelSearchText"
		'
		'errorProvider
		'
		Me.errorProvider.ContainerControl = Me
		'
		'buttonSearch
		'
		resources.ApplyResources(Me.buttonSearch, "buttonSearch")
		Me.buttonSearch.Name = "buttonSearch"
		Me.buttonSearch.UseVisualStyleBackColor = true
		AddHandler Me.buttonSearch.Click, AddressOf Me.ButtonSearchClick
		'
		'listViewDocs
		'
		resources.ApplyResources(Me.listViewDocs, "listViewDocs")
		Me.listViewDocs.BackColor = System.Drawing.SystemColors.Window
		Me.listViewDocs.CheckBoxes = true
		Me.listViewDocs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeaderID, Me.columnHeaderTitle, Me.columnHeaderAuthor, Me.columnHeaderSubject, Me.columnHeaderAdded})
		Me.listViewDocs.FullRowSelect = true
		Me.listViewDocs.GridLines = true
		Me.listViewDocs.HideSelection = false
		Me.listViewDocs.MultiSelect = false
		Me.listViewDocs.Name = "listViewDocs"
		Me.listViewDocs.UseCompatibleStateImageBehavior = false
		Me.listViewDocs.View = System.Windows.Forms.View.Details
		AddHandler Me.listViewDocs.MouseDoubleClick, AddressOf Me.ListViewDocsMouseDoubleClick
		AddHandler Me.listViewDocs.ItemChecked, AddressOf Me.ListViewDocsItemChecked
		AddHandler Me.listViewDocs.SelectedIndexChanged, AddressOf Me.ListViewDocsSelectedIndexChanged
		AddHandler Me.listViewDocs.ColumnClick, AddressOf Me.ListViewDocsColumnClick
		'
		'columnHeaderID
		'
		resources.ApplyResources(Me.columnHeaderID, "columnHeaderID")
		'
		'columnHeaderTitle
		'
		resources.ApplyResources(Me.columnHeaderTitle, "columnHeaderTitle")
		'
		'columnHeaderAuthor
		'
		resources.ApplyResources(Me.columnHeaderAuthor, "columnHeaderAuthor")
		'
		'columnHeaderSubject
		'
		resources.ApplyResources(Me.columnHeaderSubject, "columnHeaderSubject")
		'
		'columnHeaderAdded
		'
		resources.ApplyResources(Me.columnHeaderAdded, "columnHeaderAdded")
		'
		'statusStrip
		'
		Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabelListCount, Me.toolStripStatusLabelUpdateStatus, Me.toolStripStatusLabelUpload, Me.toolStripStatusLabelUploaded, Me.toolStripStatusLabelSkipped, Me.toolStripStatusLabelInfoPropEditWatcher, Me.toolStripStatusLabelUploadWatcher})
		resources.ApplyResources(Me.statusStrip, "statusStrip")
		Me.statusStrip.Name = "statusStrip"
		Me.statusStrip.ShowItemToolTips = true
		AddHandler Me.statusStrip.ItemClicked, AddressOf Me.StatusStripItemClicked
		'
		'toolStripStatusLabelListCount
		'
		Me.toolStripStatusLabelListCount.Name = "toolStripStatusLabelListCount"
		resources.ApplyResources(Me.toolStripStatusLabelListCount, "toolStripStatusLabelListCount")
		'
		'toolStripStatusLabelUpdateStatus
		'
		Me.toolStripStatusLabelUpdateStatus.Name = "toolStripStatusLabelUpdateStatus"
		resources.ApplyResources(Me.toolStripStatusLabelUpdateStatus, "toolStripStatusLabelUpdateStatus")
		Me.toolStripStatusLabelUpdateStatus.Spring = true
		'
		'toolStripStatusLabelUpload
		'
		Me.toolStripStatusLabelUpload.Name = "toolStripStatusLabelUpload"
		resources.ApplyResources(Me.toolStripStatusLabelUpload, "toolStripStatusLabelUpload")
		'
		'toolStripStatusLabelUploaded
		'
		resources.ApplyResources(Me.toolStripStatusLabelUploaded, "toolStripStatusLabelUploaded")
		Me.toolStripStatusLabelUploaded.Name = "toolStripStatusLabelUploaded"
		AddHandler Me.toolStripStatusLabelUploaded.Click, AddressOf Me.ToolStripStatusLabelUploadedClick
		'
		'toolStripStatusLabelSkipped
		'
		resources.ApplyResources(Me.toolStripStatusLabelSkipped, "toolStripStatusLabelSkipped")
		Me.toolStripStatusLabelSkipped.Name = "toolStripStatusLabelSkipped"
		AddHandler Me.toolStripStatusLabelSkipped.Click, AddressOf Me.ToolStripStatusLabelSkippedClick
		'
		'toolStripStatusLabelInfoPropEditWatcher
		'
		Me.toolStripStatusLabelInfoPropEditWatcher.AutoToolTip = true
		resources.ApplyResources(Me.toolStripStatusLabelInfoPropEditWatcher, "toolStripStatusLabelInfoPropEditWatcher")
		Me.toolStripStatusLabelInfoPropEditWatcher.Name = "toolStripStatusLabelInfoPropEditWatcher"
		AddHandler Me.toolStripStatusLabelInfoPropEditWatcher.Click, AddressOf Me.ToolStripStatusLabelInfoPropEditWatcherClick
		'
		'toolStripStatusLabelUploadWatcher
		'
		Me.toolStripStatusLabelUploadWatcher.AutoToolTip = true
		resources.ApplyResources(Me.toolStripStatusLabelUploadWatcher, "toolStripStatusLabelUploadWatcher")
		Me.toolStripStatusLabelUploadWatcher.Name = "toolStripStatusLabelUploadWatcher"
		AddHandler Me.toolStripStatusLabelUploadWatcher.Click, AddressOf Me.ToolStripStatusLabelPdfDocUploadWatcherClick
		'
		'saveFileDialog
		'
		Me.saveFileDialog.DefaultExt = "pdf"
		resources.ApplyResources(Me.saveFileDialog, "saveFileDialog")
		Me.saveFileDialog.RestoreDirectory = true
		'
		'openFileDialog
		'
		Me.openFileDialog.DefaultExt = "pdf"
		resources.ApplyResources(Me.openFileDialog, "openFileDialog")
		Me.openFileDialog.Multiselect = true
		Me.openFileDialog.RestoreDirectory = true
		'
		'backgroundWorkerUpdateCheck
		'
		AddHandler Me.backgroundWorkerUpdateCheck.DoWork, AddressOf Me.BackgroundWorkerUpdateCheckDoWork
		AddHandler Me.backgroundWorkerUpdateCheck.RunWorkerCompleted, AddressOf Me.BackgroundWorkerUpdateCheckRunWorkerCompleted
		'
		'comboBoxSearchText
		'
		resources.ApplyResources(Me.comboBoxSearchText, "comboBoxSearchText")
		Me.comboBoxSearchText.FormattingEnabled = true
		Me.comboBoxSearchText.Name = "comboBoxSearchText"
		AddHandler Me.comboBoxSearchText.DropDown, AddressOf Me.ComboBoxSearchTextDropDown
		AddHandler Me.comboBoxSearchText.TextChanged, AddressOf Me.ComboBoxSearchTextTextChanged
		'
		'tabControlDocNotesKeywords
		'
		resources.ApplyResources(Me.tabControlDocNotesKeywords, "tabControlDocNotesKeywords")
		Me.tabControlDocNotesKeywords.Controls.Add(Me.tabPageDocumentNotes)
		Me.tabControlDocNotesKeywords.Controls.Add(Me.tabPageDocumentKeywords)
		Me.tabControlDocNotesKeywords.ImageList = Me.imageList
		Me.tabControlDocNotesKeywords.Name = "tabControlDocNotesKeywords"
		Me.tabControlDocNotesKeywords.SelectedIndex = 0
		'
		'tabPageDocumentNotes
		'
		resources.ApplyResources(Me.tabPageDocumentNotes, "tabPageDocumentNotes")
		Me.tabPageDocumentNotes.Controls.Add(Me.buttonDocumentNotesRevert)
		Me.tabPageDocumentNotes.Controls.Add(Me.buttonDocumentNotesUpdate)
		Me.tabPageDocumentNotes.Controls.Add(Me.textBoxDocumentNotes)
		Me.tabPageDocumentNotes.Name = "tabPageDocumentNotes"
		Me.tabPageDocumentNotes.UseVisualStyleBackColor = true
		'
		'buttonDocumentNotesRevert
		'
		resources.ApplyResources(Me.buttonDocumentNotesRevert, "buttonDocumentNotesRevert")
		Me.buttonDocumentNotesRevert.Name = "buttonDocumentNotesRevert"
		Me.buttonDocumentNotesRevert.UseVisualStyleBackColor = true
		AddHandler Me.buttonDocumentNotesRevert.Click, AddressOf Me.ButtonDocumentNotesRevertClick
		'
		'buttonDocumentNotesUpdate
		'
		resources.ApplyResources(Me.buttonDocumentNotesUpdate, "buttonDocumentNotesUpdate")
		Me.buttonDocumentNotesUpdate.Name = "buttonDocumentNotesUpdate"
		Me.buttonDocumentNotesUpdate.UseVisualStyleBackColor = true
		AddHandler Me.buttonDocumentNotesUpdate.Click, AddressOf Me.ButtonDocumentNotesUpdateClick
		'
		'textBoxDocumentNotes
		'
		Me.textBoxDocumentNotes.AcceptsReturn = true
		resources.ApplyResources(Me.textBoxDocumentNotes, "textBoxDocumentNotes")
		Me.textBoxDocumentNotes.Name = "textBoxDocumentNotes"
		AddHandler Me.textBoxDocumentNotes.TextChanged, AddressOf Me.TextBoxDocumentNotesTextChanged
		'
		'tabPageDocumentKeywords
		'
		resources.ApplyResources(Me.tabPageDocumentKeywords, "tabPageDocumentKeywords")
		Me.tabPageDocumentKeywords.Controls.Add(Me.textBoxDocumentKeywords)
		Me.tabPageDocumentKeywords.Name = "tabPageDocumentKeywords"
		Me.tabPageDocumentKeywords.UseVisualStyleBackColor = true
		'
		'textBoxDocumentKeywords
		'
		resources.ApplyResources(Me.textBoxDocumentKeywords, "textBoxDocumentKeywords")
		Me.textBoxDocumentKeywords.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.textBoxDocumentKeywords.Name = "textBoxDocumentKeywords"
		Me.textBoxDocumentKeywords.ReadOnly = true
		Me.textBoxDocumentKeywords.TabStop = false
		'
		'imageList
		'
		Me.imageList.ImageStream = CType(resources.GetObject("imageList.ImageStream"),System.Windows.Forms.ImageListStreamer)
		Me.imageList.TransparentColor = System.Drawing.Color.Transparent
		Me.imageList.Images.SetKeyName(0, "DocumentNotes.gif")
		Me.imageList.Images.SetKeyName(1, "DocumentKeywords.gif")
		'
		'printDocumentNotes
		'
		AddHandler Me.printDocumentNotes.PrintPage, AddressOf Me.PrintDocumentNotesPrintPage
		'
		'printDialog
		'
		Me.printDialog.Document = Me.printDocumentNotes
		Me.printDialog.UseEXDialog = true
		'
		'backgroundWorkerUpload
		'
		AddHandler Me.backgroundWorkerUpload.DoWork, AddressOf Me.BackgroundWorkerUploadDoWork
		AddHandler Me.backgroundWorkerUpload.RunWorkerCompleted, AddressOf Me.BackgroundWorkerUploadRunWorkerCompleted
		'
		'processExplorer
		'
		Me.processExplorer.StartInfo.Domain = ""
		Me.processExplorer.StartInfo.ErrorDialog = true
		Me.processExplorer.StartInfo.LoadUserProfile = false
		Me.processExplorer.StartInfo.Password = Nothing
		Me.processExplorer.StartInfo.StandardErrorEncoding = Nothing
		Me.processExplorer.StartInfo.StandardOutputEncoding = Nothing
		Me.processExplorer.StartInfo.UserName = ""
		Me.processExplorer.SynchronizingObject = Me
		'
		'processHelp
		'
		Me.processHelp.StartInfo.Arguments = "..\Help\UserGuide.html"
		Me.processHelp.StartInfo.Domain = ""
		Me.processHelp.StartInfo.ErrorDialog = true
		Me.processHelp.StartInfo.FileName = "hh.exe"
		Me.processHelp.StartInfo.LoadUserProfile = false
		Me.processHelp.StartInfo.Password = Nothing
		Me.processHelp.StartInfo.StandardErrorEncoding = Nothing
		Me.processHelp.StartInfo.StandardOutputEncoding = Nothing
		Me.processHelp.StartInfo.UserName = ""
		Me.processHelp.SynchronizingObject = Me
		'
		'processPdfViewer
		'
		Me.processPdfViewer.StartInfo.Domain = ""
		Me.processPdfViewer.StartInfo.ErrorDialog = true
		Me.processPdfViewer.StartInfo.FileName = "SumatraPDF.exe"
		Me.processPdfViewer.StartInfo.LoadUserProfile = false
		Me.processPdfViewer.StartInfo.Password = Nothing
		Me.processPdfViewer.StartInfo.StandardErrorEncoding = Nothing
		Me.processPdfViewer.StartInfo.StandardOutputEncoding = Nothing
		Me.processPdfViewer.StartInfo.UserName = ""
		Me.processPdfViewer.StartInfo.UseShellExecute = false
		Me.processPdfViewer.SynchronizingObject = Me
		'
		'fileSystemWatcherEditor
		'
		Me.fileSystemWatcherEditor.EnableRaisingEvents = true
		Me.fileSystemWatcherEditor.Filter = "*.pdf"
		Me.fileSystemWatcherEditor.NotifyFilter = System.IO.NotifyFilters.FileName
		Me.fileSystemWatcherEditor.SynchronizingObject = Me
		AddHandler Me.fileSystemWatcherEditor.Created, AddressOf Me.FileSystemWatcherEditorCreated
		AddHandler Me.fileSystemWatcherEditor.[Error], AddressOf Me.FileSystemWatcherError
		'
		'folderBrowserDialog
		'
		Me.folderBrowserDialog.ShowNewFolderButton = false
		'
		'timerUpload
		'
		Me.timerUpload.Enabled = true
		Me.timerUpload.Interval = 15000
		AddHandler Me.timerUpload.Tick, AddressOf Me.TimerUploadTick
		'
		'processNotepad
		'
		Me.processNotepad.StartInfo.Domain = ""
		Me.processNotepad.StartInfo.ErrorDialog = true
		Me.processNotepad.StartInfo.FileName = "notepad.exe"
		Me.processNotepad.StartInfo.LoadUserProfile = false
		Me.processNotepad.StartInfo.Password = Nothing
		Me.processNotepad.StartInfo.StandardErrorEncoding = Nothing
		Me.processNotepad.StartInfo.StandardOutputEncoding = Nothing
		Me.processNotepad.StartInfo.UserName = ""
		Me.processNotepad.SynchronizingObject = Me
		'
		'fileSystemWatcherUpload
		'
		Me.fileSystemWatcherUpload.EnableRaisingEvents = true
		Me.fileSystemWatcherUpload.Filter = "*.pdf"
		Me.fileSystemWatcherUpload.NotifyFilter = System.IO.NotifyFilters.FileName
		Me.fileSystemWatcherUpload.SynchronizingObject = Me
		AddHandler Me.fileSystemWatcherUpload.Created, AddressOf Me.FileSystemWatcherUploadCreated
		AddHandler Me.fileSystemWatcherUpload.[Error], AddressOf Me.FileSystemWatcherError
		'
		'MainForm
		'
		Me.AcceptButton = Me.buttonSearch
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tabControlDocNotesKeywords)
		Me.Controls.Add(Me.comboBoxSearchText)
		Me.Controls.Add(Me.statusStrip)
		Me.Controls.Add(Me.listViewDocs)
		Me.Controls.Add(Me.buttonSearch)
		Me.Controls.Add(Me.labelSearchText)
		Me.Controls.Add(Me.menuStrip)
		Me.DoubleBuffered = true
		Me.MainMenuStrip = Me.menuStrip
		Me.Name = "MainForm"
		AddHandler FormClosed, AddressOf Me.MainFormFormClosed
		AddHandler FormClosing, AddressOf Me.MainFormClosing
		Me.menuStrip.ResumeLayout(false)
		Me.menuStrip.PerformLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).EndInit
		Me.statusStrip.ResumeLayout(false)
		Me.statusStrip.PerformLayout
		Me.tabControlDocNotesKeywords.ResumeLayout(false)
		Me.tabPageDocumentNotes.ResumeLayout(false)
		Me.tabPageDocumentNotes.PerformLayout
		Me.tabPageDocumentKeywords.ResumeLayout(false)
		Me.tabPageDocumentKeywords.PerformLayout
		CType(Me.fileSystemWatcherEditor,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.fileSystemWatcherUpload,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private toolStripStatusLabelUploadWatcher As System.Windows.Forms.ToolStripStatusLabel
	Private fileSystemWatcherUpload As System.IO.FileSystemWatcher
	Private selectFolderUploadWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private enableUploadWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private disableUploadWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private configurationUploadWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
	Private uploadWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private processNotepad As System.Diagnostics.Process
	Private toolStripStatusLabelSkipped As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabelUploaded As System.Windows.Forms.ToolStripStatusLabel
	Private timerUpload As System.Windows.Forms.Timer
	Private informationPropertiesEditorWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private selectFolderInfoPropEditWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private enableInfoPropEditWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private disableInfoPropEditWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripStatusLabelInfoPropEditWatcher As System.Windows.Forms.ToolStripStatusLabel
	Private folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
	Private toolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
	Private folderWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fileSystemWatcherEditor As System.IO.FileSystemWatcher
	Private processPdfViewer As System.Diagnostics.Process
	Private processHelp As System.Diagnostics.Process
	Private processExplorer As System.Diagnostics.Process
	Private backgroundWorkerUpload As System.ComponentModel.BackgroundWorker
	Private toolStripStatusLabelUpload As System.Windows.Forms.ToolStripStatusLabel
	Private buttonDocumentNotesRevert As System.Windows.Forms.Button
	Private insertDateTimeIntoDocumentNotesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private printDialog As System.Windows.Forms.PrintDialog
	Private printDocumentNotes As System.Drawing.Printing.PrintDocument
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private printDocumentNotesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private buttonDocumentNotesUpdate As System.Windows.Forms.Button
	Private textBoxDocumentKeywords As System.Windows.Forms.TextBox
	Private textBoxDocumentNotes As System.Windows.Forms.TextBox
	Private imageList As System.Windows.Forms.ImageList
	Private tabPageDocumentNotes As System.Windows.Forms.TabPage
	Private tabPageDocumentKeywords As System.Windows.Forms.TabPage
	Private tabControlDocNotesKeywords As System.Windows.Forms.TabControl
	Private comboBoxSearchText As System.Windows.Forms.ComboBox
	Private checkNewerVersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
	Private backgroundWorkerUpdateCheck As System.ComponentModel.BackgroundWorker
	Private toolStripStatusLabelListCount As System.Windows.Forms.ToolStripStatusLabel
	Private toolStripStatusLabelUpdateStatus As System.Windows.Forms.ToolStripStatusLabel
	Private windowsExplorerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private informationPropertiesEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private uploadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private refreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private viewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private deleteCheckedDocumentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private checkAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private uncheckAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private savePDFtoDiskToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private buttonSearch As System.Windows.Forms.Button
	Private columnHeaderAdded As System.Windows.Forms.ColumnHeader
	Private columnHeaderSubject As System.Windows.Forms.ColumnHeader
	Private columnHeaderAuthor As System.Windows.Forms.ColumnHeader
	Private columnHeaderTitle As System.Windows.Forms.ColumnHeader
	Private columnHeaderID As System.Windows.Forms.ColumnHeader
	Private listViewDocs As System.Windows.Forms.ListView
	Private statusStrip As System.Windows.Forms.StatusStrip
	Private errorProvider As System.Windows.Forms.ErrorProvider
	Private labelSearchText As System.Windows.Forms.Label
	Private aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private contentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private editToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private menuStrip As System.Windows.Forms.MenuStrip
End Class
