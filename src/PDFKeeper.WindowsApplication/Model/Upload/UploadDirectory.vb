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
            Dim folderName As String = Nothing
            folderName = file.Substring(ApplicationDirectories.Upload.Length + 1)
            If folderName = Path.GetFileName(file) Then
                folderName = ApplicationDirectories.Upload
            Else
                folderName = folderName.Substring(0, folderName.IndexOf(Path.DirectorySeparatorChar))
            End If
            Try
                Dim pdfReader As New PdfFileInfoPropertiesReader(file)
                If UploadConfigDirectory.IsFolderConfigured(folderName) Then
                    Dim outputPdfFile As String = _
                        FileHelper.AddGuidToFileName( _
                            FileHelper.ChangeDirectoryName(file, _
                                                           ApplicationDirectories.UploadStaging), _
                                                       Nothing)
                    WritePdfUsingFolderConfig(file, folderName, outputPdfFile)
                    Dim txtFile As String = Path.ChangeExtension(file, "txt")
                    If IO.File.Exists(txtFile) Then
                        Dim guid As Guid = FileHelper.GetGuidFromFileName(outputPdfFile)
                        txtFile = FileHelper.AddGuidToFileName(txtFile, guid)
                        IO.File.Move(txtFile, _
                                     Path.Combine(ApplicationDirectories.UploadStaging, _
                                                  Path.GetFileName(txtFile)))
                    End If
                    FileHelper.DeleteFileToRecycleBin(file)
                Else
                    If pdfReader.Title IsNot Nothing And _
                        pdfReader.Author IsNot Nothing And _
                        pdfReader.Subject IsNot Nothing Then
                        MoveFileOutOfUpload(file, ApplicationDirectories.UploadStaging)
                    End If
                End If
            Catch ex As BadPasswordException    ' Ignore the file
            End Try
        End If
    End Sub

    Private Shared Sub MoveFileOutOfUpload(ByVal file As String, _
                                           ByVal targetDirectory As String)
        Dim txtFile As String = Path.ChangeExtension(file, "txt")
        Dim targetFile As String = FileHelper.AddGuidToFileName( _
            FileHelper.ChangeDirectoryName(file, _
                                           targetDirectory), _
                                       Nothing)
        Dim guid As Guid = FileHelper.GetGuidFromFileName(targetFile)
        Dim txtFileTarget As String = FileHelper.AddGuidToFileName(txtFile, guid)
        txtFileTarget = FileHelper.ChangeDirectoryName(txtFileTarget, targetDirectory)
        IO.File.Move(file, targetFile)
        If IO.File.Exists(txtFile) Then
            IO.File.Move(txtFile, txtFileTarget)
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
End Class
