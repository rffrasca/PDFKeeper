'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2018 Robert F. Frasca
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
Public Class PdfFile
    Inherits FileBase

    Public Sub New(ByVal file As String)
        FullName = file
    End Sub

    ''' <summary>
    ''' Checks if PDF file contains an Owner password. 
    ''' </summary>
    ''' <value></value>
    ''' <returns>True or False</returns>
    ''' <remarks>
    ''' A BadPasswordException will be thrown when PDF is protected by a User
    ''' password.
    ''' </remarks>
    Public ReadOnly Property ContainsOwnerPassword As Boolean
        Get
            Using reader As New PdfReader(FullName)
                If reader.IsOpenedWithFullPermissions Then
                    Return False
                Else
                    Return True
                End If
            End Using
        End Get
    End Property

    ''' <summary>
    ''' Returns the version of the PDF document.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Single character value. For example, 4 is = 1.4 (or Acrobat 5)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PdfVersion As Char
        Get
            Using reader As New PdfReader(FullName)
                Return reader.PdfVersion
            End Using
        End Get
    End Property

    ''' <summary>
    ''' Gets the first page of the PDF file into a PNG image file.
    ''' </summary>
    ''' <param name="resolution">Resolution in DPI of output image.</param>
    ''' <returns>Full path name of the image file created.</returns>
    ''' <remarks></remarks>
    Public Function GetPreviewImageToFile(ByVal resolution As Integer) As String
        Dim outputParam As String = Path.Combine(Path.GetDirectoryName(FullName), _
                                                 Path.GetFileNameWithoutExtension(FullName) & "-" & _
                                                 resolution)
        Using pdftopng As New Process
            pdftopng.StartInfo.FileName = Path.Combine(Application.StartupPath, _
                                                       "pdftopng.exe")
            pdftopng.StartInfo.Arguments = _
                "-f 1 -l 1 -r " & resolution & _
                " " & Chr(34) & FullName & Chr(34) & _
                " " & Chr(34) & outputParam & Chr(34)
            pdftopng.StartInfo.UseShellExecute = False
            pdftopng.StartInfo.CreateNoWindow = True
            pdftopng.Start()
            pdftopng.WaitForExit()
        End Using
        Return outputParam & "-000001.png"
    End Function

    ''' <summary>
    ''' Gets the text from PDF file.
    ''' </summary>
    ''' <returns>Text extracted from PDF file.</returns>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1024:UsePropertiesWhereAppropriate")> _
    Public Function GetText() As String
        Using reader = New PdfReader(FullName)
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

    ''' <summary>
    ''' Opens PDF file for viewing.
    ''' </summary>
    ''' <param name="useDefaultApp">
    ''' True to use default application or False to use "Sumatra PDF".
    ''' </param>
    ''' <remarks></remarks>
    Public Sub Open(ByVal useDefaultApp As Boolean)
        If useDefaultApp = False Then
            OpenSumatraPdf(Chr(34) & FullName & Chr(34))
        Else
            Process.Start(FullName)
        End If
    End Sub

    ''' <summary>
    ''' Opens PDF file for restricted viewing.
    ''' </summary>
    ''' <returns>Process ID</returns>
    ''' <remarks>Uses "Sumatra PDF" in restricted mode.</remarks>
    Public Function OpenRestricted() As Integer
        Return OpenSumatraPdf("-restrict " & Chr(34) & FullName & Chr(34))
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    Private Function OpenSumatraPdf(ByVal param As String) As Integer
        Using sumatraPdf As New Process
            sumatraPdf.StartInfo.FileName = Path.Combine(Application.StartupPath, _
                                                         "SumatraPDF.exe")
            sumatraPdf.StartInfo.Arguments = param
            sumatraPdf.StartInfo.UseShellExecute = False
            sumatraPdf.Start()
            Return sumatraPdf.Id    ' Process ID
        End Using
    End Function
End Class
