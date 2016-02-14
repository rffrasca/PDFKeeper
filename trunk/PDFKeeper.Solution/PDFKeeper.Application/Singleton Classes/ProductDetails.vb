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

''' <summary>
''' ProductDetails single instance class.
''' </summary>
Public NotInheritable Class ProductDetails
	Private Shared _instance As ProductDetails = New ProductDetails()
	Private productVersion As String = Application.ProductVersion
	
	Public Shared ReadOnly Property Instance As ProductDetails
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Gets the Name associated with the application.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property Name As String
		Get
			Return Application.ProductName
		End Get
	End Property
	
	''' <summary>
	''' Gets the Description associated with the application.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property Description As String
		Get
			Return My.Application.Info.Description
		End Get
	End Property
	
	''' <summary>
	''' Gets the Copyright associated with the application.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property Copyright As String
		Get
			Return My.Application.Info.Copyright
		End Get
	End Property
	
	''' <summary>
	''' Gets the Product Version without the build number associated with the
	''' application.
	''' </summary>
	Public ReadOnly Property Version As String
		Get
			Return Application.ProductVersion.Substring( _
				0, _
				productVersion.Length - productVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture) - 1)
		End Get
	End Property
	
	''' <summary>
	''' Gets the Build number from the Product Version associated with the
	''' application.
	''' </summary>
	Public ReadOnly Property Build As String
		Get
			Return productVersion.Substring( _
				productVersion.Length - productVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture) + 1)
		End Get
	End Property
End Class
