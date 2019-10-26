'******************************************************************************
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
Public NotInheritable Class UploadFolderConfigurationUtil
    ''' <summary>
    ''' Required by Code Analysis.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Returns an object containing an array of all Upload Folder
    ''' Configuration file names.
    ''' </summary>
    ''' <param name="showFileNamesWithoutExtension">True or False</param>
    ''' <param name="sorted">True or False</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllConfigFileNames(ByVal showFileNamesWithoutExtension As Boolean, _
                                                 ByVal sorted As Boolean) As Object
        Dim items As New GenericList(Of String)
        Dim configFiles As String() = _
            Directory.GetFiles(UserProfile.UploadConfigPath, _
                               "*.xml", _
                               SearchOption.TopDirectoryOnly)
        For Each configFile As String In configFiles
            Dim configFileNameWithoutExtension As String = _
                Path.GetFileNameWithoutExtension(configFile)
            If showFileNamesWithoutExtension Then
                items.Add(configFileNameWithoutExtension)
            Else
                items.Add(configFile)
            End If
        Next
        Return items.ToArray(sorted)
    End Function

    ''' <summary>
    ''' Returns True or False if the specified folder is a configured Upload
    ''' folder.
    ''' </summary>
    ''' <param name="folderName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsFolderConfigured(ByVal folderName As String) As Boolean
        If IO.File.Exists(Path.Combine(UserProfile.UploadConfigPath, _
                                       folderName & ".xml")) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
