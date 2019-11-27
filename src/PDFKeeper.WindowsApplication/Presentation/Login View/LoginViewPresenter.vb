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
    Private messageDisplay As IMessageDisplay = New MessageDisplay

    Public Sub New(ByVal view As ILoginView)
        Me.view = view
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
        "CA1726:UsePreferredTerms", MessageId:="Login")> _
    Public Sub Login()
        Try
            view.OnLoginStarted()
            ' NOTE: Oracle is the only supported RDBMS at this time.  To add future systems, add a ComboBox
            ' to LoginForm containing the supported Databases and bind it to the LoginDatabase setting.
            My.Settings.DbManagementSystem = "Oracle Database"
            DbManagementSystem.Password = view.Password
            DbManagementSystem.TestConnection()
            view.OnLoginSuccessful()
        Catch ex As ArgumentException
            DbManagementSystem.ResetCredential()
            messageDisplay.Show(ex.Message, True)
            view.OnLoginFailed()
        Catch ex As OracleException
            DbManagementSystem.ResetCredential()
            messageDisplay.Show(ex.Message, True)
            view.OnLoginFailed()
        End Try
    End Sub
End Class
