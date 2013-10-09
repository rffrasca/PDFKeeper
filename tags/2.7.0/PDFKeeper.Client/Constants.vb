'******************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public Module Constants
	
	' Folders.
	Friend ReadOnly TempDir As String = _
		My.Computer.FileSystem.SpecialDirectories.Temp
	Friend ReadOnly AppDataDir As String = _
		Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
	Friend ReadOnly RootDataDir As String = AppDataDir & "\PDFKeeper"
	Friend ReadOnly LocAppDataDir As String = _
		Environment.GetFolderPath( _
		Environment.SpecialFolder.LocalApplicationData) & "\PDFKeeper"
	Friend ReadOnly CacheDir As String = LocAppDataDir & "\Cache"
	Friend ReadOnly OldCacheDir As String = RootDataDir & "\Cache"
	Friend ReadOnly UploadLogDir As String = LocAppDataDir & "\UploadLogs"
	Friend ReadOnly DocsDir As String = _
		My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\PDFKeeper"
	Friend ReadOnly ArchiveDir As String = _
		My.Computer.FileSystem.SpecialDirectories.MyDocuments & _
		"\PDFKeeper\Archive"
											   
	' Create upload log file names.
	Dim oDateTime As DateTime = DateTime.Now
	Dim dateTimeFormat As String = "yyyy-MM-dd_HH.mm"
	Dim sessionDateTime As String = oDateTime.ToString(dateTimeFormat, _
											  CultureInfo.CurrentCulture)
	Friend ReadOnly UploadedLog As String = UploadLogDir & "\" & _
											sessionDateTime & "_uploaded.log"
	Friend ReadOnly SkippedLog As String = UploadLogDir & "\" & _
										   sessionDateTime & "_skipped.log"
End Module
