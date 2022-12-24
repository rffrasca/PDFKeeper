'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
Imports System.Runtime.InteropServices

Public NotInheritable Class NativeMethods
    ''' <summary>
    ''' Gets the full path of a known folder identified by the KNOWNFOLDERID of the folder.
    ''' </summary>
    ''' <param name="rfid">Reference to the KNOWNFOLDERID</param>
    ''' <param name="dwFlags">Flags that specify special retrieval options. This value can be 0.</param>
    ''' <param name="hToken">Access token that represents a particular user. Nothing represents the current user.</param>
    ''' <returns></returns>
    <DllImport("shell32.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True, PreserveSig:=False)>
    <DefaultDllImportSearchPaths(DllImportSearchPath.System32)>
    Friend Shared Function SHGetKnownFolderPath(<MarshalAs(UnmanagedType.LPStruct)> ByVal rfid As Guid,
                                                ByVal dwFlags As UInteger, ByVal hToken As IntPtr) As String
    End Function
End Class
