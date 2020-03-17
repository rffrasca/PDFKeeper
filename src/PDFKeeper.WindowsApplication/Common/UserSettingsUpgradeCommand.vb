'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
''' <summary>
''' Upgrades the user settings when user.config does not exist for the current
''' version and the UpgradeSettings value in user.config is True.
''' 
''' When an upgrade is performed and the major version of the previous
''' user.config file is less than the major version of the current version,
''' then display the "additional updates required" message.
''' </summary>
''' <remarks></remarks>
Public Class UserSettingsUpgradeCommand
    Implements ICommand

    Public Sub Execute() Implements ICommand.Execute
        If IsUserConfigFound() = False Then
            ' UpgradeSettings is an application user setting.
            If My.Settings.UpgradeSettings Then
                My.Settings.Upgrade()
                My.Settings.UpgradeSettings = False
                My.Settings.Save()
                If Not GetLastUserConfigMajorVersion() Is Nothing And _
                    GetLastUserConfigMajorVersion() < _
                    Left(Application.ProductVersion, 1) Then
                    Dim messageDisplay As IMessageDisplay = New MessageDisplay
                    messageDisplay.Show( _
                        My.Resources.AdditionalUpdatesRequiredMessage, False)
                End If
            End If
        End If
    End Sub

    Private Shared Function IsUserConfigFound() As Boolean
        Dim userConfig = _
           ConfigurationManager.OpenExeConfiguration( _
               ConfigurationUserLevel.PerUserRoamingAndLocal)
        Return userConfig.HasFile
    End Function

    Private Shared Function GetLastUserConfigMajorVersion() As String
        Dim majorVersion As String = Nothing
        Dim version As String = Nothing
        Dim userConfig = ConfigurationManager.OpenExeConfiguration( _
           ConfigurationUserLevel.PerUserRoamingAndLocal)
        Dim userConfigFolderParent = _
            Directory.GetParent(Path.GetDirectoryName(userConfig.FilePath))
        For Each folderPath As String In Directory.GetDirectories( _
            userConfigFolderParent.ToString)
            version = folderPath.Split(Path.DirectorySeparatorChar).Last
            If Not version = Application.ProductVersion Then
                majorVersion = Left(version, 1)
            End If
        Next
        Return majorVersion
    End Function
End Class
