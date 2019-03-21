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
Public Class SetClearCategoryViewPresenter
    Private view As ISetClearCategoryView

    Public Sub New(view As ISetClearCategoryView)
        Me.view = view
    End Sub

    Public Sub CategoryComboBoxEnter()
        Dim currentCategory As String = view.Category
        Dim docsDao As IDocsDao = New DocsDao
        view.Categories = docsDao.GetAllCategories
        view.Category = currentCategory
    End Sub

    Public Sub CategoryComboBoxTextChanged()
        view.Category = view.Category.TrimStart
    End Sub
End Class
