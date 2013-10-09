'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
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
	Dim origin As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg"></param>
	Public Sub New(ByVal arg As String)
		Me.InitializeComponent()
		origin = arg
	End Sub
		
	''' <summary>
	''' This subroutine will read the properties from the first PDF document
	''' in the queue and open the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InformationPropertiesEditorFormLoad(sender As Object, e As EventArgs)
		If origin = "FORM" Then
			MainForm.infoPropEditFormOpen = True
		ElseIf origin = "WATCHER" Then
			MainForm.infoPropEditWatcherOpen = True
		End If
		Do Until ReadNextDocument = 0
			If QueueCount = 0 Then
				Me.Close
				Exit Sub
			End If
		Loop
		If origin = "WATCHER" Then
			MainForm.infoPropEditWatcherLoading = False
		End If
	End Sub
	
	''' <summary>
	''' This function will read in the Information Properties of the next PDF
	''' document in the queue and update the form.
	''' </summary>
	''' <returns>0 = Success, 1 = Failure</returns>
	Private Function ReadNextDocument() As Integer
		If origin = "FORM" Then
			pdfFile = MainForm.formEditorQueue.Peek
		ElseIf origin = "WATCHER" Then
			pdfFile = MainForm.watcherEditorSyncdQueue.Peek
		End If
		textBoxPDFDocument.Text = pdfFile
		Dim oPdfProperties As New PdfProperties(pdfFile)
		If oPdfProperties.Read = 0 Then
			textBoxTitle.Text = oPdfProperties.Title
			comboBoxAuthor.Text = oPdfProperties.Author
			comboBoxSubject.Text = oPdfProperties.Subject
			textBoxKeywords.Text = oPdfProperties.Keywords
			textBoxTitle.Select
			TextComboBoxTextChanged
			If UserSettings.AfterSavingOpenPdfChecked = 1 Then
				checkBoxViewAfterSave.Checked = True
			ElseIf UserSettings.AfterSavingOpenPdfChecked = 0 Then
				checkBoxViewAfterSave.Checked = False
			End If
			If UserSettings.AfterSavingUploadPdfChecked = 1 Then
				checkBoxAddUploadQueueAfterSave.Checked = True
				If UserSettings.AfterSavingUploadingDeletePdfChecked = 1 Then
					checkBoxDeleteSavedAfterUpload.Checked = True
				End If
			ElseIf UserSettings.AfterSavingUploadPdfChecked = 0 Then
				checkBoxAddUploadQueueAfterSave.Checked = False
			End If
			If UserSettings.AfterSavingDeletePdfChecked = 1 Then
				checkBoxDeleteOrigAfterSave.Checked = True
			ElseIf UserSettings.AfterSavingDeletePdfChecked = 0 Then
				checkBoxDeleteOrigAfterSave.Checked = False
			End If
			Return 0
		Else
			RemoveFirstObjectFromQueue
			Dim oMessageDialog As New MessageDialog("Unable to " & _
												    "read " & pdfFile & _
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

		' Get the title of the PDF document and open it.
		Dim oPdfProperties As New PdfProperties(pdfFile)
		If oPdfProperties.Read = 0 Then
			Dim titleBarText As String
			If oPdfProperties.Title = Nothing Then
				titleBarText = Path.GetFileName(pdfFile) & " - SumatraPDF"
			Else
				titleBarText = Path.GetFileName(pdfFile) & " - [" & _
								   oPdfProperties.Title & "] - SumatraPDF"
			End If
			Dim oWindow As New Window(titleBarText)
			If oWindow.IsOpen(True) = False Then
				processPdfViewer.StartInfo.Arguments = "-restrict " & _
														 chr(34) & _
													     pdfFile & _
												   	     chr(34)
				processPdfViewer.Start
				processPdfViewer.WaitForInputIdle(15000)
			End If
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
	''' This subroutine will control the state of the
	''' "Delete saved PDF document after being uploaded." check box.  It gets
	''' called when the "Add PDF document to upload queue." is checked or
	''' unchecked.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub CheckBoxAddUploadQueueAfterSaveCheckedChanged(sender As Object, e As EventArgs)
		If checkBoxAddUploadQueueAfterSave.Checked Then
			checkBoxDeleteSavedAfterUpload.Enabled = True
		Else
			checkBoxDeleteSavedAfterUpload.Checked = False
			checkBoxDeleteSavedAfterUpload.Enabled = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will make the warning message visible when
	''' "Delete original PDF Document" after saving is checked.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub CheckBoxDeleteOrigAfterSaveCheckedChanged(sender As Object, e As EventArgs)
		If checkBoxDeleteOrigAfterSave.Checked Then
			labelDeletePdfWarning.Visible = True
		Else
			labelDeletePdfWarning.Visible = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will close the PDF document if open within SumatraPDF,
	''' save the information properties to the PDF document, open the PDF
	''' document if the user selected this option, delete the original PDF
	''' document if the user selected this option, queue the PDF document for
	''' uploading if the user selected this option, and delete the saved PDF
	''' document after being uploaded if the user selected this option.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonSaveClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		TerminatePdfViewer
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
				UserSettings.AfterSavingOpenPdfChecked = 1
				ButtonViewClick(Me, Nothing)
			    processPdfViewer.WaitForExit
			Else
				UserSettings.AfterSavingOpenPdfChecked = 0
			End If
			If checkBoxDeleteOrigAfterSave.Checked Then
				UserSettings.AfterSavingDeletePdfChecked = 1
				Dim oMessageDialog2 As New _
					 MessageDialog("Are you sure you want to delete the " & _
					 			   "original file " & pdfFile & "?")
				If oMessageDialog2.DisplayQuestion = 6 Then	' "Yes" was selected.
					Dim oFileTools As New FileTools(pdfFile)
					oFileTools.Delete
				End If
			Else
				UserSettings.AfterSavingDeletePdfChecked = 0
			End If
			If checkBoxAddUploadQueueAfterSave.Checked Then
				UserSettings.AfterSavingUploadPdfChecked = 1
				If checkBoxDeleteSavedAfterUpload.Checked Then
					UserSettings.AfterSavingUploadingDeletePdfChecked = 1
					Dim oMessageDialog1 As New _
						 MessageDialog("Are you sure you want to delete " & _
					 			   	   "the saved file " & _
					 			   	    PdfProperties.modPdfFile & " " & _
					 			   	    "after being uploaded?")
					If oMessageDialog1.DisplayQuestion = 6 Then	' "Yes" was selected.
						MainForm.uploadDeleteSyncdQueue.Enqueue( _
									   	  PdfProperties.modPdfFile)
					Else
						MainForm.uploadSyncdQueue.Enqueue( _
									PdfProperties.modPdfFile)
					End If
				Else
					UserSettings.AfterSavingUploadingDeletePdfChecked = 0
					MainForm.uploadSyncdQueue.Enqueue(PdfProperties.modPdfFile)
				End If
			Else
				UserSettings.AfterSavingUploadPdfChecked = 0
			End If
			Me.Cursor = Cursors.Default
			RemoveFirstObjectFromQueue
			If QueueCount > 0 Then
				Do Until ReadNextDocument = 0
					If QueueCount = 0 Then
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
		TerminatePdfViewer
		Me.Cursor = Cursors.Default
		
		' Give the user a chance to quit editing PDF files.
		RemoveFirstObjectFromQueue
		If QueueCount > 0 Then
			Dim oMessageDialog As New _
				 MessageDialog("Do you want to continue editing PDF " & _
						   	   "files?")
			If oMessageDialog.DisplayQuestion = 7 Then	' "No" was selected.
				If origin = "FORM" Then
					MainForm.formEditorQueue.Clear
				ElseIf origin = "WATCHER" Then
					MainForm.watcherEditorSyncdQueue.Clear
				End If
				Me.Close
				Exit Sub
			Else
				Do Until ReadNextDocument = 0
					If QueueCount = 0 Then
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
	
	''' <summary>
	''' This subroutine will terminate the Sumatra PDF process object.
	''' </summary>
	Private Sub TerminatePdfViewer
		Try
			processPdfViewer.Kill
		Catch ex As InvalidOperationException
		End Try
	End Sub
	
	''' <summary>
	''' This function will return the number of objects in the Information
	''' Properties Editor queue.
	''' </summary>
	''' <returns>object count</returns>
	Private Function QueueCount As Int32
		Dim objCount As Int32
		If origin = "FORM" Then
			objCount = MainForm.formEditorQueue.Count
		ElseIf origin = "WATCHER" Then
			objCount = MainForm.watcherEditorSyncdQueue.Count
		End If
		Return objCount
	End Function
	
	''' <summary>
	''' This subroutine will remove the first object from the Information
	''' Properties Editor queue.
	''' </summary>
	Private Sub RemoveFirstObjectFromQueue
		If origin = "FORM" Then
			MainForm.formEditorQueue.Dequeue
		ElseIf origin = "WATCHER" Then
			MainForm.watcherEditorSyncdQueue.Dequeue
		End If
	End Sub
		
	#End Region
	
	''' <summary>
	''' This subroutine will be called when this form is closing.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InformationPropertiesEditorFormClosing(sender As Object, e As FormClosingEventArgs)
		If origin = "FORM" Then
			MainForm.infoPropEditFormOpen = False
		ElseIf origin = "WATCHER" Then
			MainForm.infoPropEditWatcherLoading = False
			MainForm.infoPropEditWatcherOpen = False
		End If
	End Sub
End Class
