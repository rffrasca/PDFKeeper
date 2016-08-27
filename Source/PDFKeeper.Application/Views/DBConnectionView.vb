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

Public Partial Class DBConnectionView
	Private presentationModel As New DBConnectionPresentationModel
	
	Public Sub New()
		Dim systemFont As System.Drawing.Font = SystemFonts.MessageBoxFont
		Me.Font = New System.Drawing.Font( _
			systemFont.Name, _
			systemFont.Size, _
			systemFont.Style)
		Me.InitializeComponent()
		InitializeDataBindings
	End Sub
	
	Private Sub InitializeDataBindings
		textBoxUserName.DataBindings.Add( _
			"Text", _
			presentationModel, _
			"UserName", _
			False, _
			Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
		secureTextBox.DataBindings.Add( _
			"Text", _
			presentationModel, _
			"Password", _
			False, _
			Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
		textBoxDataSource.DataBindings.Add( _
			"Text", _
			presentationModel, _
			"DataSource", _
			False, _
			Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
		buttonOK.DataBindings.Add( _
			"Enabled", _
			presentationModel, _
			"OkButtonEnabled")
	End Sub
	
	Private Sub DBConnectionViewHelpRequested( _
		sender As Object, _
		hlpevent As HelpEventArgs)
		HelpTopic.Show(Me, ActiveForm.Name)
	End Sub
	
	Private Sub LinkLabelHelpLinkClicked( _
		sender As Object, _
		e As LinkLabelLinkClickedEventArgs)
		HelpTopic.Show(Me, ActiveForm.Name)
	End Sub
	
	Private Sub ButtonOKClick(sender As Object, e As EventArgs)
		Me.Cursor = Cursors.WaitCursor
		If presentationModel.OkButtonClicked(SecureTextBox.SecureText) Then
			Me.DialogResult = Windows.Forms.DialogResult.OK
		Else
			Me.Cursor = Cursors.Default
			SecureTextBox.SecureText.Clear
			textBoxUserName.Select
		End If
	End Sub
End Class
