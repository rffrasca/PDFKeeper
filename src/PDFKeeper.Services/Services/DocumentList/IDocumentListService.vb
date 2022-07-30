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

Public Interface IDocumentListService
    ''' <summary>
    ''' Lists documents by search term in the repository.
    ''' </summary>
    ''' <param name="searchTerm">Search term</param>
    ''' <returns>DataTable object</returns>
    Function ListDocumentsBySearchTerm(ByVal searchTerm As String) As DataTable

    ''' <summary>
    ''' Lists documents by selections in the repository.
    ''' </summary>
    ''' <param name="filter">FindSelectionsFilterModel object</param>
    ''' <returns>DataTable object</returns>
    Function ListDocumentsBySelections(ByVal filter As FindSelectionsFilterModel) As DataTable

    ''' <summary>
    ''' Lists documents by date added in the repository.
    ''' </summary>
    ''' <param name="dateAdded">date added in the format of YYYY-MM-DD</param>
    ''' <returns>DataTable object</returns>
    Function ListDocumentsByDateAdded(ByVal dateAdded As String) As DataTable

    ''' <summary>
    ''' Lists flagged documents in the repository.
    ''' </summary>
    ''' <returns>DataTable object</returns>
    Function ListFlaggedDocuments() As DataTable

    ''' <summary>
    ''' Lists all documents in the repository.
    ''' </summary>
    ''' <returns>DataTable object.</returns>
    Function ListAllDocuments() As DataTable
End Interface
