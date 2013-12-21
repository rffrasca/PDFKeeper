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

Public Partial Class DatabaseConnectionForm
	Friend Shared dbPassword As New SecureString
			
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will set the font to MS Sans Serif 8pt in XP or
	''' Segoe UI 9pt in Vista or later, prefill the User Name and Data
	''' Source text boxes, and clear the password string.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DatabaseConnectionFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		dbPassword.Clear
		ReadUserProperties
	End Sub
	
	''' <summary>
	''' This subroutine will read the last User Name and Data Source from the
	''' user properties and update the form.
	''' </summary>
	Private Sub ReadUserProperties
		textBoxUserName.Text = UserSettings.LastUserName
		textBoxDataSource.Text = UserSettings.LastDataSource
	End Sub
	
	''' <summary>
	''' This subroutine will display the help page for this form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="hlpevent"></param>
	Private Sub DatabaseConnectionFormHelpRequested(sender As Object, hlpevent As HelpEventArgs)
		Me.Cursor = Cursors.WaitCursor
		HelpWrapper.ShowHelp(Me, "Database Connection.html")
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will trim leading and trailing spaces from the text in
	''' the User Name and Data Source text boxes and will enable the OK button
	''' if the length of the User Name and Data Source is greater than 0.
	''' </summary>
	Private Sub TextBoxTextChanged
		textBoxUserName.Text = textBoxUserName.Text.Trim
		textBoxDataSource.Text = textBoxDataSource.Text.Trim
		If textBoxUserName.TextLength > 0 And _
		   textBoxDataSource.TextLength > 0 Then
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
				dbPassword.RemoveAt(textBoxPassword.SelectionStart)
			End If
			SetPWTextBoxTextProperty(textBoxPassword.SelectionStart)
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
				SetPWTextBoxTextProperty(textBoxPassword.SelectionStart)
			ElseIf textBoxPassword.SelectionStart > 0 Then
				dbPassword.RemoveAt(textBoxPassword.SelectionStart - 1)
				SetPWTextBoxTextProperty(textBoxPassword.SelectionStart - 1)
			End If
		Else
			If textBoxPassword.SelectionLength > 0 Then
				RemoveCharacters()
			End If
			dbPassword.InsertAt(textBoxPassword.SelectionStart, e.KeyChar)
			SetPWTextBoxTextProperty(textBoxPassword.SelectionStart + 1)
		End If
		e.Handled = True
	End Sub
		
	''' <summary>
	''' This subroutine will remove selected characters from the password
	''' string.
	''' </summary>
	Private Sub RemoveCharacters
		For i As Integer = 0 To textBoxPassword.SelectionLength - 1
			dbPassword.RemoveAt(textBoxPassword.SelectionStart)
		Next
	End Sub
	
	''' <summary>
	''' This subroutine will set the text property of the password text box to
	''' the number of password characters matching the length of the password
	''' string.
	''' </summary>
	''' <param name="caretPos"></param>
	Private Sub SetPWTextBoxTextProperty(ByVal caretPos As Integer)
		textBoxPassword.Text = New String(CChar("*"), dbPassword.Length)
		textBoxPassword.SelectionStart = caretPos
	End Sub
	
	''' <summary>
	''' This subroutine will clear the Password text box, open a database
	''' connection and then close it, and update the user settings properties
	''' with the specified User Name and Data Source.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOkClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		textBoxPassword.Text = Nothing
		If CanConnectToDatabase() = False Then
			textBoxUsername.Select
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		SaveUserProperties
		Me.Cursor = Cursors.Default
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
	
	''' <summary>
	''' This function will check if a connection to the database can be made.
	''' </summary>
	''' <returns>True or False</returns>
	Private Function CanConnectToDatabase() As Boolean
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open(textBoxUserName.Text, dbPassword, _
									textBoxDataSource.Text) = 0 Then
			oDatabaseConnection.Dispose
			Return True
		Else
			dbPassword.Clear
			Return False
		End If
	End Function
	
	''' <summary>
	''' This subroutine will save the last User Name and Data Source to the
	''' coorespending user properties.
	''' </summary>
	Private Sub SaveUserProperties
		UserSettings.LastUserName = textBoxUserName.Text
		UserSettings.LastDataSource = textBoxDataSource.Text
	End Sub
	
	''' <summary>
	''' This subroutine will clear the password string and text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonCancelClick(sender As Object, e As EventArgs)
		dbPassword.Clear
		textBoxPassword.Text = Nothing
	End Sub
End Class
