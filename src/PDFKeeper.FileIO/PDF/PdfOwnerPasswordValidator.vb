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
Imports System.Security
Imports iText.Kernel.Exceptions
Imports iText.Kernel.Pdf
Imports PDFKeeper.Common

Public Class PdfOwnerPasswordValidator
    Inherits PdfBase

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <param name="password">PDF password secure string</param>
    Public Sub New(ByVal pdfFile As String, ByVal password As SecureString)
        Me.file = New FileInfo(pdfFile)
        Me.password = password
        bouncyCastleRestore.Execute()
    End Sub

    ''' <summary>
    ''' Is the PDF Owner password valid?
    ''' </summary>
    ''' <returns>True or False</returns>
    Public Function IsValid() As Boolean
        Try
            Using reader = New PdfReader(file.FullName,
                                         New ReaderProperties().SetPassword(
                                         Text.Encoding.ASCII.GetBytes(password.Decrypt)))
                Using document = New PdfDocument(reader)
                End Using
            End Using
            Return True
        Catch ex As BadPasswordException
            Return False
        End Try
    End Function
End Class
