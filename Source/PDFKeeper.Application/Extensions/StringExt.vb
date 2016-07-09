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
'*
'* Created by SharpDevelop.
'* User: Robert
'* Date: 7/9/2016
'* Time: 5:47 PM
'*
'******************************************************************************

Public Module StringExt
	''' <summary>
	''' Checks if the specified value contains invalid file name characters.
	''' </summary>
	''' <param name="value">Folder or file path name.</param>
	''' <returns>True or False</returns>
	<ExtensionAttribute> _
	Public Function ContainsInvalidFileNameChars( _
		ByVal value As String) As Boolean
		
		For Each invalidChar In Path.GetInvalidFileNameChars()
			If value.Contains(invalidChar) Then
				Return True
			End If
		Next
		Return False
	End Function
	
	''' <summary>
	''' Prints the specified value using the specified font and print arguments
	''' from the PrintDocument component.
	''' </summary>
	''' <param name="value">Text to print.</param>
	''' <param name="font">Font to use.</param>
	''' <param name="printArgs">Data for PrintPage event.</param>
	<ExtensionAttribute> _
	Public Sub Print( _
		ByVal value As String, _
		ByVal font As System.Drawing.Font, _
		ByVal printArgs As System.Drawing.Printing.PrintPageEventArgs)
		
		Dim numChars As Integer
		Dim numLines As Integer
		Dim textForPage As String
		Dim strFormat As New StringFormat()
		Dim rectDraw As New RectangleF( _
			printArgs.MarginBounds.Left, _
			printArgs.MarginBounds.Top, _
			printArgs.MarginBounds.Width, _
			printArgs.MarginBounds.Height)
		Dim sizeMeasure As New SizeF( _
			printArgs.MarginBounds.Width, _
			printArgs.MarginBounds.Height - _
			font.GetHeight(printArgs.Graphics))
		strFormat.Trimming = StringTrimming.Word
		printArgs.Graphics.MeasureString( _
			value, _
			font, _
			sizeMeasure, _
			strFormat, _
			numChars, _
			numLines)
		textForPage = value.Substring(0, numChars)
		printArgs.Graphics.DrawString( _
			textForPage, _
			font, _
			Brushes.Black, _
			rectDraw, _
			strFormat)
		If numChars < value.Length Then
			textForPage = value.Substring(numChars)
			printArgs.HasMorePages = True
		Else
			printArgs.HasMorePages = False
		End If
	End Sub
End Module
