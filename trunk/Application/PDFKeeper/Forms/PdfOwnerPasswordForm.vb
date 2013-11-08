'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2013 Robert F. Frasca
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
	Friend Shared ownerPasswordSecure As SecureString

	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will set the font to MS Sans Serif 8pt in XP and
	''' Segoe UI 9pt in Vista or later; and select the Password text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub PdfOwnerPasswordFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		textBoxPassword.Select
	End Sub
	
	''' <summary>
	''' This subroutine will trim leading and trailing spaces from the text in
	''' the Password text box and will enable the OK button if the length of
	''' the Password is greater than 0.
	''' </summary>
	Private Sub TextBoxTextChanged
		textBoxPassword.Text = textBoxPassword.Text.Trim
		If textBoxPassword.TextLength > 0 Then
			buttonOK.Enabled = True
		Else
			buttonOK.Enabled = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will create a SecureString object of the text in the
	''' password text box and call the ZeroPasswordText subroutine.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOkClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		ownerPasswordSecure = SecureStringWrapper.ToSecureString( _
							  textBoxPassword.Text)
		ZeroPasswordText
		Me.Cursor = Cursors.Default
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
	
	''' <summary>
	''' This subroutine will call the ZeroPasswordText subroutine.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonCancelClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		ZeroPasswordText
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will zero out the memory allocated to the password
	''' text.
	''' </summary>
	Private Sub ZeroPasswordText
		NativeMethods.ZeroMemory(textBoxPassword.text, _
								 textBoxPassword.Text.Length * 2)
	End Sub
End Class
