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
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.Infrastructure
Imports PDFKeeper.Services

Public Class UploadProfilesPresenter
    Private ReadOnly view As IUploadProfilesView
    Private ReadOnly authorListSvc As IAuthorListService
    Private ReadOnly subjectListSvc As ISubjectListService
    Private ReadOnly categoryListSvc As ICategoryListService
    Private ReadOnly taxYearListSvc As ITaxYearListService
    Private ReadOnly uploadProfileSvc As IUploadProfileService
    Private ReadOnly commonDialogs As New CommonDialogs
    Private viewInstance As Form
    Private previousProfile As String

    Public Sub New(ByVal view As IUploadProfilesView, ByVal authorListSvc As IAuthorListService,
                   ByVal subjectListSvc As ISubjectListService, ByVal categoryListSvc As ICategoryListService,
                   ByVal taxYearListSvc As ITaxYearListService, ByVal uploadProfileSvc As IUploadProfileService)
        Me.view = view
        Me.authorListSvc = authorListSvc
        Me.subjectListSvc = subjectListSvc
        Me.categoryListSvc = categoryListSvc
        Me.taxYearListSvc = taxYearListSvc
        Me.uploadProfileSvc = uploadProfileSvc
    End Sub

    Friend Sub UploadProfilesDialog_Load(sender As Object, e As EventArgs)
        viewInstance = CType(sender, Form)
        Dim tokens = New List(Of String) From {
            My.Resources.DateTimeToken,
            My.Resources.DateToken,
            My.Resources.FileNameToken
        }
        view.TitleItems = tokens.ToArray
        ResetView()
    End Sub

    Friend Sub ProfileComboBox_DropDown(sender As Object, e As EventArgs)
        view.ProfileItems = uploadProfileSvc.ListProfileNames
    End Sub

    Friend Sub ProfileComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        With view
            previousProfile = .Profile
            If .Profile Is Nothing Then
                .EditEnabled = False
                .DeleteEnabled = False
            Else
                .EditEnabled = True
                .DeleteEnabled = True
            End If
        End With
    End Sub

    Friend Sub NewButton_Click(sender As Object, e As EventArgs)
        With view
            .ProfilesGroupEnabled = False
            .Profile = Nothing
            .ProfileGroupEnabled = True
            .SaveEnabled = False
            .SetFocusOnProfileNameTextBox()
        End With
    End Sub

    Friend Sub EditButton_Click(sender As Object, e As EventArgs)
        With view
            Dim model = uploadProfileSvc.ReadProfile(.Profile)
            .ProfileName = .Profile
            .Title = model.Title
            .Author = model.Author
            .Subject = model.Subject
            .Keywords = model.Keywords
            .Category = model.Category
            .TaxYear = model.TaxYear
            .FlagDocument = model.FlagDocument
            .OcrPdfTextAndImageDataPages = model.OcrPdfTextAndImageDataPages
            .ProfilesGroupEnabled = False
            .ProfileGroupEnabled = True
            .SetFocusOnProfileNameTextBox()
        End With
    End Sub

    Friend Sub DeleteButton_Click(sender As Object, e As EventArgs)
        If commonDialogs.ShowQuestionMessageBox(My.Resources.DeleteUploadProfile, False) = DialogResult.Yes Then
            uploadProfileSvc.DeleteProfile(view.Profile)
            ResetView()
        End If
    End Sub

    Friend Sub CommonComboBox_Enter(sender As Object, e As EventArgs)
        Dim control = CType(sender, Control)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            Dim currentItem = control.Text
            If control.Name.StartsWith("Author") Then
                view.AuthorItems = authorListSvc.ListAuthors
            ElseIf control.Name.StartsWith("Subject") Then
                view.SubjectItems = subjectListSvc.ListSubjects(view.Author)
            ElseIf control.Name.StartsWith("Category") Then
                view.CategoryItems = categoryListSvc.ListCategories
            ElseIf control.Name.StartsWith("TaxYear") Then
                view.TaxYearItems = taxYearListSvc.ListRangeOfTaxYears
            End If
            control.Text = currentItem
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub Common_TextChanged(sender As Object, e As EventArgs)
        With view
            If .Author.Trim.Length > 0 And .Subject.Trim.Length > 0 Then
                .SetProfileNameToAuthorSubjectLinkEnabled = True
            Else
                .SetProfileNameToAuthorSubjectLinkEnabled = False
            End If
            If .ProfileName.Trim.Length > 0 And .Title.Trim.Length > 0 And .Author.Trim.Length > 0 And
                .Subject.Trim.Length > 0 Then
                .SaveEnabled = True
            Else
                .SaveEnabled = False
            End If
        End With
    End Sub

    Friend Sub SetProfileNameToAuthorSubjectLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        With view
            .ProfileName = String.Concat(.Author.Trim, " ", .Subject.Trim)
        End With
    End Sub

    Friend Sub SaveButton_Click(sender As Object, e As EventArgs)
        Dim model = New UploadProfileModel
        With view
            model.Title = .Title
            model.Author = .Author
            model.Subject = .Subject
            model.Keywords = .Keywords
            model.Category = .Category
            model.TaxYear = .TaxYear
            model.FlagDocument = .FlagDocument
            model.OcrPdfTextAndImageDataPages = .OcrPdfTextAndImageDataPages
            Try
                If previousProfile Is Nothing Then
                    uploadProfileSvc.CreateProfile(.ProfileName, model)
                Else
                    uploadProfileSvc.UpdateProfile(.ProfileName, previousProfile, model)
                End If
                ResetView()
            Catch ex As IOException
                .SetErrorProviderMessage(My.Resources.UploadProfileExists)
                .SetFocusOnProfileNameTextBox()
            Catch ex As FormatException
                .SetErrorProviderMessage(My.Resources.ProfileNameInvalid)
                .SetFocusOnProfileNameTextBox()
            End Try
        End With
    End Sub

    Friend Sub DiscardButton_Click(sender As Object, e As EventArgs)
        If commonDialogs.ShowQuestionMessageBox(My.Resources.DiscardChanges, False) = DialogResult.Yes Then
            ResetView()
        End If
    End Sub

    Friend Sub UploadProfilesDialog_FormClosing(sender As Object, e As FormClosingEventArgs)
        If view.DiscardEnabled Then
            If commonDialogs.ShowQuestionMessageBox(My.Resources.DiscardChanges, False) = DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub ResetView()
        With view
            .ProfilesGroupEnabled = True
            .Profile = Nothing
            .EditEnabled = False
            .DeleteEnabled = False
            .ProfileGroupEnabled = False
            .ProfileName = Nothing
            .Title = Nothing
            .Author = Nothing
            .Subject = Nothing
            .SetProfileNameToAuthorSubjectLinkEnabled = False
            .Keywords = Nothing
            .Category = Nothing
            .TaxYear = Nothing
            .FlagDocument = False
            .OcrPdfTextAndImageDataPages = False
            .SetErrorProviderMessage(Nothing)
        End With
    End Sub
End Class
