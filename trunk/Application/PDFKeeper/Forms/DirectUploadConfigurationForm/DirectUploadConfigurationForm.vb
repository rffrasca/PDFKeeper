'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2015 Robert F. Frasca
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

Public Partial Class DirectUploadConfigurationForm
	Dim selectedFolder As String
	Dim folderPath As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will set the font to MS Sans Serif 8pt in XP or
	''' Segoe UI 9pt in Vista or later, create missing Direct Upload folders,
	''' fill the list box with configured folders, and fill the combo boxes
	''' with stock pre-fill items.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DirectUploadConfigurationFormLoad(sender As Object, e As EventArgs)
		Font = SystemFonts.MessageBoxFont
		DirectUpload.CreateMissingFolders
		FillList
		FillComboBoxes
	End Sub
	
	''' <summary>
	''' This subroutine will fill the combo boxes with stock pre-fill items. 
	''' </summary>
	Private Sub FillComboBoxes
		comboBoxTitlePreFill.Items.Clear
		comboBoxAuthorPreFill.Items.Clear
		comboBoxSubjectPreFill.Items.Clear
		comboBoxTitlePreFill.Items.Add( _
			DirectUploadConfigurationForm_Strings.TitleDate)
		comboBoxTitlePreFill.Items.Add( _
			DirectUploadConfigurationForm_Strings.TitleDateTime)
		comboBoxTitlePreFill.Items.Add( _
			DirectUploadConfigurationForm_Strings.TitleFileName)
		comboBoxAuthorPreFill.Items.Add( _
			DirectUploadConfigurationForm_Strings.AuthorDatabaseUserName)
		comboBoxAuthorPreFill.Items.Add( _
			DirectUploadConfigurationForm_Strings.AuthorWindowsUserName)
		comboBoxSubjectPreFill.Items.Add( _
			DirectUploadConfigurationForm_Strings.SubjectScannedDocument)
	End Sub
	
	''' <summary>
	''' This subroutine will fill the Folders list box with configured folders.
	''' </summary>
	Private Sub FillList
		Dim files As String()
		files = Directory.GetFiles(UploadXmlDir, "*.xml", _
								   SearchOption.TopDirectoryOnly)
		listBoxFolders.Items.Clear
		For Each oFile In files
			listBoxFolders.Items.Add(Path.GetFileNameWithoutExtension(oFile))
		Next
	End Sub
	
	''' <summary>
	''' This subroutine will enable the Edit button and only enable the delete
	''' button if the selected folder doesn't contain any PDF documents.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ListBoxFoldersSelectedIndexChanged(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		selectedFolder = CStr(listBoxFolders.SelectedItem)
		If Not selectedFolder = Nothing Then
			folderPath = Path.Combine(UploadDir, selectedFolder)
			buttonEdit.Enabled = True
			If FolderTask.CountOfFiles(folderPath, "pdf") = 0 Then
				buttonDelete.Enabled = True
			Else
				buttonDelete.Enabled = False
			End If
		End If
		Me.Cursor = Cursors.Default
	End Sub
		
	''' <summary>
	''' This subroutine will prepare the form for a new folder entry.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonNewClick(sender As Object, e As EventArgs)
		selectedFolder = Nothing
		folderPath = Nothing
		PrepareForm
	End Sub
	
	''' <summary>
	''' This subroutine will update the form with properties for the selected
	''' folder and prepare the form for editing.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonEditClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		If ReadFolderProperties() = 1 Then
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		PrepareForm
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will allow the user to delete the selected folder only
	''' if the folder doesn't contain PDF documents.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonDeleteClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		If FolderTask.CountOfFiles(folderPath, "pdf") = 0 Then
			If MessageBoxWrapper.ShowQuestion(String.Format( _
					CultureInfo.CurrentCulture, _
					DirectUploadConfigurationForm_Strings.DeleteFolder, _
					selectedFolder)) = 6 Then
				If FolderTask.Delete(folderPath, False) = 0 Then
					If FileTask.Delete(Path.Combine(UploadXmlDir, _
									   selectedFolder & ".xml"), False) = 0 Then
						FillList
						Discard
					End If
				End If
			End If
		Else
			MessageBoxWrapper.ShowError(String.Format( _
				CultureInfo.CurrentCulture, _
				DirectUploadConfigurationForm_Strings.CannotDeleteFolder, _
				selectedFolder))
			buttonDelete.Enabled = False
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This function will read the properties for "selectedFolder" and update
	''' the form.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Function ReadFolderProperties() As Integer
		Dim oDirectUploadFolderProperties As New _
			DirectUploadFolderProperties(selectedFolder)
		If oDirectUploadFolderProperties.Read = 0 Then
			textBoxFolderName.Text = selectedFolder
			comboBoxTitlePreFill.Text = _
				oDirectUploadFolderProperties.TitlePreFill
			comboBoxAuthorPreFill.Text = _
				oDirectUploadFolderProperties.AuthorPreFill
			comboBoxSubjectPreFill.Text = _
				oDirectUploadFolderProperties.SubjectPreFill
			textBoxKeywordsPreFill.Text = _
				oDirectUploadFolderProperties.KeywordsPreFill
			If CDbl( _
				oDirectUploadFolderProperties.UseExistingTitleChecked) = 1 Then
				checkBoxUseExistingTitle.Checked = True
			Else
				checkBoxUseExistingTitle.Checked = False
			End If
			If CDbl( _
				oDirectUploadFolderProperties.UseExistingAuthorChecked) = 1 Then
				checkBoxUseExistingAuthor.Checked = True
			Else
				checkBoxUseExistingAuthor.Checked = False
			End If
			If CDbl( _
				oDirectUploadFolderProperties.UseExistingSubjectChecked) = 1 Then
				checkBoxUseExistingSubject.Checked = True
			Else
				checkBoxUseExistingSubject.Checked = False
			End If
			If CDbl( _
				oDirectUploadFolderProperties.UseExistingKeywordsChecked) = 1 Then
				checkBoxUseExistingKeywords.Checked = True
			Else
				checkBoxUseExistingKeywords.Checked = False
			End If
			Return 0
		Else
			Return 1
		End If
	End Function
	
	''' <summary>
	''' This subroutine will prepare the form for adding/editing a folder
	''' entry. 
	''' </summary>
	Private Sub PrepareForm
		listBoxFolders.Enabled = False
		buttonNew.Enabled = False
		buttonEdit.Enabled = False
		buttonDelete.Enabled = False
		textBoxFolderName.Enabled = True
		textBoxFolderName.Select
		comboBoxTitlePreFill.Enabled = True
		checkBoxUseExistingTitle.Enabled = True
		comboBoxAuthorPreFill.Enabled = True
		checkBoxUseExistingAuthor.Enabled = True
		comboBoxSubjectPreFill.Enabled = True
		checkBoxUseExistingSubject.Enabled = True
		textBoxKeywordsPreFill.Enabled = True
		checkBoxUseExistingKeywords.Enabled = True
		buttonDiscard.Enabled = True
	End Sub
	
	''' <summary>
	''' This subroutine will trim the leading space from the text in all of
	''' the combo and text boxes; and enable the Save button, if the folder
	''' name doesn't contain invalid characters, the folder is not already
	''' configured, and the length of the folder name, Title, Author, and
	''' Subject is greater than 0.
	''' </summary>
	Private Sub TextAndComboBoxTextChanged
		Me.Cursor = Cursors.WaitCursor
		errorProvider.Clear
		buttonSave.Enabled = False
		TrimSpacesFromTextBoxes
		If textBoxFolderName.Text.Length = 0 Then
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		If FolderTask.NameContainsInvalidChars(textBoxFolderName.Text) Then
			errorProvider.SetError(textBoxFolderName, _
				DirectUploadConfigurationForm_Strings.FolderNameInvalid)
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		If SpecifiedFolderNameAlreadyConfigured() Then
			errorProvider.SetError(textBoxFolderName, _
					String.Format(CultureInfo.CurrentCulture, _
					DirectUploadConfigurationForm_Strings.FolderAlreadyConfigured, _
					textBoxFolderName.Text))
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		If comboBoxTitlePreFill.Text.Length > 0 And _
			comboBoxAuthorPreFill.Text.Length > 0 And _
		   	comboBoxSubjectPreFill.Text.Length > 0 Then
			buttonSave.Enabled = True
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will trim the leading space from the text in all of
	''' the combo and text boxes.
	''' </summary>
	Private Sub TrimSpacesFromTextBoxes
		textBoxFolderName.Text = textBoxFolderName.Text.TrimStart
		comboBoxTitlePreFill.Text = comboBoxTitlePreFill.Text.TrimStart
		comboBoxAuthorPreFill.Text = comboBoxAuthorPreFill.Text.TrimStart
		comboBoxSubjectPreFill.Text = comboBoxSubjectPreFill.Text.TrimStart
		textBoxKeywordsPreFill.Text = textBoxKeywordsPreFill.Text.TrimStart
	End Sub
			
	''' <summary>
	''' This function will return true or false if the specified folder name
	''' is already configured.
	''' </summary>
	''' <returns>True or False</returns>
	Private Function SpecifiedFolderNameAlreadyConfigured() As Boolean
		Dim performFolderCheck As Boolean = False
		If selectedFolder = Nothing Then
			performFolderCheck = True
		ElseIf Not selectedFolder.ToUpper(CultureInfo.CurrentCulture) = Nothing And _
			Not selectedFolder.ToUpper(CultureInfo.CurrentCulture) = _
			textBoxFolderName.Text.ToUpper(CultureInfo.CurrentCulture) Then
			performFolderCheck = True
		End If
		If performFolderCheck Then
			If System.IO.File.Exists(Path.Combine(UploadXmlDir, _
							  textBoxFolderName.Text & ".xml")) Then
				Return True
			End If
		End If
		Return False
	End Function
	
	''' <summary>
	''' This subroutine will display the help page for this form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="hlpevent"></param>
	Private Sub DirectUploadConfigurationFormHelpRequested(sender As Object, hlpevent As HelpEventArgs)
		Me.Cursor = Cursors.WaitCursor
		HelpWrapper.ShowHelp(Me, "Configuring Direct Upload sub-folders.html")
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This subroutine will save the current folder entry and clear the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonSaveClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		If CreateSpecifiedFolder() = 1 Then
			Me.Cursor = Cursors.Default
			Exit Sub
		End If
		If SaveFolderProperties() = 0 Then
			FillList
			Discard
		End If
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This function will create the specified folder if a new folder entry or
	''' move the original folder to the specified folder when an existing
	''' folder entry is editied. 
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Function CreateSpecifiedFolder() As Integer
		If selectedFolder = Nothing Then
			If FolderTask.Create(Path.Combine(UploadDir, _
				textBoxFolderName.Text)) = 0 Then
				Return 0
			Else
				Return 1
			End If
		Else
			If FileTask.Delete(Path.Combine(UploadXmlDir, _
							   selectedFolder & ".xml"), False) = 1 Then
				Return 1
			End If
			If Not selectedFolder.ToUpper(CultureInfo.CurrentCulture) = _
				textBoxFolderName.Text.ToUpper(CultureInfo.CurrentCulture) Then
				Try
					Directory.Move(Path.Combine(UploadDir, selectedFolder), _
							  Path.Combine(UploadDir, textBoxFolderName.Text))
				Catch ex As IOException
					MessageBoxWrapper.ShowError(ex.Message)
					Return 1
				End Try
			End If
			Return 0
		End If
	End Function
	
	''' <summary>
	''' This function will save the folder properties to XML file. 
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Private Function SaveFolderProperties() As Integer
		Dim oDirectUploadFolderProperties As New _
			DirectUploadFolderProperties(textBoxFolderName.Text)
		oDirectUploadFolderProperties.TitlePreFill = _
			comboBoxTitlePreFill.Text.Trim
		oDirectUploadFolderProperties.AuthorPreFill = _
			comboBoxAuthorPreFill.Text.Trim
		oDirectUploadFolderProperties.SubjectPreFill = _
			comboBoxSubjectPreFill.Text.Trim
		oDirectUploadFolderProperties.KeywordsPreFill = _
			textBoxKeywordsPreFill.Text.Trim
		If checkBoxUseExistingTitle.Checked Then
			oDirectUploadFolderProperties.UseExistingTitleChecked = CStr(1)
		Else
			oDirectUploadFolderProperties.UseExistingTitleChecked = CStr(0)
		End If
		If checkBoxUseExistingAuthor.Checked Then
			oDirectUploadFolderProperties.UseExistingAuthorChecked = CStr(1)
		Else
			oDirectUploadFolderProperties.UseExistingAuthorChecked = CStr(0)
		End If
		If checkBoxUseExistingSubject.Checked Then
			oDirectUploadFolderProperties.UseExistingSubjectChecked = CStr(1)
		Else
			oDirectUploadFolderProperties.UseExistingSubjectChecked = CStr(0)
		End If
		If checkBoxUseExistingKeywords.Checked Then
			oDirectUploadFolderProperties.UseExistingKeywordsChecked = CStr(1)
		Else
			oDirectUploadFolderProperties.UseExistingKeywordsChecked = CStr(0)
		End If
		Return oDirectUploadFolderProperties.Write
	End Function
	
	''' <summary>
	''' This subroutine will discard the current folder entry and clear the
	''' form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonDiscardClick(sender As Object, e As EventArgs)
		If MessageBoxWrapper.ShowQuestion( _
			DirectUploadConfigurationForm_Strings.CancelPrompt) = 6 Then ' Yes
			FillList
			Discard
		End If
		DialogResult = Nothing
	End Sub
	
	''' <summary>
	''' This subroutine will perform the discard of the folder entry. 
	''' </summary>
	Private Sub Discard
		listBoxFolders.Enabled = True
		buttonNew.Enabled = True
		buttonEdit.Enabled = False
		buttonDelete.Enabled = False
		textBoxFolderName.Text = Nothing
		textBoxFolderName.Enabled = False
		comboBoxTitlePreFill.Text = Nothing
		comboBoxTitlePreFill.Enabled = False
		checkBoxUseExistingTitle.Checked = False
		checkBoxUseExistingTitle.Enabled = False
		comboBoxAuthorPreFill.Text = Nothing
		comboBoxAuthorPreFill.Enabled = False
		checkBoxUseExistingAuthor.Checked = False
		checkBoxUseExistingAuthor.Enabled = False
		comboBoxSubjectPreFill.Text = Nothing
		comboBoxSubjectPreFill.Enabled = False
		checkBoxUseExistingSubject.Checked = False
		checkBoxUseExistingSubject.Enabled = False
		textBoxKeywordsPreFill.Text = Nothing
		textBoxKeywordsPreFill.Enabled = False
		checkBoxUseExistingKeywords.Checked = False
		checkBoxUseExistingKeywords.Enabled = False
		buttonDiscard.Enabled = False
	End Sub
	
	''' <summary>
	''' This subroutine will close the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonCloseClick(sender As Object, e As EventArgs)
		Me.Close
	End Sub
	
	''' <summary>
	''' This subroutine will prompt the user to discard changes and close the
	''' form if the user chooses to discard.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub DirectUploadConfigurationFormFormClosing(sender As Object, e As FormClosingEventArgs)
		If buttonDiscard.Enabled Then
			ButtonDiscardClick(Me, Nothing)
		End If
		If buttonDiscard.Enabled = False Then
			DialogResult = Windows.Forms.DialogResult.Cancel
		End If
	End Sub
End Class
