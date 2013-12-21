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

Public Partial Class AboutForm
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will set the font to MS Sans Serif 8pt in XP or
	''' Segoe UI 9pt in Vista or later; read the Assebly details, and fill the
	''' fill the License and Third-Party Notices tabs.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub AboutFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont	' Use Segoe UI in Vista & 7
		ReadAssemblyDetails
		FillTabs
	End Sub
	
	''' <summary>
	''' This subroutine will read the Product name, description, copyright,
	''' version and build from the Assembly Details and update the form.
	''' </summary>
	Private Sub ReadAssemblyDetails
		labelName.Text = My.Application.Info.ProductName
		labelDescription.Text = My.Application.Info.Description
		labelCopyright.Text = My.Application.Info.Copyright
		labelVersion.Text = "Version: " & Product.Version & _
							" - Build: " & Product.Build
	End Sub
	
	''' <summary>
	''' This subroutine will fill the License and Third-Party Notice tabs.
	''' </summary>
	Private Sub FillTabs
		textBoxLicense.Text = AboutForm_Strings.License
		textBoxThirdPartyCredits.Text = AboutForm_Strings.ThirdPartyCredits
	End Sub
	
	''' <summary>
	''' This subroutine will open the Project Site URL using the default web
	''' browser.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub LinkLabelProjectSiteLinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
		Me.Cursor = Cursors.WaitCursor
		Process.Start(ConfigurationManager.AppSettings("ProjectSiteUrl"))
		Me.Cursor = Cursors.Default
	End Sub
End Class
