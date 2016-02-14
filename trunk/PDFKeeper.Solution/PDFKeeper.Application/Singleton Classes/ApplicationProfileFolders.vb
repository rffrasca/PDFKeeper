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
''' ApplicationProfileFolders single instance class.
''' </summary>
Public NotInheritable Class ApplicationProfileFolders
	Inherits WindowsProfileFolders	
	Private Shared _instance As ApplicationProfileFolders = _
		New ApplicationProfileFolders()
	
	Overloads Shared ReadOnly Property Instance As ApplicationProfileFolders
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the parent folder in the user's profile
	''' where application XML files are stored.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property RoamingParent As String
		Get
			Return Path.Combine( _
				ApplicationProfileFolders.Instance.AppData, _
				"PDFKeeper")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the parent folder in the user's profile
	''' where temporary data is stored.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property LocalParent As String
		Get
			Return Path.Combine( _
				ApplicationProfileFolders.Instance.LocalAppData, _
				"PDFKeeper")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the Cache folder in the user's profile.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property Cache As String
		Get
			Return Path.Combine(LocalParent, "Cache")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the Capture folder in the user's
	''' profile.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property Capture As String
		Get
			Return Path.Combine(LocalParent, "Capture")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the CaptureTemp folder in the user's
	''' profile.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property CaptureTemp As String
		Get
			Return Path.Combine(LocalParent, "CaptureTemp")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the DirectUpload folder in the user's
	''' profile.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property DirectUpload As String
		Get
			Return Path.Combine(LocalParent, "DirectUpload")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the DirectUploadTemp folder in the
	''' user's profile.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property DirectUploadTemp As String
		Get
			Return Path.Combine(LocalParent, "DirectUploadTemp")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate pathname to the DirectUpload folder in the user's
	''' profile used for storing Direct Upload XML files.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property DirectUploadXml As String
		Get
			Return Path.Combine( _
				ApplicationProfileFolders.Instance.RoamingParent, _
				"DirectUpload")
		End Get
	End Property
End Class
