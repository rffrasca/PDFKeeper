'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Public Class XmlFileSerializer
    Private m_XmlFilePath As String

    Public Sub New(ByVal xmlFilePath As String)
        m_XmlFilePath = xmlFilePath
    End Sub

    ''' <summary>
    ''' Serializes the specified object to the XML file object.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="objName"></param>
    ''' <remarks></remarks>
    Public Sub Serialize(Of T As New)(ByVal objName As T)
        Dim writerSettings As New XmlWriterSettings
        writerSettings.NewLineHandling = NewLineHandling.Entitize
        Using writer As XmlWriter = XmlWriter.Create(m_XmlFilePath, _
                                                     writerSettings)
            Dim serializer As New XmlSerializer(GetType(T))
            serializer.Serialize(writer, objName)
        End Using
    End Sub

    ''' <summary>
    ''' Deserializes and returns the XML file object as an object of the
    ''' specified type.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Deserialize(Of T)() As T
        Using reader As XmlReader = XmlReader.Create(m_XmlFilePath)
            Dim serializer As New XmlSerializer(GetType(T))
            Return CType(serializer.Deserialize(reader), T)
        End Using
    End Function
End Class
