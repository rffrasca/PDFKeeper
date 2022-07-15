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
Imports PDFKeeper.Infrastructure

Public Class DocumentService
    Implements IDocumentService
    Private ReadOnly repository As IDocumentRepository

    Public Sub New(ByVal repository As IDocumentRepository)
        Me.repository = repository
    End Sub

    Public Sub CreateDocument(pdfFile As String, infoModel As PdfInfoModel, infoExtModel As PdfInfoExtModel) Implements IDocumentService.CreateDocument
        If infoModel Is Nothing Then
            Throw New ArgumentNullException(NameOf(infoModel))
        End If
        If infoExtModel Is Nothing Then
            Throw New ArgumentNullException(NameOf(infoExtModel))
        End If
        Dim pdf = New PdfFile(pdfFile)
        Dim model = New DocumentModel
        With model
            .Title = infoModel.Title
            .Author = infoModel.Author
            .Subject = infoModel.Subject
            .Keywords = infoModel.Keywords
            .Notes = String.Empty
            .Pdf = pdf.ToByteArray
            .Category = infoExtModel.Category
            .Flag = infoExtModel.Flag
            .TaxYear = infoExtModel.TaxYear
            .TextAnnotations = pdf.GetTextAnnot
            .Text = pdf.GetText(infoExtModel.OcrPdfTextAndImageDataPages)
        End With
        repository.CreateDocument(model)
    End Sub

    Public Function ReadDocument(id As Integer) As DocumentModel Implements IDocumentService.ReadDocument
        Return repository.ReadDocument(id)
    End Function

    Public Sub UpdateDocument(id As Integer, model As DocumentModel) Implements IDocumentService.UpdateDocument
        repository.UpdateDocument(id, model)
    End Sub

    Public Sub DeleteDocument(id As Integer) Implements IDocumentService.DeleteDocument
        repository.DeleteDocument(id)
    End Sub
End Class
