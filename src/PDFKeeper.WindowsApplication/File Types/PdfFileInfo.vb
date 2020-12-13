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
Public Class PdfFileInfo
    Private ReadOnly fileInfo As FileInfo

    Public Sub New(ByVal pdfPath As String)
        fileInfo = New FileInfo(pdfPath)
    End Sub

    ''' <summary>
    ''' If the PDF file object contains an Owner password.
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
    ''' If the PDF file object exists. 
    ''' </summary>
    ''' <value></value>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Exists As Boolean
        Get
            Return fileInfo.Exists
        End Get
    End Property

    ''' <summary>
    ''' Gets the path name of the PDF file object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FullName As String
        Get
            Return fileInfo.FullName
        End Get
    End Property

    ''' <summary>
    ''' Returns the hash value for the PDF file object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ComputeHash() As String
        Return fileInfo.ComputeHash
    End Function

    ''' <summary>
    ''' Copies the PDF file object to a new file path.
    ''' </summary>
    ''' <param name="targetPdfPath"></param>
    ''' <remarks></remarks>
    Public Sub CopyTo(ByVal targetPdfPath As String)
        fileInfo.CopyTo(targetPdfPath, True)
    End Sub

    ''' <summary>
    ''' Creates a PNG image file containing the first page of the PDF file
    ''' object.
    ''' </summary>
    ''' <param name="resolution">DPI of output image.</param>
    ''' <returns>Path name of file that contains the image.</returns>
    Public Function GetPreviewImageToFile(ByVal resolution As Integer) As String
        Dim outputParam As String = Path.Combine(Path.GetDirectoryName(fileInfo.FullName),
                                                 Path.GetFileNameWithoutExtension(fileInfo.FullName) & "-" &
                                                 resolution & ".png")
        MagickNET.SetGhostscriptDirectory(Application.StartupPath)
        Using imageCollection = New MagickImageCollection
            Dim settings = New MagickReadSettings With {
                .Density = New Density(resolution),
                .FrameIndex = 0,
                .FrameCount = 1
            }
            imageCollection.Read(fileInfo.FullName, settings)
            imageCollection.Write(outputParam)
        End Using
        Return outputParam
    End Function

    ''' <summary>
    ''' Returns the text from PDF file object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetText() As String
        Using reader = New PdfReader(fileInfo.FullName)
            Dim textString As New StringBuilder
            Using pdfDoc As New PdfDocument(reader)
                For page As Integer = 1 To pdfDoc.GetNumberOfPages
                    Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
                    Dim pageText As String = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page),
                                                                              strategy)
                    pageText = Encoding.UTF8.GetString(
                        ASCIIEncoding.Convert(Encoding.Default,
                                              Encoding.UTF8, Encoding.Default.GetBytes(pageText)))
                    Dim lines As String() = pageText.Split(ControlChars.Lf)
                    For Each line In lines
                        textString.AppendLine(line)
                    Next
                Next
            End Using
            Return textString.ToString
        End Using
    End Function
End Class
