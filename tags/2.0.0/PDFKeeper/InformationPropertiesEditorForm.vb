'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2010 Robert F. Frasca
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
	Dim m_pdfFile As String
				
	Public Sub New(ByVal pdfFile As String)
		Me.InitializeComponent()
		m_pdfFile = pdfFile
	End Sub
	
	''' <summary>
	''' This subroutine will read the properties from the PDF document and
	''' update the form.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub InformationPropertiesEditorFormLoad(sender As Object, e As EventArgs)
		textBoxPDFDocument.Text = m_pdfFile
		Dim objPdfProperties As New PdfProperties(m_pdfFile)
		If objPdfProperties.Read = 0 Then
			textBoxTitle.Text = objPdfProperties.Title
			comboBoxAuthor.Text = objPdfProperties.Author
			comboBoxSubject.Text = objPdfProperties.Subject
			textBoxKeywords.Text = objPdfProperties.Keywords
			textBoxTitle.Select
			TextComboBoxTextChanged
		Else
			Me.Close
		End If
	End Sub
	
	#Region "Form Object Events"
	
	''' <summary>
	''' This subroutine will call the View subroutine in the PdfFunctions
	''' class to display the PDF document.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonViewClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		PdfProcess.Start(m_pdfFile, True)
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
	''' This subroutine will save the information properties to the PDF
	''' document.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonSaveClick(sender As Object, e As EventArgs)
		If IsPdfOpen Then
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		
		' Get values from the form and save to the PDF document.
		Dim objPdfProperties As New PdfProperties(m_pdfFile)
		objPdfProperties.Title = textBoxTitle.Text.Trim
		objPdfProperties.Author = comboBoxAuthor.Text.Trim
		objPdfProperties.Subject = comboBoxSubject.Text.Trim
		objPdfProperties.Keywords = textBoxKeywords.Text.Trim
		If objPdfProperties.Save = 1 Then
			Me.Cursor = Cursors.Default
			Exit Sub
		Else
			Me.Cursor = Cursors.Default
			
			' Prompt to view the modified PDF document.
			Dim button = MessageDialog.Display("Question", m_pdfFile & " " & _
											   "has been updated.  Do you " & _
											   "want to view it?")
			If button = 6 Then	' "Yes" was selected
				ButtonViewClick(Me, Nothing)
				
				' Wait for user to close the viewer.
				Dim viewerOpen as Boolean = True
				While viewerOpen = True
					viewerOpen = WinProcess.IsWindowAlreadyOpen( _
											Path.GetFileName(m_pdfFile))
					Thread.Sleep(1000)
				End While
			End If
			
			Me.DialogResult = Windows.Forms.DialogResult.OK
		End If
	End Sub
	
	''' <summary>
	''' This subroutine will close the form if the PDF document is not open
	''' within SumatraPDF.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub ButtonCancelClick(sender As Object, e As EventArgs)
		If IsPdfOpen Then
			Me.DialogResult = Windows.Forms.DialogResult.None
		Else
			Me.DialogResult = Windows.Forms.DialogResult.Cancel
		End If
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
		Dim objDatabaseConnection As New DatabaseConnection
		If objDatabaseConnection.Open = 1 Then
			objDatabaseConnection.Dispose
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
			Using objOracleCommand As New OracleCommand(sql, _
				  objDatabaseConnection.objOracleConnection)
				Using objOracleDataReader As OracleDataReader = _
					  objOracleCommand.ExecuteReader()
	
					' Fill combo box with the results.
					Do While (objOracleDataReader.Read())
						If comboBoxTitle = "Author" Then
							comboBoxAuthor.Items.Add( _
								objOracleDataReader("doc_author"))
						ElseIf comboBoxTitle = "Subject" Then
							comboBoxSubject.Items.Add( _
								objOracleDataReader("doc_subject"))
						End If
					Loop
				End Using
			End Using
		Catch ex As OracleException
			Me.Cursor = Cursors.Default
			MessageDialog.Display("Error", ex.Message.ToString())
		Finally
			objDatabaseConnection.Dispose
		End Try
		Me.Cursor = Cursors.Default
	End Sub
	
	''' <summary>
	''' This function will check if the PDF file is still open within
	''' SumatraPDF.
	''' </summary>
	''' <returns>True or False</returns>
	Private Function IsPdfOpen() As Boolean
		If WinProcess.IsWindowAlreadyOpen(Path.GetFileName(m_pdfFile)) Then
			Me.Activate
			MessageDialog.Display("Information", "You must close out of " & _
				 	  m_pdfFile & " before continuing.")
			Return True
		Else
			Return False
		End If
	End Function
	
	#End Region
End Class
