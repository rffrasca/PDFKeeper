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
Public NotInheritable Class DocumentRepository
    Implements IDocumentRepository, IDisposable
    Private repository As IDocumentRepository = Nothing

    Public Sub New()
        If DbInstance.DbManagementSystem = "Oracle Database" Then
            repository = New OracleDbDocumentRepository
        End If
    End Sub

    Public Function GetAllAuthors() As DataTable Implements IDocumentRepository.GetAllAuthors
        Return repository.GetAllAuthors
    End Function

    Public Function GetAllSubjects() As DataTable Implements IDocumentRepository.GetAllSubjects
        Return repository.GetAllSubjects
    End Function

    Public Function GetAllSubjectsByAuthor(author As String) As DataTable Implements IDocumentRepository.GetAllSubjectsByAuthor
        Return repository.GetAllSubjectsByAuthor(author)
    End Function

    Public Function GetAllCategories() As DataTable Implements IDocumentRepository.GetAllCategories
        Return repository.GetAllCategories
    End Function

    Public Function GetAllRecordsBySearchString(searchValue As String) As DataTable Implements IDocumentRepository.GetAllRecordsBySearchString
        Return repository.GetAllRecordsBySearchString(searchValue)
    End Function

    Public Function GetAllRecordsByAuthor(author As String) As DataTable Implements IDocumentRepository.GetAllRecordsByAuthor
        Return repository.GetAllRecordsByAuthor(author)
    End Function

    Public Function GetAllRecordsBySubject(subject As String) As DataTable Implements IDocumentRepository.GetAllRecordsBySubject
        Return repository.GetAllRecordsBySubject(subject)
    End Function

    Public Function GetAllRecordsByAuthorAndSubject(author As String, subject As String) As DataTable Implements IDocumentRepository.GetAllRecordsByAuthorAndSubject
        Return repository.GetAllRecordsByAuthorAndSubject(author, subject)
    End Function

    Public Function GetAllRecordsByCategory(category As String) As DataTable Implements IDocumentRepository.GetAllRecordsByCategory
        Return repository.GetAllRecordsByCategory(category)
    End Function

    Public Function GetAllRecordsByDateAdded(dateAdded As String) As DataTable Implements IDocumentRepository.GetAllRecordsByDateAdded
        Return repository.GetAllRecordsByDateAdded(dateAdded)
    End Function

    Public Function GetTotalRecordsCount() As Integer Implements IDocumentRepository.GetTotalRecordsCount
        Return repository.GetTotalRecordsCount
    End Function

    Public Function GetFlaggedRecordsCount() As Integer Implements IDocumentRepository.GetFlaggedRecordsCount
        Return repository.GetFlaggedRecordsCount
    End Function

    Public Function GetAllRecords() As DataTable Implements IDocumentRepository.GetAllRecords
        Return repository.GetAllRecords
    End Function

    Public Function GetAllFlaggedRecords() As DataTable Implements IDocumentRepository.GetAllFlaggedRecords
        Return repository.GetAllFlaggedRecords
    End Function

    Public Function GetNotesById(id As Integer) As DataTable Implements IDocumentRepository.GetNotesById
        Return repository.GetNotesById(id)
    End Function

    Public Function GetKeywordsById(id As Integer) As DataTable Implements IDocumentRepository.GetKeywordsById
        Return repository.GetKeywordsById(id)
    End Function

    Public Function GetCategoryById(id As Integer) As DataTable Implements IDocumentRepository.GetCategoryById
        Return repository.GetCategoryById(id)
    End Function

    Public Function GetFlagStateById(id As Integer) As DataTable Implements IDocumentRepository.GetFlagStateById
        Return repository.GetFlagStateById(id)
    End Function

    Public Sub GetPdfById(id As Integer, pdfFile As String) Implements IDocumentRepository.GetPdfById
        repository.GetPdfById(id, pdfFile)
    End Sub

    Public Sub CreateRecord(title As String, author As String, subject As String, keywords As String, notes As String, pdfFile As String, category As String, flag As Integer) Implements IDocumentRepository.CreateRecord
        repository.CreateRecord(title, author, subject, keywords, notes, pdfFile, category, flag)
    End Sub

    Public Sub UpdateNotesById(id As Integer, notes As String) Implements IDocumentRepository.UpdateNotesById
        repository.UpdateNotesById(id, notes)
    End Sub

    Public Sub UpdateCategoryById(id As Integer, category As String) Implements IDocumentRepository.UpdateCategoryById
        repository.UpdateCategoryById(id, category)
    End Sub

    Public Sub UpdateFlagStateById(id As Integer, flag As Integer) Implements IDocumentRepository.UpdateFlagStateById
        repository.UpdateFlagStateById(id, flag)
    End Sub

    Public Sub DeleteRecordById(id As Integer) Implements IDocumentRepository.DeleteRecordById
        repository.DeleteRecordById(id)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Public Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                repository.Dispose()
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
