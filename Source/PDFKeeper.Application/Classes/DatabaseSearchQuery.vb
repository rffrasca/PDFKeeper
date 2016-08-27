'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
'* Copyright (C) 2009-2016 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify it under
'* the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public Class DatabaseSearchQuery
	Dim sql As String
	
	''' <summary>
	''' Search SQL query constructor.
	''' </summary>
	''' <param name="searchString"></param>
	''' <param name="orderBy"></param>
	Public Sub New(searchValue As String, orderBy As String)
		sql = "select doc_id,doc_title,doc_author," & _
			  "doc_subject,doc_added from pdfkeeper.docs where " & _
			  "(contains(doc_dummy,q'[" & searchValue & "]'))>0 " & _
			  "order by " & orderBy
	End Sub
		
	''' <summary>
	''' Executes the query set by the constructor.
	''' </summary>
	''' <returns>Records returned from the query in a DataTable.</returns>
	<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", _
		"CA1306:SetLocaleForDataTypes")> _
	Public Function ExecuteQuery As DataTable
		Using oraConnection As New OracleConnection
			Try
				oraConnection.ConnectionString = _
					DBConnectionString.GetString
				oraConnection.Open
				Dim adapter As New OracleDataAdapter(sql, oraConnection)
				Dim table As New DataTable
				adapter.Fill(table)
				Return table
			Catch ex As OracleException
				Throw New DataException(ex.Message.ToString())
			End Try
		End Using
	End Function
End Class
