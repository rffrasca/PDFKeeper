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

Public Class LoginPresenter
    Private ReadOnly view As ILoginView
    Private ReadOnly loginSvc As ILoginService
    Private ReadOnly message As New MessageBoxHelper
    Private viewInstance As Form

    Public Sub New(ByVal view As ILoginView, ByVal loginSvc As ILoginService)
        Me.view = view
        Me.loginSvc = loginSvc
    End Sub

    Friend Sub LoginForm_Load(sender As Object, e As EventArgs)
        viewInstance = CType(sender, Form)
        viewInstance.Activate()
    End Sub

    Friend Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            Dim model = New DbSessionModel
            With model
                .UserName = view.UserName
                .Password = view.Password
                .DataSource = view.DataSource
            End With
            loginSvc.InitSession(My.Settings.DbManagementSystem, model)
            viewInstance.DialogResult = DialogResult.OK
        Catch ex As ArgumentException
            message.ShowMessage(ex.Message, True)
            view.ResetView()
        Catch ex As DbException
            message.ShowMessage(ex.Message, True)
            loginSvc.ResetCredential()
            view.ResetView()
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        viewInstance.DialogResult = DialogResult.Cancel
    End Sub
End Class
