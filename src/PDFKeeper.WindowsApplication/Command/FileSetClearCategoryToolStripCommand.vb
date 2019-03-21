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
Public Class FileSetClearCategoryToolStripCommand
    Implements ICommand
    Private m_parentForm As Control
    Private m_presenter As MainViewSearchPresenter
    Private Shared s_Category As String

    Public Sub New(ByVal parentForm As Control, _
                   ByVal presenter As MainViewSearchPresenter)
        m_parentForm = parentForm
        m_presenter = presenter
    End Sub

    Public Shared ReadOnly Property Category As String
        Get
            Return s_Category
        End Get
    End Property

    Public Sub Execute() Implements ICommand.Execute
        Using categoryDialog As New SetClearCategoryDialog
            If categoryDialog.ShowDialog = DialogResult.OK Then
                s_Category = categoryDialog.Category
                m_parentForm.Cursor = Cursors.WaitCursor
                m_presenter.FileSetClearCategoryCommandExecute()
                m_parentForm.Cursor = Cursors.Default
            End If
        End Using
    End Sub
End Class
