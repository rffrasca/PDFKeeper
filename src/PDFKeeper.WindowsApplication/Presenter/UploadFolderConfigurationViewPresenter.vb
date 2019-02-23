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
Public Class UploadFolderConfigurationViewPresenter
    Private view As IUploadFolderConfigurationView

    Public Sub New(view As IUploadFolderConfigurationView)
        Me.view = view
    End Sub

    Public Sub UploadFolderConfigurationViewLoad()
        Dim config As New UploadFolderConfiguration
        UploadConfigDirectory.ReadConfig(config, view.EditFolderName)
        view.FolderName = view.EditFolderName
        view.Title = config.TitlePrefill
        view.Author = config.AuthorPrefill
        view.Subject = config.SubjectPrefill
        view.Keywords = config.KeywordsPrefill
        view.FlagDocument = config.FlagDocument
    End Sub

    Public Sub TitleComboBoxEnter()
        Dim tokens As New GenericList(Of String)
        tokens.Add(UploadFolderConfigurationTokens.DateToken)
        tokens.Add(UploadFolderConfigurationTokens.DateTimeToken)
        tokens.Add(UploadFolderConfigurationTokens.FileNameToken)
        view.Titles = tokens.ToArray(False)
    End Sub

    Public Sub AuthorComboBoxEnter()
        Dim currentAuthor As String = view.Author
        Dim docsDao As IDocsDao = New DocsDao
        view.Authors = docsDao.GetAllAuthors
        view.Author = currentAuthor
    End Sub

    Public Sub SubjectComboBoxEnter()
        Dim currentSubject As String = view.Subject
        Dim docsDao As IDocsDao = New DocsDao
        view.Subjects = docsDao.GetAllSubjectsByAuthor(view.Author)
        view.Subject = currentSubject
    End Sub

    Public Sub TextBoxTextChanged()
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

    Public Sub OkButtonClick()
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
        config.FlagDocument = view.FlagDocument
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
