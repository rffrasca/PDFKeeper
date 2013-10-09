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

Public Partial Class MainForm
	Dim updateAvailable As Boolean
	Dim history As New ArrayList
	Dim sortColumn As Byte
	Dim sortOrder As string = "asc"
	Dim selectedId As Integer

	Public Sub New()
		Me.InitializeComponent()
		SetFormPosition
		If UserSettings.UpdateCheck = 1 Then
			toolStripStatusLabelUpdateStatus.Text = _
				"Checking for a newer version of PDFKeeper..."
			BackgroundWorkerUpdateCheck.RunWorkerAsync
		End If
	End Sub
	
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
		If GetPDF(selectedId, pdfFile) = 0 Then
			Me.Cursor = Cursors.Default
			MessageDialog.Display("Information", pdfFile & " has been saved.")
		End If
		Me.Cursor = Cursors.Default
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
	''' This subroutine will call child form that will query document
	''' notes for the selected document, then display them for editing.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DocumentNotesToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim objDocumentNotesForm As New _
			   DocumentNotesForm(selectedId)
		objDocumentNotesForm.ShowDialog
	End Sub

	''' <summary>
	''' This subroutine will check all listview items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub CheckAllToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim objListViewItem As ListViewItem
		For Each objListViewItem In listViewDocs.Items
			objListViewItem.Checked = True
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
		Dim objListViewItem As ListViewItem
		For Each objListViewItem In listViewDocs.Items
			objListViewItem.Checked = False
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
		Dim button = MessageDialog.Display("Question", _
										   "Delete all checked documents?")
		If button = 7 Then	' "No" was selected
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		Dim objDatabaseConnection As New DatabaseConnection
		If objDatabaseConnection.Open = 1 Then
			objDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		Dim objListViewItem As ListViewItem
		For Each objListViewItem In listViewDocs.CheckedItems
			Dim sql As String = "delete from pdfkeeper.docs where " & _
								"doc_id =" & objListViewItem.Text
			Using objOracleCommand As New OracleCommand(sql, _
				  objDatabaseConnection.objOracleConnection)
				Try
					objOracleCommand.ExecuteNonQuery
				Catch ex As OracleException
					Me.Cursor = Cursors.Default
					MessageDialog.Display("Error", ex.Message.ToString())
					objDatabaseConnection.Dispose
					Exit Sub
				End Try
			End Using
			objListViewItem.Checked = False
			objListViewItem.Remove
		Next
		objDatabaseConnection.Dispose
		UpdateListCountStatusBar
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will query keywords for the selected document, and then
	''' display them in a child form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DocumentKeywordsToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim objDatabaseConnection As New DatabaseConnection
		If objDatabaseConnection.Open = 1 Then
			objDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		Dim sql As String = "select doc_keywords from pdfkeeper.docs " & _
							"where doc_id = " & selectedId
		Try
			Using objOracleCommand As New OracleCommand(sql, _
				  objDatabaseConnection.objOracleConnection)
				Using objOracleDataReader As OracleDataReader = _
					  objOracleCommand.ExecuteReader()
					objOracleDataReader.Read()
					
					' If the result of the query is an empty string, display a
					' message and exit.
					If objOracleDataReader.IsDBNull(0) Then
						objDatabaseConnection.Dispose
						Me.Cursor = Cursors.Default
						MessageDialog.Display("Information", _
								  			  "Document does not contain " & _
								  			  "and keywords.")
						Exit Sub
					End If
					
					Me.Cursor = Cursors.Default
					Dim objDocumentKeywordsForm As New _
			   			   DocumentKeywordsForm(objOracleDataReader.GetString(0))
					objDatabaseConnection.Dispose
					objDocumentKeywordsForm.ShowDialog
				End Using
			End Using
		Catch ex As OracleException
			objDatabaseConnection.Dispose
			Me.Cursor = Cursors.Default
			MessageDialog.Display("Error", ex.Message.ToString())
			Exit Sub
		End Try
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
	''' This subroutine will open the "My Documents" folder with Windows
	''' Explorer.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId := "My Documents")> _
	Private Sub WindowsExplorerToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		WinProcess.Start("explorer.exe", MyDocsDir, "My Documents")
		Me.Cursor = Cursors.Default
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
		WinProcess.Start("hh.exe", "UserGuide.html", "HTML Help")
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
		Dim uploaded As Integer = 0
		Dim skipped As Integer = 0
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
		
		' Process the sorted array of PDF files.
		remaining = pdfFiles.Count
		For Each pdfFile As String In pdfFiles
			remaining -= 1
			If operation = "INFO_EDIT" Then
				Dim objInformationPropertiesEditorForm As New _
					   InformationPropertiesEditorForm(pdfFile)
				If objInformationPropertiesEditorForm.ShowDialog() = _
						   Windows.Forms.DialogResult.Cancel Then
					
					' Give the user a chance to quit editing PDF files, if
					' Cancel was selected on the Information Properties Editor
					' form.
					If remaining > 0 Then
						Me.Cursor = Cursors.Default
						Dim button = MessageDialog.Display("Question", _
													   	   "Do you want " & _
													   	   "to continue " & _
													   	   "editing PDF " & _
													   	   "files?")
						If button = 7 Then	' "No" was selected
							Exit Sub
						End If
						Me.Cursor = Cursors.WaitCursor
					End If
				End If
			ElseIf operation = "UPLOAD" Then
				If UploadPdfFile(pdfFile) = 1 Then
					skipped += 1
					MessageDialog.Display("Information", _
								pdfFile & " was not loaded.")
				Else
					uploaded += 1
				End If
			End If
		Next
		
		Me.Cursor = Cursors.Default
		
		' Provide status after uploading.
		If operation = "UPLOAD" Then
			MessageDialog.Display("Information", "Uploaded " & uploaded & _
								  " document(s), skipped " & skipped & _
								  " document(s).")
		End If
	End Sub
		
	''' <summary>
	''' This function will upload "pdfFile" to the database.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Shared Function UploadPdfFile(ByVal pdfFile As String) As Byte
		
		' Read properties from PDF document.
		Dim objPdfProperties As New PdfProperties(pdfFile)
		If objPdfProperties.Read = 1 Then
			Return 1
		End If
		
		' Verify Title, Author, and Subject are not blank.
		If objPdfProperties.Title = Nothing Or _
		   objPdfProperties.Author = Nothing Or _
		   objPdfProperties.Subject = Nothing Then
		   	MessageDialog.Display("Error", _
		   						  "The Title, Author, and Subject " & _
		   						  "properties cannot be blank.")
		   	Return 1
		End If
		
		' Read the PDF file into a byte array for loading.
		Dim result As Byte
		Using pdfStream As FileStream = New FileStream(pdfFile, _
											FileMode.Open, _
										    FileAccess.Read)
			Dim pdfBlob As Byte()
			ReDim pdfBlob(pdfStream.Length)
			Try
				pdfStream.Read(pdfBlob, 0, System.Convert.ToInt32(pdfStream.Length))
				result = 0
			Catch ex As IOException
				MessageDialog.Display("Error", ex.Message)
				result = 1
			Finally
				pdfStream.Close
			End Try
			If result = 1 Then
				Return 1
			End If
		
			' Create the Anonymous PL/SQL block statement for the insert.
			Dim objDatabaseConnection As New DatabaseConnection
			If objDatabaseConnection.Open = 1 Then
				Return 1
			End If
			Dim sql As String = " begin " & _
								" insert into pdfkeeper.docs values( " & _
								" pdfkeeper.docs_seq.NEXTVAL, " & _
								" q'[" & objPdfProperties.Title & "]', " & _
								" q'[" & objPdfProperties.Author & "]', " & _
								" q'[" & objPdfProperties.Subject & "]', " & _
								" q'[" & objPdfProperties.Keywords & "]', " & _
								" to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'), " & _
								" '', :1, '') ;" & _
   								" end ;"
   			
   			Using objOracleCommand As New OracleCommand()
				objOracleCommand.CommandText = sql
				objOracleCommand.Connection = _
				  	  objDatabaseConnection.objOracleConnection
				objOracleCommand.CommandType = CommandType.Text
		
				' Bind the parameter to the insert statement.
				Dim objOracleParameter As OracleParameter = _
					objOracleCommand.Parameters.Add("doc_pdf", OracleDbType.Blob)
				objOracleParameter.Direction = ParameterDirection.Input
				objOracleParameter.Value = pdfBlob

				' Perform the insert.
				Try
					objOracleCommand.ExecuteNonQuery()
					result = 0
  				Catch ex As OracleException
					MessageDialog.Display("Error", ex.Message.ToString())
					result = 1
  				Finally
  					objDatabaseConnection.Dispose
				End Try
			End Using
		End Using
		
		Return result
	End Function
	
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
		Me.Cursor = Cursors.WaitCursor
		Dim orderBy As String = Nothing
		listViewDocs.SelectedItems.Clear
		listViewDocs.Items.Clear
		toolStripStatusLabelListCount.Text = ""
		Dim objDatabaseConnection As New DatabaseConnection
		If objDatabaseConnection.Open = 1 Then
			objDatabaseConnection.Dispose
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
		Using objOracleCommand As New OracleCommand(sql, _
			  objDatabaseConnection.objOracleConnection)
			Try
				Using objOracleDataReader As OracleDataReader = _
					  objOracleCommand.ExecuteReader()
			
					' Fill listview with the results.
					Dim objListViewItem As ListViewItem
					Do While (objOracleDataReader.Read())
						objListViewItem = listViewDocs.Items.Add( _
							objOracleDataReader("doc_id"))
						objListViewItem.SubItems.Add(objOracleDataReader( _
												"doc_title"))
						objListViewItem.SubItems.Add(objOracleDataReader( _
												"doc_author"))
						objListViewItem.SubItems.Add(objOracleDataReader( _
												"doc_subject"))
						objListViewItem.SubItems.Add(objOracleDataReader( _
												"doc_added"))
					Loop
				End Using
  			Catch ex As OracleException
				objDatabaseConnection.Dispose
				Me.Cursor = Cursors.Default
				MessageDialog.Display("Error", ex.Message.ToString())
				comboBoxSearchText.Select
				Exit Sub
			End Try
		End Using
		
		objDatabaseConnection.Dispose
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
	''' This subroutine will capture the ID of the selected listview item, and
	''' then enable or disable document record specific, function menu items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListViewDocsSelectedIndexChanged(sender As Object, e As EventArgs)
		If listViewDocs.SelectedItems.Count = 0 Then
			selectedId = 0	' no item is selected
			savePDFtoDiskToolStripMenuItem.Enabled = False
			documentNotesToolStripMenuItem.Enabled = False
			documentKeywordsToolStripMenuItem.Enabled = False
		Else
			selectedId = listViewDocs.SelectedItems(0).Text.Trim
			savePDFtoDiskToolStripMenuItem.Enabled = True
			documentNotesToolStripMenuItem.Enabled = True
			documentKeywordsToolStripMenuItem.Enabled = True
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
		pdfFile = TempDir & "\pdfkeeper" & selectedId & ".pdf"
		If GetPDF(selectedId, pdfFile) = 0 Then
			PdfProcess.Start(pdfFile, False)
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
	''' This subroutine will open the application update URL using the default
	''' web browser.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub StatusStripItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
		If e.ClickedItem.Text = "A newer version of PDFKeeper is available" Then
			Me.Cursor = Cursors.WaitCursor
			Dim objUpdateCheck As New UpdateCheck
			objUpdateCheck.OpenUpdateUrl
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
		Dim objUpdateCheck As New UpdateCheck
		updateAvailable = objUpdateCheck.IsUpdateAvailable
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
			toolStripStatusLabelUpdateStatus.Text = "A newer version of PDFKeeper is available"
			toolStripStatusLabelUpdateStatus.ForeColor = System.Drawing.SystemColors.ActiveCaption
			toolStripStatusLabelUpdateStatus.IsLink = True
		Else
			toolStripStatusLabelUpdateStatus.Text = Nothing
		End If
	End Sub
	
	#End Region
	
	#Region "Form Object Utility Functions and Subroutines"
	
	''' <summary>
	''' This subroutine will update the status bar with the number of items in
	''' the listview.
	''' </summary>
	Private Sub UpdateListCountStatusBar
		toolStripStatusLabelListCount.Text = listViewDocs.Items.Count & _
									 	   " document(s)"
	End Sub
		
	''' <summary>
	''' This subroutine is a workaround to the listview control forcing the
	''' far left column to be left justified.  However, right justification is
	''' required to properly align the ID's in the listview.
	''' </summary>
	Private Sub RightJustifyListViewDocIds
		Dim maxLength As Integer = 0
		Dim objListViewItem As ListViewItem
		For Each objListViewItem In listViewDocs.Items
			If objListViewItem.Text.Length > maxLength Then
				maxLength = objListViewItem.Text.Length
			End If
		Next
		If maxLength > 0 Then
			Dim diff As Integer
			For Each objListViewItem In listViewDocs.Items
				If objListViewItem.Text.Length < maxLength Then
					diff = maxLength - objListViewItem.Text.Length
					objListViewItem.Text = _
					objListViewItem.Text.PadLeft(maxLength + diff)
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
	''' This function will retrieve the PDF document from the database for the
	''' specified ID, and then save it as "pdfFile".
	''' </summary>
	''' <param name="selectedId"></param>
	''' <param name="pdfFile"></param>
	''' <returns>0 = Successful, 1 = Failed</returns>
	Private Shared Function GetPDF(ByRef selectedId As Integer, _
					  			   ByRef pdfFile As String) As Byte
		Dim objDatabaseConnection As New DatabaseConnection
		If objDatabaseConnection.Open = 1 Then
			objDatabaseConnection.Dispose
			Return 1
		End If
		Dim result As Byte = 0
		Dim sql As String = "select doc_pdf from pdfkeeper.docs " & _
							"where doc_id =" & selectedId
		Using objOracleCommand As New OracleCommand(sql, _
			  objDatabaseConnection.objOracleConnection)
			Try
				Using objOracleDataReader As OracleDataReader = _
					  objOracleCommand.ExecuteReader()
  					objOracleDataReader.Read()
  					Using objOracleBlob As OracleBlob = _
  						  objOracleDataReader.GetOracleBlob(0)
  						Using objMemoryStream As New _
  											  MemoryStream(objOracleBlob.Value)
  							Using objFileStream As New FileStream(pdfFile, _
  									   FileMode.Create,FileAccess.Write)
								Try
									objFileStream.Write( _
										objMemoryStream.ToArray, 0, _
										objOracleBlob.Length)
								Catch ex As IOException
									MessageDialog.Display("Error", ex.Message)
									objDatabaseConnection.Dispose
									result = 1
  								Finally
  									objFileStream.Close()
								End Try
								If result = 1 Then
									Return result
								End If
							End Using
						End Using
					End Using
				End Using
			Catch ex As OracleException
				MessageDialog.Display("Error", ex.Message.ToString())
				result = 1
			Finally
				objDatabaseConnection.Dispose
			End Try
		End Using
		Return result
	End Function
	
	#End Region
	
	#Region "Form Closing Subroutines"
	
	''' <summary>
	''' This subroutine will allow the form to close if no background worker
	''' thread's are busy.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub MainFormClosing(sender As Object, e As FormClosingEventArgs)
		If BackgroundWorkersBusy Then
			e.Cancel = True
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will call subroutines to delete temporary files,
	''' dispose the database connection string, save the form size and postion,
	''' and then save the user settings.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub MainFormFormClosed(sender As Object, e As FormClosedEventArgs)
		DeleteTempFiles
		DatabaseConnectionString.Dispose
		SaveFormPosition
		
		Dim objUserSettings As New UserSettings
		objUserSettings.Write
	End Sub

	''' <summary>
	''' This function will check if any background worker thread's are busy.
	''' </summary>
	''' <returns>True or False</returns>
	Function BackgroundWorkersBusy() As Boolean
		If backgroundWorkerUpdateCheck.IsBusy Then
			Return True
		Else
			Return False
		End If
	End Function

	''' <summary>
	''' This subroutine will delete temporary PDF files.
	''' </summary>
	Private Shared Sub DeleteTempFiles
		Dim objDirectoryInfo As New DirectoryInfo(TempDir)
		Dim files As FileInfo() = objDirectoryInfo.GetFiles("pdfkeeper*.pdf")
		For Each file In files
			Try
				file.Delete
			Catch ex as IOException
			End Try
		Next
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
			UserSettings.FormPositionWindowState = 0
		End If
		If Me.WindowState.ToString = "Maximized" Then
			UserSettings.FormPositionWindowState = 2
		End If
	End Sub
	
	#End Region
End Class
