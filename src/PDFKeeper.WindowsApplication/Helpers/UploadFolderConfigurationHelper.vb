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
Public Class UploadFolderConfigurationHelper
    Private ReadOnly serializer As XmlSerializerHelper
    Private ReadOnly m_ConfigName As String
    Private ReadOnly configXmlPath As String
    Private ReadOnly configFolderPath As String

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="configName"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal configName As String)
        m_ConfigName = configName
        configXmlPath = IO.Path.Combine(UserProfile.UploadConfigPath,
                                        m_ConfigName & ".xml")
        configFolderPath = IO.Path.Combine(UserProfile.UploadPath,
                                           m_ConfigName)
        serializer = New XmlSerializerHelper(configXmlPath)
    End Sub

    ''' <summary>
    ''' Returns an UploadFolderConfiguration object containing the Upload
    ''' Folder Configuration. 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Read() As UploadFolderConfiguration
        Dim config As UploadFolderConfiguration =
            serializer.Deserialize(Of UploadFolderConfiguration)()
        Return config
    End Function

    ''' <summary>
    ''' Saves the specified UploadFolderConfiguration object to an XML file
    ''' in the UploadConfig folder and ensures that a corresponding folder exists
    ''' in the Upload folder.
    ''' </summary>
    ''' <param name="config">UploadFolderConfiguration object to save.</param>
    ''' <param name="originalConfigName">
    ''' When specified, the original configuration file will be deleted from
    ''' the UploadConfig folder and its corresponding folder in the Upload
    ''' folder will be renamed. When creating a brand new configuration,
    ''' specify Nothing. 
    ''' </param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal config As UploadFolderConfiguration, _
                    ByVal originalConfigName As String)
        UploadService.Instance.WaitUntilUploadCycleIsNotExecuting()
        If Not originalConfigName Is Nothing Then
            IO.File.Delete(IO.Path.Combine(UserProfile.UploadConfigPath,
                                           originalConfigName & ".xml"))
            If Not m_ConfigName = originalConfigName Then
                Dim sourcePath As String = IO.Path.Combine(UserProfile.UploadPath,
                                                           originalConfigName)
                Dim targetPath As String = configFolderPath
                Dim dirInfo As New DirectoryInfo(sourcePath)
                dirInfo.Rename(targetPath)
            End If
        End If
        serializer.Serialize(config)
        Directory.CreateDirectory(configFolderPath)
    End Sub

    ''' <summary>
    ''' Deletes the Upload Folder Configuration from the UploadConfig
    ''' folder, including the corresponding folder from the Upload folder. 
    ''' </summary>
    ''' <returns>
    ''' True: Configuration was deleted.
    ''' False: Configuration folder cannot be deleted because it contains
    ''' one or more files.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function Delete() As Boolean
        UploadService.Instance.WaitUntilUploadCycleIsNotExecuting()
        Dim dirInfo As New DirectoryInfo(configFolderPath)
        If dirInfo.Exists Then
            If dirInfo.ContainsFiles = False Then
                dirInfo.Delete(True)
            Else
                Return False
            End If
        End If
        IO.File.Delete(configXmlPath)
        Return True
    End Function
End Class
