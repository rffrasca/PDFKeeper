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

Public Class SetCategoryDialog
    Implements ISetCategoryView
    Private ReadOnly presenter As SetCategoryPresenter
    Private ReadOnly help As New HelpProvider

    Public Sub New()
        InitializeComponent()
        presenter = New SetCategoryPresenter(Me, New CategoryListService(DocumentRepositoryFactory.Repository))
        HelpProvider.HelpNamespace = help.HelpFileName
        AddHandlers()
    End Sub

    Public Property Category As String Implements ISetCategoryView.Category
        Get
            Return CategoryComboBox.Text
        End Get
        Set(value As String)
            CategoryComboBox.Text = value
        End Set
    End Property

    Public Property Categories As DataTable Implements ISetCategoryView.Categories
        Get
            Return CategoryComboBox.DataSource
        End Get
        Set(value As DataTable)
            CategoryComboBox.DataSource = value
            CategoryComboBox.DisplayMember = CategoryComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Private Sub AddHandlers()
        AddHandler MyBase.Load, AddressOf presenter.SetCategoryDialog_Load
        AddHandler CategoryComboBox.Enter, AddressOf presenter.CategoryComboBox_Enter
        AddHandler OK_Button.Click, AddressOf presenter.OK_Button_Click
        AddHandler Cancel_Button.Click, AddressOf presenter.Cancel_Button_Click
    End Sub
End Class
