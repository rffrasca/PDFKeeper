'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but
'* WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'*************************************************************************

Public Class Window
	Dim windowTitle As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg: windowTitle"></param>
	Public Sub New(ByVal arg As String)
		windowTitle = arg
	End Sub
	
	''' <summary>
	''' This function will check if a window with the object title is already
	''' open and set it as the foreground window.
	''' </summary>
	''' <returns>True or False</returns>
	Public Function IsOpen As Boolean
		Dim handle As Integer
		handle = NativeMethods.FindWindow(Nothing, windowTitle)
		If handle > 0 Then
			NativeMethods.SetForegroundWindow(handle)
			Return True
		Else
			Return False
		End If
	End Function
	
	''' <summary>
	''' This subroutine will wait until a window with the object title is
	''' opened.
	''' </summary>
	Public Sub WaitUntilOpened
		Dim windowOpen As Boolean = False
		While windowOpen = False
			Dim oWindow As New Window(windowTitle)
			windowOpen = IsOpen
			Thread.Sleep(1000)
		End While
	End Sub
	
	''' <summary>
	''' This subroutine will wait until a window with the object title is
	''' closed.
	''' </summary>
	Public Sub WaitUntilClosed
		Dim windowOpen As Boolean = True
		While windowOpen = True
			Dim oWindow As New Window(windowTitle)
			windowOpen = IsOpen
			Thread.Sleep(1000)
		End While
	End Sub
End Class
