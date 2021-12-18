'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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
Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim help As IHelpDisplayService = New HelpDisplayService
            Dim previousVersion As String = UserConfig.CurrentVersion
            UserConfig.Upgrade()
            If UserConfig.IsFirstUse Then
                help.ShowAndWait("PDFKeeper.html")
            ElseIf previousVersion < "7.0.0.0" Then
                help.ShowAndWait("Schema Upgrade for Oracle Database.html")
            End If
            Dependency.RestoreBouncyCastle()
            Dependency.SetMagickNetGhostscriptDirectory()
            If DbInstanceProperties.Platform.Length = 0 Then
                If IO.File.Exists(UserProfile.LocalDatabasePath) Then
                    DbInstanceProperties.Platform = DatabasePlatform.Sqlite.ToString
                End If
            End If
            If DbInstanceProperties.Platform <> DatabasePlatform.Sqlite.ToString Then
                ' When adding client-server database platforms, a prompt will
                ' be needed here for the user to select the platform to use.
                DbInstanceProperties.Platform = DatabasePlatform.Oracle.ToString
                If DbInstanceProperties.Platform = DatabasePlatform.Oracle.ToString Then
                    AddHandler AppDomain.CurrentDomain.AssemblyResolve,
                        AddressOf Dependency.GetOracleDataAccessAssemblyPath
                End If
                If LoginForm.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                End If
            End If
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim exceptionHandler As New UnhandledExceptionHandler(e)
            exceptionHandler.WriteToLog()
            exceptionHandler.Show()
        End Sub
    End Class
End Namespace
