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

output = "|" & trim(ReadValue("title.txt")) & _
	 "|" & trim(ReadValue("author.txt")) & _
	 "|" & trim(ReadValue("subject.txt")) & _
	 "|" & trim(ReadValue("keywords.txt")) & _
	 "|||" & trim(ReadValue("filename.txt"))

Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.CreateTextFile("pdfdb.txt", True)
objFile.WriteLine(output)
objFile.Close

Wscript.Quit

Function ReadValue(metafile)
	Set objFSO = CreateObject("Scripting.FileSystemObject")
	Set objFile = objFSO.GetFile(metafile)
	if objFile.Size > 0 then
		Set objTextFile = objFSO.OpenTextFile(metafile, 1)
		if metafile = "filename.txt" then
			ReadValue = objTextFile.Readline
		else
			ReadValue = mid(objTextFile.Readline,17)
		end if
		objTextFile.Close
	else
		ReadValue = ""
	end if
	objFSO.DeleteFile(metafile)
End Function
