'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
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
Public Class GenericDictionaryList(Of TKey, TValue)
    Private m_List As New Dictionary(Of TKey, TValue)

    ''' <summary>
    ''' Sets the specified key and value in the Dictionary list.
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetItem(ByVal key As TKey, ByVal value As TValue)
        m_List.Item(key) = value
    End Sub

    ''' <summary>
    ''' Returns an object containing all of the keys from the Dictionary list.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Keys As Object
        Get
            Return m_List.Keys
        End Get
    End Property

    ''' <summary>
    ''' Returns the value of the specified key from the Dictionary list.
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItem(ByVal key As TKey) As TValue
        If m_List.ContainsKey(key) = False Then
            Return Nothing
        End If
        Return m_List.Item(key)
    End Function
End Class
