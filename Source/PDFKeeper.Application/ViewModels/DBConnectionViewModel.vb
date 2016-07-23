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
	Private _dataSource As String = String.Empty
	Private _okEnabled As Boolean
	
	''' <summary>
	''' Constructor gets LastUserName and LastDataSource from the UserSettings
	''' object.
	''' </summary>
	Public Sub New()
		_userName = UserSettings.Instance.LastUserName
		_dataSource = UserSettings.Instance.LastDataSource
	End Sub
	
	''' <summary>
	''' Gets/Sets the User Name for the Database Connection and is bound to the
	''' view.
	''' </summary>
	Public Property UserName As String
		Get
			Return _userName
		End Get
		Set(ByVal value As String)
			_userName = value.Trim
			SetOkEnabled
			OnPropertyChanged("UserName")
		End Set
	End Property
		
	''' <summary>
	''' Gets/Sets the Password string as asterisks and is bound to the view.
	''' </summary>
	Public Property Password As String
		Get
			Return _password
		End Get
		Set(ByVal value As String)
			_password = value.Trim
			SetOkEnabled
			OnPropertyChanged("Password")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the Data Source for the Database Connection bound to the
	''' view.
	''' </summary>
	Public Property DataSource As String
		Get
			Return _dataSource
		End Get
		Set(ByVal value As String)
			_dataSource = value.Trim
			SetOkEnabled
			OnPropertyChanged("DataSource")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the OK button enabled state and is bound to the view.
	''' </summary>
	Public Property OkEnabled As Boolean
		Get
			Return _okEnabled
		End Get
		Set(ByVal value As Boolean)
			_okEnabled = value
			OnPropertyChanged("OkEnabled")
		End Set
	End Property
	
	''' <summary>
	''' Sets OkEnabled to True when the length of UserName, Password, and
	''' DataSource are > 0; otherwise, sets OkEnabled = False.
	''' </summary>
	Private Sub SetOkEnabled
		If UserName.Length > 0 And _
			Password.Length > 0 And _
			DataSource.Length > 0 Then
			
			OkEnabled = True
		Else
			OkEnabled = False
		End If
	End Sub
	
	''' <summary>
	''' Invokes SetModel from this class, and then TestConnection from the
	''' model.
	''' </summary>
	''' <param name="secureText">SecureString object.</param>
	''' <returns>True or False for successful.</returns>
	Public Function OkClicked(ByVal secureText As SecureString) As Boolean
		SetModel(secureText)
		If DBConnection.Instance.TestConnection Then
			Return True
		Else
			Password = String.Empty
			Return False
		End If
	End Function
	
	''' <summary>
	''' Sets UserName, SecurePassword, and DataSource on the model.
	''' </summary>
	''' <param name="secureText">SecureString object.</param>
	Private Sub SetModel(ByVal secureText As SecureString)
		DBConnection.Instance.UserName = UserName
		DBConnection.Instance.SecurePassword = secureText
		DBConnection.Instance.DataSource = DataSource
	End Sub
End Class
