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

Public Partial Class MainForm
	Dim updateAvailable As Boolean
	Dim history As New ArrayList
	Dim sortColumn As Byte
	Dim sortOrder As string = "asc"
	Dim selectedId As Integer
	Dim documentNotesUndoText As String
	Dim uploadQueue As New Queue
	Friend Shared formEditorQueue As New Queue
	Friend Shared infoPropEditFormOpen As Boolean
	Friend Shared watcherEditorQueue As New Queue
	Friend Shared infoPropEditWatcherOpen As Boolean
	
	Public Sub New()
		Me.InitializeComponent()
		SetFormPosition
		StartUpdateCheck
		fileSystemWatcherEditor.EnableRaisingEvents = False
		ToggleFolderWatcher
	End Sub
	
	#Region "Form Loading Subroutines"
	
	''' <summary>
	''' This subroutine will set the form size and position based on the values
	''' retrieved from the registry.  A default will be used for any value that
	''' is NUL.
	''' </summary>
	Private Sub SetFormPosition
		Me.Top = UserSettings.FormPositionTop
		Me.Left = UserSettings.FormPositionLeft
		Me.Height = UserSettings.FormPositionHeight
		Me.Width = UserSettings.FormPositionWidth
		Me.WindowState = UserSettings.FormPositionWindowState
	End Sub
	
	''' <summary>
	''' This subroutine will start the update check thread.
	''' </summary>
	Private Sub StartUpdateCheck
		If UserSettings.UpdateCheck = 1 Then
			toolStripStatusLabelUpdateStatus.Text = _
				"Checking for a newer version of PDFKeeper..."
			BackgroundWorkerUpdateCheck.RunWorkerAsync
		End If
	End Sub
	
	#End Region
	
	#Region "Form Menu Events"
	
	''' <summary>
	''' This subroutine will prompt the user for the folder and file name to
	''' save the PDF as, and then query the database to get the PDF document
	''' for the selected ID.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub SavePDFtoDiskToolStripMenuItemClick(sender As Object, e As EventArgs)
		SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
		
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
		If Not pdfFileInfo.Extension.ToUpper(CultureInfo.InvariantCulture) = ".PDF" Then
			pdfFile = pdfFile & ".pdf"
		End If
		Dim oPdfFile As New PdfFile(pdfFile)
		If oPdfFile.Retrieve(selectedId) = 0 Then
			Me.Cursor = Cursors.Default
			Dim oMessageDialog As New MessageDialog(pdfFile & " has been saved.")
			oMessageDialog.DisplayInformation
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
	Private Sub PrintDocumentNotesToolStripMenuItemClick(sender As Object, e As EventArgs)
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
	''' This subroutine will exit the form and the application.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ExitToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Close
	End Sub

	''' <summary>
	''' This subroutine will select the "Document Notes" tab and append a
	''' Date/Time stamp with the database user name to the existing text in
	''' the Document Notes text box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InsertDateTimeIntoDocumentNotesToolStripMenuItemClick(sender As Object, e As EventArgs)
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
	Private Sub CheckAllToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oListViewItem As ListViewItem
		For Each oListViewItem In listViewDocs.Items
			oListViewItem.Checked = True
		Next
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will uncheck all listview items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub UncheckAllToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oListViewItem As ListViewItem
		For Each oListViewItem In listViewDocs.Items
			oListViewItem.Checked = False
		Next
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will delete the database records for all checked
	''' listview items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DeleteCheckedDocumentsToolStripMenuItemClick(sender As Object, e As EventArgs)
		DocumentNotesModifiedCheck
		Dim oMessageDialog1 As New MessageDialog("Delete all checked " & _
											     "documents?")
		If oMessageDialog1.DisplayQuestion = 7 Then	' "No" was selected.
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open = 1 Then
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
					Dim oMessageDialog2 As New _
						 MessageDialog(ex.Message.ToString())
					oMessageDialog2.DisplayError
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
	''' This subroutine will call the PDF document Information Properties
	''' Editor.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InformationPropertiesEditorToolStripMenuItemClick(sender As Object, e As EventArgs)
		SelectAndProcessPdfFiles("INFO_EDIT")
	End Sub
	
	''' <summary>
	''' This subroutine will call the PDF document upload.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub UploadToolStripMenuItemClick(sender As Object, e As EventArgs)
		SelectAndProcessPdfFiles("UPLOAD")
	End Sub
	
	''' <summary>
	''' This subroutine will enable/disable the "Information Properties Editor"
	''' Folder Watcher sub menus.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InformationPropertiesEditorWatcherToolStripMenuItemClick(sender As Object, e As EventArgs)
		If UserSettings.InfoPropEditWatcherFolder = Nothing Then
			selectFolderInfoPropEditWatcherToolStripMenuItem.Enabled = True
			enableInfoPropEditWatcherToolStripMenuItem.Enabled = False
			disableInfoPropEditWatcherToolStripMenuItem.Enabled = False
		Else
			If fileSystemWatcherEditor.EnableRaisingEvents = True Then
				selectFolderInfoPropEditWatcherToolStripMenuItem.Enabled = False
				enableInfoPropEditWatcherToolStripMenuItem.Enabled = False
				disableInfoPropEditWatcherToolStripMenuItem.Enabled = True
			Else
				selectFolderInfoPropEditWatcherToolStripMenuItem.Enabled = True
				enableInfoPropEditWatcherToolStripMenuItem.Enabled = True
				disableInfoPropEditWatcherToolStripMenuItem.Enabled = False
			End If
		End If
	End Sub
		
	''' <summary>
	''' This subroutine will display a folder browser dialog for the user to
	''' select the folder to be watched by the Information Properties Editor
	''' folder watcher.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub SelectFolderInfoPropEditWatcherToolStripMenuItemClick(sender As Object, e As EventArgs)
		folderBrowserDialog.SelectedPath = _
			   UserSettings.InfoPropEditWatcherFolder
		folderBrowserDialog.Description = "Select the folder to be " & _
										  "watched that will call the " & _
										  "Information Properties Editor:"
		If folderBrowserDialog.ShowDialog() = 1 Then
			UserSettings.InfoPropEditWatcherFolder = _
						 folderBrowserDialog.SelectedPath
			fileSystemWatcherEditor.Path = _
					   UserSettings.InfoPropEditWatcherFolder
			Dim oMessageDialog As New MessageDialog("Enable the " & _
													"Information " & _
													"Properties Editor " & _
													"Caller folder watcher?")
			If oMessageDialog.DisplayQuestion = 6 Then
				EnableInfoPropEditWatcherToolStripMenuItemClick(Me,	Nothing)
			End If
		End If
	End Sub
		
	''' <summary>
	''' This subroutine will enable the Information Properties Editor folder
	''' watcher.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub EnableInfoPropEditWatcherToolStripMenuItemClick(sender As Object, e As EventArgs)
		UserSettings.InfoPropEditWatcherEnabled = 1
		ToggleFolderWatcher
	End Sub
	
	''' <summary>
	''' This subroutine will disable the Information Properties Editor folder
	''' watcher.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DisableInfoPropEditWatcherToolStripMenuItemClick(sender As Object, e As EventArgs)
		UserSettings.InfoPropEditWatcherEnabled = 0
		ToggleFolderWatcher
	End Sub
		
	''' <summary>
	''' This subroutine will open the "My Documents" folder with Windows
	''' Explorer.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId := "My Documents")> _
	Private Sub WindowsExplorerToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oWindow As New Window("My Documents")
		If oWindow.IsOpen = False Then
			processExplorer.StartInfo.Arguments = _
				My.Computer.FileSystem.SpecialDirectories.MyDocuments
			processExplorer.Start
			oWindow.WaitUntilOpened
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will enable/disable the
	''' "Information Properties Editor..." menu item.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ToolsToolStripMenuItemClick(sender As Object, e As EventArgs)
		If infoPropEditFormOpen = True Then
			informationPropertiesEditorToolStripMenuItem.Enabled = False
		Else
			informationPropertiesEditorToolStripMenuItem.Enabled = True
		End If
	End Sub
			
	''' <summary>
	''' This subroutine will check/uncheck the
	''' "Automatically Check for Newer Version" menu item.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub HelpToolStripMenuItemClick(sender As Object, e As EventArgs)
		If UserSettings.UpdateCheck = 1 Then
			checkNewerVersionToolStripMenuItem.Checked = True
		Else
			checkNewerVersionToolStripMenuItem.Checked = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will display the User Guide.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ContentsToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim oWindow As New Window("HTML Help")
		If oWindow.IsOpen = False Then
			processHelp.Start
			oWindow.WaitUntilOpened
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will enable/disable update checking.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub CheckNewerVersionToolStripMenuItemClick(sender As Object, e As EventArgs)
		If checkNewerVersionToolStripMenuItem.Checked Then
			UserSettings.UpdateCheck = 0
			checkNewerVersionToolStripMenuItem.Checked = False
		Else
			UserSettings.UpdateCheck = 1
			checkNewerVersionToolStripMenuItem.Checked = True
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will display the About box.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub AboutToolStripMenuItemClick(sender As Object, e As EventArgs)
		AboutForm.ShowDialog()
	End Sub
	
	#End Region
	
	#Region "Form Menu Utility Functions and Subroutines"
	
	''' <summary>
	''' This subroutine will prompt the user to select PDF file(s), and then
	''' will call the Information Properties Editor or the PDF file upload
	''' for each valid PDF file that was selected.  For editing Information
	''' Properties, "operation" must be "INFO_EDIT".  For uploading,
	''' "operation" must be "UPLOAD".
	''' </summary>
	''' <param name="operation"></param>
	Private Sub SelectAndProcessPdfFiles(ByVal operation As String)
		Dim remaining as Integer = 0
		OpenFileDialog.InitialDirectory = _
		            My.Computer.FileSystem.SpecialDirectories.MyDocuments
		If operation = "INFO_EDIT" Then
			OpenFileDialog.Title = "Select PDF File(s) to Edit"
		ElseIf operation = "UPLOAD" Then
			OpenFileDialog.Title = "Select PDF File(s) to Upload"
		Else
			Exit Sub
		End If
		OpenFileDialog.FileName = Nothing
		If OpenFileDialog.ShowDialog() = 2 Then
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
	
		' Create an array of selected PDF files and sort it.
		Dim pdfFiles As New ArrayList
		For Each fileName As String In OpenFileDialog.FileNames
			pdfFiles.Add(fileName)
		Next
		pdfFiles.Sort
	
		' Queue the sorted, selected documents.
		For Each pdfFile As String In pdfFiles
			If operation = "INFO_EDIT" Then
				formEditorQueue.Enqueue(pdfFile)
			ElseIf operation = "UPLOAD" Then
				uploadQueue.Enqueue(pdfFile)
			End If
		Next
		
		' Open the Information Proprties Editor if editing document
		' properties.
		If operation = "INFO_EDIT" Then
			Dim oInformationPropertiesEditorForm As New _
				 InformationPropertiesEditorForm("FORM")
			oInformationPropertiesEditorForm.Show()
		End If
				
		' Start the upload thread if uploading documents.
		If operation = "UPLOAD" Then
			uploadToolStripMenuItem.Enabled = False
			BackgroundWorkerUpload.RunWorkerAsync
		End If
		
		Me.Cursor = Cursors.Default
	End Sub

	#End Region
		
	#Region "Form Object Events"
	
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
		sortColumn = 0			' set default value
		sortOrder = "asc"		' set default value
		refreshToolStripMenuItem.Enabled = False
		buttonSearch.Enabled = False
		comboBoxSearchText.Text = comboBoxSearchText.Text.TrimStart
		If comboBoxSearchText.Text.Length = 0 Then
			Exit Sub
		End If
		If comboBoxSearchText.Text.IndexOf("*", StringComparison.Ordinal) <> -1 Then
			errorMessage = "Database does not support the use of * " & _
				  		   "as a wildcard character! Use % instead.  " & _
				  		   "For more information, please consult the " & _
				  		   "User Guide."
			errorProvider.SetError(comboBoxSearchText, errorMessage)
			comboBoxSearchText.Select
			Exit Sub
		End If
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
			errorMessage = "Inproper use of operators and/or " & _
				  		   "characters. For more information, please " & _
				  		   "consult the User Guide."
			errorProvider.SetError(comboBoxSearchText, errorMessage)
			comboBoxSearchText.Select
			Exit Sub
		End If
		errorProvider.Clear
		buttonSearch.Enabled = True
	End Sub
	
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
		Dim orderBy As String = Nothing
		listViewDocs.SelectedItems.Clear
		listViewDocs.Items.Clear
		toolStripStatusLabelListCount.Text = ""
		Me.Refresh	' Form needed to be refreshed for status label to clear.
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open = 1 Then
			oDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			comboBoxSearchText.Select
			Exit Sub
		End If
		
		' Build query string.
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
					Do While (oOracleDataReader.Read())
						oListViewItem = listViewDocs.Items.Add( _
								oOracleDataReader("doc_id"))
						oListViewItem.SubItems.Add(oOracleDataReader( _
											      "doc_title"))
						oListViewItem.SubItems.Add(oOracleDataReader( _
												  "doc_author"))
						oListViewItem.SubItems.Add(oOracleDataReader( _
												  "doc_subject"))
						oListViewItem.SubItems.Add(oOracleDataReader( _
												  "doc_added"))
					Loop
				End Using
  			Catch ex As OracleException
				oDatabaseConnection.Dispose
				Me.Cursor = Cursors.Default
				Dim oMessageDialog As New MessageDialog(ex.Message.ToString())
				oMessageDialog.DisplayError
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
			checkAllToolStripMenuItem.Enabled = True
			If Not history.Contains(comboBoxSearchText.Text) Then
				history.Add(comboBoxSearchText.Text)
			End If
		Else
			checkAllToolStripMenuItem.Enabled = False
		End If
		refreshToolStripMenuItem.Enabled = True
		buttonSearch.Enabled = False
		Me.Cursor = Cursors.Default
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
		sortColumn = e.Column
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
			savePDFtoDiskToolStripMenuItem.Enabled = False
			insertDateTimeIntoDocumentNotesToolStripMenuItem.Enabled = False
			textBoxDocumentNotes.Enabled = False
			textBoxDocumentKeywords.Enabled = False
			buttonDocumentNotesUpdate.Enabled = False
			buttonDocumentNotesRevert.Enabled = False
			Me.Text = "PDFKeeper"
		Else
			selectedId = listViewDocs.SelectedItems(0).Text.Trim
			Me.Cursor = Cursors.WaitCursor
			Dim oDatabaseConnection As New DatabaseConnection
			If oDatabaseConnection.Open = 1 Then
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
						savePDFtoDiskToolStripMenuItem.Enabled = True
						insertDateTimeIntoDocumentNotesToolStripMenuItem.Enabled = True
						textBoxDocumentNotes.Enabled = True
						TextBoxDocumentNotesScrollToEnd
						Me.Text = "PDFKeeper - [" & selectedId & "]"
						Me.Cursor = Cursors.Default
					End Using
				End Using
			Catch ex As OracleException
				oDatabaseConnection.Dispose
				Me.Cursor = Cursors.Default
				Dim oMessageDialog As New MessageDialog(ex.Message.ToString())
				oMessageDialog.DisplayError
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
		pdfFile = CacheDir & "\pdfkeeper" & selectedId & ".pdf"
		Dim oPdfFile As New PdfFile(pdfFile)
		If oPdfFile.Retrieve(selectedId) = 0 Then
			
			' Encrypt the PDF document if the operating system supports the
			' Encrypting File System (EFS).
			Dim oIOFile As New IOFile(pdfFile)
			oIOFile.EncryptFile
						
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
				If oWindow.IsOpen = False Then
					processPdfViewer.StartInfo.Arguments = chr(34) & _
														   pdfFile & _
													   	   chr(34)
					processPdfViewer.Start
					oWindow.WaitUntilOpened
				End If
			End If
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will toggle "Uncheck All" and
	''' "Delete Checked Documents" menu items based on if any listview items
	''' are checked or none are checked.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListViewDocsItemChecked(sender As Object, e As ItemCheckedEventArgs)
		If listViewDocs.CheckedItems.Count > 0 Then
			uncheckAllToolStripMenuItem.Enabled = True
			deleteCheckedDocumentsToolStripMenuItem.Enabled = True
		Else
			uncheckAllToolStripMenuItem.Enabled = False
			deleteCheckedDocumentsToolStripMenuItem.Enabled = False
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
			printDocumentNotesToolStripMenuItem.Enabled = True
		Else
			printDocumentNotesToolStripMenuItem.Enabled = False
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
		If oDatabaseConnection.Open = 1 Then
			oDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		Dim sql As String = "update pdfkeeper.docs set " & _
			 				"doc_notes =q'[" & textBoxDocumentNotes.Text & _
			 				"]',doc_dummy = '' where doc_id = " & selectedId
		Using oOracleCommand As New OracleCommand(sql, _
			  oDatabaseConnection.oraConnection)
			Dim result As Byte = 0
			Try
				oOracleCommand.ExecuteNonQuery
			Catch ex As OracleException
				Me.Cursor = Cursors.Default
				Dim oMessageDialog As New MessageDialog(ex.Message.ToString())
				oMessageDialog.DisplayError
				result = 1
			Finally
				oDatabaseConnection.Dispose
			End Try
			If result = 1 Then
				Exit Sub
			End If
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
	''' This subroutine will open the application update URL using the default
	''' web browser.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub StatusStripItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
		If e.ClickedItem.Text = "A newer version of PDFKeeper is available" Then
			Me.Cursor = Cursors.WaitCursor
			Dim oUpdateCheck As New UpdateCheck
			oUpdateCheck.OpenUpdateUrl
			Me.Cursor = Cursors.Default
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will check if an update is available and trigger the
	''' RunWorkerCompleted event.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerUpdateCheckDoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
		Dim oUpdateCheck As New UpdateCheck
		updateAvailable = oUpdateCheck.IsUpdateAvailable
		BackgroundWorkerUpdateCheckRunWorkerCompleted(Me, Nothing)
	End Sub
	
	''' <summary>
	''' This subroutine will create a link on the status bar if an update is
	''' available.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerUpdateCheckRunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
		If updateAvailable Then
			toolStripStatusLabelUpdateStatus.Text = "A newer version of " & _
													"PDFKeeper is available"
			toolStripStatusLabelUpdateStatus.ForeColor = _
				 System.Drawing.SystemColors.ActiveCaption
			toolStripStatusLabelUpdateStatus.IsLink = True
		Else
			toolStripStatusLabelUpdateStatus.Text = Nothing
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will upload all PDF documents in the upload queue and
	''' trigger the RunWorkerCompleted event.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerUploadDoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
		Dim uploaded As Integer = 0
		Dim skipped As Integer = 0
		Dim fileToUpload As String
		Dim counter As Integer = 0
		Dim totalCount As Integer = uploadQueue.Count
		Do While uploadQueue.Count > 0
			counter += 1
			toolStripStatusLabelUpload.Text = "Uploading (" & counter & _
											  " of " & totalCount & ")..."
			fileToUpload = uploadQueue.Dequeue
			Dim oPdfFile As New PdfFile(fileToUpload)
			If oPdfFile.Upload = 1 Then
				skipped += 1
				toolStripStatusLabelUpload.Text = Nothing
				Dim oMessageDialog As New MessageDialog(fileToUpload & _
														" was not loaded.")
				oMessageDialog.DisplayInformation
			Else
				uploaded += 1
			End If
		Loop
		toolStripStatusLabelUpload.Text = "Uploaded " & uploaded & _
							  			  " document(s), skipped " & skipped & _
										  " document(s)."
		Thread.Sleep(30000)
		toolStripStatusLabelUpload.Text = Nothing
		BackgroundWorkerUploadRunWorkerCompleted(Me, Nothing)
	End Sub

	''' <summary>
	''' This subroutine is called by the Upload, DoWork thread to enable the
	''' Upload menu item.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub BackgroundWorkerUploadRunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
		uploadToolStripMenuItem.Enabled = True
	End Sub
	
	''' <summary>
	''' This subroutine will queue the PDF document that was added to the
	''' watched folder and open an instance of the Information Properties
	''' Editor for editing of queued documents if not already open.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub FileSystemWatcherEditorCreated(sender As Object, e As FileSystemEventArgs)
		If e.FullPath.Substring(e.FullPath.Length - 26) <> _
					".modified_by_pdfkeeper.pdf" Then
			Me.Cursor = Cursors.WaitCursor
			Dim oIOFile As New IOFile(e.FullPath)
			Do While oIOFile.IsInUse
				Thread.Sleep(1000)
			Loop
			watcherEditorQueue.Enqueue(e.FullPath)
			If infoPropEditWatcherOpen = False Then
				Dim oInformationPropertiesEditorForm As New _
			 		 InformationPropertiesEditorForm("WATCHER")
				oInformationPropertiesEditorForm.Show()
				Dim oWindow As New Window(InfoPropEditTitle)
				oWindow.WaitUntilOpened
			End If
			Me.Cursor = Cursors.Default
		End If
	End Sub
	
	#End Region
	
	#Region "Form Object Utility Functions and Subroutines"
	
	''' <summary>
	''' This subroutine will print the contents of the Document Notes text box
	''' using the printer chosen by the user.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub PrintDocumentNotesPrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)
		Dim oPrintPages As New PrintPages(textBoxDocumentNotes.Text, _
										  textBoxDocumentNotes.Font)
		oPrintPages.FromString(e)
	End Sub
	
	''' <summary>
	''' This subroutine will update the status bar with the number of items in
	''' the listview.
	''' </summary>
	Private Sub UpdateListCountStatusBar
		toolStripStatusLabelListCount.Text = listViewDocs.Items.Count & _
									 	   " document(s) found."
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
	''' maximum width of 175 each, only if the total width of all columns
	''' exceeds the width of the form.
	''' </summary>
	Private Sub ResizeListViewColumns
		Dim tasWidth As Integer
		tasWidth = 0
		For j = 0 To 4
			listViewDocs.Columns(j).Width = -2
			If j > 0 And j < 4 Then
				tasWidth = tasWidth + listViewDocs.Columns(j).Width
			End If
		Next
		If tasWidth > Me.Width Then
			For k = 1 To 3
				If listViewDocs.Columns(k).Width > 175 Then
					listViewDocs.Columns(k).Width = 175
				End If
			Next
		End If
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
			Dim oMessageDialog As New _
				 MessageDialog("The Document Notes text box contains " & _
							   "unsaved data.  Do you want to save changes?")
			If oMessageDialog.DisplayQuestion = 6 Then
				ButtonDocumentNotesUpdateClick(Me, Nothing)
			Else
				ButtonDocumentNotesRevertClick(Me, Nothing)
			End If
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will Enable/Disable the folder watcher based on the
	''' value of the InfoPropEditWatcherEnabled setting.
	''' </summary>
	Private Sub ToggleFolderWatcher
		If UserSettings.InfoPropEditWatcherEnabled = 1 Then
			Dim result As Integer = 0
			Try
				fileSystemWatcherEditor.Path = _
						   UserSettings.InfoPropEditWatcherFolder
			Catch ex As ArgumentException
				result = 1
				Dim oMessageDialog As New MessageDialog(ex.Message)
				oMessageDialog.DisplayError
			Finally
				If result = 0 Then
					fileSystemWatcherEditor.EnableRaisingEvents = True
					toolStripStatusLabelInfoPropEditWatcher.Visible = True
				Else
					UserSettings.InfoPropEditWatcherEnabled = 0
					UserSettings.InfoPropEditWatcherFolder = Nothing
				End If
			End Try
		Else
			fileSystemWatcherEditor.EnableRaisingEvents = False
			toolStripStatusLabelInfoPropEditWatcher.Visible = False
		End If
	End Sub

	''' <summary>
	''' This subroutine is the folder watcher error event handler that will
	''' log the exception information to the folder watcher log file.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub FileSystemWatcherEditorError(sender As Object, e As ErrorEventArgs)
		Using oStreamWriter As StreamWriter = _
		   				   New StreamWriter(LocAppDataDir & _
		   				   				  "\FolderWatcher.log")
		   	Try
				oStreamWriter.WriteLine("----- Start of error event -----")
				oStreamWriter.WriteLine(DateTime.Now)
				oStreamWriter.WriteLine("Information Properties Editor " & _
										"folder watcher")
				oStreamWriter.WriteLine(e.GetException().ToString)
				oStreamWriter.WriteLine("------ End of error event ------")
			Catch ex As IOException
				Dim oMessageDialog As New MessageDialog(ex.Message)
				oMessageDialog.DisplayError
			End Try
		End Using
	End Sub
	
	#End Region
	
	#Region "Form Closing Subroutines"
	
	''' <summary>
	''' This subroutine will allow the form to close if no background worker
	''' thread's are busy and no Information Properties Editor forms are open.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub MainFormClosing(sender As Object, e As FormClosingEventArgs)
		If BackgroundWorkersBusy Then
			e.Cancel = True
		End If
		Dim oWindow As New Window(InfoPropEditTitle)
		If oWindow.IsOpen Then
			e.Cancel = True
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will call subroutines to check for unsaved Document
	''' Notes, delete cached and temporary PDF files, dispose the database
	''' connection string, save the form size and postion, and then save the
	''' user settings.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub MainFormFormClosed(sender As Object, e As FormClosedEventArgs)
		DocumentNotesModifiedCheck
		Dim folders As New ArrayList
		folders.Add(CacheDir)
		folders.Add(TempDir)
		For Each folder As String In folders
			Dim oIOFolder As New IOFolder(folder)
			oIOFolder.DeletePdfFiles
		Next
		DatabaseConnectionString.Dispose
		SaveFormPosition
		Dim oUserSettings As New UserSettings
		oUserSettings.Write
	End Sub

	''' <summary>
	''' This function will check if any background worker thread's are busy.
	''' </summary>
	''' <returns>True or False</returns>
	Function BackgroundWorkersBusy() As Boolean
		If backgroundWorkerUpdateCheck.IsBusy Or _
		   backgroundWorkerUpload.IsBusy Then
			Return True
		Else
			Return False
		End If
	End Function

	''' <summary>
	''' This subroutine will save the form size and postion.
	''' </summary>
	Private Sub SaveFormPosition
		If Me.WindowState.ToString = "Normal" Then
			UserSettings.FormPositionTop = Me.Top.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionLeft = Me.Left.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionHeight = Me.Height.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionWidth = Me.Width.ToString(CultureInfo.InvariantCulture)
			UserSettings.FormPositionWindowState = 0
		End If
		If Me.WindowState.ToString = "Maximized" Then
			UserSettings.FormPositionWindowState = 2
		End If
	End Sub
	
	#End Region
End Class
