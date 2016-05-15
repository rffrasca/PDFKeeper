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
	''' Gets the absolute path name of the parent folder in the user's profile
	''' where application XML files are stored.  The folder will be created if
	''' it does not exist.
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
	''' Gets the absolute path name of the parent folder in the user's profile
	''' where temporary data is stored.  The folder will be created if it does
	''' not exist.
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
	''' Gets the absolute path name of the Cache folder in the user's profile.
	''' The folder will be created if it does not exist.
	''' </summary>
	Public Shared ReadOnly Property Cache As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "Cache")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absolute path name of the Capture folder in the user's
	''' profile.  The folder will be created if it does not exist and
	''' Document Capture shortcuts to this folder will be created in the Links
	''' and SendTo folders in the user's profile.
	''' </summary>
	Public Shared ReadOnly Property Capture As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "Capture")
			Directory.CreateDirectory(pathName)
			CreateFolderShortcut(Shortcuts.DocumentCaptureInLinks, pathName)
			CreateFolderShortcut(Shortcuts.DocumentCaptureInSendTo, pathName)
			Return pathName			
		End Get
	End Property
	
	''' <summary>
	''' Gets the absolute path name of the CaptureTemp folder in the user's
	''' profile.  The folder will be created if it does not exist.
	''' </summary>
	Public Shared ReadOnly Property CaptureTemp As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "CaptureTemp")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absolute path name of the DirectUpload folder in the user's
	''' profile.  The folder will be created if it does not exist and a Direct
	''' Upload shortcut to this folder will be created in the Links folders in
	''' the user's profile.
	''' </summary>
	Public Shared ReadOnly Property DirectUpload As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "DirectUpload")
			Directory.CreateDirectory(pathName)
			CreateFolderShortcut(Shortcuts.DirectUploadInLinks, pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absolute path name to the DirectUploadTemp folder in the
	''' user's profile.  The folder will be created if it does not exist.
	''' </summary>
	Public Shared ReadOnly Property DirectUploadTemp As String
		Get
			Dim pathName As String = Path.Combine(LocalParent, "DirectUploadTemp")
			Directory.CreateDirectory(pathName)
			Return pathName
		End Get
	End Property
	
	''' <summary>
	''' Gets the absolute path name to the DirectUpload folder in the user's
	''' profile used for storing Direct Upload XML files.  The folder will be
	''' created if it does not exist.
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
