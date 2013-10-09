'********************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2010 Robert F. Frasca
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

' Upload folder selection
set objFolder = objShell.BrowseForFolder(0, _
	       "Select the Document Loader Upload folder:", 0)
if objFolder is nothing then
	Wscript.Quit
end if

' Set the upload folder environment variable
set objFolder = objFolder.Self
set WshEnv = WshShell.Environment("USER")
WshEnv("PDFKEEPER_UPLOAD") = objFolder.Path

' Prompt to reboot
if Msgbox("Before using the Document Loader, a system reboot " & _
	  "is necessary. Would you like to reboot now?", 36, _
	  "Question") = vbYes then
	WshShell.Run("shutdown -r -t 5 -f")
end if
