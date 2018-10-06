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
Public Interface IAddPdfDocumentView
    ReadOnly Property OriginalPdfFile As String
    ReadOnly Property OriginalPdfFilePassword As SecureString
    Property OriginalPdfPathName As String
    Property Title As String
    Property Authors As DataTable
    Property Author As String
    Property Subjects As DataTable
    Property Subject As String
    Property Keywords As String
    Property SaveEnabled As Boolean
    Property PreviewEnabled As Boolean
    Property OkEnabled As Boolean
End Interface
