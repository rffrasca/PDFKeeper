﻿'******************************************************************************
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
'* Date: 7/9/2016
'* Time: 5:45 PM
'*
'******************************************************************************

Public Module SecureStringExt
	''' <summary>
	''' Gets the string value from the specified SecureString object.
	''' </summary>
	''' <param name="param">SecureString object.</param>
	''' <returns>String value.</returns>
	<ExtensionAttribute> _
	Friend Function GetString(ByVal param As SecureString) As String
		Dim secureStringPtr As IntPtr
		Try
			secureStringPtr = Marshal.SecureStringToGlobalAllocUnicode(param)
			Return Marshal.PtrToStringAuto(secureStringPtr)
		Finally
			Marshal.ZeroFreeGlobalAllocUnicode(secureStringPtr)
		End Try
	End Function	
End Module