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

Imports PDFKeeper.Core.Presenters

Public Class UploadProfilesView
    Private ReadOnly presenter As UploadProfilesPresenter

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        presenter = New UploadProfilesPresenter(New MessageBoxService,
                                                New UploadProfileEditorDialogService(Of String))
        Dim viewModel = presenter.ViewModel
        UploadProfilesViewModelBindingSource.DataSource = viewModel
        HelpProvider.HelpNamespace = New HelpFile().FileName
        UploadProfilesFileSystemWatcher.Path = viewModel.UploadProfilesDirectoryPath
    End Sub

    Private Sub UploadProfileNamesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UploadProfileNamesListBox.SelectedIndexChanged
        UploadProfileNamesListBox.Select()
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        presenter.AddUploadProfile()
    End Sub

    Private Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
        presenter.EditUploadProfile()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        presenter.DeleteUploadProfile()
    End Sub

    Private Sub UploadProfilesFileSystemWatcher_Created(sender As Object, e As FileSystemEventArgs) Handles UploadProfilesFileSystemWatcher.Created
        presenter.GetUploadProfileNames()
    End Sub

    Private Sub UploadProfilesFileSystemWatcher_Deleted(sender As Object, e As FileSystemEventArgs) Handles UploadProfilesFileSystemWatcher.Deleted
        presenter.GetUploadProfileNames()
    End Sub

    Private Sub UploadProfilesFileSystemWatcher_Renamed(sender As Object, e As RenamedEventArgs) Handles UploadProfilesFileSystemWatcher.Renamed
        presenter.GetUploadProfileNames()
    End Sub
End Class
