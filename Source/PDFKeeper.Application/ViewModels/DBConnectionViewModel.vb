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
	Private _password As String
	Private _readyToConnect As Boolean
	
	''' <summary>
	''' Gets/Sets the database connection user name and is a binder between the
	''' view and the model.
	''' </summary>
	Public Property UserName As String
		Get
			SetReadyToConnect
			Return DBConnection.Instance.UserName
		End Get
		Set(ByVal value As String)
			DBConnection.Instance.UserName = value.Trim
			SetReadyToConnect
			OnPropertyChanged("UserName")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the password string of asterisks and is bound to the view.
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
	''' Gets/Sets the database connection secure password object.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public Property SecurePassword As SecureString
		Get
			Return DBConnection.Instance.SecurePassword
		End Get
		Set(ByVal value As SecureString)
			DBConnection.Instance.SecurePassword = value
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the database connection data source and is a binder between
	''' the view and the model. 
	''' </summary>
	Public Property DataSource As String
		Get
			SetReadyToConnect
			Return DBConnection.Instance.DataSource
		End Get
		Set(ByVal value As String)
			DBConnection.Instance.DataSource = value.Trim
			SetReadyToConnect
			OnPropertyChanged("UserName")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the ready to connect status and is a binder between the view
	''' and the model.
	''' </summary>
	Public Property ReadyToConnect As Boolean
		Get
			return _readyToConnect
		End Get
		Set(ByVal value As Boolean)
			_readyToConnect = value
			OnPropertyChanged("ReadyToConnect")
		End Set
	End Property
	
	''' <summary>
	''' Gets/Sets the connection validation status and is used by the view. It
	''' also sets the Password property to an empty string when the validation
	''' status is false to clear the Password text box on the view.
	''' </summary>
	<System.Diagnostics.CodeAnalysis.SuppressMessage( _
		"Microsoft.Performance", _
		"CA1822:MarkMembersAsStatic")> _
	Public ReadOnly Property ConnectionValidated As Boolean
		Get
			Dim result As Boolean = DBConnection.Instance.ConnectionValidated
			If result = False Then
				Password = ""
			End If
			Return result
		End Get
	End Property
	
	''' <summary>
	''' Sets the ready to connect status to the value of the model.
	''' </summary>
	Private Sub SetReadyToConnect
		' This if check is needed to avoid a StackOverflowException.
		If ReadyToConnect <> DBConnection.Instance.ReadyToConnect Then
			If DBConnection.Instance.ReadyToConnect
				ReadyToConnect = True
			Else
				ReadyToConnect = False
			End If
		End If
	End Sub
End Class
