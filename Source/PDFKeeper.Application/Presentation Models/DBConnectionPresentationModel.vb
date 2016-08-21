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

Public Class DBConnectionPresentationModel
	Inherits NotifyPropertyChangedBase
	
	Private readonly xmlFile As String = Path.Combine( _
		ApplicationProfileFolders.RoamingParent, "DBConnection.xml")
	Private dbConnection As New DBConnection
	
	' String members must be set to an empty string to prevent a
	' NullReferenceException from being thrown when SetOkButtonState executes.
	Private _userName As String = String.Empty
	Private _password As String = String.Empty
	Private _dataSource As String = String.Empty
	Private _okButtonEnabled As Boolean
	
	''' <summary>
	''' Constructor deserializes the DBConnection model UserName and DataSource
	''' properties with data from the XML file, and then sets cooresponding
	''' properties on this class to update the view.
	''' </summary>
	Public Sub New()
		Serializer.DeserializeFromXml(dbConnection, xmlFile)
		UserName = dbConnection.UserName
		DataSource = dbConnection.DataSource
	End Sub
	
	Public Property UserName As String
		Get
			Return _userName
		End Get
		Set(ByVal value As String)
			_userName = value.Trim
			dbConnection.UserName = _userName
			OnPropertyChanged("UserName")
			SetOkButtonState
		End Set
	End Property
	
	''' <summary>
	''' This property does not store the password, only a string of asterisks
	''' equal in length to the actual password.
	''' </summary>
	Public Property Password As String
		Get
			Return _password
		End Get
		Set(ByVal value As String)
			_password = value
			' Added to send a property change notification only when property
			' is set to an empty string to clear the TextBox.
			If _password = String.Empty Then
				OnPropertyChanged("Password")
			End If
			SetOkButtonState
		End Set
	End Property
		
	Public Property DataSource As String
		Get
			Return _dataSource
		End Get
		Set(ByVal value As String)
			_dataSource = value.Trim
			dbConnection.DataSource = _dataSource
			OnPropertyChanged("DataSource")
			SetOkButtonState
		End Set
	End Property
	
	Public Property OkButtonEnabled As Boolean
		Get
			Return _okButtonEnabled
		End Get
		Set(ByVal value As Boolean)
			_okButtonEnabled = value
		End Set
	End Property
	
	Private Sub SetOkButtonState
		If UserName.Length > 0 And _
			Password.Length > 0 And _
			DataSource.Length > 0 Then
			OkButtonEnabled = True
		Else
			OkButtonEnabled = False
		End If
	End Sub
	
	''' <summary>
	''' Sets the password property on the DBConnection model, performs a
	''' database test connection, and then serializes the UserName and
	''' DataSource properties from the DBConnection model to an XML file.
	''' </summary>
	''' <param name="passwordParam">
	''' SecureString object containing the password.
	''' </param>
	''' <returns>True for success or False for failure.</returns>
	Friend Function OkButtonClicked(passwordParam As SecureString) As Boolean
		DBConnection.Password = passwordParam
		If DBConnectionUtil.TestConnection Then
			Serializer.SerializeToXml(dbConnection, xmlFile)
			Return True
		Else
			' On failure, the Password TextBox on the view needs to be cleared.
			Password = String.Empty
			Return False
		End If
	End Function
End Class
