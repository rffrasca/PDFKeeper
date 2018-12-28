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
Public Class LoginForm
    Implements ILoginView
    Private presenter As LoginViewPresenter

    Public Sub New()
        InitializeComponent()
        presenter = New LoginViewPresenter(Me)
        HelpProvider.HelpNamespace = HelpProviderHelper.HelpFile
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Activate()
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Me.Cursor = Cursors.WaitCursor
        presenter.Login()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

#Region "ILoginView members get by LoginViewPresenter"
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

    Public ReadOnly Property DatabaseSystem As String Implements ILoginView.DatabaseSystem
        Get
            ' NOTE: Oracle is the only supported RDBMS at this time.  To add future
            ' systems, add a ComboBox to LoginForm containing the supported
            ' Database Management Systems  and bind it to the
            ' LoginViewDatabaseSystem setting.
            My.Settings.LoginDatabaseManagementSystem = "Oracle"
            Return My.Settings.LoginDatabaseManagementSystem
        End Get
    End Property
#End Region

#Region "ILoginView members called by LoginViewPresenter"
    Public Sub OnLoginFinished(successful As Boolean) Implements ILoginView.OnLoginFinished
        If successful Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            PasswordSecureTextBox.SecureText.Clear()
            PasswordSecureTextBox.Text = Nothing
            UsernameTextBox.Select()
        End If
    End Sub
#End Region

End Class
