'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
'* Copyright (C) 2009-2015 Robert F. Frasca
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

Public NotInheritable Class StringUtil
	
	''' <summary>
	''' Required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New()
	End Sub	
	
	''' <summary>
	''' Return True or False if the specified file or folder name contains one
	''' or more invalid characters.
	''' </summary>
	''' <param name="fileOrFolderName"></param>
	''' <returns>True or False</returns>
	Public Shared Function ContainsInvalidFileNameChars(ByVal fileOrFolderName As String) As Boolean
		For Each invalidChar In Path.GetInvalidFileNameChars()
			If fileOrFolderName.Contains(invalidChar) Then
				Return True
			End If
		Next
		Return False
	End Function
	
	''' <summary>
	'''	Return the string of the specified SecureString.
	''' </summary>
	''' <param name="textSecureString"></param>
	''' <returns>string</returns>
	Friend Shared Function SecureStringToString(ByVal textSecureString As _
												SecureString) As String
		Dim textStringPtr As IntPtr
		Try
			textStringPtr = Marshal.SecureStringToBSTR(textSecureString)
			Return Marshal.PtrToStringBSTR(textStringPtr)
		Finally
			Marshal.ZeroFreeBSTR(textStringPtr)
		End Try
	End Function
End Class
