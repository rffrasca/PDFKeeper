'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
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
Public NotInheritable Class FileHelper
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    ''' <summary>
    ''' Adds GUID to a file name right before the file extension.
    ''' </summary>
    ''' <param name="file">File name.</param>
    ''' <param name="value">
    ''' GUID to add or if "Nothing" is specified, create a new GUID.
    ''' </param>
    ''' <returns>Filename with GUID added.</returns>
    ''' <remarks></remarks>
    Public Shared Function AddGuidToFileName(ByVal file As String, _
                                             ByVal value As Guid) As String
        If value = Nothing Then
            value = Guid.NewGuid
        End If
        Dim extenstion As String = Path.GetExtension(file)
        Return Path.Combine(Path.GetDirectoryName(file), _
                            Path.GetFileNameWithoutExtension(file) & "-" & _
                            value.ToString & extenstion)
    End Function

    ''' <summary>
    ''' Gets the GUID from a file name.  GUID must be in the name right before
    ''' the file extension.
    ''' </summary>
    ''' <param name="file">File name.</param>
    ''' <returns>GUID extracted from "file".</returns>
    ''' <remarks></remarks>
    Public Shared Function GetGuidFromFileName(ByVal file As String) As Guid
        Dim guid As New Guid(Path.GetFileNameWithoutExtension(file).Substring( _
            Path.GetFileNameWithoutExtension(file).Length - 36))
        Return guid
    End Function

    ''' <summary>
    ''' Changes the directory name in a file.
    ''' </summary>
    ''' <param name="file">File name to change.</param>
    ''' <param name="newDirectoryName">New directory name for file.</param>
    ''' <returns>New path name of file.</returns>
    ''' <remarks></remarks>
    Public Shared Function ChangeDirectoryName(ByVal file As String, _
                                               ByVal newDirectoryName As String)
        Return Path.Combine(newDirectoryName, Path.GetFileName(file))
    End Function

    ''' <summary>
    ''' Deletes "file" to the Recycle Bin.
    ''' </summary>
    ''' <param name="file">File to delete.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteFileToRecycleBin(ByVal file As String)
        If IO.File.Exists(file) Then
            My.Computer.FileSystem.DeleteFile(file, _
                                              FileIO.UIOption.OnlyErrorDialogs, _
                                              FileIO.RecycleOption.SendToRecycleBin)
        End If
    End Sub

    ''' <summary>
    ''' Gets "file" as a byte array.
    ''' </summary>
    ''' <param name="file">File to read.</param>
    ''' <returns>Byte array of "file".</returns>
    ''' <remarks></remarks>
    Public Shared Function GetFileAsByteArray(ByVal file As String) As Byte()
        Dim byteArray As Byte()
        Using stream As FileStream = New FileStream(file, _
                                                    FileMode.Open, _
                                                    FileAccess.Read)
            ReDim byteArray(CInt(stream.Length))
            stream.Read(byteArray, 0, System.Convert.ToInt32(stream.Length))
            Return byteArray
        End Using
    End Function

    ''' <summary>
    ''' Waits until "file" can be opened with read/write access.
    ''' </summary>
    ''' <param name="file">File name.</param>
    ''' <remarks></remarks>
    Public Shared Sub WaitWhileFileIsInUse(ByVal file As String)
        Do Until IsFileInUse(file) = False
            Threading.Thread.Sleep(5000)
        Loop
    End Sub

    Private Shared Function IsFileInUse(ByVal file As String) As Boolean
        If System.IO.File.Exists(file) Then
            Try
                Using stream As New FileStream(file, _
                                               FileMode.Open, _
                                               FileAccess.ReadWrite, _
                                               FileShare.None)
                End Using
                Return False
            Catch ex As IOException
                Return True
            Catch ex As UnauthorizedAccessException
                Return True
            End Try
        End If
        Return False
    End Function
End Class
