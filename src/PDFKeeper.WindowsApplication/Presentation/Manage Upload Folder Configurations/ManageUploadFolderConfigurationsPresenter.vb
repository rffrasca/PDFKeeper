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
Public Class ManageUploadFolderConfigurationsPresenter
    Private view As IManageUploadFolderConfigurationsView
    Private model As New UploadFolderConfiguration
    Private helper As UploadFolderConfigurationHelper = Nothing
    Private messageDisplay As IMessageDisplay = New MessageDisplay
    Private questionDisplay As IQuestionDisplay = New QuestionDisplay
    Private origUploadFolderConfiguration As String = Nothing

    Public Sub New(view As IManageUploadFolderConfigurationsView)
        Me.view = view
    End Sub

    Public Sub ResetView()
        view.UploadFolderConfigurationsElementsEnabled = True
        view.UploadFolderConfiguration = Nothing
        view.EditEnabled = False
        view.DeleteEnabled = False
        view.FolderName = Nothing
        view.Title = Nothing
        view.AuthorPaired = Nothing
        view.SubjectPaired = Nothing
        view.Keywords = Nothing
        view.Category = Nothing
        view.FlagDocument = False
        view.UploadFolderConfigurationElementsEnabled = False
    End Sub

    Public Sub GetConfigurations()
        view.UploadFolderConfigurations = _
            UploadFolderConfigurationUtil.GetAllConfigFileNames(True, False)
    End Sub

    Public Sub ConfigurationSelected()
        If view.UploadFolderConfiguration Is Nothing Then
            view.EditEnabled = False
            view.DeleteEnabled = False
            origUploadFolderConfiguration = Nothing
        Else
            helper = New UploadFolderConfigurationHelper( _
                view.UploadFolderConfiguration)
            origUploadFolderConfiguration = view.UploadFolderConfiguration
            view.EditEnabled = True
            view.DeleteEnabled = True
        End If
    End Sub

    Public Sub NewConfiguration()
        view.UploadFolderConfigurationsElementsEnabled = False
        view.UploadFolderConfiguration = Nothing
        view.UploadFolderConfigurationElementsEnabled = True
        view.SaveEnabled = False
    End Sub

    Public Sub EditConfiguration()
        view.UploadFolderConfigurationsElementsEnabled = False
        model = helper.Read
        UpdateView()
        view.UploadFolderConfigurationElementsEnabled = True
    End Sub

    Public Sub DeleteConfiguration()
        If questionDisplay.Show( _
            My.Resources.DeleteUploadFolderConfigurationPrompt, _
                                False) = DialogResult.Yes Then
            view.OnLongRunningOperationStarted()
            Dim result As Boolean = helper.Delete
            view.OnLongRunningOperationFinished()
            If result Then
                ResetView()
            Else
                messageDisplay.Show(My.Resources.UploadFolderCannotBeDeleted, _
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
            If view.FolderName.Length > 0 And _
            view.Title.Length > 0 And _
            view.AuthorPaired.Length > 0 And _
            view.SubjectPaired.Length > 0 Then
                view.SaveEnabled = True
            Else
                view.SaveEnabled = False
            End If
        End If
    End Sub

    Public Sub SaveConfiguration()
        UpdateModel()
        view.OnLongRunningOperationStarted()
        helper = New UploadFolderConfigurationHelper(view.FolderName)
        If origUploadFolderConfiguration Is Nothing Then
            helper.Save(model, Nothing)
        Else
            helper.Save(model, origUploadFolderConfiguration)
        End If
        view.OnLongRunningOperationFinished()
        ResetView()
    End Sub

    Public Sub Discard()
        If questionDisplay.Show(My.Resources.GenericDiscardChangesPrompt, _
                                False) = DialogResult.Yes Then
            ResetView()
        End If
    End Sub

    Public Function ViewClosingPrompt() As Boolean
        Return questionDisplay.ShowFormClosingPrompt
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
