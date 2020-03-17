'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
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
Public Class PdfFileInfo
    Private fileInfo As FileInfo

    Public Sub New(ByVal pdfPath As String)
        fileInfo = New FileInfo(pdfPath)
    End Sub

    ''' <summary>
    ''' Returns True or False if the PDF file object contains an Owner
    ''' password. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' A BadPasswordException will be thrown when PDF is protected by a User
    ''' password.
    ''' </remarks>
    Public ReadOnly Property ContainsOwnerPassword As Boolean
        Get
            Using reader As New PdfReader(fileInfo.FullName)
                If reader.IsOpenedWithFullPermissions Then
                    Return False
                Else
                    Return True
                End If
            End Using
        End Get
    End Property

    ''' <summary>
    ''' Checks if the PDF file object exists. 
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
    ''' <remarks></remarks>
    Public Function GetPreviewImageToFile(ByVal resolution As Integer) As String
        Dim outputParam As String = Path.Combine(Path.GetDirectoryName(fileInfo.FullName), _
                                                 Path.GetFileNameWithoutExtension(fileInfo.FullName) & "-" & _
                                                 resolution)
        Using pdftopng As New Process
            pdftopng.StartInfo.FileName = Path.Combine(Application.StartupPath, _
                                                       "pdftopng.exe")
            pdftopng.StartInfo.Arguments = _
                "-f 1 -l 1 -r " & resolution & _
                " " & Chr(34) & fileInfo.FullName & Chr(34) & _
                " " & Chr(34) & outputParam & Chr(34)
            pdftopng.StartInfo.UseShellExecute = False
            pdftopng.StartInfo.CreateNoWindow = True
            pdftopng.Start()
            pdftopng.WaitForExit()
        End Using
        Return outputParam & "-000001.png"
    End Function

    ''' <summary>
    ''' Returns the text from PDF file object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1024:UsePropertiesWhereAppropriate")> _
    Public Function GetText() As String
        Using reader = New PdfReader(fileInfo.FullName)
            Dim textString As New StringBuilder
            For i As Integer = 1 To reader.NumberOfPages
                Dim strategy As parser.ITextExtractionStrategy = _
                    New parser.LocationTextExtractionStrategy
                Dim currentPage As String = _
                    parser.PdfTextExtractor.GetTextFromPage(reader, i, strategy)
                Dim lines As String() = currentPage.Split(ControlChars.Lf)
                For Each line As String In lines
                    textString.AppendLine(line)
                Next
            Next
            Return textString.ToString
        End Using
    End Function
End Class
