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

Public Class UserSettingsHelper
    Private ReadOnly configUserlevel As Configuration

    Public Sub New()
        configUserlevel = ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.PerUserRoamingAndLocal)
    End Sub

    ''' <summary>
    ''' Is this the first use of PDFKeeper?
    ''' </summary>
    ''' <returns>True or False</returns>
    Public ReadOnly Property IsFirstUse As Boolean
        Get
            If My.Settings.DbManagementSystem.Length.Equals(0) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Upgrades the user settings when user.config does not exist for the current version and the
    ''' UpgradeSettings value in user.config is True.
    ''' </summary>
    Public Sub Upgrade()
        If configUserlevel.HasFile = False Then
            If My.Settings.UpgradeSettings Then ' UpgradeSettings is an application user setting.
                My.Settings.Upgrade()
                My.Settings.UpgradeSettings = False
                My.Settings.Save()
            End If
        End If
    End Sub

    Private Function GetConfigParentPath() As DirectoryInfo
        Return Directory.GetParent(Path.GetDirectoryName(configUserlevel.FilePath))
    End Function
End Class
