'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2014 Robert F. Frasca
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

Public Partial Class MainForm
	Dim updateAvailable As Boolean
	Dim history As New ArrayList
	Dim sortColumn As Byte
	Dim sortOrder As string = "asc"
	Dim selectedId As Integer
	Dim documentNotesUndoText As String
	Dim documentCaptureFolderChanged As Boolean = True
	Dim searchLastTitleText As String = "PDFKeeper"
	Dim searchLastStatusMessage As String
	Dim captureLastStatusMessage As String
	Dim capturePdfFile As String
	Dim captureModPdfFile As String
	Dim lastPdfDocumentCheckResult As Integer
	
	Public Sub New()
		Me.InitializeComponent()
		StartUpdateCheck
		fileSystemWatcherDocumentCapture.Path = CaptureDir
	End Sub
	
	#Region "Form Loading"
	
	''' <summary>
	''' This subroutine will set the font to MS Sans Serif 8pt in XP or
	''' Segoe UI 9pt in Vista or later; set the form size and position based
	''' on the values retrieved from the User Settings object properties, a
	''' default will be used for any value that is NUL; call the
	''' ResizeListViewColumns subroutine; and select the Search Text combo box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub MainFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		PositionAndSizeForm
		ResizeListViewColumns
		comboBoxSearchText.Select
	End Sub
	
	''' <summary>
	''' This subroutine will start the update check thread.
	''' </summary>
	Private Sub StartUpdateCheck
		If CDbl(UserSettings.UpdateCheck) = 1 Then
			toolStripStatusLabelUpdateStatus.Text = _
				MainForm_Strings.CheckingVersion
			BackgroundWorkerUpdateCheck.RunWorkerAsync
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will the form size and position based on the values
	''' retrieved from the User Settings properties, a default will be used
	''' for any NUL value.
	''' </summary>
	Private Sub PositionAndSizeForm
		Me.Top = CInt(UserSettings.FormPositionTop)
		Me.Left = CInt(UserSettings.FormPositionLeft)
		If CInt(UserSettings.FormPositionHeight) = Nothing Then
			Dim workingRectangle As System.Drawing.Rectangle = _
				Screen.PrimaryScreen.WorkingArea
			Me.Size = New System.Drawing.Size(workingRectangle.Width - 10, _
				workingRectangle.Height - 10)
			If Me.Height > 714 Then
				Me.Height = 714
			End If
		Else
			Me.Height = CInt(UserSettings.FormPositionHeight)
		End If
		If Not CInt(UserSettings.FormPositionWidth) = Nothing Then
			Me.Width = CInt(UserSettings.FormPositionWidth)
		ElseIf Me.Width > 714 Then
			Me.Width = 714			
		End If
		Me.WindowState = CType(UserSettings.FormPositionWindowState, _
			Windows.Forms.FormWindowState)
	End Sub
	
	#End Region
	
	#Region "Form Menu"
	
	''' <summary>
	''' This subroutine will prompt the user for the folder and file name to
	''' save the PDF as, and then query the database to get the PDF document
	''' for the selected ID.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemSavePdfToDiskClick(sender As Object, e As EventArgs)
		SaveFileDialog.InitialDirectory = UserSettings.SaveFileLastFolder
		
		' Construct the file name prefill using the title of the selected
		' list view item.
		SaveFileDialog.FileName = listViewDocs.SelectedItems(0).SubItems(1).Text & _
								".pdf"

		If SaveFileDialog.ShowDialog() = 2 Then
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		Dim pdfFile As String = SaveFileDialog.FileName
		Dim pdfFileInfo As New FileInfo(pdfFile)
		UserSettings.SaveFileLastFolder = pdfFileInfo.DirectoryName
		If Not pdfFileInfo.Extension.ToUpper(CultureInfo.InvariantCulture) = ".PDF" Then
			pdfFile = pdfFile & ".pdf"
		End If
		If PdfFileTask.RetrieveFromDatabase(selectedId, pdfFile) = 0 Then
			Me.Cursor = Cursors.Default
			MessageBoxWrapper.ShowInformation(String.Format( _
									CultureInfo.CurrentCulture, _
									MainForm_Strings.PdfSaved, pdfFile))
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will select the "Document Notes" tab; prompt the user
	''' to select the printer to use for printing the contents of the document
	''' notes text box; and then call the print process, if the OK button was
	''' selected.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemPrintDocumentNotesClick(sender As Object, e As EventArgs)
		tabControlDocNotesKeywords.SelectTab(0)
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
	''' This subroutine will print the contents of the Document Notes text box
	''' using the printer chosen by the user.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub PrintDocumentNotesPrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)
		Print.Text(textBoxDocumentNotes.Text, textBoxDocumentNotes.Font, e)
	End Sub
	
	''' <summary>
	''' This subroutine will exit the form and the application.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemExitClick(sender As Object, e As EventArgs)
		Me.Close
	End Sub

	''' <summary>
	''' This subroutine will select the "Document Notes" tab and append a
	''' Date/Time stamp with the database user name to the existing text in
	''' the Document Notes text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemInsertDateTimeIntoDocumentNotesClick(sender As Object, e As EventArgs)
		tabControlDocNotesKeywords.SelectTab(0)
		TextBoxDocumentNotesScrollToEnd
		If textBoxDocumentNotes.TextLength > 0 Then
			textBoxDocumentNotes.AppendText(vbCrLf & vbCrLf)
		End If
		textBoxDocumentNotes.AppendText("--- " & Date.Now & " (" & _
		 			 UserSettings.LastUserName & ") ---" & vbCrLf)
		TextBoxDocumentNotesScrollToEnd
	End Sub

	''' <summary>
	''' This subroutine will check all listview items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemCheckAllClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oListViewItem As ListViewItem
		For Each oListViewItem In listViewDocs.Items
			oListViewItem.Checked = True
		Next
		UpdateListCountStatusBar
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will uncheck all listview items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemUncheckAllClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oListViewItem As ListViewItem
		For Each oListViewItem In listViewDocs.Items
			oListViewItem.Checked = False
		Next
		UpdateListCountStatusBar
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will delete the database records for all checked
	''' listview items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemDeleteCheckedDocumentsClick(sender As Object, e As EventArgs)
		DocumentNotesModifiedCheck
		If MessageBoxWrapper.ShowQuestion( _
				MainForm_Strings.DeleteChecked) = 7 Then ' No
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open(UserSettings.LastUserName, _
				DatabaseConnectionForm.dbPassword, _
				UserSettings.LastDataSource) = 1 Then
			oDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		Dim oListViewItem As ListViewItem
		For Each oListViewItem In listViewDocs.CheckedItems
			Dim sql As String = "delete from pdfkeeper.docs where " & _
								"doc_id =" & oListViewItem.Text
			Using oOracleCommand As New OracleCommand(sql, _
				  oDatabaseConnection.oraConnection)
				Try
					oOracleCommand.ExecuteNonQuery
				Catch ex As OracleException
					Me.Cursor = Cursors.Default
					MessageBoxWrapper.ShowError(ex.Message.ToString())
					oDatabaseConnection.Dispose
					Exit Sub
				End Try
			End Using
			oListViewItem.Checked = False
			oListViewItem.Remove
		Next
		oDatabaseConnection.Dispose
		UpdateListCountStatusBar
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will prompt the user to select one or more HTML files,
	'''	and then proceed to convert the selected file(s) to PDF.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemHtmlConverterClick(sender As Object, e As EventArgs)
		OpenFileDialog.InitialDirectory = UserSettings.OpenFileLastFolder
		OpenFileDialog.Title = MainForm_Strings.OpenFileDialogHtmlConvert
        OpenFileDialog.FileName = Nothing
 		If OpenFileDialog.ShowDialog() = 2 Then
 			Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
 		For Each fileName As String In OpenFileDialog.FileNames
 			Dim oFileInfo As New fileinfo(fileName)
 			UserSettings.OpenFileLastFolder = oFileInfo.DirectoryName
 			If FileTask.ConvertToPdf(fileName) = 1 Then
 				MessageBoxWrapper.ShowError(String.Format( _
					CultureInfo.CurrentCulture, _
					MainForm_Strings.FailedToConvert, fileName))
			End If
        Next
		Me.Cursor = Cursors.Default 
	End Sub
	
	''' <summary>
	''' This subroutine will open the Capture folder with Windows Explorer.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemCaptureFolderClick(sender As Object, e As EventArgs)
		OpenFolder(CaptureDir)
	End Sub
	
	''' <summary>
	''' This subroutine will open the DirectUpload folder with Windows Explorer.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemDirectUploadFolderClick(sender As Object, e As EventArgs)
		OpenFolder(UploadDir)
	End Sub
	
	''' <summary>
	''' This subroutine will open the specified folder with Windows Explorer.
	''' </summary>
	''' <param name="folderPath"></param>
	Private Sub OpenFolder(folderPath As String)
		Me.Cursor = Cursors.WaitCursor
		processExplorer.StartInfo.FileName = folderPath
		processExplorer.Start
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will open the Direct Upload Configuration form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemDirectUploadConfigClick(sender As Object, e As EventArgs)
		timerDirectUpload.Stop
		DirectUploadConfigurationForm.ShowDialog()
		timerDirectUpload.Start
	End Sub
	
	''' <summary>
	''' This subroutine will check/uncheck the
	''' "Automatically Check for Newer Version" menu item.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemHelpClick(sender As Object, e As EventArgs)
		If CDbl(UserSettings.UpdateCheck) = 1 Then
			toolStripMenuItemCheckNewerVersion.Checked = True
		Else
			toolStripMenuItemCheckNewerVersion.Checked = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will display the help file.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemContentsClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim helpFile As String
		If tabControlMain.SelectedIndex = 0 Then	' "Document Search" tab
			helpFile = "Document Search.html"
		Else	' "Document Capture" tab
			helpFile = "Document Capture.html"
		End If
		HelpWrapper.ShowHelp(Me, helpFile)
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will enable/disable update checking.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemCheckNewerVersionClick(sender As Object, e As EventArgs)
		If toolStripMenuItemCheckNewerVersion.Checked Then
			UserSettings.UpdateCheck = CStr(0)
			toolStripMenuItemCheckNewerVersion.Checked = False
		Else
			UserSettings.UpdateCheck = CStr(1)
			toolStripMenuItemCheckNewerVersion.Checked = True
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will open the New Issue page on the Project Site using
	''' the default web browser.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemReportNewIssueClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Process.Start(ConfigurationManager.AppSettings("NewIssueUrl"))
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will display the About box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripMenuItemAboutClick(sender As Object, e As EventArgs)
		AboutForm.ShowDialog()
	End Sub
	
	#End Region

	#Region "Document Search"
	
	''' <summary>
	''' This subroutine will fill the Search Text combo box with the items
	''' contained in the history array.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ComboBoxSearchTextDropDown(sender As Object, e As EventArgs)
		comboBoxSearchText.Items.Clear
		For Each historyItem As String In history
			comboBoxSearchText.Items.Add(historyItem)
		Next
	End Sub
	
	''' <summary>
	''' This subroutine will enable the Search button if the search text does
	''' not contain any syntax errors.  The user will be alerted if a syntax
	''' error was detected.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ComboBoxSearchTextTextChanged(sender As Object, e As EventArgs)
		Dim errorMessage As String
		errorProvider.Clear
		toolStripMenuItemRefresh.Enabled = False
		buttonSearch.Enabled = False
		comboBoxSearchText.Text = comboBoxSearchText.Text.TrimStart
		If comboBoxSearchText.Text.Length = 0 Then
			Exit Sub
		End If
		If comboBoxSearchText.Text.IndexOf("*", StringComparison.Ordinal) <> -1 Then
			errorMessage = MainForm_Strings.SearchTextUsageError
			errorProvider.SetError(comboBoxSearchText, errorMessage)
			comboBoxSearchText.Select
			Exit Sub
		End If
		If SearchTextInvalid() Then
			errorMessage = MainForm_Strings.ImproperUsage
			errorProvider.SetError(comboBoxSearchText, errorMessage)
			comboBoxSearchText.Select
			Exit Sub
		End If
		errorProvider.Clear
		buttonSearch.Enabled = True
	End Sub
	
	''' <summary>
	''' This function will return True or False if the Search Text specified is
	''' invalid.
	''' </summary>
	''' <returns>True or False</returns>
	Private Function SearchTextInvalid() As Boolean
		If comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "MINUS" Or _
			comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "MINUS " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "NEAR" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "NEAR " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "NOT" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "NOT " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "AND" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "AND " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "EQUIV" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "EQUIV " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "WITHIN" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "WITHIN " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "OR" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "OR " Or _
 		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "ACCUM" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "ACCUM " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "FUZZY" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "FUZZY " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "ABOUT" Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "ABOUT " Or _
		   	comboBoxSearchText.Text.ToUpper(CultureInfo.InvariantCulture) = "ABOUT()" Or _
		   	comboBoxSearchText.Text.IndexOf("{}", StringComparison.Ordinal) <> -1 Or _
		   	comboBoxSearchText.Text.IndexOf("()", StringComparison.Ordinal) <> -1 Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "=" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = ";" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = ">" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "-" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "~" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "&" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "|" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "," Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "!" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "{" Or _
		   	comboBoxSearchText.Text.Substring(0,1) = "(" Or _
		   	comboBoxSearchText.Text = "?" Or _
		   	comboBoxSearchText.Text = "$" Then
			Return True
		End If
		Return False
	End Function
	
	''' <summary>
	''' This subroutine will search the database for document records that
	''' match the Search Text, and then add the matching records to the
	''' listview, sorted by the selected column. If the search returns one or
	''' more records, add the search text to the search text history, only if
	''' the search text doesn't already exist in the history.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonSearchClick(sender As Object, e As EventArgs)
		DocumentNotesModifiedCheck
		Me.Cursor = Cursors.WaitCursor
		listViewDocs.SelectedItems.Clear
		listViewDocs.Items.Clear
		toolStripStatusLabelMessage.Text = Nothing
		Me.Refresh	' Form needed to be refreshed for status label to clear.
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open(UserSettings.LastUserName, _
				DatabaseConnectionForm.dbPassword, _
				UserSettings.LastDataSource) = 1 Then
			oDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			comboBoxSearchText.Select
			Exit Sub
		End If
		
		' Build query string.
		Dim orderBy As String = Nothing
		Select Case sortColumn
			Case = 0	' ID
				orderBy = "doc_id " & sortOrder & "," & _
						  "doc_title " & sortOrder & "," & _
					  	  "doc_author " & sortOrder & "," & _
					  	  "doc_subject " & sortOrder & "," & _
					 	  "doc_added " & sortOrder
			Case = 1	' Title
				orderBy = "doc_title " & sortOrder & "," & _
						  "doc_author " & sortOrder & "," & _
				      	  "doc_subject " & sortOrder & "," & _
						  "doc_added " & sortOrder & "," & _
					  	  "doc_id " & sortOrder
			Case = 2	' Author
				orderBy = "doc_author " & sortOrder & "," & _
						  "doc_subject " & sortOrder & "," & _
						  "doc_added " & sortOrder & "," & _
					  	  "doc_id " & sortOrder & "," & _
					  	  "doc_title " & sortOrder
			Case = 3	' Subject
				orderBy = "doc_subject " & sortOrder & "," & _
						  "doc_added " & sortOrder & "," & _
					  	  "doc_id " & sortOrder & "," & _
					  	  "doc_title " & sortOrder & "," & _
						  "doc_author " & sortOrder
			Case = 4	' Added
				orderBy = "doc_added " & sortOrder & "," & _
						  "doc_id " & sortOrder & "," & _
					  	  "doc_title " & sortOrder & "," & _
						  "doc_author " & sortOrder & "," & _
						  "doc_subject " & sortOrder
		End Select
		Dim sql As String = "select doc_id,doc_title,doc_author," & _
							"doc_subject,doc_added from " & _
							"pdfkeeper.docs where " & _
							"(contains(doc_dummy,q'[" & _
							 comboBoxSearchText.Text & "]'))>0 " & _
							"order by " & orderBy
		
		' Perform the query.
		Using oOracleCommand As New OracleCommand(sql, _
			  oDatabaseConnection.oraConnection)
			Try
				Using oOracleDataReader As OracleDataReader = _
					  oOracleCommand.ExecuteReader()
			
					' Fill listview with the results.
					Dim oListViewItem As ListViewItem
					Dim itemArray(5) As String
					Do While (oOracleDataReader.Read())
						itemArray(0) = CType( _
							oOracleDataReader("doc_id"), String)
						itemArray(1) = CType( _
							oOracleDataReader("doc_title"), String)
						itemArray(2) = CType( _
							oOracleDataReader("doc_author"), String)
 						itemArray(3) = CType( _
 							oOracleDataReader("doc_subject"), String)
 						itemArray(4) = CType( _
 							oOracleDataReader("doc_added"), String)
						oListViewItem = New ListViewItem(itemArray)
 						ListViewDocs.Items.Add(oListViewItem)
					Loop
				End Using
  			Catch ex As OracleException
				oDatabaseConnection.Dispose
				Me.Cursor = Cursors.Default
				MessageBoxWrapper.ShowError(ex.Message.ToString())
				comboBoxSearchText.Select
				Exit Sub
			End Try
		End Using
		
		oDatabaseConnection.Dispose
		UpdateListCountStatusBar
		RightJustifyListViewDocIds
		ResizeListViewColumns
		If listViewDocs.Items.Count > 0 Then
			If sortOrder = "asc" Then
				listViewDocs.EnsureVisible(listViewDocs.Items.Count - 1)
			End If
			toolStripMenuItemCheckAll.Enabled = True
			AddSearchTextToHistory
			listViewDocs.Select
		Else
			toolStripMenuItemCheckAll.Enabled = False
		End If
		toolStripMenuItemRefresh.Enabled = True
		buttonSearch.Enabled = False
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will add the Search Text to the Search Text history.
	''' </summary>
	Private Sub AddSearchTextToHistory
		If Not history.Contains(comboBoxSearchText.Text) Then
			history.Add(comboBoxSearchText.Text)
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will set the listview sort order based on the column
	''' selected, and then refresh the listview.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListViewDocsColumnClick(sender As Object, e As ColumnClickEventArgs)
		If comboBoxSearchText.Text.Length = 0 Then
			Exit Sub
		End If
		If e.Column = sortColumn Then
			If sortOrder = "asc" Then
				sortOrder = "desc"
			Else
				sortOrder = "asc"
			End If
		Else
			sortOrder = "asc"
		End If
		sortColumn = CByte(e.Column)
		ButtonSearchClick(Me, Nothing)
	End Sub
	
	''' <summary>
	''' This subroutine will query the Document Notes and Document Keywords
	''' of the selected listview item, load the results into the Document
	''' Notes and Document Keywords text boxes, and then enable/disable
	''' document record specific menu/control items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListViewDocsSelectedIndexChanged(sender As Object, e As EventArgs)
		DocumentNotesModifiedCheck
		textBoxDocumentKeywords.Text = Nothing
		textBoxDocumentNotes.Text = Nothing
		documentNotesUndoText = Nothing
		If listViewDocs.SelectedItems.Count = 0 Then
			selectedId = 0	' no item is selected
			toolStripMenuItemSavePDFtoDisk.Enabled = False
			toolStripMenuItemInsertDateTimeIntoDocumentNotes.Enabled = False
			textBoxDocumentNotes.Enabled = False
			textBoxDocumentKeywords.Enabled = False
			buttonDocumentNotesUpdate.Enabled = False
			buttonDocumentNotesRevert.Enabled = False
			Me.Text = "PDFKeeper"
			searchLastTitleText = Me.Text
		Else
			selectedId = CInt(listViewDocs.SelectedItems(0).Text.Trim)
			Me.Cursor = Cursors.WaitCursor
			Dim oDatabaseConnection As New DatabaseConnection
			If oDatabaseConnection.Open(UserSettings.LastUserName, _
					DatabaseConnectionForm.dbPassword, _
					UserSettings.LastDataSource) = 1 Then
				oDatabaseConnection.Dispose
				Me.Cursor = Cursors.Default
				Exit Sub
			End If
			Dim sql As String = "select doc_keywords,doc_notes from pdfkeeper.docs " & _
								"where doc_id = " & selectedId
			Try
				Using oOracleCommand As New OracleCommand(sql, _
					  oDatabaseConnection.oraConnection)
					Using oOracleDataReader As OracleDataReader = _
						  oOracleCommand.ExecuteReader()
						oOracleDataReader.Read()
						If oOracleDataReader.IsDBNull(0) = False Then
							textBoxDocumentKeywords.Text = oOracleDataReader.GetString(0)
							textBoxDocumentKeywords.Enabled = True
						End If
						If oOracleDataReader.IsDBNull(1) = False Then
							textBoxDocumentNotes.Text = oOracleDataReader.GetString(1)
							documentNotesUndoText = oOracleDataReader.GetString(1)
						End If
						oDatabaseConnection.Dispose
						toolStripMenuItemSavePDFtoDisk.Enabled = True
						toolStripMenuItemInsertDateTimeIntoDocumentNotes.Enabled = True
						textBoxDocumentNotes.Enabled = True
						TextBoxDocumentNotesScrollToEnd
						Me.Text = "PDFKeeper - [" & selectedId & "]"
						searchLastTitleText = Me.Text
						Me.Cursor = Cursors.Default
					End Using
				End Using
			Catch ex As OracleException
				oDatabaseConnection.Dispose
				Me.Cursor = Cursors.Default
				MessageBoxWrapper.ShowError(ex.Message.ToString())
				Exit Sub
			End Try
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will open the PDF document for the selected ID.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListViewDocsMouseDoubleClick(sender As Object, e As MouseEventArgs)
		Me.Cursor = Cursors.WaitCursor
		listViewDocs.SelectedItems(0).Checked = False
		Dim pdfFile As String
		pdfFile = Path.Combine(CacheDir, "pdfkeeper" & selectedId & ".pdf")
		If PdfFileTask.RetrieveFromDatabase(selectedId, pdfFile) = 0 Then
			Try
				System.IO.File.Encrypt(pdfFile)
			Catch ex As IOException
			End Try
						
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
				If WindowFinder.Exists(titleBarText, True) = False Then
					processSearchPdfViewer.StartInfo.Arguments = chr(34) & _
																 pdfFile & _
													   	   		 chr(34)
					processSearchPdfViewer.Start
					processSearchPdfViewer.WaitForInputIdle(15000)
				End If
			End If
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will toggle "Uncheck All" and
	''' "Delete Checked Documents" menu items based on if any listview items
	''' are checked or none are checked and update the status bar with the
	''' number of checked list view items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListViewDocsItemChecked(sender As Object, e As ItemCheckedEventArgs)
		If listViewDocs.CheckedItems.Count > 0 Then
			toolStripMenuItemUncheckAll.Enabled = True
			toolStripMenuItemDeleteCheckedDocuments.Enabled = True
		Else
			toolStripMenuItemUncheckAll.Enabled = False
			toolStripMenuItemDeleteCheckedDocuments.Enabled = False
		End If
		UpdateListCountStatusBar
	End Sub
	
	''' <summary>
	''' This subroutine is a workaround to the listview control forcing the
	''' far left column to be left justified.  However, right justification is
	''' required to properly align the ID's in the listview.
	''' </summary>
	Private Sub RightJustifyListViewDocIds
		Dim maxLength As Integer = 0
		Dim oListViewItem As ListViewItem
		For Each oListViewItem In listViewDocs.Items
			If oListViewItem.Text.Length > maxLength Then
				maxLength = oListViewItem.Text.Length
			End If
		Next
		If maxLength > 0 Then
			Dim diff As Integer
			For Each oListViewItem In listViewDocs.Items
				If oListViewItem.Text.Length < maxLength Then
					diff = maxLength - oListViewItem.Text.Length
					oListViewItem.Text = _
					oListViewItem.Text.PadLeft(maxLength + diff)
				End If
			Next
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will resize each listview column by auto-adjusting each
	''' column to fit the header and contents, then resize columns 1-3 to a
	''' maximum width of 259 each, only if the total width of all columns
	''' exceeds the width of the form.
	''' </summary>
	Private Sub ResizeListViewColumns
		If listViewDocs.Items.Count > 0 Then
			listViewDocs.AutoResizeColumns( _
				ColumnHeaderAutoResizeStyle.ColumnContent)
			Dim totalColWidth As Integer
			totalColWidth = 0
			For j = 0 To 4
				If j > 0 And j < 4 Then
					totalColWidth = totalColWidth + _
						listViewDocs.Columns(j).Width
				End If
			Next
			If totalColWidth > Me.Width Then
				For k = 1 To 3
					If listViewDocs.Columns(k).Width > 259 Then
						listViewDocs.Columns(k).Width = 259
					End If
				Next
			End If
		Else
			listViewDocs.AutoResizeColumns( _
				ColumnHeaderAutoResizeStyle.HeaderSize)
		End If
	End Sub
		
	''' <summary>
	''' This subroutine will trim a leading space from the text in the
	''' Document Notes text box, enable the "Print Document Notes" menu item
	''' if the Document Notes text box contains text, and enable the Update
	''' and Revert buttons if the text in the Document Notes text box was
	''' modified.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TextBoxDocumentNotesTextChanged(sender As Object, e As EventArgs)
		textBoxDocumentNotes.Text = textBoxDocumentNotes.Text.TrimStart
		If textBoxDocumentNotes.Text.Length > 0 Then
			toolStripMenuItemPrintDocumentNotes.Enabled = True
		Else
			toolStripMenuItemPrintDocumentNotes.Enabled = False
		End If
		If textBoxDocumentNotes.Modified Then
			buttonDocumentNotesUpdate.Enabled = True
			buttonDocumentNotesRevert.Enabled = True
		Else
			buttonDocumentNotesUpdate.Enabled = False
			buttonDocumentNotesRevert.Enabled = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will update the Document Notes for selectedId in the
	''' database with the contents of the Document Notes text box, and then
	''' disable the Update and Revert buttons.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonDocumentNotesUpdateClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		textBoxDocumentNotes.Text = textBoxDocumentNotes.Text.Trim
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open(UserSettings.LastUserName, _
				DatabaseConnectionForm.dbPassword, _
				UserSettings.LastDataSource) = 1 Then
			oDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		Dim sql As String = "update pdfkeeper.docs set " & _
			 				"doc_notes =q'[" & textBoxDocumentNotes.Text & _
			 				"]',doc_dummy = '' where doc_id = " & selectedId
		Using oOracleCommand As New OracleCommand(sql, _
			  oDatabaseConnection.oraConnection)
			Try
				oOracleCommand.ExecuteNonQuery
			Catch ex As OracleException
				Me.Cursor = Cursors.Default
				MessageBoxWrapper.ShowError(ex.Message.ToString())
				Exit Sub
			Finally
				oDatabaseConnection.Dispose
			End Try
		End Using
		documentNotesUndoText = textBoxDocumentNotes.Text
		buttonDocumentNotesUpdate.Enabled = False
		buttonDocumentNotesRevert.Enabled = False
		TextBoxDocumentNotesScrollToEnd
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will revert all changes made to the Document Notes
	''' text box, and then disable the Update and Revert buttons.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonDocumentNotesRevertClick(sender As Object, e As EventArgs)
		textBoxDocumentNotes.Text = documentNotesUndoText
		buttonDocumentNotesUpdate.Enabled = False
		buttonDocumentNotesRevert.Enabled = False
		TextBoxDocumentNotesScrollToEnd
	End Sub
	
	''' <summary>
	''' This subroutine will select the Document Notes text box, and then move
	''' the cursor to the end of the text.
	''' </summary>
	Private Sub TextBoxDocumentNotesScrollToEnd
		textBoxDocumentNotes.Select
		textBoxDocumentNotes.Select(textBoxDocumentNotes.Text.Length,0)
		textBoxDocumentNotes.ScrollToCaret
	End Sub
	
	''' <summary>
	''' This subroutine will prompt to save Document Notes if unsaved data
	''' exists and either Update or Revert the changes based on the user
	''' response.
	''' </summary>
	Private Sub DocumentNotesModifiedCheck
		If buttonDocumentNotesUpdate.Enabled Then
			tabControlDocNotesKeywords.SelectTab(0)
			If MessageBoxWrapper.ShowQuestion( _
					MainForm_Strings.DocumentNotesSavePrompt) = 6 Then ' Yes
				ButtonDocumentNotesUpdateClick(Me, Nothing)
			Else
				ButtonDocumentNotesRevertClick(Me, Nothing)
			End If
		End If
	End Sub
	
	#End Region
	
	#Region "Document Capture"
	
	''' <summary>
	''' This subroutine will fill the "Document Capture Queue" list box with
	''' the absolute pathname for each PDF document in the Capture folder.
	''' </summary>
	Private Sub FillDocCaptureQueueList
		Me.Cursor = Cursors.WaitCursor
		listBoxDocCaptureQueue.Items.Clear
		Dim files As String()
		files = Directory.GetFiles(CaptureDir, "*.pdf", _
				SearchOption.AllDirectories)
		For Each oFile In files
			listBoxDocCaptureQueue.Items.Add(oFile)
		Next
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will read the information properties for the selected
	''' PDF document and update the form.  When a PDF document is selected,
	''' the "Document Capture Queue" list box is disabled.  If the selected PDF
	''' document is protected by an OWNER password, a password prompt will be
	''' displayed.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListBoxDocCaptureQueueSelectedIndexChanged(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		capturePdfFile = CStr(listBoxDocCaptureQueue.SelectedItem)
		If Not capturePdfFile = Nothing Then
			captureModPdfFile = Path.Combine(CaptureTempDir, _
								Path.GetFileName(capturePdfFile))
			lastPdfDocumentCheckResult = PdfFileTask.SecurityCheck(capturePdfFile)
			If lastPdfDocumentCheckResult = 1 Then
				If PdfOwnerPasswordForm.ShowDialog() = _
						Windows.Forms.DialogResult.Cancel Then
					Me.Cursor = Cursors.Default
					Exit Sub
				End If
			ElseIf lastPdfDocumentCheckResult = 2
				Me.Cursor = Cursors.Default
				Exit Sub
			End If
			Dim oPdfProperties As PdfProperties = Nothing
			If lastPdfDocumentCheckResult = 0 Then
				Dim oPdfProperties1 As New PdfProperties(capturePdfFile)
				oPdfProperties = oPdfProperties1
			ElseIf lastPdfDocumentCheckResult = 1 Then
				Dim oPdfProperties2 As New PdfProperties(capturePdfFile, _
									   PdfOwnerPasswordForm.ownerPassword)
				oPdfProperties = oPdfProperties2
			End If
			If oPdfProperties.Read = 0 Then
				textBoxPDFDocument.Text = capturePdfFile
				buttonView.Enabled = True
				textBoxTitle.Text = oPdfProperties.Title
				
				' If the title is blank, default to the filename without the
				' PDF extension.
				If oPdfProperties.Title Is Nothing Then
					textBoxTitle.Text = Path.GetFileNameWithoutExtension( _
						capturePdfFile)
				End If
				
				textBoxTitle.Enabled = True
				textBoxTitle.Select
				listBoxDocCaptureQueue.Enabled = False
				comboBoxAuthor.Text = oPdfProperties.Author
				comboBoxAuthor.Enabled = True
				comboBoxSubject.Text = oPdfProperties.Subject
				comboBoxSubject.Enabled = True
				textBoxKeywords.Text = oPdfProperties.Keywords
				textBoxKeywords.Enabled = True
				buttonDeselect.Enabled = True
				CaptureComboBoxTextChanged
			Else
				MessageBoxWrapper.ShowError(String.Format( _
								CultureInfo.CurrentCulture, _
								MainForm_Strings.UnableRead, capturePdfFile))
			End If
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
 	''' This subroutine will call the CaptureViewPdf subroutine to display the
 	''' PDF document in a restricted Sumatra PDF process.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonViewClick(sender As Object, e As EventArgs)
		CaptureViewPdf(capturePdfFile)
	End Sub
	
	''' <summary>
	''' This subroutine will fill the Author combo box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ComboBoxAuthorDropDown(sender As Object, e As EventArgs)
		FillCaptureComboBox("Author")
	End Sub
	
	''' <summary>
	''' This subroutine will fill the Subject combo box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ComboBoxSubjectDropDown(sender As Object, e As EventArgs)
		FillCaptureComboBox("Subject")
	End Sub
	
	''' <summary>
	''' This subroutine will fill "comboBoxName" with either authors or
	''' subjects for the selected/specified author.  "comboBoxName" can be
	''' "Author" or "Subject.
	''' </summary>
	''' <param name="comboBoxName"></param>
	Private Sub FillCaptureComboBox(ByVal comboBoxTitle As String)
		If comboBoxTitle = "Author" Then
			comboBoxAuthor.Items.Clear
		ElseIf comboBoxTitle = "Subject" Then
			comboBoxSubject.Items.Clear
		Else
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open(UserSettings.LastUserName, _
				DatabaseConnectionForm.dbPassword, _
				UserSettings.LastDataSource) = 1 Then
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
			Me.Cursor = Cursors.Default
		Catch ex As OracleException
			Me.Cursor = Cursors.Default
			MessageBoxWrapper.ShowError(ex.Message.ToString())
		Finally
			oDatabaseConnection.Dispose
		End Try
	End Sub
		
	''' <summary>
	''' This subroutine will trim the leading space from the text in all of
	''' the Document Capture text and combo boxes, and enables the Save button
	''' if the length of the Title, Author, and Subject is greater than 0.
	''' </summary>
	Private Sub CaptureComboBoxTextChanged
		toolStripStatusLabelMessage.Text = Nothing
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
		buttonPreview.Enabled = False
		buttonUpload.Enabled = False
	End Sub
	
	''' <summary>
	''' This subroutine will call the TerminateCapturePdfViewer subroutine;
	''' save the information properties to the new PDF document; and clear
	'''	the PDF password secure string, if it was specified.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonSaveClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		DisableCaptureControls(False)
		TerminateCapturePdfViewer
		toolStripStatusLabelMessage.Text = MainForm_Strings.CaptureSaving
		Application.DoEvents
		Dim oPdfProperties As PdfProperties = Nothing
		If lastPdfDocumentCheckResult = 0 Then
			Dim oPdfProperties1 As New PdfProperties(capturePdfFile, _
													 captureModPdfFile)
			oPdfProperties = oPdfProperties1
		ElseIf lastPdfDocumentCheckResult = 1 Then
			Dim oPdfProperties2 As New PdfProperties(capturePdfFile, _
								   captureModPdfFile, _
								   PdfOwnerPasswordForm.ownerPassword)
			oPdfProperties = oPdfProperties2
		End If
		oPdfProperties.Title = textBoxTitle.Text.Trim
		oPdfProperties.Author = comboBoxAuthor.Text.Trim
		oPdfProperties.Subject = comboBoxSubject.Text.Trim
		oPdfProperties.Keywords = textBoxKeywords.Text.Trim
		If oPdfProperties.Write = 0 Then
			buttonSave.Enabled = False
			buttonPreview.Enabled = True
			buttonUpload.Enabled = True
			toolStripStatusLabelMessage.Text = MainForm_Strings.CaptureSaved
		Else
			buttonPreview.Enabled = False
			buttonUpload.Enabled = False
			toolStripStatusLabelMessage.Text = Nothing
		End If
		captureLastStatusMessage = toolStripStatusLabelMessage.Text
		EnableCaptureControls(False)
		If lastPdfDocumentCheckResult = 1 Then
			PdfOwnerPasswordForm.ownerPassword.Clear
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will call the CaptureViewPdf subroutine to display the
 	''' modified PDF document in a restricted Sumatra PDF process.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonPreviewClick(sender As Object, e As EventArgs)
		CaptureViewPdf(captureModPdfFile)
	End Sub
	
	''' <summary>
	''' This subroutine will call the TerminateCapturePdfViewer subroutine,
	''' upload the modified PDF document to the database, delete the original
	''' PDF document to the recycle bin, call the ClearCaptureSelection and
	''' FillDocCaptureQueueList subroutines.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonUploadClick(sender As Object, e As EventArgs)
		If MessageBoxWrapper.ShowQuestion(String.Format( _
				CultureInfo.CurrentCulture, _
				MainForm_Strings.UploadPrompt, captureModPdfFile)) = 6 Then ' Yes
			Me.Cursor = Cursors.WaitCursor
			DisableCaptureControls(True)
			TerminateCapturePdfViewer
			toolStripStatusLabelMessage.Text = MainForm_Strings.CaptureUploading
			Application.DoEvents
			If PdfFileTask.UploadToDatabase(captureModPdfFile) = 0 Then
				toolStripStatusLabelMessage.Text = Nothing
				Application.DoEvents
				FileTask.Delete(capturePdfFile, True)
				ClearCaptureSelection
				FillDocCaptureQueueList
			Else
				EnableCaptureControls(True)
			End If
			Me.Cursor = Cursors.Default
		End If
	End Sub
	
	''' <summary>
	''' This subroutine is to be called at the start of a Save or Upload.  It
	''' will disable all text and combo boxes, and the appropriate buttons
	''' based on the action being performed.  If performing a Save, "uploading"
	''' should be False; if performing an upload, "uploading" should be True.
	''' </summary>
	''' <param name="uploading"></param>
	Private Sub DisableCaptureControls(uploading As Boolean)
		buttonView.Enabled = False
		textBoxTitle.Enabled = False
		comboBoxAuthor.Enabled = False
		comboBoxSubject.Enabled = False
		textBoxKeywords.Enabled = False
		buttonSave.Enabled = False
		If uploading Then
			buttonPreview.Enabled = False
			buttonUpload.Enabled = False
		End If
		buttonDeselect.Enabled = False
	End Sub
	
	''' <summary>
	''' This subroutine is to be called at the end of a Save or when an Upload
	''' has failed.  It will disable all text and combo boxes, and the
	''' appropriate buttons based on the action being performed.  If performing
	''' a Save, "uploading" should be False; if performing an upload,
	''' "uploading" should be True.
	''' </summary>
	''' <param name="uploading"></param>
	Private Sub EnableCaptureControls(uploading As Boolean)
		buttonView.Enabled = True
		textBoxTitle.Enabled = True
		comboBoxAuthor.Enabled = True
		comboBoxSubject.Enabled = True
		textBoxKeywords.Enabled = True
		If uploading Then
			buttonPreview.Enabled = True
			buttonUpload.Enabled = True
		End If
		buttonDeselect.Enabled = True
	End Sub
	
	''' <summary>
	''' This subroutine will perform the following, if the user selects "Yes"
	''' at the prompt: call the TerminateCapturePdfViewer and
	''' ClearCaptureSelection subroutines; and dispose the PDF password secure
	'''	string, if it was specified.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonDeselectClick(sender As Object, e As EventArgs)
		If MessageBoxWrapper.ShowQuestion( _
				MainForm_Strings.DeselectPrompt) = 6 Then ' Yes
			Me.Cursor = Cursors.WaitCursor
			TerminateCapturePdfViewer
			ClearCaptureSelection
			If lastPdfDocumentCheckResult = 1 Then
				PdfOwnerPasswordForm.ownerPassword.Clear
			End If
			Me.Cursor = Cursors.Default
		End If
	End Sub
		
	''' <summary>
	''' This subroutine will display the PDF document "file" in the restricted
	''' Sumatra PDF viewer.  If a Sumatra PDF process object does exist, it will
	''' be terminated.
	''' </summary>
	''' <param name="file"></param>
	Private Sub CaptureViewPdf(file As String)
		Me.Cursor = Cursors.WaitCursor

		' Get the title of the PDF document and open it.
		Dim oPdfProperties As New PdfProperties(file)
		If oPdfProperties.Read = 0 Then
			Dim titleBarText As String
			If oPdfProperties.Title = Nothing Then
				titleBarText = Path.GetFileName(file) & " - SumatraPDF"
			Else
				titleBarText = Path.GetFileName(file) & " - [" & _
								   oPdfProperties.Title & "] - SumatraPDF"
			End If
			If WindowFinder.Exists(titleBarText, True) = False Then
				TerminateCapturePdfViewer
				processCapturePdfViewer.StartInfo.Arguments = "-restrict " & _
												  	chr(34) & file & chr(34)
				processCapturePdfViewer.Start
				processCapturePdfViewer.WaitForInputIdle(15000)
			End If
		End If
		Me.Cursor = Cursors.Default
	End Sub
		
	''' <summary>
	''' This subroutine will terminate the restricted Sumatra PDF process
	''' object.
	''' </summary>
	Private Sub TerminateCapturePdfViewer
		Try
			processCapturePdfViewer.Kill
		Catch ex As InvalidOperationException
		End Try
	End Sub
	
	''' <summary>
	''' This subroutine will clear the "Document Capture" form controls, enable
	''' the "Document Capture Queue" list box, and delete the modified copy of
	''' the selected PDF document.
	''' </summary>
	Private Sub ClearCaptureSelection
		toolStripStatusLabelMessage.Text = Nothing
		captureLastStatusMessage = Nothing
		textBoxPDFDocument.Text = Nothing
		buttonView.Enabled = False
		textBoxTitle.Text = Nothing
		textBoxTitle.Enabled = False
		comboBoxAuthor.Text = Nothing
		comboBoxAuthor.Enabled = False
		comboBoxSubject.Text = Nothing
		comboBoxSubject.Enabled = False
		textBoxKeywords.Text = Nothing
		textBoxKeywords.Enabled = False
		buttonSave.Enabled = False
		buttonPreview.Enabled = False
		buttonUpload.Enabled = False
		buttonDeselect.Enabled = False
		listBoxDocCaptureQueue.Enabled = True
		FileTask.Delete(captureModPdfFile, False)
	End Sub
	
	#End Region
	
	#Region "Tab Control, Title, and Status Bar"
	
	''' <summary>
	''' This subroutine will update the form title bar text, select the last
	''' selected search list view item, enable/disable menu items, and set the
	''' status bar text to the last message displayed for the selected tab.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TabControlMainSelectedIndexChanged(sender As Object, e As EventArgs)
		If tabControlMain.SelectedIndex = 0 Then	' "Document Search" tab
			Me.Text = searchLastTitleText
			If selectedId > 0 Then
				listViewDocs.Focus
				listViewDocs.SelectedItems(0).Text = CStr(selectedId)
				toolStripMenuItemSavePDFtoDisk.Enabled = True
			Else
				toolStripMenuItemSavePDFtoDisk.Enabled = False
			End If
			If textBoxDocumentNotes.Text.Length > 0 Then
				toolStripMenuItemPrintDocumentNotes.Enabled = True
			Else
				toolStripMenuItemPrintDocumentNotes.Enabled = False
			End If
			toolStripMenuItemEdit.Enabled = True
			toolStripMenuItemView.Enabled = True
			toolStripMenuItemHtmlConverter.Enabled = False
			toolStripMenuItemCaptureFolder.Enabled = False
			toolStripStatusLabelMessage.Text = searchLastStatusMessage
		Else	' "Document Capture" tab
			Me.Text = "PDFKeeper"
			toolStripMenuItemSavePDFtoDisk.Enabled = False
			toolStripMenuItemPrintDocumentNotes.Enabled = False
			toolStripMenuItemEdit.Enabled = False
			toolStripMenuItemView.Enabled = False
			Dim wkhtmltopdf As String = CStr(Registry.GetValue( _
				"HKEY_LOCAL_MACHINE\SOFTWARE\wkhtmltopdf", "PdfPath", Nothing))
			If Not wkhtmltopdf = Nothing Then
				If System.IO.File.Exists(wkhtmltopdf) Then
					toolStripMenuItemHtmlConverter.Enabled = True
				End If	
			End If
			toolStripMenuItemCaptureFolder.Enabled = True
			toolStripStatusLabelMessage.Text = captureLastStatusMessage
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will update the status bar with the number of items in
	''' the listview.
	''' </summary>
	Private Sub UpdateListCountStatusBar
		toolStripStatusLabelMessage.Text = String.Format( _
										   CultureInfo.CurrentCulture, _
										   MainForm_Strings.ListViewCountChecked, _
										   listViewDocs.Items.Count, _
										   listViewDocs.CheckedItems.Count)
		searchLastStatusMessage = toolStripStatusLabelMessage.Text
	End Sub
	
	''' <summary>
	''' This subroutine will open the project site using the default browser.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub StatusStripItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
		If e.ClickedItem.Text = MainForm_Strings.NewerVersionAvailable Then
			Me.Cursor = Cursors.WaitCursor
			Process.Start(ConfigurationManager.AppSettings("ProjectSiteUrl"))
			Me.Cursor = Cursors.Default
		End If
	End Sub
		
	''' <summary>
	''' This subroutine will select the "Document Capture" tab.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolStripStatusLabelCapturedClick(sender As Object, e As EventArgs)
		tabControlMain.SelectTab(1)
	End Sub
	
	#End Region
	
	#Region "Components"
	
	''' <summary>
	''' This subroutine will process the Capture folder by converting any XPS
	''' documents to PDF; display or hide the "Document Capture" status bar
	''' icon; refresh the "Document Capture Queue" list box, if the folder
	''' change flag is set to True; delete all empty sub-folders; and display
	''' or hide the "Document Capture Deny" status bar icon.  To maintain
	''' synchronization, the timer is stopped during the execution of this
	''' subroutine.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TimerCaptureCheckTick(sender As Object, e As EventArgs)
		timerCaptureCheck.Stop
		FolderTask.ConvertAllXpsFilesToPdf(CaptureDir)
		If FolderTask.CountOfFiles(CaptureDir, "pdf") > 0 Then
			toolStripStatusLabelCaptured.Visible = True
		Else
			toolStripStatusLabelCaptured.Visible = False
		End If
		If documentCaptureFolderChanged Then
			documentCaptureFolderChanged = False
			FillDocCaptureQueueList
		End If
		FolderTask.DeleteAllEmptySubfolders(CaptureDir)
		timerCaptureCheck.Start
	End Sub
	
	''' <summary>
	''' This subroutine will process the Direct Upload folder on a worker
	''' thread by uploading all PDF documents in each configured folder,
	''' including subfolders.  To maintain synchronization, the timer is
	''' stopped during the execution of this subroutine.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TimerDirectUploadTick(sender As Object, e As EventArgs)
		toolStripMenuItemDirectUploadConfig.Enabled = False
		timerDirectUpload.Stop
		backgroundWorkerDirectUpload.RunWorkerAsync
	End Sub
	
	''' <summary>
	''' This subroutine will check if an update is available and automatically
	''' trigger the RunWorkerCompleted event.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerUpdateCheckDoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
		updateAvailable = Product.UpdateAvailable
	End Sub
	
	''' <summary>
	''' This subroutine will create a link on the status bar if an update is
	''' available.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerUpdateCheckRunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If updateAvailable Then
			toolStripStatusLabelUpdateStatus.Text = _
				MainForm_Strings.NewerVersionAvailable
			toolStripStatusLabelUpdateStatus.ForeColor = _
				 System.Drawing.SystemColors.ActiveCaption
			toolStripStatusLabelUpdateStatus.IsLink = True
		Else
			toolStripStatusLabelUpdateStatus.Text = Nothing
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will create missing Direct Upload folders; process the
	''' Direct Upload folder by uploading all PDF documents in each configured
	''' folder, including subfolders; delete any subfolders inside of each
	''' configured folder; and automatically trigger the RunWorkerCompleted
	''' event.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerDirectUploadDoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
		DirectUpload.CreateMissingFolders
		If DirectUpload.CountOfPdfFiles > 0 Then
			toolStripStatusLabelUploading.Visible = True
			Application.DoEvents
			DirectUpload.UploadAllPdfFiles
			toolStripStatusLabelUploading.Visible = False
			Application.DoEvents
		End If
		DirectUpload.DeleteAllEmptySubfolders
	End Sub
	
	''' <summary>
	''' This subroutine will start the Direct Upload timer and enable the
	''' "Direct Upload Configuration" menu item.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerDirectUploadRunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
		timerDirectUpload.Start
		toolStripMenuItemDirectUploadConfig.Enabled = True
	End Sub
		
	''' <summary>
	''' This subroutine will execute when a file is added to the Capture
	''' folder.  If the file added is a PDF document, then set the Capture
	''' folder changed flag to True.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub FileSystemWatcherDocumentCaptureCreated(sender As Object, e As FileSystemEventArgs)
		If Path.GetExtension(e.FullPath) = ".pdf" Then
			Me.Cursor = Cursors.WaitCursor
			FileTask.WaitForFileCreation(e.FullPath)
			If listBoxDocCaptureQueue.FindStringExact(e.FullPath) = -1 Then
				documentCaptureFolderChanged = True
			End If
			Me.Cursor = Cursors.Default
		End If
	End Sub

	''' <summary>
	''' This subroutine will set the Capture folder changed flag to True and
	''' clear the form if the PDF document was selected.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub FileSystemWatcherDocumentCaptureDeleted(sender As Object, e As FileSystemEventArgs)
		If Not listBoxDocCaptureQueue.FindStringExact(e.FullPath) = -1 Then
			documentCaptureFolderChanged = True
			If textBoxPDFDocument.Text = e.FullPath Then
				MessageBoxWrapper.ShowInformation( _
					MainForm_Strings.SelectedDocDeleted)
				Me.Cursor = Cursors.WaitCursor
				TerminateCapturePdfViewer
				ClearCaptureSelection
				Me.Cursor = Cursors.Default
			End If
		End If
	End Sub

	''' <summary>
	''' This subroutine will set the Capture folder changed flag to True and if
	''' the renamed PDF document was selected, clear the form and select the
	''' renamed PDF document.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub FileSystemWatcherDocumentCaptureRenamed(sender As Object, e As RenamedEventArgs)
		If Not listBoxDocCaptureQueue.FindStringExact(e.OldFullPath) = -1 Then
			documentCaptureFolderChanged = False
			FillDocCaptureQueueList
			If textBoxPDFDocument.Text = e.OldFullPath Then
				MessageBoxWrapper.ShowInformation( _
					MainForm_Strings.SelectedDocRenamed)
				Me.Cursor = Cursors.WaitCursor
				TerminateCapturePdfViewer
				ClearCaptureSelection
				listBoxDocCaptureQueue.SetSelected( _
							listBoxDocCaptureQueue.FindStringExact(e.FullPath), _
							True)
				ListBoxDocCaptureQueueSelectedIndexChanged(Me,Nothing)
				Me.Cursor = Cursors.Default
			End If
		End If
	End Sub
	
	''' <summary>
	''' This subroutine is the "Document Capture" folder watcher error event
	''' handler that will display the error exception message and enable the
	''' folder watcher.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub FileSystemWatcherDocumentCaptureError(sender As Object, e As ErrorEventArgs)
		MessageBoxWrapper.ShowError(e.GetException().ToString)
		fileSystemWatcherDocumentCapture.EnableRaisingEvents = True
	End Sub

	#End Region
	
	#Region "Form Closing"
	
	''' <summary>
	''' This subroutine will allow this form to close if no background worker
	''' thread's or timers are busy and no additional application form's are
	''' open.  If a PDF document is selected on the "Document Capture" tab,
	''' then prompt the user before closing the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub MainFormClosing(sender As Object, e As FormClosingEventArgs)
		If BackgroundWorkersBusy Or My.Application.OpenForms.Count > 1 _
								 Or timerCaptureCheck.Enabled = False _
								 Or timerDirectUpload.Enabled = False Then
			e.Cancel = True
		Else
			If Not textBoxPDFDocument.Text = Nothing Then
				If MessageBoxWrapper.ShowQuestion( _
					MainForm_Strings.FormClosingPromptSelected) = 6 Then ' Yes
					Me.Cursor = Cursors.WaitCursor
					TerminateCapturePdfViewer
					ClearCaptureSelection
					Me.Cursor = Cursors.Default
				Else
					e.Cancel = True
				End If
			End If
		End If
	End Sub
	
	''' <summary>
	''' This function will check if any background worker thread's are busy.
	''' </summary>
	''' <returns>True or False</returns>
	Function BackgroundWorkersBusy() As Boolean
		If backgroundWorkerUpdateCheck.IsBusy Or _
				backgroundWorkerDirectUpload.IsBusy Then
			Return True
		Else
			Return False
		End If
	End Function
	
	''' <summary>
	''' This subroutine will call subroutines to check for unsaved Document
	''' Notes, delete cached PDF files, dispose the database password string,
	'''	save the form size and postion, and then save the user settings.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub MainFormFormClosed(sender As Object, e As FormClosedEventArgs)
		DocumentNotesModifiedCheck
		FolderTask.DeletePdfKeeperCreatedPdfFiles(CacheDir)
		DatabaseConnectionForm.dbPassword.Dispose
		SaveFormPosition
		UserProfileFoldersTask.DeleteDocumentCaptureShortcuts
		UserProfileFoldersTask.DeleteDirectUploadShortcut
		Dim oUserSettings As New UserSettings
		oUserSettings.Write
	End Sub
	
	''' <summary>
	''' This subroutine will save the form size and postion.
	''' </summary>
	Private Sub SaveFormPosition
		If Me.WindowState.ToString = "Normal" Then
			UserSettings.FormPositionTop = Me.Top.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionLeft = Me.Left.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionHeight = Me.Height.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionWidth = Me.Width.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionWindowState = CStr(0)
		End If
		If Me.WindowState.ToString = "Maximized" Then
			UserSettings.FormPositionWindowState = CStr(2)
		End If
	End Sub
	
	#End Region
End Class
