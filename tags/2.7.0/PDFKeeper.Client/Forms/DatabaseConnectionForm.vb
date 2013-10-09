'******************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Public Partial Class DatabaseConnectionForm
	Dim oGCHandle As GCHandle = GCHandle.Alloc(textBoxPassword, _
								GCHandleType.Pinned)
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will prefill the User Name and Data Source text boxes.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DatabaseConnectionFormLoad(sender As Object, e As EventArgs)
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
	''' This subroutine will call a method to create the database connection
	''' string; open a database connection, and then close it; and finally
	''' will update the user settings properties with the specified User Name
	''' and Data Source.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOkClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oDatabaseConnectionString As New _
			 DatabaseConnectionString(textBoxUserName.Text, _
							      	  textBoxPassword.Text, _
								  	  textBoxDataSource.Text)
		oDatabaseConnectionString.Create						  		
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open = 0 Then
			oDatabaseConnection.Dispose
		Else
			textBoxUsername.Select
			DatabaseConnectionString.oraConnectionString.Clear
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		UserSettings.LastUserName = textBoxUserName.Text
		UserSettings.LastDataSource = textBoxDataSource.Text
		NativeMethods.ZeroMemory(textBoxPassword.text, _
								 textBoxPassword.Text.Length * 2)
		oGCHandle.Free
		Me.Cursor = Cursors.Default
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
End Class
