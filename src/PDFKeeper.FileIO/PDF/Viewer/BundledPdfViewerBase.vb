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

Public MustInherit Class BundledPdfViewerBase
    Inherits PdfBase

    ''' <summary>
    ''' Opens bundled PDF viewer.
    ''' </summary>
    ''' <param name="arguments">Viewer command line arguments</param>
    ''' <returns>PID</returns>
    Protected Function OpenViewer(ByVal arguments As String) As Integer
        Using sumatraPdf = New Process
            With sumatraPdf
                .StartInfo.FileName = Path.Combine(My.Application.Info.DirectoryPath, "SumatraPDF-3.4.6-64.exe")
                .StartInfo.Arguments = arguments
                .StartInfo.UseShellExecute = False
                .Start()
                Return sumatraPdf.Id
            End With
        End Using
    End Function
End Class
