'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2010 Robert F. Frasca
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
	''' This subroutine will start a process only if a window with
	''' "windowTitle" is not already open.  If the process is started, this
	''' subroutine will wait until a window with the title of "windowTitle"
	''' is open.
	''' </summary>
	''' <param name="processName"></param>
	''' <param name="processArgs"></param>
	''' <param name="windowTitle"></param>
	Public Shared Sub Start(ByVal processName As String, _
							ByVal processArgs As String, _
							ByVal windowTitle as String)
		If IsWindowAlreadyOpen(windowTitle) = False Then
			Using objProcess As New Process
				objProcess.StartInfo.FileName = processName
				objprocess.StartInfo.Arguments = processArgs
				
				' UseShellExecute must be set to False to suppress
				' "Open File - Security Warning" dialog.
				objProcess.StartInfo.UseShellExecute = False
				
				objProcess.StartInfo.ErrorDialog = True
				objProcess.Start
			End Using
		End If
		Dim windowOpen As Boolean = False
		While windowOpen = False
			windowOpen = IsWindowAlreadyOpen(windowTitle)
			Thread.Sleep(1000)
		End While
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
