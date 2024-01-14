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

Public Class HelpFileShowCommand
    Implements ICommand
    Private ReadOnly helpFile As HelpFile
    Private ReadOnly view As MainView

    ''' <summary>
    ''' Initializes a new instance of the HelpFileShowCommand class that shows the
    ''' "Using PDFKeeper.html" help topic when executed.
    ''' </summary>
    ''' <param name="helpFile">The HelpFile instance.</param>
    ''' <param name="view">The MainView instance.</param>
    Public Sub New(helpFile As HelpFile, view As MainView)
        Me.helpFile = helpFile
        Me.view = view
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        helpFile.Show("Using PDFKeeper.html", view)
    End Sub
End Class
