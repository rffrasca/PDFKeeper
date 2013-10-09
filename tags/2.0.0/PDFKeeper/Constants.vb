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

Public Module Constants
	Friend ReadOnly TempDir As String = _
		My.Computer.FileSystem.SpecialDirectories.Temp
	Friend ReadOnly MyDocsDir As String = _
		My.Computer.FileSystem.SpecialDirectories.MyDocuments
	Friend ReadOnly AppDataDir As String = _
		Environment.GetEnvironmentVariable("APPDATA")
End Module
