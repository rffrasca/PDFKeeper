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

Public NotInheritable Class Shortcuts
	Private Sub New()
		' Class cannot be instantiated as it only contains shared members.
		' Required for FxCop compliance (CA1053).
	End Sub
	
	''' <summary>
	''' Gets the absolute path name of the Document Capture shortcut in the
	''' Links folder of the user's profile.
	''' </summary>
	Public Shared ReadOnly Property DocumentCaptureInLinks As String
		Get
			Return Path.Combine( _
				WindowsProfileFolders.Links, _
				"PDFKeeper " & PdfKeeper.Strings.DocumentCapture & ".lnk")
		End Get
	End Property
		
	''' <summary>
	''' Gets the absolute path name of the Document Capture shortcut in the
	''' SendTo folder of the user's profile.
	''' </summary>
	Public Shared ReadOnly Property DocumentCaptureInSendTo As String
		Get
			Return Path.Combine( _
				WindowsProfileFolders.SendTo, _
				"PDFKeeper " & PdfKeeper.Strings.DocumentCapture & ".lnk")
		End Get
	End Property
	
	''' <summary>
	''' Gets the absolute path name of the Direct Upload shortcut in the Links
	''' folder of the user's profile.
	''' </summary>
	Public Shared ReadOnly Property DirectUploadInLinks As String
		Get
			Return Path.Combine( _
				WindowsProfileFolders.Links, _
				"PDFKeeper " & PdfKeeper.Strings.DirectUpload & ".lnk")
		End Get
	End Property
End Class
