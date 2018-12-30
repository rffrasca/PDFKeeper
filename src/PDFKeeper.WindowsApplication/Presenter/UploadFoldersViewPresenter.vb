'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
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
Public Class UploadFoldersViewPresenter
    Private view As IUploadFoldersView

    Public Sub New(view As IUploadFoldersView)
        Me.view = view
    End Sub

    Public Sub UploadFoldersViewLoad()
        FillFolders()
    End Sub

    Public Sub FoldersListBoxSelectedIndexChanged()
        If Not view.Folder Is Nothing Then
            view.EditEnabled = True
            view.DeleteEnabled = True
        Else
            view.EditEnabled = False
            view.DeleteEnabled = False
        End If
    End Sub

    Public Sub AddButtonClick()
        Using uploadFolderConfigDialog As New UploadFolderConfigurationDialog(Nothing)
            uploadFolderConfigDialog.ShowDialog()
        End Using
        FillFolders()
    End Sub

    Public Sub EditButtonClick()
        Using uploadFolderConfigDialog As New UploadFolderConfigurationDialog(view.Folder)
            uploadFolderConfigDialog.ShowDialog()
        End Using
        FillFolders()
    End Sub

    Public Sub DeleteButtonClick()
        Dim result As DialogResult
        Dim displayService As IMessageDisplayService = New MessageDisplayService
        result = displayService.ShowQuestion(My.Resources.DeleteUploadConfigurationPrompt, _
                                             False)
        If result = Windows.Forms.DialogResult.Yes Then
            UploadController.WaitWhileUploadRunning()
            If UploadDirectory.DeleteChildDirectory(view.Folder) Then
                UploadConfigDirectory.DeleteConfig(view.Folder)
                FillFolders()
            Else
                displayService.ShowError(My.Resources.UploadFolderCannotBeDeleted)
            End If
        End If
    End Sub

    Private Sub FillFolders()
        view.EditEnabled = False
        view.DeleteEnabled = False
        Dim configFileNames As String() = _
            UploadConfigDirectory.GetConfigFileNames(True, False)
        For Each configFileName In configFileNames
            UploadDirectory.CreateChildDirectory(configFileName)    ' to ensure folder exists
        Next
        view.Folders = configFileNames
    End Sub
End Class
