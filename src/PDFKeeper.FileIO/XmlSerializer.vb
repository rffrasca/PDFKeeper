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
Imports System.Xml

Public NotInheritable Class XmlSerializer
    ''' <summary>
    ''' Serializes an object to an XML file.
    ''' </summary>
    ''' <typeparam name="T">Object type</typeparam>
    ''' <param name="obj">Object name</param>
    ''' <param name="xmlFile">XML file name</param>
    Public Shared Sub Serialize(Of T As New)(obj As T, xmlFile As String)
        Dim settings = New XmlWriterSettings With {.NewLineHandling = NewLineHandling.Entitize}
        Using writer = XmlWriter.Create(xmlFile, settings)
            Dim serializer = New Serialization.XmlSerializer(GetType(T))
            serializer.Serialize(writer, obj)
        End Using
    End Sub

    ''' <summary>
    ''' Deserializes an XML file into an object.
    ''' </summary>
    ''' <typeparam name="T">Object type</typeparam>
    ''' <param name="xmlFile">XML file name</param>
    ''' <returns>Object of specified type</returns>
    Public Shared Function Deserialize(Of T)(xmlFile As String) As T
        Dim settings = New XmlReaderSettings With {.DtdProcessing = DtdProcessing.Prohibit}
        Using reader = XmlReader.Create(xmlFile, settings)
            Dim serializer = New Serialization.XmlSerializer(GetType(T))
            Return CType(serializer.Deserialize(reader), T)
        End Using
    End Function
End Class
