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

Friend Class NativeMethods

	''' <summary>
	''' Private constructor added to address CA1812.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function is used to find an open window.  lpClassName can be
	''' Nothing and lpWindowName is the text that is displayed on the
	''' titlebar of the window to find.
	''' </summary>
	''' <param name="lpClassName"></param>
	''' <param name="lpWindowName"></param>
	''' <returns>window handle or 0 if window not found</returns>
	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Public Shared Function FindWindow(ByVal lpClassName As String, _
								   	  ByVal lpWindowName As String) As IntPtr
	End Function
	
	''' <summary>
	''' This function is used to bring a window to the foreground.  hwnd
	''' is the handle of the window.
	''' </summary>
	''' <param name="hWnd"></param>
	''' <returns></returns>
	<DllImport("user32.dll")> _						 
	Public Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) _
						   As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function
		
	''' <summary>
	''' This function is used to fill a block in memory with zeros.
	''' </summary>
	''' <param name="Destination"></param>
	''' <param name="Length"></param>
	Public Declare Sub ZeroMemory Lib "kernel32.dll" Alias "RtlZeroMemory" _
								 (ByVal Destination As String, _
         					 	  ByVal Length As Integer)
End Class
