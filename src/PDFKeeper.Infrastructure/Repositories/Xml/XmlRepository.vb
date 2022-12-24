'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
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
Imports System.IO
Imports PDFKeeper.Common
Imports PDFKeeper.FileIO

Public Class XmlRepository(Of T)
    Implements IXmlRepository(Of T)
    Private ReadOnly repositoryPath As String

    Public Sub New(ByVal repositoryPath As String)
        Me.repositoryPath = repositoryPath
    End Sub

    Public Function ListItems() As Collection(Of String) Implements IXmlRepository(Of T).ListItems
        Dim items = New Collection(Of String)
        For Each xmlFile In Directory.GetFiles(repositoryPath, "*.xml", SearchOption.TopDirectoryOnly)
            items.Add(Path.GetFileNameWithoutExtension(xmlFile))
        Next
        Return items
    End Function

    Public Function ItemExists(item As String) As Boolean Implements IXmlRepository(Of T).ItemExists
        Return File.Exists(GetXmlPath(item))
    End Function

    Public Sub CreateItem(Of T As New)(item As String, model As T) Implements IXmlRepository(Of T).CreateItem
        XmlSerializer.Serialize(model, GetXmlPath(item))
    End Sub

    Public Function ReadItem(item As String) As T Implements IXmlRepository(Of T).ReadItem
        Return XmlSerializer.Deserialize(Of T)(GetXmlPath(item))
    End Function

    Public Sub UpdateItem(Of T As New)(newItem As String, oldItem As String, model As T) Implements IXmlRepository(Of T).UpdateItem
        File.Move(GetXmlPath(oldItem), GetXmlPath(newItem))
        XmlSerializer.Serialize(model, GetXmlPath(newItem))
    End Sub

    Public Sub DeleteItem(item As String) Implements IXmlRepository(Of T).DeleteItem
        Dim xmlFile = New FileInfo(GetXmlPath(item))
        xmlFile.DeleteToRecycleBin()
    End Sub

    Private Function GetXmlPath(ByVal item As String) As String
        Return Path.Combine(repositoryPath, String.Concat(item, ".xml"))
    End Function
End Class
