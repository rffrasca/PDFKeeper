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
Public NotInheritable Class UploadConfigDirectory
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared Function GetConfigFileNames(ByVal showFileNamesWithoutExtension As Boolean, _
                                              ByVal sorted As Boolean) As Object
        Dim uploadFolders As New GenericList(Of String)
        Dim configFiles As String() = Directory.GetFiles(ApplicationDirectories.UploadConfig, _
                                                         "*.xml", _
                                                         SearchOption.TopDirectoryOnly)
        For Each configFile As String In configFiles
            If showFileNamesWithoutExtension Then
                uploadFolders.Add(Path.GetFileNameWithoutExtension(configFile))
            Else
                uploadFolders.Add(configFile)
            End If
        Next
        Return uploadFolders.ToArray(sorted)
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", _
        MessageId:="0#")> _
    Public Shared Sub ReadConfig(ByRef configObjName As UploadFolderConfiguration, _
                                 ByVal configName As String)
        SerializerHelper.FromXmlToObj(configObjName, _
                                       Path.Combine(ApplicationDirectories.UploadConfig, _
                                            configName & ".xml"))
    End Sub

    Public Shared Sub WriteConfig(ByVal configObjName As UploadFolderConfiguration, _
                                  ByVal configName As String)
        SerializerHelper.FromObjToXml(configObjName, _
                                       Path.Combine(ApplicationDirectories.UploadConfig, _
                                            configName & ".xml"))
    End Sub

    Public Shared Sub DeleteConfig(ByVal configName As String)
        IO.File.Delete(Path.Combine(ApplicationDirectories.UploadConfig, _
                                    configName & ".xml"))
    End Sub

    Public Shared Function IsFolderConfigured(ByVal folderName As String) As Boolean
        If IO.File.Exists(Path.Combine(ApplicationDirectories.UploadConfig, _
                                       folderName & ".xml")) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
