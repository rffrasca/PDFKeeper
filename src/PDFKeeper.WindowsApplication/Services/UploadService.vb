﻿'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Public NotInheritable Class UploadService
    Private Shared executing As Boolean
    Private Shared paused As Boolean

    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared ReadOnly Property CanUploadBeExecuted As Boolean
        Get
            If executing Or paused Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public Shared Sub ExecuteUpload()
        If CanUploadBeExecuted = False Then
            Throw New InvalidOperationException( _
                My.Resources.UploadCannotBeStarted)
        End If
        executing = True
        EnsureConfiguredUploadFoldersExist()
        StagePdfsAndSupplementalDataForUpload()
        UploadFoldersCleanup()
        UploadStagedPdfsAndSupplementalData()
        executing = False
    End Sub

    Public Shared Sub PauseUpload(ByVal value As Boolean)
        WaitForUploadToFinish()
        paused = value
    End Sub

    Public Shared Sub WaitForUploadToFinish()
        Do While executing
            Threading.Thread.Sleep(1000)
        Loop
    End Sub

#Region "Step 1: EnsureConfiguredUploadFoldersExist"
    ''' <summary>
    ''' Ensures a folder for each Upload Configuration exists in the Upload
    ''' folder.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub EnsureConfiguredUploadFoldersExist()
        For Each config As String In _
            UploadFolderConfigurationUtil.GetAllConfigFileNames(True, False)
            Directory.CreateDirectory(Path.Combine(UserProfile.UploadPath, _
                                                   config))
        Next
    End Sub
#End Region

#Region "Step 2: StagePdfsAndSupplementalDataForUpload"
    Private Shared Sub StagePdfsAndSupplementalDataForUpload()
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
#End Region

#Region "Step 3: UploadFoldersCleanup"
    ''' <summary>
    ''' Deletes all empty non configured folders from the Upload folder and any
    ''' empty sub-folders under each configured upload folder.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub UploadFoldersCleanup()
        Dim subFolders As String() = _
            Directory.GetDirectories(UserProfile.UploadPath)
        For Each subFolder In subFolders
            Dim dirInfo As New DirectoryInfo(subFolder)
            If UploadFolderConfigurationUtil.IsFolderConfigured( _
                dirInfo.Name) Then
                ' When the folder is a configured folder then only
                ' delete empty sub-folders under the configured folder.
                Dim subFoldersL2 As String() = _
                    Directory.GetDirectories(subFolder)
                For Each subFolderL2 In subFoldersL2
                    If Directory.GetFiles(subFolderL2).Count = 0 Then
                        Directory.Delete(subFolderL2, True)
                    End If
                Next
            Else
                If Directory.GetFiles(subFolder).Count = 0 Then
                    Directory.Delete(subFolder, True)
                End If
            End If
        Next
    End Sub
#End Region

#Region "Step 4: UploadStagedPdfsAndSupplementalData"
    ''' <summary>
    ''' Uploads all PDF files and supplemental data in the UploadStaging
    ''' folder. 
    ''' </summary>
    ''' <remarks>
    ''' Password protected PDF files will not be uploaded.
    ''' </remarks>
    Private Shared Sub UploadStagedPdfsAndSupplementalData()
        Dim pdfs = Directory.GetFiles(UserProfile.UploadStagingPath, _
                                          "*.pdf", _
                                          SearchOption.TopDirectoryOnly).OrderBy( _
                                          Function(f) New FileInfo(f).LastWriteTime)
        For Each pdf In pdfs
            UploadPdfAndSupplementalData(pdf)
        Next
    End Sub

    Private Shared Sub UploadPdfAndSupplementalData(ByVal pdfPath As String)
        Dim fileInfo As New FileInfo(pdfPath)
        fileInfo.WaitWhileIsInUse()
        Dim pdfInfo As New PdfFileInfo(pdfPath)
        If pdfInfo.ContainsOwnerPassword = False Then
            Try
                Dim pdfReader As New PdfInformationPropertiesReader(pdfPath)
                If pdfReader.Title IsNot Nothing And _
                    pdfReader.Author IsNot Nothing And _
                    pdfReader.Subject IsNot Nothing Then
                    Dim notes As String = Nothing
                    Dim category As String = Nothing
                    Dim flag As Integer = 0
                    Dim suppDataHelper As New PdfSupplementalDataHelper(pdfPath)
                    Dim suppData As PdfSupplementalData = suppDataHelper.Read
                    If suppData IsNot Nothing Then
                        notes = suppData.Notes
                        category = suppData.Category
                        flag = suppData.FlagState
                    End If
                    Dim dataClient As IDataClient = New DataClient
                    dataClient.CreateRecord(pdfReader.Title, _
                                            pdfReader.Author, _
                                            pdfReader.Subject, _
                                            pdfReader.Keywords, _
                                            notes, _
                                            pdfPath, _
                                            category, _
                                            flag)
                    IO.File.Delete(pdfPath)
                    Dim suppDataXmlPath As String = _
                        Path.ChangeExtension(pdfPath, "xml")
                    IO.File.Delete(suppDataXmlPath)
                End If
            Catch ex As BadPasswordException    ' Ignore the file.            
            End Try
        End If
    End Sub
#End Region

End Class