'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
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
Public Class PdfInformationPropertiesWriter
    Inherits PdfInformationProperties
    Private passwordSpecified As Boolean
    Private m_InputPdfPath As String
    Private m_InputPdfPassword As SecureString
    Private m_OutputPdfPath As String

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="inputPdfPath">Input PDF.</param>
    ''' <param name="outputPdfPath">Output PDF.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal inputPdfPath As String, _
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
    Public Sub New(ByVal inputPdfPath As String, _
                   ByVal inputPdfPassword As SecureString, _
                   ByVal outputPdfPath As String)
        passwordSpecified = True
        m_InputPdfPath = inputPdfPath
        m_InputPdfPassword = inputPdfPassword
        m_OutputPdfPath = outputPdfPath
    End Sub

    ''' <summary>
    ''' Writes a new copy of the input PDF file with the modified information
    ''' properties from the base object.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Write()
        If passwordSpecified = False Then
            Using reader As New PdfReader(m_InputPdfPath)
                Write(reader, m_OutputPdfPath)
            End Using
        Else
            Using reader As New PdfReader(m_InputPdfPath, _
                                          System.Text.Encoding.ASCII.GetBytes( _
                                              m_InputPdfPassword.SecureStringToString))
                Write(reader, m_OutputPdfPath)
            End Using
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Private Sub Write(ByVal reader As PdfReader, _
                      ByVal outputPdfPath As String)
        Dim dictionary As New Dictionary(Of String, String)
        ' ITextSharp's PdfStamper class disposes the FileStream object causing
        ' a CA2000 violation. Instantiating the FileStream object in a using
        ' block results in a CA2202 violation.
        Dim outputPdf As New FileStream(outputPdfPath, FileMode.Create)
        Using stamper As New PdfStamper(reader, outputPdf)
            dictionary("Title") = Title
            dictionary("Author") = Author
            dictionary("Subject") = Subject
            dictionary("Keywords") = Keywords
            stamper.MoreInfo = dictionary
        End Using
    End Sub
End Class
