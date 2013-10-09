'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but
'* WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'*************************************************************************

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
		Me.columnHeaderID = New System.Windows.Forms.ColumnHeader(0)
		Me.columnHeaderTitle = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderAuthor = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderSubject = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderAdded = New System.Windows.Forms.ColumnHeader
		Me.statusStrip = New System.Windows.Forms.StatusStrip
		Me.toolStripStatusLabelListCount = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelUpdateStatus = New System.Windows.Forms.ToolStripStatusLabel
		Me.toolStripStatusLabelUpload = New System.Windows.Forms.ToolStripStatusLabel
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
		Me.menuStrip.SuspendLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).BeginInit
		Me.statusStrip.SuspendLayout
		Me.tabControlDocNotesKeywords.SuspendLayout
		Me.tabPageDocumentNotes.SuspendLayout
		Me.tabPageDocumentKeywords.SuspendLayout
		Me.SuspendLayout
		'
		'menuStrip
		'
		Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.viewToolStripMenuItem, Me.toolsToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.menuStrip.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip.Name = "menuStrip"
		Me.menuStrip.Size = New System.Drawing.Size(742, 24)
		Me.menuStrip.TabIndex = 0
		Me.menuStrip.Text = "menuStrip"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.savePDFtoDiskToolStripMenuItem, Me.toolStripSeparator1, Me.printDocumentNotesToolStripMenuItem, Me.toolStripSeparator2, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'savePDFtoDiskToolStripMenuItem
		'
		Me.savePDFtoDiskToolStripMenuItem.Enabled = false
		Me.savePDFtoDiskToolStripMenuItem.Image = CType(resources.GetObject("savePDFtoDiskToolStripMenuItem.Image"),System.Drawing.Image)
		Me.savePDFtoDiskToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.savePDFtoDiskToolStripMenuItem.Name = "savePDFtoDiskToolStripMenuItem"
		Me.savePDFtoDiskToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S"
		Me.savePDFtoDiskToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S),System.Windows.Forms.Keys)
		Me.savePDFtoDiskToolStripMenuItem.Size = New System.Drawing.Size(269, 22)
		Me.savePDFtoDiskToolStripMenuItem.Text = "&Save PDF Document to Disk..."
		AddHandler Me.savePDFtoDiskToolStripMenuItem.Click, AddressOf Me.SavePDFtoDiskToolStripMenuItemClick
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(266, 6)
		'
		'printDocumentNotesToolStripMenuItem
		'
		Me.printDocumentNotesToolStripMenuItem.Enabled = false
		Me.printDocumentNotesToolStripMenuItem.Image = CType(resources.GetObject("printDocumentNotesToolStripMenuItem.Image"),System.Drawing.Image)
		Me.printDocumentNotesToolStripMenuItem.Name = "printDocumentNotesToolStripMenuItem"
		Me.printDocumentNotesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+P"
		Me.printDocumentNotesToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P),System.Windows.Forms.Keys)
		Me.printDocumentNotesToolStripMenuItem.Size = New System.Drawing.Size(269, 22)
		Me.printDocumentNotesToolStripMenuItem.Text = "&Print Document Notes..."
		AddHandler Me.printDocumentNotesToolStripMenuItem.Click, AddressOf Me.PrintDocumentNotesToolStripMenuItemClick
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(266, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(269, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		AddHandler Me.exitToolStripMenuItem.Click, AddressOf Me.ExitToolStripMenuItemClick
		'
		'editToolStripMenuItem
		'
		Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.insertDateTimeIntoDocumentNotesToolStripMenuItem, Me.toolStripSeparator4, Me.checkAllToolStripMenuItem, Me.uncheckAllToolStripMenuItem, Me.deleteCheckedDocumentsToolStripMenuItem})
		Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
		Me.editToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
		Me.editToolStripMenuItem.Text = "&Edit"
		'
		'insertDateTimeIntoDocumentNotesToolStripMenuItem
		'
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Enabled = false
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Image = CType(resources.GetObject("insertDateTimeIntoDocumentNotesToolStripMenuItem.Image"),System.Drawing.Image)
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Name = "insertDateTimeIntoDocumentNotesToolStripMenuItem"
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T),System.Windows.Forms.Keys)
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Size = New System.Drawing.Size(320, 22)
		Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Text = "Insert Date/&Time into Document Notes"
		AddHandler Me.insertDateTimeIntoDocumentNotesToolStripMenuItem.Click, AddressOf Me.InsertDateTimeIntoDocumentNotesToolStripMenuItemClick
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(317, 6)
		'
		'checkAllToolStripMenuItem
		'
		Me.checkAllToolStripMenuItem.Enabled = false
		Me.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem"
		Me.checkAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C),System.Windows.Forms.Keys)
		Me.checkAllToolStripMenuItem.Size = New System.Drawing.Size(320, 22)
		Me.checkAllToolStripMenuItem.Text = "&Check All"
		AddHandler Me.checkAllToolStripMenuItem.Click, AddressOf Me.CheckAllToolStripMenuItemClick
		'
		'uncheckAllToolStripMenuItem
		'
		Me.uncheckAllToolStripMenuItem.Enabled = false
		Me.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem"
		Me.uncheckAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U),System.Windows.Forms.Keys)
		Me.uncheckAllToolStripMenuItem.Size = New System.Drawing.Size(320, 22)
		Me.uncheckAllToolStripMenuItem.Text = "&Uncheck All"
		AddHandler Me.uncheckAllToolStripMenuItem.Click, AddressOf Me.UncheckAllToolStripMenuItemClick
		'
		'deleteCheckedDocumentsToolStripMenuItem
		'
		Me.deleteCheckedDocumentsToolStripMenuItem.Enabled = false
		Me.deleteCheckedDocumentsToolStripMenuItem.Image = CType(resources.GetObject("deleteCheckedDocumentsToolStripMenuItem.Image"),System.Drawing.Image)
		Me.deleteCheckedDocumentsToolStripMenuItem.Name = "deleteCheckedDocumentsToolStripMenuItem"
		Me.deleteCheckedDocumentsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D),System.Windows.Forms.Keys)
		Me.deleteCheckedDocumentsToolStripMenuItem.Size = New System.Drawing.Size(320, 22)
		Me.deleteCheckedDocumentsToolStripMenuItem.Text = "&Delete Checked Documents..."
		AddHandler Me.deleteCheckedDocumentsToolStripMenuItem.Click, AddressOf Me.DeleteCheckedDocumentsToolStripMenuItemClick
		'
		'viewToolStripMenuItem
		'
		Me.viewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.refreshToolStripMenuItem})
		Me.viewToolStripMenuItem.Name = "viewToolStripMenuItem"
		Me.viewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.viewToolStripMenuItem.Text = "&View"
		'
		'refreshToolStripMenuItem
		'
		Me.refreshToolStripMenuItem.Enabled = false
		Me.refreshToolStripMenuItem.Image = CType(resources.GetObject("refreshToolStripMenuItem.Image"),System.Drawing.Image)
		Me.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem"
		Me.refreshToolStripMenuItem.ShortcutKeyDisplayString = "F5"
		Me.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
		Me.refreshToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.refreshToolStripMenuItem.Text = "&Refresh"
		AddHandler Me.refreshToolStripMenuItem.Click, AddressOf Me.ButtonSearchClick
		'
		'toolsToolStripMenuItem
		'
		Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.informationPropertiesEditorToolStripMenuItem, Me.uploadToolStripMenuItem, Me.toolStripSeparator3, Me.windowsExplorerToolStripMenuItem})
		Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
		Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
		Me.toolsToolStripMenuItem.Text = "&Tools"
		AddHandler Me.toolsToolStripMenuItem.Click, AddressOf Me.ToolsToolStripMenuItemClick
		'
		'informationPropertiesEditorToolStripMenuItem
		'
		Me.informationPropertiesEditorToolStripMenuItem.Name = "informationPropertiesEditorToolStripMenuItem"
		Me.informationPropertiesEditorToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I),System.Windows.Forms.Keys)
		Me.informationPropertiesEditorToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
		Me.informationPropertiesEditorToolStripMenuItem.Text = "&Information Properties Editor..."
		AddHandler Me.informationPropertiesEditorToolStripMenuItem.Click, AddressOf Me.InformationPropertiesEditorToolStripMenuItemClick
		'
		'uploadToolStripMenuItem
		'
		Me.uploadToolStripMenuItem.Image = CType(resources.GetObject("uploadToolStripMenuItem.Image"),System.Drawing.Image)
		Me.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem"
		Me.uploadToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
						Or System.Windows.Forms.Keys.U),System.Windows.Forms.Keys)
		Me.uploadToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
		Me.uploadToolStripMenuItem.Text = "&Upload..."
		AddHandler Me.uploadToolStripMenuItem.Click, AddressOf Me.UploadToolStripMenuItemClick
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(270, 6)
		'
		'windowsExplorerToolStripMenuItem
		'
		Me.windowsExplorerToolStripMenuItem.Name = "windowsExplorerToolStripMenuItem"
		Me.windowsExplorerToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W),System.Windows.Forms.Keys)
		Me.windowsExplorerToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
		Me.windowsExplorerToolStripMenuItem.Text = "&Windows Explorer"
		AddHandler Me.windowsExplorerToolStripMenuItem.Click, AddressOf Me.WindowsExplorerToolStripMenuItemClick
		'
		'helpToolStripMenuItem
		'
		Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.contentsToolStripMenuItem, Me.toolStripSeparator6, Me.checkNewerVersionToolStripMenuItem, Me.toolStripSeparator5, Me.aboutToolStripMenuItem})
		Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
		Me.helpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.helpToolStripMenuItem.Text = "&Help"
		AddHandler Me.helpToolStripMenuItem.Click, AddressOf Me.HelpToolStripMenuItemClick
		'
		'contentsToolStripMenuItem
		'
		Me.contentsToolStripMenuItem.Image = CType(resources.GetObject("contentsToolStripMenuItem.Image"),System.Drawing.Image)
		Me.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem"
		Me.contentsToolStripMenuItem.ShortcutKeyDisplayString = "F1"
		Me.contentsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
		Me.contentsToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
		Me.contentsToolStripMenuItem.Text = "&Contents"
		AddHandler Me.contentsToolStripMenuItem.Click, AddressOf Me.ContentsToolStripMenuItemClick
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		Me.toolStripSeparator6.Size = New System.Drawing.Size(278, 6)
		'
		'checkNewerVersionToolStripMenuItem
		'
		Me.checkNewerVersionToolStripMenuItem.Checked = true
		Me.checkNewerVersionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkNewerVersionToolStripMenuItem.Name = "checkNewerVersionToolStripMenuItem"
		Me.checkNewerVersionToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
		Me.checkNewerVersionToolStripMenuItem.Text = "Automatically Check for Newer &Version"
		AddHandler Me.checkNewerVersionToolStripMenuItem.Click, AddressOf Me.CheckNewerVersionToolStripMenuItemClick
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		Me.toolStripSeparator5.Size = New System.Drawing.Size(278, 6)
		'
		'aboutToolStripMenuItem
		'
		Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
		Me.aboutToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
		Me.aboutToolStripMenuItem.Text = "&About..."
		AddHandler Me.aboutToolStripMenuItem.Click, AddressOf Me.AboutToolStripMenuItemClick
		'
		'labelSearchText
		'
		Me.labelSearchText.Location = New System.Drawing.Point(12, 32)
		Me.labelSearchText.Name = "labelSearchText"
		Me.labelSearchText.Size = New System.Drawing.Size(75, 23)
		Me.labelSearchText.TabIndex = 1
		Me.labelSearchText.Text = "Search Text:"
		Me.labelSearchText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'errorProvider
		'
		Me.errorProvider.ContainerControl = Me
		'
		'buttonSearch
		'
		Me.buttonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonSearch.Enabled = false
		Me.buttonSearch.Image = CType(resources.GetObject("buttonSearch.Image"),System.Drawing.Image)
		Me.buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.buttonSearch.Location = New System.Drawing.Point(655, 33)
		Me.buttonSearch.Name = "buttonSearch"
		Me.buttonSearch.Size = New System.Drawing.Size(75, 23)
		Me.buttonSearch.TabIndex = 3
		Me.buttonSearch.Text = "&Search"
		Me.buttonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.buttonSearch.UseVisualStyleBackColor = true
		AddHandler Me.buttonSearch.Click, AddressOf Me.ButtonSearchClick
		'
		'listViewDocs
		'
		Me.listViewDocs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.listViewDocs.BackColor = System.Drawing.SystemColors.Window
		Me.listViewDocs.CheckBoxes = true
		Me.listViewDocs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeaderID, Me.columnHeaderTitle, Me.columnHeaderAuthor, Me.columnHeaderSubject, Me.columnHeaderAdded})
		Me.listViewDocs.FullRowSelect = true
		Me.listViewDocs.GridLines = true
		Me.listViewDocs.HideSelection = false
		Me.listViewDocs.Location = New System.Drawing.Point(0, 65)
		Me.listViewDocs.MultiSelect = false
		Me.listViewDocs.Name = "listViewDocs"
		Me.listViewDocs.Size = New System.Drawing.Size(742, 390)
		Me.listViewDocs.TabIndex = 4
		Me.listViewDocs.UseCompatibleStateImageBehavior = false
		Me.listViewDocs.View = System.Windows.Forms.View.Details
		AddHandler Me.listViewDocs.MouseDoubleClick, AddressOf Me.ListViewDocsMouseDoubleClick
		AddHandler Me.listViewDocs.ItemChecked, AddressOf Me.ListViewDocsItemChecked
		AddHandler Me.listViewDocs.SelectedIndexChanged, AddressOf Me.ListViewDocsSelectedIndexChanged
		AddHandler Me.listViewDocs.ColumnClick, AddressOf Me.ListViewDocsColumnClick
		'
		'columnHeaderID
		'
		Me.columnHeaderID.Text = "ID"
		'
		'columnHeaderTitle
		'
		Me.columnHeaderTitle.Text = "Title"
		Me.columnHeaderTitle.Width = 180
		'
		'columnHeaderAuthor
		'
		Me.columnHeaderAuthor.Text = "Author"
		Me.columnHeaderAuthor.Width = 180
		'
		'columnHeaderSubject
		'
		Me.columnHeaderSubject.Text = "Subject"
		Me.columnHeaderSubject.Width = 180
		'
		'columnHeaderAdded
		'
		Me.columnHeaderAdded.Text = "Added"
		Me.columnHeaderAdded.Width = 125
		'
		'statusStrip
		'
		Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabelListCount, Me.toolStripStatusLabelUpdateStatus, Me.toolStripStatusLabelUpload})
		Me.statusStrip.Location = New System.Drawing.Point(0, 654)
		Me.statusStrip.Name = "statusStrip"
		Me.statusStrip.Size = New System.Drawing.Size(742, 22)
		Me.statusStrip.TabIndex = 5
		AddHandler Me.statusStrip.ItemClicked, AddressOf Me.StatusStripItemClicked
		'
		'toolStripStatusLabelListCount
		'
		Me.toolStripStatusLabelListCount.Name = "toolStripStatusLabelListCount"
		Me.toolStripStatusLabelListCount.Size = New System.Drawing.Size(0, 17)
		'
		'toolStripStatusLabelUpdateStatus
		'
		Me.toolStripStatusLabelUpdateStatus.Name = "toolStripStatusLabelUpdateStatus"
		Me.toolStripStatusLabelUpdateStatus.Size = New System.Drawing.Size(727, 17)
		Me.toolStripStatusLabelUpdateStatus.Spring = true
		'
		'toolStripStatusLabelUpload
		'
		Me.toolStripStatusLabelUpload.Name = "toolStripStatusLabelUpload"
		Me.toolStripStatusLabelUpload.Size = New System.Drawing.Size(0, 17)
		'
		'saveFileDialog
		'
		Me.saveFileDialog.DefaultExt = "pdf"
		Me.saveFileDialog.Filter = "Adobe PDF Files|*.pdf"
		Me.saveFileDialog.RestoreDirectory = true
		Me.saveFileDialog.Title = "Save PDF File As"
		'
		'openFileDialog
		'
		Me.openFileDialog.DefaultExt = "pdf"
		Me.openFileDialog.Filter = "Adobe PDF Files|*.pdf"
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
		Me.comboBoxSearchText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.comboBoxSearchText.FormattingEnabled = true
		Me.comboBoxSearchText.Location = New System.Drawing.Point(93, 35)
		Me.comboBoxSearchText.MaxLength = 4000
		Me.comboBoxSearchText.Name = "comboBoxSearchText"
		Me.comboBoxSearchText.Size = New System.Drawing.Size(546, 21)
		Me.comboBoxSearchText.TabIndex = 2
		AddHandler Me.comboBoxSearchText.DropDown, AddressOf Me.ComboBoxSearchTextDropDown
		AddHandler Me.comboBoxSearchText.TextChanged, AddressOf Me.ComboBoxSearchTextTextChanged
		'
		'tabControlDocNotesKeywords
		'
		Me.tabControlDocNotesKeywords.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.tabControlDocNotesKeywords.Controls.Add(Me.tabPageDocumentNotes)
		Me.tabControlDocNotesKeywords.Controls.Add(Me.tabPageDocumentKeywords)
		Me.tabControlDocNotesKeywords.ImageList = Me.imageList
		Me.tabControlDocNotesKeywords.Location = New System.Drawing.Point(0, 455)
		Me.tabControlDocNotesKeywords.Name = "tabControlDocNotesKeywords"
		Me.tabControlDocNotesKeywords.SelectedIndex = 0
		Me.tabControlDocNotesKeywords.Size = New System.Drawing.Size(744, 200)
		Me.tabControlDocNotesKeywords.TabIndex = 6
		'
		'tabPageDocumentNotes
		'
		Me.tabPageDocumentNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.tabPageDocumentNotes.Controls.Add(Me.buttonDocumentNotesRevert)
		Me.tabPageDocumentNotes.Controls.Add(Me.buttonDocumentNotesUpdate)
		Me.tabPageDocumentNotes.Controls.Add(Me.textBoxDocumentNotes)
		Me.tabPageDocumentNotes.ImageKey = "DocumentNotes.gif"
		Me.tabPageDocumentNotes.Location = New System.Drawing.Point(4, 23)
		Me.tabPageDocumentNotes.Name = "tabPageDocumentNotes"
		Me.tabPageDocumentNotes.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPageDocumentNotes.Size = New System.Drawing.Size(736, 173)
		Me.tabPageDocumentNotes.TabIndex = 0
		Me.tabPageDocumentNotes.Text = "Document Notes"
		Me.tabPageDocumentNotes.UseVisualStyleBackColor = true
		'
		'buttonDocumentNotesRevert
		'
		Me.buttonDocumentNotesRevert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonDocumentNotesRevert.Enabled = false
		Me.buttonDocumentNotesRevert.Location = New System.Drawing.Point(656, 144)
		Me.buttonDocumentNotesRevert.Name = "buttonDocumentNotesRevert"
		Me.buttonDocumentNotesRevert.Size = New System.Drawing.Size(75, 23)
		Me.buttonDocumentNotesRevert.TabIndex = 2
		Me.buttonDocumentNotesRevert.Text = "&Revert"
		Me.buttonDocumentNotesRevert.UseVisualStyleBackColor = true
		AddHandler Me.buttonDocumentNotesRevert.Click, AddressOf Me.ButtonDocumentNotesRevertClick
		'
		'buttonDocumentNotesUpdate
		'
		Me.buttonDocumentNotesUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonDocumentNotesUpdate.Enabled = false
		Me.buttonDocumentNotesUpdate.Location = New System.Drawing.Point(575, 144)
		Me.buttonDocumentNotesUpdate.Name = "buttonDocumentNotesUpdate"
		Me.buttonDocumentNotesUpdate.Size = New System.Drawing.Size(75, 23)
		Me.buttonDocumentNotesUpdate.TabIndex = 1
		Me.buttonDocumentNotesUpdate.Text = "&Update"
		Me.buttonDocumentNotesUpdate.UseVisualStyleBackColor = true
		AddHandler Me.buttonDocumentNotesUpdate.Click, AddressOf Me.ButtonDocumentNotesUpdateClick
		'
		'textBoxDocumentNotes
		'
		Me.textBoxDocumentNotes.AcceptsReturn = true
		Me.textBoxDocumentNotes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBoxDocumentNotes.Enabled = false
		Me.textBoxDocumentNotes.Location = New System.Drawing.Point(6, 6)
		Me.textBoxDocumentNotes.MaxLength = 4000
		Me.textBoxDocumentNotes.Multiline = true
		Me.textBoxDocumentNotes.Name = "textBoxDocumentNotes"
		Me.textBoxDocumentNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.textBoxDocumentNotes.Size = New System.Drawing.Size(724, 132)
		Me.textBoxDocumentNotes.TabIndex = 0
		AddHandler Me.textBoxDocumentNotes.TextChanged, AddressOf Me.TextBoxDocumentNotesTextChanged
		'
		'tabPageDocumentKeywords
		'
		Me.tabPageDocumentKeywords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.tabPageDocumentKeywords.Controls.Add(Me.textBoxDocumentKeywords)
		Me.tabPageDocumentKeywords.ImageKey = "DocumentKeywords.gif"
		Me.tabPageDocumentKeywords.Location = New System.Drawing.Point(4, 23)
		Me.tabPageDocumentKeywords.Name = "tabPageDocumentKeywords"
		Me.tabPageDocumentKeywords.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPageDocumentKeywords.Size = New System.Drawing.Size(736, 173)
		Me.tabPageDocumentKeywords.TabIndex = 1
		Me.tabPageDocumentKeywords.Text = "Document Keywords"
		Me.tabPageDocumentKeywords.UseVisualStyleBackColor = true
		'
		'textBoxDocumentKeywords
		'
		Me.textBoxDocumentKeywords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBoxDocumentKeywords.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.textBoxDocumentKeywords.Enabled = false
		Me.textBoxDocumentKeywords.Location = New System.Drawing.Point(6, 6)
		Me.textBoxDocumentKeywords.MaxLength = 4000
		Me.textBoxDocumentKeywords.Multiline = true
		Me.textBoxDocumentKeywords.Name = "textBoxDocumentKeywords"
		Me.textBoxDocumentKeywords.ReadOnly = true
		Me.textBoxDocumentKeywords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.textBoxDocumentKeywords.Size = New System.Drawing.Size(724, 161)
		Me.textBoxDocumentKeywords.TabIndex = 0
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
		'MainForm
		'
		Me.AcceptButton = Me.buttonSearch
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(742, 676)
		Me.Controls.Add(Me.tabControlDocNotesKeywords)
		Me.Controls.Add(Me.comboBoxSearchText)
		Me.Controls.Add(Me.statusStrip)
		Me.Controls.Add(Me.listViewDocs)
		Me.Controls.Add(Me.buttonSearch)
		Me.Controls.Add(Me.labelSearchText)
		Me.Controls.Add(Me.menuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MainMenuStrip = Me.menuStrip
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "PDFKeeper"
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
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
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
