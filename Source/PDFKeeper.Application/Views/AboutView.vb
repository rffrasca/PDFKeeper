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
		Font = SystemFonts.MessageBoxFont
		labelTitle.Text = AppInfo.Title
		labelDescription.Text = AppInfo.Description
		textBoxVersion.Text = AppInfo.Version
		textBoxBuild.Text = AppInfo.Build
		labelCopyright.Text = AppInfo.Copyright
		textBoxLicense.Text = AppInfo.License
		textBoxThirdPartyNotice.Text = AppInfo.ThirdPartyNotice
	End Sub
	
	Private Sub LinkLabelHomepageLinkClicked( _
		sender As Object, _
		e As LinkLabelLinkClickedEventArgs)
		
		ShowHomepage
	End Sub
End Class
