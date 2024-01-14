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
Imports PDFKeeper.Core.ViewModels

Public Class UploadProfileEditorView
    Private ReadOnly presenter As UploadProfileEditorPresenter
    Private ReadOnly viewModel As UploadProfileEditorViewModel

    ''' <summary>
    ''' Initializes a new instance of the UploadProfileEditorView class.
    ''' </summary>
    ''' <param name="uploadProfileName">
    ''' The upload profile name or null when editing a new upload profile.
    ''' </param>
    Public Sub New(uploadProfileName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        presenter = New UploadProfileEditorPresenter(uploadProfileName, New MessageBoxService)
        viewModel = presenter.ViewModel
        UploadProfileEditorViewModelBindingSource.DataSource = presenter.ViewModel
        HelpProvider.HelpNamespace = New HelpFile().FileName
        AddEventHandlers()
    End Sub

    Private Sub AddEventHandlers()
        AddHandler presenter.ApplyPendingChangesRequested,
            AddressOf Presenter_ApplyPendingChangesRequested
        AddHandler presenter.ViewCloseCancelled, AddressOf Presenter_ViewCloseCancelled
        AddHandler viewModel.PropertyChanged, AddressOf ViewModel_PropertyChanged
    End Sub

    Private Sub UploadProfileEditorView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter.GetCollections()
        UploadProfileEditorViewModelBindingSource.ResetBindings(False)
    End Sub

    Private Sub SubjectUserControl_Enter(sender As Object, e As EventArgs) Handles SubjectUserControl.Enter
        presenter.GetSubjects()
    End Sub

    Private Sub SetNameToAuthorSubjectLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles SetNameToAuthorSubjectLinkLabel.LinkClicked
        presenter.SetNameToAuthorAndSubject()
    End Sub

    Private Sub UploadOptionsUserControl_Leave(sender As Object, e As EventArgs) Handles UploadOptionsUserControl.Leave
        Presenter_ApplyPendingChangesRequested(Me, Nothing)
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        presenter.SaveUploadProfile()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        presenter.Cancel()
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Presenter_ApplyPendingChangesRequested(sender As Object, e As EventArgs)
        UploadProfileEditorViewModelBindingSource.EndEdit()
    End Sub

    Private Sub Presenter_ViewCloseCancelled(sender As Object, e As EventArgs)
        NameUserControl.NameTextBox.Select()
    End Sub

    Private Sub ViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName.Equals("TitleTokens", StringComparison.Ordinal) Then
            TitleUserControl.TitleTokens = viewModel.TitleTokens
        ElseIf e.PropertyName.Equals("Authors", StringComparison.Ordinal) Then
            AuthorUserControl.Authors = viewModel.Authors
        ElseIf e.PropertyName.Equals("Subjects", StringComparison.Ordinal) Then
            SubjectUserControl.Subjects = viewModel.Subjects
        ElseIf e.PropertyName.Equals("Categories", StringComparison.Ordinal) Then
            CategoryUserControl.Categories = viewModel.Categories
        ElseIf e.PropertyName.Equals("TaxYears", StringComparison.Ordinal) Then
            TaxYearDropDownListUserControl.TaxYears = viewModel.TaxYears
        End If
    End Sub

    Private Sub UploadProfileEditorView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = presenter.CancelViewClosing
    End Sub
End Class
