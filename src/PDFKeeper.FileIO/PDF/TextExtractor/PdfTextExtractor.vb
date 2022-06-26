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
Imports System.IO
Imports System.Text
Imports iText.Kernel.Pdf
Imports PDFKeeper.Common

Public Class PdfTextExtractor
    Inherits PdfBase

    ''' <summary>
    ''' Create an instance of the class.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    Public Sub New(ByVal pdfFile As String)
        Me.file = New FileInfo(pdfFile)
    End Sub

    ''' <summary>
    ''' Gets text annotations from the PDF.
    ''' </summary>
    ''' <returns>Text annotations</returns>
    Public Function GetTextAnnot() As String
        Using reader = New PdfReader(file)
            Dim output = New StringBuilder
            Using document = New PdfDocument(reader)
                For pageCounter = 1 To document.GetNumberOfPages
                    Dim page = document.GetPage(pageCounter)
                    Dim annotations = page.GetAnnotations()
                    For Each annotation In annotations
                        Dim dict = annotation.GetPdfObject
                        Dim text = dict.GetAsString(PdfName.Contents)
                        If text IsNot Nothing Then
                            output.AppendLine(text.ToUnicodeString)
                        End If
                    Next
                Next
            End Using
            Return output.ToString
        End Using
    End Function

    ''' <summary>
    ''' Gets text from the PDF using the appropriate extraction strategy for the PDF page being processed.
    ''' 
    ''' Text Extraction Strategy:
    ''' Uses iText for text based PDF page except when page contains an invalid encoding due to iText's strict adherence
    ''' to the PDF specification (ISO 32000) or when iText is unable to extract text from the page for another reason.
    ''' 
    ''' OCR Text Extraction Strategy:
    ''' Uses OCR for text based PDF when page is rejected by iText or when the page is "Image-only".
    ''' </summary>
    ''' <returns>Text</returns>
    Public Function GetText() As String
        Dim pdfText = New StringBuilder
        Dim strategy As IPdfTextExtractionStrategy
        For Each pdf In New PdfFile(file.FullName).Split(AppFolders.GetPath(AppFolders.AppFolder.Temp))
            strategy = New PdfTextExtractionStrategy
            Dim text = strategy.GetText(pdf)
            If text Is Nothing Then
                strategy = New PdfOcrTextExtractionStrategy
                text = strategy.GetText(pdf)
            ElseIf text.Trim.Length = 0 Then
                strategy = New PdfOcrTextExtractionStrategy
                text = strategy.GetText(pdf)
            End If
            pdfText.Append(text)
            pdf.Delete()
        Next
        Return pdfText.ToString
    End Function
End Class
