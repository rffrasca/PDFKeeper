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
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Text
Imports ImageMagick
Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas.Parser
Imports PDFKeeper.Common
Imports PDFKeeper.FileIO.PdfPasswordTypes
Imports iText.Kernel.Pdf.Canvas.Parser.Util
Imports iText.Kernel.Exceptions

Public Class PdfFile
    Inherits PdfBase

    ''' <summary>
    ''' Creates an instance of the class.
    ''' </summary>
    ''' <param name="pdfFile">PDF file name</param>
    Public Sub New(ByVal pdfFile As String)
        Me.file = New FileInfo(pdfFile)
        MagickNET.SetGhostscriptDirectory(My.Application.Info.DirectoryPath)
        MagickNET.SetTempDirectory(AppFolders.GetPath(AppFolders.AppFolder.Temp))
        bouncyCastleRestore.Execute()
    End Sub

    ''' <summary>
    ''' Gets the password type set in the PDF.
    ''' </summary>
    ''' <returns>PDF password type</returns>
    Public ReadOnly Property PasswordType As PdfPasswordType
        Get
            Try
                Using reader = New PdfReader(file)
                    Using pdfDoc = New PdfDocument(reader)
                        If reader.IsOpenedWithFullPermission Then
                            Return PdfPasswordType.None
                        Else
                            Return PdfPasswordType.Owner
                        End If
                    End Using
                End Using
            Catch ex As BadPasswordException
                Return PdfPasswordType.User
            Catch ex As iText.IO.exceptions.IOException
                Return PdfPasswordType.Unknown
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Splits PDF into separate one page PDF files.
    ''' </summary>
    ''' <param name="destPath">Destination folder path</param>
    ''' <returns>Collection of PDF file path names</returns>
    Public Function Split(ByVal destPath As String) As Collection(Of FileInfo)
        Dim splitPdfFiles As New Collection(Of FileInfo)
        Using reader = New PdfReader(file)
            Using pdfDoc = New PdfDocument(reader)
                Dim splitter = New PdfFileSplitter(pdfDoc, destPath, file.Name)
                For Each splittedDoc In splitter.SplitByPageCount(1)
                    splittedDoc.Close()
                Next
            End Using
        End Using
        For Each splitPdfFile In Directory.GetFiles(destPath,
                                                    String.Concat(Path.GetFileNameWithoutExtension(file.Name),
                                                                  "_*.pdf"))
            splitPdfFiles.Add(New FileInfo(splitPdfFile))
        Next
        Return splitPdfFiles
    End Function

    ''' <summary>
    ''' Creates a preview image containing the first page of the PDF.
    ''' </summary>
    ''' <param name="pixelDensity">Output image pixels per inch (PPI)</param>
    ''' <returns>Preview image in PNG format as a byte array</returns>
    Public Function CreatePreviewImage(ByVal pixelDensity As Decimal) As Byte()
        Using image = New MagickImageCollection
            Dim settings = New MagickReadSettings With {
                .Density = New Density(pixelDensity),
                .FrameIndex = 0,
                .FrameCount = 1
            }
            image.Read(file.FullName, settings)
            Using output = New MemoryStream
                image.Write(output, MagickFormat.Png)
                Return output.ToArray()
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Gets a collection containing each page of the PDF as a TIFF image byte array.
    ''' </summary>
    ''' <returns>Collection of images as byte arrays</returns>
    Public Function GetAllPagesAsTiffImages() As ObjectModel.Collection(Of Byte())
        Dim imageList = New ObjectModel.Collection(Of Byte())
        Using images = New MagickImageCollection
            Dim settings = New MagickReadSettings With {
                .Density = New Density(600, 600),
                .Compression = CompressionMethod.LZW
            }
            images.Read(file.FullName, settings)
            For Each image In images
                Using output = New MemoryStream
                    image.Write(output, MagickFormat.Tiff)
                    imageList.Add(output.ToArray())
                End Using
            Next
        End Using
        Return imageList
    End Function

    ''' <summary>
    ''' Gets text annotations from the PDF.
    ''' </summary>
    ''' <returns>Text annotations</returns>
    Public Function GetTextAnnot() As String
        Using reader = New PdfReader(file)
            Dim output = New StringBuilder
            Using document = New PdfDocument(reader)
                For pageCounter = 1 To document.GetNumberOfPages
                    Dim page = document.GetPage(pageCounter)
                    Dim annotations = page.GetAnnotations()
                    For Each annotation In annotations
                        Dim dict = annotation.GetPdfObject
                        Dim text = dict.GetAsString(PdfName.Contents)
                        If text IsNot Nothing Then
                            output.AppendLine(text.ToUnicodeString)
                        End If
                    Next
                Next
            End Using
            Return output.ToString
        End Using
    End Function

    ''' <summary>
    ''' Gets text from the PDF using the appropriate extraction strategy for the PDF page being processed.
    ''' 
    ''' Text Extraction Strategy:
    ''' Uses iText for text based PDF page except when page contains an invalid encoding due to iText's strict
    ''' adherence to the PDF specification (ISO 32000) or when iText is unable to extract text from the page for
    ''' another reason.
    ''' 
    ''' OCR Text Extraction Strategy:
    ''' Uses OCR for text based PDF when page is rejected by iText or when the page is "Image-only". This strategy
    ''' will also be used when PDF page contains image data and ocrImageDataPages is True. 
    ''' </summary>
    ''' <param name="ocrImageDataPages">True or False to OCR each PDF page containing image data.</param>
    ''' <returns>Extracted text</returns>
    Public Function GetText(ByVal ocrImageDataPages As Boolean) As String
        Dim pdfText = New StringBuilder
        Dim strategy As IPdfTextExtractionStrategy
        For Each pdf In New PdfFile(file.FullName).Split(AppFolders.GetPath(AppFolders.AppFolder.Temp))
            Dim text As String = Nothing
            If ocrImageDataPages And CheckForImageData() Then
                strategy = New PdfOcrTextExtractionStrategy
                text = strategy.GetText(pdf)
            Else
                strategy = New PdfTextExtractionStrategy
                text = strategy.GetText(pdf)
                If text Is Nothing Then
                    strategy = New PdfOcrTextExtractionStrategy
                    text = strategy.GetText(pdf)
                ElseIf text.Trim.Length = 0 Then
                    strategy = New PdfOcrTextExtractionStrategy
                    text = strategy.GetText(pdf)
                End If
            End If
            pdfText.Append(text)
            pdf.Delete()
        Next
        Return pdfText.ToString
    End Function

    Private Function CheckForImageData() As Boolean
        Dim result = False
        Using reader = New PdfReader(file)
            Using pdfDoc = New PdfDocument(reader)
                For page = 1 To pdfDoc.GetNumberOfPages
                    Try
                        Dim detector = New PdfImageDetector
                        Dim canvasProcessor = New PdfCanvasProcessor(detector)
                        canvasProcessor.ProcessPageContent(pdfDoc.GetPage(1))
                        If detector.Detected Then
                            result = True
                        End If
                    Catch ex As ArgumentException   ' PDF contains an invalid encoding.
                        Return result
                    Catch ex As InlineImageParsingUtils.InlineImageParseException
                        Return result
                    End Try
                Next
            End Using
        End Using
        Return result
    End Function
End Class
