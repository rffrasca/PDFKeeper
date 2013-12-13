'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2013 Robert F. Frasca
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

Partial Class PdfOwnerPasswordForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PdfOwnerPasswordForm))
		Me.labelPassword = New System.Windows.Forms.Label()
		Me.textBoxPassword = New System.Windows.Forms.TextBox()
		Me.buttonOK = New System.Windows.Forms.Button()
		Me.buttonCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout
		'
		'labelPassword
		'
		resources.ApplyResources(Me.labelPassword, "labelPassword")
		Me.labelPassword.Name = "labelPassword"
		'
		'textBoxPassword
		'
		resources.ApplyResources(Me.textBoxPassword, "textBoxPassword")
		Me.textBoxPassword.Name = "textBoxPassword"
		Me.textBoxPassword.ShortcutsEnabled = false
		Me.textBoxPassword.UseSystemPasswordChar = true
		AddHandler Me.textBoxPassword.TextChanged, AddressOf Me.TextBoxTextChanged
		AddHandler Me.textBoxPassword.KeyDown, AddressOf Me.TextBoxPasswordKeyDown
		AddHandler Me.textBoxPassword.KeyPress, AddressOf Me.TextBoxPasswordKeyPress
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
		AddHandler Me.buttonCancel.Click, AddressOf Me.ButtonCancelClick
		'
		'PdfOwnerPasswordForm
		'
		Me.AcceptButton = Me.buttonOK
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		Me.CancelButton = Me.buttonCancel
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.textBoxPassword)
		Me.Controls.Add(Me.labelPassword)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "PdfOwnerPasswordForm"
		AddHandler Load, AddressOf Me.PdfOwnerPasswordFormLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private textBoxPassword As System.Windows.Forms.TextBox
	Private buttonOK As System.Windows.Forms.Button
	Private buttonCancel As System.Windows.Forms.Button
	Private labelPassword As System.Windows.Forms.Label
End Class
