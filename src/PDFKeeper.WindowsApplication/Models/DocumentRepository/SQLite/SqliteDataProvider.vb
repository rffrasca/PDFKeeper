'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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
Public NotInheritable Class SqliteDataProvider
    Implements IDisposable
    Private ReadOnly m_Connection As SQLiteConnection
    Private disposedValue As Boolean

    Public Sub New()
        If m_Connection Is Nothing Then
            m_Connection = New SQLiteConnection(
                DbInstanceProperties.ConnectionString)
        End If
    End Sub

    ''' <summary>
    ''' Returns the database connection object.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Connection As SQLiteConnection
        Get
            Return m_Connection
        End Get
    End Property

    Public Sub RebuildIndex()
        Dim sqlStatement As String = "insert into docs_index(docs_index) values('rebuild')"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, Connection)
                ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Function QueryToDataTable(ByVal sqlCommand As SQLiteCommand) As DataTable
        Using adapter As New SQLiteDataAdapter(sqlCommand)
            Using table As New DataTable
                table.Locale = CultureInfo.InvariantCulture
                Connection.Open()
                LoadExtensionLibrary()
                adapter.Fill(table)
                Connection.Close()
                Return table
            End Using
        End Using
    End Function

    Public Function QueryToObject(ByVal sqlCommand As SQLiteCommand) As Object
        If sqlCommand Is Nothing Then
            Throw New ArgumentNullException(NameOf(sqlCommand))
        End If
        Try
            Connection.Open()
            LoadExtensionLibrary()
            Return sqlCommand.ExecuteScalar
        Finally
            Connection.Close()
        End Try
    End Function

    Public Sub QueryBlobToFile(ByVal sqlCommand As SQLiteCommand,
                               ByVal targetPathName As String)
        If sqlCommand Is Nothing Then
            Throw New ArgumentNullException(NameOf(sqlCommand))
        End If
        Try
            Connection.Open()
            LoadExtensionLibrary()
            Dim bytes As Byte() = sqlCommand.ExecuteScalar
            Using stream As New FileStream(targetPathName,
                                           FileMode.Create,
                                           FileAccess.Write)
                stream.Write(bytes, 0, bytes.Length)
            End Using
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Sub ExecuteNonQuery(ByVal sqlCommand As SQLiteCommand)
        If sqlCommand Is Nothing Then
            Throw New ArgumentNullException(NameOf(sqlCommand))
        End If
        Connection.Open()
        LoadExtensionLibrary()
        sqlCommand.ExecuteNonQuery()
        Connection.Close()
    End Sub

    Private Sub LoadExtensionLibrary()
        Connection.EnableExtensions(True)
        Connection.LoadExtension("SQLite.Interop.dll", "sqlite3_fts5_init")
    End Sub

    Private Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                m_Connection.Dispose()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
