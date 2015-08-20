'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
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

Public NotInheritable Class Print
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This subroutine will generate print page's that contain the contents
	''' of "textToPrint" using "textFont".
	''' </summary>
	''' <param name="textToPrint"></param>
	''' <param name="textFont"></param>
	''' <param name="printArgs"></param>
	Public Shared Sub Text(ByVal textToPrint As String, _
			ByVal textFont As System.Drawing.Font, _
			ByVal printArgs As System.Drawing.Printing.PrintPageEventArgs)
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
