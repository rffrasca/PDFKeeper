'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
'* Copyright (C) 2009-2019 Robert F. Frasca
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
''' Upgrades the user settings when the UpgradeSettings value in the user
''' settings file is True and opens the Database Schema Upgrade for Oracle
''' Database help topic.
''' </summary>
''' <remarks></remarks>
Public Class UserSettingsUpgradeCommand
    Implements ICommand

    Public Sub Execute() Implements ICommand.Execute
        ' UpgradeSettings is an application user setting.
        If My.Settings.UpgradeSettings Then
            My.Settings.Upgrade()
            My.Settings.UpgradeSettings = False
            My.Settings.Save()
            If Directory.Exists(Path.Combine( _
                                Environment.GetFolderPath( _
                                    Environment.SpecialFolder.ApplicationData), _
                                My.Application.Info.CompanyName, _
                                Application.ProductName)) Then
                Dim messageDisplay As IMessageDisplay = New MessageDisplay
                messageDisplay.Show(My.Resources.UpgradeMessage, False)
            End If
        End If
    End Sub
End Class
