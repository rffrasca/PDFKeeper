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
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Security.Cryptography
Imports System.Threading
Imports IWshRuntimeLibrary
Imports Microsoft.VisualBasic.FileIO
Imports File = System.IO.File
Imports SearchOption = System.IO.SearchOption

Public Module ExtensionMethods

#Region "Byte()"
    ''' <summary>
    ''' Saves a byte array to a file.
    ''' </summary>
    ''' <param name="array">Byte array</param>
    ''' <param name="targetFile">Target file name</param>
    <Extension()>
    Public Sub ToFile(ByVal array As Byte(), ByVal targetFile As String)
        If array Is Nothing Then
            Throw New ArgumentNullException(NameOf(array))
        End If
        Using memory = New MemoryStream(array)
            Using file = New FileStream(targetFile, FileMode.Create, FileAccess.Write)
                file.Write(memory.ToArray, 0, CInt(array.Length))
            End Using
        End Using
    End Sub
#End Region

#Region "DirectoryInfo"
    ''' <summary>
    ''' Creates a shortcut to a directory.
    ''' </summary>
    ''' <param name="dir">DirectoryInfo object</param>
    ''' <param name="shortcutPath">Shortcut file path</param>
    <Extension()>
    Public Sub CreateShortcut(ByVal dir As DirectoryInfo, ByVal shortcutPath As String)
        If dir Is Nothing Then
            Throw New ArgumentNullException(NameOf(dir))
        End If
        Dim wshShell = New WshShell
        Dim shortcut = DirectCast(wshShell.CreateShortcut(shortcutPath), IWshShortcut)
        shortcut.TargetPath = dir.FullName
        ' Only create the shortcut if it does not exist.  This is to prevent the occassional
        ' IOException: "The process cannot access the file because it is being used by another process"
        ' from being thrown.
        If File.Exists(shortcutPath) = False Then
            shortcut.Save()
        End If
    End Sub

    ''' <summary>
    ''' Gets all PDF files in a directory, including all sub-directories, ordered by last write time.
    ''' </summary>
    ''' <param name="dir">DirectoryInfo object</param>
    ''' <returns>Sorted sequence of file paths</returns>
    <Extension()>
    Public Function GetPdfFilesOrderByLastWriteTime(ByVal dir As DirectoryInfo) As IOrderedEnumerable(Of String)
        If dir Is Nothing Then
            Throw New ArgumentNullException(NameOf(dir))
        End If
        Return Directory.GetFiles(dir.FullName, "*.pdf",
                                  SearchOption.AllDirectories).OrderBy(Function(f) New FileInfo(f).LastWriteTime)
    End Function
#End Region

#Region "FileInfo"
    ''' <summary>
    ''' Generates a new FileInfo object containing the new directory path and the original file name with _ and a
    ''' random GUID appended to the file name.
    ''' </summary>
    ''' <param name="file">FileInfo object</param>
    ''' <param name="newDirPath">New directory path of the file.</param>
    ''' <returns>FileInfo object</returns>
    <Extension()>
    Public Function GenerateNewPathNameWithGuid(ByVal file As FileInfo, ByVal newDirPath As String) As FileInfo
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If
        Return New FileInfo(Path.Combine(newDirPath, String.Concat(Path.GetFileNameWithoutExtension(file.FullName),
                                                                   "_", Guid.NewGuid, file.Extension)))
    End Function

    ''' <summary>
    ''' Deletes a file to the Recycle Bin.
    ''' </summary>
    ''' <param name="file">FileInfo object</param>
    <Extension()>
    Public Sub DeleteToRecycleBin(ByVal file As FileInfo)
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If
        If file.Exists Then
            My.Computer.FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
        End If
    End Sub

    ''' <summary>
    ''' Gets the contents of a file.
    ''' </summary>
    ''' <param name="file">FileInfo object</param>
    ''' <returns>Contents of file as a Byte array</returns>
    <Extension()>
    Public Function ToByteArray(ByVal file As FileInfo) As Byte()
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If
        Dim byteArray As Byte()
        Using stream = New FileStream(file.FullName, FileMode.Open, FileAccess.Read)
            ReDim byteArray(CInt(stream.Length))
            stream.Read(byteArray, 0, System.Convert.ToInt32(stream.Length))
            Return byteArray
        End Using
    End Function

    ''' <summary>
    ''' Computes the hash value of a file.
    ''' </summary>
    ''' <param name="file">FileInfo object</param>
    ''' <returns>SHA1 hash value of file as a string</returns>
    <Extension()>
    Public Function ComputeHash(ByVal file As FileInfo) As String
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If
        Using algorithm = HashAlgorithm.Create("SHA1")
            Using stream = New FileStream(file.FullName, FileMode.Open, FileAccess.Read)
                Dim hash = algorithm.ComputeHash(stream)
                Return BitConverter.ToString(hash)
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Is the file locked?
    ''' </summary>
    ''' <param name="file">FileInfo object</param>
    ''' <returns>True or False</returns>
    <Extension()>
    Public Function IsLocked(ByVal file As FileInfo) As Boolean
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If
        If file.Exists Then
            Try
                Using stream = New FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
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
    ''' Waits while the file is locked.
    ''' </summary>
    ''' <param name="file">FileInfo object</param>
    <Extension()>
    Public Sub WaitWhileLocked(ByVal file As FileInfo)
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If
        Do Until file.IsLocked = False
            Thread.Sleep(15000)
        Loop
    End Sub
