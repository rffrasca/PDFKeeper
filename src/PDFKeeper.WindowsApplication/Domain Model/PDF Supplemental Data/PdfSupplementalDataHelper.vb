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
Public Class PdfSupplementalDataHelper
    Private serializer As XmlFileSerializer
    Private m_PdfPath As String
    Private suppDataXmlPath As String
    Private suppData As New PdfSupplementalData

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="pdfPath">
    ''' Path name of PDF to associate supplemental data with.
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal pdfPath As String)
        m_PdfPath = pdfPath
        suppDataXmlPath = Path.ChangeExtension(m_PdfPath, "xml")
        serializer = New XmlFileSerializer(suppDataXmlPath)
    End Sub

    ''' <summary>
    ''' Reads the XML file with the same name as the PDF file object into a
    ''' PdfSupplementalData object.
    ''' </summary>
    ''' <returns>
    ''' PdfFileSupplementalDataItem object containing the supplemental data or
    ''' Nothing if the XML file does not exist.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function Read() As PdfSupplementalData
        If IO.File.Exists(suppDataXmlPath) Then
            suppData = serializer.Deserialize(Of PdfSupplementalData)()
        End If
        Return suppData
    End Function

    ''' <summary>
    ''' Writes the specified notes, category, and flag state to an XML file
    ''' with the same name and in the same folder as the PDF file object.
    ''' </summary>
    ''' <param name="notes"></param>
    ''' <param name="category"></param>
    ''' <param name="flagState">0 or 1, where 1 = flagged</param>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", _
        "CA1726:UsePreferredTerms", MessageId:="flag")> _
    Public Sub Write(ByVal notes As String, _
                     ByVal category As String, _
                     ByVal flagState As Integer)
        With suppData
            .Notes = notes
            .Category = category
            .FlagState = flagState
        End With
        serializer.Serialize(suppData)
    End Sub
End Class
