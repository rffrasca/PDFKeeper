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
Imports PDFKeeper.FileIO

Public Interface IDocumentService
    ''' <summary>
    ''' Creates a document in the repository.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <param name="infoModel">PdfInfoModel object</param>
    ''' <param name="infoExtModel">PdfInfoExtModel object</param>
    Sub CreateDocument(ByVal pdfFile As String, ByVal infoModel As PdfInfoModel, ByVal infoExtModel As PdfInfoExtModel)

    ''' <summary>
    ''' Reads a document from the repository.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <returns>DocumentModel object</returns>
    Function ReadDocument(ByVal id As Integer) As DocumentModel

    ''' <summary>
    ''' Updates a document in the repository.
    ''' 
    ''' Only the doc_notes, doc_category, doc_tax_year, doc_flag, doc_text_annotations, and doc_text columns will be
    ''' updated.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <param name="model">DocumentModel object</param>
    Sub UpdateDocument(ByVal id As Integer, ByVal model As DocumentModel)

    ''' <summary>
    ''' Deletes a document from the repository.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    Sub DeleteDocument(ByVal id As Integer)
End Interface
