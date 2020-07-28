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
Public Class CacheFilePathName
    Private ReadOnly m_DocumentRecordId As Integer

    Public Sub New(ByVal documentRecordId As Integer)
        m_DocumentRecordId = documentRecordId
    End Sub

    ''' <summary>
    ''' Returns a PDF file path name combining the application Cache folder
    ''' path name and the file name of the PDF that starts with product name
    ''' and the document record ID.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Pdf As String
        Get
            Return Path.Combine(UserProfile.CachePath, _
                                My.Application.Info.ProductName & m_DocumentRecordId & ".pdf")
        End Get
    End Property

    ''' <summary>
    ''' Returns a PNG file path name combining the application Cache folder
    ''' path name and the file name of the image PNG that starts with product
    ''' name and the document record ID followed by "-", the preview image
    ''' resolution value, and then -000001.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PdfPreview As String
        Get
            Return Path.Combine(Path.GetDirectoryName(Pdf), _
                                Path.GetFileNameWithoutExtension(Pdf) & "-" & _
                                My.Settings.PreviewImageResolution & "-000001.png")
        End Get
    End Property
End Class
