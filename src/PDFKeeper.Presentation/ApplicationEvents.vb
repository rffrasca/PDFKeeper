'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
Imports PDFKeeper.Common
Imports PDFKeeper.Infrastructure

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim help = New HelpFile
            Dim previousVersion = UserSettingsConfig.Version
            UserSettingsConfig.Upgrade()
            If UserSettingsConfig.FirstUse Then
                help.Show("PDFKeeper.html")
            ElseIf previousVersion < "7.0.0.0" Then
                help.Show("Schema Upgrade for Oracle Database.html")
            End If
            If Settings.DbManagementSystem.Length = 0 Then
                If File.Exists(DbSession.LocalDatabasePath) Then
                    Settings.DbManagementSystem = DbSession.DbPlatform.Sqlite.ToString
                End If
            End If
            If Settings.DbManagementSystem <> DbSession.DbPlatform.Sqlite.ToString Then
                ' NOTE: Oracle is the only supported RDBMS at this time. To add future systems, add a
                ' ComboBox to LoginView containing the supported Databases and bind it to the
                ' DbManagementSystem setting.
                Settings.DbManagementSystem = DbSession.DbPlatform.Oracle.ToString
                If LoginForm.ShowDialog = DialogResult.Cancel Then
                    e.Cancel = True
                End If
            Else
                DbSession.Platform = DbSession.DbPlatform.Sqlite
            End If
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
            AppFolderShortcuts.Delete()
            AppFolders.Empty(AppFolders.AppFolder.Cache)
            AppFolders.Empty(AppFolders.AppFolder.Temp)
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim handler = New UnhandledExceptionHandler(e)
            handler.Log()
            handler.Show()
        End Sub
    End Class
End Namespace
