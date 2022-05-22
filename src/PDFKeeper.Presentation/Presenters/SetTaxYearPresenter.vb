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
Imports PDFKeeper.Services

Public Class SetTaxYearPresenter
    Private ReadOnly view As ISetTaxYearView
    Private ReadOnly taxYearListSvc As ITaxYearListService
    Private viewInstance As Form

    Public Sub New(ByVal view As ISetTaxYearView, ByVal taxYearListSvc As ITaxYearListService)
        Me.view = view
        Me.taxYearListSvc = taxYearListSvc
    End Sub

    Friend Sub SetTaxYearDialog_Load(sender As Object, e As EventArgs)
        viewInstance = CType(sender, Form)
    End Sub

    Friend Sub TaxYearComboBox_Enter(sender As Object, e As EventArgs)
        view.TaxYears = taxYearListSvc.ListRangeOfTaxYears
    End Sub

    Friend Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        viewInstance.DialogResult = DialogResult.OK
    End Sub

    Friend Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        viewInstance.DialogResult = DialogResult.Cancel
    End Sub
End Class
