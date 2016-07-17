'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
'* Copyright (C) 2009-2016 Robert F. Frasca
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

Partial Class DBConnectionView
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DBConnectionView))
		Me.labelUserName = New System.Windows.Forms.Label()
		Me.textBoxUserName = New System.Windows.Forms.TextBox()
		Me.labelPassword = New System.Windows.Forms.Label()
		Me.labelDataSource = New System.Windows.Forms.Label()
		Me.textBoxDataSource = New System.Windows.Forms.TextBox()
		Me.buttonOK = New System.Windows.Forms.Button()
		Me.buttonCancel = New System.Windows.Forms.Button()
		Me.pictureBoxLogo = New System.Windows.Forms.PictureBox()
		Me.labelHelp = New System.Windows.Forms.Label()
		Me.textBoxSecure = New PdfKeeper.TextBoxSecure()
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'labelUserName
		'
		resources.ApplyResources(Me.labelUserName, "labelUserName")
		Me.labelUserName.Name = "labelUserName"
		'
		'textBoxUserName
		'
		resources.ApplyResources(Me.textBoxUserName, "textBoxUserName")
		Me.textBoxUserName.Name = "textBoxUserName"
		'
		'labelPassword
		'
		resources.ApplyResources(Me.labelPassword, "labelPassword")
		Me.labelPassword.Name = "labelPassword"
		'
		'labelDataSource
		'
		resources.ApplyResources(Me.labelDataSource, "labelDataSource")
		Me.labelDataSource.Name = "labelDataSource"
		'
		'textBoxDataSource
		'
		resources.ApplyResources(Me.textBoxDataSource, "textBoxDataSource")
		Me.textBoxDataSource.Name = "textBoxDataSource"
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
		'pictureBoxLogo
		'
		resources.ApplyResources(Me.pictureBoxLogo, "pictureBoxLogo")
		Me.pictureBoxLogo.Name = "pictureBoxLogo"
		Me.pictureBoxLogo.TabStop = false
		'
		'labelHelp
		'
		resources.ApplyResources(Me.labelHelp, "labelHelp")
		Me.labelHelp.Name = "labelHelp"
		'
		'textBoxSecure
		'
		resources.ApplyResources(Me.textBoxSecure, "textBoxSecure")
		Me.textBoxSecure.Name = "textBoxSecure"
		Me.textBoxSecure.ShortcutsEnabled = false
		Me.textBoxSecure.UseSystemPasswordChar = true
		'
		'DBConnectionView
		'
		Me.AcceptButton = Me.buttonOK
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonCancel
		Me.Controls.Add(Me.textBoxSecure)
		Me.Controls.Add(Me.labelHelp)
		Me.Controls.Add(Me.pictureBoxLogo)
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.textBoxDataSource)
		Me.Controls.Add(Me.labelDataSource)
		Me.Controls.Add(Me.labelPassword)
		Me.Controls.Add(Me.textBoxUserName)
		Me.Controls.Add(Me.labelUserName)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DBConnectionView"
		AddHandler HelpRequested, AddressOf Me.DBConnectionViewHelpRequested
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private textBoxSecure As PdfKeeper.TextBoxSecure
	Private labelHelp As System.Windows.Forms.Label
	Private pictureBoxLogo As System.Windows.Forms.PictureBox
	Private textBoxUserName As System.Windows.Forms.TextBox
	Private labelUserName As System.Windows.Forms.Label
	Private textBoxDataSource As System.Windows.Forms.TextBox
	Private buttonOK As System.Windows.Forms.Button
	Private buttonCancel As System.Windows.Forms.Button
	Private labelDataSource As System.Windows.Forms.Label
	Private labelPassword As System.Windows.Forms.Label
End Class
