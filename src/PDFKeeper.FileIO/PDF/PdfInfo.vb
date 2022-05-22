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
Imports iText.Kernel.Pdf
Imports iText.Kernel.XMP
Imports PDFKeeper.Common

Public Class PdfInfo
    Inherits PdfBase
    Private _Title As String
    Private _Author As String
    Private _Subject As String
    Private _Keywords As String

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    ''' <param name="password">PDF password secure string or Nothing</param>
    Public Sub New(ByVal pdfFile As String, ByVal password As SecureString)
        Me.file = New FileInfo(pdfFile)
        Me.password = password
        bouncyCastleRestore.Execute()
        Read()
    End Sub

    ''' <summary>
    ''' Gets or Sets the PDF Title.
    ''' </summary>
    ''' <returns>Title</returns>
    Public Property Title As String
        Get
            Return _Title
        End Get
        Set(value As String)
            _Title = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the PDF Author.
    ''' </summary>
    ''' <returns>Author</returns>
    Public Property Author As String
        Get
            Return _Author
        End Get
        Set(value As String)
            _Author = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the PDF Subject.
    ''' </summary>
    ''' <returns>Subject</returns>
    Public Property Subject As String
        Get
            Return _Subject
        End Get
        Set(value As String)
            _Subject = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the PDF Keywords.
    ''' </summary>
    ''' <returns>Keywords</returns>
    Public Property Keywords As String
        Get
            Return _Keywords
        End Get
        Set(value As String)
            _Keywords = value
        End Set
    End Property

    ''' <summary>
    ''' Writes a new PDF with the values from the Title, Author, Subject, and Keywords properties.  
    ''' </summary>
    ''' <param name="pdfFile">New PDF file name</param>
    Public Sub Write(ByVal pdfFile As String)
        If password Is Nothing Then
            Using reader = New PdfReader(file)
                Write(reader, pdfFile)
            End Using
        Else
            Using reader = New PdfReader(file.FullName,
                                         New ReaderProperties().SetPassword(
                                         Text.Encoding.ASCII.GetBytes(password.Decrypt)))
                Write(reader, pdfFile)
            End Using
        End If
    End Sub

    Private Sub Read()
        If password Is Nothing Then
            Using reader = New PdfReader(file)
                GetInfo(reader)
            End Using
        Else
            Using reader = New PdfReader(file.FullName,
                                         New ReaderProperties().SetPassword(
                                         Text.Encoding.ASCII.GetBytes(password.Decrypt)))
                GetInfo(reader)
            End Using
        End If
    End Sub

    Private Sub GetInfo(ByVal reader As PdfReader)
        Using document = New PdfDocument(reader)
            Dim info = document.GetDocumentInfo
            With info
                Title = .GetTitle
                Author = .GetAuthor
                Subject = .GetSubject
                Keywords = .GetKeywords
            End With
        End Using
    End Sub

    Private Sub Write(ByVal reader As PdfReader, ByVal pdfFile As String)
        Using writer = New PdfWriter(pdfFile)
            Using document = New PdfDocument(reader, writer)
                Dim info = document.GetDocumentInfo
                With info
                    .SetTitle(Title)
                    .SetAuthor(Author)
                    .SetSubject(Subject)
                    .SetKeywords(Keywords)
                End With
                document.SetXmpMetadata(XMPMetaFactory.Create)
            End Using
        End Using
    End Sub
End Class
