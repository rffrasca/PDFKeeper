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
'* Date: 7/3/2016
'* Time: 12:47 PM
'*
'******************************************************************************

Public Class DatabaseLogOnViewModel
	Inherits ViewModelBase
	Private _userName As String
	Private _password As String
	Private _securePassword As SecureString
	Private _dataSource As String
	Private _isOkToLogOn As Boolean
	Private _isAbleToConnect As Boolean
	
	''' <summary>
	''' Gets/Sets textBoxUserName text value.
	''' </summary>
	Public Property UserName As String
		Get
			Return _userName
		End Get
		Set(ByVal value As String)
			_userName = value.Trim
			DatabaseLogOn.Instance.UserName = _userName	
			OnPropertyChanged("UserName")
			SetIsOkToLogon
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets textBoxSecure text value.
	''' </summary>
	Public Property Password As String
		Get
			Return _password
		End Get
		Set(ByVal value As String)
			_password = value.Trim
			OnPropertyChanged("Password")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets secure password object.
	''' </summary>
	Public Property SecurePassword As SecureString
		Get
			Return _securePassword
		End Get
		Set(ByVal value As SecureString)
			_securePassword = value
			DatabaseLogOn.Instance.Password = _securePassword
			OnSecurePasswordChanged 
		End Set
	End Property	
	
	''' <summary>
	''' Gets/Sets textBoxDataSource text value.
	''' </summary>
	Public Property DataSource As String
		Get
			Return _dataSource
		End Get
		Set(ByVal value As String)
			_dataSource = value.Trim
			DatabaseLogOn.Instance.DataSource = _dataSource
			OnPropertyChanged("DataSource")
			SetIsOkToLogon
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets buttonOK state.
	''' </summary>
	Public Property IsOkToLogOn As Boolean
		Get
			Return _isOkToLogOn
		End Get
		Set(ByVal value As Boolean)
			_isOkToLogOn = value
			OnPropertyChanged("IsOkToLogOn")
		End Set
	End Property
	
	''' <summary>
	''' Gets True or False if able to connect to the database.   
	''' </summary>
	Public ReadOnly Property IsAbleToConnect As Boolean
		Get
			Return _isAbleToConnect
		End Get
	End Property
		
	''' <summary>
	''' Gets the User Name and Data Source from last log on and sets the
	''' corresponding properties.
	''' </summary>
	Public Sub GetLastLogOnUserNameAndDataSource
		UserName = UserSettings.Instance.LastUserName
		DataSource = UserSettings.Instance.LastDataSource
	End Sub
	
	''' <summary>
	''' Sets the IsOkToLogOn property to True when the UserName and DataSource
	''' properties contain values; otherwise, sets to False.
	''' </summary>
	Private Sub SetIsOkToLogon
		If UserName IsNot Nothing And DataSource IsNot Nothing Then
			If UserName.Length > 0 And DataSource.Length > 0 Then
				IsOkToLogOn = True
			Else
				IsOkToLogOn = False
			End If
		Else
			IsOkToLogOn = False
		End If
	End Sub
	
	''' <summary>
	''' Executes the IsConnectionStringOk method from the model and sets the
	''' IsAbleToConnect property based on the result.
	''' </summary>
	Private Sub OnSecurePasswordChanged
		If DatabaseLogOn.Instance.IsConnectionStringOk Then
			_isAbleToConnect = True
		Else
			Password = ""
			_isAbleToConnect = False
		End If
	End Sub
End Class
