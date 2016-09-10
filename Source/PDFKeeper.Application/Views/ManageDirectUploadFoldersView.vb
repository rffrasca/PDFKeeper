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

Public Partial Class ManageDirectUploadFoldersView
	Private presentationModel As _
		New ManageDirectUploadFoldersPresentationModel
	
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
		listBoxFolderNames.DisplayMember = "SelectedFolder"
		listBoxFolderNames.DataSource = DirectUploadFolders.Names
		listBoxFolderNames.DataBindings.Add( _
			"SelectedValue", _
			presentationModel, _
			"SelectedFolder", _
			True, _
			DataSourceUpdateMode.OnPropertyChanged)
		buttonModify.DataBindings.Add( _
			"Enabled", _
			presentationModel, _
			"ModifyButtonEnabled")
		buttonDelete.DataBindings.Add( _
			"Enabled", _
			presentationModel, _
			"DeleteButtonEnabled")
		
		' Added to make sure no list box item is selected. The default is
		' for the first item to be selected, but this doesn't set the
		' SelectedFolder property on the presentaltion model.
		listBoxFolderNames.SelectedIndex = -1
	End Sub
	
	Private Sub ButtonAddClick(sender As Object, e As EventArgs)
		presentationModel.AddButtonClicked
	End Sub
	
	Private Sub ButtonModifyClick(sender As Object, e As EventArgs)
		presentationModel.ModifyButtonClicked
	End Sub
	
	Private Sub ButtonDeleteClick(sender As Object, e As EventArgs)
		presentationModel.DeleteButtonClicked
	End Sub
	
	Private Sub LinkLabelHelpLinkClicked( _
		sender As Object, _
		e As LinkLabelLinkClickedEventArgs)
		msgbox("Not Implemented.")
	End Sub
End Class
