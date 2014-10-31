'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2014 Robert F. Frasca
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

Public NotInheritable Class WindowFinder
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will check if a window with "titleBarText" exists and set
	''' it as the foreground window if "forefront" is True. 
	''' </summary>
	''' <param name="titleBarText"></param>
	''' <param name="foreFront"></param>
	''' <returns>True or False</returns>
	Public Shared Function Exists(ByVal titleBarText As String, _
								  ByVal forefront As Boolean) As Boolean
		If FindForm(titleBarText, forefront) Then
			Return True
		End If
		If FindWindow(titleBarText, forefront) Then
			Return True
		End If
		Return False
	End Function
	
	''' <summary>
	''' This function will find a form with "titleBarText" if it exists and
	''' set it as the foreground window if "forefront" is True. 
	''' </summary>
	''' <param name="titleBarText"></param>
	''' <param name="forefront"></param>
	''' <returns>True or False</returns>
	Private Shared Function FindForm(ByVal titleBarText As String, _
									 ByVal forefront As Boolean) As Boolean
		Dim oForm As Form
		For Each oForm In My.Application.OpenForms
			If oForm.Name.ToString = titleBarText Then
				If foreFront Then
					oForm.BringToFront
				End If
				Return True
			End If
		Next
		Return False
	End Function
	
	''' <summary>
	''' This function will find a window with "titleBarText" if it exists and
	''' set it as the foreground window if "forefront" is True. 
	''' </summary>
	''' <param name="titleBarText"></param>
	''' <param name="forefront"></param>
	''' <returns>True or False</returns>
	Private Shared Function FindWindow(ByVal titleBarText As String, _
									   ByVal forefront As Boolean) As Boolean
		Dim handle As Integer
		handle = CInt(NativeMethods.FindWindow(Nothing, titleBarText))
		If handle > 0 Then
			If foreFront Then
				NativeMethods.SetForegroundWindow(CType(handle, System.IntPtr))
			End If
			Return True
		Else
			Return False
		End If
	End Function
End Class
