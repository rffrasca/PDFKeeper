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

Partial Class UploadFolderWatcherConfigurationForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadFolderWatcherConfigurationForm))
		Me.groupBoxTitlePreFill = New System.Windows.Forms.GroupBox
		Me.checkBoxUseExistingTitle = New System.Windows.Forms.CheckBox
		Me.comboBoxTitlePreFill = New System.Windows.Forms.ComboBox
		Me.checkBoxUseExistingAuthor = New System.Windows.Forms.CheckBox
		Me.comboBoxAuthorPreFill = New System.Windows.Forms.ComboBox
		Me.groupBoxAuthorPreFill = New System.Windows.Forms.GroupBox
		Me.checkBoxUseExistingSubject = New System.Windows.Forms.CheckBox
		Me.comboBoxSubjectPreFill = New System.Windows.Forms.ComboBox
		Me.groupBoxSubjectPreFill = New System.Windows.Forms.GroupBox
		Me.groupBoxKeywordsPreFill = New System.Windows.Forms.GroupBox
		Me.textBoxKeywordsPreFill = New System.Windows.Forms.TextBox
		Me.checkBoxUseExistingKeywords = New System.Windows.Forms.CheckBox
		Me.checkBoxDeleteAfterUploaded = New System.Windows.Forms.CheckBox
		Me.labelDeletePdfWarning = New System.Windows.Forms.Label
		Me.buttonOK = New System.Windows.Forms.Button
		Me.buttonCancel = New System.Windows.Forms.Button
		Me.groupBoxTitlePreFill.SuspendLayout
		Me.groupBoxAuthorPreFill.SuspendLayout
		Me.groupBoxSubjectPreFill.SuspendLayout
		Me.groupBoxKeywordsPreFill.SuspendLayout
		Me.SuspendLayout
		'
		'groupBoxTitlePreFill
		'
		Me.groupBoxTitlePreFill.Controls.Add(Me.checkBoxUseExistingTitle)
		Me.groupBoxTitlePreFill.Controls.Add(Me.comboBoxTitlePreFill)
		resources.ApplyResources(Me.groupBoxTitlePreFill, "groupBoxTitlePreFill")
		Me.groupBoxTitlePreFill.Name = "groupBoxTitlePreFill"
		Me.groupBoxTitlePreFill.TabStop = false
		'
		'checkBoxUseExistingTitle
		'
		resources.ApplyResources(Me.checkBoxUseExistingTitle, "checkBoxUseExistingTitle")
		Me.checkBoxUseExistingTitle.Name = "checkBoxUseExistingTitle"
		Me.checkBoxUseExistingTitle.UseVisualStyleBackColor = true
		'
		'comboBoxTitlePreFill
		'
		Me.comboBoxTitlePreFill.FormattingEnabled = true
		resources.ApplyResources(Me.comboBoxTitlePreFill, "comboBoxTitlePreFill")
		Me.comboBoxTitlePreFill.Name = "comboBoxTitlePreFill"
		Me.comboBoxTitlePreFill.Sorted = true
		AddHandler Me.comboBoxTitlePreFill.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'checkBoxUseExistingAuthor
		'
		resources.ApplyResources(Me.checkBoxUseExistingAuthor, "checkBoxUseExistingAuthor")
		Me.checkBoxUseExistingAuthor.Name = "checkBoxUseExistingAuthor"
		Me.checkBoxUseExistingAuthor.UseVisualStyleBackColor = true
		'
		'comboBoxAuthorPreFill
		'
		Me.comboBoxAuthorPreFill.FormattingEnabled = true
		resources.ApplyResources(Me.comboBoxAuthorPreFill, "comboBoxAuthorPreFill")
		Me.comboBoxAuthorPreFill.Name = "comboBoxAuthorPreFill"
		Me.comboBoxAuthorPreFill.Sorted = true
		AddHandler Me.comboBoxAuthorPreFill.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'groupBoxAuthorPreFill
		'
		Me.groupBoxAuthorPreFill.Controls.Add(Me.checkBoxUseExistingAuthor)
		Me.groupBoxAuthorPreFill.Controls.Add(Me.comboBoxAuthorPreFill)
		resources.ApplyResources(Me.groupBoxAuthorPreFill, "groupBoxAuthorPreFill")
		Me.groupBoxAuthorPreFill.Name = "groupBoxAuthorPreFill"
		Me.groupBoxAuthorPreFill.TabStop = false
		'
		'checkBoxUseExistingSubject
		'
		resources.ApplyResources(Me.checkBoxUseExistingSubject, "checkBoxUseExistingSubject")
		Me.checkBoxUseExistingSubject.Name = "checkBoxUseExistingSubject"
		Me.checkBoxUseExistingSubject.UseVisualStyleBackColor = true
		'
		'comboBoxSubjectPreFill
		'
		Me.comboBoxSubjectPreFill.FormattingEnabled = true
		resources.ApplyResources(Me.comboBoxSubjectPreFill, "comboBoxSubjectPreFill")
		Me.comboBoxSubjectPreFill.Name = "comboBoxSubjectPreFill"
		AddHandler Me.comboBoxSubjectPreFill.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'groupBoxSubjectPreFill
		'
		Me.groupBoxSubjectPreFill.Controls.Add(Me.checkBoxUseExistingSubject)
		Me.groupBoxSubjectPreFill.Controls.Add(Me.comboBoxSubjectPreFill)
		resources.ApplyResources(Me.groupBoxSubjectPreFill, "groupBoxSubjectPreFill")
		Me.groupBoxSubjectPreFill.Name = "groupBoxSubjectPreFill"
		Me.groupBoxSubjectPreFill.TabStop = false
		'
		'groupBoxKeywordsPreFill
		'
		Me.groupBoxKeywordsPreFill.Controls.Add(Me.textBoxKeywordsPreFill)
		Me.groupBoxKeywordsPreFill.Controls.Add(Me.checkBoxUseExistingKeywords)
		resources.ApplyResources(Me.groupBoxKeywordsPreFill, "groupBoxKeywordsPreFill")
		Me.groupBoxKeywordsPreFill.Name = "groupBoxKeywordsPreFill"
		Me.groupBoxKeywordsPreFill.TabStop = false
		'
		'textBoxKeywordsPreFill
		'
		resources.ApplyResources(Me.textBoxKeywordsPreFill, "textBoxKeywordsPreFill")
		Me.textBoxKeywordsPreFill.Name = "textBoxKeywordsPreFill"
		AddHandler Me.textBoxKeywordsPreFill.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'checkBoxUseExistingKeywords
		'
		resources.ApplyResources(Me.checkBoxUseExistingKeywords, "checkBoxUseExistingKeywords")
		Me.checkBoxUseExistingKeywords.Name = "checkBoxUseExistingKeywords"
		Me.checkBoxUseExistingKeywords.UseVisualStyleBackColor = true
		'
		'checkBoxDeleteAfterUploaded
		'
		resources.ApplyResources(Me.checkBoxDeleteAfterUploaded, "checkBoxDeleteAfterUploaded")
		Me.checkBoxDeleteAfterUploaded.Name = "checkBoxDeleteAfterUploaded"
		Me.checkBoxDeleteAfterUploaded.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxDeleteAfterUploaded.CheckedChanged, AddressOf Me.CheckBoxDeleteAfterUploadedCheckedChanged
		'
		'labelDeletePdfWarning
		'
		resources.ApplyResources(Me.labelDeletePdfWarning, "labelDeletePdfWarning")
		Me.labelDeletePdfWarning.Name = "labelDeletePdfWarning"
		'
		'buttonOK
		'
		resources.ApplyResources(Me.buttonOK, "buttonOK")
		Me.buttonOK.Name = "buttonOK"
		Me.buttonOK.UseVisualStyleBackColor = true
		AddHandler Me.buttonOK.Click, AddressOf Me.ButtonOKClick
		'
		'buttonCancel
		'
		Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		resources.ApplyResources(Me.buttonCancel, "buttonCancel")
		Me.buttonCancel.Name = "buttonCancel"
		Me.buttonCancel.UseVisualStyleBackColor = true
		'
		'UploadFolderWatcherConfigurationForm
		'
		Me.AcceptButton = Me.buttonOK
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonCancel
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.labelDeletePdfWarning)
		Me.Controls.Add(Me.checkBoxDeleteAfterUploaded)
		Me.Controls.Add(Me.groupBoxKeywordsPreFill)
		Me.Controls.Add(Me.groupBoxSubjectPreFill)
		Me.Controls.Add(Me.groupBoxAuthorPreFill)
		Me.Controls.Add(Me.groupBoxTitlePreFill)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "UploadFolderWatcherConfigurationForm"
		AddHandler Load, AddressOf Me.UploadFolderWatcherConfigurationFormLoad
		Me.groupBoxTitlePreFill.ResumeLayout(false)
		Me.groupBoxAuthorPreFill.ResumeLayout(false)
		Me.groupBoxSubjectPreFill.ResumeLayout(false)
		Me.groupBoxKeywordsPreFill.ResumeLayout(false)
		Me.groupBoxKeywordsPreFill.PerformLayout
		Me.ResumeLayout(false)
	End Sub
	Private checkBoxDeleteAfterUploaded As System.Windows.Forms.CheckBox
	Private buttonCancel As System.Windows.Forms.Button
	Private buttonOK As System.Windows.Forms.Button
	Private labelDeletePdfWarning As System.Windows.Forms.Label
	Private checkBoxUseExistingKeywords As System.Windows.Forms.CheckBox
	Private textBoxKeywordsPreFill As System.Windows.Forms.TextBox
	Private groupBoxKeywordsPreFill As System.Windows.Forms.GroupBox
	Private checkBoxUseExistingAuthor As System.Windows.Forms.CheckBox
	Private groupBoxSubjectPreFill As System.Windows.Forms.GroupBox
	Private comboBoxSubjectPreFill As System.Windows.Forms.ComboBox
	Private checkBoxUseExistingSubject As System.Windows.Forms.CheckBox
	Private groupBoxAuthorPreFill As System.Windows.Forms.GroupBox
	Private comboBoxAuthorPreFill As System.Windows.Forms.ComboBox
	Private comboBoxTitlePreFill As System.Windows.Forms.ComboBox
	Private checkBoxUseExistingTitle As System.Windows.Forms.CheckBox
	Private groupBoxTitlePreFill As System.Windows.Forms.GroupBox
End Class
