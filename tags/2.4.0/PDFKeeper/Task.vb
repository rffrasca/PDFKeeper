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

Public Class Task
	Dim taskName As String
	Dim taskArgs As String
	Dim windowTitle As String
	Dim taskId As Integer
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg1: taskName"></param>
	''' <param name="arg2: taskArgs"></param>
	''' <param name="arg3: windowTitle"></param>
	Public Sub New(ByVal arg1 As String, ByVal arg2 As String, _
				   ByVal arg3 As String)
		taskName = arg1
		taskArgs = arg2
		windowTitle = arg3
	End Sub
	
	''' <summary>
	''' This subroutine is the class overload constructor.
	''' </summary>
	''' <param name="arg: taskId"></param>
	Public Sub New(ByVal arg As Integer)
		taskId = arg		
	End Sub
	
	''' <summary>
	''' This function will start the object window task only if a window with
	''' the object window title is not already open.  If the task is started,
	''' this subroutine will wait until a window with the object window title
	''' has been opened.
	''' </summary>
	''' <returns>
	''' Task ID or 0 if a window with object window title is already
	''' open.
	''' </returns>
	Public Function StartWindow As Integer
		Dim procId As Integer
		Thread.Sleep(3000)
		Dim oWindow1 As New Window(windowTitle)
		If oWindow1.IsOpen = False Then
			Using oProcess As New Process
				oProcess.StartInfo.FileName = taskName
				oprocess.StartInfo.Arguments = taskArgs
				
				' UseShellExecute must be set to False to suppress
				' "Open File - Security Warning" dialog.
				oProcess.StartInfo.UseShellExecute = False
				
				oProcess.StartInfo.ErrorDialog = True
				oProcess.Start
				procId = oProcess.Id
			End Using
		End If
		Dim oWindow2 As New Window(windowTitle)
		oWindow2.WaitUntilOpened
		Return procId
	End Function
	
	''' <summary>
	''' This subroutine will terminate the object task ID.
	''' </summary>
	Public Sub Terminate
		Try
			Using oProcess As Process = Process.GetProcessById(taskId)
				If oProcess.CloseMainWindow Then
					If Not oProcess.WaitForExit(5000) Then
						oProcess.Kill
						Thread.Sleep(5000)
					End If
				Else
					oProcess.Kill
					Thread.Sleep(5000)
				End If
			End Using
		Catch ex As System.ComponentModel.Win32Exception
		Catch ex As ArgumentException
		End Try
	End Sub
End Class
