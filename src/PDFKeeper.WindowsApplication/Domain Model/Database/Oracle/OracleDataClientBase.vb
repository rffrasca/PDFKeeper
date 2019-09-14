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
Public MustInherit Class OracleDataClientBase
    Implements IDisposable
    Private connnectionProperties As DatabaseConnectionProperties = _
       DatabaseConnectionProperties.Instance
    Private connectionString As String = _
        "Data Source=" + connnectionProperties.DataSource + ";" & _
        "Pooling=True"
    Private credential As OracleCredential
    Private m_Connection As OracleConnection

    Protected Sub New()
        credential = New OracleCredential(connnectionProperties.UserName, _
                                          connnectionProperties.Password)
        m_Connection = New OracleConnection(connectionString, credential)
    End Sub

    Protected ReadOnly Property Connection As OracleConnection
        Get
            Return m_Connection
        End Get
    End Property

    Protected Sub Open()
        Connection.Open()
    End Sub

    Protected Function QueryToDataTable(ByVal sqlCommand As OracleCommand) As DataTable
        Using adapter As New OracleDataAdapter(sqlCommand)
            Using table As New DataTable
                table.Locale = CultureInfo.InvariantCulture
                Open()
                adapter.Fill(table)
                Close()
                Return table
            End Using
        End Using
    End Function

    Public Function QueryToObject(ByVal sqlCommand As OracleCommand) As Object
        If sqlCommand Is Nothing Then
            Throw New ArgumentException("oraCommand Cannot be a Nothing (null) reference.")
        End If
        Try
            Open()
            Return sqlCommand.ExecuteScalar
        Finally
            Close()
        End Try
    End Function

    Protected Sub QueryBlobToFile(ByVal sqlCommand As OracleCommand, _
                                  ByVal targetPathName As String)
        If sqlCommand Is Nothing Then
            Throw New ArgumentException("oraCommand Cannot be a Nothing (null) reference.")
        End If
        Using adapter As New OracleDataAdapter(sqlCommand)
            Open()
            Using dataReader As OracleDataReader = sqlCommand.ExecuteReader
                dataReader.Read()
                Dim blob As OracleBlob = dataReader.GetOracleBlob(0)
                Using memoryStream As New MemoryStream(blob.Value)
                    Using fileStream As New FileStream(targetPathName, _
                                                       FileMode.Create, _
                                                       FileAccess.Write)
                        fileStream.Write(memoryStream.ToArray, _
                                         0, _
                                         CInt(blob.Length))
                    End Using
                End Using
            End Using
            Close()
        End Using
    End Sub

    Protected Sub ExecuteNonQuery(ByVal sqlCommand As OracleCommand)
        If sqlCommand Is Nothing Then
            Throw New ArgumentException("oraCommand Cannot be a Nothing (null) reference.")
        End If
        Open()
        sqlCommand.ExecuteNonQuery()
        Close()
    End Sub

    Protected Sub Close()
        Dispose()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                m_Connection.Close()
                m_Connection.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
