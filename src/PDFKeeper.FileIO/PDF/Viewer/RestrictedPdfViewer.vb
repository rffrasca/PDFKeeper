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

Public Class RestrictedPdfViewer
    Inherits BundledPdfViewerBase
    Private Shared ReadOnly pidList As New List(Of Integer)

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    Public Sub New(ByVal pdfFile As String)
        Me.file = New FileInfo(pdfFile)
    End Sub

    ''' <summary>
    ''' Shows PDF with the bundled viewer in restricted mode.
    ''' </summary>
    Public Sub ShowPdf()
        Dim id = OpenViewer(String.Concat("-restrict ", Chr(34), file.FullName, Chr(34)))
        pidList.Add(id)
    End Sub

    ''' <summary>
    ''' Closes the restricted PDF viewer.
    ''' </summary>
    Public Shared Sub Close()
        For Each id In pidList.ToList
            Try
                Using process As Process = Process.GetProcessById(id)
                    process.CloseMainWindow()
                    process.Close()
                End Using
                pidList.Remove(id)
            Catch ex As ArgumentException
            End Try
        Next
    End Sub
End Class
