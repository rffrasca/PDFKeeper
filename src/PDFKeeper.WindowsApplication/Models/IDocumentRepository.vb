'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Interface IDocumentRepository
    Inherits IDisposable
    Function GetAllAuthors() As DataTable
    Function GetAllAuthorsBySubject(ByVal subject As String) As DataTable
    Function GetAllAuthorsByCategory(ByVal category As String) As DataTable
    Function GetAllAuthorsBySubjectAndCategory(ByVal subject As String,
                                               ByVal category As String)
    Function GetAllSubjects() As DataTable
    Function GetAllSubjectsByAuthor(ByVal author As String) As DataTable
    Function GetAllSubjectsByCategory(ByVal category As String)
    Function GetAllSubjectsByAuthorAndCategory(ByVal author As String,
                                               ByVal category As String)
    Function GetAllCategories() As DataTable
    Function GetAllCategoriesByAuthor(ByVal author As String)
    Function GetAllCategoriesBySubject(ByVal subject As String)
    Function GetAllCategoriesByAuthorAndSubject(ByVal author As String,
                                                ByVal subject As String)
    Function GetAllRecordsBySearchText(ByVal searchValue As String) As DataTable
    Function GetAllRecordsByAuthorSubjectAndCategory(ByVal author As String,
                                                     ByVal subject As String,
                                                     ByVal category As String) As DataTable
    Function GetAllRecordsByDateAdded(ByVal dateAdded As String) As DataTable
    Function GetAllFlaggedRecords() As DataTable
    Function GetAllRecords() As DataTable
    Function GetFlaggedRecordsCount() As Integer
    Function GetNotesById(ByVal id As Integer) As DataTable
    Function GetKeywordsById(ByVal id As Integer) As DataTable
    Function GetCategoryById(ByVal id As Integer) As DataTable
    Function GetFlagStateById(ByVal id As Integer) As DataTable
    Sub GetPdfById(ByVal id As Integer, ByVal pdfFile As String)
    Sub CreateRecord(ByVal title As String,
                     ByVal author As String,
                     ByVal subject As String,
                     ByVal keywords As String,
                     ByVal notes As String,
                     ByVal pdfFile As String,
                     ByVal category As String,
                     ByVal flag As Integer)
    Sub UpdateNotesById(ByVal id As Integer, ByVal notes As String)
    Sub UpdateCategoryById(ByVal id As Integer, ByVal category As String)
    Sub UpdateFlagStateById(ByVal id As Integer, ByVal flag As Integer)
    Sub DeleteRecordById(ByVal id As Integer)
End Interface
