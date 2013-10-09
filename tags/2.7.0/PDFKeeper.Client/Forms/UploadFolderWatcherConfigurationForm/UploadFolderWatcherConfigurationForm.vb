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

Public Partial Class UploadFolderWatcherConfigurationForm
	
	''' <summary>
	''' This subroutine Is the Class constructor.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
	''' <summary>
	''' This subroutine will update the form with values retrieved from the
	''' User Settings object properties.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub UploadFolderWatcherConfigurationFormLoad(sender As Object, e As EventArgs)
		
		' Title PreFill
		comboBoxTitlePreFill.Items.Clear
		comboBoxTitlePreFill.Items.Add( _
			UploadFolderWatcherConfigurationForm_Strings.TitleDate)
		comboBoxTitlePreFill.Items.Add( _
			UploadFolderWatcherConfigurationForm_Strings.TitleDateTime)
		comboBoxTitlePreFill.Items.Add( _
			UploadFolderWatcherConfigurationForm_Strings.Filename)
		comboBoxTitlePreFill.Text = UserSettings.TitlePreFill
		If UserSettings.UseExistingTitleChecked = 1 Then
			checkBoxUseExistingTitle.Checked = True
		End If
		
		' Author PreFill
		comboBoxAuthorPreFill.Items.Clear
		comboBoxAuthorPreFill.Items.Add( _
			UploadFolderWatcherConfigurationForm_Strings.AuthorDatabaseUsername)
		comboBoxAuthorPreFill.Items.Add( _
			UploadFolderWatcherConfigurationForm_Strings.AuthorWindowsUsername)
		comboBoxAuthorPreFill.Text = UserSettings.AuthorPreFill
		If UserSettings.UseExistingAuthorChecked = 1 Then
			checkBoxUseExistingAuthor.Checked = True
		End If
		
		' Subject PreFill
		comboBoxSubjectPreFill.Items.Clear
		comboBoxSubjectPreFill.Items.Add( _
			UploadFolderWatcherConfigurationForm_Strings.SubjectScannedDocument)
		comboBoxSubjectPreFill.Text = UserSettings.SubjectPreFill
		If UserSettings.UseExistingSubjectChecked = 1 Then
			checkBoxUseExistingSubject.Checked = True
		End If
		
		' Keywords PreFill
		textBoxKeywordsPreFill.Text = UserSettings.KeywordsPreFill
		If UserSettings.UseExistingKeywordsChecked = 1 Then
			checkBoxUseExistingKeywords.Checked = True
		End If
		
		If UserSettings.AfterUploadingDeletePdfChecked = 1 Then
			checkBoxDeleteAfterUploaded.Checked = True
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will make the warning message visible when
	''' "Delete PDF document after being uploaded" is checked.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub CheckBoxDeleteAfterUploadedCheckedChanged(sender As Object, e As EventArgs)
		If checkBoxDeleteAfterUploaded.Checked Then
			labelDeletePdfWarning.Visible = True
		Else
			labelDeletePdfWarning.Visible = False
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will update the User Settings object properties with
	''' values from the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonOKClick(sender As Object, e As EventArgs)
		UserSettings.TitlePreFill =	comboBoxTitlePreFill.Text.Trim
		UserSettings.AuthorPreFill = comboBoxAuthorPreFill.Text.Trim
		UserSettings.SubjectPreFill = comboBoxSubjectPreFill.Text.Trim
		UserSettings.KeywordsPreFill = textBoxKeywordsPreFill.Text.Trim
		If checkBoxUseExistingTitle.Checked Then
			UserSettings.UseExistingTitleChecked = 1
		Else
			UserSettings.UseExistingTitleChecked = 0
		End If
		If checkBoxUseExistingAuthor.Checked Then
			UserSettings.UseExistingAuthorChecked = 1
		Else
			UserSettings.UseExistingAuthorChecked = 0
		End If
		If checkBoxUseExistingSubject.Checked Then
			UserSettings.UseExistingSubjectChecked = 1
		Else
			UserSettings.UseExistingSubjectChecked = 0
		End If
		If checkBoxUseExistingKeywords.Checked Then
			UserSettings.UseExistingKeywordsChecked = 1
		Else
			UserSettings.UseExistingKeywordsChecked = 0
		End If
		If checkBoxDeleteAfterUploaded.Checked Then
			UserSettings.AfterUploadingDeletePdfChecked = 1
		Else
			UserSettings.AfterUploadingDeletePdfChecked = 0
		End If
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
	
	''' <summary>
	''' This subroutine will trim the leading space from the text in all of
	''' the combo and text boxes, and enables the Save button if the length of
	''' the Title, Author, and Subject is greater than 0.
	''' </summary>
	Private Sub TextComboBoxTextChanged
		comboBoxTitlePreFill.Text = comboBoxTitlePreFill.Text.TrimStart
		comboBoxAuthorPreFill.Text = comboBoxAuthorPreFill.Text.TrimStart
		comboBoxSubjectPreFill.Text = comboBoxSubjectPreFill.Text.TrimStart
		textBoxKeywordsPreFill.Text = textBoxKeywordsPreFill.Text.TrimStart
		If comboBoxTitlePreFill.Text.Length > 0 And _
		   comboBoxAuthorPreFill.Text.Length > 0 And _
		   comboBoxSubjectPreFill.Text.Length > 0 Then
			buttonOK.Enabled = True
		Else
			buttonOK.Enabled = False
		End If
	End Sub
End Class
