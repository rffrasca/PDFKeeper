'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
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

Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
	Partial Class MyApplication
		Public Sub New()
			MyBase.New(AuthenticationMode.Windows)
			Me.IsSingleInstance = True
			Me.EnableVisualStyles = True
			
			' MySettings are not supported in SharpDevelop.
			Me.SaveMySettingsOnExit = False
			
			Me.ShutDownStyle = ShutdownMode.AfterMainFormCloses

			' Create PDFKeeper application data folders and delete the legacy
			' cache folder if it does exist.
			Dim folders As New ArrayList
			folders.Add(RootDataDir)
			folders.Add(LocAppDataDir)
			folders.Add(CacheDir)
			folders.Add(UploadLogDir)
			For Each folder As String In folders
				Dim oFolderTools1 As New FolderTools(folder)
				If oFolderTools1.Create = 1 Then
					Environment.Exit(1)
				End If
			Next
			Dim oFolderTools2 As New FolderTools(OldCacheDir)
			If oFolderTools2.Delete = 1 Then
				Environment.Exit(1)
			End If
						
			' Read in User Settings.
			Dim oUserSettings As New UserSettings
			If Not oUserSettings.Read = 0 Then
				Environment.Exit(1)
			End If
			
			' Show Database Connection Dialog to the user.
			If DatabaseConnectionForm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
				DatabaseConnectionString.Dispose
				Environment.Exit(1)
			End If
		End Sub
		
		Protected Overrides Sub OnCreateMainForm()
			Me.MainForm = My.Forms.MainForm
		End Sub
	End Class
End Namespace
