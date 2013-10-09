'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
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

Public Class PrintPages
	Dim textToPrint As String
	Dim textFont As System.Drawing.Font
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg1: textToPrint"></param>
	''' <param name="arg2: textFont"></param>
	Public Sub New(ByVal arg1 As String, ByVal arg2 As System.Drawing.Font)
		textToPrint = arg1
		textFont = arg2
	End Sub
	
	''' <summary>
	''' This subroutine will generate print page's that contain the contents
	''' of the object string using the object font.
	''' </summary>
	''' <param name="printArgs"></param>
	Public Sub FromString(ByVal printArgs As Object)
		Dim numChars As Integer
		Dim numLines As Integer
		Dim textForPage As String
		Dim objStringFormat As New StringFormat()
		Dim printFont As System.Drawing.Font = textFont
		Dim rectDraw As New RectangleF(printArgs.MarginBounds.Left, _
									   printArgs.MarginBounds.Top, _
									   printArgs.MarginBounds.Width, _
									   printArgs.MarginBounds.Height)
		Dim sizeMeasure As New SizeF(printArgs.MarginBounds.Width, _
									 printArgs.MarginBounds.Height - _
									 printFont.GetHeight(printArgs.Graphics))
		objStringFormat.Trimming = StringTrimming.Word
		printArgs.Graphics.MeasureString(textToPrint, printFont, sizeMeasure, _
							   objStringFormat, numChars, numLines)
		textForPage = textToPrint.Substring(0, numChars)
		printArgs.Graphics.DrawString(textForPage, printFont, Brushes.Black, _
							  		  rectDraw, objStringFormat)
		If numChars < textToPrint.Length Then
			textForPage = textToPrint.Substring(numChars)
			printArgs.HasMorePages = True
		Else
			printArgs.HasMorePages = False
		End If
	End Sub
End Class
