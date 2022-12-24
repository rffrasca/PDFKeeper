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
Imports System.Globalization
Imports System.IO
Imports Oracle.ManagedDataAccess.Client
Imports PDFKeeper.Domain

Public Class DocumentRepositoryOracle
    Inherits DbRepositoryBase(Of OracleCommand)
    Implements IDocumentRepository
    Private Shared credential As OracleCredential

    Public Sub New()
        If credential Is Nothing Then
            credential = New OracleCredential(DbSession.UserName, DbSession.Password)
            If DbSession.OracleWalletPath IsNot Nothing Then
                If OracleConfiguration.WalletLocation.Length = 0 Then
                    OracleConfiguration.TnsAdmin = DbSession.OracleWalletPath
                    OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin
                End If
            End If
        End If
    End Sub

    Protected Overrides ReadOnly Property ConnectionString As String
        Get
            Return "Data Source=" + DbSession.DataSource + ";Pooling=True;Connection Timeout=60"
        End Get
    End Property

    Public Function ListDocuments(choice As IDocumentRepository.DocumentListAction) As DataTable Implements IDocumentRepository.ListDocuments
        Dim sql As String
        If choice = IDocumentRepository.DocumentListAction.Flagged Then
            sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
                  "from pdfkeeper.docs where doc_flag = 1"
        ElseIf choice = IDocumentRepository.DocumentListAction.All Then
            sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added from pdfkeeper.docs"
        Else
            Throw New ArgumentException(Nothing, NameOf(choice))
        End If
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListDocuments(choice As IDocumentRepository.DocumentListAction, value As String) As DataTable Implements IDocumentRepository.ListDocuments
        Dim sql As String
        If choice = IDocumentRepository.DocumentListAction.BySearchTerm Then
            sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
                  "from pdfkeeper.docs where (contains(doc_dummy,:doc_dummy))>0"
        ElseIf choice = IDocumentRepository.DocumentListAction.ByDateAdded Then
            sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
                  "from pdfkeeper.docs where doc_added like :doc_added || '%'"
        Else
            Throw New ArgumentException(Nothing, NameOf(choice))
        End If
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    If choice = IDocumentRepository.DocumentListAction.BySearchTerm Then
                        command.Parameters.Add("doc_dummy", value)
                    ElseIf choice = IDocumentRepository.DocumentListAction.ByDateAdded Then
                        command.Parameters.Add("doc_added", value)
                    End If
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
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
        Dim sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
                  "from pdfkeeper.docs " & where
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    If filter.Author IsNot Nothing Then
                        command.Parameters.Add("doc_author", filter.Author)
                    End If
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.Add("doc_subject", filter.Subject)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.Add("doc_category", filter.Category)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.Add("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListAuthors() As DataTable Implements IDocumentRepository.ListAuthors
        Dim sql = "select doc_author,count(doc_author) from pdfkeeper.docs group by doc_author"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
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
        Dim sql = "select doc_author from pdfkeeper.docs " & where & " group by doc_author"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.Add("doc_subject", filter.Subject)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.Add("doc_category", filter.Category)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.Add("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListSubjects() As DataTable Implements IDocumentRepository.ListSubjects
        Dim sql = "select doc_subject,count(doc_subject) from pdfkeeper.docs group by doc_subject"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListSubjects(author As String) As DataTable Implements IDocumentRepository.ListSubjects
        Dim sql = "select doc_subject from pdfkeeper.docs where doc_author = :doc_author group by doc_subject"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    command.Parameters.Add("doc_author", author)
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
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
        Dim sql = "select doc_subject from pdfkeeper.docs " & where & " group by doc_subject"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    If filter.Author IsNot Nothing Then
                        command.Parameters.Add("doc_author", filter.Author)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.Add("doc_category", filter.Category)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.Add("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListCategories() As DataTable Implements IDocumentRepository.ListCategories
        Dim sql = "select doc_category,count(doc_category) from pdfkeeper.docs " &
                  "group by doc_category having count(doc_category) > 0"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
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
        Dim sql = "select doc_category from pdfkeeper.docs " & where & " group by doc_category"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    If filter.Author IsNot Nothing Then
                        command.Parameters.Add("doc_author", filter.Author)
                    End If
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.Add("doc_subject", filter.Subject)
                    End If
                    If filter.TaxYear IsNot Nothing Then
                        command.Parameters.Add("doc_tax_year", filter.TaxYear)
                    End If
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Function ListTaxYears() As DataTable Implements IDocumentRepository.ListTaxYears
        Dim sql = "select doc_tax_year,count(doc_tax_year) from pdfkeeper.docs " &
                  "group by doc_tax_year having count(doc_tax_year) > 0"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
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
        Dim sql = "select doc_tax_year from pdfkeeper.docs " & where & " group by doc_tax_year"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    If filter.Author IsNot Nothing Then
                        command.Parameters.Add("doc_author", filter.Author)
                    End If
                    If filter.Subject IsNot Nothing Then
                        command.Parameters.Add("doc_subject", filter.Subject)
                    End If
                    If filter.Category IsNot Nothing Then
                        command.Parameters.Add("doc_category", filter.Category)
                    End If
                    connection.Open()
                    Return ExecuteQuery(command)
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Sub CreateDocument(model As DocumentModel) Implements IDocumentRepository.CreateDocument
        If model Is Nothing Then
            Throw New ArgumentNullException(NameOf(model))
        End If
        Dim sql = "insert into pdfkeeper.docs values(" &
                  "pdfkeeper.docs_seq.NEXTVAL," &
                  ":doc_title," &
                  ":doc_author," &
                  ":doc_subject," &
                  ":doc_keywords," &
                  "to_char(sysdate,'YYYY-MM-DD HH24:MI:SS')," &
                  ":doc_notes," &
                  ":doc_pdf," &
                  "''," &
                  ":doc_category," &
                  ":doc_flag," &
                  ":doc_tax_year," &
                  ":doc_text_annotations," &
                  ":doc_text)"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    With command
                        .BindByName = True
                        .Parameters.Add("doc_title", model.Title)
                        .Parameters.Add("doc_author", model.Author)
                        .Parameters.Add("doc_subject", model.Subject)
                        .Parameters.Add("doc_keywords", model.Keywords)
                        .Parameters.Add("doc_notes", model.Notes)
                        .Parameters.Add("doc_pdf", OracleDbType.Blob, model.Pdf, ParameterDirection.Input)
                        .Parameters.Add("doc_category", model.Category)
                        .Parameters.Add("doc_flag", model.Flag)
                        .Parameters.Add("doc_tax_year", model.TaxYear)
                        .Parameters.Add("doc_text_annotations", model.TextAnnotations)
                        .Parameters.Add("doc_text", model.Text)
                    End With
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Sub

    Public Function ReadDocument(id As Integer, searchTerm As String) As DocumentModel Implements IDocumentRepository.ReadDocument
        If searchTerm Is Nothing Then
            searchTerm = String.Empty
        End If
        Dim sql = "select doc_title,doc_author,doc_subject,doc_keywords,doc_notes,doc_pdf,doc_category," &
                  "doc_flag,doc_tax_year,doc_text from pdfkeeper.docs where doc_id = :doc_id"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    Dim model = New DocumentModel
                    command.BindByName = True
                    command.Parameters.Add("doc_id", id)
                    connection.Open()
                    Using reader = command.ExecuteReader()
                        reader.Read()
                        With model
                            .Title = reader("doc_title").ToString
                            .Author = reader("doc_author").ToString
                            .Subject = reader("doc_subject").ToString
                            .Keywords = reader("doc_keywords").ToString
                            .Notes = reader("doc_notes").ToString
                            .Category = reader("doc_category").ToString
                            .Flag = Convert.ToInt32(reader("doc_flag"))
                            .TaxYear = reader("doc_tax_year").ToString
                            .Text = reader("doc_text").ToString
                            .SearchTermSnippets = GetSearchTermSnippets(id, searchTerm)
                        End With
                        Dim blob = reader.GetOracleBlob(5)
                        Using memoryStream = New MemoryStream(blob.Value)
                            model.Pdf = memoryStream.ToArray
                        End Using
                    End Using
                    Return model
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Function

    Public Sub UpdateDocument(id As Integer, model As DocumentModel) Implements IDocumentRepository.UpdateDocument
        If model Is Nothing Then
            Throw New ArgumentNullException(NameOf(model))
        End If
        Dim sql = "update pdfkeeper.docs set " &
                  "doc_notes = :doc_notes," &
                  "doc_dummy = ''," &
                  "doc_category = :doc_category," &
                  "doc_tax_year = :doc_tax_year," &
                  "doc_flag = :doc_flag," &
                  "doc_text_annotations = :doc_text_annotations," &
                  "doc_text = :doc_text where doc_id = :doc_id"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    With command
                        .BindByName = True
                        .Parameters.Add("doc_notes", model.Notes)
                        .Parameters.Add("doc_category", model.Category)
                        .Parameters.Add("doc_tax_year", model.TaxYear)
                        .Parameters.Add("doc_flag", model.Flag)
                        .Parameters.Add("doc_text_annotations", model.TextAnnotations)
                        .Parameters.Add("doc_text", model.Text)
                        .Parameters.Add("doc_id", id)
                    End With
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Sub

    Public Sub DeleteDocument(id As Integer) Implements IDocumentRepository.DeleteDocument
        Dim sql = "delete from pdfkeeper.docs where doc_id = :doc_id"
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.BindByName = True
                    command.Parameters.Add("doc_id", id)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As OracleException
            Throw New DbException(ex.Message)
        End Try
    End Sub

    Public Sub TestConnection() Implements IDocumentRepository.TestConnection
        Try
            Using connection = New OracleConnection(ConnectionString, credential)
                connection.Open()
            End Using
        Catch ex As OracleException
            credential = Nothing
            Throw New DbException(ex.Message)
        End Try
    End Sub

    Public Sub ResetCredential() Implements IDocumentRepository.ResetCredential
        credential = Nothing
    End Sub

    Protected Overrides Function ExecuteQuery(command As OracleCommand) As DataTable
        Using adapter = New OracleDataAdapter(command)
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
            Dim sql = "select ctx_doc.snippet('pdfkeeper.docs_idx', :doc_id, :doc_dummy, '[', ']'," &
                      ":translate, '||') from dual"
            Using connection = New OracleConnection(ConnectionString, credential)
                Using command = New OracleCommand(sql, connection)
                    command.Parameters.Add("doc_id", id)
                    command.Parameters.Add("doc_dummy", searchTerm)
                    command.Parameters.Add("translate", OracleDbType.Boolean, False, ParameterDirection.Input)
                    connection.Open()
                    Using reader = command.ExecuteReader
                        reader.Read()
                        result = reader.Item(0)
                    End Using
                End Using
            End Using
        End If
        Return result
    End Function
End Class
