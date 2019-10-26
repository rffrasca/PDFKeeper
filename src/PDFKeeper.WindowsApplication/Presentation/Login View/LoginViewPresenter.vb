'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
    "CA1726:UsePreferredTerms", MessageId:="Login")> _
Public Class LoginViewPresenter
    Private view As ILoginView
    Private model As DatabaseConnectionProperties = _
        DatabaseConnectionProperties.Instance
    Private messageDisplay As IMessageDisplay = New MessageDisplay

    Public Sub New(ByVal view As ILoginView)
        Me.view = view
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
        "CA1726:UsePreferredTerms", MessageId:="Login")> _
    Public Sub Login()
        Try
            view.OnLoginStarted()
            UpdateModel()
            model.Validate()
            view.OnLoginSuccessful()
        Catch ex As ArgumentException
            messageDisplay.Show(ex.Message, True)
            view.OnLoginFailed()
        Catch ex As OracleException
            messageDisplay.Show(ex.Message, True)
            view.OnLoginFailed()
        End Try
    End Sub

    Private Sub UpdateModel()
        With model
            .UserName = view.UserName
            .Password = view.Password
            .DataSource = view.DataSource
            .DatabaseManagementSystem = view.DatabaseManagementSystem
        End With
    End Sub
End Class
