'********************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or
'* modify it under the terms of the GNU General Public License as
'* published by the Free Software Foundation, either version 3 of
'* the License, or (at your option) any later version.
'*
'* This program is distributed in the hope that it will be
'* useful, but WITHOUT ANY WARRANTY; without even the implied
'* warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
'* PURPOSE.  See the GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public
'* License along with this program.  If not, see
'* <http://www.gnu.org/licenses/>.
'*
'********************************************************************

set objShell = CreateObject("Shell.Application")
set WshShell = WScript.CreateObject("WScript.Shell")

set objFolder = objShell.BrowseForFolder(0, _
	       "Select the Document Loader Upload folder:", 0)
if objFolder is nothing then
	Wscript.Quit
end if

set objFolder = objFolder.Self
regHive = "HKLM\SYSTEM"
regFolder = "CurrentControlSet\Control\Session Manager\Environment"
regKey = "PDFKEEPER_UPLOAD"
regPath = regHive & "\" & regFolder & "\" & regKey
WshShell.RegWrite regPath, objFolder.Path, "REG_SZ"
