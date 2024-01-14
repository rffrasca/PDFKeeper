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
Imports PDFKeeper.Core.Presenters

Public Class PrintTextCommand
    Implements ICommand
    Private ReadOnly presenter As MainPresenter
    Private ReadOnly usePrintPreview As Boolean

    ''' <summary>
    ''' Initializes a new instance of the PrintTextCommand class that provides document data text
    ''' printing when executed.
    ''' </summary>
    ''' <param name="presenter">The MainPresenter instance.</param>
    ''' <param name="usePrintPreview">True or False to use Print Preview.</param>
    Public Sub New(presenter As MainPresenter, usePrintPreview As Boolean)
        Me.presenter = presenter
        Me.usePrintPreview = usePrintPreview
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        presenter.PrintDocumentDataText(usePrintPreview, My.Settings.MainViewSize)
    End Sub
End Class
