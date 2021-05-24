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
Public Class PdfTextExtractorFactory
    Private ReadOnly pdfFile As String

    Public Sub New(ByVal pdfFile As String)
        Me.pdfFile = pdfFile
    End Sub

    ''' <summary>
    ''' Factory method that returns the text from the PDF using the appropriate
    ''' extractor object based on the type of PDF being processed.
    ''' 
    ''' Primary Extractor: used for text based PDF except when PDF contains an
    ''' invalid encoding.
    ''' 
    ''' Alternate Extractor: used when PDF is text based and contains an
    ''' invalid encoding.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetText() As String
        Dim primaryExtactor As New PrimaryPdfTextExtractor(pdfFile)
        Dim pdfText As String = primaryExtactor.GetText
        If pdfText Is Nothing Then
            Dim alternateExtractor As New AlternatePdfTextExtractor(pdfFile)
            pdfText = alternateExtractor.GetText
        End If
        Return pdfText
    End Function
End Class
