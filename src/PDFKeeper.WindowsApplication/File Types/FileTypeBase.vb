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
Public MustInherit Class FileTypeBase
    Protected fileInfo As FileInfo

    ''' <summary>
    ''' Checks if the object exists. 
    ''' </summary>
    ''' <value></value>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Exists As Boolean
        Get
            Return fileInfo.Exists
        End Get
    End Property

    ''' <summary>
    ''' Gets the path name of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FullName As String
        Get
            Return fileInfo.FullName
        End Get
    End Property

    ''' <summary>
    ''' Copies the object to a new file path.
    ''' </summary>
    ''' <param name="targetFilePath"></param>
    ''' <remarks></remarks>
    Public Sub CopyTo(ByVal targetFilePath As String)
        fileInfo.CopyTo(targetFilePath, True)
    End Sub

    ''' <summary>
    ''' Returns the hash value of the object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ComputeHash() As String
        Return fileInfo.ComputeHash
    End Function
End Class
