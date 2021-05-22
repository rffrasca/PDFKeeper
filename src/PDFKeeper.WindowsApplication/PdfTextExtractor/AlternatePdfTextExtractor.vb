'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Imports UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor

Public Class AlternatePdfTextExtractor
    Inherits PdfTextExtractorBase

    Public Sub New(ByVal pdfFile As String)
        MyBase.New(pdfFile)
    End Sub

    ''' <summary>
    ''' Returns the text from the PDF using UglyToad.PdfPig. This is to be used
    ''' when iText is unable to read all of the text from the PDF due to its
    ''' strict adherence to the PDF specification (ISO 32000).
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetText() As String
        Using pdfDoc = UglyToad.PdfPig.PdfDocument.Open(pdfFile)
            Dim textString As New StringBuilder
            For Each page In pdfDoc.GetPages()
                textString.Append(ContentOrderTextExtractor.GetText(page))
            Next
            Return textString.ToString
        End Using
    End Function
End Class
