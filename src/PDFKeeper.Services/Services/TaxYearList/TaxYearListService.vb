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
Imports PDFKeeper.Domain
Imports PDFKeeper.Infrastructure

Public Class TaxYearListService
    Implements ITaxYearListService
    Private ReadOnly repository As IDocumentRepository

    Public Sub New()
        Me.repository = Nothing
    End Sub

    Public Sub New(ByVal repository As IDocumentRepository)
        Me.repository = repository
    End Sub

    Public Function ListTaxYears() As DataTable Implements ITaxYearListService.ListTaxYears
        If repository Is Nothing Then
            Throw New InvalidOperationException
        End If
        Return repository.ListTaxYears
    End Function

    Public Function ListTaxYears(filter As TaxYearFilterModel) As DataTable Implements ITaxYearListService.ListTaxYears
        If repository Is Nothing Then
            Throw New InvalidOperationException
        End If
        Return repository.ListTaxYears(filter)
    End Function

    Public Function ListRangeOfTaxYears() As DataTable Implements ITaxYearListService.ListRangeOfTaxYears
        Dim taxYearTable = New DataTable("TaxYears")
        Dim yearColumn = taxYearTable.Columns.Add("Year", GetType(String))
        yearColumn.AllowDBNull = False
        yearColumn.Unique = True
        Dim emptyRow = taxYearTable.NewRow
        emptyRow("Year") = String.Empty
        taxYearTable.Rows.Add(emptyRow)
        Dim taxYearTempList = New List(Of String)
        Dim lastYear = DateTime.Now.Year
        Dim j = lastYear - 10
        Do While j <= lastYear
            j += 1
            taxYearTempList.Add(j)
        Loop
        taxYearTempList.Reverse()
        For Each item In taxYearTempList
            Dim row = taxYearTable.NewRow
            row("Year") = item
            taxYearTable.Rows.Add(row)
        Next
        Return taxYearTable
    End Function
End Class
