' *****************************************************************************
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

Public Class StatusBarToggleCommand
    Implements ICommand
    Private ReadOnly view As MainView

    ''' <summary>
    ''' Initializes a new instance of the StatusBarToggleCommand class that toggles the View Status
    ''' Bar menu item Checked state which controls the StatusStrip Visible state when executed.
    ''' </summary>
    ''' <param name="view">The MainView instance.</param>
    Public Sub New(view As MainView)
        Me.view = view
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        With view
            If .ViewStatusBarToolStripMenuItem.Checked Then
                .ViewStatusBarToolStripMenuItem.Checked = False
            Else
                .ViewStatusBarToolStripMenuItem.Checked = True
            End If
            .StatusStrip.Visible = .ViewStatusBarToolStripMenuItem.Checked
        End With
    End Sub
End Class
