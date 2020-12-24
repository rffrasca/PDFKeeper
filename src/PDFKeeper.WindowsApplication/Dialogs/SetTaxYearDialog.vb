'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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

Public Class SetTaxYearDialog
    Implements ICommonView
    Private ReadOnly presenter As CommonPresenter
    Private ReadOnly help As IHelpDisplayService = New HelpDisplayService

    Public Sub New()
        InitializeComponent()
        presenter = New CommonPresenter(Me)
        HelpProvider.HelpNamespace = help.Name
        TaxYearComboBox.Select()
    End Sub

#Region "Interface Members"
    Public Property TaxYears As Object Implements ICommonView.TaxYears
        Get
            Return TaxYearComboBox.Items
        End Get
        Set(value As Object)
            TaxYearComboBox.Items.Clear()
            TaxYearComboBox.Items.AddRange(value)
        End Set
    End Property

    Public Property TaxYear As String Implements ICommonView.TaxYear
        Get
            Return TaxYearComboBox.Text
        End Get
        Set(value As String)
            TaxYearComboBox.Text = value
        End Set
    End Property

    Public ReadOnly Property ActiveElement As String Implements ICommonView.ActiveElement
        Get
            Return Me.ActiveControl.Name
        End Get
    End Property

    Public Sub SetCursor(wait As Boolean) Implements ICommonView.SetCursor
        If wait Then
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub
#End Region

#Region "Unused Interface Members"
    Public Property AuthorsGroup As DataTable Implements ICommonView.AuthorsGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AuthorGroup As String Implements ICommonView.AuthorGroup
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SubjectsGroup As DataTable Implements ICommonView.SubjectsGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SubjectGroup As String Implements ICommonView.SubjectGroup
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CategoriesGroup As DataTable Implements ICommonView.CategoriesGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CategoryGroup As String Implements ICommonView.CategoryGroup
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property TaxYearsGroup As DataTable Implements ICommonView.TaxYearsGroup
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property TaxYearGroup As String Implements ICommonView.TaxYearGroup
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AuthorsPaired As DataTable Implements ICommonView.AuthorsPaired
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AuthorPaired As String Implements ICommonView.AuthorPaired
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SubjectsPaired As DataTable Implements ICommonView.SubjectsPaired
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property SubjectPaired As String Implements ICommonView.SubjectPaired
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Categories As DataTable Implements ICommonView.Categories
        Get
            Return Nothing
        End Get
        Set(value As DataTable)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Category As String Implements ICommonView.Category
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property
#End Region

    Private Sub TaxYearComboBox_Enter(sender As Object, e As EventArgs) Handles TaxYearComboBox.Enter
        presenter.FillTaxYears()
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
