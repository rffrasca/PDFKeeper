'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2014 Robert F. Frasca
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

Public Module UserProfileFolders
	Friend ReadOnly LinksDir As String = Path.Combine( _
		Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _
								 "Links")
	Friend ReadOnly AppDataDir As String = _
		Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
	Friend ReadOnly RootDataDir As String = Path.Combine(AppDataDir, "PDFKeeper")
	Friend ReadOnly UploadXmlDir As String = Path.Combine(RootDataDir, _
		"DirectUpload")
	Friend ReadOnly LocAppDataDir As String = Path.Combine( _
		Environment.GetFolderPath( _
		Environment.SpecialFolder.LocalApplicationData), "PDFKeeper")
	Friend ReadOnly CaptureDir As String = Path.Combine(LocAppDataDir, _
		"Capture")
	Friend ReadOnly CaptureTempDir As String = Path.Combine(LocAppDataDir, _
		"CaptureTemp")
	Friend ReadOnly UploadDir As String = Path.Combine(LocAppDataDir, _
		"DirectUpload")
	Friend ReadOnly UploadTempDir As String = Path.Combine(LocAppDataDir, _
		"DirectUploadTemp")
	Friend ReadOnly CacheDir As String = Path.Combine(LocAppDataDir, "Cache")
End Module
