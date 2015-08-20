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

Public NotInheritable Class DirectUpload
	Dim Shared configuredFolders As New ArrayList
		
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will make sure that there is a matching folder for each
	''' XML configuration file.  Missing folders will be created.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function CreateMissingFolders() As Integer
		Dim xmlFiles As String()
		xmlFiles = Directory.GetFiles(UploadXmlDir, "*.xml", _
			SearchOption.TopDirectoryOnly)
		For Each xmlFile In xmlFiles
			If FolderTask.Create(Path.Combine(UploadDir, _
						  Path.GetFileNameWithoutExtension(xmlFile))) = 1 Then
				Return 1
			End If
		Next
		Return 0
	End Function
	
	''' <summary>
	''' This subroutine will delete all empty subfolders from each configured
	''' folder.
	''' </summary>
	Public Shared Sub DeleteAllEmptySubfolders
		FillConfiguredFoldersArray
		For Each configuredFolder As String In configuredFolders
			If FolderTask.DeleteAllEmptySubfolders(Path.Combine(UploadDir, _
												   configuredFolder)) = 0 Then
				If System.IO.File.Exists(Path.Combine(UploadXmlDir, _
								  configuredFolder & ".xml")) = False Then
					If FolderTask.CountOfFiles(Path.Combine(UploadDir, _
											   configuredFolder), "*") = 0 Then
						FolderTask.Delete(Path.Combine(UploadDir, _
										  configuredFolder), False)
					End If
				End If
			End If
		Next
	End Sub
	
	''' <summary>
	''' This function will return a count of PDF documents in each configured
	''' folder, including subfolders.
	''' </summary>
	''' <returns>count</returns>
	Public Shared Function CountOfPdfFiles() As Integer
		Dim count As Integer
		FillConfiguredFoldersArray
		For Each configuredFolder As String In configuredFolders
			count = count + FolderTask.CountOfFiles(Path.Combine(UploadDir, _
													configuredFolder), "pdf")
		Next
		Return count
	End Function
	
	''' <summary>
	''' This subroutine will upload all PDF documents in each configured
	'''	folder, including subfolders.
	''' </summary>
	Public Shared Sub UploadAllPdfFiles
		Dim oSortedList As New SortedList()
		FillConfiguredFoldersArray
		Dim counter As Integer = 0
		For Each configuredFolder As String In configuredFolders
			Dim oDirectoryInfo As New DirectoryInfo(Path.Combine(UploadDir, _
													configuredFolder))
			Dim oFileSystemInfo As FileSystemInfo() = _
				oDirectoryInfo.GetFileSystemInfos("*.pdf", _
					SearchOption.AllDirectories)
			For Each pdfFile In oFileSystemInfo
				FileTask.WaitForFileCreation(pdfFile.FullName)
				Dim oDate As Date = pdfFile.LastWriteTime
				counter += 1
				oSortedList.Add(oDate.ToString("s", _
					CultureInfo.CurrentCulture()) & "_" & counter, _
					pdfFile.FullName)
			Next	
		Next
		Dim index As Integer
		Dim inputPdfFile As String
		Dim outputPdfFile As String
		Dim pdfConfigFolder As String
		Dim pdfConfigFolderPtr As Integer
		For index = 0 To oSortedList.Count - 1
			inputPdfFile = CStr(oSortedList.GetByIndex(index))
			outputPdfFile = Path.Combine(UploadTempDir, _
				Path.GetFileName(inputPdfFile))
			pdfConfigFolder = Path.GetDirectoryName( _
				inputPdfFile.Substring(Len(UploadDir) + 1))
			pdfConfigFolderPtr = pdfConfigFolder.IndexOf( _
				Path.DirectorySeparatorChar)
			If Not pdfConfigFolderPtr = -1 Then
				pdfConfigFolder = Left(pdfConfigFolder, pdfConfigFolderPtr)
			End If
			Dim oDirectUploadFolderProperties As New _
				DirectUploadFolderProperties(pdfConfigFolder)
			If oDirectUploadFolderProperties.Read = 1 Then
				Exit Sub
			End If
			If PdfFileTask.SecurityCheck(inputPdfFile) = 0 Then
				Dim oPdfProperties As New PdfProperties(inputPdfFile, _
					outputPdfFile)
				If oPdfProperties.Read = 0 Then
					Dim oFileInfo As New FileInfo(inputPdfFile)
					If oPdfProperties.Title = Nothing Or CDbl( _
						oDirectUploadFolderProperties.UseExistingTitleChecked) = 0 Then
						If oDirectUploadFolderProperties.TitlePreFill.Trim = _
							DirectUploadConfigurationForm_Strings.TitleDate Then
							oPdfProperties.Title = DateTime.Now.ToString( _
								"yyyy-MM-dd", CultureInfo.CurrentCulture)
						ElseIf oDirectUploadFolderProperties.TitlePreFill.Trim = _
							DirectUploadConfigurationForm_Strings.TitleDateTime Then
							oPdfProperties.Title = DateTime.Now.ToString( _
								"yyyy-MM-dd HH:mm:ss", _
								CultureInfo.CurrentCulture)
						ElseIf oDirectUploadFolderProperties.TitlePreFill.Trim = _
							DirectUploadConfigurationForm_Strings.TitleFileName Then
							oPdfProperties.Title = _
								oFileInfo.Name.ToString.Substring(0, ( _
								oFileInfo.Name.ToString.Length - 4))
						Else
							oPdfProperties.Title = _
								oDirectUploadFolderProperties.TitlePreFill.Trim
						End If
					End If
					If oPdfProperties.Author = Nothing Or CDbl( _
						oDirectUploadFolderProperties.UseExistingAuthorChecked) = 0 Then
						If oDirectUploadFolderProperties.AuthorPreFill.Trim = _
							DirectUploadConfigurationForm_Strings.AuthorDatabaseUserName Then
							oPdfProperties.Author = UserSettings.LastUserName
						ElseIf oDirectUploadFolderProperties.AuthorPreFill.Trim = _
							DirectUploadConfigurationForm_Strings.AuthorWindowsUserName Then
							oPdfProperties.Author = Environment.UserName
						Else
							oPdfProperties.Author = _
								oDirectUploadFolderProperties.AuthorPreFill.Trim
						End If
					End If
					If oPdfProperties.Subject = Nothing Or CDbl( _
						oDirectUploadFolderProperties.UseExistingSubjectChecked) = 0 Then
						oPdfProperties.Subject = _
							oDirectUploadFolderProperties.SubjectPreFill.Trim
					End If
					If oPdfProperties.Keywords = Nothing Or CDbl( _
						oDirectUploadFolderProperties.UseExistingKeywordsChecked) = 0 Then
						oPdfProperties.Keywords = _
							oDirectUploadFolderProperties.KeywordsPreFill.Trim
					End If
				End If
				If oPdfProperties.Write = 0 Then
					If PdfFileTask.UploadToDatabase(outputPdfFile) = 0 Then
						FileTask.Delete(inputPdfFile, True)
						FileTask.Delete(outputPdfFile, False)
					End If
				End If
			Else
				MessageBoxWrapper.ShowError(String.Format( _
					CultureInfo.CurrentCulture, _
					DirectUpload_Strings.ContainsPassword, inputPdfFile))
			End If
		Next
	End Sub
	
	''' <summary>
	''' This subroutine will fill the Direct Upload configured folders array.
	''' </summary>
	Private Shared Sub FillConfiguredFoldersArray
		configuredFolders.Clear
		Dim odirectoryInfo As DirectoryInfo = New DirectoryInfo(UploadDir)
		Dim subFolders() As DirectoryInfo = _
			oDirectoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly)
		Dim oDirectory As DirectoryInfo
		For Each oDirectory In subFolders
			configuredFolders.Add(oDirectory.Name)
		Next
	End Sub
End Class
