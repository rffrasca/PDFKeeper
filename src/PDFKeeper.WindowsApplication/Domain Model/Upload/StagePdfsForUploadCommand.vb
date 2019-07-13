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
Public Class StagePdfsForUploadCommand
    Implements ICommand

    Public Sub Execute() Implements ICommand.Execute
        Dim pdfs = Directory.GetFiles(UserProfile.UploadPath, _
                                      "*.pdf", _
                                      SearchOption.AllDirectories).OrderBy( _
                                      Function(f) New FileInfo(f).LastWriteTime)
        For Each pdf In pdfs
            StagePdfAndSupplementalData(pdf)
        Next
    End Sub

    Private Shared Sub StagePdfAndSupplementalData(ByVal pdfPath As String)
        Dim fileInfo As New FileInfo(pdfPath)
        fileInfo.WaitWhileIsInUse()
        Dim pdfInfo As New PdfFileInfo(pdfPath)
        If pdfInfo.ContainsOwnerPassword = False Then
            Dim uploadFolderName As String = _
                pdfPath.Substring(UserProfile.UploadPath.Length + 1)
            If uploadFolderName = Path.GetFileName(pdfPath) Then
                uploadFolderName = UserProfile.UploadPath
            Else
                uploadFolderName = _
                    uploadFolderName.Substring(0, _
                                               uploadFolderName.IndexOf( _
                                                   Path.DirectorySeparatorChar))
            End If
            Try
                Dim outputPdfPath As String = fileInfo.GenerateUploadStagingFilePath
                Dim pdfReader As New PdfInformationPropertiesReader(pdfPath)
                If UploadFolderConfigurationUtil.IsFolderConfigured(uploadFolderName) Then
                    WriteNewPdfAndSupplementalData(pdfPath, _
                                                   outputPdfPath, _
                                                   uploadFolderName)
                Else
                    If pdfReader.Title IsNot Nothing And _
                        pdfReader.Author IsNot Nothing And _
                        pdfReader.Subject IsNot Nothing Then
                        StageExistingPdfAndSupplementalData(pdfPath, _
                                                            outputPdfPath)
                    End If
                End If
            Catch ex As BadPasswordException    ' Ignore the file.            
            End Try
        End If
    End Sub

    Private Shared Sub WriteNewPdfAndSupplementalData(ByVal inputPdfPath As String, _
                                                      ByVal outputPdfPath As String, _
                                                      ByVal uploadFolderConfigName As String)
        Dim pdfInfoPropHelper As New PdfInformationPropertiesHelper(inputPdfPath, Nothing)
        pdfInfoPropHelper.Write(outputPdfPath, uploadFolderConfigName)
        Dim uploadFolderConfigHelper As _
            New UploadFolderConfigurationHelper(uploadFolderConfigName)
        Dim uploadFolderConfig As UploadFolderConfiguration = _
            uploadFolderConfigHelper.Read
        Dim suppDataHelper As New PdfSupplementalDataHelper(outputPdfPath)
        Dim suppData As New PdfSupplementalData
        Dim flag As String = uploadFolderConfig.FlagDocument.ToString
        Dim state As Integer = 0
        If flag Then
            state = 1
        End If
        With suppData
            .Notes = String.Empty
            .Category = uploadFolderConfig.CategoryPrefill
            .FlagState = state
        End With
        suppDataHelper.Write(suppData.Notes, _
                             suppData.Category, _
                             suppData.FlagState)
        Dim fileInfo As New FileInfo(inputPdfPath)
        fileInfo.DeleteToRecycleBin()
    End Sub

    Private Shared Sub StageExistingPdfAndSupplementalData(ByVal sourcePdfPath As String, _
                                                           ByVal targetPdfPath As String)
        Dim sourceXmlPath As String = Path.ChangeExtension(sourcePdfPath, "xml")
        Dim targetXmlPath As String = Path.ChangeExtension(targetPdfPath, "xml")
        IO.File.Move(sourcePdfPath, targetPdfPath)
        If IO.File.Exists(sourceXmlPath) Then
            IO.File.Move(sourceXmlPath, targetXmlPath)
        End If
    End Sub
End Class
