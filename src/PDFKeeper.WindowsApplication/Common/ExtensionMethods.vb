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
Module ExtensionMethods

#Region "DirectoryInfo"
    ''' <summary>
    ''' Returns True or False if the DirectoryInfo object, including
    ''' sub-folders contains files.
    ''' </summary>
    ''' <param name="dirInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function ContainsFiles(ByVal dirInfoParam As DirectoryInfo) As Boolean
        If dirInfoParam.GetFiles("*", SearchOption.AllDirectories).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Opens the DirectoryInfo object using the operating system.
    ''' </summary>
    ''' <param name="dirInfoParam"></param>
    ''' <remarks></remarks>
    <Extension()> _
    Public Sub Explore(ByVal dirInfoParam As DirectoryInfo)
        Process.Start(dirInfoParam.FullName)
    End Sub

    ''' <summary>
    ''' Renames the DirectoryInfo object to the specified folder path.
    ''' </summary>
    ''' <param name="dirInfoParam"></param>
    ''' <param name="newPath"></param>
    ''' <remarks></remarks>
    <Extension()> _
    Public Sub Rename(ByVal dirInfoParam As DirectoryInfo, _
                      ByVal newPath As String)
        If dirInfoParam.Exists Then
            dirInfoParam.MoveTo(newPath)
        End If
    End Sub
#End Region

