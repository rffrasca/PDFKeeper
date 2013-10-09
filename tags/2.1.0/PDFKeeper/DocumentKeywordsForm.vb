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

Public Partial Class DocumentKeywordsForm
	Dim keywords As String
		
	Public Sub New(ByVal keywordsArg As string)
		Me.InitializeComponent()
		keywords = keywordsArg
	End Sub

	''' <summary>
	''' This subroutine will load the document keywords into the text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DocumentKeywordsFormLoad(sender As Object, e As EventArgs)
		textBoxKeywords.Text = keywords
		
		' Prevent text from being highlighted.
		textBoxKeywords.SelectionStart = 0
	End Sub
End Class
