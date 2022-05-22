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
Imports System.Collections.ObjectModel

Public Interface IXmlRepository(Of T)
    ''' <summary>
    ''' Lists items in the repository.
    ''' </summary>
    ''' <returns>Collection object</returns>
    Function ListItems() As Collection(Of String)

    ''' <summary>
    ''' Does item exist in the repository?
    ''' </summary>
    ''' <param name="item">Item name</param>
    ''' <returns>True or False</returns>
    Function ItemExists(ByVal item As String) As Boolean

    ''' <summary>
    ''' Creates an item in the repository.
    ''' </summary>
    ''' <param name="item">Item name</param>
    ''' <param name="model">Object of type</param>
    Sub CreateItem(Of T As New)(ByVal item As String, ByVal model As T)

    ''' <summary>
    ''' Reads an item from the repository. 
    ''' </summary>
    ''' <param name="item">Item name</param>
    ''' <returns>Item contents object of type</returns>
    Function ReadItem(ByVal item As String) As T

    ''' <summary>
    ''' Updates an item in the repository.
    ''' </summary>
    ''' <param name="newItem">New item name</param>
    ''' <param name="oldItem">Old item name</param>
    ''' <param name="model">Model object of type</param>
    Sub UpdateItem(Of T As New)(ByVal newItem As String, ByVal oldItem As String, ByVal model As T)

    ''' <summary>
    ''' Deletes an item from the repository to the Windows Recycle Bin.
    ''' </summary>
    ''' <param name="item">Item name</param>
    Sub DeleteItem(ByVal item As String)
End Interface
