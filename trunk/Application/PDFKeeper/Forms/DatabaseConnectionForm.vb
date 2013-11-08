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
	Friend Shared connectionStringSecure As SecureString
		
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will set the font to MS Sans Serif 8pt in XP and
	''' Segoe UI 9pt in Vista or later; and prefill the User Name and Data
	''' Source text boxes.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DatabaseConnectionFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
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
	''' This subroutine will trim leading and trailing spaces from the text in
	''' all of the text boxes and will enable the OK button if the length of
	''' the User Name and Data Source is greater than 0.
	''' </summary>
	Private Sub TextBoxTextChanged
		textBoxUserName.Text = textBoxUserName.Text.Trim
		textBoxPassword.Text = textBoxPassword.Text.Trim
		textBoxDataSource.Text = textBoxDataSource.Text.Trim
		If textBoxUserName.TextLength > 0 And _
		   textBoxDataSource.TextLength > 0 Then
			buttonOK.Enabled = True
		Else
			buttonOK.Enabled = False
		End If
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
	''' This subroutine will create the secure database connection string;
	''' open a database connection, and then close it; and update the user
	''' settings properties with the specified User Name and Data Source.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOkClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		CreateSecureConnectionString
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
	''' This subroutine will create a secure database connection string.
	''' </summary>
	Private Sub CreateSecureConnectionString
		connectionStringSecure = SecureStringWrapper.ToSecureString( _
							"User Id=" + textBoxUserName.Text + ";" & _
							"Password=" + textBoxPassword.Text + ";" & _
							"Data Source=" + textBoxDataSource.Text + ";" & _
							"Persist Security Info=False;Pooling=True")
		ZeroPasswordText
	End Sub
	
	''' <summary>
	''' This function will check if a connection to the database can be made.
	''' </summary>
	''' <returns>True or False</returns>
	Private Shared Function CanConnectToDatabase() As Boolean
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open = 0 Then
			oDatabaseConnection.Dispose
			Return True
		Else
			connectionStringSecure.Dispose
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
