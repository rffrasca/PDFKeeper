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
Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas.Parser
Imports iText.Kernel.Pdf.Canvas.Parser.Listener

Public Class PrimaryPdfTextExtractor
    Inherits PdfTextExtractorBase

    Public Sub New(ByVal pdfFile As String)
        MyBase.New(pdfFile)
    End Sub

    ''' <summary>
    ''' Returns the text from the PDF using iText.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetText() As String
        Using reader = New PdfReader(pdfFile)
            Dim textString As New StringBuilder
            Using pdfDoc As New PdfDocument(reader)
                For page As Integer = 1 To pdfDoc.GetNumberOfPages
                    Try
                        Dim strategy As ITextExtractionStrategy =
                            New LocationTextExtractionStrategy
                        Dim pageText As String =
                            PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page),
                                                             strategy)
                        Dim lines As String() = pageText.Split(ControlChars.Lf)
                        For Each line In lines
                            textString.AppendLine(line)
                        Next
                    Catch ex As ArgumentException   ' PDF contains an invalid encoding.
                        Return Nothing
                    End Try
                Next
            End Using
            Return textString.ToString
        End Using
    End Function
End Class
