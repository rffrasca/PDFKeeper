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

Public Class DatabaseAuthorsSubjectsQuery
	Dim sql As String
	Dim tableColumn As String
	
	''' <summary>
	''' All Authors SQL query constructor.
	''' </summary>
	Public Sub New()
		sql = "select doc_author,count(doc_author) from pdfkeeper.docs " & _
			  "group by doc_author"
		tableColumn = "doc_author"
	End Sub
	
	''' <summary>
	''' All Subjects for specified Author SQL query constructor.
	''' </summary>
	''' <param name="author"></param>
	Public Sub New(author As String)
		sql = "select doc_subject from pdfkeeper.docs where " & _
			  "doc_author = q'[" & author & "]' group by doc_subject"
		tableColumn = "doc_subject"	
	End Sub
	
	''' <summary>
	''' Executes the query set by the constructor.
	''' </summary>
	''' <returns>Records returned from the query in an ArrayList.</returns>
	Public Function ExecuteQuery As ArrayList
		Dim rows As New ArrayList
		Using oraConnection As New OracleConnection
			Try
				oraConnection.ConnectionString = _
					DBConnection.Instance.ConnectionString
				oraConnection.Open
				Dim oraCommand As New OracleCommand(sql, oraConnection)
				Dim dataReader As OracleDataReader = oraCommand.ExecuteReader()
				Do While (dataReader.Read())
					rows.Add(dataReader(tableColumn))
				Loop
				Return rows
			Catch ex As OracleException
				Throw New DataException(ex.Message.ToString())
			End Try
		End Using
	End Function
End Class
