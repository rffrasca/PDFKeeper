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

Public Class ManageDirectUploadFoldersPresentationModel
	Inherits NotifyPropertyChangedBase
	Private selectedUploadFolder As String
	Private selectedXmlFile As String
	Private _selectedFolder As String
	Private _modifyButtonEnabled As Boolean
	Private _deleteButtonEnabled As Boolean
	
	Public Property SelectedFolder As String
		Get
			Return _selectedFolder
		End Get
		Set(ByVal value As String)
			_selectedFolder = value
			If _selectedFolder IsNot Nothing Then
				selectedUploadFolder = Path.Combine( _
					ApplicationProfileFolders.DirectUpload, _
					_selectedFolder)
				selectedXmlFile = Path.Combine( _
					ApplicationProfileFolders.DirectUploadXml, _
					_selectedFolder & ".xml")
			Else
				selectedUploadFolder = Nothing
				selectedXmlFile = Nothing
			End If
			SetButtonsState
			OnPropertyChanged("SelectedFolder")
		End Set
	End Property
	
	Public Property ModifyButtonEnabled As Boolean
		Get
			Return _modifyButtonEnabled
		End Get
		Set(ByVal value As Boolean)
			_modifyButtonEnabled = value
		End Set
	End Property
	
	Public Property DeleteButtonEnabled As Boolean
		Get
			Return _deleteButtonEnabled
		End Get
		Set(ByVal value As Boolean)
			_deleteButtonEnabled = value
		End Set
	End Property
	
	''' <summary>
	''' When a folder name is selected, enable the Modify button; and the
	''' Delete button, only when the selected folder does not contain PDF
	''' files.
	''' </summary>
	Private Sub SetButtonsState
		If SelectedFolder IsNot Nothing Then
			ModifyButtonEnabled = True
			If CountOfFilesInfolder(selectedUploadFolder, "pdf") = 0 Then
				DeleteButtonEnabled = True
			Else
				DeleteButtonEnabled = False
			End If
		Else
			ModifyButtonEnabled = False
			DeleteButtonEnabled = False
		End If
	End Sub
	
	Public Sub AddButtonClicked
		MsgBox("Not Implemented.")
		'		MsgBox(_selectedFolder)
'		_selectedFolder = DirectUploadFolders.Names.Item(0).ToString
	End Sub
	
	Public Sub ModifyButtonClicked
		MsgBox("Not Implemented.")
	End Sub
	
	Public Sub DeleteButtonClicked
		If ShowQuestion( _
			String.Format( _
			CultureInfo.CurrentCulture, _
			PdfKeeper.Strings.DeleteFolderQuestion, _
			selectedFolder)) = 6 Then ' True
			System.IO.File.Delete(selectedXmlFile)
			DirectUploadFolders.Refresh
			If DirectUploadFolders.Names.Count > 0 Then
				SelectedFolder = DirectUploadFolders.Names.Item(0).ToString
			Else
				SelectedFolder = Nothing		
			End If
		End If
	End Sub
End Class
