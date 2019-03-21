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
Imports System.Windows.Forms

Public Class SetClearCategoryDialog
    Implements ISetClearCategoryView
    Private presenter As SetClearCategoryViewPresenter

    Public Sub New()
        InitializeComponent()
        presenter = New SetClearCategoryViewPresenter(Me)
        HelpProvider.HelpNamespace = HelpProviderHelper.HelpFile
    End Sub

#Region "View Members"
    Public Property Categories As DataTable Implements ISetClearCategoryView.Categories
        Get
            Return CategoryComboBox.DataSource
        End Get
        Set(value As DataTable)
            CategoryComboBox.DataSource = value
            CategoryComboBox.DisplayMember = "doc_category"
        End Set
    End Property

    Public Property Category As String Implements ISetClearCategoryView.Category
        Get
            Return CategoryComboBox.Text
        End Get
        Set(value As String)
            CategoryComboBox.Text = value
        End Set
    End Property
#End Region
    
    Private Sub CategoryComboBox_Enter(sender As Object, e As EventArgs) Handles CategoryComboBox.Enter
        Me.Cursor = Cursors.WaitCursor
        presenter.CategoryComboBoxEnter()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CategoryComboBox_TextChanged(sender As Object, e As EventArgs) Handles CategoryComboBox.TextChanged
        presenter.CategoryComboBoxTextChanged()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
