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

Public Partial Class InformationPropertiesEditorForm
	Dim pdfFile As String
	Dim pdfViewId As Integer
	
	Public Sub New()
		Me.InitializeComponent()
	End Sub
		
	''' <summary>
	''' This subroutine will read the properties from the first PDF document
	''' in the queue and open the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InformationPropertiesEditorFormLoad(sender As Object, e As EventArgs)
		MainForm.infoPropEditMainOpen = True
		Do Until ReadNextDocument = 0
			If MainForm.mainEditorQueue.Count = 0 Then
				Me.Close
				Exit Sub
			End If
		Loop
	End Sub
	
	''' <summary>
	''' This function will read in the Information Properties of the next PDF
	''' document in the queue and update the form.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Private Function ReadNextDocument() As Integer
		pdfFile = MainForm.mainEditorQueue.Dequeue
		textBoxPDFDocument.Text = pdfFile
		Dim oPdfProperties As New PdfProperties(pdfFile)
		If oPdfProperties.Read = 0 Then
			textBoxTitle.Text = oPdfProperties.Title
			comboBoxAuthor.Text = oPdfProperties.Author
			comboBoxSubject.Text = oPdfProperties.Subject
			textBoxKeywords.Text = oPdfProperties.Keywords
			textBoxTitle.Select
			TextComboBoxTextChanged
			Return 0
		Else
			Dim oMessageDialog As New MessageDialog("Unable to read " & pdfFile & _
								  					". File will be skipped.")
			oMessageDialog.DisplayError
			Return 1
		End If
	End Function

	#Region "Form Object Events"
	
	''' <summary>
	''' This subroutine will call the View subroutine in the PdfFunctions
	''' class to display the PDF document.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonViewClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim result As Integer
		Dim oPdfFile As New PdfFile(pdfFile)
		result = oPdfFile.View(True)
		If result > 0 Then
			pdfViewId = result
		End If
		Me.Cursor = Cursors.Default
	End Sub

	''' <summary>
	''' This subroutine will fill the Subject combo box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ComboBoxAuthorDropDown(sender As Object, e As EventArgs)
		FillComboBox("Author")
	End Sub
		
	''' <summary>
	''' This subroutine will fill the Subject combo box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ComboBoxSubjectDropDown(sender As Object, e As EventArgs)
		FillComboBox("Subject")
	End Sub
	
	''' <summary>
	''' This subroutine will trim the leading space from the text in all of
	''' the text and combo boxes, and enables the Save button if the length of
	''' the Title, Author, and Subject is greater than 0.
	''' </summary>
	Private Sub TextComboBoxTextChanged
		textBoxTitle.Text = textBoxTitle.Text.TrimStart
		comboBoxAuthor.Text = comboBoxAuthor.Text.TrimStart
		comboBoxSubject.Text = comboBoxSubject.Text.TrimStart
		textBoxKeywords.Text = textBoxKeywords.Text.TrimStart
		If textBoxTitle.TextLength > 0 And _
		   comboBoxAuthor.Text.Length > 0 And _
		   comboBoxSubject.Text.Length > 0 Then
			buttonSave.Enabled = True
		Else
			buttonSave.Enabled = False
		End If
	End Sub
		
	''' <summary>
	''' This subroutine will close the PDF document, if open within SumatraPDF;
	''' save the information properties to the PDF document; and give the user
	''' the option to view the PDF document.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonSaveClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oTask As New Task(pdfViewId)
		oTask.Terminate
		Dim oPdfProperties As New PdfProperties(pdfFile)
		oPdfProperties.Title = textBoxTitle.Text.Trim
		oPdfProperties.Author = comboBoxAuthor.Text.Trim
		oPdfProperties.Subject = comboBoxSubject.Text.Trim
		oPdfProperties.Keywords = textBoxKeywords.Text.Trim
		If oPdfProperties.Save = 1 Then
			Me.Cursor = Cursors.Default
			Exit Sub
		Else
			If checkBoxViewAfterSave.Checked Then
				ButtonViewClick(Me, Nothing)
							
			    ' Wait for user to close the viewer.
			    Dim oWindow As New _
					 Window(Path.GetFileName(pdfFile) & " - SumatraPDF")
			    oWindow.WaitUntilClosed
			End If
			Me.Cursor = Cursors.Default
			If MainForm.mainEditorQueue.Count > 0 Then
				Do Until ReadNextDocument = 0
					If MainForm.mainEditorQueue.Count = 0 Then
						Me.Close
						Exit Sub
					End If
				Loop
			Else
				Me.Close
				Exit Sub
			End If
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will close the PDF document if open within SumatraPDF,
	''' and then close the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonCancelClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oTask As New Task(pdfViewId)
		oTask.Terminate
		Me.Cursor = Cursors.Default
		
		' Give the user a chance to quit editing PDF files.
		If MainForm.mainEditorQueue.Count > 0 Then
			Dim oMessageDialog As New _
				 MessageDialog("Do you want to continue editing PDF " & _
						   	   "files?")
			If oMessageDialog.DisplayQuestion = 7 Then	' "No" was selected.
				MainForm.mainEditorQueue.Clear
				Me.Close
				Exit Sub
			Else
				Do Until ReadNextDocument = 0
					If MainForm.mainEditorQueue.Count = 0 Then
						Me.Close
						Exit Sub
					End If
				Loop
				Exit Sub
			End If
		End If
		Me.Close
	End Sub
	
	#End Region

	#Region "Form Object Utility Functions and Subroutines"
	
	''' <summary>
	''' This subroutine will fill "comboBoxName" with either authors or
	''' subjects for the selected/specified author.  "comboBoxName" can be
	''' "Author" or "Subject.
	''' </summary>
	''' <param name="comboBoxName"></param>
	Private Sub FillComboBox(ByVal comboBoxTitle As String)
		If comboBoxTitle = "Author" Then
			comboBoxAuthor.Items.Clear
		ElseIf comboBoxTitle = "Subject" Then
			comboBoxSubject.Items.Clear
		Else
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open = 1 Then
			oDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		
		' Perform the query.
		Dim sql As String = Nothing
		If comboBoxTitle = "Author" Then
			sql = "select doc_author,count(doc_author) from " & _
				  "pdfkeeper.docs group by doc_author"
		ElseIf comboBoxTitle = "Subject" Then
			sql = "select doc_subject from pdfkeeper.docs where " & _
				  "doc_author = q'[" & comboBoxAuthor.Text & "]' group by " & _
				  "doc_subject"
		End If
		Try
			Using oOracleCommand As New OracleCommand(sql, _
				  oDatabaseConnection.oraConnection)
				Using oOracleDataReader As OracleDataReader = _
					  oOracleCommand.ExecuteReader()
	
					' Fill combo box with the results.
					Do While (oOracleDataReader.Read())
						If comboBoxTitle = "Author" Then
							comboBoxAuthor.Items.Add( _
								oOracleDataReader("doc_author"))
						ElseIf comboBoxTitle = "Subject" Then
							comboBoxSubject.Items.Add( _
								oOracleDataReader("doc_subject"))
						End If
					Loop
				End Using
			End Using
		Catch ex As OracleException
			Me.Cursor = Cursors.Default
			Dim oMessageDialog As New MessageDialog(ex.Message.ToString())
			oMessageDialog.DisplayError
		Finally
			oDatabaseConnection.Dispose
		End Try
		Me.Cursor = Cursors.Default
	End Sub
	
	#End Region
	
	''' <summary>
	''' This subroutine will be called when this form is closing.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InformationPropertiesEditorFormClosing(sender As Object, e As FormClosingEventArgs)
		MainForm.infoPropEditMainOpen = False
	End Sub
End Class
