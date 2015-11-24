'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
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

Partial Class RotatePagesForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RotatePagesForm))
		Me.labelFromPage = New System.Windows.Forms.Label()
		Me.maskedTextBoxFromPage = New System.Windows.Forms.MaskedTextBox()
		Me.labelToPage = New System.Windows.Forms.Label()
		Me.maskedTextBoxToPage = New System.Windows.Forms.MaskedTextBox()
		Me.labelTotalPages = New System.Windows.Forms.Label()
		Me.textBoxTotalPages = New System.Windows.Forms.TextBox()
		Me.labelRotation = New System.Windows.Forms.Label()
		Me.comboBoxRotation = New System.Windows.Forms.ComboBox()
		Me.buttonOK = New System.Windows.Forms.Button()
		Me.buttonCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout
		'
		'labelFromPage
		'
		resources.ApplyResources(Me.labelFromPage, "labelFromPage")
		Me.labelFromPage.Name = "labelFromPage"
		'
		'maskedTextBoxFromPage
		'
		resources.ApplyResources(Me.maskedTextBoxFromPage, "maskedTextBoxFromPage")
		Me.maskedTextBoxFromPage.Name = "maskedTextBoxFromPage"
		Me.maskedTextBoxFromPage.ValidatingType = GetType(Integer)
		'
		'labelToPage
		'
		resources.ApplyResources(Me.labelToPage, "labelToPage")
		Me.labelToPage.Name = "labelToPage"
		'
		'maskedTextBoxToPage
		'
		resources.ApplyResources(Me.maskedTextBoxToPage, "maskedTextBoxToPage")
		Me.maskedTextBoxToPage.Name = "maskedTextBoxToPage"
		Me.maskedTextBoxToPage.ValidatingType = GetType(Integer)
		'
		'labelTotalPages
		'
		resources.ApplyResources(Me.labelTotalPages, "labelTotalPages")
		Me.labelTotalPages.Name = "labelTotalPages"
		'
		'textBoxTotalPages
		'
		resources.ApplyResources(Me.textBoxTotalPages, "textBoxTotalPages")
		Me.textBoxTotalPages.Name = "textBoxTotalPages"
		Me.textBoxTotalPages.ReadOnly = true
		Me.textBoxTotalPages.TabStop = false
		'
		'labelRotation
		'
		resources.ApplyResources(Me.labelRotation, "labelRotation")
		Me.labelRotation.Name = "labelRotation"
		'
		'comboBoxRotation
		'
		Me.comboBoxRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.comboBoxRotation.FormattingEnabled = true
		Me.comboBoxRotation.Items.AddRange(New Object() {resources.GetString("comboBoxRotation.Items"), resources.GetString("comboBoxRotation.Items1"), resources.GetString("comboBoxRotation.Items2")})
		resources.ApplyResources(Me.comboBoxRotation, "comboBoxRotation")
		Me.comboBoxRotation.Name = "comboBoxRotation"
		'
		'buttonOK
		'
		resources.ApplyResources(Me.buttonOK, "buttonOK")
		Me.buttonOK.Name = "buttonOK"
		Me.buttonOK.UseVisualStyleBackColor = true
		AddHandler Me.buttonOK.Click, AddressOf Me.ButtonOkClick
		'
		'buttonCancel
		'
		Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		resources.ApplyResources(Me.buttonCancel, "buttonCancel")
		Me.buttonCancel.Name = "buttonCancel"
		Me.buttonCancel.UseVisualStyleBackColor = true
		'
		'RotatePagesForm
		'
		Me.AcceptButton = Me.buttonOK
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonCancel
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.comboBoxRotation)
		Me.Controls.Add(Me.labelRotation)
		Me.Controls.Add(Me.textBoxTotalPages)
		Me.Controls.Add(Me.labelTotalPages)
		Me.Controls.Add(Me.maskedTextBoxToPage)
		Me.Controls.Add(Me.labelToPage)
		Me.Controls.Add(Me.maskedTextBoxFromPage)
		Me.Controls.Add(Me.labelFromPage)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "RotatePagesForm"
		AddHandler Load, AddressOf Me.RotatePagesFormLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private buttonCancel As System.Windows.Forms.Button
	Private buttonOK As System.Windows.Forms.Button
	Private comboBoxRotation As System.Windows.Forms.ComboBox
	Private labelRotation As System.Windows.Forms.Label
	Private textBoxTotalPages As System.Windows.Forms.TextBox
	Private labelTotalPages As System.Windows.Forms.Label
	Private maskedTextBoxToPage As System.Windows.Forms.MaskedTextBox
	Private labelToPage As System.Windows.Forms.Label
	Private maskedTextBoxFromPage As System.Windows.Forms.MaskedTextBox
	Private labelFromPage As System.Windows.Forms.Label
End Class
