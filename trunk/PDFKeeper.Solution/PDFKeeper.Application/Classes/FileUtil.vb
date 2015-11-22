'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
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

Public NotInheritable Class FileUtil
	
	''' <summary>
	''' Required for FxCop compliance (CA1053).
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' Compute the hash value of "file" and return it to the caller as a
	''' string value.
	''' </summary>
	''' <param name="file"></param>
	''' <returns>Hash value of "file"</returns>
	Public Shared Function ComputeHashValue(ByVal file As String) As String
		Dim algorithm As HashAlgorithm = HashAlgorithm.Create("SHA1")
		Using stream As New FileStream(file, FileMode.Open, _
									  		 FileAccess.Read)
			Dim hash As Byte() = algorithm.ComputeHash(stream)
			Return BitConverter.ToString(hash)
		End Using
	End Function
	
	''' <summary>
	''' Delete "file" if it does exist.  To delete "file" and move it to the
	''' Recycle Bin, set "recycle" to True; otherwise, set "recycle" to False. 
	''' </summary>
	''' <param name="file"></param>
	''' <param name="recycle"></param>
	''' <returns>0 = Successful or file does not exist, 1 = Failed</returns>
	Public Shared Function Delete(ByVal file As String, _
								  ByVal recycle As Boolean) As Integer
		Try
			If System.IO.File.Exists(file) Then
				If recycle Then
					My.Computer.FileSystem.DeleteFile(file, _
					Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, _
					Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin)
				Else
					System.IO.File.Delete(file)
				End If
			End If
			Return 0
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' Eencrypt "file" if the operating system supports the Encrypting File
	''' System (EFS).
	''' </summary>
	''' <param name="file"></param>
	Public Shared Sub Encrypt(ByVal file As String)
		Try
			System.IO.File.Encrypt(file)
		Catch ex As IOException
		Catch ex As UnauthorizedAccessException
			MessageBoxWrapper.ShowError(ex.Message)
		End Try
	End Sub
	
	''' <summary>
	''' Return True or False if "file" is in use.  If "file" does not exist,
	''' False is returned. 
	''' </summary>
	''' <param name="file"></param>
	''' <returns>True or False</returns>
	Public Shared Function IsInUse(ByVal file As String) As Boolean
		If System.IO.File.Exists(file) Then
			Try
				Using stream As New FileStream(file, FileMode.Open, _
								  	FileAccess.ReadWrite, FileShare.None)
				End Using
				Return False
			Catch ex As IOException
				Return True
			Catch ex As UnauthorizedAccessException
				Return True
			End Try
		End If
		Return False
	End Function
		
	''' <summary>
	''' Move "sourceFile" to "targetFile"; can be used to rename "sourceFile"
	''' to "targetFile".
	''' </summary>
	''' <param name="sourceFile"></param>
	''' <param name="targetFile"></param>
	''' <returns>0 = Successful, 1 = Failed</returns>
	Public Shared Function Move(ByVal sourceFile As String, _
								ByVal targetFile As String) As Integer
		Try
			System.IO.File.Move(SourceFile, targetFile)
			Return 0
		Catch ex As IOException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' Wait for "file" to be created.  It will wait until "file" exists and is
	''' not in use.
	''' </summary>
	''' <param name="file"></param>
	Public Shared Sub WaitForFileCreation(ByVal file As String)
		Do Until System.IO.File.Exists(file)
			Thread.Sleep(2000)
		Loop
		Do While IsInUse(file)
			Thread.Sleep(2000)
		Loop
	End Sub
End Class
