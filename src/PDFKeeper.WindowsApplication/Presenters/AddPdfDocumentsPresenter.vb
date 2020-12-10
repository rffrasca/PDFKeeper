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
Public Class AddPdfDocumentsPresenter
    Implements IDisposable
    Private ReadOnly view As IAddPdfDocumentsView
    Private model As PdfMetadataReader
    Private helper As PdfMetadataHelper
    Private ReadOnly passwordPrompt As IPasswordPromptView =
        New PasswordPromptView
    Private ReadOnly message As IMessageDisplayService =
        New MessageDisplayService
    Private ReadOnly question As IQuestionDisplayService =
        New QuestionDisplayService
    Private ReadOnly fileSelect As IFileSelectDisplayService =
        New FileSelectDisplayService
    Private ReadOnly pdfViewer As IRestrictedPdfViewerService =
        New RestrictedPdfViewerService
    Private stagedPdfPath As String

    Public Sub New(view As IAddPdfDocumentsView)
        Me.view = view
    End Sub

    Public Sub ResetView()
        With view
            .SelectedPdfPath = Nothing
            .Title = Nothing
            .AuthorPaired = Nothing
            .SubjectPaired = Nothing
            .Keywords = Nothing
            .Category = Nothing
            .SelectEnabled = True
            .ViewEnabled = False
            .TitleEnabled = False
            .SetTitleToFileNameEnabled = False
            .AuthorPairedEnabled = False
            .SubjectPairedEnabled = False
            .KeywordsEnabled = False
            .CategoryEnabled = False
            .FlagDocumentEnabled = False
            .DeleteSelectedPdfOnOkEnabled = False
            .SaveEnabled = False
            .PreviewEnabled = False
            .AddEnabled = False
            .DiscardEnabled = False
        End With
    End Sub

    Public Sub SelectPdf()
        fileSelect.Filter = "pdf"
        view.SelectedPdfPath = fileSelect.ShowOpen()
        If view.SelectedPdfPath.Length > 0 Then
            Dim selectedPdfPassword As SecureString = Nothing
            Dim containsPassword As Boolean = False
            Try
                Dim pdf As New PdfFileInfo(view.SelectedPdfPath)
                If pdf.ContainsOwnerPassword Then
                    passwordPrompt.Title = My.Resources.PdfOwnerPassword
                    passwordPrompt.TextLabel = My.Resources.EnterOwnerPassword
                    selectedPdfPassword = passwordPrompt.Show
                    If Not selectedPdfPassword Is Nothing Then
                        selectedPdfPassword.MakeReadOnly()
                        containsPassword = True
                    Else
                        view.SelectedPdfPath = Nothing
                        Exit Sub
                    End If
                End If
                If containsPassword Then
                    ' reader is not used because OWNER password validation
                    ' is performed in the constructor of iText's PdfReader
                    ' class. All that is needed is to validate the password
                    ' entered matches the OWNER password stored in the PDF
                    ' document.
                    Dim reader As New PdfMetadataReader(view.SelectedPdfPath,
                                                        selectedPdfPassword)
                    reader = reader    'Added to address CA1804 violation.
                End If
                view.SetCursor(True)
                ReadPdfInformationPropertiesIntoModel(view.SelectedPdfPath,
                                                      selectedPdfPassword)
                UpdateView()
                UploadService.Instance.CanUploadCycleStart = False
            Catch ex As BadPasswordException
                view.SelectedPdfPath = Nothing
                message.Show(ex.Message, True)
            Finally
                view.SetCursor(False)
            End Try
        End If
    End Sub

    Public Sub ViewSelectedPdf()
        pdfViewer.Open(view.SelectedPdfPath)
    End Sub

    Public Sub SetTitleToFileName()
        view.Title = Path.GetFileNameWithoutExtension(view.SelectedPdfPath)
    End Sub

    Public Sub RequiredTextElementChanged()
        If IO.File.Exists(stagedPdfPath) Then
            pdfViewer.Close()
        End If
        If view.Title.Length > 0 And
            view.AuthorPaired.Length > 0 And
            view.SubjectPaired.Length > 0 Then
            view.SaveEnabled = True
        Else
            view.SaveEnabled = False
        End If
        view.PreviewEnabled = False
        view.AddEnabled = False
    End Sub

    Public Sub SaveStagedPdf()
        view.SetCursor(True)
        pdfViewer.Close()
        helper.Write(stagedPdfPath,
                     view.Title.Trim,
                     view.AuthorPaired.Trim,
                     view.SubjectPaired.Trim,
                     view.Keywords.Trim)
        view.SaveEnabled = False
        view.PreviewEnabled = True
        view.AddEnabled = True
        view.SetCursor(False)
    End Sub

    Public Sub PreviewStagedPdf()
        pdfViewer.Open(stagedPdfPath)
    End Sub

    Public Sub AddStagedPdf()
        view.SetCursor(True)
        pdfViewer.Close()
        WriteStagedPdfSupplementalData()
        DeleteSelectedPdf()
        ResetView()
        UploadService.Instance.CanUploadCycleStart = True
        view.SetCursor(False)
    End Sub

    Public Sub Discard()
        If question.Show(My.Resources.GenericDiscardChangesPrompt,
                                False) = DialogResult.Yes Then
            pdfViewer.Close()
            DiscardStagedPdf()
            ResetView()
            UploadService.Instance.CanUploadCycleStart = True
        End If
    End Sub

    Public Function ViewClosingPrompt() As Boolean
        If question.ShowFormClosingPrompt Then
            pdfViewer.Close()
            DiscardStagedPdf()
            UploadService.Instance.CanUploadCycleStart = True
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ReadPdfInformationPropertiesIntoModel(ByVal pdfPath As String, _
                                                      ByVal pdfPassword As SecureString)
        Dim fileInfo As New FileInfo(view.SelectedPdfPath)
        stagedPdfPath = fileInfo.GenerateUploadStagingFilePath
        helper = New PdfMetadataHelper(pdfPath, pdfPassword)
        model = helper.Read
    End Sub

    Private Sub UpdateView()
        With view
            .Title = model.Title
            .AuthorPaired = model.Author
            .SubjectPaired = model.Subject
            .Keywords = model.Keywords
            .SelectEnabled = False
            .ViewEnabled = True
            .TitleEnabled = True
            .SetTitleToFileNameEnabled = True
            .AuthorPairedEnabled = True
            .SubjectPairedEnabled = True
            .KeywordsEnabled = True
            .CategoryEnabled = True
            .FlagDocumentEnabled = True
            .DeleteSelectedPdfOnOkEnabled = True
            .DiscardEnabled = True
        End With
    End Sub

    Private Sub WriteStagedPdfSupplementalData()
        Dim flag As Integer = 0
        If My.Settings.AddPdfFlagDocument Then
            flag = 1
        End If
        Dim suppDataHelper As New PdfSupplementalDataHelper(stagedPdfPath)
        suppDataHelper.Write(String.Empty, view.Category, flag)
    End Sub

    Private Sub DeleteSelectedPdf()
        If My.Settings.AddPdfDeleteInputPdfOnOK Then
            Dim fileInfo As New FileInfo(view.SelectedPdfPath)
            fileInfo.DeleteToRecycleBin()
        End If
    End Sub

    Private Sub DiscardStagedPdf()
        If Not stagedPdfPath Is Nothing Then
            IO.File.Delete(stagedPdfPath)
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                fileSelect.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
