'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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

Public Class PdfViewer
    Inherits BundledPdfViewerBase

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    Public Sub New(ByVal pdfFile As String)
        Me.file = New FileInfo(pdfFile)
    End Sub

    ''' <summary>
    ''' Shows PDF with bundled viewer or default PDF application.
    ''' </summary>
    ''' <param name="useDefault">True or False to use Windows default PDF application.</param>
    Public Sub Show(ByVal useDefault As Boolean)
        If useDefault Then
            Process.Start(file.FullName)
        Else
            OpenViewer(String.Concat(Chr(34), file.FullName, Chr(34)))
        End If
    End Sub
End Class