#Region "FileInfo"
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
    <Extension()> _
    Public Function AppendGuidToName(ByVal fileInfoParam As FileInfo, _
                                     ByVal value As Guid) As String
        If value = Nothing Then
            value = Guid.NewGuid
        End If
        Dim extension As String = Path.GetExtension(fileInfoParam.FullName)
        Return Path.Combine(Path.GetDirectoryName(fileInfoParam.FullName), _
                            Path.GetFileNameWithoutExtension(fileInfoParam.FullName) & "-" & _
                            value.ToString & extension)
    End Function

    ''' <summary>
    ''' Returns a string containing the hash value of the FileInfo object.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function ComputeHash(ByVal fileInfoParam As FileInfo) As String
        Using algorithm As HashAlgorithm = HashAlgorithm.Create("SHA1")
            Using inputStream As New FileStream(fileInfoParam.FullName, _
                                                FileMode.Open, _
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
    <Extension()> _
    Public Sub DeleteToRecycleBin(ByVal fileInfoParam As FileInfo)
        If fileInfoParam.Exists Then
            My.Computer.FileSystem.DeleteFile(fileInfoParam.FullName, _
                                              FileIO.UIOption.OnlyErrorDialogs, _
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
    <Extension()> _
    Public Function GenerateUploadStagingFilePath(ByVal fileInfoParam As FileInfo) As String
        Dim fileInfo As New  _
            FileInfo(fileInfoParam.SwitchFolderPathName( _
                     UserProfile.UploadStagingPath))
        Return fileInfo.AppendGuidToName(Nothing)
    End Function

    ''' <summary>
    ''' Returns True or False if the FileInfo object is in use.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function IsInUse(ByVal fileInfoParam As FileInfo) As Boolean
        If fileInfoParam.Exists Then
            Try
                Using stream As New FileStream(fileInfoParam.FullName, _
                                               FileMode.Open, _
                                               FileAccess.ReadWrite, _
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
    <Extension()> _
    Public Function SwitchFolderPathName(ByVal fileInfoParam As FileInfo, _
                                         ByVal newFolderPathName As String) As String
        Return Path.Combine(newFolderPathName, Path.GetFileName(fileInfoParam.FullName))
    End Function

    ''' <summary>
    ''' Returns the contents of the FileInfo object as a Byte array.
    ''' </summary>
    ''' <param name="fileInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function ToByteArray(ByVal fileInfoParam As FileInfo) As Byte()
        Dim byteArray As Byte()
        Using stream As FileStream = New FileStream(fileInfoParam.FullName, _
                                                    FileMode.Open, _
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
    <Extension()> _
    Public Sub WaitWhileIsInUse(ByVal fileInfoParam As FileInfo)
        Do Until fileInfoParam.IsInUse = False
            Threading.Thread.Sleep(5000)
        Loop
    End Sub
#End Region

#Region "SecureString"
    ''' <summary>
    ''' Returns a string containing the contents of the SecureString object.
    ''' </summary>
    ''' <param name="secureStringParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Function SecureStringToString(ByVal secureStringParam As SecureString) As String
        Dim secureStringPtr As IntPtr
        Try
            secureStringPtr = _
                Marshal.SecureStringToGlobalAllocUnicode(secureStringParam)
            Return Marshal.PtrToStringAuto(secureStringPtr)
        Finally
            Marshal.ZeroFreeGlobalAllocUnicode(secureStringPtr)
        End Try
    End Function
#End Region

#Region "String"
    ''' <summary>
    ''' Returns a new String containing a carriage return, followed by the
    ''' date, time, and specified text appended to the String object.
    ''' </summary>
    ''' <param name="stringParam"></param>
    ''' <param name="stringToAppend"></param>
    ''' <returns>Appended string</returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function AppendDateTimeAndTextToString(ByVal stringParam As String, _
                                                  ByVal stringToAppend As String) As String
        If stringParam.Length > 0 Then
            stringParam = stringParam & vbCrLf & vbCrLf
        End If
        Return stringParam & "--- " & Date.Now & " (" & _
            stringToAppend & ") ---" & vbCrLf
    End Function

    ''' <summary>
    ''' Verifies String object contains characters not allowed in file names.
    ''' </summary>
    ''' <param name="stringParam"></param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function ContainsInvalidFileNameChars(ByVal stringParam As String) As Boolean
        For Each invalidChar In Path.GetInvalidFileNameChars()
            If stringParam.Contains(invalidChar) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Prints the string object to the selected printer.
    ''' </summary>
    ''' <param name="stringParam"></param>
    ''' <remarks></remarks>
    <Extension()> _
    Public Sub Print(ByVal stringParam As String)
        Dim print As IPrintString = New PrintString(stringParam)
        print.Print()
    End Sub

    ''' <summary>
    ''' Previews the string object for printing using the default printer.
    ''' </summary>
    ''' <param name="stringParam"></param>
    ''' <param name="printPreviewFormSize"></param>
    ''' <remarks></remarks>
    <Extension()> _
    Public Sub PrintPreview(ByVal stringParam As String, _
                            ByVal printPreviewFormSize As System.Drawing.Size)
        Dim printPreview As IPrintString = New PrintString(stringParam)
        printPreview.PrintPreview(printPreviewFormSize)
    End Sub

    ''' <summary>
    ''' Writes the String object to the specified file path.
    ''' </summary>
    ''' <param name="stringParam"></param>
    ''' <param name="filePath"></param>
    ''' <remarks></remarks>
    <Extension()> _
    Public Sub WriteToFile(ByVal stringParam As String, _
                           ByVal filePath As String)
        IO.File.WriteAllText(filePath, stringParam)
    End Sub

    ''' <summary>
    ''' Verifies proper usage of query operators in String object.
    ''' </summary>
    ''' <param name="stringParam"></param>
    ''' <returns>True or False</returns>
    ''' <remarks>
    ''' These query operators are specific to the Oracle Database.
    ''' </remarks>
    <Extension()> _
    Public Function VerifyProperUsageOfQueryOperators(ByVal stringParam As String) As Boolean
        stringParam = stringParam.ToUpper(CultureInfo.CurrentCulture)
        If stringParam = "MINUS" Or _
            stringParam = "NEAR" Or _
            stringParam = "NOT" Or _
            stringParam = "AND" Or _
            stringParam = "EQUIV" Or _
            stringParam = "WITHIN" Or _
            stringParam = "OR" Or _
            stringParam = "ACCUM" Or _
            stringParam = "FUZZY" Or _
            stringParam = "ABOUT" Or _
            stringParam.StartsWith("MINUS ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("NEAR ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("NOT ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("AND ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("EQUIV ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("WITHIN ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("OR ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("ACCUM ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("FUZZY ", StringComparison.CurrentCulture) Or _
            stringParam.StartsWith("ABOUT ", StringComparison.CurrentCulture) Or _
            stringParam.IndexOf("{}", StringComparison.Ordinal) <> -1 Or _
            stringParam.IndexOf("()", StringComparison.Ordinal) <> -1 Or _
            stringParam.Substring(0, 1) = "=" Or _
            stringParam.Substring(0, 1) = ";" Or _
            stringParam.Substring(0, 1) = ">" Or _
            stringParam.Substring(0, 1) = "-" Or _
            stringParam.Substring(0, 1) = "~" Or _
            stringParam.Substring(0, 1) = "&" Or _
            stringParam.Substring(0, 1) = "|" Or _
            stringParam.Substring(0, 1) = "," Or _
            stringParam.Substring(0, 1) = "!" Or _
            stringParam.Substring(0, 1) = "{" Or _
            stringParam.Substring(0, 1) = "(" Or _
            stringParam = "?" Or _
            stringParam = "$" Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

End Module
