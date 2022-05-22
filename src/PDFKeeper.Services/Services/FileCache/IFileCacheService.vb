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
Public Interface IFileCacheService
    ''' <summary>
    ''' Adds PDF to the Cache folder.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <param name="pdf">PDF file contents byte array</param>
    Sub AddPdfToCache(ByVal id As Integer, ByVal pdf As Byte())

    ''' <summary>
    ''' Creates preview image from cached PDF in the Cache folder.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <param name="density">DPI of the image to create</param>
    Sub CreatePreviewInCache(ByVal id As Integer, ByVal density As Decimal)

    ''' <summary>
    ''' Get full name of PDF in Cache folder.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <returns>Full path name of PDF</returns>
    Function GetCachedPdfFullName(ByVal id As Integer)

    ''' <summary>
    ''' Gets preview image from image file in the Cache folder. 
    ''' </summary>
    ''' <param name="id">Document ID</param>
    ''' <param name="pixelDensity">Output image pixels per inch (PPI)</param>
    ''' <returns>Image object</returns>
    Function GetPreviewFromCache(ByVal id As Integer, ByVal pixelDensity As Decimal) As Drawing.Image

    ''' <summary>
    ''' Deletes PDF from the Cache folder.
    ''' </summary>
    ''' <param name="id">Document ID</param>
    Sub DeletePdfFromCache(ByVal id As Integer)
End Interface
