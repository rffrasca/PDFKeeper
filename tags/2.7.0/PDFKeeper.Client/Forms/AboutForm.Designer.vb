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
		resources.ApplyResources(Me.pictureBoxLogo, "pictureBoxLogo")
		Me.pictureBoxLogo.Name = "pictureBoxLogo"
		Me.pictureBoxLogo.TabStop = false
		'
		'labelLegal
		'
		resources.ApplyResources(Me.labelLegal, "labelLegal")
		Me.labelLegal.Name = "labelLegal"
		'
		'buttonOK
		'
		Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
		resources.ApplyResources(Me.buttonOK, "buttonOK")
		Me.buttonOK.Name = "buttonOK"
		Me.buttonOK.UseVisualStyleBackColor = true
		'
		'labelProduct
		'
		resources.ApplyResources(Me.labelProduct, "labelProduct")
		Me.labelProduct.Name = "labelProduct"
		'
		'labelDescription
		'
		resources.ApplyResources(Me.labelDescription, "labelDescription")
		Me.labelDescription.Name = "labelDescription"
		'
		'labelCopyright
		'
		resources.ApplyResources(Me.labelCopyright, "labelCopyright")
		Me.labelCopyright.Name = "labelCopyright"
		'
		'AboutForm
		'
		Me.AcceptButton = Me.buttonOK
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
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
