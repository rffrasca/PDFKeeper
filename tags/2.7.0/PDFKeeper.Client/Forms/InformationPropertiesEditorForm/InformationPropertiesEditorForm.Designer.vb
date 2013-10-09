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
		Me.processPdfViewer = New System.Diagnostics.Process
		Me.checkBoxAddUploadQueueAfterSave = New System.Windows.Forms.CheckBox
		Me.checkBoxDeleteOrigAfterSave = New System.Windows.Forms.CheckBox
		Me.groupBoxAfterSavingOptions = New System.Windows.Forms.GroupBox
		Me.checkBoxDeleteSavedAfterUpload = New System.Windows.Forms.CheckBox
		Me.labelDeletePdfWarning = New System.Windows.Forms.Label
		Me.groupBoxPDFDocument.SuspendLayout
		Me.groupBoxProperties.SuspendLayout
		Me.groupBoxAfterSavingOptions.SuspendLayout
		Me.SuspendLayout
		'
		'groupBoxPDFDocument
		'
		Me.groupBoxPDFDocument.Controls.Add(Me.buttonView)
		Me.groupBoxPDFDocument.Controls.Add(Me.textBoxPDFDocument)
		resources.ApplyResources(Me.groupBoxPDFDocument, "groupBoxPDFDocument")
		Me.groupBoxPDFDocument.Name = "groupBoxPDFDocument"
		Me.groupBoxPDFDocument.TabStop = false
		'
		'buttonView
		'
		resources.ApplyResources(Me.buttonView, "buttonView")
		Me.buttonView.Name = "buttonView"
		Me.buttonView.UseVisualStyleBackColor = true
		AddHandler Me.buttonView.Click, AddressOf Me.ButtonViewClick
		'
		'textBoxPDFDocument
		'
		resources.ApplyResources(Me.textBoxPDFDocument, "textBoxPDFDocument")
		Me.textBoxPDFDocument.Name = "textBoxPDFDocument"
		Me.textBoxPDFDocument.ReadOnly = true
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
		resources.ApplyResources(Me.groupBoxProperties, "groupBoxProperties")
		Me.groupBoxProperties.Name = "groupBoxProperties"
		Me.groupBoxProperties.TabStop = false
		'
		'textBoxKeywords
		'
		resources.ApplyResources(Me.textBoxKeywords, "textBoxKeywords")
		Me.textBoxKeywords.Name = "textBoxKeywords"
		AddHandler Me.textBoxKeywords.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelKeywords
		'
		resources.ApplyResources(Me.labelKeywords, "labelKeywords")
		Me.labelKeywords.Name = "labelKeywords"
		'
		'comboBoxSubject
		'
		resources.ApplyResources(Me.comboBoxSubject, "comboBoxSubject")
		Me.comboBoxSubject.Name = "comboBoxSubject"
		Me.comboBoxSubject.Sorted = true
		AddHandler Me.comboBoxSubject.SelectedIndexChanged, AddressOf Me.TextComboBoxTextChanged
		AddHandler Me.comboBoxSubject.DropDown, AddressOf Me.ComboBoxSubjectDropDown
		AddHandler Me.comboBoxSubject.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelSubject
		'
		resources.ApplyResources(Me.labelSubject, "labelSubject")
		Me.labelSubject.Name = "labelSubject"
		'
		'comboBoxAuthor
		'
		Me.comboBoxAuthor.BackColor = System.Drawing.SystemColors.Window
		resources.ApplyResources(Me.comboBoxAuthor, "comboBoxAuthor")
		Me.comboBoxAuthor.Name = "comboBoxAuthor"
		Me.comboBoxAuthor.Sorted = true
		AddHandler Me.comboBoxAuthor.SelectedIndexChanged, AddressOf Me.TextComboBoxTextChanged
		AddHandler Me.comboBoxAuthor.DropDown, AddressOf Me.ComboBoxAuthorDropDown
		AddHandler Me.comboBoxAuthor.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelAuthor
		'
		resources.ApplyResources(Me.labelAuthor, "labelAuthor")
		Me.labelAuthor.Name = "labelAuthor"
		'
		'textBoxTitle
		'
		Me.textBoxTitle.BackColor = System.Drawing.SystemColors.Window
		resources.ApplyResources(Me.textBoxTitle, "textBoxTitle")
		Me.textBoxTitle.Name = "textBoxTitle"
		AddHandler Me.textBoxTitle.TextChanged, AddressOf Me.TextComboBoxTextChanged
		'
		'labelTitle
		'
		resources.ApplyResources(Me.labelTitle, "labelTitle")
		Me.labelTitle.Name = "labelTitle"
		'
		'buttonSave
		'
		resources.ApplyResources(Me.buttonSave, "buttonSave")
		Me.buttonSave.Name = "buttonSave"
		Me.buttonSave.UseVisualStyleBackColor = true
		AddHandler Me.buttonSave.Click, AddressOf Me.ButtonSaveClick
		'
		'buttonCancel
		'
		Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		resources.ApplyResources(Me.buttonCancel, "buttonCancel")
		Me.buttonCancel.Name = "buttonCancel"
		Me.buttonCancel.UseVisualStyleBackColor = true
		AddHandler Me.buttonCancel.Click, AddressOf Me.ButtonCancelClick
		'
		'checkBoxViewAfterSave
		'
		resources.ApplyResources(Me.checkBoxViewAfterSave, "checkBoxViewAfterSave")
		Me.checkBoxViewAfterSave.Name = "checkBoxViewAfterSave"
		Me.checkBoxViewAfterSave.UseVisualStyleBackColor = true
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
		'checkBoxAddUploadQueueAfterSave
		'
		resources.ApplyResources(Me.checkBoxAddUploadQueueAfterSave, "checkBoxAddUploadQueueAfterSave")
		Me.checkBoxAddUploadQueueAfterSave.Name = "checkBoxAddUploadQueueAfterSave"
		Me.checkBoxAddUploadQueueAfterSave.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxAddUploadQueueAfterSave.CheckedChanged, AddressOf Me.CheckBoxAddUploadQueueAfterSaveCheckedChanged
		'
		'checkBoxDeleteOrigAfterSave
		'
		resources.ApplyResources(Me.checkBoxDeleteOrigAfterSave, "checkBoxDeleteOrigAfterSave")
		Me.checkBoxDeleteOrigAfterSave.Name = "checkBoxDeleteOrigAfterSave"
		Me.checkBoxDeleteOrigAfterSave.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxDeleteOrigAfterSave.CheckedChanged, AddressOf Me.CheckBoxDeleteOrigAfterSaveCheckedChanged
		'
		'groupBoxAfterSavingOptions
		'
		Me.groupBoxAfterSavingOptions.Controls.Add(Me.checkBoxDeleteSavedAfterUpload)
		Me.groupBoxAfterSavingOptions.Controls.Add(Me.checkBoxViewAfterSave)
		Me.groupBoxAfterSavingOptions.Controls.Add(Me.checkBoxDeleteOrigAfterSave)
		Me.groupBoxAfterSavingOptions.Controls.Add(Me.checkBoxAddUploadQueueAfterSave)
		resources.ApplyResources(Me.groupBoxAfterSavingOptions, "groupBoxAfterSavingOptions")
		Me.groupBoxAfterSavingOptions.Name = "groupBoxAfterSavingOptions"
		Me.groupBoxAfterSavingOptions.TabStop = false
		'
		'checkBoxDeleteSavedAfterUpload
		'
		resources.ApplyResources(Me.checkBoxDeleteSavedAfterUpload, "checkBoxDeleteSavedAfterUpload")
		Me.checkBoxDeleteSavedAfterUpload.Name = "checkBoxDeleteSavedAfterUpload"
		Me.checkBoxDeleteSavedAfterUpload.UseVisualStyleBackColor = true
		'
		'labelDeletePdfWarning
		'
		resources.ApplyResources(Me.labelDeletePdfWarning, "labelDeletePdfWarning")
		Me.labelDeletePdfWarning.Name = "labelDeletePdfWarning"
		'
		'InformationPropertiesEditorForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonCancel
		Me.Controls.Add(Me.labelDeletePdfWarning)
		Me.Controls.Add(Me.groupBoxAfterSavingOptions)
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonSave)
		Me.Controls.Add(Me.groupBoxProperties)
		Me.Controls.Add(Me.groupBoxPDFDocument)
		Me.DoubleBuffered = true
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.Name = "InformationPropertiesEditorForm"
		AddHandler Load, AddressOf Me.InformationPropertiesEditorFormLoad
		AddHandler FormClosing, AddressOf Me.InformationPropertiesEditorFormClosing
		Me.groupBoxPDFDocument.ResumeLayout(false)
		Me.groupBoxPDFDocument.PerformLayout
		Me.groupBoxProperties.ResumeLayout(false)
		Me.groupBoxProperties.PerformLayout
		Me.groupBoxAfterSavingOptions.ResumeLayout(false)
		Me.ResumeLayout(false)
	End Sub
	Private checkBoxDeleteSavedAfterUpload As System.Windows.Forms.CheckBox
	Private labelDeletePdfWarning As System.Windows.Forms.Label
	Private checkBoxAddUploadQueueAfterSave As System.Windows.Forms.CheckBox
	Private checkBoxDeleteOrigAfterSave As System.Windows.Forms.CheckBox
	Private groupBoxAfterSavingOptions As System.Windows.Forms.GroupBox
	Private processPdfViewer As System.Diagnostics.Process
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
