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
Imports PDFKeeper.Services

Public Class LoginForm
    Implements ILoginView
    Private ReadOnly presenter As LoginPresenter
    Private ReadOnly help As New HelpProvider

    Public Sub New()
        InitializeComponent()
        presenter = New LoginPresenter(Me, New LoginService)
        HelpProvider.HelpNamespace = help.HelpFileName
        AddHandlers()
    End Sub

    Public ReadOnly Property UserName As String Implements ILoginView.UserName
        Get
            Return UsernameTextBox.Text
        End Get
    End Property

    Public ReadOnly Property Password As SecureString Implements ILoginView.Password
        Get
            Return PasswordSecureTextBox.SecureText
        End Get
    End Property

    Public ReadOnly Property DataSource As String Implements ILoginView.DataSource
        Get
            Return DatasourceTextBox.Text
        End Get
    End Property

    Public Sub ResetView() Implements ILoginView.ResetView
        PasswordSecureTextBox.SecureText.Dispose()
        PasswordSecureTextBox.Text = Nothing
        PasswordSecureTextBox.ConstructSecureString()
        Focus()
        UsernameTextBox.Select()
    End Sub

    Private Sub AddHandlers()
        AddHandler MyBase.Load, AddressOf presenter.LoginForm_Load
        AddHandler OK.Click, AddressOf presenter.OK_Click
        AddHandler Cancel.Click, AddressOf presenter.Cancel_Click
    End Sub
End Class
