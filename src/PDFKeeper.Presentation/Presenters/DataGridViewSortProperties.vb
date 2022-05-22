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
Public Class DataGridViewSortProperties
    Private _SortedColumn As DataGridViewColumn
    Private _SortOrder As SortOrder
    Private _SortColumnIndex As Integer = 1
    Private _SortDirection As ListSortDirection = ListSortDirection.Ascending

    Public Property SortedColumn As DataGridViewColumn
        Get
            Return _SortedColumn
        End Get
        Set(value As DataGridViewColumn)
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            _SortedColumn = value
            _SortColumnIndex = value.Index
        End Set
    End Property

    Public Property SortOrder As SortOrder
        Get
            Return _SortOrder
        End Get
        Set(value As SortOrder)
            If value = SortOrder.Ascending Then
                _SortDirection = ListSortDirection.Ascending
            ElseIf value = SortOrder.Descending Then
                _SortDirection = ListSortDirection.Descending
            End If
            _SortOrder = value
        End Set
    End Property

    Public ReadOnly Property SortColumnIndex As Integer
        Get
            Return _SortColumnIndex
        End Get
    End Property

    Public ReadOnly Property SortDirection As ListSortDirection
        Get
            Return _SortDirection
        End Get
    End Property
End Class
