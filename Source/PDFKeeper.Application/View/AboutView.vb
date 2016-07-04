'******************************************************************************
'*
'* PDFKeeper -- Capture, Upload, and Search for PDF Documents
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

Public Partial Class AboutView
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	Private Sub AboutViewLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		GetData
	End Sub
	
	Private Sub GetData
		labelTitle.Text = AboutViewModel.Title
		labelDescription.Text = AboutViewModel.Description
		labelVersion.Text = AboutViewModel.Version
		labelBuild.Text = AboutViewModel.Build
		labelCopyright.Text = AboutViewModel.Copyright
		linkLabelHomepageName.Text = AboutViewModel.HomepageName
		textBoxLicense.Text = AboutViewModel.License
		textBoxThirdPartyNotice.Text = AboutViewModel.ThirdPartyNotice
	End Sub
	
	Private Sub LinkLabelHomepageNameLinkClicked( _
		sender As Object, _
		e As LinkLabelLinkClickedEventArgs)
		
		AboutHelper.OpenHomepage
	End Sub
End Class
