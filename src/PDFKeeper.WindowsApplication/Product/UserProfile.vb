'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Public NotInheritable Class UserProfile
    Private Sub New()
        ' All members are shared.
    End Sub

    ''' <summary>
    ''' Returns the file path of the local database or Nothing if not exists.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property LocalDatabasePath As String
        Get
            Dim database As String = Path.Combine(ApplicationDataRoot,
                                                  String.Concat(Application.ProductName,
                                                                ".sqlite"))
            If IO.File.Exists(database) Then
                Return database
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the folder path used to cache PDF and image previews from
    ''' selected document records.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property CachePath As String
        Get
            Dim folderPath As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.Cache)
            Directory.CreateDirectory(folderPath)
            Return folderPath.ToString(CultureInfo.CurrentCulture)
        End Get
    End Property

    ''' <summary>
    ''' Returns the folder path used for copying PDF files to be uploaded.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UploadPath As String
        Get
            Dim folderPath As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.Upload)
            Directory.CreateDirectory(folderPath)
            ShortcutUtil.Create(UploadShortcutPath, folderPath)
            Return folderPath.ToString(CultureInfo.CurrentCulture)
        End Get
    End Property

    ''' <summary>
    ''' Returns the "PDFKeeper Upload" shortcut file path.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UploadShortcutPath As String
        Get
            Return Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, _
                                Application.ProductName & " " & My.Resources.Upload & ".lnk")
        End Get
    End Property

    ''' <summary>
    ''' Returns the folder path used to store Upload Folder Configurations.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UploadConfigPath As String
        Get
            Dim folderPath As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.UploadConfig)
            Directory.CreateDirectory(folderPath)
            Return folderPath.ToString(CultureInfo.CurrentCulture)
        End Get
    End Property

    ''' <summary>
    ''' Returns the folder path used to store PDF documents and cooresponding
    ''' supplemental data ready to be uploaded.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UploadStagingPath As String
        Get
            Dim folderPath As String = Path.Combine( _
                ApplicationDataRoot, _
                My.Resources.UploadStaging)
            Directory.CreateDirectory(folderPath)
            Return folderPath.ToString(CultureInfo.CurrentCulture)
        End Get
    End Property

    ''' <summary>
    ''' Deletes the "PDFKeeper Upload" shortcut file.
    ''' </summary>
    ''' <remarks>Called during application shutdown.</remarks>
    Public Shared Sub DeleteUploadShortcut()
        Try
            IO.File.Delete(UserProfile.UploadShortcutPath)
        Catch ex As IOException
        End Try
    End Sub

    ''' <summary>
    ''' Deletes all cached files from the Cache folder.
    ''' 
    ''' For each cached file that cannot be deleted, its extension will be
    ''' changed to "delete" and will be deleted the next time the application
    ''' is closed.
    ''' </summary>
    Public Shared Sub DeleteCachedFiles()
        For Each cachedFile In Directory.GetFiles(UserProfile.CachePath,
                                                  Application.ProductName & "*.*")
            Try
                IO.File.Delete(cachedFile)
            Catch ex As IOException
            End Try
            If IO.File.Exists(cachedFile) Then
                Try
                    IO.File.Move(cachedFile,
                                 Path.ChangeExtension(cachedFile, "delete"))
                Catch ex As IOException
                End Try
            End If
        Next
    End Sub

    Private Shared ReadOnly Property ApplicationDataRoot As String
        Get
            Dim folderPath As String = Path.Combine( _
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _
                My.Application.Info.CompanyName, _
                Application.ProductName)
            Directory.CreateDirectory(folderPath)
            Return folderPath.ToString(CultureInfo.CurrentCulture)
        End Get
    End Property
End Class
