'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Class StringPrintService
    Implements IStringPrintService
    Private m_Value As String
    Private m_ValueCopy As String
    Private WithEvents printDocument As New PrintDocument

    Public Sub New(ByVal value As String)
        m_Value = value
        m_ValueCopy = m_Value
    End Sub

    Public Sub Print() Implements IStringPrintService.Print
        Using printDialog As New PrintDialog
            printDialog.Document = PrintDocument
            If printDialog.ShowDialog = DialogResult.OK Then
                PrintDocument.Print()
            End If
        End Using
    End Sub

    Public Sub PrintPreview(printPreviewFormSize As Size) Implements IStringPrintService.PrintPreview
        Using printPreview As New PrintPreviewDialog
            printPreview.Document = printDocument
            printPreview.ClientSize = printPreviewFormSize
            printPreview.ShowIcon = False
            printPreview.UseAntiAlias = True
            printPreview.ShowDialog()
        End Using
    End Sub

    Private Sub Document_PrintPage(ByVal sender As Object, _
                                   ByVal e As PrintPageEventArgs) Handles printDocument.PrintPage
        Using font As New Font("Lucida Console", 10)
            Dim charactersOnPage As Integer = 0
            Dim linesPerPage As Integer = 0
            e.Graphics.MeasureString(m_Value, _
                                     font, _
                                     e.MarginBounds.Size, _
                                     StringFormat.GenericTypographic, _
                                     charactersOnPage, _
                                     linesPerPage)
            e.Graphics.DrawString(m_Value, _
                                  font, _
                                  Brushes.Black, _
                                  e.MarginBounds, _
                                  StringFormat.GenericTypographic)
            m_Value = m_Value.Substring(charactersOnPage)
            e.HasMorePages = m_Value.Length > 0
        End Using
    End Sub

    Private Sub Document_EndPrint() Handles printDocument.EndPrint
        m_Value = m_ValueCopy
    End Sub
End Class
