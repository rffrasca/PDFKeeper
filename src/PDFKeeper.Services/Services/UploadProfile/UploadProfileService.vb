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
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.Infrastructure

Public Class UploadProfileService
    Implements IUploadProfileService
    Private ReadOnly repository As IXmlRepository(Of UploadProfileModel)

    Public Sub New(repository As IXmlRepository(Of UploadProfileModel))
        Me.repository = repository
    End Sub

    Public Function ListProfileNames() As Object Implements IUploadProfileService.ListProfileNames
        Return repository.ListItems.ToArray
    End Function

    Public Function ProfileExists(name As String) As Boolean Implements IUploadProfileService.ProfileExists
        Return repository.ItemExists(name)
    End Function

    Public Sub CreateProfile(name As String, model As UploadProfileModel) Implements IUploadProfileService.CreateProfile
        If name Is Nothing Then
            Throw New ArgumentNullException(NameOf(name))
        End If
        If model Is Nothing Then
            Throw New ArgumentNullException(NameOf(model))
        End If
        name = name.Trim
        model = TrimModelProperties(model)
        If repository.ItemExists(name) Then
            Throw New IOException
        End If
        If ContainInvalidFileNameChars(name) Then
            Throw New FormatException
        End If
        repository.CreateItem(Of UploadProfileModel)(name, model)
    End Sub

    Public Function ReadProfile(name As String) As UploadProfileModel Implements IUploadProfileService.ReadProfile
        Return repository.ReadItem(name)
    End Function

    Public Sub UpdateProfile(name As String, previousName As String, model As UploadProfileModel) Implements IUploadProfileService.UpdateProfile
        If name Is Nothing Then
            Throw New ArgumentNullException(NameOf(name))
        End If
        If model Is Nothing Then
            Throw New ArgumentNullException(NameOf(model))
        End If
        name = name.Trim
        model = TrimModelProperties(model)
        If ContainInvalidFileNameChars(name) Then
            Throw New FormatException
        End If
        repository.UpdateItem(Of UploadProfileModel)(name, previousName, model)
    End Sub

    Public Sub DeleteProfile(name As String) Implements IUploadProfileService.DeleteProfile
        repository.DeleteItem(name)
    End Sub

    Private Function TrimModelProperties(ByVal model As UploadProfileModel) As UploadProfileModel
        With model
            .Title = .Title.Trim
            .Author = .Author.Trim
            .Subject = .Subject.Trim
            .Keywords = .Keywords.Trim
            .Category = .Category.Trim
        End With
        Return model
    End Function

    Private Shared Function ContainInvalidFileNameChars(ByVal value As String) As Boolean
        Return value.ContainInvalidFileNameChars
    End Function

    Public Sub CreateMissingUploadFolders() Implements IUploadProfileService.CreateMissingUploadFolders
        For Each profile In repository.ListItems
            Directory.CreateDirectory(Path.Combine(AppFolders.GetPath(AppFolders.AppFolder.Upload), profile))
        Next
    End Sub

    Public Sub DeleteDormantUploadFolders() Implements IUploadProfileService.DeleteDormantUploadFolders
        For Each folder In Directory.GetDirectories(AppFolders.GetPath(AppFolders.AppFolder.Upload))
            If repository.ItemExists(folder) Then
                For Each folderL2 In Directory.GetDirectories(folder)
                    If Directory.GetFiles(folderL2).Any = False Then
                        Directory.Delete(folderL2, True)
                    End If
                Next
            Else
                If Directory.GetFiles(folder).Any = False Then
                    Directory.Delete(folder, True)
                End If
            End If
        Next
    End Sub
End Class
