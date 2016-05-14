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
	''' Gets the first page from the Document Record PDF as an image.
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
		Dim query As DatabaseQueryDocumentRecord
		_keywords = Nothing
		_notes = Nothing
		If Id = 0 Then
			Exit Sub
		End If
		If FileCache.Instance.ContainsItemAndHashValuesMatch(PdfPathName) Then
			query = New DatabaseQueryDocumentRecord(CStr(Id))
		Else
			query = New DatabaseQueryDocumentRecord(CStr(Id), PdfPathName)
		End If
		_keywords = query.Keywords
		_notes = query.Notes
		If FileCache.Instance.ContainsItemAndHashValuesMatch( _
			PdfPathName) = False Then
						
			FileCache.Instance.Add(PdfPathName)
		End If
		EncryptFile(PdfPathName)
	End Sub
	
	''' <summary>
	''' Updates Document Notes in the database for Id.
	''' </summary>
	Private Sub OnNotesChanged
		Dim nonQuery As New DatabaseNonQuery(CStr(Id), Notes)
		Try
			nonQuery.ExecuteNonQuery
		Catch ex As DataException
			_notes = undoNotes
		End Try
	End Sub
End Class
