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
				
				MessageBoxError( _
					PdfKeeper.Strings.PdfPropertiesBlank)
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
				MessageBoxError(ex.Message)
				Return 1
			Finally
				pdfStream.Close
			End Try
		
			Dim oDatabaseConnection As New DatabaseConnection
			If oDatabaseConnection.Open(UserSettings.Instance.LastUserName, _
					DatabaseConnectionForm.dbPassword, _
					UserSettings.Instance.LastDataSource) = 1 Then
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
					MessageBoxError(ex.Message.ToString())
					Return 1
  				Finally
  					oDatabaseConnection.Dispose
				End Try
			End Using
		End Using
		
		Return 0
	End Function
End Class
