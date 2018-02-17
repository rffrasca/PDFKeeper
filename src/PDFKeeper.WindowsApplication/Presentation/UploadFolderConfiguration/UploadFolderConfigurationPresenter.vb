'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2018 Robert F. Frasca
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
Public Class UploadFolderConfigurationPresenter
    Private view As IUploadFolderConfigurationView

    Public Sub New(view As IUploadFolderConfigurationView)
        Me.view = view
    End Sub

    Public Sub ReadFolderConfiguration()
        Dim config As New UploadFolderConfiguration
        UploadConfigDirectory.ReadConfig(config, view.EditFolderName)
        view.FolderName = view.EditFolderName
        view.Title = config.TitlePrefill
        view.Author = config.AuthorPrefill
        view.Subject = config.SubjectPrefill
        view.Keywords = config.KeywordsPrefill
    End Sub

    Public Sub TextChanged()
        TrimText()
        If view.FolderName.ContainsInvalidFileNameChars Then
            view.FolderNameErrorProviderMessage = _
                My.Resources.FolderNameContainsInvalidChars
            view.OkEnabled = False
        Else
            If view.FolderName.Length > 0 And _
                view.Title.Length > 0 And _
                view.Author.Length > 0 And _
                view.Subject.Length > 0 Then
                view.OkEnabled = True
            Else
                view.OkEnabled = False
            End If
            view.FolderNameErrorProviderMessage = Nothing
        End If
    End Sub

    Public Sub GetTitleTokens()
        Dim tokens As New GenericList(Of String)
        tokens.Add(UploadFolderConfigurationTokens.DateToken)
        tokens.Add(UploadFolderConfigurationTokens.DateTimeToken)
        tokens.Add(UploadFolderConfigurationTokens.FileNameToken)
        view.Titles = tokens.ToArray(False)
    End Sub

    Public Sub GetAuthors()
        SharedPresenterQueries.GetAuthors(view.Author, view.Authors)
    End Sub

    Public Sub GetSubjectsByAuthor()
        SharedPresenterQueries.GetSubjectsByAuthor(view.Author, _
                                                   view.Subject, _
                                                   view.Subjects)
    End Sub

    Public Sub SaveFolderConfiguration()
        UploadController.WaitWhileUploadRunning()
        If Not view.EditFolderName Is Nothing Then
            UploadConfigDirectory.DeleteConfig(view.EditFolderName)
            If Not view.EditFolderName = view.FolderName Then
                UploadDirectory.RenameChildDirectory(view.EditFolderName, _
                                                     view.FolderName)
            End If
        End If
        Dim config As New UploadFolderConfiguration
        config.TitlePrefill = view.Title
        config.AuthorPrefill = view.Author
        config.SubjectPrefill = view.Subject
        config.KeywordsPrefill = view.Keywords
        UploadConfigDirectory.WriteConfig(config, view.FolderName)
        UploadDirectory.CreateChildDirectory(view.FolderName)
    End Sub

    Private Sub TrimText()
        view.FolderName = view.FolderName.TrimStart
        view.Title = view.Title.TrimStart
        view.Author = view.Author.TrimStart
        view.Subject = view.Subject.TrimStart
        view.Keywords = view.Keywords.TrimStart
    End Sub
End Class
