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
'*
'* Created by SharpDevelop.
'* User: Robert
'* Date: 7/9/2016
'* Time: 10:07 AM
'*
'******************************************************************************

''' <summary>
''' Database connection singleton model.
''' </summary>
Public NotInheritable Class DBConnection
	Private Shared _instance As DBConnection = New DBConnection()
	Private _userName As String = String.Empty
	Private _securePassword As SecureString
	Private _dataSource As String = String.Empty
	
	Public Shared ReadOnly Property Instance As DBConnection
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Gets/Sets the user name for the database connection.
	''' </summary>
	Friend ReadOnly Property UserName As String
		Get
			If _userName.Length = 0 Then
				_userName = UserSettings.Instance.LastUserName
			End If
			Return _userName
		End Get
	End Property
	
	''' <summary>
	''' Gets/Sets the password for the database connection.
	''' </summary>
	Friend ReadOnly Property SecurePassword As SecureString
		Get
			Return _securePassword
		End Get
	End Property
	
	''' <summary>
	''' Gets/Sets the data source for the database connection.
	''' </summary>
	Friend ReadOnly Property DataSource As String
		Get
			If _dataSource.Length = 0 Then
				_dataSource = UserSettings.Instance.LastDataSource
			End If
			Return _dataSource
		End Get
	End Property
	
	''' <summary>
	''' Gets the connection string for the database connection.
	''' </summary>
	Friend ReadOnly Property ConnectionString As String
		Get
			Return "User Id=" + UserName + ";" & _
				"Password=" + SecurePassword.GetString + ";" & _
				"Data Source=" + DataSource + ";" & _
				"Persist Security Info=False;Pooling=True"
		End Get
	End Property
	
	''' <summary>
	''' Sets the model properties for the database connection. 
	''' </summary>
	''' <param name="userNameParam"></param>
	''' <param name="securePasswordParam"></param>
	''' <param name="dataSourceParam"></param>
	Friend Sub SetModelProperties( _
		ByVal userNameParam As String, _
		ByVal securePasswordParam As SecureString, _
		ByVal dataSourceParam As String)
		
		_userName = userNameParam
		_securePassword = securePasswordParam
		_dataSource = dataSourceParam
	End Sub
	
	''' <summary>
	''' Performs a test connection with the data source and calls the
	''' SetLastUserNameAndDataSource except when the fails. 
	''' </summary>
	''' <returns>Test passed (True or False)</returns>
	Friend Function PerformTestConnection As Boolean
		Using connection As New OracleConnection
			Try
				connection.ConnectionString = ConnectionString
				connection.Open
				SetLastUserNameAndDataSource
				Return True
			Catch ex As OracleException
				ShowError(ex.Message)
				Return False
			End Try
		End Using
	End Function
	
	''' <summary>
	''' Sets the user name and data source cooresponding properties in the
	''' UserSettings object.
	''' </summary>
	Private Sub SetLastUserNameAndDataSource
		UserSettings.Instance.LastUserName = UserName
		UserSettings.Instance.LastDataSource = DataSource
	End Sub
End Class
