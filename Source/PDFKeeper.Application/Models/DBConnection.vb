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
''' Database Connection singleton model.
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
	''' Gets/Sets the User Name for the Database Connection.
	''' </summary>
	Friend Property UserName As String
		Get
			Return _userName
		End Get
		Set(ByVal value As String)
			_userName = value
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the Password for the Database Connection.
	''' </summary>
	Friend Property SecurePassword As SecureString
		Get
			Return _securePassword
		End Get
		Set(ByVal value As SecureString)
			_securePassword = value
		End Set
	End Property
		
	''' <summary>
	''' Gets/Sets the Data Source for the Database Connection.
	''' </summary>
	Friend Property DataSource As String
		Get
			Return _dataSource
		End Get
		Set(ByVal value As String)
			_dataSource = value
		End Set
	End Property
	
	''' <summary>
	''' Gets the connection string for the Database Connection.
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
	''' Tests a database connection. When successful, makes the SecurePassword
	''' member readonly and invokes SetLastUserNameAndDataSource.
	''' </summary>
	''' <returns>True for test passed or False for test failed.</returns>
	Friend Function TestConnection As Boolean
		Using connection As New OracleConnection
			Try
				connection.ConnectionString = ConnectionString
				connection.Open
				_securePassword.MakeReadOnly
				SetLastUserNameAndDataSource
				Return True
			Catch ex As OracleException
				ShowError(ex.Message)
				Return False
			End Try
		End Using
	End Function
	
	''' <summary>
	''' Sets both UserName to LastUserName and DataSource to LastDataSource in
	''' the UserSettings object.
	''' </summary>
	Private Sub SetLastUserNameAndDataSource
		UserSettings.Instance.LastUserName = UserName
		UserSettings.Instance.LastDataSource = DataSource
	End Sub
End Class
