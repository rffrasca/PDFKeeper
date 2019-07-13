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

    ''' <summary>
    ''' Prevents multiple instances of this class and allows this class to be
    ''' subclassed.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub New()
    End Sub

    ''' <summary>
    ''' Allows single instance access to the class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    Public Property DatabaseManagementSystem As String
End Class
