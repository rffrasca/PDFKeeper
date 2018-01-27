'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2018 Robert F. Frasca
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
Public Interface INonQueryService
    Sub InsertDocument(ByVal title As String, _
                       ByVal author As String, _
                       ByVal subject As String, _
                       ByVal keywords As String, _
                       ByVal notes As String, _
                       ByVal pdfFile As String)
    Sub SetDocumentNotes(ByVal id As Integer, ByVal notes As String)
    Sub DeleteDocument(ByVal id As Integer)
End Interface
