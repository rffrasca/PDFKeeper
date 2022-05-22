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
Imports PDFKeeper.Services

Public Class SetTaxYearDialog
    Implements ISetTaxYearView
    Private ReadOnly presenter As SetTaxYearPresenter
    Private ReadOnly help As New HelpProvider

    Public Sub New()
        InitializeComponent()
        presenter = New SetTaxYearPresenter(Me, New TaxYearListService)
        HelpProvider.HelpNamespace = help.HelpFileName
        AddHandlers()
    End Sub

    Public ReadOnly Property TaxYear As String
        Get
            Return TaxYearComboBox.Text
        End Get
    End Property

    Public Property TaxYears As DataTable Implements ISetTaxYearView.TaxYears
        Get
            Return TaxYearComboBox.DataSource
        End Get
        Set(value As DataTable)
            TaxYearComboBox.DataSource = value
            TaxYearComboBox.DisplayMember = TaxYearComboBox.DataSource.Columns.Item(0).ToString
        End Set
    End Property

    Private Sub AddHandlers()
        AddHandler MyBase.Load, AddressOf presenter.SetTaxYearDialog_Load
        AddHandler TaxYearComboBox.Enter, AddressOf presenter.TaxYearComboBox_Enter
        AddHandler OK_Button.Click, AddressOf presenter.OK_Button_Click
        AddHandler Cancel_Button.Click, AddressOf presenter.Cancel_Button_Click
    End Sub
End Class
