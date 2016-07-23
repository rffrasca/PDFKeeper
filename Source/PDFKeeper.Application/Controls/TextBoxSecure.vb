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
'* Date: 7/5/2016
'* Time: 12:16 PM
'*
'******************************************************************************

Public Class TextBoxSecure
	Inherits System.Windows.Forms.TextBox
	Friend SecureText As New SecureString
	
	''' <summary>
	''' Constructor sets text box properties.
	''' </summary>
	Public Sub New()
		Me.UseSystemPasswordChar = True
		Me.ShortcutsEnabled = False
	End Sub

	''' <summary>
	''' Deletes characters that are selected in the text box from the secure
	''' string, ignoring the Esc and Enter keys.
	''' </summary>
	''' <param name="e"></param>
	Protected Overrides Sub OnKeyDown( _
		ByVal e As System.Windows.Forms.KeyEventArgs)
		
		If e.KeyCode = Keys.Delete Then
			If Me.SelectionLength > 0 Then
				RemoveCharsFromSecureString
			ElseIf Me.SelectionStart < Me.Text.Length Then
				SecureText.RemoveAt(Me.SelectionStart)
			End If
			SetTextBoxTextProperty(Me.SelectionStart)
			e.Handled = True
		ElseIf e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Enter Then
			e.Handled = True
		End If
	End Sub
		
	''' <summary>
	''' Updates the secure string by either removing the printable character
	''' that was removed from the text box by pressing the Backspace key or
	''' adding the printable character entered.
	''' </summary>
	''' <param name="e"></param>
	Protected Overrides Sub OnKeyPress( _
		ByVal e As System.Windows.Forms.KeyPressEventArgs)
	
		If e.KeyChar = ControlChars.Back Then
			If Me.SelectionLength > 0 Then
				RemoveCharsFromSecureString
				SetTextBoxTextProperty(Me.SelectionStart)
			ElseIf Me.SelectionStart > 0 Then
				SecureText.RemoveAt(Me.SelectionStart - 1)
				SetTextBoxTextProperty(Me.SelectionStart - 1)
			End If
		Else
			If Me.SelectionLength > 0 Then
				RemoveCharsFromSecureString
			End If
			SecureText.InsertAt(Me.SelectionStart, e.KeyChar)
			SetTextBoxTextProperty(Me.SelectionStart + 1)
		End If
		e.Handled = True
    End Sub
     
    ''' <summary>
    ''' Removes characters that are selected in the text box from the secure
	''' string.
    ''' </summary>
	Private Sub RemoveCharsFromSecureString
    	For i As Integer = 0 To Me.SelectionLength - 1
    		SecureText.RemoveAt(Me.SelectionStart)
		Next
	End Sub
	
    ''' <summary>
    ''' Sets the text property of the text box to a string of asterisks
	''' matching the length of the secure string, and then sets the text
	''' selection starting point.
    ''' </summary>
    ''' <param name="startingPos">Selection starting point.</param>
    Private Sub SetTextBoxTextProperty(ByVal startingPos As Integer)
    	Me.Text = New String(CChar("*"), SecureText.Length)
    	Me.SelectionStart = startingPos
    End Sub
End Class
