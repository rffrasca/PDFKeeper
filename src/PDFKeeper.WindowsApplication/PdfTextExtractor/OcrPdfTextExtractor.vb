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
Imports PDFKeeper.Ocr

Public Class OcrPdfTextExtractor
    Inherits PdfTextExtractorBase

    Public Sub New(ByVal pdfFile As String)
        MyBase.New(pdfFile)
    End Sub

    ''' <summary>
    ''' Gets text from the PDF using OCR.
    ''' </summary>
    ''' <returns>Text or Nothing if platform is not supported.</returns>
    Public Overrides Function GetText() As String
        Dim tiffFiles As New ArrayList(GetAllPagesAsTiffFiles)
        Try
            Dim ocr As New ImageTextExtractor(tiffFiles)
            Dim result As Task(Of String) = ocr.GetText
            Return result.Result
        Catch ex As PlatformNotSupportedException
            Return Nothing
        Finally
            CleanupFiles(tiffFiles)
        End Try
    End Function

    Private Function GetAllPagesAsTiffFiles() As ArrayList
        Dim imagesList As New ArrayList
        Using images = New MagickImageCollection
            Dim settings = New MagickReadSettings With {
                .Density = New Density(600, 600),
                .Compression = CompressionMethod.LZW
                }
            images.Read(pdfFile, settings)
            Dim page = 1
            For Each image In images
                Dim imageFile As String =
                    Path.Combine(Path.GetTempPath,
                                 Path.GetFileNameWithoutExtension(pdfFile) & "." & page & ".tiff")
                image.Write(imageFile)
                imagesList.Add(imageFile)
                page += 1
            Next
        End Using
        Return imagesList
    End Function

    Private Shared Sub CleanupFiles(ByVal filePaths As ArrayList)
        For Each filePath As String In filePaths
            Dim filePathInfo As New FileInfo(filePath)
            filePathInfo.WaitWhileIsInUse
            filePathInfo.Delete()
        Next
    End Sub
End Class
