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
Public Class AddPdfDocumentsViewPresenter
    Implements IDisposable
    Private view As IAddPdfDocumentsView
    Private model As PdfInformationPropertiesReader
    Private helper As PdfInformationPropertiesHelper
    Private passwordPrompt As IPasswordPrompt = New PasswordPrompt
    Private messageDisplay As IMessageDisplay = New MessageDisplay
    Private questionDisplay As IQuestionDisplay = New QuestionDisplay
    Private fileSelect As IFileSelect = New FileSelect
    Private pdfViewer As IRestrictedPdfViewer = New RestrictedPdfViewer
    Private uploadFacade As UploadFacade = uploadFacade.Instance
    Private stagedPdfPath As String

    Public Sub New(view As IAddPdfDocumentsView)
        Me.view = view
    End Sub

    Public Sub ResetView()
        view.SelectedPdfPath = Nothing
        view.Title = Nothing
        view.AuthorPaired = Nothing
        view.SubjectPaired = Nothing
        view.Keywords = Nothing
        view.Category = Nothing
        view.SelectEnabled = True
        view.ViewEnabled = False
        view.TitleEnabled = False
        view.SetTitleToFileNameEnabled = False
        view.AuthorPairedEnabled = False
        view.SubjectPairedEnabled = False
        view.KeywordsEnabled = False
        view.CategoryEnabled = False
        view.FlagDocumentEnabled = False
        view.DeleteSelectedPdfOnOkEnabled = False
        view.SaveEnabled = False
        view.PreviewEnabled = False
        view.AddEnabled = False
        view.DiscardEnabled = False
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
                    ' is performed in the constructor of iTextSharp's PdfReader
                    ' class. All that is needed is to validate the password
                    ' entered matches the OWNER password stored in the PDF
                    ' document.
                    Dim reader As New PdfInformationPropertiesReader(view.SelectedPdfPath, _
                                                                     selectedPdfPassword)
                    reader = reader    'Added to address CA1804 violation.
                End If
                view.OnLongRunningOperationStarted()
                ReadPdfInformationPropertiesIntoModel(view.SelectedPdfPath, _
                                                      selectedPdfPassword)
                UpdateView()
                uploadFacade.PauseUpload(True)
            Catch ex As BadPasswordException
                view.SelectedPdfPath = Nothing
                messageDisplay.Show(ex.Message, True)
            Finally
                view.OnLongRunningOperationFinished()
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
        If view.Title.Length > 0 And _
            view.AuthorPaired.Length > 0 And _
            view.SubjectPaired.Length > 0 Then
            view.SaveEnabled = True
        Else
            view.SaveEnabled = False
        End If
        view.PreviewEnabled = False
        view.AddEnabled = False
    End Sub

    Public Sub SaveStagedPdf()
        view.OnLongRunningOperationStarted()
        pdfViewer.Close()
        helper.Write(stagedPdfPath, _
                     view.Title.Trim, _
                     view.AuthorPaired.Trim, _
                     view.SubjectPaired.Trim, _
                     view.Keywords.Trim)
        view.SaveEnabled = False
        view.PreviewEnabled = True
        view.AddEnabled = True
        view.OnLongRunningOperationFinished()
    End Sub

    Public Sub PreviewStagedPdf()
        pdfViewer.Open(stagedPdfPath)
    End Sub

    Public Sub AddStagedPdf()
        view.OnLongRunningOperationStarted()
        pdfViewer.Close()
        WriteStagedPdfSupplementalData()
        DeleteSelectedPdf()
        ResetView()
        uploadFacade.PauseUpload(False)
        view.OnLongRunningOperationFinished()
    End Sub

    Public Sub Discard()
        If questionDisplay.Show(My.Resources.GenericDiscardChangesPrompt, _
                                False) = DialogResult.Yes Then
            pdfViewer.Close()
            DiscardStagedPdf()
            ResetView()
            uploadFacade.PauseUpload(False)
        End If
    End Sub

    Public Function ViewClosingPrompt() As Boolean
        If questionDisplay.ShowFormClosingPrompt Then
            pdfViewer.Close()
            DiscardStagedPdf()
            uploadFacade.PauseUpload(False)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ReadPdfInformationPropertiesIntoModel(ByVal pdfPath As String, _
                                                      ByVal pdfPassword As SecureString)
        Dim fileInfo As New FileInfo(view.SelectedPdfPath)
        stagedPdfPath = fileInfo.GenerateUploadStagingFilePath
        helper = New PdfInformationPropertiesHelper(pdfPath, pdfPassword)
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
