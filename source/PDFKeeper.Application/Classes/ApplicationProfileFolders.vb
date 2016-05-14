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

Public NotInheritable Class ApplicationProfileFolders
	Private Sub New()
		' Class cannot be instantiated as it only contains shared members.
		' Required for FxCop compliance (CA1053).
	End Sub
	
	''' <summary>
	''' Gets the absoluate path name to the parent folder in the user's profile
	''' where application XML files are stored.  If the folder does not exist,
	''' it will be created.
	''' </summary>
	Public Shared ReadOnly Property RoamingParent As String
		Get
			Dim pathName As String = Path.Combine( _
				WindowsProfileFolders.AppData, _
				"PDFKeeper")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate path name to the parent folder in the user's profile
	''' where temporary data is stored.  If the folder does not exist, it will
	''' be created.
	''' </summary>
	Public Shared ReadOnly Property LocalParent As String
		Get
			Dim pathName As String = Path.Combine( _
				WindowsProfileFolders.LocalAppData, _
				"PDFKeeper")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate path name to the Cache folder in the user's profile.
	''' If the folder does not exist, it will be created.
	''' </summary>
	Public Shared ReadOnly Property Cache As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "Cache")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate path name to the Capture folder in the user's
	''' profile.  If the folder does not exist, it will be created.
	''' </summary>
	Public Shared ReadOnly Property Capture As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "Capture")
			Directory.CreateDirectory(pathName)
			Return pathName			
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate path name to the CaptureTemp folder in the user's
	''' profile.  If the folder does not exist, it will be created.
	''' </summary>
	Public Shared ReadOnly Property CaptureTemp As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "CaptureTemp")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate path name to the DirectUpload folder in the user's
	''' profile.  If the folder does not exist, it will be created.
	''' </summary>
	Public Shared ReadOnly Property DirectUpload As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "DirectUpload")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate path name to the DirectUploadTemp folder in the
	''' user's profile.  If the folder does not exist, it will be created.
	''' </summary>
	Public Shared ReadOnly Property DirectUploadTemp As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "DirectUploadTemp")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absoluate path name to the DirectUpload folder in the user's
	''' profile used for storing Direct Upload XML files.  If the folder does
	''' not exist, it will be created.
	''' </summary>
	Public Shared ReadOnly Property DirectUploadXml As String
		Get
			Dim pathName As String = Path.Combine( _
				ApplicationProfileFolders.RoamingParent, _
				"DirectUpload")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
End Class
