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

Friend Class WinProcess
	
	''' <summary>
	''' Private constructor added to address CA1812.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will start a process only if a window with
	''' "windowTitle" is not already open.  If the process is started, this
	''' subroutine will wait until a window with the title of "windowTitle"
	''' is open.
	''' </summary>
	''' <param name="processName"></param>
	''' <param name="processArgs"></param>
	''' <param name="windowTitle"></param>
	''' <returns>
	''' Process ID or 0 if a window with title of "windowTitle" is already
	''' open.
	''' </returns>
	Public Shared Function Start(ByVal processName As String, _
							 	 ByVal processArgs As String, _
								 ByVal windowTitle As String) As Integer
		Dim procId As Integer
		Thread.Sleep(3000)
		If IsWindowAlreadyOpen(windowTitle) = False Then
			Using objProcess As New Process
				objProcess.StartInfo.FileName = processName
				objprocess.StartInfo.Arguments = processArgs
				
				' UseShellExecute must be set to False to suppress
				' "Open File - Security Warning" dialog.
				objProcess.StartInfo.UseShellExecute = False
				
				objProcess.StartInfo.ErrorDialog = True
				objProcess.Start
				procId = objProcess.Id
			End Using
		End If
		Dim windowOpen As Boolean = False
		While windowOpen = False
			windowOpen = IsWindowAlreadyOpen(windowTitle)
			Thread.Sleep(1000)
		End While
		Return procId
	End Function

	''' <summary>
	''' This subroutine will terminate the specified process ID.
	''' </summary>
	''' <param name="procId"></param>
	Public Shared Sub Terminate(ByVal procId As Integer)
		Try
			Using objProcess As Process = Process.GetProcessById(procId)
				If objProcess.CloseMainWindow Then
					If Not objProcess.WaitForExit(5000) Then
						objProcess.Kill
						Thread.Sleep(5000)
					End If
				Else
					objProcess.Kill
					Thread.Sleep(5000)
				End If
			End Using
		Catch ex As System.ComponentModel.Win32Exception
		Catch ex As ArgumentException
		End Try
	End Sub
	
	''' <summary>
	''' This function will check if a window with the title "windowTitle" is
	''' already open and set it as the foreground window, if already open.
	''' </summary>
	''' <param name="windowTitle"></param>
	''' <returns>True or False</returns>
	Public Shared Function IsWindowAlreadyOpen(ByVal windowTitle As String) As Boolean
		Dim handle As Integer
		handle = NativeMethods.FindWindow(Nothing, windowTitle)
		If handle > 0 Then
			NativeMethods.SetForegroundWindow(handle)
			Return True
		Else
			Return False
		End If
	End Function
End Class
