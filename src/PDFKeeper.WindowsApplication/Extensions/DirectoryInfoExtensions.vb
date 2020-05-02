'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
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
Module DirectoryInfoExtensions
    ''' <summary>
    ''' Returns True or False if the DirectoryInfo object, including
    ''' sub-folders contains files.
    ''' </summary>
    ''' <param name="dirInfoParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function ContainsFiles(ByVal dirInfoParam As DirectoryInfo) As Boolean
        If dirInfoParam Is Nothing Then
            Throw New ArgumentNullException("dirInfoParam")
        End If
        If dirInfoParam.GetFiles("*", SearchOption.AllDirectories).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns True or False if the DirectoryInfo object, including
    ''' sub-folders and excluding the specified search pattern contains files.
    ''' </summary>
    ''' <param name="dirInfoParam"></param>
    ''' <param name="excludeSearchPattern">Search string to exclude.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function ContainsFiles(ByVal dirInfoParam As DirectoryInfo,
                                  ByVal excludeSearchPattern As String) As Boolean
        If dirInfoParam Is Nothing Then
            Throw New ArgumentNullException("dirInfoParam")
        End If
        If excludeSearchPattern Is Nothing Then
            Throw New ArgumentNullException("searchPattern")
        End If
        If dirInfoParam.GetFiles("*",
                                 SearchOption.AllDirectories).Count -
                                 dirInfoParam.GetFiles(
                                 excludeSearchPattern,
                                 SearchOption.AllDirectories).Count > 0 Then
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
    <Extension()>
    Public Sub Explore(ByVal dirInfoParam As DirectoryInfo)
        If dirInfoParam Is Nothing Then
            Throw New ArgumentNullException("dirInfoParam")
        End If
        Process.Start(dirInfoParam.FullName)
    End Sub

    ''' <summary>
    ''' Renames the DirectoryInfo object to the specified folder path.
    ''' </summary>
    ''' <param name="dirInfoParam"></param>
    ''' <param name="newPath"></param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub Rename(ByVal dirInfoParam As DirectoryInfo,
                      ByVal newPath As String)
        If dirInfoParam Is Nothing Then
            Throw New ArgumentNullException("dirInfoParam")
        End If
        If dirInfoParam.Exists Then
            dirInfoParam.MoveTo(newPath)
        End If
    End Sub
End Module
