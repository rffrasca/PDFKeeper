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

Public Class DialogShowCommand
    Implements ICommand
    Private ReadOnly dialog As Form
    Private ReadOnly postCommand As ICommand

    ''' <summary>
    ''' Initializes a new instance of the DialogShowCommand class that shows a dialog when
    ''' executed.
    ''' </summary>
    ''' <param name="dialog">The dialog instance.</param>
    ''' <param name="postCommand">
    ''' The ICommand object to execute after dialog has been closed or Nothing.
    ''' </param>
    Public Sub New(dialog As Form, postCommand As ICommand)
        Me.dialog = dialog
        Me.postCommand = postCommand
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        dialog.ShowDialog()
        If postCommand IsNot Nothing Then
            postCommand.Execute()
        End If
    End Sub
End Class
