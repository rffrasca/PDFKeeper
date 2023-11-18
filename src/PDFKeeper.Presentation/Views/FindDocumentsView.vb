' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2023 Robert F. Frasca
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

Public Class FindDocumentsView
    Private ReadOnly presenter As FindDocumentsPresenter
    Private ReadOnly viewModel As FindDocumentsViewModel
    Private selectedChoice As RadioButton

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        presenter = New FindDocumentsPresenter(New MessageBoxService)
        viewModel = presenter.ViewModel
        FindDocumentsViewModelBindingSource.DataSource = viewModel
        HelpProvider.HelpNamespace = New HelpFile().FileName
        AddEventHandlers()
    End Sub

    Private Sub AddEventHandlers()
        AddHandler presenter.LongRunningOperationStarted,
            AddressOf Presenter_LongRunningOperationStarted
        AddHandler presenter.LongRunningOperationFinished,
            AddressOf Presenter_LongRunningOperationFinished
        AddHandler presenter.ApplyPendingChangesRequested,
            AddressOf Presenter_ApplyPendingChangesRequested
        AddHandler presenter.ViewCloseCancelled,
            AddressOf Presenter_ViewCloseCancelled
        AddHandler viewModel.PropertyChanged, AddressOf ViewModel_PropertyChanged
    End Sub

    Private Sub FindDocumentsView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter.ApplyParamObjectFromApplicationGlobal()
        If FindBySearchTermRadioButton.Checked Then
            selectedChoice = FindBySearchTermRadioButton
        ElseIf FindBySelectionsRadioButton.Checked Then
            selectedChoice = FindBySelectionsRadioButton
        ElseIf FindByDateAddedRadioButton.Checked Then
            selectedChoice = FindByDateAddedRadioButton
        ElseIf FindFlaggedDocumentsRadioButton.Checked Then
            selectedChoice = FindFlaggedDocumentsRadioButton
        ElseIf AllDocumentsRadioButton.Checked Then
            selectedChoice = AllDocumentsRadioButton
        End If
        If selectedChoice Is Nothing Then
            selectedChoice = FindBySearchTermRadioButton
        End If
    End Sub

    Private Sub RadioButton_Click(sender As Object, e As EventArgs) Handles FindBySearchTermRadioButton.Click, FindFlaggedDocumentsRadioButton.Click, FindBySelectionsRadioButton.Click, FindByDateAddedRadioButton.Click, AllDocumentsRadioButton.Click
        Dim radioButton = TryCast(sender, RadioButton)
        If radioButton IsNot Nothing AndAlso Not radioButton.Checked Then
            radioButton.Checked = Not radioButton.Checked
        End If
        If radioButton Is FindByDateAddedRadioButton And FindByDateAddedRadioButton.Checked Then
            If viewModel.DateAdded Is Nothing Then
                viewModel.DateAdded = DateAddedDateTimePicker.Text
            End If
        End If
    End Sub

    Private Sub SearchTermUserControl_Enter(sender As Object, e As EventArgs) Handles SearchTermUserControl.Enter
        presenter.GetSearchTermHistory()
    End Sub

    Private Sub AuthorDropDownListUserControl_Enter(sender As Object, e As EventArgs) Handles AuthorDropDownListUserControl.Enter
        presenter.GetAuthors()
    End Sub

    Private Sub SubjectDropDownListUserControl_Enter(sender As Object, e As EventArgs) Handles SubjectDropDownListUserControl.Enter
        presenter.GetSubjects()
    End Sub

    Private Sub CategoryDropDownListUserControl_Enter(sender As Object, e As EventArgs) Handles CategoryDropDownListUserControl.Enter
        presenter.GetCategories()
    End Sub

    Private Sub TaxYearDropDownListUserControl_Enter(sender As Object, e As EventArgs) Handles TaxYearDropDownListUserControl.Enter
        presenter.GetTaxYears()
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        presenter.FindDocuments()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        selectedChoice.Select()
        presenter.Cancel()
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Presenter_LongRunningOperationStarted(sender As Object, e As EventArgs)
        Cursor = Cursors.WaitCursor
    End Sub

    Private Sub Presenter_LongRunningOperationFinished(sender As Object, e As EventArgs)
        Cursor = Cursors.Default
    End Sub

    Private Sub Presenter_ApplyPendingChangesRequested(sender As Object, e As EventArgs)
        FindDocumentsViewModelBindingSource.EndEdit()
    End Sub

    Private Sub Presenter_ViewCloseCancelled(sender As Object, e As EventArgs)
        SelectNextControl(Me, True, True, True, True)
    End Sub

    Private Sub ViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName.Equals("Authors", StringComparison.Ordinal) Then
            AuthorDropDownListUserControl.Authors = viewModel.Authors
        ElseIf e.PropertyName.Equals("Subjects", StringComparison.Ordinal) Then
            SubjectDropDownListUserControl.Subjects = viewModel.Subjects
        ElseIf e.PropertyName.Equals("Categories", StringComparison.Ordinal) Then
            CategoryDropDownListUserControl.Categories = viewModel.Categories
        ElseIf e.PropertyName.Equals("TaxYears", StringComparison.Ordinal) Then
            TaxYearDropDownListUserControl.TaxYears = viewModel.TaxYears
        End If
    End Sub

    Private Sub FindDocumentsView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = presenter.CancelViewClosing
    End Sub
End Class
