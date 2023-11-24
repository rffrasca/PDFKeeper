﻿' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2023 Robert F. Frasca
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

Public Class CutCommand
    Implements ICommand
    Private ReadOnly view As MainView
    Private ReadOnly presenter As MainPresenter

    ''' <summary>
    ''' Initializes a new instance of the CutCommand class that cuts the selected text from the
    ''' Notes text box into the clipboard when executed.
    ''' </summary>
    ''' <param name="view">The MainView instance.</param>
    ''' <param name="presenter">The MainPresenter instance.</param>
    Public Sub New(view As MainView, presenter As MainPresenter)
        Me.view = view
        Me.presenter = presenter
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        view.NotesTextBox.Cut()
        TextBoxHelper.SyncSelectedTextWithViewModel(view.NotesTextBox, view, presenter.ViewModel)
        presenter.SetStateForTextBoxSelectedText()
    End Sub
End Class