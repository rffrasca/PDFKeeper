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
Public Class FilePrintToolStripCommand
    Implements ICommand
    Private m_Text As String
    Private m_TextCopy As String
    Private m_UsePreview As Boolean
    Private m_PrintPreviewFormSize As System.Drawing.Size
    Private WithEvents printDocument As New PrintDocument

    Public Sub New(ByVal text As String, ByVal usePreview As Boolean)
        m_Text = text
        m_TextCopy = m_Text
        m_UsePreview = usePreview
    End Sub

    Public Sub New(ByVal text As String, _
                   ByVal usePreview As Boolean, _
                   ByVal printPreviewFormSize As System.Drawing.Size)
        m_Text = text
        m_TextCopy = m_Text
        m_UsePreview = usePreview
        m_PrintPreviewFormSize = printPreviewFormSize
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        If m_UsePreview = False Then
            Print()
        Else
            PrintPreview()
        End If
    End Sub

    Private Sub Print()
        Using printDialog As New PrintDialog
            printDialog.Document = printDocument
            If printDialog.ShowDialog = DialogResult.OK Then
                printDocument.Print()
            End If
        End Using
    End Sub

    Private Sub PrintPreview()
        Using printPreview As New PrintPreviewDialog
            printPreview.Document = printDocument
            printPreview.ClientSize = m_PrintPreviewFormSize
            printPreview.ShowIcon = False
            printPreview.UseAntiAlias = True
            printPreview.ShowDialog()
        End Using
    End Sub

    Private Sub Document_PrintPage(ByVal sender As Object, _
                                   ByVal e As PrintPageEventArgs) Handles PrintDocument.PrintPage
        Using font As New Font("Lucida Console", 10)
            Dim charactersOnPage As Integer = 0
            Dim linesPerPage As Integer = 0
            e.Graphics.MeasureString(m_Text, _
                                     font, _
                                     e.MarginBounds.Size, _
                                     StringFormat.GenericTypographic, _
                                     charactersOnPage, _
                                     linesPerPage)
            e.Graphics.DrawString(m_Text, _
                                  font, _
                                  Brushes.Black, _
                                  e.MarginBounds, _
                                  StringFormat.GenericTypographic)
            m_Text = m_Text.Substring(charactersOnPage)
            e.HasMorePages = m_Text.Length > 0
        End Using
    End Sub

    Private Sub Document_EndPrint() Handles PrintDocument.EndPrint
        m_Text = m_TextCopy
    End Sub
End Class
