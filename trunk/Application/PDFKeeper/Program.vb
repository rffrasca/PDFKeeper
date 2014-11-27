'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2015 Robert F. Frasca
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

Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
	Partial Class MyApplication
		Public Sub New()
			MyBase.New(AuthenticationMode.Windows)
			Me.IsSingleInstance = True
			Me.EnableVisualStyles = True
			AddHandler AppDomain.CurrentDomain.UnhandledException, _
				AddressOf MyApplication_UnhandledException
				' MySettings are not supported in SharpDevelop.
			Me.SaveMySettingsOnExit = False
			Me.ShutDownStyle = ShutdownMode.AfterMainFormCloses
				
			' Create User Profile folders and delete deprecated folders.
			If UserProfileFoldersTask.Create = 1 Then
				Environment.Exit(1)
			End If
			If UserProfileFoldersTask.DeleteDeprecated = 1 Then
				Environment.Exit(1)
			End If
			If DirectUpload.CreateMissingFolders = 1 Then
				Environment.Exit(1)
			End If
			
			' Create Document Capture Shortcuts.
			If UserProfileFoldersTask.CreateDocumentCaptureShortcuts = 1 Then
				Environment.Exit(1)
			End If
			
			' Create Direct Upload Shortcut.
			If UserProfileFoldersTask.CreateDirectUploadShortcut = 1 Then
				Environment.Exit(1)
			End If
			
			' Read User Settings.
			Dim oUserSettings As New UserSettings
			If Not oUserSettings.Read = 0 Then
				Environment.Exit(1)
			End If
			
			' Show Database Connection Dialog to the user.
			If DatabaseConnectionForm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
				Environment.Exit(1)
			End If
		End Sub
		
		Protected Overrides Sub OnCreateMainForm()
			Me.MainForm = My.Forms.MainForm
		End Sub
		
		''' <summary>
		''' This subroutine is the application global unhandled exception
		''' handler that will display the error to the user.		
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub MyApplication_UnhandledException(sender As Object, _
				e As System.UnhandledExceptionEventArgs)
			MessageBoxWrapper.ShowError(e.ExceptionObject.ToString)
		End Sub
	End Class
End Namespace
