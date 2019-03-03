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
Public Class SearchResultsSortParameters
    Private m_SortedColumn As DataGridViewColumn
    Private m_SortOrder As SortOrder
    Private m_SortColumnIndex As Integer = 1
    Private m_SortDirection As ListSortDirection = ListSortDirection.Ascending

    Public Property SortedColumn As DataGridViewColumn
        Get
            Return m_SortedColumn
        End Get
        Set(value As DataGridViewColumn)
            If value Is Nothing Then
                Throw New ArgumentNullException("value")
            End If
            m_SortedColumn = value
            m_SortColumnIndex = value.Index
        End Set
    End Property

    Public Property SortOrder As SortOrder
        Get
            Return m_SortOrder
        End Get
        Set(value As SortOrder)
            If value = Windows.Forms.SortOrder.Ascending Then
                m_SortDirection = ListSortDirection.Ascending
            ElseIf value = Windows.Forms.SortOrder.Descending Then
                m_SortDirection = ListSortDirection.Descending
            End If
            m_SortOrder = value
        End Set
    End Property

    Public ReadOnly Property SortColumnIndex As Integer
        Get
            Return m_SortColumnIndex
        End Get
    End Property

    Public ReadOnly Property SortDirection As ListSortDirection
        Get
            Return m_SortDirection
        End Get
    End Property
End Class
