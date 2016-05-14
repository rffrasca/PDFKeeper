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

Public Module FileUtil
	''' <summary>
	''' Returns the hash value of "file".
	''' </summary>
	''' <param name="file"></param>
	''' <returns></returns>
	Public Function ComputeFileHashValue(ByVal file As String) As String
		Dim algorithm As HashAlgorithm = HashAlgorithm.Create("SHA1")
		Using stream As New FileStream(file, FileMode.Open, FileAccess.Read)
			Dim hash As Byte() = algorithm.ComputeHash(stream)
			Return BitConverter.ToString(hash)
		End Using
	End Function
	
	''' <summary>
	''' Deletes "file".  When "recycle" is True, deletes to the Windows Recycle
	''' Bin.
	''' </summary>
	''' <param name="file"></param>
	''' <param name="recycle">True or False</param>
	''' <returns>0 = Successful or file does not exist, 1 = Failed</returns>
	Public Function DeleteFile( _
		ByVal file As String, _
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
			ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' Eencrypts "file" when the operating system supports the Encrypting File
	''' System (EFS).
	''' </summary>
	''' <param name="file"></param>
	Public Sub EncryptFile(ByVal file As String)
		Try
			System.IO.File.Encrypt(file)
		Catch ex As IOException
		Catch ex As UnauthorizedAccessException
			ShowError(ex.Message)
		End Try
	End Sub
	
	''' <summary>
	''' Returns a byte array of the specified file.
	''' </summary>
	''' <param name="file"></param>
	''' <returns></returns>
	Public Function FileToByteArray(ByVal file As String) As Byte()
		Dim blob As Byte()
		Using stream As FileStream = New FileStream( _
			file, _
			FileMode.Open, _
			FileAccess.Read)
				
			ReDim blob(CInt(stream.Length))
			Try
				stream.Read(blob, 0, System.Convert.ToInt32(stream.Length))
				Return blob
			Catch ex As IOException
				Throw New IOException(ex.Message.ToString())
			End Try
		End Using
	End Function
	
	''' <summary>
	''' Returns True or False if "file" is in use.  If "file" does not exist,
	''' False is returned. 
	''' </summary>
	''' <param name="file"></param>
	''' <returns></returns>
	Public Function IsFileInUse(ByVal file As String) As Boolean
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
	''' Moves/renames "sourceFile" to "targetFile".
	''' </summary>
	''' <param name="sourceFile"></param>
	''' <param name="targetFile"></param>
	''' <returns>0 = Successful, 1 = Failed</returns>
	Public Function MoveFile( _
		ByVal sourceFile As String, _
		ByVal targetFile As String) As Integer
		
		Try
			System.IO.File.Move(SourceFile, targetFile)
			Return 0
		Catch ex As IOException
			ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' Waits for "file" to be created by waiting until "file" exists and
	''' not in use.
	''' </summary>
	''' <param name="file"></param>
	Public Sub WaitForFileCreation(ByVal file As String)
		Do Until System.IO.File.Exists(file)
			Thread.Sleep(2000)
		Loop
		Do While IsFileInUse(file)
			Thread.Sleep(2000)
		Loop
	End Sub
End Module
