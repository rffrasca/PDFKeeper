'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
Imports System.Data.SQLite
Imports System.Globalization
Imports PDFKeeper.Domain

Public Class DocumentRepositorySqlite
    Inherits DbRepositoryBase(Of SQLiteCommand)
    Implements IDocumentRepository

    Protected Overrides ReadOnly Property ConnectionString As String
        Get
            Return "Data Source=" + DbSession.LocalDatabasePath + ";Version=3"
        End Get
    End Property

    Public Function ListDocuments(choice As IDocumentRepository.DocumentListAction) As DataTable Implements IDocumentRepository.ListDocuments
        Dim sql As String
        If choice = IDocumentRepository.DocumentListAction.Flagged Then
            sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
                  "from docs where doc_flag = 1"
        ElseIf choice = IDocumentRepository.DocumentListAction.All Then
            sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added from docs"
        Else
            Throw New ArgumentException(Nothing, NameOf(choice))
        End If
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListDocuments(choice As IDocumentRepository.DocumentListAction, value As String) As DataTable Implements IDocumentRepository.ListDocuments
        Dim sql As String
        If choice = IDocumentRepository.DocumentListAction.BySearchTerm Then
            sql = "select rowid,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
                  "from docs_index where docs_index match :doc_dummy"
        ElseIf choice = IDocumentRepository.DocumentListAction.ByDateAdded Then
            sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
                  "from docs where doc_added like :doc_added || '%'"
        Else
            Throw New ArgumentException(Nothing, NameOf(choice))
        End If
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    If choice = IDocumentRepository.DocumentListAction.BySearchTerm Then
                        command.Parameters.AddWithValue("doc_dummy", value)
                    ElseIf choice = IDocumentRepository.DocumentListAction.ByDateAdded Then
                        command.Parameters.AddWithValue("doc_added", value)
                    End If
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListDocuments(filter As FindSelectionsFilterModel) As DataTable Implements IDocumentRepository.ListDocuments
        If filter Is Nothing Then
            Throw New ArgumentNullException(NameOf(filter))
        End If
        Dim where = "where "
        Dim andNeeded = False
        If filter.Author IsNot Nothing Then
            where &= "doc_author = :doc_author"
            andNeeded = True
        End If
        If filter.Subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If filter.Category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
            andNeeded = True
        End If
        If filter.TaxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added from docs " & where
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    If filter.Author IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_author", filter.Author)
                    End If
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_subject", filter.Subject)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_category", filter.Category)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListAuthors() As DataTable Implements IDocumentRepository.ListAuthors
        Dim sql = "select doc_author,count(doc_author) from docs group by doc_author"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListAuthors(filter As AuthorFilterModel) As DataTable Implements IDocumentRepository.ListAuthors
        If filter Is Nothing Then
            Throw New ArgumentNullException(NameOf(filter))
        End If
        Dim where = "where "
        Dim andNeeded = False
        If filter.Subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If filter.Category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
            andNeeded = True
        End If
        If filter.TaxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sql = "select doc_author from docs " & where & " group by doc_author"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_subject", filter.Subject)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_category", filter.Category)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListSubjects() As DataTable Implements IDocumentRepository.ListSubjects
        Dim sql = "select doc_subject,count(doc_subject) from docs group by doc_subject"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListSubjects(author As String) As DataTable Implements IDocumentRepository.ListSubjects
        Dim sql = "select doc_subject from docs where doc_author = :doc_author group by doc_subject"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    command.Parameters.AddWithValue("doc_author", author)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListSubjects(filter As SubjectFilterModel) As DataTable Implements IDocumentRepository.ListSubjects
        If filter Is Nothing Then
            Throw New ArgumentNullException(NameOf(filter))
        End If
        Dim where = "where "
        Dim andNeeded = False
        If filter.Author IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_author = :doc_author"
            Else
                where &= "doc_author = :doc_author"
            End If
            andNeeded = True
        End If
        If filter.Category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
            andNeeded = True
        End If
        If filter.TaxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sql = "select doc_subject from docs " & where & " group by doc_subject"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    If filter.Author IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_author", filter.Author)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_category", filter.Category)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListCategories() As DataTable Implements IDocumentRepository.ListCategories
        Dim sql = "select doc_category,count(doc_category) from docs group by doc_category having count(doc_category) > 0"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListCategories(filter As CategoryFilterModel) As DataTable Implements IDocumentRepository.ListCategories
        If filter Is Nothing Then
            Throw New ArgumentNullException(NameOf(filter))
        End If
        Dim where = "where "
        Dim andNeeded = False
        If filter.Author IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_author = :doc_author"
            Else
                where &= "doc_author = :doc_author"
            End If
            andNeeded = True
        End If
        If filter.Subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If filter.TaxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sql = "select doc_category from docs " & where & " group by doc_category"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    If filter.Author IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_author", filter.Author)
                    End If
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_subject", filter.Subject)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListTaxYears() As DataTable Implements IDocumentRepository.ListTaxYears
        Dim sql = "select doc_tax_year,count(doc_tax_year) from docs group by doc_tax_year having count(doc_tax_year) > 0"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListTaxYears(filter As TaxYearFilterModel) As DataTable Implements IDocumentRepository.ListTaxYears
        If filter Is Nothing Then
            Throw New ArgumentNullException(NameOf(filter))
        End If
        Dim where = "where "
        Dim andNeeded = False
        If filter.Author IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_author = :doc_author"
            Else
                where &= "doc_author = :doc_author"
            End If
            andNeeded = True
        End If
        If filter.Subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If filter.Category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
        End If
        Dim sql = "select doc_tax_year from docs " & where & " group by doc_tax_year"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    If filter.Author IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_author", filter.Author)
                    End If
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_subject", filter.Subject)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.AddWithValue("doc_category", filter.Category)
                    End If
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Sub CreateDocument(model As DocumentModel) Implements IDocumentRepository.CreateDocument
        If model Is Nothing Then
            Throw New ArgumentNullException(NameOf(model))
        End If
        Dim dateTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture)
        Dim sql = "insert into docs values(null," &
                  ":doc_title," &
                  ":doc_author," &
                  ":doc_subject," &
                  ":doc_keywords," &
                  ":doc_added," &
                  ":doc_notes," &
                  ":doc_pdf," &
                  ":doc_category," &
                  ":doc_flag," &
                  ":doc_tax_year," &
                  ":doc_text_annotations," &
                  ":doc_text)"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    With command
                        .Parameters.AddWithValue(":doc_title", model.Title)
                        .Parameters.AddWithValue(":doc_author", model.Author)
                        .Parameters.AddWithValue(":doc_subject", model.Subject)
                        .Parameters.AddWithValue(":doc_keywords", model.Keywords)
                        .Parameters.AddWithValue(":doc_added", dateTimeStamp)
                        .Parameters.AddWithValue(":doc_notes", model.Notes)
                        .Parameters.Add(":doc_pdf", DbType.Binary).Value = model.Pdf
                        .Parameters.AddWithValue(":doc_category", model.Category)
                        .Parameters.AddWithValue(":doc_flag", model.Flag)
                        .Parameters.AddWithValue(":doc_tax_year", model.TaxYear)
                        .Parameters.AddWithValue(":doc_text_annotations", model.TextAnnotations)
                        .Parameters.AddWithValue(":doc_text", model.Text)
                    End With
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Sub

    Public Function ReadDocument(id As Integer, searchTerm As String) As DocumentModel Implements IDocumentRepository.ReadDocument
        If searchTerm Is Nothing Then
            searchTerm = String.Empty
        End If
        Dim sql = "select doc_title,doc_author,doc_subject,doc_keywords,doc_notes,doc_pdf,doc_category," &
                  "doc_flag,doc_tax_year,doc_text from docs where doc_id = :doc_id"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    Dim model = New DocumentModel
                    command.Parameters.AddWithValue("doc_id", id)
                    connection.Open()
                    Using reader = command.ExecuteReader
                        reader.Read()
                        With model
                            .Title = reader("doc_title").ToString
                            .Author = reader("doc_author").ToString
                            .Subject = reader("doc_subject").ToString
                            .Keywords = reader("doc_keywords").ToString
                            .Notes = reader("doc_notes").ToString
                            .Pdf = CType(reader("doc_pdf"), System.Byte())
                            .Category = reader("doc_category").ToString
                            .Flag = Convert.ToInt32(reader("doc_flag"))
                            .TaxYear = reader("doc_tax_year").ToString
                            .Text = reader("doc_text").ToString
                            .SearchTermSnippets = GetSearchTermSnippets(id, searchTerm)
                        End With
                    End Using
                    Return model
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Sub UpdateDocument(id As Integer, model As DocumentModel) Implements IDocumentRepository.UpdateDocument
        If model Is Nothing Then
            Throw New ArgumentNullException(NameOf(model))
        End If
        Dim sql = "update docs set " &
                  "doc_notes = :doc_notes," &
                  "doc_category = :doc_category," &
                  "doc_tax_year = :doc_tax_year," &
                  "doc_flag = :doc_flag," &
                  "doc_text_annotations = :doc_text_annotations," &
                  "doc_text = :doc_text where doc_id = :doc_id"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    With command
                        .Parameters.AddWithValue("doc_notes", model.Notes)
                        .Parameters.AddWithValue("doc_category", model.Category)
                        .Parameters.AddWithValue("doc_tax_year", model.TaxYear)
                        .Parameters.AddWithValue("doc_flag", model.Flag)
                        .Parameters.AddWithValue("doc_text_annotations", model.TextAnnotations)
                        .Parameters.AddWithValue("doc_text", model.Text)
                        .Parameters.AddWithValue("doc_id", id)
                    End With
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Sub

    Public Sub DeleteDocument(id As Integer) Implements IDocumentRepository.DeleteDocument
        Dim sql = "delete from docs where doc_id = :doc_id"
        Try
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    command.Parameters.AddWithValue("doc_id", id)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SQLiteException
            Throw New DbException(ex.Message)
        End Try
    End Sub

    Public Sub TestConnection() Implements IDocumentRepository.TestConnection
        Throw New NotSupportedException
    End Sub

    Public Sub ResetCredential() Implements IDocumentRepository.ResetCredential
        Throw New NotSupportedException
    End Sub

    Protected Overrides Function ExecuteQuery(command As SQLiteCommand) As DataTable
        Using adapter = New SQLiteDataAdapter(command)
            Using table = New DataTable
                table.Locale = CultureInfo.InvariantCulture
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Private Function GetSearchTermSnippets(ByVal id As Integer, ByVal searchTerm As String) As String
        Dim result As String = Nothing
        If searchTerm.Length > 0 Then
            Dim sql = "select snippet(docs_index, 9, '[', ']', '', 32) from docs_index where rowid = :doc_id and " &
                      "docs_index match :doc_dummy"
            Using connection = New SQLiteConnection(ConnectionString)
                Using command = New SQLiteCommand(sql, connection)
                    command.Parameters.AddWithValue("doc_id", id)
                    command.Parameters.AddWithValue("doc_dummy", searchTerm)
                    connection.Open()
                    LoadExtensionLibrary(connection)
                    Using reader = command.ExecuteReader
                        reader.Read()
                        result = reader.Item(0)
                    End Using
                End Using
            End Using
        End If
        Return result
    End Function

    Private Shared Sub LoadExtensionLibrary(ByVal connection As SQLiteConnection)
        connection.EnableExtensions(True)
        connection.LoadExtension("SQLite.Interop.dll", "sqlite3_fts5_init")
    End Sub
End Class
