'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
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

Friend Class StringDecoder
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub	
	
	''' <summary>
	'''	This function will return the string of the specified SecureString
	''' object.
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
