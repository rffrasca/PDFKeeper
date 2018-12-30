'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
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
Public Class FileSaveAsToolStripCommand
    Implements ICommand
    Private m_id As Integer
    Private m_text As String

    Public Sub New(ByVal id As Integer, ByVal text As String)
        m_id = id
        m_text = text
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        Dim targetExtension As String
        Dim targetFile As String
        Dim pdfFile As New PdfFile( _
                    FilePathNameGenerator.GenerateCachePdfFilePathName(m_id))
        Dim pdfInfo As New PdfFileInfoPropertiesReader(pdfFile.FullName)
        If m_text Is Nothing Then
            targetExtension = "pdf"
        Else
            targetExtension = "txt"
        End If
        targetFile = GetTargetFileName(pdfInfo.Title, targetExtension)
        If targetFile.Length > 0 Then
            If m_text Is Nothing Then
                pdfFile.CopyTo(targetFile)
            Else
                m_text.SaveToFile(targetFile)
            End If
            Dim displayService As IMessageDisplayService = New MessageDisplayService
            displayService.ShowInformation(String.Format(CultureInfo.CurrentCulture, _
                                                         My.Resources.ResourceManager.GetString("FileSaved"), _
                                                         targetFile))
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Private Function GetTargetFileName(ByVal fileNamePrefill As String, _
                                       ByVal fileExtension As String) As String
        Dim fileService As IFileDialogDisplayService = _
            New FileDialogDisplayService(fileNamePrefill, fileExtension)
        Return fileService.SaveDialog
    End Function
End Class
