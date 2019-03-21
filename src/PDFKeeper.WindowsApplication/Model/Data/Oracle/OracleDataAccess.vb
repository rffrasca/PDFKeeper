'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
'* Copyright (C) 2009-2019 Robert F. Frasca
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
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
    "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")> _
Public Class OracleDataAccess
    Implements IDocsDao
    Private dataProvider As IDataProvider
    Private columnsParameter As String
    Private whereParameter As String
    Private groupByParameter As String
    Private havingParameter As String
    Private searchColumns As String = _
        "doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added"
    Private sqlStatement As String

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Public Sub New()
        dataProvider = New OracleDataProvider
    End Sub

    Public ReadOnly Property TotalRecordCount As Integer Implements IDocsDao.TotalRecordCount
        Get
            columnsParameter = "count(*)"
            Return dataProvider.ExecuteScalarQuery(BuildQueryString)
        End Get
    End Property

    Public ReadOnly Property FlaggedRecordCount As Integer Implements IDocsDao.FlaggedRecordCount
        Get
            columnsParameter = "count(doc_flag)"
            whereParameter = "doc_flag = 1"
            Return dataProvider.ExecuteScalarQuery(BuildQueryString)
        End Get
    End Property

    Public Function GetAllAuthors() As DataTable Implements IDocsDao.GetAllAuthors
        columnsParameter = "doc_author,count(doc_author)"
        groupByParameter = "doc_author"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllSubjects() As DataTable Implements IDocsDao.GetAllSubjects
        columnsParameter = "doc_subject,count(doc_subject)"
        groupByParameter = "doc_subject"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllSubjectsByAuthor(author As String) As DataTable Implements IDocsDao.GetAllSubjectsByAuthor
        columnsParameter = "doc_subject"
        whereParameter = "doc_author = q'[" & author & "]'"
        groupByParameter = "doc_subject"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllCategories() As DataTable Implements IDocsDao.GetAllCategories
        columnsParameter = "doc_category,count(doc_category)"
        groupByParameter = "doc_category"
        havingParameter = "count(doc_category) > 0"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllRecordsBySearchString(searchValue As String) As DataTable Implements IDocsDao.GetAllRecordsBySearchString
        columnsParameter = searchColumns
        whereParameter = "(contains(doc_dummy,q'[" & searchValue & "]'))>0"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllRecordsByAuthor(author As String) As DataTable Implements IDocsDao.GetAllRecordsByAuthor
        columnsParameter = searchColumns
        whereParameter = "doc_author = q'[" & author & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllRecordsBySubject(subject As String) As DataTable Implements IDocsDao.GetAllRecordsBySubject
        columnsParameter = searchColumns
        whereParameter = "doc_subject = q'[" & subject & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllRecordsByAuthorAndSubject(author As String, subject As String) As DataTable Implements IDocsDao.GetAllRecordsByAuthorAndSubject
        columnsParameter = searchColumns
        whereParameter = "doc_author = q'[" & author & "]' and doc_subject = q'[" & subject & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllRecordsByCategory(category As String) As DataTable Implements IDocsDao.GetAllRecordsByCategory
        columnsParameter = searchColumns
        whereParameter = "doc_category = q'[" & category & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllRecordsByDateAdded(dateAdded As String) As DataTable Implements IDocsDao.GetAllRecordsByDateAdded
        columnsParameter = searchColumns
        whereParameter = "doc_added like q'[" & dateAdded & "%" & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllRecords() As DataTable Implements IDocsDao.GetAllRecords
        columnsParameter = searchColumns
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllFlaggedRecords() As DataTable Implements IDocsDao.GetAllFlaggedRecords
        columnsParameter = searchColumns
        whereParameter = "doc_flag = 1"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetNotesById(id As Integer) As DataTable Implements IDocsDao.GetNotesById
        columnsParameter = "doc_notes"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetKeywordsById(id As Integer) As DataTable Implements IDocsDao.GetKeywordsById
        columnsParameter = "doc_keywords"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetCategoryById(id As Integer) As DataTable Implements IDocsDao.GetCategoryById
        columnsParameter = "doc_category"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetFlagStateById(id As Integer) As DataTable Implements IDocsDao.GetFlagStateById
        columnsParameter = "doc_flag"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Sub GetPdfById(id As Integer, pdfFile As String) Implements IDocsDao.GetPdfById
        columnsParameter = "doc_pdf"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        dataProvider.ExecuteBlobQuery(BuildQueryString, pdfFile)
    End Sub

    Public Sub CreateRecord(title As String, author As String, subject As String, keywords As String, notes As String, pdfFile As String, category As String, flag As Integer) Implements IDocsDao.CreateRecord
        ' Create the Anonymous PL/SQL block statement for the insert.
        sqlStatement = _
            " begin " & _
            " insert into pdfkeeper.docs values( " & _
            " pdfkeeper.docs_seq.NEXTVAL, " & _
            " q'[" & title & "]', " & _
            " q'[" & author & "]', " & _
            " q'[" & subject & "]', " & _
            " q'[" & keywords & "]', " & _
            " to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'), " & _
            " q'[" & notes & "]', " & _
            " :1, '',q'[" & category & "]'," & flag & ") ;" & _
            " end ;"
        dataProvider.ExecuteBlobInsert(sqlStatement, pdfFile)
    End Sub

    Public Sub UpdateNotesById(id As Integer, notes As String) Implements IDocsDao.UpdateNotesById
        sqlStatement = "update pdfkeeper.docs " & _
            "set doc_notes =q'[" & notes & "]',doc_dummy = '' " & _
            "where doc_id = " & id
        dataProvider.ExecuteNonQuery(sqlStatement)
    End Sub

    Public Sub UpdateCategoryById(id As Integer, category As String) Implements IDocsDao.UpdateCategoryById
        sqlStatement = "update pdfkeeper.docs " & _
            "set doc_category =q'[" & category & "]',doc_dummy = '' " & _
            "where doc_id = " & id
        dataProvider.ExecuteNonQuery(sqlStatement)
    End Sub

    Public Sub UpdateFlagStateById(id As Integer, flag As Integer) Implements IDocsDao.UpdateFlagStateById
        sqlStatement = "update pdfkeeper.docs " & _
            "set doc_flag = " & flag & _
            "where doc_id = " & id
        dataProvider.ExecuteNonQuery(sqlStatement)
    End Sub

    Public Sub DeleteRecordById(id As Integer) Implements IDocsDao.DeleteRecordById
        sqlStatement = "delete from pdfkeeper.docs where doc_id = " & id
        dataProvider.ExecuteNonQuery(sqlStatement)
    End Sub

    Private Function BuildQueryString() As String
        Dim query As String
        query = "select " & columnsParameter & " from pdfkeeper.docs"
        If Not whereParameter Is Nothing Then
            query = query & " where " & whereParameter
        End If
        If Not groupByParameter Is Nothing Then
            query = query & " group by " & groupByParameter
        End If
        If Not havingParameter Is Nothing Then
            query = query & " having " & havingParameter
        End If
        Return query
    End Function
End Class
