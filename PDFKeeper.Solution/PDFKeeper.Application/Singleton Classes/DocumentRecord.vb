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

Public NotInheritable Class DocumentRecord
	Private Shared _instance As DocumentRecord = New DocumentRecord()
	Private _id As Integer
	Private _keywords As String
	Private _notes As String
	Private undoNotes As String
	
	Public Shared ReadOnly Property Instance As DocumentRecord
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Gets or sets the Document Record ID.  Setting the Id to 0 will set a
	''' null record.
	''' </summary>
	Public Property Id As Integer
		Get
			Return _id
		End Get
		Set(ByVal value As Integer)
			_id = value
			OnIdChanged
		End Set
	End Property
	
	''' <summary>
	''' Gets the Document Record Keywords.
	''' </summary>
	Public ReadOnly Property Keywords As String
		Get
			Return _keywords
		End Get
	End Property
	
	''' <summary>
	''' Gets or sets the Document Record Notes.
	''' </summary>
	Public Property Notes As String
		Get
			Return _notes
		End Get
		Set(ByVal value As String)
			undoNotes = _notes
			_notes = value
			OnNotesChanged
		End Set
	End Property
	
	''' <summary>
	''' Gets the absolute path name of the Document Record PDF.
	''' </summary>
	Public ReadOnly Property PdfPathName As String
		Get
			Return Path.Combine( _
				ApplicationProfileFolders.Cache, _
				"pdfkeeper" & Id & ".pdf")
		End Get
	End Property
	
	''' <summary>
	''' 
	''' </summary>
	Public ReadOnly Property PdfPreviewImage As System.Drawing.Image
		Get
			Return PdfFirstPageToImage(PdfPathName)
		End Get
	End Property
	
	''' <summary>
	''' Gets the text from the Document Record PDF.
	''' </summary>
	Public ReadOnly Property PdfText As String
		Get
			Return PdfTextToString(PdfPathName)
		End Get
	End Property
		
	''' <summary>
	''' Queries Document Record Keywords, Notes, and PDF from the database.
	''' Only query Keywords and Notes when the the Document Record PDF is
	''' contained in the file cache.  When the PDF document is queried, add to
	''' the file cache.  Encrypt the PDF document if file system encryption is
	''' supported by the operating system.
	''' </summary>
	Private Sub OnIdChanged
		Dim query As String
		_keywords = Nothing
		_notes = Nothing
		If Id = 0 Then
			Exit Sub
		End If
		Dim connection As New DatabaseConnection
		If connection.Open( _
			UserSettings.LastUserName, _
			DatabaseConnectionForm.dbPassword, _
			UserSettings.LastDataSource) = 1 Then
			connection.Dispose
			_id = 0
			Throw New DataException(String.Format( _
				CultureInfo.CurrentCulture, _
				PdfKeeper.Strings.DatabaseUnavailable, _
				UserSettings.LastDataSource))
		End If
		If FileCache.Instance.ContainsItemAndHashValuesMatch( _
			PdfPathName) Then
			
			query = "select doc_keywords,doc_notes " & _
					"from pdfkeeper.docs " & _
					"where doc_id = " & Id
		Else
			query = "select doc_keywords,doc_notes,doc_pdf " & _
					"from pdfkeeper.docs " & _
					"where doc_id = " & Id
		End If
		Try
			Using command As New OracleCommand(query, connection.oraConnection)
				Using dataReader As OracleDataReader = command.ExecuteReader()
					dataReader.Read()
					If dataReader.IsDBNull(0) = False Then
						_keywords = dataReader.GetString(0)
					End If
					If dataReader.IsDBNull(1) = False Then
						_notes = dataReader.GetString(1)
					End If
					If FileCache.Instance.ContainsItemAndHashValuesMatch( _
						PdfPathName) = False Then
						
						Using blob As OracleBlob = dataReader.GetOracleBlob(2)
  							Using memory As New MemoryStream(blob.Value)
  								Using file As New FileStream( _
  									PdfPathName, _
  									FileMode.Create, _
  									FileAccess.Write)
									Try
										file.Write( _
											memory.ToArray, _
											0, _
											CInt(blob.Length))
									Catch ex As IOException
										_keywords = Nothing
										_notes = Nothing
										_id = 0
										Throw New IOException( _
											ex.Message.ToString())
										connection.Dispose
  									Finally
  										file.Close()
									End Try
								End Using
							End Using
						End Using
						FileCache.Instance.Add(PdfPathName)
					End If
					EncryptFile(PdfPathName)
				End Using
			End Using
		Catch ex As OracleException
			_id = 0
			Throw New DataException(ex.Message.ToString())
		Finally
			connection.Dispose
		End Try
	End Sub
	
	''' <summary>
	''' Updates Document Notes in the database for Id.
	''' </summary>
	Private Sub OnNotesChanged
		Dim connection As New DatabaseConnection
		If connection.Open( _
			UserSettings.LastUserName, _
			DatabaseConnectionForm.dbPassword, _
			UserSettings.LastDataSource) = 1 Then
			connection.Dispose
			_notes = undoNotes
			Throw New DataException(String.Format( _
				CultureInfo.CurrentCulture, _
				PdfKeeper.Strings.DatabaseUnavailable, _
				UserSettings.LastDataSource))
		End If
		Dim update As String = _
			"update pdfkeeper.docs " & _
			"set doc_notes =q'[" & Notes & "]',doc_dummy = '' " & _
			"where doc_id = " & Id
		Using command As New OracleCommand(update, connection.oraConnection)
			Try
				command.ExecuteNonQuery
			Catch ex As OracleException
				_notes = undoNotes
				Throw New DataException(ex.Message.ToString())
			Finally
				connection.Dispose
			End Try
		End Using
	End Sub
End Class
