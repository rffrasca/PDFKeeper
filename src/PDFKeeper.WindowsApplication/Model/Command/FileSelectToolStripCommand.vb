'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
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
Public Class FileSelectToolStripCommand
    Implements ICommand
    Private m_dataGridView As DataGridView
    Private m_stateChange As Enums.SelectionState

    Public Sub New(ByVal dataGridView As DataGridView, _
                   ByVal stateChange As Enums.SelectionState)
        m_dataGridView = dataGridView
        m_stateChange = stateChange
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        For Each row As DataGridViewRow In m_dataGridView.Rows
            If m_stateChange = Enums.SelectionState.SelectAll Then
                row.Cells(0).Value = True
            ElseIf m_stateChange = Enums.SelectionState.DeselectAll Then
                row.Cells(0).Value = False
            End If
        Next
        m_dataGridView.RefreshEdit()
    End Sub
End Class
