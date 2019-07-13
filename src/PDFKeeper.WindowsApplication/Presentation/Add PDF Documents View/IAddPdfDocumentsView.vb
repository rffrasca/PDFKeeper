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
Public Interface IAddPdfDocumentsView
    Inherits ICommonView
    Property SelectedPdfPath As String
    Property Title As String
    Property SelectEnabled As Boolean
    Property ViewEnabled As Boolean
    Property TitleEnabled As Boolean
    Property SetTitleToFileNameEnabled As Boolean
    Property AuthorPairedEnabled As Boolean
    Property SubjectPairedEnabled As Boolean
    Property Keywords As String
    Property KeywordsEnabled As Boolean
    Property CategoryEnabled As Boolean
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
                                                     "CA1726:UsePreferredTerms", _
                                                     MessageId:="Flag")> _
    Property FlagDocumentEnabled As Boolean
    Property DeleteSelectedPdfOnOkEnabled As Boolean
    Property SaveEnabled As Boolean
    Property PreviewEnabled As Boolean
    Property AddEnabled As Boolean
    Property DiscardEnabled As Boolean
End Interface
