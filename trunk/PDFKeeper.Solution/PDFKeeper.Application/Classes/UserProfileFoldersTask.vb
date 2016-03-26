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

Public NotInheritable Class UserProfileFoldersTask
	Dim Shared ReadOnly DocumentCaptureSendToShortcut As String = _
		Path.Combine(Environment.GetFolderPath( _
		Environment.SpecialFolder.SendTo), _
		"PDFKeeper " & PdfKeeper.Strings.DocumentCapture & ".lnk")
	Dim Shared ReadOnly DocumentCaptureLinksShortcut As String = _
		Path.Combine(WindowsProfileFolders.Links, "PDFKeeper " & _
		PdfKeeper.Strings.DocumentCapture & ".lnk")
	Dim Shared ReadOnly DocumentCaptureMyDocsShortcut As String = _
		Path.Combine(Environment.GetFolderPath( _
		Environment.SpecialFolder.MyDocuments), _
		"PDFKeeper " & PdfKeeper.Strings.DocumentCapture & ".lnk")
	Dim Shared ReadOnly DirectUploadLinksShortcut As String = _
		Path.Combine(WindowsProfileFolders.Links, "PDFKeeper " & _
		PdfKeeper.Strings.DirectUpload & ".lnk")
	Dim Shared ReadOnly DirectUploadMyDocsShortcut As String = _
		Path.Combine(Environment.GetFolderPath( _
		Environment.SpecialFolder.MyDocuments), _
		"PDFKeeper " & PdfKeeper.Strings.DirectUpload & ".lnk")
		
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This subroutine will create the Document Capture folder shortcuts.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Public Shared Function CreateDocumentCaptureShortcuts As Integer
		If Directory.Exists(WindowsProfileFolders.Links) Then
			If CreateFolderShortcut( _
				DocumentCaptureLinksShortcut, _
				ApplicationProfileFolders.Capture) = 1 Then
				
				Return 1
			End If
		Else
			If CreateFolderShortcut( _
				DocumentCaptureMyDocsShortcut, _
				ApplicationProfileFolders.Capture) = 1 Then
				
				Return 1
			End If
		End If
		If CreateFolderShortcut( _
			DocumentCaptureSendToShortcut, _
			ApplicationProfileFolders.Capture) = 1 Then
			
			Return 1
		End If
		Return 0
	End Function
	
	''' <summary>
	''' This subroutine will create the Direct Upload folder shortcut.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Public Shared Function CreateDirectUploadShortcut As Integer
		If Directory.Exists(WindowsProfileFolders.Links) Then
			If CreateFolderShortcut( _
				DirectUploadLinksShortcut, _
				ApplicationProfileFolders.DirectUpload) = 1 Then
				
				Return 1
			End If
		Else
			If CreateFolderShortcut( _
				DirectUploadMyDocsShortcut, _
				ApplicationProfileFolders.DirectUpload) = 1 Then
				
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
		If Directory.Exists(WindowsProfileFolders.Links) Then
			DeleteFile(DocumentCaptureLinksShortcut, False)
		Else
			DeleteFile(DocumentCaptureMyDocsShortcut, False)
		End If
		DeleteFile(DocumentCaptureSendToShortcut, False)
	End Sub
	
	''' <summary>
	''' This subroutine will delete the Direct Upload folder shortcut.
	''' </summary>
	''' <returns></returns>
	Public Shared Sub DeleteDirectUploadShortcut
		If Directory.Exists(WindowsProfileFolders.Links) Then
			DeleteFile(DirectUploadLinksShortcut, False)
		Else
			DeleteFile(DirectUploadMyDocsShortcut, False)
		End If
	End Sub
End Class
