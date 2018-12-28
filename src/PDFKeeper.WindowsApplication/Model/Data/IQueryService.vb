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
Public Interface IQueryService
    ReadOnly Property DBDocumentRecordsCount As Integer
    Function GetSearchResultsBySearchString(ByVal searchValue As String) As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetAuthors() As DataTable
    Function GetSearchResultsByAuthor(ByVal author As String) As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetSubjects() As DataTable
    Function GetSearchResultsBySubject(ByVal subject As String) As DataTable
    Function GetSubjectsByAuthor(ByVal author As String) As DataTable
    Function GetSearchResultsByAuthorAndSubject(ByVal author As String, _
                                                ByVal subject As String) As DataTable
    Function GetSearchResultsByDateAdded(ByVal dateAdded As String) As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetAllDBDocumentRecords() As DataTable
    Function GetDocumentNotes(ByVal id As Integer) As DataTable
    Function GetDocumentKeywords(ByVal id As Integer) As DataTable
    Sub GetDocumentPdf(ByVal id As Integer, ByVal pdfFile As String)
End Interface
