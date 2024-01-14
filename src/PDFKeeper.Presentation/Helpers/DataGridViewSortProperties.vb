' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

Public Class DataGridViewSortProperties
    Private _sortedColumn As DataGridViewColumn
    Private _sortOrder As SortOrder
    Private _sortColumnIndex As Integer = 1
    Private _sortDirection As ListSortDirection = ListSortDirection.Ascending

    Public Property SortedColumn As DataGridViewColumn
        Get
            Return _sortedColumn
        End Get
        Set(value As DataGridViewColumn)
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            _sortedColumn = value
            _sortColumnIndex = value.Index
        End Set
    End Property

    Public Property SortOrder As SortOrder
        Get
            Return _sortOrder
        End Get
        Set(value As SortOrder)
            If value.Equals(SortOrder.Ascending) Then
                _sortDirection = ListSortDirection.Ascending
            ElseIf value.Equals(SortOrder.Descending) Then
                _sortDirection = ListSortDirection.Descending
            End If
            _sortOrder = value
        End Set
    End Property

    Public ReadOnly Property SortColumnIndex As Integer
        Get
            Return _sortColumnIndex
        End Get
    End Property

    Public ReadOnly Property SortDirection As ListSortDirection
        Get
            Return _sortDirection
        End Get
    End Property
End Class
