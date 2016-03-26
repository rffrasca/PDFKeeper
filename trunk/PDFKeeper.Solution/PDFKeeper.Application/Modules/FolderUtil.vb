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
	''' Returns a count of files for the specified extension in the specified
	''' folder, including subfolders.
	''' </summary>
	''' <param name="folder">Path name of folder.</param>
	''' <param name="extension">Extension of files to count, * for all files.
	''' </param>
	''' <returns>count of files.</returns>
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
	''' Creates the specified folder if it doesn't exist.
	''' </summary>
	''' <param name="folder">Path name of folder.</param>
	''' <returns>0 = Success, 1 = Failed.</returns>
	Public Function CreateFolder(ByVal folder As String) As Integer
		Try
			If Directory.Exists(folder) = False Then
				Directory.CreateDirectory(folder)
			End If
			Return 0
		Catch ex As IOException
			MessageBoxError(ex.Message)
			Return 1
		End Try
	End Function
		
	''' <summary>
	''' Creates the specified shortcut to the specified folder.
	''' </summary>
	''' <param name="shortcut">Path name of shortcut.</param>
	''' <param name="folder">Path name of folder.</param>
	''' <returns>0 = Success, 1 = Failed.</returns>
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
			MessageBoxError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' Deletes the specified folder if it does exist, including subfolders
	''' and files.
	''' </summary>
	''' <param name="folder">Path name of folder.</param>
	''' <param name="recycle">True or False to delete folder to the Recycle
	''' Bin.</param>
	''' <returns>0 = Success, 1 = Failed.</returns>
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
			MessageBoxError(ex.Message)
			Return 1
		End Try
	End Function
		
	''' <summary>
	''' Deletes all empty subfolders from the specified folder.
	''' </summary>
	''' <param name="folder">Path name of folder.</param>
	''' <returns>0 = Success, 1 = Failed.</returns>
	Public Function DeleteEmptySubfoldersFromFolder( _
		ByVal folder As String) As Integer
		
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
					MessageBoxError(ex.Message)
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
