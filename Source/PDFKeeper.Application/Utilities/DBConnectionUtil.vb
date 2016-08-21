'******************************************************************************
'*
'* PDFKeeper -- Capture, Upload, and Search for PDF Documents
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

Public NotInheritable Class DBConnectionUtil
	Private Sub New()
		' Because type 'DBConnectionUtil' contains only 'Shared' members, a
		' default private constructor was added to prevent the compiler from
		' adding a default public constructor. (CA1053)
	End Sub
	
	''' <summary>
	''' Gets the database connection string. 
	''' </summary>
	''' <remarks>
	''' This method should be called inside a Using block to minimize password
	''' exposure.
	''' </remarks>
	''' <returns>The connection string.</returns>
	Friend Shared Function GetConnectionString As String
		Dim dbConnection As New DBConnection
		Return "User Id=" + dbConnection.UserName + ";" & _
			"Password=" + dbConnection.Password.GetString + ";" & _
			"Data Source=" + dbConnection.DataSource + ";" & _
			"Persist Security Info=False;Pooling=True"
	End Function
	
	''' <summary>
	''' Performs a tests database connection.
	''' </summary>
	''' <remarks>
	''' This method is intended to confirm the User Name, Password, and Data
	''' Source provided by the user is valid.
	''' </remarks>
	''' <returns>True for test passed or False for test failed.</returns>
	Friend Shared Function TestConnection As Boolean
		Using connection As New OracleConnection
			Try
				connection.ConnectionString = GetConnectionString
				connection.Open
				Return True
			Catch ex As OracleException
				ShowError(ex.Message)
				Return False
			End Try
		End Using
	End Function
End Class
