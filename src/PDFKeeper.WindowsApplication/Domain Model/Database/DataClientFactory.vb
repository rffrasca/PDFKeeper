'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
    "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")> _
Public Class DataClientFactory
    Private Shared connnectionProperties As DatabaseConnectionProperties = _
       DatabaseConnectionProperties.Instance
    Private m_DataClient As IDataClient

    Public Sub New(ByVal dataClient As IDataClient)
        m_DataClient = dataClient
    End Sub

    ''' <summary>
    ''' Returns an IDataClient object set to the DataClient for the database
    ''' management system stored in the DatabaseConnectionProperties object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Public Function SetDataClient() As IDataClient
        If connnectionProperties.DatabaseManagementSystem = "Oracle" Then
            m_DataClient = New OracleDataClient
        End If
        Return m_DataClient
    End Function
End Class
