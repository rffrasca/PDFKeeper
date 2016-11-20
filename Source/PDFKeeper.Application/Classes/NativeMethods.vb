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

Friend Class NativeMethods
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function is used to find an open window.  lpClassName can be
	''' Nothing and lpWindowName is the text that is displayed on the titlebar
	''' of the window to find.
	''' </summary>
	''' <param name="lpClassName"></param>
	''' <param name="lpWindowName"></param>
	''' <returns>window handle or 0 if window not found</returns>
	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Friend Shared Function FindWindow(ByVal lpClassName As String, _
	ByVal lpWindowName As String) As IntPtr
	End Function
		
	''' <summary>
	''' This function is used to bring a window to the foreground.  hWnd is the
	''' handle of the window.
	''' </summary>
	''' <param name="hWnd"></param>
	''' <returns></returns>
	<DllImport("user32.dll")> _						 
	Friend Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) _
	As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function
	
	''' <summary>
	''' This function will determine if the specified path is an empty
	''' directory.
	''' </summary>
	''' <param name="pszPath"></param>
	''' <returns>True or False</returns>
	<DllImport("shlwapi.dll", CharSet:=CharSet.Unicode, _
		BestFitMapping:=False)> _
	Friend Shared Function PathIsDirectoryEmpty( _
		<InAttribute(), MarshalAs(UnmanagedType.LPTStr)> _
		ByVal pszPath As String) As <MarshalAs(UnmanagedType.Bool)> Boolean
 	End Function
End Class
