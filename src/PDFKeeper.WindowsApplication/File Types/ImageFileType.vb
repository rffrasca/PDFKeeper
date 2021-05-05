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
Public Class ImageFileType
    Inherits FileTypeBase

    Public Sub New(ByVal imagePath As String)
        fileInfo = New FileInfo(imagePath)
    End Sub

    ''' <summary>
    ''' Returns an image from the object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToImage() As System.Drawing.Image
        Using file As New FileStream(fileInfo.FullName,
                                     FileMode.Open,
                                     FileAccess.Read)
            Return System.Drawing.Image.FromStream(file)
        End Using
    End Function
End Class
