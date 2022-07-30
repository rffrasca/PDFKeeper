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
Imports PDFKeeper.Domain

Public Interface IDocumentRepository
    ''' <summary>
    ''' Document list action.
    ''' </summary>
    Enum DocumentListAction
        BySearchTerm
        ByDateAdded
        Flagged
        All
    End Enum

    ''' <summary>
    ''' Lists documents in the repository.
    ''' </summary>
    ''' <param name="choice">Flagged or All</param>
    ''' <returns>DataTable object</returns>
    Function ListDocuments(ByVal choice As DocumentListAction) As DataTable

    ''' <summary>
    ''' Lists documents in the repository.
    ''' </summary>
    ''' <param name="choice">BySearchTerm or ByDateAdded</param>
    ''' <param name="value">Search term or date added as YYYY-MM-DD</param>
    ''' <returns>DataTable object</returns>
    Function ListDocuments(ByVal choice As DocumentListAction, ByVal value As String) As DataTable

    ''' <summary>
    ''' Lists documents in the repository.
    ''' </summary>
    ''' <param name="filter">FindSelectionsFilterModel object</param>
    ''' <returns>DataTable object</returns> 
    Function ListDocuments(ByVal filter As FindSelectionsFilterModel) As DataTable

    ''' <summary>
    ''' Lists authors in the repository by group.
    ''' </summary>
    ''' <returns>DataTable object</returns>
    Function ListAuthors() As DataTable

    ''' <summary>
    ''' Lists authors in the repository by group.
    ''' </summary>
    ''' <param name="filter">AuthorsFilterModel object</param>
    ''' <returns>DataTable object</returns>
    Function ListAuthors(ByVal filter As AuthorFilterModel) As DataTable

    ''' <summary>
    ''' Lists subjects in the repository by group.
    ''' </summary>
    ''' <returns>DataTable object</returns>
    Function ListSubjects() As DataTable

    ''' <summary>
    ''' Lists subjects in the repository by group.
    ''' </summary>
    ''' <param name="author">Filter by author name</param>
    ''' <returns>DataTable object</returns>
    Function ListSubjects(ByVal author As String) As DataTable

    ''' <summary>
    ''' Lists subjects in the repository by group.
    ''' </summary>
    ''' <param name="filter">SubjectsFilterModel object</param>
    ''' <returns>DataTable object</returns>
    Function ListSubjects(ByVal filter As SubjectFilterModel) As DataTable

    ''' <summary>
    ''' Lists categories in the repository by group.
    ''' </summary>
    ''' <returns>DataTable object</returns>
    Function ListCategories() As DataTable

    ''' <summary>
    ''' Lists categories in the repository by group.
    ''' </summary>
    ''' <param name="filter">CategoriesFilterModel object</param>
    ''' <returns>DataTable object</returns>
    Function ListCategories(ByVal filter As CategoryFilterModel) As DataTable

    ''' <summary>
    ''' Lists tax years in the repository by group.
    ''' </summary>
    ''' <returns>DataTable object</returns>
    Function ListTaxYears() As DataTable

    ''' <summary>
    ''' Lists tax years in the repository by group.
    ''' </summary>
    ''' <param name="filter">TaxYearsFilterModel object</param>
    ''' <returns>DataTable object</returns>
    Function ListTaxYears(ByVal filter As TaxYearFilterModel) As DataTable

    ''' <summary>
    ''' Creates a document in the repository.
    ''' </summary>
    ''' <param name="model">DocumentModel object</param>
    Sub CreateDocument(ByVal model As DocumentModel)

    ''' <summary>
    ''' Reads a document from the repository.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <param name="searchTerm">Specified Search Term or nothing</param>
    ''' <returns>DocumentModel object</returns>
    Function ReadDocument(ByVal id As Integer, ByVal searchTerm As String) As DocumentModel

    ''' <summary>
    ''' Updates a document in the repository.
    ''' 
    ''' Only the doc_notes, doc_category, doc_tax_year, doc_flag, doc_text_annotations, and doc_text columns will be updated. 
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <param name="model">DocumentModel object</param>
    Sub UpdateDocument(ByVal id As Integer, ByVal model As DocumentModel)

    ''' <summary>
    ''' Deletes a document from the repository.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    Sub DeleteDocument(ByVal id As Integer)

    ''' <summary>
    ''' Performs a test connection to the database.
    ''' 
    ''' NotSupportedException will be thrown when database platform is SQLite.
    ''' </summary>
    Sub TestConnection()

    ''' <summary>
    ''' Resets the credential object used for connecting to the database.
    ''' 
    ''' NotSupportedException will be thrown when database platform does not implement a credential object.
    ''' </summary>
    Sub ResetCredential()

    ''' <summary>
    ''' Rebuilds the database Full-Text Search Index.
    ''' 
    ''' NotSupportedException will be thrown when database platform is not SQLite.
    ''' </summary>
    Sub RebuildFullTextSearchIndex()
End Interface
