'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2013 Robert F. Frasca
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

Public NotInheritable Class UserProfileFoldersTask
	Dim Shared ReadOnly DocumentCaptureSendToShortcut As String = _
		Path.Combine(Environment.GetFolderPath( _
		Environment.SpecialFolder.SendTo), _
		"PDFKeeper " & UserProfileFoldersTask_Strings.DocumentCapture & ".lnk")
	Dim Shared ReadOnly DocumentCaptureLinksShortcut As String = _
		Path.Combine(LinksDir, "PDFKeeper " & _
		UserProfileFoldersTask_Strings.DocumentCapture & ".lnk")
	Dim Shared ReadOnly DocumentCaptureMyDocsShortcut As String = _
		Path.Combine(Environment.GetFolderPath( _
		Environment.SpecialFolder.MyDocuments), _
		"PDFKeeper " & UserProfileFoldersTask_Strings.DocumentCapture & ".lnk")
	Dim Shared ReadOnly DirectUploadLinksShortcut As String = _
		Path.Combine(LinksDir, "PDFKeeper " & _
		UserProfileFoldersTask_Strings.DirectUpload & ".lnk")
	Dim Shared ReadOnly DirectUploadMyDocsShortcut As String = _
		Path.Combine(Environment.GetFolderPath( _
		Environment.SpecialFolder.MyDocuments), _
		"PDFKeeper " & UserProfileFoldersTask_Strings.DirectUpload & ".lnk")
		
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will create User Profile folders needed for PDFKeeper
	''' operation.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Public Shared Function Create As Integer
		Dim folders As New ArrayList
		folders.Add(RootDataDir)
		folders.Add(UploadXmlDir)
		folders.Add(LocAppDataDir)
		folders.Add(CaptureDir)
		Folders.Add(CaptureTempDir)
		folders.Add(UploadDir)
		folders.Add(UploadTempDir)
		folders.Add(CacheDir)
		For Each folder As String In folders
			If FolderTask.Create(folder) = 1 Then
				Return 1
			End If
		Next
		Return 0
	End Function
	
	''' <summary>
	''' This function will delete User Profile folders no longer needed for
	''' PDFKeeper operation.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Public Shared Function DeleteDeprecated As Integer
		Dim folders As New ArrayList
		folders.Add(OldCacheDir)
		folders.Add(UploadLogDir)
		For Each folder As String In folders
			If FolderTask.Delete(folder, False) = 1 Then
				Return 1
			End If
		Next
		Return 0
	End Function
	
	''' <summary>
	''' This subroutine will create the Document Capture folder shortcuts.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Public Shared Function CreateDocumentCaptureShortcuts As Integer
		If Directory.Exists(LinksDir) Then
			If FolderTask.CreateShortcutToFolder( _
				DocumentCaptureLinksShortcut, CaptureDir) = 1 Then
				Return 1
			End If
		Else
			If FolderTask.CreateShortcutToFolder( _
				DocumentCaptureMyDocsShortcut, CaptureDir) = 1 Then
				Return 1
			End If
		End If
		If FolderTask.CreateShortcutToFolder( _
			DocumentCaptureSendToShortcut, CaptureDir) = 1 Then
			Return 1
		End If
		Return 0
	End Function
	
	''' <summary>
	''' This subroutine will create the Direct Upload folder shortcut.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Public Shared Function CreateDirectUploadShortcut As Integer
		If Directory.Exists(LinksDir) Then
			If FolderTask.CreateShortcutToFolder( _
				DirectUploadLinksShortcut, UploadDir) = 1 Then
				Return 1
			End If
		Else
			If FolderTask.CreateShortcutToFolder( _
				DirectUploadMyDocsShortcut, UploadDir) = 1 Then
				Return 1
			End If
		End If
		Return 0
	End Function
	
	''' <summary>
	''' This subroutine will delete the Document Capture folder shortcuts.
	''' </summary>
	''' <returns></returns>
	Public Shared Sub DeleteDocumentCaptureShortcuts
		If Directory.Exists(LinksDir) Then
			FileTask.Delete(DocumentCaptureLinksShortcut, False)
		Else
			FileTask.Delete(DocumentCaptureMyDocsShortcut, False)
		End If
		FileTask.Delete(DocumentCaptureSendToShortcut, False)
	End Sub
	
	''' <summary>
	''' This subroutine will delete the Direct Upload folder shortcut.
	''' </summary>
	''' <returns></returns>
	Public Shared Sub DeleteDirectUploadShortcut
		If Directory.Exists(LinksDir) Then
			FileTask.Delete(DirectUploadLinksShortcut, False)
		Else
			FileTask.Delete(DirectUploadMyDocsShortcut, False)
		End If
	End Sub
End Class
