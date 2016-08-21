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

''' <summary>
''' Secure implemenation of the TextBox control.
''' </summary>
Public Class SecureTextBox
	Inherits System.Windows.Forms.TextBox
	
	''' <summary>
	''' Text entered by the user is stored in this member. The TextBox Text
	''' property will only contain asterisks.
	''' </summary>
	Friend SecureText As New SecureString
	
	''' <summary>
	''' When the user presses the Delete key, either remove the character after
	''' the cursor or all selected characters from the SecureString member.
	''' </summary>
	''' <param name="e"></param>
	Protected Overrides Sub OnKeyDown( _
		ByVal e As System.Windows.Forms.KeyEventArgs)
		If e.KeyCode = Keys.Delete Then
			If Me.SelectionLength > 0 Then
				RemoveSelectedCharsFromSecureString
			ElseIf Me.SelectionStart < Me.Text.Length Then
				SecureText.RemoveAt(Me.SelectionStart)
			End If
			SetTextPropertyAndCursorPosition(Me.SelectionStart)
			e.Handled = True
		ElseIf e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Enter Then
			e.Handled = True
		End If
	End Sub
	
	''' <summary>
	''' When the user presses the Backspace key, either remove the character
	''' before the cursor or all seleected characters. For any other printable
	''' character, remove all selected characters and insert printable
	''' character into SecureString member.
	''' </summary>
	''' <param name="e"></param>
	Protected Overrides Sub OnKeyPress( _
		ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = ControlChars.Back Then
			If Me.SelectionLength > 0 Then
				RemoveSelectedCharsFromSecureString
				SetTextPropertyAndCursorPosition(Me.SelectionStart)
			ElseIf Me.SelectionStart > 0 Then
				SecureText.RemoveAt(Me.SelectionStart - 1)
				SetTextPropertyAndCursorPosition(Me.SelectionStart - 1)
			End If
		Else
			If Me.SelectionLength > 0 Then
				RemoveSelectedCharsFromSecureString
			End If
			SecureText.InsertAt(Me.SelectionStart, e.KeyChar)
			SetTextPropertyAndCursorPosition(Me.SelectionStart + 1)
		End If
		e.Handled = True
	End Sub
	
	Private Sub RemoveSelectedCharsFromSecureString
    	For i As Integer = 0 To Me.SelectionLength - 1
    		SecureText.RemoveAt(Me.SelectionStart)
		Next
	End Sub
	
	''' <summary>
	''' Sets the text property of the text box to a string of asterisks
	''' matching the length of the SecureString member, and then sets the
	''' cursor position.
	''' </summary>
	''' <param name="cursorPosition">Where to position the cursor.</param>
	Private Sub SetTextPropertyAndCursorPosition( _
		ByVal cursorPosition As Integer)
    	Me.Text = New String(CChar("*"), SecureText.Length)
    	' The next step must be performed after setting the Text property. It
    	' does not work properly if moved out of this method.
    	Me.SelectionStart = cursorPosition
	End Sub
End Class
