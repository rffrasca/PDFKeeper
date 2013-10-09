'******************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public Class FormDetect
	Dim formName As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg: formName"></param>
	Public Sub New(ByVal arg As String)
		formName = arg
	End Sub
	
	''' <summary>
	''' This function will check if a form with the object name is already
	''' open. 
	''' </summary>
	''' <returns>True or False</returns>
	Public Function IsOpen As Boolean
		Dim formOpen As Boolean = False
		Dim oForm As Form
		For Each oForm In My.Application.OpenForms
			If oForm.Name.ToString = formName Then
				formOpen = True
			End If
		Next
		Return formOpen
	End Function
	
	''' <summary>
	''' This subroutine will wait until a form with the object name is opened.
	''' </summary>
	Public Sub WaitUntilOpened
		Dim formOpen As Boolean = False
		While formOpen = False
			Dim oFormDetect As New FormDetect(formName)
			formOpen = IsOpen
			Thread.Sleep(1000)
		End While
	End Sub
End Class
