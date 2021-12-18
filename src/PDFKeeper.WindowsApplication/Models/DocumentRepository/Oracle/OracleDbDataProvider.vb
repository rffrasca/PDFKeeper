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
Public NotInheritable Class OracleDbDataProvider
    Implements IDisposable
    Private ReadOnly m_Connection As OracleConnection
    Private Shared credential As OracleCredential

    Public Sub New()
        If credential Is Nothing Then
            credential = New OracleCredential(DbInstanceProperties.UserName,
                                              DbInstanceProperties.Password)
        End If
        If m_Connection Is Nothing Then
            m_Connection = New OracleConnection(
                DbInstanceProperties.ConnectionString,
                credential)
        End If
    End Sub

    ''' <summary>
    ''' Returns the database connection object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Connection As OracleConnection
        Get
            Return m_Connection
        End Get
    End Property

    Public Sub TestConnection()
        Connection.Open()
        Connection.Close()
    End Sub

#Disable Warning CA1822 ' Mark members as static
    Public Sub ResetCredential()
#Enable Warning CA1822 ' Mark members as static
        credential = Nothing
    End Sub

    Public Function QueryToDataTable(ByVal sqlCommand As OracleCommand) As DataTable
        Using adapter As New OracleDataAdapter(sqlCommand)
            Using table As New DataTable
                table.Locale = CultureInfo.InvariantCulture
                Connection.Open()
                adapter.Fill(table)
                Connection.Close()
                Return table
            End Using
        End Using
    End Function

    Public Function QueryToObject(ByVal sqlCommand As OracleCommand) As Object
        If sqlCommand Is Nothing Then
            Throw New ArgumentNullException(NameOf(sqlCommand))
        End If
        Try
            Connection.Open()
            Return sqlCommand.ExecuteScalar
        Finally
            Connection.Close()
        End Try
    End Function

    Public Sub QueryBlobToFile(ByVal sqlCommand As OracleCommand, _
                               ByVal targetPathName As String)
        If sqlCommand Is Nothing Then
            Throw New ArgumentNullException(NameOf(sqlCommand))
        End If
        Using adapter As New OracleDataAdapter(sqlCommand)
            Connection.Open()
            Using dataReader As OracleDataReader = sqlCommand.ExecuteReader
                dataReader.Read()
                Dim blob As OracleBlob = dataReader.GetOracleBlob(0)
                Using memoryStream As New MemoryStream(blob.Value)
                    Using fileStream As New FileStream(targetPathName, _
                                                       FileMode.Create, _
                                                       FileAccess.Write)
                        fileStream.Write(memoryStream.ToArray, 0, _
                                         CInt(blob.Length))
                    End Using
                End Using
            End Using
            Connection.Close()
        End Using
    End Sub

    Public Sub ExecuteNonQuery(ByVal sqlCommand As OracleCommand)
        If sqlCommand Is Nothing Then
            Throw New ArgumentNullException(NameOf(sqlCommand))
        End If
        Connection.Open()
        sqlCommand.ExecuteNonQuery()
        Connection.Close()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Public Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                m_Connection.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
