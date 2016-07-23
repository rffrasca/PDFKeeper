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
'* Date: 7/17/2016
'* Time: 11:51 AM
'*
'******************************************************************************

Public Class PdfOwnerPasswordViewModel
	Inherits ViewModelBase
	Private _password As String = String.Empty
	Private _okEnabled As Boolean
	
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
	''' Sets OkEnabled to True when the length of Password is > 0; otherwise,
	''' sets OkEnabled to False.
	''' </summary>
	Private Sub SetOkEnabled
		If Password.Length > 0 Then			
			OkEnabled = True
		Else
			OkEnabled = False
		End If
	End Sub
End Class
