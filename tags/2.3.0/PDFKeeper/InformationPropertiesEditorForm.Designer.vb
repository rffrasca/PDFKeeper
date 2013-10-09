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

Partial Class InformationPropertiesEditorForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InformationPropertiesEditorForm))
		Me.groupBoxPDFDocument = New System.Windows.Forms.GroupBox
		Me.buttonView = New System.Windows.Forms.Button
		Me.textBoxPDFDocument = New System.Windows.Forms.TextBox
		Me.groupBoxProperties = New System.Windows.Forms.GroupBox
		Me.textBoxKeywords = New System.Windows.Forms.TextBox
		Me.labelKeywords = New System.Windows.Forms.Label
		Me.comboBoxSubject = New System.Windows.Forms.ComboBox
		Me.labelSubject = New System.Windows.Forms.Label
		Me.comboBoxAuthor = New System.Windows.Forms.ComboBox
		Me.labelAuthor = New System.Windows.Forms.Label
		Me.textBoxTitle = New System.Windows.Forms.TextBox
		Me.labelTitle = New System.Windows.Forms.Label
		Me.buttonSave = New System.Windows.Forms.Button
		Me.buttonCancel = New System.Windows.Forms.Button
		Me.checkBoxViewAfterSave = New System.Windows.Forms.CheckBox
		Me.groupBoxPDFDocument.SuspendLayout
		Me.groupBoxProperties.SuspendLayout
		Me.SuspendLayout
		'
		'groupBoxPDFDocument
		'
		Me.groupBoxPDFDocument.Controls.Add(Me.buttonView)
		Me.groupBoxPDFDocument.Controls.Add(Me.textBoxPDFDocument)
		Me.groupBoxPDFDocument.Location = New System.Drawing.Point(12, 12)
		Me.groupBoxPDFDocument.Name = "groupBoxPDFDocument"
		Me.groupBoxPDFDocument.Size = New System.Drawing.Size(566, 53)
		Me.groupBoxPDFDocument.TabIndex = 0
		Me.groupBoxPDFDocument.TabStop = false
		Me.groupBoxPDFDocument.Text = "PDF Document"
		'
		'buttonView
		'
		Me.buttonView.Image = CType(resources.GetObject("buttonView.Image"),System.Drawing.Image)
		Me.buttonView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.buttonView.Location = New System.Drawing.Point(483, 22)
		Me.buttonView.Name = "buttonView"
		Me.buttonView.Size = New System.Drawing.Size(75, 23)
		Me.buttonView.TabIndex = 1
		Me.buttonView.Text = "&View"
		Me.buttonView.UseVisualStyleBackColor = true
		AddHandler Me.buttonView.Click, AddressOf Me.ButtonViewClick
		'
		'textBoxPDFDocument
		'
		Me.textBoxPDFDocument.Location = New System.Drawing.Point(6, 24)
		Me.textBoxPDFDocument.MaxLength = 512
		Me.textBoxPDFDocument.Name = "textBoxPDFDocument"
		Me.textBoxPDFDocument.ReadOnly = true
		Me.textBoxPDFDocument.Size = New System.Drawing.Size(471, 20)
		Me.textBoxPDFDocument.TabIndex = 0
		Me.textBoxPDFDocument.TabStop = false
		'
		'groupBoxProperties
		'
		Me.groupBoxProperties.Controls.Add(Me.textBoxKeywords)
		Me.groupBoxProperties.Controls.Add(Me.labelKeywords)
		Me.groupBoxProperties.Controls.Add(Me.comboBoxSubject)
		Me.groupBoxProperties.Controls.Add(Me.labelSubject)
		Me.groupBoxProperties.Controls.Add(Me.comboBoxAuthor)
		Me.groupBoxProperties.Controls.Add(Me.labelAuthor)
		Me.groupBoxProperties.Controls.Add(Me.textBoxTitle)
		Me.groupBoxProperties.Controls.Add(Me.labelTitle)
		Me.groupBoxProperties.Location = New System.Drawing.Point(12, 81)
		Me.groupBoxProperties.Name = "groupBoxProperties"
		Me.groupBoxProperties.Size = New System.Drawing.Size(566, 228)
		Me.groupBoxProperties.TabIndex = 1
		Me.groupBoxProperties.TabStop = false
		Me.groupBoxProperties.Text = "PDF Document Information Properties"
		'
		'textBoxKeywords
		'
		Me.textBoxKeywords.Location = New System.Drawing.Point(87, 139)
		Me.textBoxKeywords.MaxLength = 4000
		Me.textBoxKeywords.Multiline = true
		Me.textBoxKeywords.Name = "textBoxKeywords"
		Me.textBoxKeywords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.textBoxKeywords.Size = New System.Drawing.Size(471, 72)
		Me.textBoxKeywords.TabIndex = 7
		AddHandler Me.textBoxKeywords.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelKeywords
		'
		Me.labelKeywords.Location = New System.Drawing.Point(6, 136)
		Me.labelKeywords.Name = "labelKeywords"
		Me.labelKeywords.Size = New System.Drawing.Size(75, 23)
		Me.labelKeywords.TabIndex = 6
		Me.labelKeywords.Text = "Keywords:"
		Me.labelKeywords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'comboBoxSubject
		'
		Me.comboBoxSubject.Location = New System.Drawing.Point(87, 102)
		Me.comboBoxSubject.MaxLength = 4000
		Me.comboBoxSubject.Name = "comboBoxSubject"
		Me.comboBoxSubject.Size = New System.Drawing.Size(471, 21)
		Me.comboBoxSubject.Sorted = true
		Me.comboBoxSubject.TabIndex = 5
		AddHandler Me.comboBoxSubject.SelectedIndexChanged, AddressOf Me.TextComboBoxTextChanged
		AddHandler Me.comboBoxSubject.DropDown, AddressOf Me.ComboBoxSubjectDropDown
		AddHandler Me.comboBoxSubject.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelSubject
		'
		Me.labelSubject.Location = New System.Drawing.Point(6, 99)
		Me.labelSubject.Name = "labelSubject"
		Me.labelSubject.Size = New System.Drawing.Size(75, 23)
		Me.labelSubject.TabIndex = 4
		Me.labelSubject.Text = "Subject:"
		Me.labelSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'comboBoxAuthor
		'
		Me.comboBoxAuthor.BackColor = System.Drawing.SystemColors.Window
		Me.comboBoxAuthor.Location = New System.Drawing.Point(87, 65)
		Me.comboBoxAuthor.MaxLength = 4000
		Me.comboBoxAuthor.Name = "comboBoxAuthor"
		Me.comboBoxAuthor.Size = New System.Drawing.Size(471, 21)
		Me.comboBoxAuthor.Sorted = true
		Me.comboBoxAuthor.TabIndex = 3
		AddHandler Me.comboBoxAuthor.SelectedIndexChanged, AddressOf Me.TextComboBoxTextChanged
		AddHandler Me.comboBoxAuthor.DropDown, AddressOf Me.ComboBoxAuthorDropDown
		AddHandler Me.comboBoxAuthor.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelAuthor
		'
		Me.labelAuthor.Location = New System.Drawing.Point(6, 62)
		Me.labelAuthor.Name = "labelAuthor"
		Me.labelAuthor.Size = New System.Drawing.Size(75, 23)
		Me.labelAuthor.TabIndex = 2
		Me.labelAuthor.Text = "Author:"
		Me.labelAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBoxTitle
		'
		Me.textBoxTitle.BackColor = System.Drawing.SystemColors.Window
		Me.textBoxTitle.Location = New System.Drawing.Point(87, 29)
		Me.textBoxTitle.MaxLength = 4000
		Me.textBoxTitle.Name = "textBoxTitle"
		Me.textBoxTitle.Size = New System.Drawing.Size(471, 20)
		Me.textBoxTitle.TabIndex = 1
		AddHandler Me.textBoxTitle.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelTitle
		'
		Me.labelTitle.Location = New System.Drawing.Point(6, 26)
		Me.labelTitle.Name = "labelTitle"
		Me.labelTitle.Size = New System.Drawing.Size(75, 23)
		Me.labelTitle.TabIndex = 0
		Me.labelTitle.Text = "Title:"
		Me.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'buttonSave
		'
		Me.buttonSave.Enabled = false
		Me.buttonSave.Image = CType(resources.GetObject("buttonSave.Image"),System.Drawing.Image)
		Me.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.buttonSave.Location = New System.Drawing.Point(422, 329)
		Me.buttonSave.Name = "buttonSave"
		Me.buttonSave.Size = New System.Drawing.Size(75, 23)
		Me.buttonSave.TabIndex = 3
		Me.buttonSave.Text = "&Save"
		Me.buttonSave.UseVisualStyleBackColor = true
		AddHandler Me.buttonSave.Click, AddressOf Me.ButtonSaveClick
		'
		'buttonCancel
		'
		Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.buttonCancel.Location = New System.Drawing.Point(503, 329)
		Me.buttonCancel.Name = "buttonCancel"
		Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
		Me.buttonCancel.TabIndex = 4
		Me.buttonCancel.Text = "Cancel"
		Me.buttonCancel.UseVisualStyleBackColor = true
		AddHandler Me.buttonCancel.Click, AddressOf Me.ButtonCancelClick
		'
		'checkBoxViewAfterSave
		'
		Me.checkBoxViewAfterSave.Location = New System.Drawing.Point(12, 329)
		Me.checkBoxViewAfterSave.Name = "checkBoxViewAfterSave"
		Me.checkBoxViewAfterSave.Size = New System.Drawing.Size(300, 24)
		Me.checkBoxViewAfterSave.TabIndex = 2
		Me.checkBoxViewAfterSave.Text = "&After saving, open PDF document in viewer."
		Me.checkBoxViewAfterSave.UseVisualStyleBackColor = true
		'
		'InformationPropertiesEditorForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonCancel
		Me.ClientSize = New System.Drawing.Size(590, 364)
		Me.Controls.Add(Me.checkBoxViewAfterSave)
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonSave)
		Me.Controls.Add(Me.groupBoxProperties)
		Me.Controls.Add(Me.groupBoxPDFDocument)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "InformationPropertiesEditorForm"
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Information Properties Editor"
		AddHandler Load, AddressOf Me.InformationPropertiesEditorFormLoad
		Me.groupBoxPDFDocument.ResumeLayout(false)
		Me.groupBoxPDFDocument.PerformLayout
		Me.groupBoxProperties.ResumeLayout(false)
		Me.groupBoxProperties.PerformLayout
		Me.ResumeLayout(false)
	End Sub
	Private checkBoxViewAfterSave As System.Windows.Forms.CheckBox
	Private buttonSave As System.Windows.Forms.Button
	Private buttonCancel As System.Windows.Forms.Button
	Private labelSubject As System.Windows.Forms.Label
	Private comboBoxSubject As System.Windows.Forms.ComboBox
	Private labelKeywords As System.Windows.Forms.Label
	Private textBoxKeywords As System.Windows.Forms.TextBox
	Private labelAuthor As System.Windows.Forms.Label
	Private comboBoxAuthor As System.Windows.Forms.ComboBox
	Private textBoxTitle As System.Windows.Forms.TextBox
	Private labelTitle As System.Windows.Forms.Label
	Private groupBoxProperties As System.Windows.Forms.GroupBox
	Private buttonView As System.Windows.Forms.Button
	Private textBoxPDFDocument As System.Windows.Forms.TextBox
	Private groupBoxPDFDocument As System.Windows.Forms.GroupBox
End Class