#End Region

#Region "SecureString"
    ''' <summary>
    ''' Decrypts a secure string.
    ''' </summary>
    ''' <param name="value">Secure string</param>
    ''' <returns>Decrypted contents</returns>
    <Extension()>
    Public Function Decrypt(ByVal value As SecureString) As String
        Dim secureStringPtr As IntPtr
        Try
            secureStringPtr = Marshal.SecureStringToGlobalAllocUnicode(value)
            Return Marshal.PtrToStringAuto(secureStringPtr)
        Finally
            Marshal.ZeroFreeGlobalAllocUnicode(secureStringPtr)
        End Try
    End Function
#End Region

#Region "String"
    ''' <summary>
    ''' Does string contain characters not allowed in file names?
    ''' </summary>
    ''' <param name="value">String to check</param>
    ''' <returns>True or False</returns>
    <Extension()>
    Public Function ContainInvalidFileNameChars(ByVal value As String) As Boolean
        If value Is Nothing Then
            Throw New ArgumentNullException(NameOf(value))
        End If
        For Each invalidChar In IO.Path.GetInvalidFileNameChars()
            If value.Contains(invalidChar) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Writes a string to a file.
    ''' </summary>
    ''' <param name="value">String to write</param>
    ''' <param name="outputFile">Output file name</param>
    <Extension()>
    Public Sub WriteToFile(ByVal value As String, ByVal outputFile As String)
        IO.File.WriteAllText(outputFile, value)
    End Sub

    ''' <summary>
    ''' Appends the date, time, and text to a string. 
    ''' </summary>
    ''' <param name="value">String to append to</param>
    ''' <param name="text">Text to append</param>
    ''' <returns>New string.</returns>
    <Extension()>
    Public Function AppendDateTimeAndText(ByVal value As String, ByVal text As String)
        If value Is Nothing Then
            Throw New ArgumentNullException(NameOf(value))
        End If
        If value.Length > 0 Then
            value = String.Concat(value, vbCrLf, vbCrLf)
        End If
        Return String.Concat(value, "--- ", Date.Now, " (", text, ") ---", vbCrLf)
    End Function

    ''' <summary>
    ''' Appends the contents of a text file to a string.
    ''' </summary>
    ''' <param name="value">String to append to</param>
    ''' <param name="file">Text file to append</param>
    ''' <returns>New string</returns>
    <Extension()>
    Public Function AppendTextFile(ByVal value As String, ByVal file As String) As String
        If value Is Nothing Then
            Throw New ArgumentNullException(NameOf(value))
        End If
        If value.Length > 0 Then
            If value.Substring(value.Length - 1) <> vbLf Then
                value &= vbCrLf
            End If
        End If
        Return String.Concat(value, IO.File.ReadAllText(file).Trim, vbCrLf)
    End Function
#End Region

End Module
