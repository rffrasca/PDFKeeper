'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
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

Partial Class DatabaseConnectionForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DatabaseConnectionForm))
		Me.labelUserName = New System.Windows.Forms.Label
		Me.textBoxUserName = New System.Windows.Forms.TextBox
		Me.labelPassword = New System.Windows.Forms.Label
		Me.textBoxPassword = New System.Windows.Forms.TextBox
		Me.labelDataSource = New System.Windows.Forms.Label
		Me.textBoxDataSource = New System.Windows.Forms.TextBox
		Me.buttonOK = New System.Windows.Forms.Button
		Me.buttonCancel = New System.Windows.Forms.Button
		Me.pictureBoxLogo = New System.Windows.Forms.PictureBox
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'labelUserName
		'
		Me.labelUserName.Location = New System.Drawing.Point(12, 62)
		Me.labelUserName.Name = "labelUserName"
		Me.labelUserName.Size = New System.Drawing.Size(75, 23)
		Me.labelUserName.TabIndex = 0
		Me.labelUserName.Text = "User Name:"
		Me.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBoxUserName
		'
		Me.textBoxUserName.Location = New System.Drawing.Point(93, 65)
		Me.textBoxUserName.MaxLength = 30
		Me.textBoxUserName.Name = "textBoxUserName"
		Me.textBoxUserName.Size = New System.Drawing.Size(285, 20)
		Me.textBoxUserName.TabIndex = 1
		AddHandler Me.textBoxUserName.TextChanged, AddressOf Me.TextBoxTextChanged
		'
		'labelPassword
		'
		Me.labelPassword.Location = New System.Drawing.Point(12, 98)
		Me.labelPassword.Name = "labelPassword"
		Me.labelPassword.Size = New System.Drawing.Size(75, 23)
		Me.labelPassword.TabIndex = 2
		Me.labelPassword.Text = "Password:"
		Me.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBoxPassword
		'
		Me.textBoxPassword.Location = New System.Drawing.Point(93, 101)
		Me.textBoxPassword.MaxLength = 30
		Me.textBoxPassword.Name = "textBoxPassword"
		Me.textBoxPassword.Size = New System.Drawing.Size(285, 20)
		Me.textBoxPassword.TabIndex = 3
		Me.textBoxPassword.UseSystemPasswordChar = true
		AddHandler Me.textBoxPassword.TextChanged, AddressOf Me.TextBoxTextChanged
		'
		'labelDataSource
		'
		Me.labelDataSource.Location = New System.Drawing.Point(12, 134)
		Me.labelDataSource.Name = "labelDataSource"
		Me.labelDataSource.Size = New System.Drawing.Size(75, 23)
		Me.labelDataSource.TabIndex = 4
		Me.labelDataSource.Text = "Data Source:"
		Me.labelDataSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBoxDataSource
		'
		Me.textBoxDataSource.Location = New System.Drawing.Point(93, 137)
		Me.textBoxDataSource.MaxLength = 260
		Me.textBoxDataSource.Name = "textBoxDataSource"
		Me.textBoxDataSource.Size = New System.Drawing.Size(285, 20)
		Me.textBoxDataSource.TabIndex = 5
		AddHandler Me.textBoxDataSource.TextChanged, AddressOf Me.TextBoxTextChanged
		'
		'buttonOK
		'
		Me.buttonOK.Enabled = false
		Me.buttonOK.Location = New System.Drawing.Point(222, 179)
		Me.buttonOK.Name = "buttonOK"
		Me.buttonOK.Size = New System.Drawing.Size(75, 23)
		Me.buttonOK.TabIndex = 6
		Me.buttonOK.Text = "OK"
		Me.buttonOK.UseVisualStyleBackColor = true
		AddHandler Me.buttonOK.Click, AddressOf Me.ButtonOKClick
		'
		'buttonCancel
		'
		Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.buttonCancel.Location = New System.Drawing.Point(303, 179)
		Me.buttonCancel.Name = "buttonCancel"
		Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
		Me.buttonCancel.TabIndex = 7
		Me.buttonCancel.Text = "Cancel"
		Me.buttonCancel.UseVisualStyleBackColor = true
		'
		'pictureBoxLogo
		'
		Me.pictureBoxLogo.Image = CType(resources.GetObject("pictureBoxLogo.Image"),System.Drawing.Image)
		Me.pictureBoxLogo.Location = New System.Drawing.Point(12, 12)
		Me.pictureBoxLogo.Name = "pictureBoxLogo"
		Me.pictureBoxLogo.Size = New System.Drawing.Size(366, 32)
		Me.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
		Me.pictureBoxLogo.TabIndex = 8
		Me.pictureBoxLogo.TabStop = false
		'
		'DatabaseConnectionForm
		'
		Me.AcceptButton = Me.buttonOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonCancel
		Me.ClientSize = New System.Drawing.Size(390, 214)
		Me.Controls.Add(Me.pictureBoxLogo)
		Me.Controls.Add(Me.buttonCancel)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.textBoxDataSource)
		Me.Controls.Add(Me.labelDataSource)
		Me.Controls.Add(Me.textBoxPassword)
		Me.Controls.Add(Me.labelPassword)
		Me.Controls.Add(Me.textBoxUserName)
		Me.Controls.Add(Me.labelUserName)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "DatabaseConnectionForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "PDFKeeper - Database Connection"
		AddHandler Load, AddressOf Me.DatabaseConnectionFormLoad
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private pictureBoxLogo As System.Windows.Forms.PictureBox
	Private textBoxUserName As System.Windows.Forms.TextBox
	Private labelUserName As System.Windows.Forms.Label
	Private textBoxPassword As System.Windows.Forms.TextBox
	Private textBoxDataSource As System.Windows.Forms.TextBox
	Private buttonOK As System.Windows.Forms.Button
	Private buttonCancel As System.Windows.Forms.Button
	Private labelDataSource As System.Windows.Forms.Label
	Private labelPassword As System.Windows.Forms.Label
End Class
