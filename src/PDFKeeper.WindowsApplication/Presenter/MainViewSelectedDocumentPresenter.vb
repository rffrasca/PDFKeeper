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
Public Class MainViewSelectedDocumentPresenter
    Private view As IMainViewSelectedDocument
    Private fileHashes As New GenericDictionaryList(Of String, String)
    Private lastDocumentNotes As String
    Private preUpdateDocumentNotes As String

    Public Sub New(view As IMainViewSelectedDocument)
        Me.view = view
    End Sub

    Public Function MainViewClosing() As Boolean
        Dim result As DialogResult
        Dim displayService As IMessageDisplayService = _
            New MessageDisplayService
        result = displayService.ShowQuestion( _
            My.Resources.DocumentNotesModified, True)
        If result = Windows.Forms.DialogResult.Yes Then
            UpdateDocumentNotes()
            Return True
        ElseIf result = DialogResult.No Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub FileSaveToolStripMenuItemClick()
        Try
            GetDocumentNotes(False)
        Catch ex As IndexOutOfRangeException
            Dim displayService As IMessageDisplayService = _
                        New MessageDisplayService
            displayService.ShowError(String.Format( _
                                     CultureInfo.CurrentCulture, _
                                     My.Resources.ResourceManager.GetString( _
                                         "DocumentRecordMayHaveBeenDeleted"), _
                                     ex.Message))
            ClearSelectedDocument()
            Exit Sub
        End Try
        If preUpdateDocumentNotes = lastDocumentNotes Then
            UpdateDocumentNotes()
        Else
            Clipboard.SetText(view.DocumentNotes.Trim)
            GetDocumentNotes(True)
            Dim displayService As IMessageDisplayService = _
                New MessageDisplayService
            displayService.ShowError(My.Resources.UnableToSaveDocumentNotes)
        End If
    End Sub

    Public Sub EditRestoreToolStripMenuItemClick()
        view.DocumentNotes = lastDocumentNotes
        lastDocumentNotes = view.DocumentNotes
    End Sub

    Public Sub EditDateTimeToolStripMenuItemClick()
        view.DocumentNotes = _
            view.DocumentNotes.InsertDateTimeAndText(My.Settings.LoginUsername)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
        "CA1726:UsePreferredTerms", _
        MessageId:="Flag")> _
    Public Sub EditFlagDocumentToolStripMenuItemClick()
        Dim docsDao As IDocsDao = New DocsDao
        docsDao.UpdateFlagStateById(view.DocumentId, view.DocumentFlagState)
    End Sub

    Public Sub ViewSetPreviewImageResolutionToolStripMenuItemClick()
        Task.Run(Sub() GetDocumentPdfPreview())
    End Sub

    Public Sub SearchResultsDataGridViewSelectionChanged()
        If view.DocumentId > 0 Then
            Dim tasks(6) As Task
            tasks(1) = Task.Run(Sub() GetDocumentPdf())
            tasks(2) = Task.Run(Sub() GetDocumentNotes(True))
            tasks(3) = Task.Run(Sub() GetDocumentKeywords())
            tasks(4) = Task.Run(Sub() GetFlagState())
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
                ClearSelectedDocument()
                Exit Sub
            End Try
            tasks(5) = Task.Run(Sub() GetDocumentPdfPreview())
            tasks(6) = Task.Run(Sub() GetDocumentPdfText())
            Try
                tasks(2).Wait()
            Catch ex As AggregateException
                For Each e In ex.InnerExceptions
                    Dim displayService As IMessageDisplayService = _
                        New MessageDisplayService
                    displayService.ShowError(e.Message)
                Next
                ClearSelectedDocument()
                Exit Sub
            End Try
            view.RightTabControlEnabled = True
        Else
            ClearSelectedDocument()
        End If
    End Sub

    Public Sub NotesTextBoxTextChanged()
        view.DocumentNotes = view.DocumentNotes.TrimStart
        If view.DocumentNotes <> lastDocumentNotes Then ' IsNot would not work here.
            view.DocumentNotesChanged = True
        Else
            view.DocumentNotesChanged = False
        End If
    End Sub

    Private Sub ClearSelectedDocument()
        lastDocumentNotes = Nothing
        view.RightTabControlEnabled = False
        view.DocumentNotes = Nothing
        view.DocumentKeywords = Nothing
        view.DocumentPreview = Nothing
        view.DocumentText = Nothing
        view.DocumentFlagState = Nothing
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
            Dim docsDao As IDocsDao = New DocsDao
            docsDao.GetPdfById(view.DocumentId, pdfFile.FullName)
            fileHashes.SetItem(pdfFile.FullName, pdfFile.ComputeHash)
        End If
    End Sub

    Private Sub GetDocumentNotes(ByVal updateView As Boolean)
        Dim docsDao As IDocsDao = New DocsDao
        Dim dataTableNotes As DataTable = docsDao.GetNotesById(view.DocumentId)
        Dim notes As String = Convert.ToString(dataTableNotes.Rows(0)("doc_notes"), _
                                               CultureInfo.CurrentCulture)
        If updateView Then
            view.DocumentNotes = notes
            lastDocumentNotes = view.DocumentNotes
            view.DocumentNotesChanged = False
        Else
            preUpdateDocumentNotes = notes
        End If
    End Sub

    Private Sub GetDocumentKeywords()
        Dim docsDao As IDocsDao = New DocsDao
        Dim dataTableKeywords As DataTable = docsDao.GetKeywordsById(view.DocumentId)
        view.DocumentKeywords = Convert.ToString(dataTableKeywords.Rows(0)("doc_keywords"), _
                                                 CultureInfo.CurrentCulture)
    End Sub

    Private Sub GetFlagState()
        Dim docsDao As IDocsDao = New DocsDao
        Dim dataTableFlagState As DataTable = docsDao.GetFlagStateById(view.DocumentId)
        view.DocumentFlagState = Convert.ToInt32(dataTableFlagState.Rows(0)("doc_flag"), _
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

    Private Sub UpdateDocumentNotes()
        view.DocumentNotes = view.DocumentNotes.Trim
        Dim docsDao As IDocsDao = New DocsDao
        docsDao.UpdateNotesById(view.DocumentId, view.DocumentNotes)
        lastDocumentNotes = view.DocumentNotes
    End Sub
End Class
