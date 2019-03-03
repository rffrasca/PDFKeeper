'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
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
Public Class DatabaseConnectionProperties
    Private Shared s_Instance As DatabaseConnectionProperties

    Protected Sub New()
        ' Prevents multiple instances of this class and allows this class to
        ' be subclassed. 
    End Sub

    Public Shared ReadOnly Property Instance As DatabaseConnectionProperties
        Get
            If s_Instance Is Nothing Then
                s_Instance = New DatabaseConnectionProperties
            End If
            Return s_Instance
        End Get
    End Property

    Public Property UserName As String
    Public Property Password As SecureString
    Public Property DataSource As String
    Public Property DatabaseSystem As String

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Public Sub Test()
        Dim dataProvider As IDataProvider = Nothing
        If DatabaseSystem = "Oracle" Then
            dataProvider = New OracleDataProvider
        End If
        dataProvider.Test()
    End Sub
End Class
