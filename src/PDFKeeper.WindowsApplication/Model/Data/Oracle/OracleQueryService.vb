'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
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
Public Class OracleQueryService
    Implements IQueryService
    Private dataProvider As IDataProvider
    Private columnsParameter As String
    Private whereParameter As String
    Private groupByParameter As String
    Private searchColumns As String = _
        "doc_id,doc_title,doc_author,doc_subject,doc_added"

    Public Sub New()
        DataProviderHelper.SetDataProvider(dataProvider)
    End Sub

    Public ReadOnly Property DBDocumentRecordsCount As Integer Implements IQueryService.DBDocumentRecordsCount
        Get
            columnsParameter = "count(*)"
            Return dataProvider.ExecuteScalarQuery(BuildQueryString)
        End Get
    End Property

    Public Function GetSearchResultsBySearchString(searchValue As String) As DataTable Implements IQueryService.GetSearchResultsBySearchString
        columnsParameter = searchColumns
        whereParameter = "(contains(doc_dummy,q'[" & searchValue & "]'))>0"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAuthors() As DataTable Implements IQueryService.GetAuthors
        columnsParameter = "doc_author,count(doc_author)"
        groupByParameter = "doc_author"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetSearchResultsByAuthor(author As String) As DataTable Implements IQueryService.GetSearchResultsByAuthor
        columnsParameter = searchColumns
        whereParameter = "doc_author = q'[" & author & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetSubjects() As DataTable Implements IQueryService.GetSubjects
        columnsParameter = "doc_subject,count(doc_subject)"
        groupByParameter = "doc_subject"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetSearchResultsBySubject(subject As String) As DataTable Implements IQueryService.GetSearchResultsBySubject
        columnsParameter = searchColumns
        whereParameter = "doc_subject = q'[" & subject & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetSubjectsByAuthor(author As String) As DataTable Implements IQueryService.GetSubjectsByAuthor
        columnsParameter = "doc_subject"
        whereParameter = "doc_author = q'[" & author & "]'"
        groupByParameter = "doc_subject"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetSearchResultsByAuthorAndSubject(author As String, _
                                                       subject As String) As DataTable Implements IQueryService.GetSearchResultsByAuthorAndSubject
        columnsParameter = searchColumns
        whereParameter = "doc_author = q'[" & author & "]' and doc_subject = q'[" & subject & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetSearchResultsByDateAdded(dateAdded As String) As DataTable Implements IQueryService.GetSearchResultsByDateAdded
        columnsParameter = searchColumns
        whereParameter = "doc_added like q'[" & dateAdded & "%" & "]'"
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetAllDBDocumentRecords() As DataTable Implements IQueryService.GetAllDBDocumentRecords
        columnsParameter = searchColumns
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetDocumentNotes(id As Integer) As DataTable Implements IQueryService.GetDocumentNotes
        columnsParameter = "doc_notes"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Function GetDocumentKeywords(id As Integer) As DataTable Implements IQueryService.GetDocumentKeywords
        columnsParameter = "doc_keywords"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        Return dataProvider.ExecuteQuery(BuildQueryString)
    End Function

    Public Sub GetDocumentPdf(id As Integer, _
                              pdfFile As String) Implements IQueryService.GetDocumentPdf
        columnsParameter = "doc_pdf"
        whereParameter = "doc_id = " & id.ToString(CultureInfo.InvariantCulture)
        dataProvider.ExecuteBlobQuery(BuildQueryString, pdfFile)
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
        Return query
    End Function
End Class
