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
Imports System.IO
Imports System.Text
Imports Windows.Graphics.Imaging
Imports Windows.Media.Ocr

Public Class ImageTextExtractor
    Private ReadOnly imageFiles As ArrayList

    ''' <summary>
    ''' Class constructor.
    ''' 
    ''' Exceptions:
    ''' PlatformNotSupportedException: Operating System is not Windows 10.
    ''' </summary>
    ''' <param name="imageFiles">Array of image path names to OCR.</param>
    Public Sub New(ByVal imageFiles As ArrayList)
        If Not My.Computer.Info.OSFullName.Contains("Windows 10") Then
            Throw New PlatformNotSupportedException
        End If
        Me.imageFiles = imageFiles
    End Sub

    ''' <summary>
    ''' Extracts the text from the array of images using Windows OCR.
    ''' </summary>
    ''' <returns>Text</returns>
    Public Async Function GetText() As Task(Of String)
        Dim text As New StringBuilder
        For Each imageFile As String In imageFiles
            Using stream = File.Open(imageFile, FileMode.Open, FileAccess.Read)
                Dim bmpDecoder = Await BitmapDecoder.CreateAsync(
                    stream.AsRandomAccessStream()).AsTask.ConfigureAwait(False)
                Dim softwareBmp = Await bmpDecoder.GetSoftwareBitmapAsync()
                Dim ocrEngine As OcrEngine = OcrEngine.TryCreateFromUserProfileLanguages
                Dim ocrResult = Await ocrEngine.RecognizeAsync(softwareBmp)
                For Each line In ocrResult.Lines
                    text.AppendLine(line.Text)
                Next
                softwareBmp.Dispose()
            End Using
        Next
        Return text.ToString()
    End Function
End Class
