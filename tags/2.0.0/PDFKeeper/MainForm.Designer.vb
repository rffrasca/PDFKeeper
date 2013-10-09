'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2010 Robert F. Frasca
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
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.documentNotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.checkAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.uncheckAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.deleteCheckedDocumentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.viewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.documentKeywordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.refreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.informationPropertiesEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.uploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.windowsExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.contentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.labelSearchText = New System.Windows.Forms.Label
		Me.textBoxSearchText = New System.Windows.Forms.TextBox
		Me.errorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.buttonSearch = New System.Windows.Forms.Button
		Me.listViewDocs = New System.Windows.Forms.ListView
		Me.columnHeaderID = New System.Windows.Forms.ColumnHeader(0)
		Me.columnHeaderTitle = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderAuthor = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderSubject = New System.Windows.Forms.ColumnHeader
		Me.columnHeaderAdded = New System.Windows.Forms.ColumnHeader
		Me.statusStrip = New System.Windows.Forms.StatusStrip
		Me.toolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.menuStrip.SuspendLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).BeginInit
		Me.statusStrip.SuspendLayout
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
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.savePDFtoDiskToolStripMenuItem, Me.toolStripSeparator1, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'savePDFtoDiskToolStripMenuItem
		'
		Me.savePDFtoDiskToolStripMenuItem.Enabled = false
		Me.savePDFtoDiskToolStripMenuItem.Image = CType(resources.GetObject("savePDFtoDiskToolStripMenuItem.Image"),System.Drawing.Image)
		Me.savePDFtoDiskToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.savePDFtoDiskToolStripMenuItem.Name = "savePDFtoDiskToolStripMenuItem"
		Me.savePDFtoDiskToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
		Me.savePDFtoDiskToolStripMenuItem.Text = "&Save PDF Document to Disk..."
		AddHandler Me.savePDFtoDiskToolStripMenuItem.Click, AddressOf Me.SavePDFtoDiskToolStripMenuItemClick
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(226, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		AddHandler Me.exitToolStripMenuItem.Click, AddressOf Me.ExitToolStripMenuItemClick
		'
		'editToolStripMenuItem
		'
		Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.documentNotesToolStripMenuItem, Me.toolStripSeparator4, Me.checkAllToolStripMenuItem, Me.uncheckAllToolStripMenuItem, Me.deleteCheckedDocumentsToolStripMenuItem})
		Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
		Me.editToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.editToolStripMenuItem.Text = "&Edit"
		'
		'documentNotesToolStripMenuItem
		'
		Me.documentNotesToolStripMenuItem.Enabled = false
		Me.documentNotesToolStripMenuItem.Image = CType(resources.GetObject("documentNotesToolStripMenuItem.Image"),System.Drawing.Image)
		Me.documentNotesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.documentNotesToolStripMenuItem.Name = "documentNotesToolStripMenuItem"
		Me.documentNotesToolStripMenuItem.Size = New System.Drawing.Size(228, 22)
		Me.documentNotesToolStripMenuItem.Text = "Document &Notes..."
		AddHandler Me.documentNotesToolStripMenuItem.Click, AddressOf Me.DocumentNotesToolStripMenuItemClick
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(225, 6)
		'
		'checkAllToolStripMenuItem
		'
		Me.checkAllToolStripMenuItem.Enabled = false
		Me.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem"
		Me.checkAllToolStripMenuItem.Size = New System.Drawing.Size(228, 22)
		Me.checkAllToolStripMenuItem.Text = "&Check All"
		AddHandler Me.checkAllToolStripMenuItem.Click, AddressOf Me.CheckAllToolStripMenuItemClick
		'
		'uncheckAllToolStripMenuItem
		'
		Me.uncheckAllToolStripMenuItem.Enabled = false
		Me.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem"
		Me.uncheckAllToolStripMenuItem.Size = New System.Drawing.Size(228, 22)
		Me.uncheckAllToolStripMenuItem.Text = "&Uncheck All"
		AddHandler Me.uncheckAllToolStripMenuItem.Click, AddressOf Me.UncheckAllToolStripMenuItemClick
		'
		'deleteCheckedDocumentsToolStripMenuItem
		'
		Me.deleteCheckedDocumentsToolStripMenuItem.Enabled = false
		Me.deleteCheckedDocumentsToolStripMenuItem.Image = CType(resources.GetObject("deleteCheckedDocumentsToolStripMenuItem.Image"),System.Drawing.Image)
		Me.deleteCheckedDocumentsToolStripMenuItem.Name = "deleteCheckedDocumentsToolStripMenuItem"
		Me.deleteCheckedDocumentsToolStripMenuItem.Size = New System.Drawing.Size(228, 22)
		Me.deleteCheckedDocumentsToolStripMenuItem.Text = "&Delete Checked Documents..."
		AddHandler Me.deleteCheckedDocumentsToolStripMenuItem.Click, AddressOf Me.DeleteCheckedDocumentsToolStripMenuItemClick
		'
		'viewToolStripMenuItem
		'
		Me.viewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.documentKeywordsToolStripMenuItem, Me.toolStripSeparator2, Me.refreshToolStripMenuItem})
		Me.viewToolStripMenuItem.Name = "viewToolStripMenuItem"
		Me.viewToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
		Me.viewToolStripMenuItem.Text = "&View"
		'
		'documentKeywordsToolStripMenuItem
		'
		Me.documentKeywordsToolStripMenuItem.Enabled = false
		Me.documentKeywordsToolStripMenuItem.Image = CType(resources.GetObject("documentKeywordsToolStripMenuItem.Image"),System.Drawing.Image)
		Me.documentKeywordsToolStripMenuItem.Name = "documentKeywordsToolStripMenuItem"
		Me.documentKeywordsToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
		Me.documentKeywordsToolStripMenuItem.Text = "Document &Keywords..."
		AddHandler Me.documentKeywordsToolStripMenuItem.Click, AddressOf Me.DocumentKeywordsToolStripMenuItemClick
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(192, 6)
		'
		'refreshToolStripMenuItem
		'
		Me.refreshToolStripMenuItem.Enabled = false
		Me.refreshToolStripMenuItem.Image = CType(resources.GetObject("refreshToolStripMenuItem.Image"),System.Drawing.Image)
		Me.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem"
		Me.refreshToolStripMenuItem.ShortcutKeyDisplayString = "F5"
		Me.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
		Me.refreshToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
		Me.refreshToolStripMenuItem.Text = "&Refresh"
		AddHandler Me.refreshToolStripMenuItem.Click, AddressOf Me.ButtonSearchClick
		'
		'toolsToolStripMenuItem
		'
		Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.informationPropertiesEditorToolStripMenuItem, Me.uploadToolStripMenuItem, Me.toolStripSeparator3, Me.windowsExplorerToolStripMenuItem})
		Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
		Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.toolsToolStripMenuItem.Text = "&Tools"
		'
		'informationPropertiesEditorToolStripMenuItem
		'
		Me.informationPropertiesEditorToolStripMenuItem.Name = "informationPropertiesEditorToolStripMenuItem"
		Me.informationPropertiesEditorToolStripMenuItem.Size = New System.Drawing.Size(236, 22)
		Me.informationPropertiesEditorToolStripMenuItem.Text = "&Information Properties Editor..."
		AddHandler Me.informationPropertiesEditorToolStripMenuItem.Click, AddressOf Me.InformationPropertiesEditorToolStripMenuItemClick
		'
		'uploadToolStripMenuItem
		'
		Me.uploadToolStripMenuItem.Image = CType(resources.GetObject("uploadToolStripMenuItem.Image"),System.Drawing.Image)
		Me.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem"
		Me.uploadToolStripMenuItem.Size = New System.Drawing.Size(236, 22)
		Me.uploadToolStripMenuItem.Text = "&Upload..."
		AddHandler Me.uploadToolStripMenuItem.Click, AddressOf Me.UploadToolStripMenuItemClick
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(233, 6)
		'
		'windowsExplorerToolStripMenuItem
		'
		Me.windowsExplorerToolStripMenuItem.Name = "windowsExplorerToolStripMenuItem"
		Me.windowsExplorerToolStripMenuItem.Size = New System.Drawing.Size(236, 22)
		Me.windowsExplorerToolStripMenuItem.Text = "Windows Explorer"
		AddHandler Me.windowsExplorerToolStripMenuItem.Click, AddressOf Me.WindowsExplorerToolStripMenuItemClick
		'
		'helpToolStripMenuItem
		'
		Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.contentsToolStripMenuItem, Me.toolStripSeparator5, Me.aboutToolStripMenuItem})
		Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
		Me.helpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
		Me.helpToolStripMenuItem.Text = "&Help"
		'
		'contentsToolStripMenuItem
		'
		Me.contentsToolStripMenuItem.Image = CType(resources.GetObject("contentsToolStripMenuItem.Image"),System.Drawing.Image)
		Me.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem"
		Me.contentsToolStripMenuItem.ShortcutKeyDisplayString = "F1"
		Me.contentsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
		Me.contentsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.contentsToolStripMenuItem.Text = "&Contents"
		AddHandler Me.contentsToolStripMenuItem.Click, AddressOf Me.ContentsToolStripMenuItemClick
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		Me.toolStripSeparator5.Size = New System.Drawing.Size(149, 6)
		'
		'aboutToolStripMenuItem
		'
		Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
		Me.aboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
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
		'textBoxSearchText
		'
		Me.textBoxSearchText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBoxSearchText.Location = New System.Drawing.Point(93, 35)
		Me.textBoxSearchText.MaxLength = 4000
		Me.textBoxSearchText.Name = "textBoxSearchText"
		Me.textBoxSearchText.Size = New System.Drawing.Size(546, 20)
		Me.textBoxSearchText.TabIndex = 2
		AddHandler Me.textBoxSearchText.TextChanged, AddressOf Me.TextBoxSearchTextTextChanged
		'
		'errorProvider
		'
		Me.errorProvider.ContainerControl = Me
		'
		'buttonSearch
		'
		Me.buttonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonSearch.Enabled = false
		Me.buttonSearch.Location = New System.Drawing.Point(655, 33)
		Me.buttonSearch.Name = "buttonSearch"
		Me.buttonSearch.Size = New System.Drawing.Size(75, 23)
		Me.buttonSearch.TabIndex = 3
		Me.buttonSearch.Text = "&Search"
		Me.buttonSearch.UseVisualStyleBackColor = true
		AddHandler Me.buttonSearch.Click, AddressOf Me.ButtonSearchClick
		'
		'listViewDocs
		'
		Me.listViewDocs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.listViewDocs.CheckBoxes = true
		Me.listViewDocs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeaderID, Me.columnHeaderTitle, Me.columnHeaderAuthor, Me.columnHeaderSubject, Me.columnHeaderAdded})
		Me.listViewDocs.FullRowSelect = true
		Me.listViewDocs.GridLines = true
		Me.listViewDocs.Location = New System.Drawing.Point(0, 65)
		Me.listViewDocs.MultiSelect = false
		Me.listViewDocs.Name = "listViewDocs"
		Me.listViewDocs.Size = New System.Drawing.Size(742, 430)
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
		Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel})
		Me.statusStrip.Location = New System.Drawing.Point(0, 494)
		Me.statusStrip.Name = "statusStrip"
		Me.statusStrip.Size = New System.Drawing.Size(742, 22)
		Me.statusStrip.TabIndex = 5
		'
		'toolStripStatusLabel
		'
		Me.toolStripStatusLabel.Name = "toolStripStatusLabel"
		Me.toolStripStatusLabel.Size = New System.Drawing.Size(0, 17)
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
		'MainForm
		'
		Me.AcceptButton = Me.buttonSearch
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(742, 516)
		Me.Controls.Add(Me.statusStrip)
		Me.Controls.Add(Me.listViewDocs)
		Me.Controls.Add(Me.buttonSearch)
		Me.Controls.Add(Me.textBoxSearchText)
		Me.Controls.Add(Me.labelSearchText)
		Me.Controls.Add(Me.menuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MainMenuStrip = Me.menuStrip
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "PDFKeeper"
		AddHandler FormClosed, AddressOf Me.MainFormFormClosed
		Me.menuStrip.ResumeLayout(false)
		Me.menuStrip.PerformLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).EndInit
		Me.statusStrip.ResumeLayout(false)
		Me.statusStrip.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private windowsExplorerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private informationPropertiesEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private uploadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private refreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private documentKeywordsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private viewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private deleteCheckedDocumentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private checkAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private uncheckAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private documentNotesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private savePDFtoDiskToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private buttonSearch As System.Windows.Forms.Button
	Private columnHeaderAdded As System.Windows.Forms.ColumnHeader
	Private columnHeaderSubject As System.Windows.Forms.ColumnHeader
	Private columnHeaderAuthor As System.Windows.Forms.ColumnHeader
	Private columnHeaderTitle As System.Windows.Forms.ColumnHeader
	Private columnHeaderID As System.Windows.Forms.ColumnHeader
	Private listViewDocs As System.Windows.Forms.ListView
	Private toolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
	Private statusStrip As System.Windows.Forms.StatusStrip
	Private errorProvider As System.Windows.Forms.ErrorProvider
	Private textBoxSearchText As System.Windows.Forms.TextBox
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
