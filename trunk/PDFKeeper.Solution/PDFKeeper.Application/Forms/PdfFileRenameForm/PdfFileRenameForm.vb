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

Public Partial Class PdfFileRenameForm
	
	''' <summary>
	''' Class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' Set the font to MS Sans Serif 8pt in XP or Segoe UI 9pt in Vista or
	''' later and select the file name text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub PdfFileRenameFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		textBoxFileName.Select
	End Sub
	
	''' <summary>
	''' Enable the OK button if the length of the file name string is greater
	''' than 0.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TextBoxFileNameTextChanged(sender As Object, e As EventArgs)
		textBoxFileName.Text = textBoxFileName.Text.Trim
		If textBoxFileName.Text.Length > 0 Then		
			buttonOK.Enabled = True
		Else
			buttonOK.Enabled = False
		End If
	End Sub
End Class
