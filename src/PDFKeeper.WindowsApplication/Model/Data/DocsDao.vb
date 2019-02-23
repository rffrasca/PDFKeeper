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
Public Class DocsDao
    Implements IDocsDao
    Private dataAccess As IDocsDao = Nothing
    Private connnectionProperties As DatabaseConnectionProperties = _
        DatabaseConnectionProperties.Instance

    Public Sub New()
        If connnectionProperties.DatabaseSystem = "Oracle" Then
            dataAccess = New OracleDataAccess
        End If
    End Sub

    Public ReadOnly Property TotalRecordCount As Integer Implements IDocsDao.TotalRecordCount
        Get
            Return dataAccess.TotalRecordCount
        End Get
    End Property

    Public ReadOnly Property FlaggedRecordCount As Integer Implements IDocsDao.FlaggedRecordCount
        Get
            Return dataAccess.FlaggedRecordCount
        End Get
    End Property

    Public Function GetAllAuthors() As DataTable Implements IDocsDao.GetAllAuthors
        Return dataAccess.GetAllAuthors
    End Function

    Public Function GetAllSubjects() As DataTable Implements IDocsDao.GetAllSubjects
        Return dataAccess.GetAllSubjects
    End Function

    Public Function GetAllSubjectsByAuthor(author As String) As DataTable Implements IDocsDao.GetAllSubjectsByAuthor
        Return dataAccess.GetAllSubjectsByAuthor(author)
    End Function

    Public Function GetAllRecordsBySearchString(searchValue As String) As DataTable Implements IDocsDao.GetAllRecordsBySearchString
        Return dataAccess.GetAllRecordsBySearchString(searchValue)
    End Function

    Public Function GetAllRecordsByAuthor(author As String) As DataTable Implements IDocsDao.GetAllRecordsByAuthor
        Return dataAccess.GetAllRecordsByAuthor(author)
    End Function

    Public Function GetAllRecordsBySubject(subject As String) As DataTable Implements IDocsDao.GetAllRecordsBySubject
        Return dataAccess.GetAllRecordsBySubject(subject)
    End Function

    Public Function GetAllRecordsByAuthorAndSubject(author As String, subject As String) As DataTable Implements IDocsDao.GetAllRecordsByAuthorAndSubject
        Return dataAccess.GetAllRecordsByAuthorAndSubject(author, subject)
    End Function

    Public Function GetAllRecordsByDateAdded(dateAdded As String) As DataTable Implements IDocsDao.GetAllRecordsByDateAdded
        Return dataAccess.GetAllRecordsByDateAdded(dateAdded)
    End Function

    Public Function GetAllRecords() As DataTable Implements IDocsDao.GetAllRecords
        Return dataAccess.GetAllRecords
    End Function

    Public Function GetAllFlaggedRecords() As DataTable Implements IDocsDao.GetAllFlaggedRecords
        Return dataAccess.GetAllFlaggedRecords
    End Function

    Public Function GetNotesById(id As Integer) As DataTable Implements IDocsDao.GetNotesById
        Return dataAccess.GetNotesById(id)
    End Function

    Public Function GetKeywordsById(id As Integer) As DataTable Implements IDocsDao.GetKeywordsById
        Return dataAccess.GetKeywordsById(id)
    End Function

    Public Function GetFlagStateById(id As Integer) As DataTable Implements IDocsDao.GetFlagStateById
        Return dataAccess.GetFlagStateById(id)
    End Function

    Public Sub GetPdfById(id As Integer, pdfFile As String) Implements IDocsDao.GetPdfById
        dataAccess.GetPdfById(id, pdfFile)
    End Sub

    Public Sub CreateRecord(title As String, author As String, subject As String, keywords As String, notes As String, pdfFile As String, flag As Integer) Implements IDocsDao.CreateRecord
        dataAccess.CreateRecord(title, author, subject, keywords, notes, pdfFile, flag)
    End Sub

    Public Sub UpdateNotesById(id As Integer, notes As String) Implements IDocsDao.UpdateNotesById
        dataAccess.UpdateNotesById(id, notes)
    End Sub

    Public Sub UpdateFlagStateById(id As Integer, flag As Integer) Implements IDocsDao.UpdateFlagStateById
        dataAccess.UpdateFlagStateById(id, flag)
    End Sub

    Public Sub DeleteRecordById(id As Integer) Implements IDocsDao.DeleteRecordById
        dataAccess.DeleteRecordById(id)
    End Sub
End Class
