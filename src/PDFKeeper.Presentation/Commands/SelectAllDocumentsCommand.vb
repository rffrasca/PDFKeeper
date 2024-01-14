' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

Imports PDFKeeper.Core.Commands

Public Class SelectAllDocumentsCommand
    Implements ICommand
    Private ReadOnly view As MainView
    Private ReadOnly check As Boolean

    ''' <summary>
    ''' Initializes a new instance of the SelectAllDocumentsCommand class that checks/unchecks all
    ''' documents in the DocumentsDataGridView when executed.
    ''' </summary>
    ''' <param name="view">The MainView instance.</param>
    ''' <param name="check">True to check all documents; False to uncheck all documents.</param>
    Public Sub New(view As MainView, check As Boolean)
        Me.view = view
        Me.check = check
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        view.Cursor = Cursors.WaitCursor
        For Each row In view.DocumentsDataGridView.Rows
            row.Cells(0).Value = check
        Next
        view.DocumentsDataGridView.RefreshEdit()
        view.Cursor = Cursors.Default
    End Sub
End Class
