'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2015 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify it under
'* the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Partial Class DirectUploadConfigurationForm
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DirectUploadConfigurationForm))
		Me.groupBoxFolders = New System.Windows.Forms.GroupBox()
		Me.buttonEdit = New System.Windows.Forms.Button()
		Me.buttonDelete = New System.Windows.Forms.Button()
		Me.buttonNew = New System.Windows.Forms.Button()
		Me.listBoxFolders = New System.Windows.Forms.ListBox()
		Me.groupBoxFolderName = New System.Windows.Forms.GroupBox()
		Me.textBoxFolderName = New System.Windows.Forms.TextBox()
		Me.checkBoxUseExistingTitle = New System.Windows.Forms.CheckBox()
		Me.comboBoxTitlePreFill = New System.Windows.Forms.ComboBox()
		Me.groupBoxTitlePreFill = New System.Windows.Forms.GroupBox()
		Me.checkBoxUseExistingSubject = New System.Windows.Forms.CheckBox()
		Me.comboBoxSubjectPreFill = New System.Windows.Forms.ComboBox()
		Me.groupBoxSubjectPreFill = New System.Windows.Forms.GroupBox()
		Me.checkBoxUseExistingAuthor = New System.Windows.Forms.CheckBox()
		Me.comboBoxAuthorPreFill = New System.Windows.Forms.ComboBox()
		Me.groupBoxAuthorPreFill = New System.Windows.Forms.GroupBox()
		Me.textBoxKeywordsPreFill = New System.Windows.Forms.TextBox()
		Me.checkBoxUseExistingKeywords = New System.Windows.Forms.CheckBox()
		Me.groupBoxKeywordsPreFill = New System.Windows.Forms.GroupBox()
		Me.groupBoxProperties = New System.Windows.Forms.GroupBox()
		Me.buttonDiscard = New System.Windows.Forms.Button()
		Me.buttonSave = New System.Windows.Forms.Button()
		Me.buttonClose = New System.Windows.Forms.Button()
		Me.errorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.labelHelp = New System.Windows.Forms.Label()
		Me.groupBoxFolders.SuspendLayout
		Me.groupBoxFolderName.SuspendLayout
		Me.groupBoxTitlePreFill.SuspendLayout
		Me.groupBoxSubjectPreFill.SuspendLayout
		Me.groupBoxAuthorPreFill.SuspendLayout
		Me.groupBoxKeywordsPreFill.SuspendLayout
		Me.groupBoxProperties.SuspendLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'groupBoxFolders
		'
		Me.groupBoxFolders.Controls.Add(Me.buttonEdit)
		Me.groupBoxFolders.Controls.Add(Me.buttonDelete)
		Me.groupBoxFolders.Controls.Add(Me.buttonNew)
		Me.groupBoxFolders.Controls.Add(Me.listBoxFolders)
		resources.ApplyResources(Me.groupBoxFolders, "groupBoxFolders")
		Me.groupBoxFolders.Name = "groupBoxFolders"
		Me.groupBoxFolders.TabStop = false
		'
		'buttonEdit
		'
		resources.ApplyResources(Me.buttonEdit, "buttonEdit")
		Me.buttonEdit.Name = "buttonEdit"
		Me.buttonEdit.UseVisualStyleBackColor = true
		AddHandler Me.buttonEdit.Click, AddressOf Me.ButtonEditClick
		'
		'buttonDelete
		'
		resources.ApplyResources(Me.buttonDelete, "buttonDelete")
		Me.buttonDelete.Name = "buttonDelete"
		Me.buttonDelete.UseVisualStyleBackColor = true
		AddHandler Me.buttonDelete.Click, AddressOf Me.ButtonDeleteClick
		'
		'buttonNew
		'
		resources.ApplyResources(Me.buttonNew, "buttonNew")
		Me.buttonNew.Name = "buttonNew"
		Me.buttonNew.UseVisualStyleBackColor = true
		AddHandler Me.buttonNew.Click, AddressOf Me.ButtonNewClick
		'
		'listBoxFolders
		'
		Me.listBoxFolders.FormattingEnabled = true
		resources.ApplyResources(Me.listBoxFolders, "listBoxFolders")
		Me.listBoxFolders.Name = "listBoxFolders"
		Me.listBoxFolders.Sorted = true
		AddHandler Me.listBoxFolders.SelectedIndexChanged, AddressOf Me.ListBoxFoldersSelectedIndexChanged
		'
		'groupBoxFolderName
		'
		Me.groupBoxFolderName.Controls.Add(Me.textBoxFolderName)
		resources.ApplyResources(Me.groupBoxFolderName, "groupBoxFolderName")
		Me.groupBoxFolderName.Name = "groupBoxFolderName"
		Me.groupBoxFolderName.TabStop = false
		'
		'textBoxFolderName
		'
		resources.ApplyResources(Me.textBoxFolderName, "textBoxFolderName")
		Me.textBoxFolderName.Name = "textBoxFolderName"
		AddHandler Me.textBoxFolderName.TextChanged, AddressOf Me.TextAndComboBoxTextChanged
		'
		'checkBoxUseExistingTitle
		'
		resources.ApplyResources(Me.checkBoxUseExistingTitle, "checkBoxUseExistingTitle")
		Me.checkBoxUseExistingTitle.Name = "checkBoxUseExistingTitle"
		Me.checkBoxUseExistingTitle.UseVisualStyleBackColor = true
		'
		'comboBoxTitlePreFill
		'
		resources.ApplyResources(Me.comboBoxTitlePreFill, "comboBoxTitlePreFill")
		Me.comboBoxTitlePreFill.FormattingEnabled = true
		Me.comboBoxTitlePreFill.Name = "comboBoxTitlePreFill"
		Me.comboBoxTitlePreFill.Sorted = true
		AddHandler Me.comboBoxTitlePreFill.TextChanged, AddressOf Me.TextAndComboBoxTextChanged
		'
		'groupBoxTitlePreFill
		'
		Me.groupBoxTitlePreFill.Controls.Add(Me.checkBoxUseExistingTitle)
		Me.groupBoxTitlePreFill.Controls.Add(Me.comboBoxTitlePreFill)
		resources.ApplyResources(Me.groupBoxTitlePreFill, "groupBoxTitlePreFill")
		Me.groupBoxTitlePreFill.Name = "groupBoxTitlePreFill"
		Me.groupBoxTitlePreFill.TabStop = false
		'
		'checkBoxUseExistingSubject
		'
		resources.ApplyResources(Me.checkBoxUseExistingSubject, "checkBoxUseExistingSubject")
		Me.checkBoxUseExistingSubject.Name = "checkBoxUseExistingSubject"
		Me.checkBoxUseExistingSubject.UseVisualStyleBackColor = true
		'
		'comboBoxSubjectPreFill
		'
		resources.ApplyResources(Me.comboBoxSubjectPreFill, "comboBoxSubjectPreFill")
		Me.comboBoxSubjectPreFill.FormattingEnabled = true
		Me.comboBoxSubjectPreFill.Name = "comboBoxSubjectPreFill"
		AddHandler Me.comboBoxSubjectPreFill.TextChanged, AddressOf Me.TextAndComboBoxTextChanged
		'
		'groupBoxSubjectPreFill
		'
		Me.groupBoxSubjectPreFill.Controls.Add(Me.checkBoxUseExistingSubject)
		Me.groupBoxSubjectPreFill.Controls.Add(Me.comboBoxSubjectPreFill)
		resources.ApplyResources(Me.groupBoxSubjectPreFill, "groupBoxSubjectPreFill")
		Me.groupBoxSubjectPreFill.Name = "groupBoxSubjectPreFill"
		Me.groupBoxSubjectPreFill.TabStop = false
		'
		'checkBoxUseExistingAuthor
		'
		resources.ApplyResources(Me.checkBoxUseExistingAuthor, "checkBoxUseExistingAuthor")
		Me.checkBoxUseExistingAuthor.Name = "checkBoxUseExistingAuthor"
		Me.checkBoxUseExistingAuthor.UseVisualStyleBackColor = true
		'
		'comboBoxAuthorPreFill
		'
		resources.ApplyResources(Me.comboBoxAuthorPreFill, "comboBoxAuthorPreFill")
		Me.comboBoxAuthorPreFill.FormattingEnabled = true
		Me.comboBoxAuthorPreFill.Name = "comboBoxAuthorPreFill"
		Me.comboBoxAuthorPreFill.Sorted = true
		AddHandler Me.comboBoxAuthorPreFill.TextChanged, AddressOf Me.TextAndComboBoxTextChanged
		'
		'groupBoxAuthorPreFill
		'
		Me.groupBoxAuthorPreFill.Controls.Add(Me.checkBoxUseExistingAuthor)
		Me.groupBoxAuthorPreFill.Controls.Add(Me.comboBoxAuthorPreFill)
		resources.ApplyResources(Me.groupBoxAuthorPreFill, "groupBoxAuthorPreFill")
		Me.groupBoxAuthorPreFill.Name = "groupBoxAuthorPreFill"
		Me.groupBoxAuthorPreFill.TabStop = false
		'
		'textBoxKeywordsPreFill
		'
		resources.ApplyResources(Me.textBoxKeywordsPreFill, "textBoxKeywordsPreFill")
		Me.textBoxKeywordsPreFill.Name = "textBoxKeywordsPreFill"
		AddHandler Me.textBoxKeywordsPreFill.TextChanged, AddressOf Me.TextAndComboBoxTextChanged
		'
		'checkBoxUseExistingKeywords
		'
		resources.ApplyResources(Me.checkBoxUseExistingKeywords, "checkBoxUseExistingKeywords")
		Me.checkBoxUseExistingKeywords.Name = "checkBoxUseExistingKeywords"
		Me.checkBoxUseExistingKeywords.UseVisualStyleBackColor = true
		'
		'groupBoxKeywordsPreFill
		'
		Me.groupBoxKeywordsPreFill.Controls.Add(Me.textBoxKeywordsPreFill)
		Me.groupBoxKeywordsPreFill.Controls.Add(Me.checkBoxUseExistingKeywords)
		resources.ApplyResources(Me.groupBoxKeywordsPreFill, "groupBoxKeywordsPreFill")
		Me.groupBoxKeywordsPreFill.Name = "groupBoxKeywordsPreFill"
		Me.groupBoxKeywordsPreFill.TabStop = false
		'
		'groupBoxProperties
		'
		Me.groupBoxProperties.Controls.Add(Me.buttonDiscard)
		Me.groupBoxProperties.Controls.Add(Me.buttonSave)
		Me.groupBoxProperties.Controls.Add(Me.groupBoxFolderName)
		Me.groupBoxProperties.Controls.Add(Me.groupBoxKeywordsPreFill)
		Me.groupBoxProperties.Controls.Add(Me.groupBoxTitlePreFill)
		Me.groupBoxProperties.Controls.Add(Me.groupBoxSubjectPreFill)
		Me.groupBoxProperties.Controls.Add(Me.groupBoxAuthorPreFill)
		resources.ApplyResources(Me.groupBoxProperties, "groupBoxProperties")
		Me.groupBoxProperties.Name = "groupBoxProperties"
		Me.groupBoxProperties.TabStop = false
		'
		'buttonDiscard
		'
		Me.buttonDiscard.DialogResult = System.Windows.Forms.DialogResult.Cancel
		resources.ApplyResources(Me.buttonDiscard, "buttonDiscard")
		Me.buttonDiscard.Name = "buttonDiscard"
		Me.buttonDiscard.UseVisualStyleBackColor = true
		AddHandler Me.buttonDiscard.Click, AddressOf Me.ButtonDiscardClick
		'
		'buttonSave
		'
		resources.ApplyResources(Me.buttonSave, "buttonSave")
		Me.buttonSave.Name = "buttonSave"
		Me.buttonSave.UseVisualStyleBackColor = true
		AddHandler Me.buttonSave.Click, AddressOf Me.ButtonSaveClick
		'
		'buttonClose
		'
		resources.ApplyResources(Me.buttonClose, "buttonClose")
		Me.buttonClose.Name = "buttonClose"
		Me.buttonClose.UseVisualStyleBackColor = true
		AddHandler Me.buttonClose.Click, AddressOf Me.ButtonCloseClick
		'
		'errorProvider
		'
		Me.errorProvider.ContainerControl = Me
		'
		'labelHelp
		'
		resources.ApplyResources(Me.labelHelp, "labelHelp")
		Me.labelHelp.Name = "labelHelp"
		'
		'DirectUploadConfigurationForm
		'
		Me.AcceptButton = Me.buttonSave
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		Me.CancelButton = Me.buttonDiscard
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.labelHelp)
		Me.Controls.Add(Me.buttonClose)
		Me.Controls.Add(Me.groupBoxProperties)
		Me.Controls.Add(Me.groupBoxFolders)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DirectUploadConfigurationForm"
		AddHandler FormClosing, AddressOf Me.DirectUploadConfigurationFormFormClosing
		AddHandler Load, AddressOf Me.DirectUploadConfigurationFormLoad
		AddHandler HelpRequested, AddressOf Me.DirectUploadConfigurationFormHelpRequested
		Me.groupBoxFolders.ResumeLayout(false)
		Me.groupBoxFolderName.ResumeLayout(false)
		Me.groupBoxFolderName.PerformLayout
		Me.groupBoxTitlePreFill.ResumeLayout(false)
		Me.groupBoxSubjectPreFill.ResumeLayout(false)
		Me.groupBoxAuthorPreFill.ResumeLayout(false)
		Me.groupBoxKeywordsPreFill.ResumeLayout(false)
		Me.groupBoxKeywordsPreFill.PerformLayout
		Me.groupBoxProperties.ResumeLayout(false)
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
	End Sub
	Private labelHelp As System.Windows.Forms.Label
	Private buttonEdit As System.Windows.Forms.Button
	Private errorProvider As System.Windows.Forms.ErrorProvider
	Private buttonClose As System.Windows.Forms.Button
	Private buttonSave As System.Windows.Forms.Button
	Private buttonDiscard As System.Windows.Forms.Button
	Private groupBoxProperties As System.Windows.Forms.GroupBox
	Private groupBoxKeywordsPreFill As System.Windows.Forms.GroupBox
	Private checkBoxUseExistingKeywords As System.Windows.Forms.CheckBox
	Private textBoxKeywordsPreFill As System.Windows.Forms.TextBox
	Private groupBoxAuthorPreFill As System.Windows.Forms.GroupBox
	Private comboBoxAuthorPreFill As System.Windows.Forms.ComboBox
	Private checkBoxUseExistingAuthor As System.Windows.Forms.CheckBox
	Private groupBoxSubjectPreFill As System.Windows.Forms.GroupBox
	Private comboBoxSubjectPreFill As System.Windows.Forms.ComboBox
	Private checkBoxUseExistingSubject As System.Windows.Forms.CheckBox
	Private groupBoxTitlePreFill As System.Windows.Forms.GroupBox
	Private comboBoxTitlePreFill As System.Windows.Forms.ComboBox
	Private checkBoxUseExistingTitle As System.Windows.Forms.CheckBox
	Private textBoxFolderName As System.Windows.Forms.TextBox
	Private groupBoxFolderName As System.Windows.Forms.GroupBox
	Private listBoxFolders As System.Windows.Forms.ListBox
	Private buttonNew As System.Windows.Forms.Button
	Private buttonDelete As System.Windows.Forms.Button
	Private groupBoxFolders As System.Windows.Forms.GroupBox
End Class
