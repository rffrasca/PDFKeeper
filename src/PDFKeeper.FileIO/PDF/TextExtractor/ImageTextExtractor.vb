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
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text
Imports PDFKeeper.Common
Imports Windows.Graphics.Imaging
Imports Windows.Media.Ocr

Public Class ImageTextExtractor
    Private ReadOnly images As Collection(Of Byte())
    Private ReadOnly format As ImageFormat

    ''' <summary>
    ''' Creates an instance of the class.
    ''' 
    ''' PlatformNotSupportedException will be thrown when operating system is not Windows 10.
    ''' </summary>
    ''' <param name="images">Collection of images as byte arrays</param>
    ''' <param name="format">Image format</param>
    Public Sub New(ByVal images As Collection(Of Byte()), ByVal format As ImageFormat)
        If Not My.Computer.Info.OSFullName.Contains("Windows 10") Then
            Throw New PlatformNotSupportedException
        End If
        Me.images = images
        Me.format = format
    End Sub

    ''' <summary>
    ''' Extracts text from a collection of images using Windows OCR.
    ''' </summary>
    ''' <returns>Text</returns>
    Public Async Function GetText() As Task(Of String)
        Dim text = New StringBuilder
        For Each image In images
            Try
                Dim ImageFile = Path.Combine(AppFolders.GetPath(AppFolders.AppFolder.Temp),
                                             String.Concat(Guid.NewGuid, ".", format.ToString))
                image.ToFile(imageFile)
                Using stream = File.Open(imageFile, FileMode.Open, FileAccess.Read)
                    Dim bmpDecoder = Await BitmapDecoder.CreateAsync(
                        stream.AsRandomAccessStream()).AsTask.ConfigureAwait(False)
                    Using softwareBmp = Await bmpDecoder.GetSoftwareBitmapAsync
                        Dim ocrEngine As OcrEngine = OcrEngine.TryCreateFromUserProfileLanguages
                        Dim ocrResult = Await ocrEngine.RecognizeAsync(softwareBmp)
                        For Each line In ocrResult.Lines
                            text.AppendLine(line.Text)
                        Next
                    End Using
                End Using
                File.Delete(imageFile)
            Catch ex As ArithmeticException
            End Try
        Next
        Return text.ToString()
    End Function
End Class
