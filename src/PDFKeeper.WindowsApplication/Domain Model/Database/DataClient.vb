'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Public Class DataClient
    Implements IDataClient
    Private dataClient As IDataClient = Nothing

    Public Sub New()
        Dim factory As New DataClientFactory(dataClient)
        dataClient = factory.SetDataClient
    End Sub

    Public Sub TestConnection() Implements IDataClient.TestConnection
        dataClient.TestConnection()
    End Sub

    Public Function GetAllAuthors() As DataTable Implements IDataClient.GetAllAuthors
        Return dataClient.GetAllAuthors
    End Function

    Public Function GetAllSubjects() As DataTable Implements IDataClient.GetAllSubjects
        Return dataClient.GetAllSubjects
    End Function

    Public Function GetAllSubjectsByAuthor(author As String) As DataTable Implements IDataClient.GetAllSubjectsByAuthor
        Return dataClient.GetAllSubjectsByAuthor(author)
    End Function

    Public Function GetAllCategories() As DataTable Implements IDataClient.GetAllCategories
        Return dataClient.GetAllCategories
    End Function

    Public Function GetAllRecordsBySearchString(searchValue As String) As DataTable Implements IDataClient.GetAllRecordsBySearchString
        Return dataClient.GetAllRecordsBySearchString(searchValue)
    End Function

    Public Function GetAllRecordsByAuthor(author As String) As DataTable Implements IDataClient.GetAllRecordsByAuthor
        Return dataClient.GetAllRecordsByAuthor(author)
    End Function

    Public Function GetAllRecordsBySubject(subject As String) As DataTable Implements IDataClient.GetAllRecordsBySubject
        Return dataClient.GetAllRecordsBySubject(subject)
    End Function

    Public Function GetAllRecordsByAuthorAndSubject(author As String, subject As String) As DataTable Implements IDataClient.GetAllRecordsByAuthorAndSubject
        Return dataClient.GetAllRecordsByAuthorAndSubject(author, subject)
    End Function

    Public Function GetAllRecordsByCategory(category As String) As DataTable Implements IDataClient.GetAllRecordsByCategory
        Return dataClient.GetAllRecordsByCategory(category)
    End Function

    Public Function GetAllRecordsByDateAdded(dateAdded As String) As DataTable Implements IDataClient.GetAllRecordsByDateAdded
        Return dataClient.GetAllRecordsByDateAdded(dateAdded)
    End Function

    Public Function GetTotalRecordsCount() As Integer Implements IDataClient.GetTotalRecordsCount
        Return dataClient.GetTotalRecordsCount
    End Function

    Public Function GetFlaggedRecordsCount() As Integer Implements IDataClient.GetFlaggedRecordsCount
        Return dataClient.GetFlaggedRecordsCount
    End Function

    Public Function GetAllRecords() As DataTable Implements IDataClient.GetAllRecords
        Return dataClient.GetAllRecords
    End Function

    Public Function GetAllFlaggedRecords() As DataTable Implements IDataClient.GetAllFlaggedRecords
        Return dataClient.GetAllFlaggedRecords
    End Function

    Public Function GetNotesById(id As Integer) As DataTable Implements IDataClient.GetNotesById
        Return dataClient.GetNotesById(id)
    End Function

    Public Function GetKeywordsById(id As Integer) As DataTable Implements IDataClient.GetKeywordsById
        Return dataClient.GetKeywordsById(id)
    End Function

    Public Function GetCategoryById(id As Integer) As DataTable Implements IDataClient.GetCategoryById
        Return dataClient.GetCategoryById(id)
    End Function

    Public Function GetFlagStateById(id As Integer) As DataTable Implements IDataClient.GetFlagStateById
        Return dataClient.GetFlagStateById(id)
    End Function

    Public Sub GetPdfById(id As Integer, pdfFile As String) Implements IDataClient.GetPdfById
        dataClient.GetPdfById(id, pdfFile)
    End Sub

    Public Sub CreateRecord(title As String, author As String, subject As String, keywords As String, notes As String, pdfFile As String, category As String, flag As Integer) Implements IDataClient.CreateRecord
        dataClient.CreateRecord(title, author, subject, keywords, notes, pdfFile, category, flag)
    End Sub

    Public Sub UpdateNotesById(id As Integer, notes As String) Implements IDataClient.UpdateNotesById
        dataClient.UpdateNotesById(id, notes)
    End Sub

    Public Sub UpdateCategoryById(id As Integer, category As String) Implements IDataClient.UpdateCategoryById
        dataClient.UpdateCategoryById(id, category)
    End Sub

    Public Sub UpdateFlagStateById(id As Integer, flag As Integer) Implements IDataClient.UpdateFlagStateById
        dataClient.UpdateFlagStateById(id, flag)
    End Sub

    Public Sub DeleteRecordById(id As Integer) Implements IDataClient.DeleteRecordById
        dataClient.DeleteRecordById(id)
    End Sub
End Class
