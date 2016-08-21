'******************************************************************************
'*
'* PDFKeeper -- Capture, Upload, and Search for PDF Documents
'* Copyright (C) 2009-2016 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify it under
'* the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public NotInheritable Class Serializer
	Private Sub New()
		' Because type 'SerializerUtil' contains only 'Shared' members, a
		' default private constructor was added to prevent the compiler from
		' adding a default public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Serializes the serializable members and properties of an object to
	''' an XML file.
	''' </summary>
	''' <param name="objectName">Name of object to serialize.</param>
	''' <param name="xmlFile">Path name of XML file to write.</param>
	Public Shared Sub SerializeToXml(Of T As New)( _
		ByVal objectName As T, _
		ByVal xmlFile As String)
		Dim writer As New StreamWriter(xmlFile)
		Dim serializer As New XmlSerializer(GetType(T))
		serializer.Serialize(writer, objectName)
		writer.Close
	End Sub
	
	''' <summary>
	''' Deserializes the data values from an XML file to matching members and
	''' properties of an object.
	''' </summary>
	''' <param name="objectName">Name of object to deserialize.</param>
	''' <param name="xmlFile">Path name of XML file to read.</param>
	Public Shared Sub DeserializeFromXml(Of T)( _
		ByVal objectName As T, _
		ByVal xmlFile As String)
		Try
			Dim reader As New StreamReader(xmlFile)
			Dim serializer As New XmlSerializer(objectName.GetType)
			objectName = CType(serializer.Deserialize(reader), T)
			reader.Close
		Catch ex As FileNotFoundException
		End Try
	End Sub
End Class
