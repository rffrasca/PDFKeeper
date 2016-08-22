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

Public Class PdfOwnerPasswordPresentationModel
	Inherits NotifyPropertyChangedBase
	Private _password As String
	Private _okButtonEnabled As Boolean
	
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
	
	Public Property OkButtonEnabled As Boolean
		Get
			Return _okButtonEnabled
		End Get
		Set(ByVal value As Boolean)
			_okButtonEnabled = value
		End Set
	End Property
	
	Private Sub SetOkButtonState
		If Password.Length > 0 Then
			OkButtonEnabled = True
		Else
			OkButtonEnabled = False
		End If
	End Sub
End Class
