'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public Class GenericList(Of T)
    Private m_List As New List(Of T)

    ''' <summary>
    ''' Returns a count of items in the List.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Count As Integer
        Get
            Return m_List.ToArray.Count
        End Get
    End Property

    ''' <summary>
    ''' Adds the specified item to the List.
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Public Sub Add(ByVal item As T)
        If Not m_List.Contains(item) Then
            m_List.Add(item)
        End If
    End Sub

    ''' <summary>
    ''' Clears the List.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        m_List.Clear()
    End Sub

    ''' <summary>
    ''' Returns an object containing all of the items in the List as an array.
    ''' </summary>
    ''' <param name="sorted">True or False</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToArray(ByVal sorted As Boolean) As Object
        If sorted Then
            m_List.Sort()
        End If
        Return m_List.ToArray
    End Function
End Class
