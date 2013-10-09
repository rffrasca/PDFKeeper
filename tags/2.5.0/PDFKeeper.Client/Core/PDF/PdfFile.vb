'*************************************************************************
'*
'* PDFKeeper -- PDF Document Storage for Small or Home Office
'* Copyright (C) 2009-2012 Robert F. Frasca
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

Public Class PdfFile
	Dim pdfFile As String
	
	''' <summary>
	''' This subroutine is the class constructor.
	''' </summary>
	''' <param name="arg: pdfFile"></param>
	Public Sub New(ByVal arg As String)
		pdfFile = arg
	End Sub
	
	''' <summary>
	''' This function will upload the PDF file object to the database.
	''' </summary>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Upload As Integer
			
		' Read properties from PDF document.
		Dim oPdfProperties As New PdfProperties(pdfFile)
		If oPdfProperties.Read = 1 Then
			Return 1
		End If
		
		' Verify Title, Author, and Subject are not blank.
		If oPdfProperties.Title = Nothing Or _
		   oPdfProperties.Author = Nothing Or _
		   oPdfProperties.Subject = Nothing Then
			Dim oMessageDialog As New MessageDialog("The Title, Author, " & _
													"and Subject " & _
		   						  					"properties cannot be " & _
		   						  					"blank.")
			oMessageDialog.DisplayError
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
				pdfStream.Read(pdfBlob, 0, _
							   System.Convert.ToInt32(pdfStream.Length))
				result = 0
			Catch ex As IOException
				Dim oMessageDialog As New MessageDialog(ex.Message)
				oMessageDialog.DisplayError
				result = 1
			Finally
				pdfStream.Close
			End Try
			If result = 1 Then
				Return 1
			End If
		
			' Create the Anonymous PL/SQL block statement for the insert.
			Dim oDatabaseConnection As New DatabaseConnection
			If oDatabaseConnection.Open = 1 Then
				Return 1
			End If
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
					result = 0
				Catch ex As OracleException
					Dim oMessageDialog As New MessageDialog(ex.Message.ToString())
					oMessageDialog.DisplayError
					result = 1
  				Finally
  					oDatabaseConnection.Dispose
				End Try
			End Using
		End Using
		
		Return result
	End Function
	
	''' <summary>
	''' This function will retrieve the PDF file object from the database for
	''' the specified ID, and then save it to disk.
	''' </summary>
	''' <param name="selectedId"></param>
	''' <returns>0 = Success, 1 = Failed</returns>
	Public Function Retrieve(ByVal selectedId As Integer) As Integer
		
		' If "pdfFile" is cached, then skip the retrieve.
		Dim oFileCache As New FileCache(pdfFile)
		If oFileCache.IsFileCached Then
			Return 0
		End If

		' Retrieve the PDF document from the database.
		Dim oDatabaseConnection As New DatabaseConnection
		If oDatabaseConnection.Open = 1 Then
			oDatabaseConnection.Dispose
			Return 1
		End If
		Dim result As Byte = 0
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
										oOracleBlob.Length)
								Catch ex As IOException
									Dim oMessageDialog As New _
										 MessageDialog(ex.Message)
									oMessageDialog.DisplayError
									oDatabaseConnection.Dispose
									result = 1
  								Finally
  									oFileStream.Close()
								End Try
								If result = 1 Then
									Return result
								End If
							End Using
						End Using
					End Using
				End Using
			Catch ex As OracleException
				Dim oMessageDialog As New MessageDialog(ex.Message.ToString())
				oMessageDialog.DisplayError
				result = 1
			Finally
				oDatabaseConnection.Dispose
			End Try
		End Using
		
		' If the retrieve was successful, then add "pdfFile" to the cache
		' collection.
		If result = 0 Then
			oFileCache.Add
		End If
		
		Return result
	End Function
End Class
