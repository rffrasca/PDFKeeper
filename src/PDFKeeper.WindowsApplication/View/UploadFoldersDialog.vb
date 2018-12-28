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
Imports System.Windows.Forms

Public Class UploadFoldersDialog
    Implements IUploadFoldersView
    Private presenter As UploadFoldersViewPresenter

    Public Sub New()
        InitializeComponent()
        presenter = New UploadFoldersViewPresenter(Me)
        HelpProvider.HelpNamespace = HelpProviderHelper.HelpFile
        FoldersListBox.Select()
        presenter.FillFolders()
    End Sub

    Private Sub FoldersListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FoldersListBox.SelectedIndexChanged
        presenter.FolderSelectionChanged()
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        presenter.AddFolder()
    End Sub

    Private Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
        presenter.EditFolder()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        presenter.DeleteFolder()
    End Sub

#Region "IUploadFoldersManagerView members get/set by UploadFoldersViewPresenter"
    Public Property Folders As Object Implements IUploadFoldersView.Folders
        Get
            Return FoldersListBox.Items
        End Get
        Set(value As Object)
            FoldersListBox.Items.Clear()
            FoldersListBox.Items.AddRange(value)
        End Set
    End Property

    Public ReadOnly Property Folder As String Implements IUploadFoldersView.Folder
        Get
            Return FoldersListBox.SelectedItem
        End Get
    End Property

    Public Property EditEnabled As Boolean Implements IUploadFoldersView.EditEnabled
        Get
            Return EditButton.Enabled
        End Get
        Set(value As Boolean)
            EditButton.Enabled = value
        End Set
    End Property

    Public Property DeleteEnabled As Boolean Implements IUploadFoldersView.DeleteEnabled
        Get
            Return DeleteButton.Enabled
        End Get
        Set(value As Boolean)
            DeleteButton.Enabled = value
        End Set
    End Property
#End Region

End Class
