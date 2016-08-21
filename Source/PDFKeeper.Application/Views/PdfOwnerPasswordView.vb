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
'* Time: 11:54 AM
'*
'******************************************************************************

Public Partial Class PdfOwnerPasswordView
	'Private viewModel As New PdfOwnerPasswordViewModel
	'Private _securePassword As SecureString
	
	Public Sub New()
		Dim systemFont As System.Drawing.Font = SystemFonts.MessageBoxFont
		Me.Font = New System.Drawing.Font( _
			systemFont.Name, _
			systemFont.Size, _
			systemFont.Style)
		Me.InitializeComponent()
		'InitializeDataBindings
	End Sub
	
	''' <summary>
	''' Gets the Secure Password as a secure string object that gets set when
	''' the OK button is clicked.
	''' </summary>
'	Public ReadOnly Property SecurePassword As SecureString
'		Get
'			Return _securePassword
'		End Get
'	End Property
	
	'Private Sub InitializeDataBindings
'		textBoxSecure.DataBindings.Add( _
'			"Text", _
'			viewModel, _
'			"Password", _
'			False, _
'			Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
'		buttonOK.DataBindings.Add("Enabled", viewModel, "OkEnabled")
	'End Sub
	
	Private Sub ButtonOKClick(sender As Object, e As EventArgs)
'		_securePassword = TextBoxSecure.SecureText
	End Sub
End Class
