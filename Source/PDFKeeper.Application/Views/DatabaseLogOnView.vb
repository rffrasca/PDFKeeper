'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
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

Public Partial Class DatabaseLogOnView
	Private viewModel As New DatabaseLogOnViewModel
	Friend Shared dbPassword As New SecureString
		
	Public Sub New()
		Me.InitializeComponent()
		Font = SystemFonts.MessageBoxFont
		InitializeDataBindings
		viewModel.GetLastLogOnUserNameAndDataSource
	End Sub
	
	Private Sub InitializeDataBindings
		textBoxUserName.DataBindings.Add( _
			"Text", _
			viewModel, _
			"UserName", _
			False, _
			Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
		textBoxSecure.DataBindings.Add( _
			"Text", _
			viewModel, _
			"Password", _
			False, _
			Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
		textBoxDataSource.DataBindings.Add( _
			"Text", _
			viewModel, _
			"DataSource", _
			False, _
			Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
		buttonOK.DataBindings.Add("Enabled", viewModel, "IsOkToLogOn")
	End Sub
	
	Private Sub DatabaseLogOnViewHelpRequested( _
		sender As Object, _
		hlpevent As HelpEventArgs)
		
		HelpUtil.ShowHelp(Me, ActiveForm.Name)
	End Sub
	
	Private Sub ButtonOkClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		viewModel.SecurePassword = TextBoxSecure.SecureText
		Me.Cursor = Cursors.Default
		If viewModel.IsAbleToConnect = False Then
			TextBoxSecure.SecureText.Clear
			textBoxUsername.Select
			Exit Sub
		End If
		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub
End Class
