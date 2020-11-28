'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Public Class PdfMetadataHelper
    Private ReadOnly m_PdfPath As String
    Private ReadOnly m_PdfPassword As SecureString

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="pdfPath">Source PDF.</param>
    ''' <param name="pdfPassword">Source PDF Owner password.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal pdfPath As String, _
                   ByVal pdfPassword As SecureString)
        m_PdfPath = pdfPath
        m_PdfPassword = pdfPassword
    End Sub

    ''' <summary>
    ''' Reads the metadata from the PDF specified during instantiation into a
    ''' PdfMetadataReader object.
    ''' </summary>
    ''' <returns>
    ''' PdfMetadataReader object containing the metadata.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function Read() As PdfMetadataReader
        If m_PdfPassword Is Nothing Then
            Return New PdfMetadataReader(m_PdfPath)
        Else
            Return New PdfMetadataReader(m_PdfPath, m_PdfPassword)
        End If
    End Function

    ''' <summary>
    ''' Writes the specified PDF from the PDF specified during instantiation
    ''' applying the specified Upload Folder Configuration.
    ''' </summary>
    ''' <param name="outputPdfPath"></param>
    ''' <param name="uploadFolderConfigName"></param>
    ''' <remarks></remarks>
    Public Sub Write(ByVal outputPdfPath As String, _
                     ByVal uploadFolderConfigName As String)
        Dim writer As New PdfMetadataWriter(m_PdfPath, outputPdfPath)
        Dim uploadConfigHelper As _
            New UploadFolderConfigurationHelper(uploadFolderConfigName)
        Dim uploadConfig As UploadFolderConfiguration = uploadConfigHelper.Read
        If uploadConfig.TitlePrefill = My.Resources.DateTimeToken Then
            writer.Title = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", _
                                                 CultureInfo.CurrentCulture)
        ElseIf uploadConfig.TitlePrefill = My.Resources.DateToken Then
            writer.Title = DateTime.Now.ToString("yyyy-MM-dd", _
                                                 CultureInfo.CurrentCulture)
        ElseIf uploadConfig.TitlePrefill = My.Resources.FileNameToken Then
            writer.Title = Path.GetFileNameWithoutExtension(m_PdfPath)
        Else
            writer.Title = uploadConfig.TitlePrefill
        End If
        writer.Author = uploadConfig.AuthorPrefill
        writer.Subject = uploadConfig.SubjectPrefill
        writer.Keywords = uploadConfig.KeywordsPrefill
        writer.Write()
    End Sub

    ''' <summary>
    ''' Writes the specified PDF from the PDF specified during instantiation
    ''' applying the specified metadata (title, author, subject, and keywords).
    ''' </summary>
    ''' <param name="outputPdfPath"></param>
    ''' <param name="title"></param>
    ''' <param name="author"></param>
    ''' <param name="subject"></param>
    ''' <param name="keywords"></param>
    ''' <remarks></remarks>
    Public Sub Write(ByVal outputPdfPath As String, _
                     ByVal title As String, _
                     ByVal author As String, _
                     ByVal subject As String, _
                     ByVal keywords As String)
        Dim writer As PdfMetadataWriter = Nothing
        If m_PdfPassword Is Nothing Then
            writer = New PdfMetadataWriter(m_PdfPath, outputPdfPath)
        Else
            writer = New PdfMetadataWriter(m_PdfPath,
                                           m_PdfPassword,
                                           outputPdfPath)
        End If
        With writer
            .Title = title
            .Author = author
            .Subject = subject
            .Keywords = keywords
        End With
        writer.Write()
    End Sub
End Class
