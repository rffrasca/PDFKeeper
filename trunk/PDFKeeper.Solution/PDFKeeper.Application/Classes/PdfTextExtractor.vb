'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
'* Copyright (C) 2009-2015 Robert F. Frasca
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

Public Class PdfTextExtractor
	Private m_pdfPathName As String
	
	''' <summary>
	''' Text extracted from PdfPathName.
	''' </summary>
	ReadOnly Property Text As String
		Get
			Return GetText
		End Get
	End Property
	
	''' <summary>
	''' PdfTextExtractor constructor.
	''' </summary>
	''' <param name="pdfPathName"></param>
	Public Sub New(ByVal pdfPathName As String)
		m_pdfPathName = pdfPathName
	End Sub
	
	''' <summary>
	''' Extract Text from PdfPathName.
	''' </summary>
	''' <returns>Text extracted from PdfPathName</returns>
	Private Function GetText As String
		Using reader = New PdfReader(m_PdfPathName)
			Dim text As New StringBuilder()
			For i As Integer = 1 To reader.NumberOfPages
				Dim strategy As ITextExtractionStrategy = New _
					iTextSharp.text.pdf.parser.LocationTextExtractionStrategy()
				Dim currentPage As String = _
					iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage( _
					reader, i, strategy)
				Dim lines As String() = currentPage.Split(ControlChars.Lf)
				For Each line As String In lines
					text.AppendLine(line)
				Next
			Next
			Return text.ToString()
		End Using
	End Function
End Class
