'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2014 Robert F. Frasca
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
		Me.pictureBoxLogo = New System.Windows.Forms.PictureBox()
		Me.buttonOK = New System.Windows.Forms.Button()
		Me.labelName = New System.Windows.Forms.Label()
		Me.labelDescription = New System.Windows.Forms.Label()
		Me.labelCopyright = New System.Windows.Forms.Label()
		Me.labelVersion = New System.Windows.Forms.Label()
		Me.linkLabelProjectSite = New System.Windows.Forms.LinkLabel()
		Me.tabControlLicenses = New System.Windows.Forms.TabControl()
		Me.tabPageLicense = New System.Windows.Forms.TabPage()
		Me.textBoxLicense = New System.Windows.Forms.TextBox()
		Me.tabPageThirdPartyCredits = New System.Windows.Forms.TabPage()
		Me.textBoxThirdPartyCredits = New System.Windows.Forms.TextBox()
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).BeginInit
		Me.tabControlLicenses.SuspendLayout
		Me.tabPageLicense.SuspendLayout
		Me.tabPageThirdPartyCredits.SuspendLayout
		Me.SuspendLayout
		'
		'pictureBoxLogo
		'
		resources.ApplyResources(Me.pictureBoxLogo, "pictureBoxLogo")
		Me.pictureBoxLogo.Name = "pictureBoxLogo"
		Me.pictureBoxLogo.TabStop = false
		'
		'buttonOK
		'
		Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
		resources.ApplyResources(Me.buttonOK, "buttonOK")
		Me.buttonOK.Name = "buttonOK"
		Me.buttonOK.UseVisualStyleBackColor = true
		'
		'labelName
		'
		resources.ApplyResources(Me.labelName, "labelName")
		Me.labelName.Name = "labelName"
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
		'labelVersion
		'
		resources.ApplyResources(Me.labelVersion, "labelVersion")
		Me.labelVersion.Name = "labelVersion"
		'
		'linkLabelProjectSite
		'
		resources.ApplyResources(Me.linkLabelProjectSite, "linkLabelProjectSite")
		Me.linkLabelProjectSite.Name = "linkLabelProjectSite"
		Me.linkLabelProjectSite.TabStop = true
		AddHandler Me.linkLabelProjectSite.LinkClicked, AddressOf Me.LinkLabelProjectSiteLinkClicked
		'
		'tabControlLicenses
		'
		Me.tabControlLicenses.Controls.Add(Me.tabPageLicense)
		Me.tabControlLicenses.Controls.Add(Me.tabPageThirdPartyCredits)
		resources.ApplyResources(Me.tabControlLicenses, "tabControlLicenses")
		Me.tabControlLicenses.Name = "tabControlLicenses"
		Me.tabControlLicenses.SelectedIndex = 0
		'
		'tabPageLicense
		'
		Me.tabPageLicense.Controls.Add(Me.textBoxLicense)
		resources.ApplyResources(Me.tabPageLicense, "tabPageLicense")
		Me.tabPageLicense.Name = "tabPageLicense"
		Me.tabPageLicense.UseVisualStyleBackColor = true
		'
		'textBoxLicense
		'
		resources.ApplyResources(Me.textBoxLicense, "textBoxLicense")
		Me.textBoxLicense.Name = "textBoxLicense"
		Me.textBoxLicense.ReadOnly = true
		'
		'tabPageThirdPartyCredits
		'
		Me.tabPageThirdPartyCredits.Controls.Add(Me.textBoxThirdPartyCredits)
		resources.ApplyResources(Me.tabPageThirdPartyCredits, "tabPageThirdPartyCredits")
		Me.tabPageThirdPartyCredits.Name = "tabPageThirdPartyCredits"
		Me.tabPageThirdPartyCredits.UseVisualStyleBackColor = true
		'
		'textBoxThirdPartyCredits
		'
		resources.ApplyResources(Me.textBoxThirdPartyCredits, "textBoxThirdPartyCredits")
		Me.textBoxThirdPartyCredits.Name = "textBoxThirdPartyCredits"
		Me.textBoxThirdPartyCredits.ReadOnly = true
		'
		'AboutForm
		'
		Me.AcceptButton = Me.buttonOK
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		resources.ApplyResources(Me, "$this")
		Me.ControlBox = false
		Me.Controls.Add(Me.tabControlLicenses)
		Me.Controls.Add(Me.linkLabelProjectSite)
		Me.Controls.Add(Me.labelVersion)
		Me.Controls.Add(Me.labelCopyright)
		Me.Controls.Add(Me.labelDescription)
		Me.Controls.Add(Me.labelName)
		Me.Controls.Add(Me.buttonOK)
		Me.Controls.Add(Me.pictureBoxLogo)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "AboutForm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		AddHandler Load, AddressOf Me.AboutFormLoad
		CType(Me.pictureBoxLogo,System.ComponentModel.ISupportInitialize).EndInit
		Me.tabControlLicenses.ResumeLayout(false)
		Me.tabPageLicense.ResumeLayout(false)
		Me.tabPageLicense.PerformLayout
		Me.tabPageThirdPartyCredits.ResumeLayout(false)
		Me.tabPageThirdPartyCredits.PerformLayout
		Me.ResumeLayout(false)
	End Sub
	Private textBoxThirdPartyCredits As System.Windows.Forms.TextBox
	Private textBoxLicense As System.Windows.Forms.TextBox
	Private tabPageLicense As System.Windows.Forms.TabPage
	Private tabControlLicenses As System.Windows.Forms.TabControl
	Private tabPageThirdPartyCredits As System.Windows.Forms.TabPage
	Private linkLabelProjectSite As System.Windows.Forms.LinkLabel
	Private labelVersion As System.Windows.Forms.Label
	Private labelCopyright As System.Windows.Forms.Label
	Private labelName As System.Windows.Forms.Label
	Private labelDescription As System.Windows.Forms.Label
	Private buttonOK As System.Windows.Forms.Button
	Private pictureBoxLogo As System.Windows.Forms.PictureBox
End Class
