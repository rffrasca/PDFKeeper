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
Public Class FileNewToolStripCommand
    Implements ICommand

    Public Sub Execute() Implements ICommand.Execute
        AddSelectedPdfFile()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1804:RemoveUnusedLocals", MessageId:="reader")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Private Sub AddSelectedPdfFile()
        Dim selectedPdfFile As String = SelectPdfFile()
        If selectedPdfFile.Length > 0 Then
            Dim containsPassword As Boolean = False
            Dim selectedPdfFilePassword As New SecureString
            Try
                Dim pdfFile As New PdfFile(selectedPdfFile)
                If pdfFile.ContainsOwnerPassword Then
                    selectedPdfFilePassword = ShowPasswordDialog()
                    If Not selectedPdfFilePassword Is Nothing Then
                        containsPassword = True
                    Else
                        Exit Sub
                    End If
                End If
                If containsPassword Then
                    ' Validate the password entered matches the OWNER
                    ' password in the PDF document by instantiating
                    ' a PdfFileInfoPropertiesReader object.
                    Dim reader As PdfFileInfoPropertiesReader = _
                        New PdfFileInfoPropertiesReader(selectedPdfFile, _
                                                        selectedPdfFilePassword)
                Else
                    selectedPdfFilePassword = Nothing
                End If
                UploadController.UploadPaused = True
                ShowAddPdfDocumentDialog(selectedPdfFile, selectedPdfFilePassword)
                UploadController.UploadPaused = False
            Catch ex As BadPasswordException
                Dim displayService As IMessageDisplayService = New MessageDisplayService
                displayService.ShowError(ex.Message)
            Finally
                If containsPassword Then
                    selectedPdfFilePassword.Dispose()
                End If
            End Try
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2000:Dispose objects before losing scope")> _
    Private Function SelectPdfFile() As String
        Dim fileService As IFileDialogDisplayService = New FileDialogDisplayService(Nothing, _
                                                                                    "pdf")
        Return fileService.OpenDialog
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    Private Function ShowPasswordDialog() As SecureString
        Using dialog As New PasswordDialog(My.Resources.PdfOwnerPassword, _
                                           My.Resources.EnterOwnerPassword)
            dialog.ShowDialog()
            If dialog.DialogResult = DialogResult.OK Then
                Return dialog.Password
            Else
                Return Nothing
            End If
        End Using
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", _
        "CA1822:MarkMembersAsStatic")> _
    Private Sub ShowAddPdfDocumentDialog(ByVal selectedPdfFile As String, _
                                                ByVal selectedPdfFilePassword As SecureString)
        Using dialog As New AddPdfDocumentDialog(selectedPdfFile, _
                                                 selectedPdfFilePassword)
            dialog.ShowDialog()
        End Using
    End Sub
End Class
