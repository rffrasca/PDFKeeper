'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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

Public Interface ISubjectListService
    ''' <summary>
    ''' Lists subjects in the repository by group.
    ''' </summary>
    ''' <returns>DataTable object</returns>
    Function ListSubjects() As DataTable

    ''' <summary>
    ''' Lists subjects in the repository by group.
    ''' </summary>
    ''' <param name="author">Filter by Author name</param>
    ''' <returns>DataTable object</returns>
    Function ListSubjects(ByVal author As String) As DataTable

    ''' <summary>
    ''' Lists subjects in the repository by group.
    ''' </summary>
    ''' <param name="filter">SubjectFilterModel object</param>
    ''' <returns>DataTable object</returns>
    Function ListSubjects(ByVal filter As SubjectFilterModel) As DataTable
End Interface
