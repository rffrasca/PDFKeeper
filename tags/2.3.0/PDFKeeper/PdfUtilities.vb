'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2011 Robert F. Frasca
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

Friend Class PdfUtilities
	
	''' <summary>
	''' Private constructor added to address CA1812.
	''' </summary>
	Private Sub New()
	End Sub
	
	''' <summary>
	''' This function will upload "pdfFile" to the database.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Shared Function Upload(ByVal pdfFile As String) As Integer
		
		' Read properties from PDF document.
		Dim objPdfProperties As New PdfProperties(pdfFile)
		If objPdfProperties.Read = 1 Then
			Return 1
		End If
		
		' Verify Title, Author, and Subject are not blank.
		If objPdfProperties.Title = Nothing Or _
		   objPdfProperties.Author = Nothing Or _
		   objPdfProperties.Subject = Nothing Then
		   	MessageDialog.Display("Error", _
		   						  "The Title, Author, and Subject " & _
		   						  "properties cannot be blank.")
		   	Return 1
		End If
		
		' Read the PDF file into a byte array for loading.
		Dim result As Byte
		Using pdfStream As FileStream = New FileStream(pdfFile, _
											FileMode.Open, _
										    FileAccess.Read)
			Dim pdfBlob As Byte()
			ReDim pdfBlob(pdfStream.Length)
			Try
				pdfStream.Read(pdfBlob, 0, System.Convert.ToInt32(pdfStream.Length))
				result = 0
			Catch ex As IOException
				MessageDialog.Display("Error", ex.Message)
				result = 1
			Finally
				pdfStream.Close
			End Try
			If result = 1 Then
				Return 1
			End If
		
			' Create the Anonymous PL/SQL block statement for the insert.
			Dim objDatabaseConnection As New DatabaseConnection
			If objDatabaseConnection.Open = 1 Then
				Return 1
			End If
			Dim sql As String = " begin " & _
								" insert into pdfkeeper.docs values( " & _
								" pdfkeeper.docs_seq.NEXTVAL, " & _
								" q'[" & objPdfProperties.Title & "]', " & _
								" q'[" & objPdfProperties.Author & "]', " & _
								" q'[" & objPdfProperties.Subject & "]', " & _
								" q'[" & objPdfProperties.Keywords & "]', " & _
								" to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'), " & _
								" '', :1, '') ;" & _
   								" end ;"
   			
   			Using objOracleCommand As New OracleCommand()
				objOracleCommand.CommandText = sql
				objOracleCommand.Connection = _
				  	  objDatabaseConnection.objOracleConnection
				objOracleCommand.CommandType = CommandType.Text
		
				' Bind the parameter to the insert statement.
				Dim objOracleParameter As OracleParameter = _
					objOracleCommand.Parameters.Add("doc_pdf", OracleDbType.Blob)
				objOracleParameter.Direction = ParameterDirection.Input
				objOracleParameter.Value = pdfBlob

				' Perform the insert.
				Try
					objOracleCommand.ExecuteNonQuery()
					result = 0
  				Catch ex As OracleException
					MessageDialog.Display("Error", ex.Message.ToString())
					result = 1
  				Finally
  					objDatabaseConnection.Dispose
				End Try
			End Using
		End Using
		
		Return result
	End Function
	
	''' <summary>
	''' This function will open pdfFile for viewing with SumatraPDF.  To open
	''' SumatraPDF in restricted mode, set restrict to True.  To open
	''' SumatraPDF in normal mode, set restrict to False.
	''' </summary>
	''' <param name="pdfFile"></param>
	''' <param name="restrict"></param>
	''' <returns>
	''' Process ID or 0 if "pdfFile" is already open within SumatraPDF.
	''' </returns>
	Public Shared Function View(ByVal pdfFile As String, _
							    ByVal restrict As Boolean) As Integer
		Dim processArgs As String
		If restrict Then
			processArgs = "-restrict " & chr(34) & pdfFile & chr(34)
		Else
			processArgs = chr(34) & pdfFile & chr(34)
		End If
		Return WinProcess.Start("SumatraPDF.exe", processArgs, _
								 Path.GetFileName(pdfFile) & " - SumatraPDF")
	End Function
End Class
