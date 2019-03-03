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
Public NotInheritable Class UploadDirectory
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared Sub Explore()
        Process.Start(ApplicationDirectories.Upload)
    End Sub

    Public Shared Sub CreateChildDirectory(ByVal folderName As String)
        Directory.CreateDirectory(Path.Combine(ApplicationDirectories.Upload, _
                                               folderName))
    End Sub

    ''' <summary>
    ''' Deletes a directory, including any sub directories from the Upload directory.
    ''' </summary>
    ''' <param name="directoryName">Directory to delete.</param>
    ''' <returns>
    ''' True when directory does not exist or was deleted.
    ''' False when folder cannot be deleted because it contains one or more files.
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteChildDirectory(ByVal directoryName As String) As Boolean
        Dim dirName As String = Path.Combine(ApplicationDirectories.Upload, _
                                             directoryName)
        If Directory.Exists(dirName) Then
            If DirectoryHelper.GetCountOfFiles(dirName, SearchOption.AllDirectories) = 0 Then
                Directory.Delete(dirName, True)
            Else
                Return False
            End If
        End If
        Return True
    End Function

    Public Shared Sub RenameChildDirectory(ByVal sourceFolderName As String, _
                                           ByVal targetFolderName As String)
        Dim sourceDirName As String = Path.Combine(ApplicationDirectories.Upload, _
                                                   sourceFolderName)
        Dim targetDirName As String = Path.Combine(ApplicationDirectories.Upload, _
                                                   targetFolderName)
        If Directory.Exists(sourceDirName) Then
            Directory.Move(sourceDirName, targetDirName)
        End If
    End Sub

    Public Shared Sub ProcessAllPdfFiles()
        Dim files = Directory.GetFiles(ApplicationDirectories.Upload, _
                                       "*.pdf", _
                                       SearchOption.AllDirectories).OrderBy( _
                                       Function(f) New FileInfo(f).LastWriteTime)
        For Each file In files
            FileHelper.WaitWhileFileIsInUse(file)
            ProcessPdfFile(file)
        Next
    End Sub

    Public Shared Sub DeleteEmptyNonConfiguredChildDirectories()
        Dim childDirectories As String() = _
            Directory.GetDirectories(ApplicationDirectories.Upload)
        For Each childDirectory In childDirectories
            Dim dirInfo As New DirectoryInfo(childDirectory)
            If UploadConfigDirectory.IsFolderConfigured(dirInfo.Name) Then
                ' When the directory is a configured folder then only
                ' delete empty sub directories under the configured
                ' directory.
                Dim subDirectories As String() = _
                    Directory.GetDirectories(childDirectory)
                For Each subDirectory In subDirectories
                    If Directory.GetFiles(subDirectory).Count = 0 Then
                        DeleteChildDirectory(subDirectory)
                    End If
                Next
            Else
                If Directory.GetFiles(childDirectory).Count = 0 Then
                    DeleteChildDirectory(childDirectory)
                End If
            End If
        Next
    End Sub

    Private Shared Sub ProcessPdfFile(ByVal file As String)
        Dim pdfFile As New PdfFile(file)
        If pdfFile.ContainsOwnerPassword = False Then
            Dim uploadFolderName As String = Nothing
            uploadFolderName = file.Substring(ApplicationDirectories.Upload.Length + 1)
            If uploadFolderName = Path.GetFileName(file) Then
                uploadFolderName = ApplicationDirectories.Upload
            Else
                uploadFolderName = uploadFolderName.Substring(0, _
                                                              uploadFolderName.IndexOf( _
                                                                  Path.DirectorySeparatorChar))
            End If
            Try
                Dim pdfReader As New PdfFileInfoPropertiesReader(file)
                If UploadConfigDirectory.IsFolderConfigured(uploadFolderName) Then
                    Dim outputPdfFile As String = _
                        FileHelper.AddGuidToFileName( _
                            FileHelper.ChangeDirectoryName(file, _
                                                           ApplicationDirectories.UploadStaging), _
                                                       Nothing)
                    Dim guid As Guid = FileHelper.GetGuidFromFileName(outputPdfFile)
                    WritePdfUsingFolderConfig(file, uploadFolderName, outputPdfFile)
                    SavePdfFileSupplementalData(Path.ChangeExtension(file, _
                                                                     "xml"), _
                                                                 guid, uploadFolderName)
                    FileHelper.DeleteFileToRecycleBin(file)
                Else
                    If pdfReader.Title IsNot Nothing And _
                        pdfReader.Author IsNot Nothing And _
                        pdfReader.Subject IsNot Nothing Then
                        StagePdfFileAndSupplementalDataXml(file)
                    End If
                End If
            Catch ex As BadPasswordException    ' Ignore the file
            End Try
        End If
    End Sub

    Private Shared Sub WritePdfUsingFolderConfig(ByVal sourcePdfFile As String, _
                                                 ByVal configfolderName As String, _
                                                 ByVal outputPdfFile As String)
        Dim writer As New PdfFileInfoPropertiesWriter(sourcePdfFile, _
                                                      outputPdfFile)
        Dim config As New UploadFolderConfiguration
        UploadConfigDirectory.ReadConfig(config, configfolderName.ToString)
        If config.TitlePrefill = My.Resources.DateTimeToken Then
            writer.Title = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", _
                                                 CultureInfo.CurrentCulture)
        ElseIf config.TitlePrefill = My.Resources.DateToken Then
            writer.Title = DateTime.Now.ToString("yyyy-MM-dd", _
                                                 CultureInfo.CurrentCulture)
        ElseIf config.TitlePrefill = My.Resources.FileNameToken Then
            writer.Title = Path.GetFileNameWithoutExtension(sourcePdfFile)
        Else
            writer.Title = config.TitlePrefill
        End If
        writer.Author = config.AuthorPrefill
        writer.Subject = config.SubjectPrefill
        writer.Keywords = config.KeywordsPrefill
        writer.Write()
    End Sub

    Private Shared Sub SavePdfFileSupplementalData(ByVal xmlFile As String, _
                                                   ByVal guid As Guid, _
                                                   ByVal configfolderName As String)
        xmlFile = FileHelper.AddGuidToFileName(xmlFile, guid)
        xmlFile = FileHelper.ChangeDirectoryName(xmlFile, _
                                                 ApplicationDirectories.UploadStaging)
        Dim config As New UploadFolderConfiguration
        Dim suppData As New PdfFileSupplementalData
        UploadConfigDirectory.ReadConfig(config, configfolderName.ToString)
        suppData.Notes = String.Empty
        Dim flag As String = config.FlagDocument.ToString
        If flag Then
            suppData.FlagState = 1
        Else
            suppData.FlagState = 0
        End If
        SerializerHelper.FromObjToXml(suppData, xmlFile)
    End Sub

    Private Shared Sub StagePdfFileAndSupplementalDataXml(ByVal pdfFile As String)
        Dim xmlFile As String = Path.ChangeExtension(pdfFile, "xml")
        Dim targetPdfFile As String = _
            FileHelper.AddGuidToFileName( _
                FileHelper.ChangeDirectoryName(pdfFile, _
                                               ApplicationDirectories.UploadStaging), _
                                           Nothing)
        Dim guid As Guid = FileHelper.GetGuidFromFileName(targetPdfFile)
        Dim targetXmlFile As String = FileHelper.AddGuidToFileName(xmlFile, guid)
        targetXmlFile = FileHelper.ChangeDirectoryName(targetXmlFile, _
                                                       ApplicationDirectories.UploadStaging)
        IO.File.Move(pdfFile, targetPdfFile)
        If IO.File.Exists(xmlFile) Then
            IO.File.Move(xmlFile, targetXmlFile)
        End If
    End Sub
End Class
