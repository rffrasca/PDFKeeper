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
Imports ImageMagick
Imports iText.Kernel.Crypto
Imports iText.Kernel.Pdf
Imports PDFKeeper.Common
Imports PDFKeeper.FileIO.PdfPasswordTypes

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
            Catch ex As iText.IO.IOException
                Return PdfPasswordType.Unknown
            End Try
        End Get
    End Property

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
End Class
