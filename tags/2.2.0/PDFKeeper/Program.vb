'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
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
		
			Dim objUserSettings As New UserSettings
			If Not objUserSettings.Read = 0 Then
				Environment.Exit(1)
			End If
						
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
