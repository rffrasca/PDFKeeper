'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2018 Robert F. Frasca
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
Public Class PdfFileInfoPropertiesReader
    Inherits PdfFileInfoPropertiesBase

    Public Sub New(ByVal pdfFile As String)
        Read(pdfFile)
    End Sub

    Public Sub New(ByVal pdfFile As String, _
                   ByVal pdfFilePassword As SecureString)
        ReadWithPassword(pdfFile, pdfFilePassword)
    End Sub

    Private Sub Read(ByVal pdfFile As String)
        Using reader As New PdfReader(pdfFile)
            GetPdfInfoProperties(reader)
        End Using
    End Sub

    Private Sub ReadWithPassword(ByVal pdfFile As String, _
                                 ByVal pdfFilePassword As SecureString)
        Using reader As New PdfReader(pdfFile, _
                                      System.Text.Encoding.ASCII.GetBytes(pdfFilePassword.ToPlainTextString))
            GetPdfInfoProperties(reader)
        End Using
    End Sub

    Private Sub GetPdfInfoProperties(ByVal reader As PdfReader)
        If reader.Info.ContainsKey("Title") Then
            Title = reader.Info("Title")
        Else
            Title = String.Empty
        End If
        If reader.Info.ContainsKey("Author") Then
            Author = reader.Info("Author")
        Else
            Author = String.Empty
        End If
        If reader.Info.ContainsKey("Subject") Then
            Subject = reader.Info("Subject")
        Else
            Subject = String.Empty
        End If
        If reader.Info.ContainsKey("Keywords") Then
            Keywords = reader.Info("Keywords")
        Else
            Keywords = String.Empty
        End If
    End Sub
End Class
