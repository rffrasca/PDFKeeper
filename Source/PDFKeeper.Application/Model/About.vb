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
'*
'* Created by SharpDevelop.
'* User: Robert
'* Date: 6/23/2016
'* Time: 5:13 PM
'*
'******************************************************************************

''' <summary>
''' AboutView Model.
''' </summary>
Public NotInheritable Class About
	Private Sub New()
		' Because type 'About' contains only 'Shared' members, add a default
		' private constructor to prevent the compiler from adding a default
		' public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Gets the application Product Name.
	''' </summary>
	Public Shared ReadOnly Property ProductName As String
		Get
			Return My.Application.Info.ProductName
		End Get
	End Property
	
	''' <summary>
	''' Gets the application Description.
	''' </summary>
	Public Shared ReadOnly Property Description As String
		Get
			Return My.Application.Info.Description
		End Get
	End Property
	
	''' <summary>
	''' Gets the application Version.
	''' </summary>
	Public Shared ReadOnly Property Version As String
		Get
			Return My.Application.Info.Version.ToString
		End Get
	End Property
	
	''' <summary>
	''' Gets the application Copyright.
	''' </summary>
	Public Shared ReadOnly Property Copyright As String
		Get
			Return My.Application.Info.Copyright
		End Get
	End Property
	
	''' <summary>
	''' Gets the application License.
	''' </summary>
	Public Shared ReadOnly Property License As String
		Get
			Return PdfKeeper.Strings.License
		End Get
	End Property
	
	''' <summary>
	''' Gets the application Homepage Name.
	''' </summary>
	Public Shared ReadOnly Property HomepageName As String
		Get
			Return ProductName & " " & PdfKeeper.Strings.Homepage
		End Get
	End Property
	
	''' <summary>
	''' Gets the application Third-Party Notice.
	''' </summary>
	Public Shared ReadOnly Property ThirdPartyNotice As String
		Get
			Return PdfKeeper.Strings.ThirdPartyNotice
		End Get
	End Property
	
	''' <summary>
	''' Gets the application Homepage URL.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
		"CA1056:UriPropertiesShouldNotBeStrings")> _
	Public Shared ReadOnly Property HomepageUrl As String
		Get
			Return ConfigurationManager.AppSettings("HomePageUrl")
		End Get
	End Property
End Class
