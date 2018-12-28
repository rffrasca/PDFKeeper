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
Public Class FileDeleteToolStripCommand
    Implements ICommand
    Private m_parentForm As Control
    Private m_presenter As MainViewSearchPresenter

    Public Sub New(ByVal parentForm As Control, _
                   ByVal presenter As MainViewSearchPresenter)
        m_parentForm = parentForm
        m_presenter = presenter
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        Dim displayService As IMessageDisplayService = New MessageDisplayService
        If displayService.ShowQuestion(My.Resources.DeleteSelectedDocuments, _
                                       False) = Windows.Forms.DialogResult.Yes Then
            m_parentForm.Cursor = Cursors.WaitCursor
            m_presenter.DeleteSelectedDocuments()
            m_parentForm.Cursor = Cursors.Default
        End If
    End Sub
End Class
