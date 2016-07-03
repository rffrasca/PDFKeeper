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
'* Date: 7/3/2016
'* Time: 10:43 AM
'*
'******************************************************************************

Public NotInheritable Class ApplicationInfo
	Private Shared appVersion As String = My.Application.Info.Version.ToString
	
	Private Sub New()
		' Because type 'ApplicationInfo' contains only 'Shared' members, add a
		' default private constructor to prevent the compiler from adding a
		' default public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Gets application title.
	''' </summary>
	Public Shared ReadOnly Property Title As String
		Get
			Return My.Application.Info.Title
		End Get
	End Property
	
	''' <summary>
	''' Gets application description.
	''' </summary>
	Public Shared ReadOnly Property Description As String
		Get
			Return My.Application.Info.Description
		End Get
	End Property
	
	''' <summary>
	''' Gets application version without build number.
	''' </summary>
	Public Shared ReadOnly Property Version As String
		Get
			Return appVersion.Substring(0, _
				appVersion.Length - appVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture) - 1)
		End Get
	End Property
	
	''' <summary>
	''' Gets application version build number.
	''' </summary>
	Public Shared ReadOnly Property Build As String
		Get
			Return appVersion.Substring(0, _
				appVersion.Length - appVersion.LastIndexOf(".", _
				StringComparison.CurrentCulture))
		End Get
	End Property
	
	''' <summary>
	''' Gets application copyright.
	''' </summary>
	Public Shared ReadOnly Property Copyright As String
		Get
			Return My.Application.Info.Copyright
		End Get
	End Property
End Class
