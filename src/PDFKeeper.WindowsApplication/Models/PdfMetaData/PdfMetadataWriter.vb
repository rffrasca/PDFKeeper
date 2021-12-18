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
Imports iText.Kernel.XMP

Public Class PdfMetadataWriter
    Inherits PdfMetadataBase
    Private ReadOnly passwordSpecified As Boolean
    Private ReadOnly m_InputPdfPath As String
    Private ReadOnly m_InputPdfPassword As SecureString
    Private ReadOnly m_OutputPdfPath As String

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="inputPdfPath">Input PDF.</param>
    ''' <param name="outputPdfPath">Output PDF.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal inputPdfPath As String,
                   ByVal outputPdfPath As String)
        passwordSpecified = False
        m_InputPdfPath = inputPdfPath
        m_OutputPdfPath = outputPdfPath
    End Sub

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="inputPdfPath">Source PDF.</param>
    ''' <param name="inputPdfPassword">Source PDF Owner password.</param>
    ''' <param name="outputPdfPath">Output PDF.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal inputPdfPath As String,
                   ByVal inputPdfPassword As SecureString,
                   ByVal outputPdfPath As String)
        passwordSpecified = True
        m_InputPdfPath = inputPdfPath
        m_InputPdfPassword = inputPdfPassword
        m_OutputPdfPath = outputPdfPath
    End Sub

    ''' <summary>
    ''' Writes a new copy of the input PDF file with the modified metedata from
    ''' the base object.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Write()
        If passwordSpecified = False Then
            Using reader As New PdfReader(m_InputPdfPath)
                Write(reader, m_OutputPdfPath)
            End Using
        Else
            Using reader As New PdfReader(m_InputPdfPath,
                                          New ReaderProperties().SetPassword(
                                          Text.Encoding.ASCII.GetBytes(
                                          m_InputPdfPassword.SecureStringToString)))
                Write(reader, m_OutputPdfPath)
            End Using
        End If
    End Sub

    Private Sub Write(ByVal reader As PdfReader,
                      ByVal outputPdfPath As String)
        Using writer As New PdfWriter(outputPdfPath)
            Using pdfDoc As New PdfDocument(reader, writer)
                Dim pdfDocInfo As PdfDocumentInfo = pdfDoc.GetDocumentInfo
                pdfDocInfo.SetTitle(Title)
                pdfDocInfo.SetAuthor(Author)
                pdfDocInfo.SetSubject(Subject)
                pdfDocInfo.SetKeywords(Keywords)
                pdfDoc.SetXmpMetadata(XMPMetaFactory.Create)
            End Using
        End Using
    End Sub
End Class
