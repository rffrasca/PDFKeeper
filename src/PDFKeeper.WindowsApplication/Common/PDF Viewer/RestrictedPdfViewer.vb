'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Class RestrictedPdfViewer
    Inherits PdfViewerBase
    Implements IRestrictedPdfViewer
    Private processIds As New GenericList(Of Integer)

    Public Sub Open(pdfPath As String) Implements IRestrictedPdfViewer.Open
        Dim id As Integer = _
            OpenViewer("-restrict " & Chr(34) & pdfPath & Chr(34))
        processIds.Add(id)
    End Sub

    Public Sub Close() Implements IRestrictedPdfViewer.Close
        For Each id As Integer In processIds.ToArray(False)
            Try
                Dim process As Process
                process = process.GetProcessById(id)
                process.CloseMainWindow()
                process.Close()
            Catch ex As ArgumentException
            End Try
        Next
    End Sub
End Class
