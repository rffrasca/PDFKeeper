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

Public Partial Class PdfOwnerPasswordForm
	Friend Shared ownerPassword As New SecureString
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will set the font to MS Sans Serif 8pt in XP or
	''' Segoe UI 9pt in Vista or later, clear the password string, and select
	''' the Password text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub PdfOwnerPasswordFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		ownerPassword.Clear
		textBoxPassword.Select
	End Sub
	
	''' <summary>
	''' This subroutine will enable the OK button if the length of the password
	''' string is greater than 0.
	''' </summary>
	Private Sub TextBoxTextChanged
		If ownerPassword.Length > 0 Then		
			buttonOK.Enabled = True
		Else
			buttonOK.Enabled = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will handle the deleting of selected characters from
	''' the password string, ignoring the Esc and Enter keys.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TextBoxPasswordKeyDown(sender As Object, e As KeyEventArgs)
		If e.KeyCode = Keys.Delete Then
			If textBoxPassword.SelectionLength > 0 Then
				RemoveCharacters()
			ElseIf textBoxPassword.SelectionStart < _
					textBoxPassword.Text.Length Then
				ownerPassword.RemoveAt(textBoxPassword.SelectionStart)
			End If
			SetTextBoxTextProperty(textBoxPassword.SelectionStart)
			e.Handled = True
		ElseIf e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Enter Then
			e.Handled = True
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will update the password string by removing the
	''' printable character that was removed by pressing the Backspace key or
	''' adding the printable character entered.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TextBoxPasswordKeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = ControlChars.Back Then
			If textBoxPassword.SelectionLength > 0 Then
				RemoveCharacters()
				SetTextBoxTextProperty(textBoxPassword.SelectionStart)
			ElseIf textBoxPassword.SelectionStart > 0 Then
				ownerPassword.RemoveAt(textBoxPassword.SelectionStart - 1)
				SetTextBoxTextProperty(textBoxPassword.SelectionStart - 1)
			End If
		Else
			If textBoxPassword.SelectionLength > 0 Then
				RemoveCharacters()
			End If
			ownerPassword.InsertAt(textBoxPassword.SelectionStart, e.KeyChar)
			SetTextBoxTextProperty(textBoxPassword.SelectionStart + 1)
		End If
		e.Handled = True
	End Sub
	
	''' <summary>
	''' This subroutine will remove selected characters from the password
	''' string.
	''' </summary>
	Private Sub RemoveCharacters
		For i As Integer = 0 To textBoxPassword.SelectionLength - 1
			ownerPassword.RemoveAt(textBoxPassword.SelectionStart)
		Next
	End Sub
	
	''' <summary>
	''' This subroutine will set the text property of the password text box to
	''' the number of password characters matching the length of the password
	''' string.
	''' </summary>
	''' <param name="caretPos"></param>
	Private Sub SetTextBoxTextProperty(ByVal caretPos As Integer)
		textBoxPassword.Text = New String(CChar("*"), ownerPassword.Length)
		textBoxPassword.SelectionStart = caretPos
	End Sub

	''' <summary>
	''' This subroutine will clear the text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOkClick(sender As Object, e As EventArgs)
		textBoxPassword.Text = Nothing
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
	
	''' <summary>
	''' This subroutine will clear the password string and text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonCancelClick(sender As Object, e As EventArgs)
		ownerPassword.Clear
		textBoxPassword.Text = Nothing
	End Sub
End Class
