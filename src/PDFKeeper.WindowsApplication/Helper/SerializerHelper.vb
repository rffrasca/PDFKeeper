'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
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
Public NotInheritable Class SerializerHelper
    Private Sub New()
        ' Required by Code Analysis.
    End Sub

    Public Shared Sub FromObjToXml(Of T As New)(ByVal objName As T, _
                                                ByVal xmlFile As String)
        Using writer As New StreamWriter(xmlFile)
            Dim serializer As New XmlSerializer(GetType(T))
            serializer.Serialize(writer, objName)
        End Using
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1045:DoNotPassTypesByReference", _
        MessageId:="0#")> _
    Public Shared Sub FromXmlToObj(Of T)(ByRef objName As T, _
                                         ByVal xmlFile As String)
        Using reader As New StreamReader(xmlFile)
            Dim serializer As New XmlSerializer(objName.GetType)
            objName = CType(serializer.Deserialize(reader), T)
        End Using
    End Sub
End Class
