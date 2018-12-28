'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
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
Public Class MainViewDocumentDataPresenter
    Private view As IMainViewDocumentData
    Private fileHashes As New GenericDictionaryList(Of String, String)
    Private lastDocumentNotes As String

    Public Sub New(view As IMainViewDocumentData)
        Me.view = view
    End Sub

    Public Sub GetSelectedDocumentData()
        If view.DocumentId > 0 Then
            Dim tasks(5) As Task
            tasks(1) = Task.Run(Sub() GetDocumentPdf())
            tasks(2) = Task.Run(Sub() GetDocumentNotes())
            tasks(3) = Task.Run(Sub() GetDocumentKeywords())
            Try
                tasks(1).Wait()
            Catch ex As AggregateException
                For Each e In ex.InnerExceptions
                    Dim displayService As IMessageDisplayService = _
                        New MessageDisplayService
                    If e.GetType.Name = "InvalidOperationException" Then
                        displayService.ShowError(String.Format( _
                                                 CultureInfo.CurrentCulture, _
                                                 My.Resources.ResourceManager.GetString( _
                                                     "DocumentRecordMayHaveBeenDeleted"), _
                                                 e.Message))
                    Else
                        displayService.ShowError(e.Message)
                    End If
                Next
                ResetDocumentDataPanel()
                Exit Sub
            End Try
            tasks(4) = Task.Run(Sub() GetDocumentPdfPreview())
            tasks(5) = Task.Run(Sub() GetDocumentPdfText())
            Try
                tasks(2).Wait()
            Catch ex As AggregateException
                For Each e In ex.InnerExceptions
                    Dim displayService As IMessageDisplayService = _
                        New MessageDisplayService
                    displayService.ShowError(e.Message)
                Next
                ResetDocumentDataPanel()
                Exit Sub
            End Try
            view.DocumentDataPanelEnabled = True
        Else
            ResetDocumentDataPanel()
        End If
    End Sub

    Public Sub DoNotesTextBoxTextChanged()
        view.DocumentNotes = view.DocumentNotes.TrimStart
        If view.DocumentNotes <> lastDocumentNotes Then ' IsNot would not work here.
            view.DocumentNotesChanged = True
        Else
            view.DocumentNotesChanged = False
        End If
    End Sub

    Public Sub ReloadPreview()
        Task.Run(Sub() GetDocumentPdfPreview())
    End Sub

    Public Sub SetDocumentNotes()
        view.DocumentNotes = view.DocumentNotes.Trim
        Dim nonQueryService As INonQueryService = Nothing
        NonQueryServiceHelper.SetNonQueryService(nonQueryService)
        nonQueryService.SetDocumentNotes(view.DocumentId, view.DocumentNotes)
        lastDocumentNotes = view.DocumentNotes
    End Sub

    Public Sub RestoreDocumentNotes()
        view.DocumentNotes = lastDocumentNotes
        lastDocumentNotes = view.DocumentNotes
    End Sub

    Public Sub InsertDateTimeAndTextIntoDocumentNotes()
        view.DocumentNotes = _
            view.DocumentNotes.InsertDateTimeAndText(My.Settings.LoginUsername)
    End Sub

    Public Function IsOkayForViewToClose() As Boolean
        Dim result As DialogResult
        Dim displayService As IMessageDisplayService = New MessageDisplayService
        result = displayService.ShowQuestion(My.Resources.DocumentNotesModified, True)
        If result = Windows.Forms.DialogResult.Yes Then
            SetDocumentNotes()
            Return True
        ElseIf result = DialogResult.No Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ResetDocumentDataPanel()
        lastDocumentNotes = Nothing
        view.DocumentDataPanelEnabled = False
        view.DocumentNotes = Nothing
        view.DocumentKeywords = Nothing
        view.DocumentPreview = Nothing
        view.DocumentText = Nothing
    End Sub

    Private Sub GetDocumentPdf()
        Dim cached As Boolean = False
        Dim pdfFile As New PdfFile( _
            FilePathNameGenerator.GenerateCachePdfFilePathName(view.DocumentId))
        If pdfFile.Exists Then
            If pdfFile.ComputeHash = fileHashes.GetItem(pdfFile.FullName) Then
                cached = True
            End If
        End If
        If cached = False Then
            Dim queryService As IQueryService = Nothing
            QueryServiceHelper.SetQueryService(queryService)
            queryService.GetDocumentPdf(view.DocumentId, pdfFile.FullName)
            fileHashes.SetItem(pdfFile.FullName, pdfFile.ComputeHash)
        End If
    End Sub

    Private Sub GetDocumentNotes()
        Dim queryService As IQueryService = Nothing
        QueryServiceHelper.SetQueryService(queryService)
        Dim dataTableNotes As DataTable = queryService.GetDocumentNotes(view.DocumentId)
        view.DocumentNotes = Convert.ToString(dataTableNotes.Rows(0)("doc_notes"), _
                                              CultureInfo.CurrentCulture)
        lastDocumentNotes = view.DocumentNotes
        view.DocumentNotesChanged = False
    End Sub

    Private Sub GetDocumentKeywords()
        Dim queryService As IQueryService = Nothing
        QueryServiceHelper.SetQueryService(queryService)
        Dim dataTableKeywords As DataTable = queryService.GetDocumentKeywords(view.DocumentId)
        view.DocumentKeywords = Convert.ToString(dataTableKeywords.Rows(0)("doc_keywords"), _
                                                 CultureInfo.CurrentCulture)
    End Sub

    Private Sub GetDocumentPdfPreview()
        Dim cached As Boolean = False
        Dim pdfFile As New PdfFile( _
            FilePathNameGenerator.GenerateCachePdfFilePathName(view.DocumentId))
        Dim imageFile As New ImageFile( _
            FilePathNameGenerator.GenerateCachePdfPreviewFilePathName(view.DocumentId))
        If imageFile.Exists Then
            If imageFile.ComputeHash = fileHashes.GetItem(imageFile.FullName) Then
                cached = True
            End If
        End If
        If cached = False Then
            pdfFile.GetPreviewImageToFile(My.Settings.PreviewImageResolution)
            fileHashes.SetItem(imageFile.FullName, imageFile.ComputeHash)
        End If
        view.DocumentPreview = imageFile.ToImage
    End Sub

    Private Sub GetDocumentPdfText()
        Dim pdfFile As New PdfFile( _
            FilePathNameGenerator.GenerateCachePdfFilePathName(view.DocumentId))
        view.DocumentText = pdfFile.GetText
    End Sub
End Class
