'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2018 Robert F. Frasca
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
Public Class AddPdfDocumentViewPresenter
    Private view As IAddPdfDocumentView
    Private outputPdfFile As String
    Private viewerId As Integer

    Public Sub New(view As IAddPdfDocumentView)
        Me.view = view
    End Sub

    Public Sub ViewLoad()
        ReadOriginalPdfProperties()
        CreateOutputPdfPathName()
    End Sub

    Public Sub ViewOriginalPdf()
        ViewPdf(view.OriginalPdfFile)
    End Sub

    Public Sub TextChanged()
        If view.PreviewEnabled Then
            TerminateViewer()
        End If
        view.Title = view.Title.TrimStart
        view.Author = view.Author.TrimStart
        view.Subject = view.Subject.TrimStart
        view.Keywords = view.Keywords.TrimStart
        If view.Title.Length > 0 And _
            view.Author.Length > 0 And _
            view.Subject.Length > 0 Then
            view.SaveEnabled = True
            view.PreviewEnabled = False
            view.OkEnabled = False
        Else
            view.SaveEnabled = False
            view.PreviewEnabled = False
            view.OkEnabled = False
        End If
    End Sub

    Public Sub SetTitleToPdfFileName()
        view.Title = Path.GetFileNameWithoutExtension(view.OriginalPdfFile)
    End Sub

    Public Sub GetAuthors()
        SharedPresenterQueries.GetAuthors(view.Author, view.Authors)
    End Sub

    Public Sub GetSubjectsByAuthor()
        SharedPresenterQueries.GetSubjectsByAuthor(view.Author, _
                                                   view.Subject, _
                                                   view.Subjects)
    End Sub

    Public Sub SaveOutputPdf()
        UploadController.WaitWhileUploadRunning()
        TerminateViewer()
        Dim writer As PdfFileInfoPropertiesWriter
        If view.OriginalPdfFilePassword Is Nothing Then
            writer = New PdfFileInfoPropertiesWriter(view.OriginalPdfFile, _
                                                     outputPdfFile)
        Else
            writer = New PdfFileInfoPropertiesWriter(view.OriginalPdfFile,
                                                     outputPdfFile, _
                                                     view.OriginalPdfFilePassword)
        End If
        writer.Title = view.Title
        writer.Author = view.Author
        writer.Subject = view.Subject
        writer.Keywords = view.Keywords
        writer.Write()
        view.SaveEnabled = False
        view.PreviewEnabled = True
        view.OkEnabled = True
    End Sub

    Public Sub ViewOutputPdf()
        ViewPdf(outputPdfFile)
    End Sub

    Public Sub DeleteOriginalPdf()
        TerminateViewer()
        If My.Settings.DeleteOriginalPdfOnOK Then
            FileHelper.DeleteFileToRecycleBin(view.OriginalPdfFile)
        End If
    End Sub

    Public Sub DeleteOutputPdf()
        TerminateViewer()
        If Not outputPdfFile Is Nothing Then
            IO.File.Delete(outputPdfFile)
        End If
    End Sub

    Private Sub ReadOriginalPdfProperties()
        Dim reader As PdfFileInfoPropertiesReader
        If view.OriginalPdfFilePassword Is Nothing Then
            reader = New PdfFileInfoPropertiesReader(view.OriginalPdfFile)
        Else
            reader = New PdfFileInfoPropertiesReader(view.OriginalPdfFile, _
                                                     view.OriginalPdfFilePassword)
        End If
        view.OriginalPdfPathName = view.OriginalPdfFile
        view.Title = reader.Title
        view.Author = reader.Author
        view.Subject = reader.Subject
        view.Keywords = reader.Keywords
    End Sub

    Private Sub CreateOutputPdfPathName()
        outputPdfFile = FileHelper.AddGuidToFileName( _
            FileHelper.ChangeDirectoryName(view.OriginalPdfFile, _
                                           ApplicationDirectories.UploadStaging), _
                                       Nothing)
    End Sub

    Private Sub ViewPdf(ByVal file As String)
        If viewerId > 0 Then
            If ProcessHelper.IsProcessExpired(viewerId) Then
                viewerId = 0
            End If
        End If
        Dim pdfFile As New PdfFile(file)
        If viewerId > 0 Then
            ' Set viewerId to instance of viewer still open. Otherwise, viewer
            ' will not be killed by TerminateViewer.
            Dim currentId As Integer = viewerId
            viewerId = pdfFile.OpenRestricted()
            viewerId = currentId
        Else
            viewerId = pdfFile.OpenRestricted()
        End If
    End Sub

    Private Sub TerminateViewer()
        If viewerId > 0 Then
            ProcessHelper.Close(viewerId)
        End If
    End Sub
End Class
