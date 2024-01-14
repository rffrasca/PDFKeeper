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

Public Class ViewCommand
    Implements ICommand
    Private ReadOnly view As MainView

    ''' <summary>
    ''' Initializes a new instance of the ViewCommand class that sets the View Tool Bar and Status
    ''' Bar menu items Checked state based on the ToolStrip and StatusStrip Visible states when
    ''' executed.
    ''' </summary>
    ''' <param name="view">The MainView instance.</param>
    Public Sub New(view As MainView)
        Me.view = view
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        With view
            .ViewToolBarToolStripMenuItem.Checked = .ToolStrip.Visible
            .ViewStatusBarToolStripMenuItem.Checked = .StatusStrip.Visible
        End With
    End Sub
End Class
