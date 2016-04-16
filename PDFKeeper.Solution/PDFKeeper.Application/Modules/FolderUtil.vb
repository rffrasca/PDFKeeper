'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
'* Copyright (C) 2009-2016 Robert F. Frasca
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

Public Module FolderUtil
	''' <summary>
	''' Returns a count of files with "extension" in "folder", including
	''' subfolders.
	''' </summary>
	''' <param name="folder"></param>
	''' <param name="extension">file extension minus the period.  * will count
	'''	all files.</param>
	''' <returns></returns>
	Public Function CountOfFilesInfolder( _
		ByVal folder As String, _
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
	''' Creates "folder" if it does not exist.
	''' </summary>
	''' <param name="folder"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function CreateFolder(ByVal folder As String) As Integer
		Try
			If Directory.Exists(folder) = False Then
				Directory.CreateDirectory(folder)
			End If
			Return 0
		Catch ex As IOException
			ShowError(ex.Message)
			Return 1
		End Try
	End Function
		
	''' <summary>
	''' Creates "shortcut" to "folder".
	''' </summary>
	''' <param name="shortcut"></param>
	''' <param name="folder"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function CreateFolderShortcut( _
		ByVal shortcut As String, _
		ByVal folder As String) As Integer
		
		Try
			Dim oWshShell As New WshShellClass
			Dim shortcutName As IWshShortcut = _
				DirectCast(oWshShell.CreateShortcut(shortcut), IWshShortcut)
			shortcutName.TargetPath = folder
			shortcutName.Save
			Return 0
		Catch ex As COMException
			ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' Deletes "folder", including subfolders and files.  When "recycle" is
	''' True, deletes to the Windows Recycle Bin.
	''' </summary>
	''' <param name="folder"></param>
	''' <param name="recycle">True or False</param>
	''' <returns>0 = Successful or file does not exist, 1 = Failed</returns>
	Public Function DeleteFolder( _
		ByVal folder As String, _
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
			ShowError(ex.Message)
			Return 1
		End Try
	End Function
		
	''' <summary>
	''' Deletes all empty subfolders from "folder".
	''' </summary>
	''' <param name="folder"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function DeleteEmptySubfoldersFromFolder( _
		ByVal folder As String) As Integer
		
		Dim errors As Integer = 0
		Dim dirInfo As DirectoryInfo = New DirectoryInfo(folder)
		Dim subFolders() As DirectoryInfo = _
			dirInfo.GetDirectories("*", SearchOption.AllDirectories)
		Dim dir As DirectoryInfo
		For Each dir In subFolders
			If NativeMethods.PathIsDirectoryEmpty(dir.FullName) Then
				Try
					Directory.Delete(dir.FullName)
				Catch ex As IOException
					errors += 1
					ShowError(ex.Message)
				End Try
			End If
		Next
		If errors = 0 Then
			Return 0
		Else
			Return 1
		End If
	End Function
End Module
