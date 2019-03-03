'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management System
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
Public Class ViewToggleRightPanelToolStripCommand
    Implements ICommand
    Private m_DataGridView As DataGridView
    Private m_SplitContainer As SplitContainer

    Public Sub New(ByVal dataGridView As DataGridView, _
                   ByVal splitContainer As SplitContainer)
        m_DataGridView = dataGridView
        m_SplitContainer = splitContainer
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        m_DataGridView.Columns(5).AutoSizeMode = _
            DataGridViewAutoSizeColumnMode.DisplayedCells
        If m_SplitContainer.Panel2Collapsed Then
            m_SplitContainer.Panel2Collapsed = False
        Else
            m_SplitContainer.Panel2Collapsed = True
            m_DataGridView.Columns(5).AutoSizeMode = _
                DataGridViewAutoSizeColumnMode.Fill
        End If
    End Sub
End Class
