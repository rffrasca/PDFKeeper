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
Imports iText.Kernel.Pdf.Canvas.Parser.Listener

Public Class PdfTextExtractionStrategy
    Implements IPdfTextExtractionStrategy

    Public Function GetText(pdfFile As FileInfo) As String Implements IPdfTextExtractionStrategy.GetText
        Using reader = New PdfReader(pdfFile)
            Dim text = New StringBuilder
            Using pdfDoc = New PdfDocument(reader)
                For page = 1 To pdfDoc.GetNumberOfPages
                    Try
                        Dim strategy As ITextExtractionStrategy = New LocationTextExtractionStrategy
                        Dim pageText = Canvas.Parser.PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy)
                        Dim lines = pageText.Split(ControlChars.Lf)
                        For Each line In lines
                            text.AppendLine(line)
                        Next
                    Catch ex As ArgumentException   ' PDF contains an invalid encoding.
                        Return Nothing
                    Catch ex As iText.Kernel.Pdf.Canvas.Parser.Util.InlineImageParsingUtils.InlineImageParseException
                        Return Nothing
                    End Try
                Next
            End Using
            Return text.ToString
        End Using
    End Function
End Class
