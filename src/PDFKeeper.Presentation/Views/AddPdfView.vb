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
Imports PDFKeeper.PDFViewer.Services

Public Class AddPdfView
    Private ReadOnly presenter As AddPdfPresenter
    Private ReadOnly viewModel As AddPdfViewModel

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        presenter = New AddPdfPresenter(New OpenFileDialogService,
                                        New PdfOwnerPasswordDialogService, New RestrictedPdfViewerService,
                                        New MessageBoxService)
        viewModel = presenter.ViewModel
        AddPdfViewModelBindingSource.DataSource = presenter.ViewModel
        HelpProvider.HelpNamespace = New HelpFile().FileName
        AddEventHandlers()
    End Sub

    Private Sub AddEventHandlers()
        AddHandler presenter.ApplyPendingChangesRequested,
            AddressOf Presenter_ApplyPendingChangesRequested
        AddHandler presenter.ViewCloseCancelled, AddressOf Presenter_ViewCloseCancelled
        AddHandler presenter.ViewCloseRequested, AddressOf Presenter_ViewCloseRequested
        AddHandler viewModel.PropertyChanged, AddressOf ViewModel_PropertyChanged
    End Sub

    Private Sub AddPdfView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter.SelectPdf()
        presenter.GetCollections()
        AddPdfViewModelBindingSource.ResetBindings(False)
    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click
        presenter.ViewPdf()
    End Sub

    Private Sub SetTitleToPdfFileNameLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles SetTitleToPdfFileNameLinkLabel.LinkClicked
        presenter.SetTitleToPdfFileName()
        TitleTextBox.Select()
    End Sub

    Private Sub SubjectUserControl_Enter(sender As Object, e As EventArgs) Handles SubjectUserControl.Enter
        presenter.GetSubjects()
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        presenter.AddPdf(DeleteSelectedPdfWhenAddedCheckBox.Checked)
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        presenter.Cancel()
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Presenter_ApplyPendingChangesRequested(sender As Object, e As EventArgs)
        AddPdfViewModelBindingSource.EndEdit()
    End Sub

    Private Sub Presenter_ViewCloseCancelled(sender As Object, e As EventArgs)
        TitleTextBox.Select()
    End Sub

    Private Sub Presenter_ViewCloseRequested(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub ViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName.Equals("Authors", StringComparison.Ordinal) Then
            AuthorUserControl.Authors = viewModel.Authors
        ElseIf e.PropertyName.Equals("Subjects", StringComparison.Ordinal) Then
            SubjectUserControl.Subjects = viewModel.Subjects
        ElseIf e.PropertyName.Equals("Categories", StringComparison.Ordinal) Then
            CategoryUserControl.Categories = viewModel.Categories
        ElseIf e.PropertyName.Equals("TaxYears", StringComparison.Ordinal) Then
            TaxYearDropDownListUserControl.TaxYears = viewModel.TaxYears
        End If
    End Sub

    Private Sub AddPdfView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = presenter.CancelViewClosing
    End Sub
End Class
