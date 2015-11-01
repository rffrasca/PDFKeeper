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
	Shared Friend pdfRenameName As String
	
	''' <summary>
	''' Class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' Set the font to MS Sans Serif 8pt in XP or Segoe UI 9pt in Vista or
	''' later, set textbox text to "pdfRenameName", and select the file name
	''' text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub PdfFileRenameFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		textBoxFileName.Text = pdfRenameName
		textBoxFileName.Select
	End Sub
	
	''' <summary>
	''' Enable the OK button if the length of the file name string is greater
	''' than 0, not equal to "pdfRenameName", and does not contain invalid
	''' characters.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TextBoxFileNameTextChanged(sender As Object, e As EventArgs)
		errorProvider.Clear
		textBoxFileName.Text = textBoxFileName.Text.Trim
		If textBoxFileName.Text.Length > 0 Then
			If Not textBoxFileName.Text.ToUpper(CultureInfo.CurrentCulture) = _
					pdfRenameName.ToUpper(CultureInfo.CurrentCulture) Then
				If StringUtil.ContainsInvalidFileNameChars( _
						textBoxFileName.Text) Then
					errorProvider.SetError(textBoxFileName, _
						PdfKeeper.Strings.FileNameContainsInvalidChars)
				Else
					buttonOK.Enabled = True
				End If
			Else
				buttonOK.Enabled = False
			End If
		Else
			buttonOK.Enabled = False
		End If
	End Sub
	
	''' <summary>
	''' Set "pdfRenameName" to the textbox text and close dialog. 
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOKClick(sender As Object, e As EventArgs)
		pdfRenameName = textBoxFileName.Text
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
End Class
