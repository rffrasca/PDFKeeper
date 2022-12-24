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
Public NotInheritable Class UserSettingsConfig
    ''' <summary>
    ''' Gets the version of the latest user.config file.
    ''' </summary>
    ''' <returns>Version formatted as x.x.x.x or Nothing if user.config does not exist.</returns>
    Public Shared ReadOnly Property Version As String
        Get
            Dim _Version As String = Nothing
            Try
                Dim configVersion As String
                For Each dirInfo In GetConfigParentPath.GetDirectories
                    configVersion = dirInfo.FullName.Split(Path.DirectorySeparatorChar).Last
                    _Version = configVersion
                Next
            Catch ex As DirectoryNotFoundException
            End Try
            Return _Version
        End Get
    End Property

    '''' <summary>
    '''' Is this the first use of PDFKeeper.
    '''' </summary>
    '''' <returns>True or False</returns>
    Public Shared ReadOnly Property FirstUse As Boolean
        Get
            If My.Settings.DbManagementSystem.Length = 0 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Upgrades the user settings when user.config does not exist for the current version and the UpgradeSettings
    ''' value in user.config is True.
    ''' </summary>
    Public Shared Sub Upgrade()
        If ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.PerUserRoamingAndLocal).HasFile = False Then
            If My.Settings.UpgradeSettings Then ' UpgradeSettings is an application user setting.
                Dim previousVersion = Version
                Dim previousNameSpace = "PDFKeeper.WindowsApplication.My.MySettings"
                Dim newNameSpace = "PDFKeeper.Presentation.My.MySettings"
                Dim previousConfig As String = Nothing
                Dim text As String = Nothing
                If previousVersion IsNot Nothing Then
                    previousConfig = Path.Combine(GetConfigParentPath.FullName, previousVersion, "user.config")
                    If previousVersion < "8.0.0.0" Then
                        Text = File.ReadAllText(previousConfig).Replace(previousNameSpace, newNameSpace).Replace(
                            "PreviewImageResolution", "PreviewPixelDensity").Replace(
                            "SearchResultsSelectLastRow", "SelectLastDocumentRow").Replace(
                            "OpenPdfWithDefaultApplication", "ShowPdfWithDefaultApplication").Replace(
                            "AddPdfDeleteInputPdfOnOK", "AddPdfDeleteSelectedPdfWhenAdded").Replace(
                            "ShowFlaggedDocumentsOnStartup", "ListFlaggedDocumentsOnStartup")
                        File.WriteAllText(previousConfig, Text)
                    End If
                End If
                My.Settings.Upgrade()
                My.Settings.UpgradeSettings = False
                My.Settings.Save()
                If text IsNot Nothing Then
                    text = File.ReadAllText(previousConfig).Replace(newNameSpace, previousNameSpace).Replace(
                        "PreviewPixelDensity", "PreviewImageResolution").Replace(
                        "SelectLastDocumentRow", "SearchResultsSelectLastRow").Replace(
                        "ShowPdfWithDefaultApplication", "OpenPdfWithDefaultApplication").Replace(
                        "AddPdfDeleteSelectedPdfWhenAdded", "AddPdfDeleteInputPdfOnOK").Replace(
                        "ListFlaggedDocumentsOnStartup", "ShowFlaggedDocumentsOnStartup")
                    File.WriteAllText(previousConfig, text)
                End If
            End If
        End If
    End Sub

    Private Shared Function GetConfigParentPath() As DirectoryInfo
        Return Directory.GetParent(Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(
                                                         ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath))
    End Function
End Class
