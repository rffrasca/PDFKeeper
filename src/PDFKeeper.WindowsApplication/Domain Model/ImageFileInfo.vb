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
Public Class ImageFileInfo
    Private fileInfo As FileInfo

    Public Sub New(ByVal imagePath As String)
        fileInfo = New FileInfo(imagePath)
    End Sub

    ''' <summary>
    ''' Checks if the image file object exists. 
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
    ''' Gets the path name of the image file object.
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
    ''' Returns the hash value for the image file object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ComputeHash() As String
        Return fileInfo.ComputeHash
    End Function

    ''' <summary>
    ''' Returns an image from the image file object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToImage() As System.Drawing.Image
        Using file As New FileStream(fileInfo.FullName, _
                                     FileMode.Open, _
                                     FileAccess.Read)
            Return System.Drawing.Image.FromStream(file)
        End Using
    End Function
End Class
