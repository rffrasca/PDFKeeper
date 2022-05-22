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
Imports System.Security
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.FileIO

Public Class PdfService
    Implements IPdfService

    Public Function GetPdfPasswordType(pdfFile As String) As PdfPasswordTypes.PdfPasswordType Implements IPdfService.GetPdfPasswordType
        Return New PdfFile(pdfFile).PasswordType
    End Function

    Public Function IsPdfOwnerPasswordValid(pdfFile As String, password As SecureString) As Boolean Implements IPdfService.IsPdfOwnerPasswordValid
        Return New PdfOwnerPasswordValidator(pdfFile, password).IsValid
    End Function

    Public Sub ShowPdf(pdfFile As String, useDefaultApp As Boolean) Implements IPdfService.ShowPdf
        Dim viewer = New PdfViewer(pdfFile)
        viewer.Show(useDefaultApp)
    End Sub

    Public Sub ShowPdfWithRestrictedViewer(pdfFile As String) Implements IPdfService.ShowPdfWithRestrictedViewer
        Dim viewer = New RestrictedPdfViewer(pdfFile)
        viewer.ShowPdf()
    End Sub

    Public Sub CloseRestrictedViewer() Implements IPdfService.CloseRestrictedViewer
        RestrictedPdfViewer.Close()
    End Sub

    Public Function ReadPdfInfo(pdfFile As String, password As SecureString) As PdfInfoModel Implements IPdfService.ReadPdfInfo
        Dim info = New PdfInfo(pdfFile, password)
        Dim model = New PdfInfoModel
        With model
            .Title = info.Title
            .Author = info.Author
            .Subject = info.Subject
            .Keywords = info.Keywords
        End With
        Return model
    End Function

    Public Sub WritePdfWithInfo(sourcePdfFile As String, sourcePdfPassword As SecureString, targetPdfFile As String, model As PdfInfoModel) Implements IPdfService.WritePdfWithInfo
        If model Is Nothing Then
            Throw New NullReferenceException(NameOf(model))
        End If
        Dim pdf = New PdfInfo(sourcePdfFile, sourcePdfPassword)
        With model
            pdf.Title = .Title.Trim
            pdf.Author = .Author.Trim
            pdf.Subject = .Subject.Trim
            pdf.Keywords = .Keywords.Trim
        End With
        pdf.Write(targetPdfFile)
    End Sub

    Public Function ReadPdfInfoExt(pdfFile As String) As PdfInfoExtModel Implements IPdfService.ReadPdfInfoExt
        Dim xmlFile = Path.ChangeExtension(pdfFile, "xml")
        Dim model = New PdfInfoExtModel
        If File.Exists(xmlFile) Then
            model = XmlSerializer.Deserialize(Of PdfInfoExtModel)(xmlFile)
        End If
        Return model
    End Function

    Public Sub WritePdfInfoExt(pdfFile As String, model As PdfInfoExtModel) Implements IPdfService.WritePdfInfoExt
        If model Is Nothing Then
            Throw New ArgumentNullException(NameOf(model))
        End If
        Dim xmlFile = Path.ChangeExtension(pdfFile, "xml")
        XmlSerializer.Serialize(Of PdfInfoExtModel)(model, xmlFile)
    End Sub

    Public Sub StagePdfForUpload(pdfFile As String) Implements IPdfService.StagePdfForUpload
        Dim pdf = New FileInfo(pdfFile)
        Dim xmlFile = Path.ChangeExtension(pdfFile, "xml")
        Dim destPdfPath = pdf.GenerateNewPathNameWithGuid(AppFolders.GetPath(
                                                          AppFolders.AppFolder.UploadStaging))
        Dim destXmlPath = Path.ChangeExtension(destPdfPath.FullName, "xml")
        pdf.MoveTo(destPdfPath.FullName)
        If File.Exists(xmlFile) Then
            File.Move(Path.ChangeExtension(pdfFile, "xml"), destXmlPath)
        End If
    End Sub
End Class
