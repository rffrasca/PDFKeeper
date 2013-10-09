'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but
'* WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'*************************************************************************

Friend Class PrintPages
	
	''' <summary>
	''' Private constructor added to address CA1812.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This subroutine will generate print page's that contain "textToPrint".
	''' </summary>
	''' <param name="textToPrint"></param>
	''' <param name="textFont"></param>
	''' <param name="e"></param>
	Public Shared Sub FromString(ByVal textToPrint As String, _
								 ByVal textFont As System.Drawing.Font, _
								 ByRef e As Object)
		Dim numChars As Integer
		Dim numLines As Integer
		Dim textForPage As String
		Dim objStringFormat As New StringFormat()
		Dim printFont As System.Drawing.Font = textFont
		Dim rectDraw As New RectangleF(e.MarginBounds.Left, _
									   e.MarginBounds.Top, _
									   e.MarginBounds.Width, _
									   e.MarginBounds.Height)
		Dim sizeMeasure As New SizeF(e.MarginBounds.Width, _
									 e.MarginBounds.Height - _
									 printFont.GetHeight(e.Graphics))
		objStringFormat.Trimming = StringTrimming.Word
		e.Graphics.MeasureString(textToPrint, printFont, sizeMeasure, _
								 objStringFormat, numChars, numLines)
		textForPage = textToPrint.Substring(0, numChars)
		e.Graphics.DrawString(textForPage, printFont, Brushes.Black, _
							  rectDraw, objStringFormat)
		If numChars < textToPrint.Length Then
			textForPage = textToPrint.Substring(numChars)
			e.HasMorePages = True
		Else
			e.HasMorePages = False
		End If
	End Sub
End Class
