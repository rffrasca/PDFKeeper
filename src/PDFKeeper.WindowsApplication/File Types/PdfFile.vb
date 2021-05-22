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
Imports iText.Kernel.Pdf
Imports UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor

Public Class PdfFile
    Inherits FileBase

    Public Sub New(ByVal pdfPath As String)
        fileInfo = New FileInfo(pdfPath)
    End Sub

    ''' <summary>
    ''' If the PDF contains an Owner password.
    ''' </summary>
    ''' <value></value>
    ''' <returns>True or False</returns>
    ''' <remarks>
    ''' A BadPasswordException will be thrown when PDF is protected by a User
    ''' password.
    ''' </remarks>
    Public ReadOnly Property ContainsOwnerPassword As Boolean
        Get
            Using reader As New PdfReader(fileInfo.FullName)
                Using pdfDoc As New PdfDocument(reader)
                    If reader.IsOpenedWithFullPermission Then
                        Return False
                    Else
                        Return True
                    End If
                End Using
            End Using
        End Get
    End Property

    ''' <summary>
    ''' If the PDF is image-only.
    ''' </summary>
    ''' <returns>True or False</returns>
    Public ReadOnly Property ImageOnly As Boolean
        Get
            If GetText.Length > 0 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Generates a preview image in PNG format containing the first page of
    ''' the PDF.
    ''' </summary>
    ''' <param name="resolution">DPI of output image.</param>
    ''' <returns>Path name of preview file.</returns>
    Public Function GeneratePreviewImageFile(ByVal resolution As Integer) As String
        Dim previewFile As String = IO.Path.Combine(IO.Path.GetDirectoryName(fileInfo.FullName),
                                                    IO.Path.GetFileNameWithoutExtension(fileInfo.FullName) & "-" &
                                                    resolution & ".png")
        Using imageCollection = New MagickImageCollection
            Dim settings = New MagickReadSettings With {
                .Density = New Density(resolution),
                .FrameIndex = 0,
                .FrameCount = 1
            }
            imageCollection.Read(fileInfo.FullName, settings)
            imageCollection.Write(previewFile)
        End Using
        Return previewFile
    End Function

    '''' <summary>
    '''' Returns the text annotations from the PDF.
    '''' </summary>
    '''' <returns></returns>
    Public Function GetTextAnnotations() As String
        Using reader = New PdfReader(fileInfo.FullName)
            Dim textString As New StringBuilder
            Using pdfDoc As New PdfDocument(reader)
                For page As Integer = 1 To pdfDoc.GetNumberOfPages
                    Dim annotPage As PdfPage = pdfDoc.GetPage(page)
                    Dim annotations = annotPage.GetAnnotations()
                    For Each annotation In annotations
                        Dim annotDict As PdfDictionary = annotation.GetPdfObject
                        Dim text As PdfString = Nothing
                        text = annotDict.GetAsString(PdfName.Contents)
                        If text IsNot Nothing Then
                            textString.AppendLine(text.ToUnicodeString)
                        End If
                    Next
                Next
            End Using
            Return textString.ToString
        End Using
    End Function

    ''' <summary>
    ''' Returns the text from the PDF.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetText() As String
        Dim factory As New PdfTextExtractorFactory(fileInfo.FullName)
        Return factory.GetText
    End Function
End Class
