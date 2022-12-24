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
Imports System.Drawing
Imports System.IO
Imports PDFKeeper.Common
Imports PDFKeeper.FileIO

Public Class FileCacheService
    Implements IFileCacheService
    Private ReadOnly fileHashes As New Dictionary(Of String, String)

    Public Sub AddPdfToCache(id As Integer, pdf As Byte()) Implements IFileCacheService.AddPdfToCache
        If pdf Is Nothing Then
            Throw New ArgumentNullException(NameOf(pdf))
        End If
        Dim cached As Boolean
        Dim pdfFile = New PdfFile(GetPdfFullName(id))
        If pdfFile.Exists Then
            Try
                If pdfFile.Hash = fileHashes.Item(pdfFile.FullName) Then
                    cached = True
                End If
            Catch ex As KeyNotFoundException
            End Try
        End If
        If cached = False Then
            pdf.ToFile(pdfFile.FullName)
            If fileHashes.ContainsKey(pdfFile.FullName) Then
                fileHashes.Item(pdfFile.FullName) = pdfFile.Hash
            Else
                fileHashes.Add(pdfFile.FullName, pdfFile.Hash)
            End If
        End If
    End Sub

    Public Sub CreatePreviewInCache(id As Integer, pixelDensity As Decimal) Implements IFileCacheService.CreatePreviewInCache
        Dim cached As Boolean
        Dim pdfFile = New PdfFile(GetPdfFullName(id))
        If pdfFile.Exists = False Then
            Throw New FileNotFoundException
        End If
        Dim imageFile = New ImageFile(GetPdfPreviewFullName(id, pixelDensity))
        If imageFile.Exists Then
            Try
                If imageFile.Hash = fileHashes.Item(imageFile.FullName) Then
                    cached = True
                End If
            Catch ex As KeyNotFoundException
            End Try
        End If
        If cached = False Then
            pdfFile.CreatePreviewImage(pixelDensity).ToFile(imageFile.FullName)
            fileHashes.Add(imageFile.FullName, imageFile.Hash)
        End If
    End Sub

    Public Function GetPreviewFromCache(id As Integer, pixelDensity As Decimal) As Image Implements IFileCacheService.GetPreviewFromCache
        Dim imageFile = New ImageFile(GetPdfPreviewFullName(id, pixelDensity))
        If imageFile.Exists Then
            Return imageFile.Image
        Else
            Throw New FileNotFoundException
        End If
    End Function

    Public Function GetCachedPdfFullName(id As Integer) As Object Implements IFileCacheService.GetCachedPdfFullName
        Return GetPdfFullName(id)
    End Function

    Public Sub DeletePdfFromCache(id As Integer) Implements IFileCacheService.DeletePdfFromCache
        Dim pdfFile = New FileInfo(GetPdfFullName(id))
        If pdfFile.Exists Then
            pdfFile.Delete()
        End If
        If fileHashes.ContainsKey(pdfFile.FullName) Then
            fileHashes.Remove(pdfFile.FullName)
        End If
    End Sub

    Private Function GetPdfFullName(ByVal id As Integer) As String
        Return Path.Combine(AppFolders.GetPath(AppFolders.AppFolder.Cache),
                            String.Concat(My.Application.Info.ProductName, id, ".pdf"))
    End Function

    Private Function GetPdfPreviewFullName(ByVal id As Integer, ByVal pixelDensity As Decimal) As String
        Return Path.Combine(AppFolders.GetPath(AppFolders.AppFolder.Cache),
                            String.Concat(My.Application.Info.ProductName, id, "-", pixelDensity, ".png"))
    End Function
End Class
