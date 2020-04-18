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
Public Class Login
    Public Property DbManagementSystem As String
        Get
            Return My.Settings.DbManagementSystem
        End Get
        Set(value As String)
            My.Settings.DbManagementSystem = value
        End Set
    End Property

    Public Property Password As SecureString
        Get
            Return DbInstanceProperties.Password
        End Get
        Set(value As SecureString)
            DbInstanceProperties.Password = value
        End Set
    End Property

    Public Sub TestConnection()
        DbInstanceUtil.TestConnection()
    End Sub

    Public Sub ResetCredential()
        DbInstanceUtil.ResetCredential()
    End Sub
End Class
