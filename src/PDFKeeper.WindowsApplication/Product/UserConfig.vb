'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Public NotInheritable Class UserConfig
    Private Sub New()
        ' All members are shared.
    End Sub

    ''' <summary>
    ''' Gets the version of the current user.config file.
    ''' </summary>
    ''' <returns>Version as x.x.x.x</returns>
    Public Shared ReadOnly Property CurrentVersion As String
        Get
            Dim lastVersion As String = Nothing
            Try
                Dim userConfig = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.PerUserRoamingAndLocal)
                Dim userConfigFolderParent =
                    Directory.GetParent(IO.Path.GetDirectoryName(userConfig.FilePath))
                Dim version As String
                For Each folderPath As String In Directory.GetDirectories(
                    userConfigFolderParent.ToString)
                    version = folderPath.Split(IO.Path.DirectorySeparatorChar).Last
                    lastVersion = version
                Next
            Catch ex As DirectoryNotFoundException
            End Try
            Return lastVersion
        End Get
    End Property

    ''' <summary>
    ''' Gets if this is the first use of PDFKeeper.
    ''' </summary>
    ''' <returns>True or False</returns>
    Public Shared ReadOnly Property IsFirstUse As Boolean
        Get
            If My.Settings.DbManagementSystem.Length = 0 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Upgrades the user settings when user.config does not exist for the current
    ''' version and the UpgradeSettings value in user.config is True.
    ''' </summary>
    Public Shared Sub Upgrade()
        If IsUserConfigFound() = False Then
            ' UpgradeSettings is an application user setting.
            If My.Settings.UpgradeSettings Then
                My.Settings.Upgrade()
                My.Settings.UpgradeSettings = False
                My.Settings.Save()
            End If
        End If
    End Sub

    Private Shared Function IsUserConfigFound() As Boolean
        Dim userConfig =
           ConfigurationManager.OpenExeConfiguration(
               ConfigurationUserLevel.PerUserRoamingAndLocal)
        Return userConfig.HasFile
    End Function
End Class
