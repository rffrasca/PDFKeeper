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
'* Date: 7/9/2016
'* Time: 2:10 PM
'*
'******************************************************************************

Public NotInheritable Class AppInfo
	Private Shared productVersion As String = Application.ProductVersion
	
	Private Sub New()
		' Because type 'AppInfo' contains only 'Shared' members, add a default
		' private constructor to prevent the compiler from adding a default
		' public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Gets the title associated with this application.
	''' </summary>
	Public Shared ReadOnly Property Title As String
		Get
			Return Application.ProductName
		End Get
	End Property
	
	''' <summary>
	''' Gets the description associated with this application.
	''' </summary>
	Public Shared ReadOnly Property Description As String
		Get
			Return My.Application.Info.Description
		End Get
	End Property
	
	''' <summary>
	''' Gets the version associated with this application, without the build
	''' number.
	''' </summary>
	Public Shared ReadOnly Property Version As String
		Get
			Return productVersion.Substring( _
				0, _
				productVersion.Length - productVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture) - 1)
		End Get
	End Property
		
	''' <summary>
	''' Gets the build number associated with this application.
	''' </summary>
	Public Shared ReadOnly Property Build As String
		Get
			Return productVersion.Substring( _
				productVersion.Length - productVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture))
		End Get
	End Property
	
	''' <summary>
	''' Gets the copyright notice associated with this application.
	''' </summary>
	Public Shared ReadOnly Property Copyright As String
		Get
			Return My.Application.Info.Copyright
		End Get
	End Property
	
	''' <summary>
	''' Gets the license associated with this application.
	''' </summary>
	Public Shared ReadOnly Property License As String
		Get
			Return PdfKeeper.Strings.AboutLicense
		End Get
	End Property
	
	''' <summary>
	''' Gets the third-party notice associated with this application.
	''' </summary>
	Public Shared ReadOnly Property ThirdPartyNotice As String
		Get
			Return PdfKeeper.Strings.AboutThirdPartyNotice
		End Get
	End Property
End Class
