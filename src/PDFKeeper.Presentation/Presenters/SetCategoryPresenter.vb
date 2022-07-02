'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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
Imports PDFKeeper.Common
Imports PDFKeeper.Infrastructure
Imports PDFKeeper.Services

Public Class SetCategoryPresenter
    Private ReadOnly view As ISetCategoryView
    Private ReadOnly categoryListSvc As ICategoryListService
    Private ReadOnly commonDialogs As New CommonDialogs
    Private viewInstance As Form

    Public Sub New(ByVal view As ISetCategoryView, ByVal categoryListSvc As ICategoryListService)
        Me.view = view
        Me.categoryListSvc = categoryListSvc
    End Sub

    Friend Sub SetCategoryDialog_Load(sender As Object, e As EventArgs)
        viewInstance = CType(sender, Form)
    End Sub

    Friend Sub CategoryComboBox_Enter(sender As Object, e As EventArgs)
        Try
            viewInstance.Cursor = Cursors.WaitCursor
            Dim currentItem = view.Category
            view.Categories = categoryListSvc.ListCategories
            view.Category = currentItem
        Catch ex As DbException
            commonDialogs.ShowMessageBox(ex.Message, True)
        Finally
            viewInstance.Cursor = Cursors.Default
        End Try
    End Sub

    Friend Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        viewInstance.DialogResult = DialogResult.OK
    End Sub

    Friend Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        viewInstance.DialogResult = DialogResult.Cancel
    End Sub
End Class
