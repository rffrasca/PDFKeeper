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

Public Class DatabaseNonQuery
	Private sql As String
	Private insert As Boolean = False
	Private _pdfFile As String
	Private pdfBlob As Byte()
		
	''' <summary>
	''' Delete SQL statement constructor.
	''' </summary>
	''' <param name="docId">Document ID of record to delete.</param>
	Public Sub New(ByVal docId As String)
		sql = "delete from pdfkeeper.docs where doc_id =" & docId
	End Sub
	
	''' <summary>
	''' Update SQL statement constructor.
	''' </summary>
	''' <param name="docId">Document ID of record to update</param>
	''' <param name="docNotes">Document Notes to set.</param>
	Public Sub New(ByVal docId As String, ByVal docNotes As String)
		sql = "update pdfkeeper.docs " & _
			  "set doc_notes =q'[" & docNotes & "]',doc_dummy = '' " & _
			  "where doc_id = " & docId
	End Sub
	
	''' <summary>
	''' Insert SQL statement constructor.
	''' </summary>
	''' <param name="docTitle">Document Title to insert.</param>
	''' <param name="docAuthor">Document Author to insert.</param>
	''' <param name="docSubject">Document Subject to insert.</param>
	''' <param name="docKeywords">Document Keywords to insert.</param>
	''' <param name="pdfFile">PDF file to insert.</param>
	Public Sub New( _
		ByVal docTitle As String, _
		ByVal docAuthor As String,  _
		ByVal docSubject As String, _
		ByVal docKeywords As String, _
		ByVal pdfFile As String)
		
		' Create the Anonymous PL/SQL block statement for the insert.
		sql = " begin " & _
			  " insert into pdfkeeper.docs values( " & _
			  " pdfkeeper.docs_seq.NEXTVAL, " & _
			  " q'[" & docTitle & "]', " & _
			  " q'[" & docAuthor & "]', " & _
			  " q'[" & docSubject & "]', " & _
			  " q'[" & docKeywords & "]', " & _
			  " to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'), " & _
			  " '', :1, '') ;" & _
   			  " end ;"
		insert = True
		_pdfFile = pdfFile
	End Sub
		
	''' <summary>
	''' If performing an insert, read the PDF file into a stream, and then
	''' execute the SQL statement set by the constructor. 
	''' </summary>
	Public Sub ExecuteNonQuery
		If insert Then
			pdfBlob = FileToByteArray(_pdfFile)
		End If
		Using oraConnection As New OracleConnection
			Try
				oraConnection.ConnectionString = _
					DBConnection.Instance.ConnectionString
				oraConnection.Open
				Dim oraCommand As New OracleCommand(sql, oraConnection)
				If insert Then
					oraCommand.CommandType = CommandType.Text
					
					' Bind the parameter to the insert statement.
					Dim oraParameter As OracleParameter = _
						oraCommand.Parameters.Add("doc_pdf", OracleDbType.Blob)
					oraParameter.Direction = ParameterDirection.Input
					oraParameter.Value = pdfBlob
				End If
				oraCommand.ExecuteNonQuery
			Catch ex As OracleException
				Throw New DataException(ex.Message.ToString())
			End Try
		End Using
	End Sub
End Class
