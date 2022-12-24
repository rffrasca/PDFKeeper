'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
Imports PDFKeeper.Common

Public MustInherit Class BaseFile
    Protected file As FileInfo

    ''' <summary>
    ''' Checks if the file exists.
    ''' </summary>
    ''' <returns>True or False</returns>
    Public ReadOnly Property Exists As Boolean
        Get
            Return file.Exists
        End Get
    End Property

    ''' <summary>
    ''' Gets the hash value of the file.
    ''' </summary>
    ''' <returns>SHA1 hash as a string</returns>
    Public ReadOnly Property Hash As String
        Get
            Return file.ComputeHash
        End Get
    End Property

    ''' <summary>
    ''' Gets the full path name of the file.
    ''' </summary>
    ''' <returns>Full path name</returns>
    Public ReadOnly Property FullName As String
        Get
            Return file.FullName
        End Get
    End Property

    ''' <summary>
    ''' Gets the file name of the file without the file extension.
    ''' </summary>
    ''' <returns>String containing the file name minus the extension.</returns>
    Public ReadOnly Property FileNameWithoutExtension As String
        Get
            Return Path.GetFileNameWithoutExtension(file.FullName)
        End Get
    End Property

    ''' <summary>
    ''' Copies the file to a new file.
    ''' </summary>
    ''' <param name="destFile">Destination file name</param>
    Public Sub CopyTo(ByVal destFile As String)
        file.CopyTo(destFile, True)
    End Sub

    ''' <summary>
    ''' Gets the contents of the file.
    ''' </summary>
    ''' <returns>Byte array</returns>
    Public Function ToByteArray() As Byte()
        Return file.ToByteArray
    End Function
End Class
