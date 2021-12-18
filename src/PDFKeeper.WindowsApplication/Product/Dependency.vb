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
Public NotInheritable Class Dependency
    Private Sub New()
        ' All members are shared.
    End Sub

    Public Shared Function GetOracleDataAccessAssemblyPath() As Reflection.Assembly
        Try
            Dim odpDllPath As String = My.Computer.Registry.GetValue(
                "HKEY_LOCAL_MACHINE\SOFTWARE\Oracle\ODP.NET\" &
                My.Settings.ODPMVersion,
                "DllPath",
                "")
            Dim oraHomeKey As String = "HKEY_LOCAL_MACHINE\" &
                My.Computer.FileSystem.ReadAllText(
                    odpDllPath & "\oracle.key").TrimEnd
            Dim assemblyPath As String = My.Computer.Registry.GetValue(
                oraHomeKey,
                "ORACLE_HOME", "") &
                "\odp.net\managed\common\Oracle.ManagedDataAccess.dll"
            Return Reflection.Assembly.LoadFile(assemblyPath)
        Catch ex As FileNotFoundException
            Dim messageService As IMessageDisplayService = New MessageDisplayService
            messageService.Show(My.Resources.ODPMissing, True)
            Return Nothing
        End Try
    End Function

    Public Shared Sub RestoreBouncyCastle()
        Dim bouncyCastle As String = Path.Combine(Application.StartupPath,
                                                  "BouncyCastle.Crypto.dll")
        Dim bouncyCastleBackup As String = Path.Combine(Application.StartupPath,
                                                        "BouncyCastle.Crypto.dll.bak")
        If IO.File.Exists(bouncyCastle) = False Then
            IO.File.Copy(bouncyCastleBackup, bouncyCastle)
        End If
    End Sub

    Public Shared Sub SetMagickNetGhostscriptDirectory()
        MagickNET.SetGhostscriptDirectory(Application.StartupPath)
    End Sub
End Class
