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
Public Class PdfInformationPropertiesReader
    Inherits PdfInformationProperties

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
    Public Sub New(ByVal pdfPath As String, _
                   ByVal pdfPassword As SecureString)
        Read(pdfPath, pdfPassword)
    End Sub

    Private Sub Read(ByVal pdfPath As String)
        Using reader As New PdfReader(pdfPath)
            GetProperties(reader)
        End Using
    End Sub

    Private Sub Read(ByVal pdfPath As String, _
                     ByVal pdfPassword As SecureString)
        Using reader As New PdfReader(pdfPath, _
                                      System.Text.Encoding.ASCII.GetBytes( _
                                          pdfPassword.SecureStringToString))
            GetProperties(reader)
        End Using
    End Sub

    Private Sub GetProperties(ByVal reader As PdfReader)
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
