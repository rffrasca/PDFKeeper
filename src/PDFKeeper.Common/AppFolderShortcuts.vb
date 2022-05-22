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
Imports System.IO

Public NotInheritable Class AppFolderShortcuts
    Private Shared ReadOnly shortcutName = String.Concat(
        My.Application.Info.ProductName, " ", My.Resources.Upload, ".lnk")
    Private Shared ReadOnly documentsTargetPath = Path.Combine(UserProfileFolders.Documents, shortcutName)
    Private Shared ReadOnly downloadsTargetPath = Path.Combine(UserProfileFolders.Downloads, shortcutName)

    ''' <summary>
    ''' Creates the PDFKeeper application folder shortcuts.
    ''' </summary>
    ''' <param name="targetFolderPath">Shortcut target folder path</param>
    Public Shared Sub Create(ByVal targetFolderPath As String)
        Dim dir = New DirectoryInfo(targetFolderPath)
        CreateShortcut(dir, documentsTargetPath)
        CreateShortcut(dir, downloadsTargetPath)
    End Sub

    ''' <summary>
    ''' Deletes the PDFKeeper application folder shortcuts.
    ''' </summary>
    Public Shared Sub Delete()
        Try
            IO.File.Delete(documentsTargetPath)
            IO.File.Delete(downloadsTargetPath)
        Catch ex As IOException
        End Try
    End Sub
End Class
