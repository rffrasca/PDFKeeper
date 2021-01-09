'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Public NotInheritable Class DbInstanceProperties
    Private Shared s_Password As SecureString

    Private Sub New()
        ' All methods are shared.
    End Sub

    ''' <summary>
    ''' Gets/Sets the database platform being used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Property Platform As String
        Get
            Return My.Settings.DbManagementSystem
        End Get
        Set(value As String)
            My.Settings.DbManagementSystem = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the database user name or Nothing if the database platform is SQLite. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UserName As String
        Get
            If Platform = DatabasePlatform.Sqlite.ToString Then
                Return Nothing
            Else
                Return My.Settings.Username
            End If
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
                Throw New ArgumentNullException(NameOf(value))
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
            Dim result As String = Nothing
            If Platform = DatabasePlatform.Sqlite.ToString Then
                result = "Data Source=" + UserProfile.LocalDatabasePath + ";" &
                    "Version=3; + Auto Vacuum=Full"
            ElseIf Platform = DatabasePlatform.Oracle.ToString Then
                result = "Data Source=" + My.Settings.Datasource + ";" &
                    "Pooling=" + My.Settings.Pooling
            End If
            Return result
        End Get
    End Property
End Class
