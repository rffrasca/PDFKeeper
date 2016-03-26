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

Public Partial Class AboutForm
	''' <summary>
	''' Form constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' Sets the font to MS Sans Serif 8pt in XP or Segoe UI 9pt in Vista or
	''' later; updates the form with Product Name, Description, Copyright,
	''' Version, Build, License, and Credits.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub AboutFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont	' Use Segoe UI in Vista & 7
		labelName.Text = ProductDetails.Name
		labelDescription.Text = ProductDetails.Description
		labelCopyright.Text = ProductDetails.Copyright
		labelVersion.Text = "Version: " & ProductDetails.Version & _
							" - Build: " & ProductDetails.Build
		textBoxLicense.Text = PdfKeeper.Strings.License
		textBoxCredits.Text = PdfKeeper.Strings.Credits
	End Sub
	
	''' <summary>
	''' Opens the Project Site URL using the default web browser.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub LinkLabelProjectSiteLinkClicked( _
		sender As Object, _
		e As LinkLabelLinkClickedEventArgs)
		
		Me.Cursor = Cursors.WaitCursor
		Process.Start(ConfigurationManager.AppSettings("ProjectSiteUrl"))
		Me.Cursor = Cursors.Default
	End Sub
End Class
