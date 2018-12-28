'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
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
Public Class PdfFileInfoPropertiesWriter
    Inherits PdfFileInfoPropertiesBase
    Private passwordSpecified As Boolean
    Private m_pdfFileIn As String
    Private m_pdfFileOut As String
    Private m_pdfFileInPassword As SecureString

    Public Sub New(ByVal pdfFileIn As String, ByVal pdfFileOut As String)
        passwordSpecified = False
        m_pdfFileIn = pdfFileIn
        m_pdfFileOut = pdfFileOut
    End Sub

    Public Sub New(ByVal pdfFileIn As String, _
                   ByVal pdfFileOut As String, _
                   ByVal pdfFileInPassword As SecureString)
        passwordSpecified = True
        m_pdfFileIn = pdfFileIn
        m_pdfFileOut = pdfFileOut
        m_pdfFileInPassword = pdfFileInPassword
    End Sub

    Public Sub Write()
        If passwordSpecified = False Then
            Using reader As New PdfReader(m_pdfFileIn)
                WriteOutputPdf(reader, m_pdfFileOut)
            End Using
        Else
            Using reader As New PdfReader(m_pdfFileIn, _
                                          System.Text.Encoding.ASCII.GetBytes( _
                                              m_pdfFileInPassword.ToPlainTextString))
                WriteOutputPdf(reader, m_pdfFileOut)
            End Using
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Private Sub WriteOutputPdf(ByVal reader As PdfReader, _
                               ByVal pdfFileOut As String)
        Dim dictionary As New Dictionary(Of String, String)
        Using stamper As New PdfStamper(reader, _
                                        New FileStream(pdfFileOut, _
                                                       FileMode.Create))
            dictionary("Title") = Title
            dictionary("Author") = Author
            dictionary("Subject") = Subject
            dictionary("Keywords") = Keywords
            stamper.MoreInfo = dictionary
        End Using
    End Sub
End Class
