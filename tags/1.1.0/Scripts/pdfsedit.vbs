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

if Wscript.Arguments.Count > 0 then
	Set objShell = CreateObject("Wscript.Shell")
	Set objFSO = CreateObject("Scripting.FileSystemObject")

	' Verify Open Object Rexx 4.0.0 is installed
	rexxHome = objShell.ExpandEnvironmentStrings("%REXX_HOME%")
	version = objFSO.GetFileVersion(rexxHome & "\rexxhide.exe")
	if version = "" or left(version,5) <> "4.0.0" then
		message = "Open Object Rexx 4.0.0 is not " & _
			  "installed! To correct, refer to " & _
			  "the Installation section of the " & _
			  "Installation / Quick Start Guide."
		MsgBox message,16,"Error"
		Wscript.Quit
	end if

	home = objShell.RegRead("HKLM\SOFTWARE\PDFKeeper\HomeDir")
	appDir = home & "\" & "Binaries"
	objShell.CurrentDirectory = appDir

	objShell.Run "rexxhide.exe " & "pdfsedit.rex " _
				     & WScript.Arguments(0)
end if
