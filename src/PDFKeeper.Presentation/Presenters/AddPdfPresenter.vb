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
Imports PDFKeeper.Common
Imports PDFKeeper.Domain
Imports PDFKeeper.FileIO
Imports PDFKeeper.Infrastructure
Imports PDFKeeper.Services

Public Class AddPdfPresenter
    Private ReadOnly view As IAddPdfView
    Private ReadOnly authorListSvc As IAuthorListService
    Private ReadOnly subjectListSvc As ISubjectListService
    Private ReadOnly categoryListSvc As ICategoryListService
    Private ReadOnly taxYearListSvc As ITaxYearListService
    Private ReadOnly pdfSvc As IPdfService
    Private ReadOnly commonDialogs As New CommonDialogs
    Private viewInstance As Form
    Private password As SecureString
    Private modifiedPdfFile As String
    Private bypassClosingPrompt As Boolean

    Public Sub New(ByVal view As IAddPdfView, ByVal authorListSvc As IAuthorListService,
                   ByVal subjectListSvc As ISubjectListService, ByVal categoryListSvc As ICategoryListService,
                   ByVal taxYearListSvc As ITaxYearListService, ByVal pdfSvc As IPdfService)
        Me.view = view
        Me.authorListSvc = authorListSvc
        Me.subjectListSvc = subjectListSvc
        Me.categoryListSvc = categoryListSvc
        Me.taxYearListSvc = taxYearListSvc
        Me.pdfSvc = pdfSvc
    End Sub

    Friend Sub AddPdfDialog_Load(sender As Object, e As EventArgs)
        viewInstance = CType(sender, Form)
        With view
            .SaveEnabled = False
            .PreviewEnabled = False
            .AddEnabled = False
            .ShowOpenPdfFileDialog()
            If .SelectedPdf.Length > 0 Then
                bypassClosingPrompt = True
                Dim pdfPasswordType = pdfSvc.GetPdfPasswordType(.SelectedPdf)
                If pdfPasswordType = PdfPasswordTypes.PdfPasswordType.None Then
                    UpdateView(.SelectedPdf, Nothing)
                ElseIf pdfPasswordType = PdfPasswordTypes.PdfPasswordType.Owner Then
                    Using PdfOwnerPasswordDialog
                        PdfOwnerPasswordDialog.ShowDialog()
                        If PdfOwnerPasswordDialog.DialogResult = DialogResult.OK Then
                            password = PdfOwnerPasswordDialog.Password
                        End If
                    End Using
                    If password IsNot Nothing Then
                        If password.Length > 0 Then
                            If pdfSvc.IsPdfOwnerPasswordValid(.SelectedPdf, password) = False Then
                                commonDialogs.ShowMessageBox(My.Resources.PdfOwnerPasswordInvalid, True)
                                viewInstance.Close()
                            Else
                                password.MakeReadOnly()
                                UpdateView(.SelectedPdf, password)
                            End If
                        Else
                            commonDialogs.ShowMessageBox(My.Resources.PdfOwnerPasswordRequired, True)
                            viewInstance.Close()
                        End If
                    Else
                        viewInstance.Close()
                    End If
                ElseIf pdfPasswordType = PdfPasswordTypes.PdfPasswordType.User Then
                    commonDialogs.ShowMessageBox(My.Resources.PdfContainsUserPassword, True)
                    viewInstance.Close()
                ElseIf pdfPasswordType = PdfPasswordTypes.PdfPasswordType.Unknown Then
                    commonDialogs.ShowMessageBox(My.Resources.PdfInvalid, True)
                    viewInstance.Close()
                End If
                bypassClosingPrompt = False
            Else
                viewInstance.Close()
            End If
        End With
    End Sub

    Friend Sub ViewButton_Click(sender As Object, e As EventArgs)
        pdfSvc.ShowPdfWithRestrictedViewer(view.SelectedPdf)
    End Sub

    Friend Sub SetTitleToFileNameButton_Click(sender As Object, e As EventArgs)
        view.Title = Path.GetFileNameWithoutExtension(view.SelectedPdf)
    End Sub

    Friend Sub CommonComboBox_Enter(sender As Object, e As EventArgs)
        Dim control = CType(sender, Control)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            Dim currentItem = control.Text
            If control.Name.StartsWith("Author") Then
                view.AuthorItems = authorListSvc.ListAuthors
            ElseIf control.Name.StartsWith("Subject") Then
                view.SubjectItems = subjectListSvc.ListSubjects(view.Author)
            ElseIf control.Name.StartsWith("Category") Then
                view.CategoryItems = categoryListSvc.ListCategories
            ElseIf control.Name.StartsWith("TaxYear") Then
                view.TaxYearItems = taxYearListSvc.ListRangeOfTaxYears
            End If
            control.Text = currentItem
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub Common_TextChanged(sender As Object, e As EventArgs)
        With view
            If .Title.Trim.Length > 0 And .Author.Trim.Length > 0 And .Subject.Trim.Length > 0 Then
                .SaveEnabled = True
            Else
                .SaveEnabled = False
            End If
            .PreviewEnabled = False
            If .AddEnabled Then
                .AddEnabled = False
                pdfSvc.CloseRestrictedViewer()
            End If
        End With
    End Sub

    Friend Sub SaveButton_Click(sender As Object, e As EventArgs)
        Dim control = CType(sender, Control)
        With view
            Dim model = New PdfInfoModel
            model.Title = .Title
            model.Author = .Author
            model.Subject = .Subject
            model.Keywords = .Keywords
            pdfSvc.CloseRestrictedViewer()
            pdfSvc.WritePdfWithInfo(.SelectedPdf, password, modifiedPdfFile, model)
            .SaveEnabled = False
            .PreviewEnabled = True
            .AddEnabled = True
        End With
        viewInstance.SelectNextControl(control, True, True, True, True)
    End Sub

    Friend Sub PreviewButton_Click(sender As Object, e As EventArgs)
        pdfSvc.ShowPdfWithRestrictedViewer(modifiedPdfFile)
    End Sub

    Friend Sub AddButton_Click(sender As Object, e As EventArgs)
        bypassClosingPrompt = True
        Dim model = New PdfInfoExtModel
        With view
            model.Category = .Category
            model.TaxYear = .TaxYear
            If .FlagDocument Then
                model.Flag = 1
            Else
                model.Flag = 0
            End If
            pdfSvc.CloseRestrictedViewer()
            pdfSvc.WritePdfInfoExt(modifiedPdfFile, model)
            pdfSvc.StagePdfForUpload(modifiedPdfFile)
            If My.Settings.AddPdfDeleteSelectedPdfWhenAdded Then
                Dim file = New FileInfo(.SelectedPdf)
                file.DeleteToRecycleBin
            End If
        End With
        viewInstance.Close()
    End Sub

    Friend Sub AddPdfDialog_FormClosing(sender As Object, e As FormClosingEventArgs)
        If view.SelectedPdf.Length > 0 And bypassClosingPrompt = False Then
            If commonDialogs.ShowQuestionMessageBox(My.Resources.CancelQuestion, False) = DialogResult.Yes Then
                pdfSvc.CloseRestrictedViewer()
                If modifiedPdfFile IsNot Nothing Then
                    IO.File.Delete(modifiedPdfFile)
                End If
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub UpdateView(ByVal pdfFile As String, ByVal password As SecureString)
        modifiedPdfFile = Path.Combine(AppFolders.GetPath(AppFolders.AppFolder.Temp),
                                       Path.GetFileName(pdfFile))
        Dim model = pdfSvc.ReadPdfInfo(pdfFile, password)
        With view
            .Title = model.Title
            .Author = model.Author
            .Subject = model.Subject
            .Keywords = model.Keywords
        End With
    End Sub
End Class
