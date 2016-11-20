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

Public NotInheritable Class PdfFileTask
	
	''' <summary>
	''' This subroutine is the class constructor required for FxCop compliance.
	''' </summary>
	Private Sub New()
	End Sub	
	
	''' <summary>
	''' This function will upload "pdfFile" to the database.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function UploadToDatabase(ByVal pdfFile As String) _
														  As Integer
		
		' Read properties from PDF document and confirm that the Title, Author,
		' and Subject are not blank.
		Dim oPdfProperties As New PdfProperties(pdfFile)
		If oPdfProperties.Read = 0 Then
			If oPdfProperties.Title = Nothing Or _
		   	   oPdfProperties.Author = Nothing Or _
		   	   oPdfProperties.Subject = Nothing Then
				MessageBoxWrapper.ShowError( _
					PdfFileTask_Strings.PdfPropertiesBlank)
		   		Return 1
			End If
		Else
			Return 1
		End If
		
		' Read the PDF file into a byte array for loading.
		Using pdfStream As FileStream = New FileStream(pdfFile, _
											FileMode.Open, _
										    FileAccess.Read)
			Dim pdfBlob As Byte()
			ReDim pdfBlob(CInt(pdfStream.Length))
			Try
				pdfStream.Read(pdfBlob, 0, _
							   System.Convert.ToInt32(pdfStream.Length))
			Catch ex As IOException
				MessageBoxWrapper.ShowError(ex.Message)
				Return 1
			Finally
				pdfStream.Close
			End Try
		
			Dim oDatabaseConnection As New DatabaseConnection
			If oDatabaseConnection.Open(UserSettings.LastUserName, _
					DatabaseConnectionForm.dbPassword, _
					UserSettings.LastDataSource) = 1 Then
				Return 1
			End If
			
			' Create the Anonymous PL/SQL block statement for the insert.
			Dim sql As String = " begin " & _
								" insert into pdfkeeper.docs values( " & _
								" pdfkeeper.docs_seq.NEXTVAL, " & _
								" q'[" & oPdfProperties.Title & "]', " & _
								" q'[" & oPdfProperties.Author & "]', " & _
								" q'[" & oPdfProperties.Subject & "]', " & _
								" q'[" & oPdfProperties.Keywords & "]', " & _
								" to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'), " & _
								" '', :1, '') ;" & _
   								" end ;"
   			
   			Using oOracleCommand As New OracleCommand()
				oOracleCommand.CommandText = sql
				oOracleCommand.Connection = _
				  	  oDatabaseConnection.oraConnection
				oOracleCommand.CommandType = CommandType.Text
		
				' Bind the parameter to the insert statement.
				Dim oOracleParameter As OracleParameter = _
					oOracleCommand.Parameters.Add("doc_pdf", OracleDbType.Blob)
				oOracleParameter.Direction = ParameterDirection.Input
				oOracleParameter.Value = pdfBlob

				' Perform the insert.
				Try
					oOracleCommand.ExecuteNonQuery()
				Catch ex As OracleException
					MessageBoxWrapper.ShowError(ex.Message.ToString())
					Return 1
  				Finally
  					oDatabaseConnection.Dispose
				End Try
			End Using
		End Using
		
		Return 0
	End Function
	
	''' <summary>
	''' This function will retrieve the PDF document from the database for
	''' "selectedId" and then save it to disk as "pdfFile".  If "pdfFile" is
	''' cached, then skip the retrieve.  If "pdfFile" is retrieved, then add to
	''' the cache.  If encryption is supported by the operating system, encrypt
	''' "pdfFile".
	''' </summary>
	''' <param name="selectedId"></param>
	''' <param name="pdfFile"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function RetrieveFromDatabase(ByVal selectedId As Integer, _
										ByVal pdfFile As String) As Integer
		If FileCache.IsCached(pdfFile) = False Then
			Dim oDatabaseConnection As New DatabaseConnection
			If oDatabaseConnection.Open(UserSettings.LastUserName, _
					DatabaseConnectionForm.dbPassword, _
					UserSettings.LastDataSource) = 1 Then
				oDatabaseConnection.Dispose
				Return 1
			End If
			Dim sql As String = "select doc_pdf from pdfkeeper.docs " & _
								"where doc_id =" & selectedId
			Using oOracleCommand As New OracleCommand(sql, _
				  oDatabaseConnection.oraConnection)
				Try
					Using oOracleDataReader As OracleDataReader = _
						  oOracleCommand.ExecuteReader()
  						oOracleDataReader.Read()
  						Using oOracleBlob As OracleBlob = _
  							  oOracleDataReader.GetOracleBlob(0)
  							Using oMemoryStream As New _
  								   MemoryStream(oOracleBlob.Value)
  								Using oFileStream As New FileStream(pdfFile, _
  										 FileMode.Create,FileAccess.Write)
									Try
										oFileStream.Write( _
											oMemoryStream.ToArray, 0, _
											CInt(oOracleBlob.Length))
									Catch ex As IOException
										MessageBoxWrapper.ShowError(ex.Message)
										oDatabaseConnection.Dispose
										Return 1
  									Finally
  										oFileStream.Close()
									End Try
								End Using
							End Using
						End Using
					End Using
				Catch ex As OracleException
					MessageBoxWrapper.ShowError(ex.Message.ToString())
					Return 1
				Finally
					oDatabaseConnection.Dispose
				End Try
			End Using
			FileCache.Add(pdfFile)		
		End If
		FileTask.Encrypt(pdfFile)
		Return 0
	End Function
	
	''' <summary>
	''' This function will generate a PNG file in the Cache folder containing
	''' the first page from "pdfFile".  If the PNG file is cached, then skip
	''' the file generation.  If the PNG file is generated, then add to the
	''' cache.  If encryption is supported by the operating system, encrypt the
	''' generated PNG file.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function GeneratePreviewImage(ByVal pdfFile As String) _
												As Integer
		Dim outputFile As String = Path.ChangeExtension(pdfFile, "png")
		If FileCache.IsCached(outputFile) = False Then
			Dim oProcess As New Process()
			Try
				oProcess.StartInfo.FileName = "gswin32c.exe"
				oProcess.StartInfo.Arguments = _
					"-o " & Chr(34) & outputFile & Chr(34) & _
					" -sDEVICE=pngalpha " & _
					"-dLastPage=1 " & Chr(34) & pdfFile & Chr(34)
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
				FileCache.Add(outputFile)
			Catch ex As System.ComponentModel.Win32Exception
				MessageBoxWrapper.ShowError(ex.Message)
				Return 1
			End Try
		End If
		FileTask.Encrypt(outputFile)
		Return 0
	End Function
	
	''' <summary>
	''' This function will extract the text contained within "pdfFile" and
	''' return extracted text to the caller.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>Text extracted from pdfFile</returns>
	Public Shared Function ExtractPdfText(ByVal pdfFile As String) As String
		Using oPdfReader = New PdfReader(pdfFile)
			Dim text As New StringBuilder()
			For i As Integer = 1 To oPdfReader.NumberOfPages
				Dim strategy As ITextExtractionStrategy = _
					New iTextSharp.text.pdf.parser.LocationTextExtractionStrategy()
				Dim currentPage As String = PdfTextExtractor.GetTextFromPage( _
					oPdfReader, i, strategy)
				Dim lines As String() = currentPage.Split(ControlChars.Lf)
				For Each line As String In lines
					text.AppendLine(line)
				Next
			Next
			Return text.ToString()
		End Using
	End Function
	
	''' <summary>
	''' This function will perform a security check on "pdfFile" and return
	'''	the result code.
	''' </summary>
	''' <param name="file"></param>
	''' <returns>
	''' 0 = Can be opened with full permissions.
	''' 1 = OWNER password required.
	''' 2 = Contains a USER password or unable to check document.
	''' </returns>
	Public Shared Function SecurityCheck(ByVal pdfFile As String) As Integer
		Dim oPdfReader As PdfReader = Nothing
		Dim inputOpened As Boolean = False
		Try
			oPdfReader = New PdfReader(pdfFile)
			inputOpened = True
			If oPdfReader.IsOpenedWithFullPermissions Then
				Return 0
			Else
				Return 1
			End If
		Catch ex As DocumentException
			MessageBoxWrapper.ShowError(ex.Message)
			Return 3
		Catch ex As IOException
			If ex.Message = "Bad user password" Then
				MessageBoxWrapper.ShowError(String.Format( _
							CultureInfo.CurrentCulture, _
							PdfFileTask_Strings.ContainsUserPassword, pdfFile))
			Else
				MessageBoxWrapper.ShowError(ex.Message)
			End If
			Return 2
		Finally
			If inputOpened = True Then
				oPdfReader.Close
			End If
		End Try
	End Function
End Class
