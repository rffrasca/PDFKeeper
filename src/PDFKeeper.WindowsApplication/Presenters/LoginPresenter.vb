'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
    "CA1726:UsePreferredTerms", MessageId:="Login")>
Public Class LoginPresenter
    Private view As ILoginView
    Private model As New Login
    Private message As IMessageDisplayService = New MessageDisplayService

    Public Sub New(ByVal view As ILoginView)
        Me.view = view
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
        "CA1726:UsePreferredTerms", MessageId:="Login")>
    Public Sub Login()
        Try
            view.SetCursor(True)
            ' NOTE: Oracle is the only supported RDBMS at this time.  To add future systems, add a ComboBox
            ' to LoginForm containing the supported Databases and bind it to the LoginDatabase setting.
            With model
                .DbManagementSystem = "Oracle"
                .Password = view.Password
                .TestConnection()
            End With
            view.SetCursor(False)
            view.DoSuccessful()
        Catch ex As ArgumentException
            view.SetCursor(False)
            message.Show(ex.Message, True)
            view.DoFailed()
        Catch ex As OracleException
            model.ResetCredential()
            view.SetCursor(False)
            message.Show(ex.Message, True)
            view.DoFailed()
        End Try
    End Sub
End Class
