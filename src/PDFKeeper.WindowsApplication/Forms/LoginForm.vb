'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Public Class LoginForm
    Implements ILoginView
    Private ReadOnly presenter As LoginPresenter
    Private ReadOnly help As IHelpDisplayService = New HelpDisplayService

    Public Sub New()
        InitializeComponent()
        presenter = New LoginPresenter(Me)
        HelpProvider.HelpNamespace = help.Name
    End Sub

#Region "Interface Members"
    Public ReadOnly Property UserName As String Implements ILoginView.UserName
        Get
            Return UsernameTextBox.Text.Trim
        End Get
    End Property

    Public ReadOnly Property Password As SecureString Implements ILoginView.Password
        Get
            Return PasswordSecureTextBox.SecureText
        End Get
    End Property

    Public ReadOnly Property DataSource As String Implements ILoginView.DataSource
        Get
            Return DatasourceTextBox.Text.Trim
        End Get
    End Property

    Public ReadOnly Property DatabaseManagementSystem As String Implements ILoginView.DatabaseManagementSystem
        Get
            Return My.Settings.DbManagementSystem
        End Get
    End Property

    Public Sub SetCursor(wait As Boolean) Implements ILoginView.SetCursor
        If wait Then
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub DoSuccessful() Implements ILoginView.DoSuccessful
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Sub DoFailed() Implements ILoginView.DoFailed
        PasswordSecureTextBox.SecureText.Dispose()
        PasswordSecureTextBox.Text = Nothing
        PasswordSecureTextBox.ConstructSecureString()
        Me.Focus()
        UsernameTextBox.Select()
    End Sub
#End Region

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Activate()
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        presenter.Login()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
End Class
