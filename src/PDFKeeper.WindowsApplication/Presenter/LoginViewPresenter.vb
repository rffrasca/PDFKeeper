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
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
    "CA1726:UsePreferredTerms", _
    MessageId:="Login")> _
Public Class LoginViewPresenter
    Private view As ILoginView
    Private model As DatabaseCredential = DatabaseCredential.Instance

    Public Sub New(view As ILoginView)
        Me.view = view
    End Sub

    Public Sub OkClick()
        UpdateCredential()
        Try
            TestConnection()
            view.OnLoginFinished(True)
        Catch ex As OracleException
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowError(ex.Message)
            view.OnLoginFinished(False)
        End Try
    End Sub

    Private Sub UpdateCredential()
        model.UserName = view.UserName
        model.Password = view.Password
        model.DataSource = view.DataSource
        model.DatabaseSystem = view.DatabaseSystem
    End Sub

    Private Shared Sub TestConnection()
        Dim dataProvider As IDataProvider = Nothing
        DataProviderHelper.SetDataProvider(dataProvider)
        dataProvider.TestConnection()
    End Sub
End Class
