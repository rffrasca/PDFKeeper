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
Public NotInheritable Class UserProfile
    ''' <summary>
    ''' Required by Code Analysis.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
    End Sub

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
            Return folderPath.ToString
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
            Shortcut.Create(UploadShortcutPath, folderPath)
            Return folderPath.ToString
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
            Return folderPath.ToString
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
            Return folderPath.ToString
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
    ''' Deletes the Cache folder including all of its contents.
    ''' </summary>
    ''' <remarks>Called during application shutdown.</remarks>
    Public Shared Sub DeleteCacheFolder()
        Try
            Directory.Delete(UserProfile.CachePath, True)
        Catch ex As IOException
        End Try
    End Sub

    Private Shared ReadOnly Property ApplicationDataRoot As String
        Get
            Dim folderPath As String = Path.Combine( _
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _
                My.Application.Info.CompanyName, _
                Application.ProductName)
            Directory.CreateDirectory(folderPath)
            Return folderPath.ToString
        End Get
    End Property
End Class
