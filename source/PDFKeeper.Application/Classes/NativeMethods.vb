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

Public NotInheritable Class NativeMethods
	Private Sub New()
		' Class cannot be instantiated as it only contains shared members.
		' Required for FxCop compliance (CA1053).
	End Sub

	''' <summary>
	''' Returns the window handle to the top-level window whose class name and
	''' window name match "lpClassName" and "lpWindowName".
	''' </summary>
	''' <param name="lpClassName">Window class name, can be nothing.</param>
	''' <param name="lpWindowName">Window title.</param>
	''' <returns>Window handle or 0 when window is not found.</returns>
	<DllImport("user32.dll", _
		SetLastError:=True, _
		CharSet:=CharSet.Unicode)> _
 	Friend Shared Function FindWindow( _
		ByVal lpClassName As String, _
		ByVal lpWindowName As String) As IntPtr
 	End Function
	
	''' <summary>
	''' Moves the window associated with "hWnd" to the front.
	''' </summary>
	''' <param name="hWnd"></param>
	''' <returns>True or False</returns>
	<DllImport("user32.dll")> _
	Friend Shared Function SetForegroundWindow( _
		ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function
	
	''' <summary>
	''' Returns True or False if "pszPath" is an empty directory.
	''' </summary>
	''' <param name="pszPath"></param>
	''' <returns></returns>
	<DllImport("shlwapi.dll", _
		CharSet:=CharSet.Unicode, _
		BestFitMapping:=False)> _
	Friend Shared Function PathIsDirectoryEmpty( _
		<InAttribute(), _
		MarshalAs(UnmanagedType.LPTStr)> _
		ByVal pszPath As String) As <MarshalAs(UnmanagedType.Bool)> Boolean
 	End Function
End Class
