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
Imports iText.Kernel.Pdf

Public Class PdfMetadataReader
    Inherits PdfMetadataBase

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="pdfPath">Source PDF.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal pdfPath As String)
        Read(pdfPath)
    End Sub

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="pdfPath">Source PDF.</param>
    ''' <param name="pdfPassword">Source PDF Owner password.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal pdfPath As String,
                   ByVal pdfPassword As SecureString)
        If pdfPassword Is Nothing Then
            Throw New ArgumentNullException(NameOf(pdfPassword))
        End If
        Read(pdfPath, pdfPassword)
    End Sub

    Private Sub Read(ByVal pdfPath As String)
        Using reader As New PdfReader(pdfPath)
            GetMetadata(reader)
        End Using
    End Sub

    Private Sub Read(ByVal pdfPath As String,
                     ByVal pdfPassword As SecureString)
        Using reader As New PdfReader(pdfPath,
                                      New ReaderProperties().SetPassword(
                                      Text.Encoding.ASCII.GetBytes(
                                      pdfPassword.SecureStringToString)))
            GetMetadata(reader)
        End Using
    End Sub

    Private Sub GetMetadata(ByVal reader As PdfReader)
        Using pdfDoc As New PdfDocument(reader)
            Dim pdfDocInfo As PdfDocumentInfo = pdfDoc.GetDocumentInfo
            Title = pdfDocInfo.GetTitle
            Author = pdfDocInfo.GetAuthor
            Subject = pdfDocInfo.GetSubject
            Keywords = pdfDocInfo.GetKeywords
        End Using
    End Sub
End Class
