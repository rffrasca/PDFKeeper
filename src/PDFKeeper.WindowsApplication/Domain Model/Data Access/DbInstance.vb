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
Public NotInheritable Class DbInstance
    Private Shared s_Password As SecureString

    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    ''' <summary>
    ''' Returns the database management system name being used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property DbManagementSystem As String
        Get
            Return My.Settings.DbManagementSystem
        End Get
    End Property

    ''' <summary>
    ''' Returns the database user name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UserName As String
        Get
            Return My.Settings.Username
        End Get
    End Property

    ''' <summary>
    ''' Gets/Sets the encrypted database password.
    ''' </summary>
    ''' <value>Password as a SecureString</value>
    ''' <returns>Password as a SecureString</returns>
    ''' <remarks></remarks>
    Public Shared Property Password As SecureString
        Get
            Return s_Password
        End Get
        Set(value As SecureString)
            If value Is Nothing Then
                Throw New ArgumentNullException("value")
            End If
            s_Password = value
            s_Password.MakeReadOnly()
        End Set
    End Property

    ''' <summary>
    ''' Returns the database connection string.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ConnectionString As String
        Get
            Dim m_ConnectionString As String = Nothing
            If DbManagementSystem = "Oracle Database" Then
                m_ConnectionString = _
                    "Data Source=" + My.Settings.Datasource + ";" & _
                    "Pooling=" + My.Settings.Pooling
            End If
            Return m_ConnectionString
        End Get
    End Property

    ''' <summary>
    ''' Performs a test connection to the database.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub TestConnection()
        If DbInstance.DbManagementSystem = "Oracle Database" Then
            Using provider As New OracleDbDataProvider
                provider.TestConnection()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Resets the credential object that contains the database user name and
    ''' encrypted password.  This should be called when a test connection to
    ''' the database fails.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ResetCredential()
        If DbInstance.DbManagementSystem = "Oracle Database" Then
            Using provider As New OracleDbDataProvider
                provider.ResetCredential()
            End Using
        End If
    End Sub
End Class
