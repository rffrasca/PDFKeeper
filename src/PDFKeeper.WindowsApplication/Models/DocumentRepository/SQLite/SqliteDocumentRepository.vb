'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Public NotInheritable Class SqliteDocumentRepository
    Implements IDocumentRepository, IDisposable
    Private ReadOnly provider As New SqliteDataProvider
    Private disposedValue As Boolean

    Public Function GetAllAuthors() As DataTable Implements IDocumentRepository.GetAllAuthors
        Dim sqlStatement As String =
            "select doc_author,count(doc_author) " &
            "from docs " &
            "group by doc_author"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllAuthorsBySubjectCategoryAndTaxYear(subject As String,
                                                             category As String,
                                                             taxYear As String) As DataTable Implements IDocumentRepository.GetAllAuthorsBySubjectCategoryAndTaxYear
        Dim where As String = "where "
        Dim andNeeded As Boolean = False
        If subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
            andNeeded = True
        End If
        If taxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sqlStatement As String =
            "select doc_author " &
            "from docs " & where & " group by doc_author"
        Try
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                If subject IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_subject", subject)
                End If
                If category IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_category", category)
                End If
                If taxYear IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllSubjects() As DataTable Implements IDocumentRepository.GetAllSubjects
        Dim sqlStatement As String =
            "select doc_subject,count(doc_subject) " &
            "from docs " &
            "group by doc_subject"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllSubjectsByAuthor(author As String) As DataTable Implements IDocumentRepository.GetAllSubjectsByAuthor
        Dim sqlStatement As String =
            "select doc_subject " &
            "from docs " &
            "where doc_author = :doc_author " &
            "group by doc_subject"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_author", author)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllSubjectsByAuthorCategoryAndTaxYear(author As String,
                                                             category As String,
                                                             taxYear As String) As DataTable Implements IDocumentRepository.GetAllSubjectsByAuthorCategoryAndTaxYear
        Dim where As String = "where "
        Dim andNeeded As Boolean = False
        If author IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_author = :doc_author"
            Else
                where &= "doc_author = :doc_author"
            End If
            andNeeded = True
        End If
        If category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
            andNeeded = True
        End If
        If taxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sqlStatement As String =
            "select doc_subject " &
            "from docs " & where & " group by doc_subject"
        Try
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                If author IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_author", author)
                End If
                If category IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_category", category)
                End If
                If taxYear IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllCategories() As DataTable Implements IDocumentRepository.GetAllCategories
        Dim sqlStatement As String =
            "select doc_category,count(doc_category) " &
            "from docs " &
            "group by doc_category having count(doc_category) > 0"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllCategoriesByAuthorSubjectAndTaxYear(author As String,
                                                              subject As String,
                                                              taxYear As String) As DataTable Implements IDocumentRepository.GetAllCategoriesByAuthorSubjectAndTaxYear
        Dim where As String = "where "
        Dim andNeeded As Boolean = False
        If author IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_author = :doc_author"
            Else
                where &= "doc_author = :doc_author"
            End If
            andNeeded = True
        End If
        If subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If taxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sqlStatement As String =
            "select doc_category " &
            "from docs " & where & " group by doc_category"
        Try
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                If author IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_author", author)
                End If
                If subject IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_subject", subject)
                End If
                If taxYear IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllTaxYears() As DataTable Implements IDocumentRepository.GetAllTaxYears
        Dim sqlStatement As String =
            "select doc_tax_year,count(doc_tax_year) " &
            "from docs " &
            "group by doc_tax_year having count(doc_tax_year) > 0"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllTaxYearsByAuthorSubjectAndCategory(author As String,
                                                             subject As String,
                                                             category As String) As DataTable Implements IDocumentRepository.GetAllTaxYearsByAuthorSubjectAndCategory
        Dim where As String = "where "
        Dim andNeeded As Boolean = False
        If author IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_author = :doc_author"
            Else
                where &= "doc_author = :doc_author"
            End If
            andNeeded = True
        End If
        If subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
        End If
        Dim sqlStatement As String =
            "select doc_tax_year " &
            "from docs " & where & " group by doc_tax_year"
        Try
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                If author IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_author", author)
                End If
                If subject IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_subject", subject)
                End If
                If category IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_category", category)
                End If
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllRecordsBySearchText(searchValue As String) As DataTable Implements IDocumentRepository.GetAllRecordsBySearchText
        Dim sqlStatement As String =
            "select rowid,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from docs_index " &
            "where docs_index match :doc_dummy"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_dummy", searchValue)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllRecordsByAuthorSubjectCategoryAndTaxYear(author As String,
                                                                   subject As String,
                                                                   category As String,
                                                                   taxYear As String) As DataTable Implements IDocumentRepository.GetAllRecordsByAuthorSubjectCategoryAndTaxYear
        Dim where As String = "where "
        Dim andNeeded As Boolean = False
        If author IsNot Nothing Then
            where &= "doc_author = :doc_author"
            andNeeded = True
        End If
        If subject IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_subject = :doc_subject"
            Else
                where &= "doc_subject = :doc_subject"
            End If
            andNeeded = True
        End If
        If category IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_category = :doc_category"
            Else
                where &= "doc_category = :doc_category"
            End If
            andNeeded = True
        End If
        If taxYear IsNot Nothing Then
            If andNeeded Then
                where &= " and doc_tax_year = :doc_tax_year"
            Else
                where &= "doc_tax_year = :doc_tax_year"
            End If
        End If
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from docs " & where
        Try
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                If author IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_author", author)
                End If
                If subject IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_subject", subject)
                End If
                If category IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_category", category)
                End If
                If taxYear IsNot Nothing Then
                    sqlCommand.Parameters.AddWithValue("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllRecordsByDateAdded(dateAdded As String) As DataTable Implements IDocumentRepository.GetAllRecordsByDateAdded
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from docs " &
            "where doc_added like :doc_added || '%'"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_added", dateAdded)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllFlaggedRecords() As DataTable Implements IDocumentRepository.GetAllFlaggedRecords
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from docs " &
            "where doc_flag = 1"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllRecords() As DataTable Implements IDocumentRepository.GetAllRecords
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from docs"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetFlaggedRecordsCount() As Integer Implements IDocumentRepository.GetFlaggedRecordsCount
        Dim sqlStatement As String =
            "select count(doc_flag) " &
            "from docs " &
            "where doc_flag = 1"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                Return provider.QueryToObject(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetTitleById(id As Integer) As String Implements IDocumentRepository.GetTitleById
        Return Convert.ToString(GetColumnDataById(id, "doc_title").Rows(0)("doc_title"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Function GetAuthorById(id As Integer) As String Implements IDocumentRepository.GetAuthorById
        Return Convert.ToString(GetColumnDataById(id, "doc_author").Rows(0)("doc_author"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Function GetSubjectById(id As Integer) As String Implements IDocumentRepository.GetSubjectById
        Return Convert.ToString(GetColumnDataById(id, "doc_subject").Rows(0)("doc_subject"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Function GetNotesById(id As Integer) As String Implements IDocumentRepository.GetNotesById
        Return Convert.ToString(GetColumnDataById(id, "doc_notes").Rows(0)("doc_notes"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Function GetKeywordsById(id As Integer) As String Implements IDocumentRepository.GetKeywordsById
        Return Convert.ToString(GetColumnDataById(id, "doc_keywords").Rows(0)("doc_keywords"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Function GetCategoryById(id As Integer) As String Implements IDocumentRepository.GetCategoryById
        Return Convert.ToString(GetColumnDataById(id, "doc_category").Rows(0)("doc_category"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Function GetTaxYearById(id As Integer) As String Implements IDocumentRepository.GetTaxYearById
        Return Convert.ToString(GetColumnDataById(id, "doc_tax_year").Rows(0)("doc_tax_year"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Function GetFlagStateById(id As Integer) As Integer Implements IDocumentRepository.GetFlagStateById
        Return Convert.ToInt32(GetColumnDataById(id, "doc_flag").Rows(0)("doc_flag"),
                               CultureInfo.CurrentCulture)
    End Function

    Public Function GetTextById(id As Integer) As String Implements IDocumentRepository.GetTextById
        Return Convert.ToString(GetColumnDataById(id, "doc_text").Rows(0)("doc_text"),
                                CultureInfo.CurrentCulture)
    End Function

    Public Sub GetPdfById(id As Integer, pdfFile As String) Implements IDocumentRepository.GetPdfById
        Dim sqlStatement As String =
            "select doc_pdf " &
            "from docs " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.QueryBlobToFile(sqlCommand, pdfFile)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub CreateRecord(title As String,
                            author As String,
                            subject As String,
                            keywords As String,
                            notes As String,
                            pdfFile As String,
                            category As String,
                            flag As Integer,
                            taxYear As String,
                            textAnnotations As String,
                            text As String) Implements IDocumentRepository.CreateRecord
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss",
                                                            CultureInfo.CurrentCulture)
        Dim sqlStatement As String =
            "insert into docs values(null," &
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
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                Dim fileInfo As New FileInfo(pdfFile)
                Dim blob As Byte() = fileInfo.ToByteArray
                sqlCommand.Parameters.AddWithValue(":doc_title", title)
                sqlCommand.Parameters.AddWithValue(":doc_author", author)
                sqlCommand.Parameters.AddWithValue(":doc_subject", subject)
                sqlCommand.Parameters.AddWithValue(":doc_keywords", keywords)
                sqlCommand.Parameters.AddWithValue(":doc_added", dateTimeStamp)
                sqlCommand.Parameters.AddWithValue(":doc_notes", notes)
                sqlCommand.Parameters.Add(":doc_pdf", DbType.Binary).Value = blob
                sqlCommand.Parameters.AddWithValue(":doc_category", category)
                sqlCommand.Parameters.AddWithValue(":doc_flag", flag)
                sqlCommand.Parameters.AddWithValue(":doc_tax_year", taxYear)
                sqlCommand.Parameters.AddWithValue(":doc_text_annotations", textAnnotations)
                sqlCommand.Parameters.AddWithValue(":doc_text", text)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateNotesById(id As Integer,
                               notes As String) Implements IDocumentRepository.UpdateNotesById
        Dim sqlStatement As String =
            "update docs " &
            "set doc_notes = :doc_notes " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_notes", notes)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateCategoryById(id As Integer,
                                  category As String) Implements IDocumentRepository.UpdateCategoryById
        Dim sqlStatement As String =
            "update docs " &
            "set doc_category = :doc_category " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_category", category)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTaxYearById(id As Integer,
                                 taxYear As String) Implements IDocumentRepository.UpdateTaxYearById
        Dim sqlStatement As String =
            "update docs " &
            "set doc_tax_year = :doc_tax_year " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_tax_year", taxYear)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateFlagStateById(id As Integer,
                                   flag As Integer) Implements IDocumentRepository.UpdateFlagStateById
        Dim sqlStatement As String =
            "update docs " &
            "set doc_flag = :doc_flag " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_flag", flag)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTextAnnotationsById(id As Integer,
                                         textAnnotations As String) Implements IDocumentRepository.UpdateTextAnnotationsById
        Dim sqlStatement As String =
            "update docs " &
            "set doc_text_annotations = :doc_text_annotations " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_text_annotations", textAnnotations)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTextById(id As Integer,
                              text As String) Implements IDocumentRepository.UpdateTextById
        Dim sqlStatement As String =
            "update docs " &
            "set doc_text = :doc_text " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_text", text)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub DeleteRecordById(id As Integer) Implements IDocumentRepository.DeleteRecordById
        Dim sqlStatement As String =
            "delete from docs " &
            "where doc_id = :doc_id"
        Try
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                provider.ExecuteNonQuery(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Private Function GetColumnDataById(ByVal id As Integer,
                                       ByVal columnName As String) As DataTable
        Dim sqlStatement As String =
            "select " & columnName &
            " from docs" &
            " where doc_id = :doc_id"
        Try
#Disable Warning CA2100 ' Review SQL queries for security vulnerabilities
            Using sqlCommand As New SQLiteCommand(sqlStatement, provider.Connection)
#Enable Warning CA2100 ' Review SQL queries for security vulnerabilities
                sqlCommand.CommandType = CommandType.Text
                sqlCommand.Parameters.AddWithValue("doc_id", id)
                Return provider.QueryToDataTable(sqlCommand)
            End Using
        Catch ex As SQLiteException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Private Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                provider.Dispose()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
