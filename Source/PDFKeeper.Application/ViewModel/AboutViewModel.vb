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
'*
'* Created by SharpDevelop.
'* User: Robert
'* Date: 7/2/2016
'* Time: 10:03 PM
'*
'******************************************************************************

Public NotInheritable Class AboutViewModel
	Private Sub New()
		' Because type 'AboutViewModel' contains only 'Shared' members, add a
		' default private constructor to prevent the compiler from adding a
		' default public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Gets data used by AboutView.labelTitle.
	''' </summary>
	Public Shared ReadOnly Property Title As String
		Get
			Return ApplicationInfo.Title
		End Get
	End Property
	
	''' <summary>
	''' Gets data used by AboutView.labelDescription.
	''' </summary>
	Public Shared ReadOnly Property Description As String
		Get
			Return ApplicationInfo.Description
		End Get
	End Property
	
	''' <summary>
	''' Gets data used by AboutView.labelVersion.
	''' </summary>
	Public Shared ReadOnly Property Version As String
		Get
			Return PdfKeeper.Strings.Version & ": " & ApplicationInfo.Version
		End Get
	End Property
	
	''' <summary>
	''' Gets data used by AboutView.labelBuild.
	''' </summary>
	Public Shared ReadOnly Property Build As String
		Get
			Return PdfKeeper.Strings.Build & ": " & ApplicationInfo.Build
		End Get
	End Property
	
	''' <summary>
	''' Gets data used by AboutView.labelCopyright.
	''' </summary>
	Public Shared ReadOnly Property Copyright As String
		Get
			Return ApplicationInfo.Copyright
		End Get
	End Property
	
	''' <summary>
	''' Gets data used by AboutView.linkLabelHomepageName.
	''' </summary>
	Public Shared ReadOnly Property HomepageName As String
		Get
			Return Title & " " & PdfKeeper.Strings.Homepage
		End Get
	End Property
	
	''' <summary>
	''' Gets data used by AboutView.textBoxLicense.
	''' </summary>
	Public Shared ReadOnly Property License As String
		Get
			Return PdfKeeper.Strings.ApplicationLicense
		End Get
	End Property
	
	''' <summary>
	''' Gets data used by AboutView.textBoxThirdPartyNotice.
	''' </summary>
	Public Shared ReadOnly Property ThirdPartyNotice As String
		Get
			Return PdfKeeper.Strings.ApplicationThirdPartyNotice
		End Get
	End Property
End Class
