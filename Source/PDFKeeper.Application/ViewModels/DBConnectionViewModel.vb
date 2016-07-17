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
'* Time: 10:51 AM
'*
'******************************************************************************

Public Class DBConnectionViewModel
	Inherits ViewModelBase
	Private _userName As String = String.Empty
	Private _password As String = String.Empty
	Private _securePassword As SecureString
	Private _dataSource As String = String.Empty
	Private _okButtonState As Boolean
	Private _connectTestPassed As Boolean
	Private userNameSet As Boolean
	Private dataSourceSet As Boolean
	
	''' <summary>
	''' Gets/Sets the user name for the database connection and is bound to the
	''' view.
	''' </summary>
	Public Property UserName As String
		Get
			If _userName.Length = 0 And userNameSet = False Then
				_userName = DBConnection.Instance.UserName
				userNameSet = True
				SetOkButtonStateProperty
			End If
			Return _userName
		End Get
		Set(ByVal value As String)
			_userName = value.Trim
			SetOkButtonStateProperty
			OnPropertyChanged("UserName")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the password string of asterisks for the database connection
	''' and is bound to the view.
	''' </summary>
	Public Property Password As String
		Get
			Return _password
		End Get
		Set(ByVal value As String)
			_password = value.Trim
			SetOkButtonStateProperty
			OnPropertyChanged("Password")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the secure password for the database connection and is to be
	''' set by the view; and then executes OnSecurePasswordSet.
	''' </summary>
	Public Property SecurePassword As SecureString
		Get
			Return _securePassword
		End Get
		Set(ByVal value As SecureString)
			_securePassword = value
			OnSecurePasswordSet
		End Set
	End Property
		
	''' <summary>
	''' Gets/Sets the data source for the database connection and is bound to
	''' the view.
	''' </summary>
	Public Property DataSource As String
		Get
			If _dataSource.Length = 0 And dataSourceSet = False Then
				_dataSource = DBConnection.Instance.DataSource
				dataSourceSet = True
				SetOkButtonStateProperty
			End If
			Return _dataSource
		End Get
		Set(ByVal value As String)
			_dataSource = value.Trim
			SetOkButtonStateProperty
			OnPropertyChanged("DataSource")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the ok button state and is bound to the view.
	''' </summary>
	Public Property OkButtonState As Boolean
		Get
			return _okButtonState
		End Get
		Set(ByVal value As Boolean)
			_okButtonState = value
			OnPropertyChanged("OkButtonState")
		End Set
	End Property
	
	''' <summary>
	''' Gets the connect test passed result for the view.
	''' </summary>
	Public ReadOnly Property ConnectTestPassed As Boolean
		Get
			return _connectTestPassed
		End Get
	End Property
	
	''' <summary>
	''' Executes methods on the model to set the user name, password, and data
	''' source; and then perform a test connection, setting the
	''' ConnectTestPassed property member with the result.
	''' </summary>
	Public Sub OnSecurePasswordSet
		DBConnection.Instance.SetModelProperties( _
			UserName, _
			SecurePassword, _
			DataSource)
		_connectTestPassed = DBConnection.Instance.PerformTestConnection
		If ConnectTestPassed = False Then
			Password = String.Empty
		End If
	End Sub
		
	''' <summary>
	''' Sets the OK button state property that is bound to the view.
	''' </summary>
	Private Sub SetOkButtonStateProperty
		If UserName.Length > 0 And _
			Password.Length > 0 And _
			DataSource.Length > 0 Then
			
			OkButtonState = True
		Else
			OkButtonState = False
		End If
	End Sub
End Class
