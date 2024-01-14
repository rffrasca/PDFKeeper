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

Public Class LoginView
    Private ReadOnly presenter As LoginPresenter
    Private ReadOnly viewModel As LoginViewModel

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        presenter = New LoginPresenter(New MessageBoxService)
        viewModel = presenter.ViewModel
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
        AddHandler presenter.ViewCloseRequested, AddressOf Presenter_ViewCloseRequested
        AddHandler presenter.ViewResetRequested, AddressOf Presenter_ViewResetRequested
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        presenter.Login()
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
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
        With viewModel
            .UserName = My.Settings.Username
            .Password = PasswordTextBox.SecureText
            .DataSource = My.Settings.Datasource
            .DbManagementSystem = My.Settings.DbManagementSystem
        End With
    End Sub

    Private Sub Presenter_ViewCloseRequested(sender As Object, e As EventArgs)
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Presenter_ViewResetRequested(sender As Object, e As EventArgs)
        PasswordTextBox.SecureText.Dispose()
        PasswordTextBox.Text = Nothing
        PasswordTextBox.ConstructSecureString()
        UsernameTextBox.Select()
    End Sub
End Class
