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
		Dim systemFont As System.Drawing.Font = SystemFonts.MessageBoxFont
		Me.Font = New System.Drawing.Font( _
			systemFont.Name, _
			systemFont.Size, _
			systemFont.Style)
		Me.InitializeComponent()
		SetControlData
	End Sub	
		
	Private Sub SetControlData
		labelTitle.Text = AboutApplication.Title
		labelDescription.Text = AboutApplication.Description
		textBoxVersion.Text = AboutApplication.Version
		textBoxBuild.Text = AboutApplication.Build
		labelCopyright.Text = AboutApplication.Copyright
		textBoxLicense.Text = AboutApplication.License
		textBoxThirdPartyNotice.Text = AboutApplication.ThirdPartyNotice
	End Sub
	
	Private Sub LinkLabelHomepageLinkClicked( _
		sender As Object, _
		e As LinkLabelLinkClickedEventArgs)
		WebPages.ShowHomepage
	End Sub
End Class
