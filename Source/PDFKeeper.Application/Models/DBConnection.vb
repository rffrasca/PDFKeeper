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
	Private _userName As String
	Private _securePassword As SecureString
	Private _dataSource As String
	Private _readyToConnect As Boolean
	Private _connectionValidated As Boolean
	
	Public Shared ReadOnly Property Instance As DBConnection
		Get
			Return _instance
		End Get
	End Property
	
	''' <summary>
	''' Gets/Sets the database connection user name.  On a get, the user name
	''' last used to connect will be set using the value from the UserSettings
	''' object.
	''' </summary>
	Friend Property UserName As String
		Get
			If _userName Is Nothing Then
				_userName = UserSettings.Instance.LastUserName
				OnPropertyChanged
			End If
			Return _userName
		End Get
		Set(ByVal value As String)
			_userName = value
			OnPropertyChanged
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the database connection secure password object.
	''' </summary>
	Friend Property SecurePassword As SecureString
		Get
			Return _securePassword
		End Get
		Set(ByVal value As SecureString)
			_securePassword = value
			OnPropertyChanged
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the database connection data source.  On a get, the data
	''' source last used to connect will be set using the value from the
	''' UserSettings object.
	''' </summary>
	Friend Property DataSource As String
		Get
			If _dataSource Is Nothing Then
				_dataSource = UserSettings.Instance.LastDataSource
				OnPropertyChanged
			End If
			Return _dataSource
		End Get
		Set(ByVal value As String)
			_dataSource = value
			OnPropertyChanged
		End Set
	End Property
	
	''' <summary>
	''' Gets the ready to connect status.
	''' </summary>
	Friend ReadOnly Property ReadyToConnect As Boolean
		Get
			Return _readyToConnect
		End Get
	End Property
	
	''' <summary>
	''' Gets the database connection validation status.
	''' </summary>
	Friend ReadOnly Property ConnectionValidated As Boolean
		Get
			return _connectionValidated
		End Get
	End Property
		
	''' <summary>
	''' Gets the database connection string.  It should only be called right
	''' before connecting to the database and the connection string should be
	''' disposed as quickly as possible.
	''' </summary>
	''' <returns>database connection string.</returns>
	Friend Function GetConnectionString As String
		Return "User Id=" + UserName + ";" & _
			"Password=" + SecurePassword.GetString + ";" & _
			"Data Source=" + DataSource + ";" & _
			"Persist Security Info=False;Pooling=True"
	End Function
	
	''' <summary>
	''' Sets the ready to connect status to true when the user name and data
	''' source properties are not blank.  It also validates the database
	''' connection when the secure password is not nothing and sets the
	''' connection validation status based on the result. 
	''' </summary>
	Private Sub OnPropertyChanged
		If UserName.Length > 0 And DataSource.Length > 0 Then
			_readyToConnect = True
			If SecurePassword IsNot Nothing Then
				ValidateConnection
			Else
				_connectionValidated = False
			End If
		Else
			_readyToConnect = False
		End If
	End Sub
	
	''' <summary>
	''' Validates the Database Connection by making a connection to the
	''' database, and then setting the ConnectionValidated property based on
	''' the result.
	''' </summary>
	Private Sub ValidateConnection
		Using connection As New OracleConnection
			Try
				connection.ConnectionString = GetConnectionString
				connection.Open
				SetLastUserNameAndDataSource
				_connectionValidated = True
			Catch ex As OracleException
				ShowError(ex.Message)
				_connectionValidated = False
			End Try
		End Using
	End Sub
		
	''' <summary>
	''' Sets the user name and data source to their cooresponding properties in
	''' the UserSettings object.
	''' </summary>
	Private Sub SetLastUserNameAndDataSource
		UserSettings.Instance.LastUserName = UserName
		UserSettings.Instance.LastDataSource = DataSource
	End Sub
End Class
