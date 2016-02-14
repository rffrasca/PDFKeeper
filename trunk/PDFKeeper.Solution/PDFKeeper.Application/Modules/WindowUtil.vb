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

Public Module WindowUtil
	''' <summary>
	''' Finds a Window or Form with the specified title bar text and if found
	''' sets it as the foreground window if "bringToFront" is True.
	''' </summary>
	''' <param name="titleBarText">Title bar text to search.</param>
	''' <param name="bringToFront">True or False to bring found window or Form
	''' to the front.</param>
	''' <returns>True or False.</returns>
	Public Function WindowFinder( _
		ByVal titleBarText As String, _
		ByVal bringToFront As Boolean) As Boolean
		
		For Each curForm As Form In My.Application.OpenForms
			If curForm.Name.ToString = titleBarText Then
				If bringToFront Then
					curForm.BringToFront
				End If
				Return True
			End If
		Next
		Dim handle As Integer = CInt( _
			NativeMethods.FindWindow(Nothing, _
			titleBarText))
		If handle > 0 Then
			If bringToFront Then
				NativeMethods.SetForegroundWindow(CType(handle, System.IntPtr))
			End If
			Return True
		End If
		Return False
	End Function
End Module
