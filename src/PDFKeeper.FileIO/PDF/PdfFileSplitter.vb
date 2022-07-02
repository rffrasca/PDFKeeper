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
Imports System.Threading
Imports iText.Kernel.Pdf
Imports iText.Kernel.Utils

<CLSCompliant(False)>
Friend Class PdfFileSplitter
    Inherits PdfSplitter
    Private ReadOnly destPath As String
    Private ReadOnly pdfName As String
    Private pageNumber As Integer = 1

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="pdfDoc">PdfDocument object</param>
    ''' <param name="destPath">Destination folder path</param>
    ''' <param name="pdfName">PDF file name</param>
    Public Sub New(ByVal pdfDoc As PdfDocument, ByVal destPath As String, ByVal pdfName As String)
        MyBase.New(pdfDoc)
        Me.destPath = destPath
        Me.pdfName = Path.GetFileNameWithoutExtension(pdfName)
    End Sub

    Protected Overrides Function GetNextPdfWriter(ByVal docPageRange As PageRange) As PdfWriter
        Return New PdfWriter(Path.Combine(destPath, String.Concat(pdfName, "_",
                                                                  Math.Min(Interlocked.Increment(pageNumber),
                                                                           pageNumber - 1), ".pdf")))
    End Function
End Class
