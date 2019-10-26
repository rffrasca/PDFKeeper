'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Public Class UploadFoldersCleanupCommand
    Implements ICommand

    ''' <summary>
    ''' Deletes all empty non configured folders from the Upload folder and any
    ''' empty sub-folders under each configured upload folder.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Execute() Implements ICommand.Execute
        Dim subFolders As String() = _
            Directory.GetDirectories(UserProfile.UploadPath)
        For Each subFolder In subFolders
            Dim dirInfo As New DirectoryInfo(subFolder)
            If UploadFolderConfigurationUtil.IsFolderConfigured( _
                dirInfo.Name) Then
                ' When the folder is a configured folder then only
                ' delete empty sub-folders under the configured folder.
                Dim subFoldersL2 As String() = _
                    Directory.GetDirectories(subFolder)
                For Each subFolderL2 In subFoldersL2
                    If Directory.GetFiles(subFolderL2).Count = 0 Then
                        Directory.Delete(subFolderL2, True)
                    End If
                Next
            Else
                If Directory.GetFiles(subFolder).Count = 0 Then
                    Directory.Delete(subFolder, True)
                End If
            End If
        Next
    End Sub
End Class
