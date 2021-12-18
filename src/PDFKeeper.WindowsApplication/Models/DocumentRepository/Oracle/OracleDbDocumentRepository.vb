'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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

Public NotInheritable Class OracleDbDocumentRepository
    Implements IDocumentRepository, IDisposable
    Private ReadOnly provider As New OracleDbDataProvider

    Public Function GetAllAuthors() As DataTable Implements IDocumentRepository.GetAllAuthors
        Dim sqlStatement As String =
            "select doc_author,count(doc_author) " &
            "from pdfkeeper.docs " &
            "group by doc_author"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
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
            "from pdfkeeper.docs " & where & " group by doc_author"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                If subject IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_subject", subject)
                End If
                If category IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_category", category)
                End If
                If taxYear IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllSubjects() As DataTable Implements IDocumentRepository.GetAllSubjects
        Dim sqlStatement As String =
            "select doc_subject,count(doc_subject) " &
            "from pdfkeeper.docs " &
            "group by doc_subject"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllSubjectsByAuthor(author As String) As DataTable Implements IDocumentRepository.GetAllSubjectsByAuthor
        Dim sqlStatement As String =
            "select doc_subject " &
            "from pdfkeeper.docs " &
            "where doc_author = :doc_author " &
            "group by doc_subject"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_author", author)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
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
            "from pdfkeeper.docs " & where & " group by doc_subject"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                If author IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_author", author)
                End If
                If category IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_category", category)
                End If
                If taxYear IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllCategories() As DataTable Implements IDocumentRepository.GetAllCategories
        Dim sqlStatement As String =
            "select doc_category,count(doc_category) " &
            "from pdfkeeper.docs " &
            "group by doc_category having count(doc_category) > 0"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
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
            "from pdfkeeper.docs " & where & " group by doc_category"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                If author IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_author", author)
                End If
                If subject IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_subject", subject)
                End If
                If taxYear IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllTaxYears() As DataTable Implements IDocumentRepository.GetAllTaxYears
        Dim sqlStatement As String =
            "select doc_tax_year,count(doc_tax_year) " &
            "from pdfkeeper.docs " &
            "group by doc_tax_year having count(doc_tax_year) > 0"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
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
            "from pdfkeeper.docs " & where & " group by doc_tax_year"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                If author IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_author", author)
                End If
                If subject IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_subject", subject)
                End If
                If category IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_category", category)
                End If
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllRecordsBySearchText(searchValue As String) As DataTable Implements IDocumentRepository.GetAllRecordsBySearchText
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from pdfkeeper.docs " &
            "where (contains(doc_dummy,:doc_dummy))>0"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_dummy", searchValue)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
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
            "from pdfkeeper.docs " & where
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                If author IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_author", author)
                End If
                If subject IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_subject", subject)
                End If
                If category IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_category", category)
                End If
                If taxYear IsNot Nothing Then
                    oraCommand.Parameters.Add("doc_tax_year", taxYear)
                End If
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllRecordsByDateAdded(dateAdded As String) As DataTable Implements IDocumentRepository.GetAllRecordsByDateAdded
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from pdfkeeper.docs " &
            "where doc_added like :doc_added || '%'"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_added", dateAdded)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllFlaggedRecords() As DataTable Implements IDocumentRepository.GetAllFlaggedRecords
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from pdfkeeper.docs " &
            "where doc_flag = 1"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetAllRecords() As DataTable Implements IDocumentRepository.GetAllRecords
        Dim sqlStatement As String =
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year,doc_added " &
            "from pdfkeeper.docs"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

    Public Function GetFlaggedRecordsCount() As Integer Implements IDocumentRepository.GetFlaggedRecordsCount
        Dim sqlStatement As String =
            "select count(doc_flag) " &
            "from pdfkeeper.docs " &
            "where doc_flag = 1"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Return provider.QueryToObject(oraCommand)
            End Using
        Catch ex As OracleException
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

    Public Function GetFlagStateById(id As Integer) As Int32 Implements IDocumentRepository.GetFlagStateById
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
            "from pdfkeeper.docs " &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_id", id)
                provider.QueryBlobToFile(oraCommand, pdfFile)
            End Using
        Catch ex As OracleException
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
        Dim sqlStatement As String =
            "insert into pdfkeeper.docs values(" &
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
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                Dim fileInfo As New FileInfo(pdfFile)
                Dim blob As Byte() = fileInfo.ToByteArray
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_title", title)
                oraCommand.Parameters.Add("doc_author", author)
                oraCommand.Parameters.Add("doc_subject", subject)
                oraCommand.Parameters.Add("doc_keywords", keywords)
                oraCommand.Parameters.Add("doc_notes", notes)
                oraCommand.Parameters.Add("doc_pdf", OracleDbType.Blob, blob, ParameterDirection.Input)
                oraCommand.Parameters.Add("doc_category", category)
                oraCommand.Parameters.Add("doc_flag", flag)
                oraCommand.Parameters.Add("doc_tax_year", taxYear)
                oraCommand.Parameters.Add("doc_text_annotations", textAnnotations)
                oraCommand.Parameters.Add("doc_text", text)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateNotesById(id As Integer, notes As String) Implements IDocumentRepository.UpdateNotesById
        Dim sqlStatement As String =
            "update pdfkeeper.docs " &
            "set doc_notes = :doc_notes,doc_dummy = ''" &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_notes", notes)
                oraCommand.Parameters.Add("doc_id", id)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateCategoryById(id As Integer, category As String) Implements IDocumentRepository.UpdateCategoryById
        Dim sqlStatement As String =
            "update pdfkeeper.docs " &
            "set doc_category = :doc_category,doc_dummy = '' " &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_category", category)
                oraCommand.Parameters.Add("doc_id", id)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTaxYearById(id As Integer, taxYear As String) Implements IDocumentRepository.UpdateTaxYearById
        Dim sqlStatement As String =
            "update pdfkeeper.docs " &
            "set doc_tax_year = :doc_tax_year,doc_dummy = '' " &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_tax_year", taxYear)
                oraCommand.Parameters.Add("doc_id", id)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateFlagStateById(id As Integer, flag As Integer) Implements IDocumentRepository.UpdateFlagStateById
        Dim sqlStatement As String =
            "update pdfkeeper.docs " &
            "set doc_flag = :doc_flag " &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_flag", flag)
                oraCommand.Parameters.Add("doc_id", id)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTextAnnotationsById(id As Integer,
                                         textAnnotations As String) Implements IDocumentRepository.UpdateTextAnnotationsById
        Dim sqlStatement As String =
            "update pdfkeeper.docs " &
            "set doc_text_annotations = :doc_text_annotations,doc_dummy = '' " &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_text_annotations", textAnnotations)
                oraCommand.Parameters.Add("doc_id", id)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTextById(id As Integer, text As String) Implements IDocumentRepository.UpdateTextById
        Dim sqlStatement As String =
            "update pdfkeeper.docs " &
            "set doc_text = :doc_text,doc_dummy = '' " &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_text", text)
                oraCommand.Parameters.Add("doc_id", id)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Public Sub DeleteRecordById(id As Integer) Implements IDocumentRepository.DeleteRecordById
        Dim sqlStatement As String =
            "delete from pdfkeeper.docs " &
            "where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_id", id)
                provider.ExecuteNonQuery(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Sub

    Private Function GetColumnDataById(ByVal id As Integer, ByVal columnName As String) As DataTable
        Dim sqlStatement As String =
            "select " & columnName &
            " from pdfkeeper.docs" &
            " where doc_id = :doc_id"
        Try
            Using oraCommand As New OracleCommand(sqlStatement, provider.Connection)
                oraCommand.CommandType = CommandType.Text
                oraCommand.BindByName = True
                oraCommand.Parameters.Add("doc_id", id)
                Return provider.QueryToDataTable(oraCommand)
            End Using
        Catch ex As OracleException
            Throw New CustomDbException(ex.Message)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Public Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                provider.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
