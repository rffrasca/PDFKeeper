'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
'*
'* This program is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but
'* WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*
'*************************************************************************

Public Partial Class DocumentNotesForm
	Dim currentId As Integer
			
	Public Sub New(ByVal selectedId As Integer)
		Me.InitializeComponent()
		currentId = selectedId
	End Sub
	
	''' <summary>
	''' This subroutine will query document notes for currentId and load into
	''' the text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DocumentNotesFormLoad(sender As Object, e As EventArgs)
		Dim objDatabaseConnection As New DatabaseConnection
		If objDatabaseConnection.Open = 1 Then
			objDatabaseConnection.Dispose
			Exit Sub
		End If
		Dim sql As String = "select doc_notes from pdfkeeper.docs " & _
							"where doc_id = " & currentId
		Try
			Using objOracleCommand As New OracleCommand(sql, _
				  objDatabaseConnection.objOracleConnection)
				Using objOracleDataReader As OracleDataReader = _
					  objOracleCommand.ExecuteReader()
					objOracleDataReader.Read()
					
					' To prevent an exception, ignore an empty string.
					If Not objOracleDataReader.IsDBNull(0) Then
						textBoxNotes.Text = objOracleDataReader("doc_notes")
			
						' Prevent text from being highlighted.
						textBoxNotes.SelectionStart = 0
					End If
				End Using
			End Using
		Catch ex As OracleException
			MessageDialog.Display("Error", ex.Message.ToString())
		Finally
			objDatabaseConnection.Dispose
		End Try
		
		' Prior to calling ScrollToCaret, form must be visible. Otherwise,
		' text box will not scroll to the bottom.
		Me.Show
		
		TextBoxMoveCursorToEnd
	End Sub
	
	#Region "Form Object Events"

	''' <summary>
	''' This subroutine will trim a leading space from the text in the text
	''' box; and enable the Print button, only if the text box contains text.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TextBoxNotesTextChanged(sender As Object, e As EventArgs)
		textBoxNotes.Text = textBoxNotes.Text.TrimStart
		If textBoxNotes.Text.Length > 0 Then
			buttonPrint.Enabled = True
		Else
			buttonPrint.Enabled = False
		End If
	End Sub

	''' <summary>
	''' This subroutine will append a Date/Time stamp with the database
	''' user name to the text in the text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonDateTimeClick(sender As Object, e As EventArgs)
		TextBoxMoveCursorToEnd
		If textBoxNotes.TextLength > 0 Then
			textBoxNotes.AppendText(vbCrLf & vbCrLf)
		End If
		textBoxNotes.AppendText("--- " & Date.Now & " (" & _
		 			 UserSettings.LastUserName & ") ---" & vbCrLf)
		TextBoxMoveCursorToEnd
	End Sub
	
	''' <summary>
	''' The subroutine will prompt the user to select the printer to use for
	''' printing the contents of the text box; and call the print process, if
	''' the OK button was selected.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonPrintClick(sender As Object, e As EventArgs)
		Dim result As Boolean = False
		While result = False
			Try
				If printDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
					Me.Cursor = Cursors.WaitCursor
					printDocumentNotes.Print
					Me.Cursor = Cursors.Default
				End If
				result = True
			Catch ex As System.ArgumentNullException
			Catch ex As System.ComponentModel.Win32Exception
			Catch ex As System.NullReferenceException
			End Try
		End While
	End Sub

	''' <summary>
	''' This subroutine will print the contents of the text box using the
	''' printer chosen by the user.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub PrintDocumentNotesPrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)
		PrintPages.FromString(textBoxNotes.Text, textBoxNotes.Font, e)
	End Sub
	
	''' <summary>
	''' This subroutine will update the document notes for currentId, in the
	''' database.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonUpdateClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		textBoxNotes.Text = textBoxNotes.Text.Trim
		Dim objDatabaseConnection As New DatabaseConnection
		If objDatabaseConnection.Open = 1 Then
			objDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		Dim sql As String = "update pdfkeeper.docs set " & _
			 				"doc_notes =q'[" & textBoxNotes.Text & "]'," & _
			 				"doc_dummy = '' where doc_id = " & currentId
		Using objOracleCommand As New OracleCommand(sql, _
			  objDatabaseConnection.objOracleConnection)
			Dim result As Byte = 0
			Try
				objOracleCommand.ExecuteNonQuery
			Catch ex As OracleException
				Me.Cursor = Cursors.Default
				MessageDialog.Display("Error", ex.Message.ToString())
				result = 1
			Finally
				objDatabaseConnection.Dispose
			End Try
			If result = 1 Then
				Exit Sub
			End If
		End Using
		Me.Cursor = Cursors.Default
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
	
	#End Region
	
	#Region "Form Object Utility Subroutines"

	''' <summary>
	''' This subroutine will select the text box and move the cursor to the
	''' end of the text.
	''' </summary>
	Private Sub TextBoxMoveCursorToEnd
		textBoxNotes.Select
		textBoxNotes.Select(textBoxNotes.Text.Length,0)
		textBoxNotes.ScrollToCaret
	End Sub
	
	#End Region
End Class
