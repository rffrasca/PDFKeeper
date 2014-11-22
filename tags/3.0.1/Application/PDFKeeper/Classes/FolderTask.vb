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

Public NotInheritable Class FolderTask
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will create "folder" if it doesn't exist.
	''' </summary>
	''' <param name="folder"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function Create(ByVal folder As String) As Integer
		Try
			If Directory.Exists(folder) = False Then
				Directory.CreateDirectory(folder)
			End If
			Return 0
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' This function will delete "folder" if it does exist, including
	''' subfolders and files.  To delete "folder" and move it to the Recycle
	''' Bin, set "recycle" to True; otherwise, set "recycle" to False. 
	''' </summary>
	''' <param name="folder"></param>
	''' <param name="recycle"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function Delete(ByVal folder As String, _
								  ByVal recycle As Boolean) As Integer
		Try
			If Directory.Exists(folder) Then
				If recycle Then
					Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory( _
					folder, _
					Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
					Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin)
				Else
					Directory.Delete(folder, True)
				End If
			End If
			Return 0
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' This function will create shortcut "pathName" to "folder".
	''' </summary>
	''' <param name="pathName"></param>
	''' <param name="folder"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function CreateShortcutToFolder(ByVal pathName As String, _
										 ByVal folder As String) As Integer
		Try
			Dim oWshShell As New WshShellClass
			Dim shortcut As IWshShortcut = _
				DirectCast(oWshShell.CreateShortcut(pathName), IWshShortcut)
			shortcut.TargetPath = folder
			shortcut.Save
			Return 0
		Catch ex As COMException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' This subroutine will delete all PDF files created by PDFKeeper from
	''' "folder".  This subroutine will not display a message when an
	''' IOException has been caught.
	''' </summary>
	''' <param name="folder"></param>
	Public Shared Sub DeletePdfKeeperCreatedPdfFiles(ByVal folder As String)
		Dim objDirectoryInfo As New DirectoryInfo(folder)
		Dim files As FileInfo() = objDirectoryInfo.GetFiles("pdfkeeper*.pdf")
		For Each oFile In files
			Try
				oFile.Delete
			Catch ex as IOException
			End Try
		Next
	End Sub
	
	''' <summary>
	''' This function will return a count of files in "folder" with
	'''	"extension", including subfolders.
	''' </summary>
	''' <param name="folder"></param>
	''' <param name="extension"></param>
	''' <returns>number of files</returns>
	Public Shared Function CountOfFiles(ByVal folder As String, _
										ByVal extension As String) As Integer
		Dim count As Integer = 0
		Dim files As String()
		files = Directory.GetFiles(folder, "*." & extension, _
								   SearchOption.AllDirectories)
		For Each oFile In files
			count += 1
		Next
		Return count
	End Function
	
	''' <summary>
	''' This subroutine will delete all empty subfolders in "folder".
	''' </summary>
	''' <param name="folder"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function DeleteAllEmptySubfolders(ByVal folder As String) _
																 As Integer
		Dim errors As Integer = 0
		Dim odirectoryInfo As DirectoryInfo = New DirectoryInfo(folder)
		Dim subFolders() As DirectoryInfo = _
			oDirectoryInfo.GetDirectories("*", SearchOption.AllDirectories)
		Dim oDirectory As DirectoryInfo
		For Each oDirectory In subFolders
			If NativeMethods.PathIsDirectoryEmpty(oDirectory.FullName) Then
				Try
					Directory.Delete(oDirectory.FullName)
				Catch ex As IOException
					errors += 1
					MessageBoxWrapper.ShowError(ex.Message)
				End Try
			End If
		Next
		If errors = 0 Then
			Return 0
		Else
			Return 1
		End If
	End Function
		
	''' <summary>
	''' This subroutine will convert all XPS files in "folder", including its
	''' subfolders to PDF and delete to the Recycle Bin. 
	''' </summary>
	''' <param name="folder"></param>
	Public Shared Sub ConvertAllXpsFilesToPdf(ByVal folder As String)
		Dim files As String()
		files = Directory.GetFiles(folder, "*.xps", _
								   SearchOption.AllDirectories)
		For Each oFile In files
			FileTask.WaitForFileCreation(oFile)
			If FileTask.ConvertToPdf(oFile) = 0 Then
				FileTask.Delete(oFile, True)
			End If
		Next
	End Sub
	
	''' <summary>
	''' This function will return True or False if the name of the specified
	''' folder contains one or more invalid characters.
	''' </summary>
	''' <param name="folder"></param>
	''' <returns>True or False</returns>
	Public Shared Function NameContainsInvalidChars(ByVal folder As String) As Boolean
		For Each invalidChar In Path.GetInvalidFileNameChars()
			If folder.Contains(invalidChar) Then
				Return True
			End If
		Next
		Return False
	End Function
End Class
