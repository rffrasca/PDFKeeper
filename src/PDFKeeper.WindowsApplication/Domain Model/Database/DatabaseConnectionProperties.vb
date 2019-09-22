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
Friend NotInheritable Class DatabaseConnectionProperties
    Private Shared s_Instance As DatabaseConnectionProperties
    Private m_UserName As String
    Private m_Password As SecureString
    Private m_DataSource As String
    Private m_DatabaseManagementSystem As String

    ''' <summary>
    ''' Prevents multiple instances of this class.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
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
        Get
            Return m_UserName
        End Get
        Set(value As String)
            m_UserName = value
        End Set
    End Property

    Public Property Password As SecureString
        Get
            Return m_Password
        End Get
        Set(value As SecureString)
            m_Password = value
            m_Password.MakeReadOnly()
        End Set
    End Property

    Public Property DataSource As String
        Get
            Return m_DataSource
        End Get
        Set(value As String)
            m_DataSource = value
        End Set
    End Property

    Public Property DatabaseManagementSystem As String
        Get
            Return m_DatabaseManagementSystem
        End Get
        Set(value As String)
            m_DatabaseManagementSystem = value
        End Set
    End Property

    ''' <summary>
    ''' Validates the Database Connection Properties by opening a connection to
    ''' the database, and then closing it.
    ''' </summary>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    Public Sub Validate()
        Dim dataClient As IDataClient = New DataClient
        dataClient.TestConnection()
    End Sub
End Class
