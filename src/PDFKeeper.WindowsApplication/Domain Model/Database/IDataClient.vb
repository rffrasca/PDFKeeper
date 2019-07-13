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
Public Interface IDataClient
    Sub TestConnection()
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetAllAuthors() As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetAllSubjects() As DataTable
    Function GetAllSubjectsByAuthor(ByVal author As String) As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetAllCategories() As DataTable
    Function GetAllRecordsBySearchString(ByVal searchValue As String) As DataTable
    Function GetAllRecordsByAuthor(ByVal author As String) As DataTable
    Function GetAllRecordsBySubject(ByVal subject As String) As DataTable
    Function GetAllRecordsByAuthorAndSubject(ByVal author As String, _
                                             ByVal subject As String) As DataTable
    Function GetAllRecordsByCategory(ByVal category As String) As DataTable
    Function GetAllRecordsByDateAdded(ByVal dateAdded As String) As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetTotalRecordsCount() As Integer
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetFlaggedRecordsCount() As Integer
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetAllRecords() As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
                                                     "CA1024:UsePropertiesWhereAppropriate")> _
    Function GetAllFlaggedRecords() As DataTable
    Function GetNotesById(ByVal id As Integer) As DataTable
    Function GetKeywordsById(ByVal id As Integer) As DataTable
    Function GetCategoryById(ByVal id As Integer) As DataTable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
                                                     "CA1726:UsePreferredTerms", _
                                                     MessageId:="Flag")> _
    Function GetFlagStateById(ByVal id As Integer) As DataTable
    Sub GetPdfById(ByVal id As Integer, ByVal pdfFile As String)
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
                                                     "CA1726:UsePreferredTerms", _
                                                     MessageId:="flag")> _
    Sub CreateRecord(ByVal title As String, _
                     ByVal author As String, _
                     ByVal subject As String, _
                     ByVal keywords As String, _
                     ByVal notes As String, _
                     ByVal pdfFile As String, _
                     ByVal category As String, _
                     ByVal flag As Integer)
    Sub UpdateNotesById(ByVal id As Integer, ByVal notes As String)
    Sub UpdateCategoryById(ByVal id As Integer, ByVal category As String)
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
                                                     "CA1726:UsePreferredTerms", _
                                                     MessageId:="flag")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
                                                     "CA1726:UsePreferredTerms", _
                                                     MessageId:="Flag")> _
    Sub UpdateFlagStateById(ByVal id As Integer, ByVal flag As Integer)
    Sub DeleteRecordById(ByVal id As Integer)
End Interface
