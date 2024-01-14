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

Public Class FlagStateToggleCommand
    Implements ICommand
    Private ReadOnly presenter As MainPresenter

    ''' <summary>
    ''' Initializes a new instance of the FlagStateToggleCommand class that updates the flag state
    ''' of the selected document when executed.
    ''' </summary>
    ''' <param name="presenter">The MainPresenter instance.</param>
    Public Sub New(presenter As MainPresenter)
        Me.presenter = presenter
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        presenter.UpdateCurrentDocumentFlagState()
    End Sub
End Class
