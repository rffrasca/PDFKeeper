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

Partial Class PdfFileRenameForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PdfFileRenameForm))
		Me.buttonCancel = New System.Windows.Forms.Button()
		Me.buttonOK = New System.Windows.Forms.Button()
		Me.textBoxFileName = New System.Windows.Forms.TextBox()
		Me.labelFileName = New System.Windows.Forms.Label()
		Me.SuspendLayout
		'
		'buttonCancel
		'
		Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.buttonCancel.Location = New System.Drawing.Point(241, 82)
		Me.buttonCancel.Name = "buttonCancel"
		Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
		Me.buttonCancel.TabIndex = 3
		Me.buttonCancel.Text = "Cancel"
		Me.buttonCancel.UseVisualStyleBackColor = true
		'
		'buttonOK
		'
		Me.buttonOK.Enabled = false
		Me.buttonOK.Location = New System.Drawing.Point(160, 82)
		Me.buttonOK.Name = "buttonOK"
		Me.buttonOK.Size = New System.Drawing.Size(75, 23)
		Me.buttonOK.TabIndex = 2
		Me.buttonOK.Text = "OK"
		Me.buttonOK.UseVisualStyleBackColor = true
		AddHandler Me.buttonOK.Click, AddressOf Me.ButtonOKClick
		'
		'textBoxFileName
		'
		Me.textBoxFileName.Location = New System.Drawing.Point(12, 35)
		Me.textBoxFileName.Name = "textBoxFileName"
		Me.textBoxFileName.ShortcutsEnabled = false
		Me.textBoxFileName.Size = New System.Drawing.Size(304, 20)
		Me.textBoxFileName.TabIndex = 1
		AddHandler Me.textBoxFileName.TextChanged, AddressOf Me.TextBoxFileNameTextChanged
		'
		'labelFileName
		'
		Me.labelFileName.Location = New System.Drawing.Point(12, 9)
		Me.labelFileName.Name = "labelFileName"
		Me.labelFileName.Size = New System.Drawing.Size(260, 23)
		Me.labelFileName.TabIndex = 0
		Me.labelFileName.Text = "Enter new file name:"
		Me.labelFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'PdfFileRenameForm
		'
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		Me.ClientSize = New System.Drawing.Size(328, 117)
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.textBoxFileName)
		Me.Controls.Add(Me.labelFileName)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "PdfFileRenameForm"
		Me.Text = "PDF File Rename"
		AddHandler Load, AddressOf Me.PdfFileRenameFormLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private labelFileName As System.Windows.Forms.Label
	Private textBoxFileName As System.Windows.Forms.TextBox
	Private buttonOK As System.Windows.Forms.Button
	Private buttonCancel As System.Windows.Forms.Button
End Class
