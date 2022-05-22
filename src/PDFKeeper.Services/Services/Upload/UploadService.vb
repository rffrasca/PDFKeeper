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
Imports System.Globalization
Imports System.IO
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.FileIO
Imports PDFKeeper.Infrastructure

Public Class UploadService
    Inherits PdfService
    Implements IUploadService
    Private ReadOnly repository As IXmlRepository(Of UploadProfileModel)
    Private ReadOnly documentSvc As IDocumentService
    Private ReadOnly uploadProfileSvc As IUploadProfileService
    Private _DocumentsUploaded As Boolean

    Public Sub New(ByVal repository As IXmlRepository(Of UploadProfileModel), ByVal documentSvc As IDocumentService,
                   ByVal uploadProfileSvc As IUploadProfileService)
        Me.repository = repository
        Me.documentSvc = documentSvc
        Me.uploadProfileSvc = uploadProfileSvc
    End Sub

    Public ReadOnly Property PdfFilesToUploadExist As Boolean Implements IUploadService.PdfFilesToUploadExist
        Get
            If GetPdfFiles(AppFolders.AppFolder.Upload).Any Or GetPdfFiles(AppFolders.AppFolder.UploadStaging).Any Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property DocumentsUploaded As Boolean Implements IUploadService.DocumentsUploaded
        Get
            Return _DocumentsUploaded
        End Get
    End Property

    Public ReadOnly Property RejectedPdfFilesExist As Boolean Implements IUploadService.RejectedPdfFilesExist
        Get
            Return Directory.GetFiles(AppFolders.GetPath(AppFolders.AppFolder.UploadRejected),
                                      "*.pdf", SearchOption.AllDirectories).Any
        End Get
    End Property

    Public Sub ExecuteUploadFolderMaintenance() Implements IUploadService.ExecuteUploadFolderMaintenance
        CreateMissingUploadFolders()
        DeleteDormantUploadFolders()
    End Sub

    Public Sub ExecuteUpload() Implements IUploadService.ExecuteUpload
        StagePdfFilesInUpload()
        UploadStagedPdfFiles()
    End Sub

    Private Sub CreateMissingUploadFolders()
        For Each profile In repository.ListItems
            Directory.CreateDirectory(Path.Combine(AppFolders.GetPath(AppFolders.AppFolder.Upload), profile))
        Next
    End Sub

    Private Sub DeleteDormantUploadFolders()
        For Each folder In Directory.GetDirectories(AppFolders.GetPath(AppFolders.AppFolder.Upload))
            If repository.ItemExists(Path.GetFileName(folder)) Then
                For Each folderL2 In Directory.GetDirectories(folder)
                    If Directory.GetFiles(folderL2).Any = False Then
                        Directory.Delete(folderL2, True)
                    End If
                Next
            Else
                If Directory.GetFiles(folder).Any = False Then
                    Directory.Delete(folder, True)
                End If
            End If
        Next
    End Sub

    Private Shared Function GetPdfFiles(ByVal folder As AppFolders.AppFolder) As Collection(Of FileInfo)
        Dim docs = New Collection(Of FileInfo)
        Dim dir = New DirectoryInfo(AppFolders.GetPath(folder))
        For Each file In dir.GetPdfFilesOrderByLastWriteTime
            docs.Add(New FileInfo(file))
        Next
        Return docs
    End Function

    Private Sub StagePdfFilesInUpload()
        For Each pdf In GetPdfFiles(AppFolders.AppFolder.Upload)
            pdf.WaitWhileLocked
            Dim pdfFile = New PdfFile(pdf.FullName)
            If pdfFile.PasswordType = PdfPasswordTypes.PdfPasswordType.None Then
                Dim xmlFile = New FileInfo(Path.ChangeExtension(pdf.FullName, "xml"))
                Dim folderName = pdf.FullName.Substring(AppFolders.GetPath(AppFolders.AppFolder.Upload).Length + 1)
                If folderName = pdf.Name Then
                    folderName = AppFolders.GetPath(AppFolders.AppFolder.Upload)
                Else
                    folderName = folderName.Substring(0, folderName.IndexOf(Path.DirectorySeparatorChar))
                End If
                If uploadProfileSvc.ProfileExists(folderName) Then
                    Dim profile = uploadProfileSvc.ReadProfile(folderName)
                    Dim pdfInfo = ReadPdfInfo(pdf.FullName, Nothing)
                    With pdfInfo
                        If profile.Title = My.Resources.DateTimeToken Then
                            .Title = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture)
                        ElseIf profile.Title = My.Resources.DateToken Then
                            .Title = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture)
                        ElseIf profile.Title = My.Resources.FileNameToken Then
                            .Title = Path.GetFileNameWithoutExtension(pdf.FullName)
                        Else
                            .Title = profile.Title
                        End If
                        .Author = profile.Author
                        .Subject = profile.Subject
                        .Keywords = profile.Keywords
                    End With
                    Dim stagedPdfPath = pdf.GenerateNewPathNameWithGuid(
                        AppFolders.GetPath(AppFolders.AppFolder.UploadStaging))
                    Dim link = Path.ChangeExtension(pdf.FullName, "link")
                    If File.Exists(link) Then
                        stagedPdfPath = New FileInfo(File.ReadAllText(link))
                    Else
                        File.WriteAllText(link, stagedPdfPath.FullName)
                    End If
                    WritePdfWithInfo(pdf.FullName, Nothing, stagedPdfPath.FullName, pdfInfo)
                    Dim pdfInfoExt = New PdfInfoExtModel
                    If xmlFile.Exists Then
                        pdfInfoExt = ReadPdfInfoExt(pdf.FullName)
                    End If
                    With pdfInfoExt
                        .Category = profile.Category
                        .TaxYear = profile.TaxYear
                        If profile.FlagDocument Then
                            .Flag = 1
                        Else
                            .Flag = 0
                        End If
                    End With
                    WritePdfInfoExt(stagedPdfPath.FullName, pdfInfoExt)
                    pdf.DeleteToRecycleBin
                    If xmlFile.Exists Then
                        xmlFile.DeleteToRecycleBin
                    End If
                    File.Delete(link)
                Else
                    Dim pdfInfo = ReadPdfInfo(pdf.FullName, Nothing)
                    With pdfInfo
                        If .Title Is Nothing Or .Author Is Nothing Or .Subject Is Nothing Then
                            MoveToUploadRejected(pdf)
                        Else
                            StagePdfForUpload(pdf.FullName)
                        End If
                    End With
                End If
            Else
                MoveToUploadRejected(pdf)
            End If
        Next
    End Sub

    Private Sub UploadStagedPdfFiles()
        _DocumentsUploaded = False
        For Each pdf In GetPdfFiles(AppFolders.AppFolder.UploadStaging)
            Dim pdfInfo = ReadPdfInfo(pdf.FullName, Nothing)
            Dim pdfInfoExt = ReadPdfInfoExt(pdf.FullName)
            documentSvc.CreateDocument(pdf.FullName, pdfInfo, pdfInfoExt)
            pdf.Delete()
            Dim xml = Path.ChangeExtension(pdf.FullName, "xml")
            If File.Exists(xml) Then
                File.Delete(xml)
            End If
            _DocumentsUploaded = True
        Next
    End Sub

    Private Shared Sub MoveToUploadRejected(ByVal file As FileInfo)
        Dim destFile = New FileInfo(file.FullName.Replace(AppFolders.GetPath(AppFolders.AppFolder.Upload),
                                                          AppFolders.GetPath(AppFolders.AppFolder.UploadRejected)))
        destFile.Directory.Create()
        file.MoveTo(destFile.FullName)
    End Sub
End Class
