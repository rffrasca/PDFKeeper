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
            ' Skip uploading the PDF document if the option is set to not upload
            ' PDF 1.7 documents. This option was added in v5.0.0 as Oracle Text
            ' in Oracle Databases prior to version 12c do not index PDF 1.7 
            ' documents. This PDF version check will be deprecated once Oracle
            ' drops support for 11g and eventually will be removed.
            If My.Settings.DoNotUploadPdf17Documents Then
                Dim pdfFile As New PdfFile(file)
                If pdfFile.PdfVersion <> "7" Then
                    UploadPdfFile(file)
                End If
            Else
                UploadPdfFile(file)
            End If
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
                    Dim txtFile As String = Path.ChangeExtension(file, "txt")
                    If IO.File.Exists(txtFile) Then
                        notes = IO.File.ReadAllText(txtFile)
                    End If
                    Dim nonQueryService As INonQueryService = Nothing
                    NonQueryServiceHelper.SetNonQueryService(nonQueryService)
                    nonQueryService.InsertDocument(pdfReader.Title, _
                                                   pdfReader.Author, _
                                                   pdfReader.Subject, _
                                                   pdfReader.Keywords, _
                                                   notes, _
                                                   file)
                    FileHelper.DeleteFileToRecycleBin(file)
                    FileHelper.DeleteFileToRecycleBin(txtFile)
                End If
            Catch ex As BadPasswordException    ' Ignore the file.
            Catch ex As OracleException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            End Try
        End If
    End Sub
End Class
