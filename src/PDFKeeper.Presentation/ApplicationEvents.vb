' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

Imports PDFKeeper.Core.Application
Imports PDFKeeper.Core.DataAccess
Imports PDFKeeper.Core.Extensions

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim helpFile = New HelpFile
            Dim userSettingsHelper = New UserSettingsHelper
            Dim previousVersion = userSettingsHelper.LatestConfigVersion
            userSettingsHelper.Upgrade()
            If userSettingsHelper.IsFirstUse Then
                helpFile.Show("PDFKeeper.html")
            End If
            If Settings.DbManagementSystem.Length = 0 Then
                If File.Exists(DatabaseSession.LocalDatabasePath) Then
                    Settings.DbManagementSystem =
                        DatabaseSession.CompatiblePlatformName.Sqlite.ToString
                End If
            End If
            DatabaseSession.SetMessageBoxService(New MessageBoxService)
            If Settings.DbManagementSystem <>
                DatabaseSession.CompatiblePlatformName.Sqlite.ToString Then
                ' NOTE: Oracle is the only supported RDBMS at this time. To add future systems, add
                ' a ComboBox to LoginView containing the supported Databases and bind it to the
                ' DbManagementSystem setting.
                Settings.DbManagementSystem = DatabaseSession.CompatiblePlatformName.Oracle.ToString
                If LoginView.ShowDialog = DialogResult.Cancel Then
                    e.Cancel = True
                End If
            Else
                DatabaseSession.PlatformName = DatabaseSession.CompatiblePlatformName.Sqlite
            End If
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
            Dim applicationDirectory = New ApplicationDirectory
            applicationDirectory.DeleteUploadDirectoryShortcuts()
            applicationDirectory.GetDirectory(ApplicationDirectory.SpecialName.Cache).Empty
            applicationDirectory.GetDirectory(ApplicationDirectory.SpecialName.Temp).Empty
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim unhandledExceptionHandler = New UnhandledExceptionHandler(e)
            unhandledExceptionHandler.Log()
            unhandledExceptionHandler.Show()
        End Sub
    End Class
End Namespace
