'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Module FileInfoExtensions
    ''' <summary>
    ''' Returns a string containing a "-" and a GUID appended to the name
    ''' of the FileInfo object.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <param name="value">
    ''' GUID to append or Nothing to append an auto generated GUID.
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function AppendGuidToName(ByVal fileInfoParam As FileInfo,
                                     ByVal value As Guid) As String
        If fileInfoParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(fileInfoParam))
        End If
        If value = Nothing Then
            value = Guid.NewGuid
        End If
        Dim extension As String = Path.GetExtension(fileInfoParam.FullName)
        Return Path.Combine(Path.GetDirectoryName(fileInfoParam.FullName),
                            Path.GetFileNameWithoutExtension(fileInfoParam.FullName) & "-" &
                            value.ToString & extension)
    End Function

    ''' <summary>
    ''' Returns a string containing the hash value of the FileInfo object.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function ComputeHash(ByVal fileInfoParam As FileInfo) As String
        If fileInfoParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(fileInfoParam))
        End If
        Using algorithm As HashAlgorithm = HashAlgorithm.Create("SHA1")
            Using inputStream As New FileStream(fileInfoParam.FullName,
                                                FileMode.Open,
                                                FileAccess.Read)
                Dim hash As Byte() = algorithm.ComputeHash(inputStream)
                Return BitConverter.ToString(hash)
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Deletes the FileInfo object to the Recycle Bin.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub DeleteToRecycleBin(ByVal fileInfoParam As FileInfo)
        If fileInfoParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(fileInfoParam))
        End If
        If fileInfoParam.Exists Then
            My.Computer.FileSystem.DeleteFile(fileInfoParam.FullName,
                                              FileIO.UIOption.OnlyErrorDialogs,
                                              FileIO.RecycleOption.SendToRecycleBin)
        End If
    End Sub

    ''' <summary>
    ''' Returns the FileInfo object path modified to include a "-" followed by
    ''' a random GUID that proceeds the original file name and the folder path
    ''' switched to the path of the UploadStaging folder.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function GenerateUploadStagingFilePath(ByVal fileInfoParam As FileInfo) As String
        Dim fileInfo As New _
            FileInfo(fileInfoParam.SwitchFolderPathName(
                     UserProfile.UploadStagingPath))
        Return fileInfo.AppendGuidToName(Nothing)
    End Function

    ''' <summary>
    ''' Returns True or False if the FileInfo object is in use.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function IsInUse(ByVal fileInfoParam As FileInfo) As Boolean
        If fileInfoParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(fileInfoParam))
        End If
        If fileInfoParam.Exists Then
            Try
                Using stream As New FileStream(fileInfoParam.FullName,
                                               FileMode.Open,
                                               FileAccess.ReadWrite,
                                               FileShare.None)
                End Using
            Catch ex As IOException
                Return True
            Catch ex As UnauthorizedAccessException
                Return True
            End Try
        End If
        Return False
    End Function

    ''' <summary>
    ''' Returns a string combining the specified folder path name with the name
    ''' and extension of the FileInfo object.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <param name="newFolderPathName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function SwitchFolderPathName(ByVal fileInfoParam As FileInfo,
                                         ByVal newFolderPathName As String) As String
        If fileInfoParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(fileInfoParam))
        End If
        Return Path.Combine(newFolderPathName, Path.GetFileName(fileInfoParam.FullName))
    End Function

    ''' <summary>
    ''' Returns the contents of the FileInfo object as a Byte array.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function ToByteArray(ByVal fileInfoParam As FileInfo) As Byte()
        If fileInfoParam Is Nothing Then
            Throw New ArgumentNullException(NameOf(fileInfoParam))
        End If
        Dim byteArray As Byte()
        Using stream As FileStream = New FileStream(fileInfoParam.FullName,
                                                    FileMode.Open,
                                                    FileAccess.Read)
            ReDim byteArray(CInt(stream.Length))
            stream.Read(byteArray, 0, System.Convert.ToInt32(stream.Length))
            Return byteArray
        End Using
    End Function

    ''' <summary>
    ''' Waits until the FileInfo object can be opened with read/write access.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub WaitWhileIsInUse(ByVal fileInfoParam As FileInfo)
        Do Until fileInfoParam.IsInUse = False
            Threading.Thread.Sleep(5000)
        Loop
    End Sub
End Module
