' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2024 Robert F. Frasca
' *
' * This file is part of PDFKeeper.
' *
' * PDFKeeper is free software: you can redistribute it and/or modify it
' * under the terms of the GNU General Public License as published by the
' * Free Software Foundation, either version 3 of the License, or (at your
' * option) any later version.
' *
' * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
' * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
' * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
' * more details.
' *
' * You should have received a copy of the GNU General Public License along
' * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
' *****************************************************************************

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UploadProfilesView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadProfilesView))
        Me.UploadProfileNamesListBox = New System.Windows.Forms.ListBox()
        Me.UploadProfilesViewModelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AddButton = New System.Windows.Forms.Button()
        Me.EditButton = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.UploadProfilesFileSystemWatcher = New System.IO.FileSystemWatcher()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        CType(Me.UploadProfilesViewModelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UploadProfilesFileSystemWatcher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UploadProfileNamesListBox
        '
        Me.UploadProfileNamesListBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.UploadProfilesViewModelBindingSource, "CurrentUploadProfileName", True))
        Me.UploadProfileNamesListBox.DataBindings.Add(New System.Windows.Forms.Binding("DataSource", Me.UploadProfilesViewModelBindingSource, "UploadProfileNames", True))
        Me.UploadProfileNamesListBox.FormattingEnabled = True
        resources.ApplyResources(Me.UploadProfileNamesListBox, "UploadProfileNamesListBox")
        Me.UploadProfileNamesListBox.Name = "UploadProfileNamesListBox"
        Me.UploadProfileNamesListBox.Sorted = True
        '
        'UploadProfilesViewModelBindingSource
        '
        Me.UploadProfilesViewModelBindingSource.DataSource = GetType(PDFKeeper.Core.ViewModels.UploadProfilesViewModel)
        '
        'AddButton
        '
        resources.ApplyResources(Me.AddButton, "AddButton")
        Me.AddButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.database_add
        Me.AddButton.Name = "AddButton"
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'EditButton
        '
        resources.ApplyResources(Me.EditButton, "EditButton")
        Me.EditButton.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.UploadProfilesViewModelBindingSource, "EditEnabled", True))
        Me.EditButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.database_edit
        Me.EditButton.Name = "EditButton"
        Me.EditButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        resources.ApplyResources(Me.DeleteButton, "DeleteButton")
        Me.DeleteButton.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.UploadProfilesViewModelBindingSource, "DeleteEnabled", True))
        Me.DeleteButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.database_delete
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'UploadProfilesFileSystemWatcher
        '
        Me.UploadProfilesFileSystemWatcher.EnableRaisingEvents = True
        Me.UploadProfilesFileSystemWatcher.Filter = "*.xml"
        Me.UploadProfilesFileSystemWatcher.SynchronizingObject = Me
        '
        'UploadProfilesView
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.EditButton)
        Me.Controls.Add(Me.AddButton)
        Me.Controls.Add(Me.UploadProfileNamesListBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UploadProfilesView"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        CType(Me.UploadProfilesViewModelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UploadProfilesFileSystemWatcher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UploadProfileNamesListBox As ListBox
    Friend WithEvents AddButton As Button
    Friend WithEvents EditButton As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents UploadProfilesFileSystemWatcher As FileSystemWatcher
    Friend WithEvents UploadProfilesViewModelBindingSource As BindingSource
    Friend WithEvents HelpProvider As HelpProvider
End Class
