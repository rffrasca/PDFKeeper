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

Partial Class ManageDirectUploadFoldersView
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManageDirectUploadFoldersView))
		Me.labelFolderNames = New System.Windows.Forms.Label()
		Me.listBoxFolderNames = New System.Windows.Forms.ListBox()
		Me.linkLabelHelp = New System.Windows.Forms.LinkLabel()
		Me.buttonAdd = New System.Windows.Forms.Button()
		Me.buttonModify = New System.Windows.Forms.Button()
		Me.buttonDelete = New System.Windows.Forms.Button()
		Me.SuspendLayout
		'
		'labelFolderNames
		'
		resources.ApplyResources(Me.labelFolderNames, "labelFolderNames")
		Me.labelFolderNames.Name = "labelFolderNames"
		'
		'listBoxFolderNames
		'
		Me.listBoxFolderNames.FormattingEnabled = true
		resources.ApplyResources(Me.listBoxFolderNames, "listBoxFolderNames")
		Me.listBoxFolderNames.Name = "listBoxFolderNames"
		'
		'linkLabelHelp
		'
		resources.ApplyResources(Me.linkLabelHelp, "linkLabelHelp")
		Me.linkLabelHelp.Name = "linkLabelHelp"
		Me.linkLabelHelp.TabStop = true
		AddHandler Me.linkLabelHelp.LinkClicked, AddressOf Me.LinkLabelHelpLinkClicked
		'
		'buttonAdd
		'
		resources.ApplyResources(Me.buttonAdd, "buttonAdd")
		Me.buttonAdd.Name = "buttonAdd"
		Me.buttonAdd.UseVisualStyleBackColor = true
		AddHandler Me.buttonAdd.Click, AddressOf Me.ButtonAddClick
		'
		'buttonModify
		'
		resources.ApplyResources(Me.buttonModify, "buttonModify")
		Me.buttonModify.Name = "buttonModify"
		Me.buttonModify.UseVisualStyleBackColor = true
		AddHandler Me.buttonModify.Click, AddressOf Me.ButtonModifyClick
		'
		'buttonDelete
		'
		resources.ApplyResources(Me.buttonDelete, "buttonDelete")
		Me.buttonDelete.Name = "buttonDelete"
		Me.buttonDelete.UseVisualStyleBackColor = true
		AddHandler Me.buttonDelete.Click, AddressOf Me.ButtonDeleteClick
		'
		'ManageDirectUploadFoldersView
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.buttonDelete)
		Me.Controls.Add(Me.buttonModify)
		Me.Controls.Add(Me.buttonAdd)
		Me.Controls.Add(Me.linkLabelHelp)
		Me.Controls.Add(Me.listBoxFolderNames)
		Me.Controls.Add(Me.labelFolderNames)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "ManageDirectUploadFoldersView"
		Me.ResumeLayout(false)
	End Sub
	Private buttonDelete As System.Windows.Forms.Button
	Private buttonModify As System.Windows.Forms.Button
	Private buttonAdd As System.Windows.Forms.Button
	Private linkLabelHelp As System.Windows.Forms.LinkLabel
	Private listBoxFolderNames As System.Windows.Forms.ListBox
	Private labelFolderNames As System.Windows.Forms.Label
End Class
