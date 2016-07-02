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
'* Time: 11:00 AM
'*
'******************************************************************************

Public NotInheritable Class About
	Private Shared productVersion As String = Application.ProductVersion
		
	Private Sub New()
		' Added to prevent the compiler from adding a default public
		' constructor. (CA1053)
	End Sub
	
	Public Shared ReadOnly Property Product As String
		Get
			Return Application.ProductName
		End Get
	End Property
	
	Public Shared ReadOnly Property Description As String
		Get
			Return PdfKeeper.Strings.ProductDescription
		End Get
	End Property
	
	Public Shared ReadOnly Property Version As String
		Get
			Return PdfKeeper.Strings.Version & ": " & _
				productVersion.Substring(0, _
				productVersion.Length - productVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture) - 1)
		End Get
	End Property
	
	Public Shared ReadOnly Property Build As String
		Get
			Return PdfKeeper.Strings.Build & ": " & _
				productVersion.Substring( _
				productVersion.Length - productVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture))
		End Get
	End Property
	
	Public Shared ReadOnly Property Copyright As String
		Get
			Return My.Application.Info.Copyright
		End Get
	End Property
	
	Public Shared ReadOnly Property HomepageName As String
		Get
			Return Product & " " & PdfKeeper.Strings.Homepage
		End Get
	End Property
	
	Public Shared ReadOnly Property License As String
		Get
			Return PdfKeeper.Strings.ProductLicense
		End Get
	End Property
	
	Public Shared ReadOnly Property ThirdPartyNotice As String
		Get
			Return PdfKeeper.Strings.ProductThirdPartyNotice
		End Get
	End Property
End Class
