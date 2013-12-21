'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2013 Robert F. Frasca
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

Public NotInheritable Class FileTask
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will compute the hash code of "file" and return it to
	''' the caller as a string value. 
	''' </summary>
	''' <param name="file"></param>
	''' <returns></returns>
	Public Shared Function CalcHashCode(ByVal file As String) As String
		Dim oHashAlgorithm As HashAlgorithm = HashAlgorithm.Create("SHA1")
		Using oFileStream As New FileStream(file, FileMode.Open, _
												  FileAccess.Read)
			Dim hash As Byte() = oHashAlgorithm.ComputeHash(oFileStream)
			Return BitConverter.ToString(hash)
		End Using
	End Function
		
	''' <summary>
	''' This subroutine will encrypt "file" if the operating system supports
	''' the Encrypting File System (EFS).
	''' </summary>
	''' <param name="file"></param>
	Public Shared Sub Encrypt(ByVal file As String)
		Try
			System.IO.File.Encrypt(file)
		Catch ex As IOException
		End Try
	End Sub
	
	''' <summary>
	''' This subroutine will wait for "file" to be created.  It will wait until
	''' "file" exists and is not in use.
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
		
	''' <summary>
	''' This function will return True or False if "file" is in use.  If
	''' "file" does not exist, False is returned. 
	''' </summary>
	''' <param name="file"></param>
	''' <returns>True or False</returns>
	Public Shared Function IsInUse(ByVal file As String) As Boolean
		If System.IO.File.Exists(file) Then
			Try
				Using oFileStream As New FileStream(file, FileMode.Open, _
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
	''' This function will delete "file" if it does exist.  To delete "file"
	''' and move it to the Recycle Bin, set "recycle" to True; otherwise, set
	''' "recycle" to False. 
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
	''' This function will convert "inputFile" into a PDF document.  Only XPS
	''' and HTM/HTML file types are supported.
	''' </summary>
	''' <param name="inputFile"></param>
	''' <returns>0 = Success, 1 = Failure, 2 = Unsupported file type</returns>
	Public Shared Function ConvertToPdf(ByVal inputFile As String) As Integer
		Dim fileType As String = Path.GetExtension(inputFile).ToUpper( _
								 CultureInfo.InvariantCulture)
		Dim outputFile As String
		Dim converterTool As String
		Dim converterArgs As String
		If fileType = ".XPS" Then
			outputFile = SetConvertOutputFileName(inputFile)
			converterTool = "gxps-win32.exe"
			converterArgs = "-sDEVICE=pdfwrite " & _
							"-sOutputFile=" & Chr(34) & outputFile & Chr(34) & _
						  	" -dNOPAUSE " & Chr(34) & inputFile & Chr(34)
		ElseIf fileType = ".HTM" Or fileType = ".HTML" Then
			outputFile = SetConvertOutputFileName(inputFile)
			converterTool = ConfigurationManager.AppSettings("wkhtmltopdf")
			converterArgs = Chr(34) & inputFile & Chr(34) & _
					  " " & Chr(34) & outputFile & Chr(34)
		Else
			Return 2
		End If
		Dim oProcess As New Process()
		Try
			oProcess.StartInfo.FileName = converterTool
			oProcess.StartInfo.Arguments = converterArgs
			oProcess.StartInfo.UseShellExecute = False
			oProcess.StartInfo.CreateNoWindow = True
			oProcess.StartInfo.RedirectStandardError = True
			oProcess.Start
			oProcess.WaitForExit
			If oProcess.ExitCode <> 0 Then
				Dim oStreamReader As StreamReader = oProcess.StandardError
				MessageBoxWrapper.ShowError(oStreamReader.ReadToEnd)
				Return 1
			End If
			Return 0
		Catch ex As System.ComponentModel.Win32Exception
			MessageBoxWrapper.ShowError(ex.Message)
			Return 1
		End Try
	End Function
	
	''' <summary>
	''' This function will get called by ConvertToPdf to set the output file
	''' name for the conversion so not to overwrite a file that may exist
	''' with the same name.
	''' </summary>
	''' <param name="inputFile"></param>
	''' <returns>output file name</returns>
	Private Shared Function SetConvertOutputFileName( _
			ByVal inputFile As String) As String
		Dim outputFile As String
		outputFile = Path.Combine(CaptureDir, Path.GetFileName(inputFile) & ".pdf")
		If System.IO.File.Exists(outputFile) Then
			Dim value As Integer = 0
			Do
				value += 1
				If System.IO.File.Exists(Path.Combine(CaptureDir, _
					Path.GetFileName(inputFile) & "_" & value & ".pdf")) = _
						False Then
					outputFile = Path.Combine(CaptureDir, _
						Path.GetFileName(inputFile) & "_" & value & ".pdf")
					Exit Do
				End If
			Loop
		End If
		Return outputFile
	End Function
End Class
