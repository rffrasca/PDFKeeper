'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
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

Public Module MessageBoxWrapper
	''' <summary>
	''' Displays the specified message in an Error Message Box.
	''' </summary>
	''' <param name="value">Message to display.</param>
	Public Sub MessageBoxError(ByVal value As String)
		MessageBox.Show(value, _
			PdfKeeper.Strings.MessageBoxTitleError, _
			MessageBoxButtons.OK, _
			MessageBoxIcon.Error, _
			MessageBoxDefaultButton.Button1, _
			0, _
			False)
	End Sub
	
	''' <summary>
	''' Displays the specified message in an Information Message Box.
	''' </summary>
	''' <param name="value">Message to display.</param>
	Public Sub MessageBoxInformation(ByVal value As String)
		MessageBox.Show(value, _
			PdfKeeper.Strings.MessageBoxTitleInformation, _
			MessageBoxButtons.OK, _
			MessageBoxIcon.Information, _
			MessageBoxDefaultButton.Button1, _
			0, _
			False)
	End Sub
		
	''' <summary>
	''' Displays the specified message in a Question Message Box.
	''' </summary>
	''' <param name="value">Message to display.</param>
	''' <returns>6 for Yes or 7 for No.</returns>
	Public Function MessageBoxQuestion(ByVal value As String) As Integer
		Dim button = MessageBox.Show(value, _
			PdfKeeper.Strings.MessageBoxTitleQuestion, _
			MessageBoxButtons.YesNo, _
			MessageBoxIcon.Question, _
			MessageBoxDefaultButton.Button1, _
			0, _
			False)
		Return button
	End Function
End Module
