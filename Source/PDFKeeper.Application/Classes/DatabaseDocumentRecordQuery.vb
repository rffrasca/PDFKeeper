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

Public Class DatabaseDocumentRecordQuery
	Private sql As String
	Private pdfQuery As Boolean
	Private _keywords As String
	Private _notes As String
	Private _pdfPathName As String
		
	''' <summary>
	''' Document Keywords and Document Notes SQL query constructor.
	''' </summary>
	''' <param name="docId">Document ID to query.</param>
	Public Sub New(ByVal docId As String)
		sql = "select doc_keywords,doc_notes from pdfkeeper.docs " & _
			  "where doc_id = " & docId
		pdfQuery = False
		ExecuteQuery
	End Sub
	
	''' <summary>
	''' Document Keywords, Document Notes, and Document PDF SQL query
	''' constructor.
	''' </summary>
	''' <param name="docId">Document ID to query.</param>
	''' <param name="pdfFile">Absolute path name of PDF to write.</param>
	Public Sub New(ByVal docId As String, pdfFile As String)
		sql = "select doc_keywords,doc_notes,doc_pdf from pdfkeeper.docs " & _
			  "where doc_id = " & docId
		pdfQuery = True
		_pdfPathName = pdfFile
		ExecuteQuery
	End Sub
	
	''' <summary>
	''' Get Document Keywords for Document ID.
	''' </summary>
	Public ReadOnly Property Keywords As String
		Get
			Return _keywords
		End Get
	End Property
	
	''' <summary>
	''' Get Document Notes for Document ID.
	''' </summary>
	Public ReadOnly Property Notes As String
		Get
			Return _notes
		End Get
	End Property
	
	''' <summary>
	''' Get absolute path name of PDF passed to the constructor.  An exception
	''' will be thrown if PDF file was not passed to the constructor.
	''' </summary>
	Public ReadOnly Property PdfPathName As String
		Get
			If pdfQuery = False Then
				Throw New NotSupportedException( _
					String.Format( _
					CultureInfo.CurrentCulture, _
					PdfKeeper.Strings.PropertyNotSupported, _
					"PdfPathName"))
			End If
			Return _pdfPathName
		End Get
	End Property
	
	''' <summary>
	''' Executes the query set by the constructor, setting the Keywords and
	''' Notes properties.  When pdfQuery is True, write the PDF to the
	''' specified path name.
	''' </summary>
	Private Sub ExecuteQuery
		_keywords = Nothing
		_notes = Nothing
		Using oraConnection As New OracleConnection
			Try
				oraConnection.ConnectionString = _
					DBConnectionString.GetString
				oraConnection.Open
				Dim oraCommand As New OracleCommand(sql, oraConnection)
				Dim dataReader As OracleDataReader = oraCommand.ExecuteReader()
				dataReader.Read()
				If dataReader.IsDBNull(0) = False Then
					_keywords = dataReader.GetString(0)
				End If
				If dataReader.IsDBNull(1) = False Then
					_notes = dataReader.GetString(1)
				End If
				If pdfQuery Then
					Dim blob As OracleBlob = dataReader.GetOracleBlob(2)
					Dim memory As New MemoryStream(blob.Value)
					Using file As New FileStream( _
						_pdfPathName, _
						FileMode.Create, _
						FileAccess.Write)
						
						Try
							file.Write(memory.ToArray, 0, CInt(blob.Length))
						Catch ex As IOException
							_keywords = Nothing
							_notes = Nothing
							Throw New IOException(ex.Message.ToString())
						End Try	
					End Using
				End If
			Catch ex As OracleException
				Throw New DataException(ex.Message.ToString())
			End Try
		End Using
	End Sub
End Class
