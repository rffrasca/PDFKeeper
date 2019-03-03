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
Public Class ViewToolBarToolStripCommand
    Implements ICommand
    Private m_ToolStrip As Control
    Private m_MenuItem As ToolStripMenuItem

    Public Sub New(ByVal toolStrip As Control, _
                   ByVal menuItem As ToolStripMenuItem)
        m_ToolStrip = toolStrip
        m_MenuItem = menuItem
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        If m_MenuItem.Checked Then
            m_MenuItem.Checked = False
        Else
            m_MenuItem.Checked = True
        End If
        m_ToolStrip.Visible = m_MenuItem.Checked
    End Sub
End Class
