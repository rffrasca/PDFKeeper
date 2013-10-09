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

Partial Class AboutForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
		Me.pictureBoxLogo = New System.Windows.Forms.PictureBox
		Me.labelLegal = New System.Windows.Forms.Label
		Me.buttonOK = New System.Windows.Forms.Button
		Me.labelProduct = New System.Windows.Forms.Label
		Me.labelDescription = New System.Windows.Forms.Label
		Me.labelCopyright = New System.Windows.Forms.Label
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'pictureBoxLogo
		'
		Me.pictureBoxLogo.Image = CType(resources.GetObject("pictureBoxLogo.Image"),System.Drawing.Image)
		Me.pictureBoxLogo.Location = New System.Drawing.Point(0, 12)
		Me.pictureBoxLogo.Name = "pictureBoxLogo"
		Me.pictureBoxLogo.Size = New System.Drawing.Size(395, 32)
		Me.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
		Me.pictureBoxLogo.TabIndex = 0
		Me.pictureBoxLogo.TabStop = false
		'
		'labelLegal
		'
		Me.labelLegal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.labelLegal.Location = New System.Drawing.Point(0, 135)
		Me.labelLegal.Name = "labelLegal"
		Me.labelLegal.Size = New System.Drawing.Size(395, 47)
		Me.labelLegal.TabIndex = 1
		Me.labelLegal.Text = "This program comes with ABSOLUTELY NO WARRANTY."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"This is free software, and you a"& _ 
		"re welcome to redistribute it under certain conditions. See COPYING.txt for deta"& _ 
		"ils."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
		Me.labelLegal.TextAlign = System.Drawing.ContentAlignment.TopCenter
		'
		'buttonOK
		'
		Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.buttonOK.Location = New System.Drawing.Point(160, 203)
		Me.buttonOK.Name = "buttonOK"
		Me.buttonOK.Size = New System.Drawing.Size(75, 23)
		Me.buttonOK.TabIndex = 2
		Me.buttonOK.Text = "OK"
		Me.buttonOK.UseVisualStyleBackColor = true
		'
		'labelProduct
		'
		Me.labelProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.labelProduct.Location = New System.Drawing.Point(0, 55)
		Me.labelProduct.Name = "labelProduct"
		Me.labelProduct.Size = New System.Drawing.Size(395, 23)
		Me.labelProduct.TabIndex = 1
		Me.labelProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'labelDescription
		'
		Me.labelDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.labelDescription.Location = New System.Drawing.Point(0, 75)
		Me.labelDescription.Name = "labelDescription"
		Me.labelDescription.Size = New System.Drawing.Size(395, 23)
		Me.labelDescription.TabIndex = 3
		Me.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'labelCopyright
		'
		Me.labelCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.labelCopyright.Location = New System.Drawing.Point(0, 95)
		Me.labelCopyright.Name = "labelCopyright"
		Me.labelCopyright.Size = New System.Drawing.Size(395, 23)
		Me.labelCopyright.TabIndex = 4
		Me.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'AboutForm
		'
		Me.AcceptButton = Me.buttonOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(390, 238)
		Me.ControlBox = false
		Me.Controls.Add(Me.labelCopyright)
		Me.Controls.Add(Me.labelDescription)
		Me.Controls.Add(Me.labelProduct)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.labelLegal)
		Me.Controls.Add(Me.pictureBoxLogo)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "AboutForm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "About"
		AddHandler Load, AddressOf Me.AboutFormLoad
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
	End Sub
	Private labelLegal As System.Windows.Forms.Label
	Private labelCopyright As System.Windows.Forms.Label
	Private labelDescription As System.Windows.Forms.Label
	Private labelProduct As System.Windows.Forms.Label
	Private buttonOK As System.Windows.Forms.Button
	Private pictureBoxLogo As System.Windows.Forms.PictureBox
End Class
