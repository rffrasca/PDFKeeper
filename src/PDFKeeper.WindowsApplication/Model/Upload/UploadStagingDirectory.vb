'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
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
Public NotInheritable Class UploadStagingDirectory
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared Sub Explore()
        Process.Start(ApplicationDirectories.UploadStaging)
    End Sub

    Public Shared Sub UploadAllPdfFiles()
        Dim files = Directory.GetFiles(ApplicationDirectories.UploadStaging, _
                                       "*.pdf", _
                                       SearchOption.TopDirectoryOnly).OrderBy( _
                                       Function(f) New FileInfo(f).LastWriteTime)
        For Each file In files
            FileHelper.WaitWhileFileIsInUse(file)
            UploadPdfFile(file)
        Next
    End Sub

    Private Shared Sub UploadPdfFile(ByVal file As String)
        Dim pdfFile As New PdfFile(file)
        If pdfFile.ContainsOwnerPassword = False Then
            Try
                Dim pdfReader As New PdfFileInfoPropertiesReader(file)
                If pdfReader.Title IsNot Nothing And _
                    pdfReader.Author IsNot Nothing And _
                    pdfReader.Subject IsNot Nothing Then
                    Dim notes As String = Nothing
                    Dim flag As Integer = 0
                    Dim suppData As New PdfFileSupplementalData
                    Dim suppDataXml As String = Path.ChangeExtension(file, _
                                                                     "xml")
                    If IO.File.Exists(suppDataXml) Then
                        SerializerHelper.FromXmlToObj(suppData, suppDataXml)
                        notes = suppData.Notes
                        flag = suppData.FlagState
                    End If
                    Dim docsDao As IDocsDao = New DocsDao
                    docsDao.CreateRecord(pdfReader.Title, _
                                         pdfReader.Author, _
                                         pdfReader.Subject, _
                                         pdfReader.Keywords, _
                                         notes, _
                                         file, _
                                         flag)
                    FileHelper.DeleteFileToRecycleBin(file)
                    If notes.Length > 0 Then
                        FileHelper.DeleteFileToRecycleBin(suppDataXml)
                    Else
                        IO.File.Delete(suppDataXml)
                    End If
                End If
            Catch ex As BadPasswordException    ' Ignore the file.
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        End If
    End Sub
End Class
