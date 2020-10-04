'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Class ManageUploadFolderConfigurationsPresenter
    Private ReadOnly view As IManageUploadFolderConfigurationsView
    Private model As New UploadFolderConfiguration
    Private helper As UploadFolderConfigurationHelper
    Private ReadOnly message As IMessageDisplayService =
        New MessageDisplayService
    Private ReadOnly question As IQuestionDisplayService =
        New QuestionDisplayService
    Private origUploadFolderConfiguration As String

    Public Sub New(view As IManageUploadFolderConfigurationsView)
        Me.view = view
    End Sub

    Public Sub ResetView()
        With view
            .UploadFolderConfigurationsElementsEnabled = True
            .UploadFolderConfiguration = Nothing
            .EditEnabled = False
            .DeleteEnabled = False
            .FolderName = Nothing
            .Title = Nothing
            .AuthorPaired = Nothing
            .SubjectPaired = Nothing
            .Keywords = Nothing
            .Category = Nothing
            .FlagDocument = False
            .UploadFolderConfigurationElementsEnabled = False
        End With
    End Sub

    Public Sub GetConfigurations()
        view.UploadFolderConfigurations =
            UploadFolderConfigurationUtil.GetAllConfigFileNames(True, False)
    End Sub

    Public Sub ConfigurationSelected()
        If view.UploadFolderConfiguration Is Nothing Then
            view.EditEnabled = False
            view.DeleteEnabled = False
            origUploadFolderConfiguration = Nothing
        Else
            helper = New UploadFolderConfigurationHelper(
                view.UploadFolderConfiguration)
            origUploadFolderConfiguration = view.UploadFolderConfiguration
            view.EditEnabled = True
            view.DeleteEnabled = True
        End If
    End Sub

    Public Sub NewConfiguration()
        With view
            .UploadFolderConfigurationsElementsEnabled = False
            .UploadFolderConfiguration = Nothing
            .UploadFolderConfigurationElementsEnabled = True
            .SaveEnabled = False
        End With
    End Sub

    Public Sub EditConfiguration()
        view.UploadFolderConfigurationsElementsEnabled = False
        model = helper.Read
        UpdateView()
        view.UploadFolderConfigurationElementsEnabled = True
    End Sub

    Public Sub DeleteConfiguration()
        If question.Show(
            My.Resources.DeleteUploadFolderConfigurationPrompt,
                                False) = DialogResult.Yes Then
            view.SetCursor(True)
            Dim result As Boolean = helper.Delete
            view.SetCursor(False)
            If result Then
                ResetView()
            Else
                message.Show(My.Resources.UploadFolderCannotBeDeleted,
                                    True)
            End If
        End If
    End Sub

    Public Sub GetTokens()
        view.Titles = UploadFolderConfigurationTokens.ToArray
    End Sub

    Public Sub RequiredTextElementChanged()
        If view.FolderName.ContainsInvalidFileNameChars Then
            view.FolderNameError = My.Resources.FolderNameContainsInvalidChars
            view.SaveEnabled = False
        Else
            view.FolderNameError = Nothing
            If view.FolderName.Length > 0 And
            view.Title.Length > 0 And
            view.AuthorPaired.Length > 0 And
            view.SubjectPaired.Length > 0 Then
                view.SaveEnabled = True
            Else
                view.SaveEnabled = False
            End If
        End If
    End Sub

    Public Sub SaveConfiguration()
        UpdateModel()
        view.SetCursor(True)
        helper = New UploadFolderConfigurationHelper(view.FolderName)
        If origUploadFolderConfiguration Is Nothing Then
            helper.Save(model, Nothing)
        Else
            helper.Save(model, origUploadFolderConfiguration)
        End If
        view.SetCursor(False)
        ResetView()
    End Sub

    Public Sub Discard()
        If question.Show(My.Resources.GenericDiscardChangesPrompt,
                                False) = DialogResult.Yes Then
            ResetView()
        End If
    End Sub

    Public Function ViewClosingPrompt() As Boolean
        Return question.ShowFormClosingPrompt
    End Function

    Private Sub UpdateView()
        With view
            .FolderName = view.UploadFolderConfiguration
            .Title = model.TitlePrefill
            .AuthorPaired = model.AuthorPrefill
            .SubjectPaired = model.SubjectPrefill
            .Keywords = model.KeywordsPrefill
            .Category = model.CategoryPrefill
            .FlagDocument = model.FlagDocument
        End With
    End Sub

    Private Sub UpdateModel()
        With model
            .TitlePrefill = view.Title.TrimEnd
            .AuthorPrefill = view.AuthorPaired.TrimEnd
            .SubjectPrefill = view.SubjectPaired.TrimEnd
            .KeywordsPrefill = view.Keywords.TrimEnd
            .CategoryPrefill = view.Category.TrimEnd
            .FlagDocument = view.FlagDocument
        End With
    End Sub
End Class
