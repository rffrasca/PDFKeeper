'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UploadFolderConfigurationDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadFolderConfigurationDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.FolderNameTextBox = New System.Windows.Forms.TextBox()
        Me.FolderNameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TitleComboBox = New System.Windows.Forms.ComboBox()
        Me.FolderNameLabel = New System.Windows.Forms.Label()
        Me.TitlePrefillLabel = New System.Windows.Forms.Label()
        Me.AuthorPrefillLabel = New System.Windows.Forms.Label()
        Me.AuthorComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectPrefillLabel = New System.Windows.Forms.Label()
        Me.SubjectComboBox = New System.Windows.Forms.ComboBox()
        Me.KeywordsPrefillLabel = New System.Windows.Forms.Label()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.FlagDocumentCheckBox = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.FolderNameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'Cancel_Button
        '
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Name = "Cancel_Button"
        '
        'FolderNameTextBox
        '
        resources.ApplyResources(Me.FolderNameTextBox, "FolderNameTextBox")
        Me.FolderNameTextBox.Name = "FolderNameTextBox"
        '
        'FolderNameErrorProvider
        '
        Me.FolderNameErrorProvider.ContainerControl = Me
        '
        'TitleComboBox
        '
        Me.TitleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.TitleComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TitleComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.TitleComboBox, "TitleComboBox")
        Me.TitleComboBox.Name = "TitleComboBox"
        Me.TitleComboBox.Sorted = True
        '
        'FolderNameLabel
        '
        resources.ApplyResources(Me.FolderNameLabel, "FolderNameLabel")
        Me.FolderNameLabel.Name = "FolderNameLabel"
        '
        'TitlePrefillLabel
        '
        resources.ApplyResources(Me.TitlePrefillLabel, "TitlePrefillLabel")
        Me.TitlePrefillLabel.Name = "TitlePrefillLabel"
        '
        'AuthorPrefillLabel
        '
        resources.ApplyResources(Me.AuthorPrefillLabel, "AuthorPrefillLabel")
        Me.AuthorPrefillLabel.Name = "AuthorPrefillLabel"
        '
        'AuthorComboBox
        '
        Me.AuthorComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.AuthorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AuthorComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.AuthorComboBox, "AuthorComboBox")
        Me.AuthorComboBox.Name = "AuthorComboBox"
        Me.AuthorComboBox.Sorted = True
        '
        'SubjectPrefillLabel
        '
        resources.ApplyResources(Me.SubjectPrefillLabel, "SubjectPrefillLabel")
        Me.SubjectPrefillLabel.Name = "SubjectPrefillLabel"
        '
        'SubjectComboBox
        '
        Me.SubjectComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SubjectComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SubjectComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.SubjectComboBox, "SubjectComboBox")
        Me.SubjectComboBox.Name = "SubjectComboBox"
        Me.SubjectComboBox.Sorted = True
        '
        'KeywordsPrefillLabel
        '
        resources.ApplyResources(Me.KeywordsPrefillLabel, "KeywordsPrefillLabel")
        Me.KeywordsPrefillLabel.Name = "KeywordsPrefillLabel"
        '
        'KeywordsTextBox
        '
        resources.ApplyResources(Me.KeywordsTextBox, "KeywordsTextBox")
        Me.KeywordsTextBox.Name = "KeywordsTextBox"
        '
        'FlagDocumentCheckBox
        '
        resources.ApplyResources(Me.FlagDocumentCheckBox, "FlagDocumentCheckBox")
        Me.FlagDocumentCheckBox.Name = "FlagDocumentCheckBox"
        Me.FlagDocumentCheckBox.UseVisualStyleBackColor = True
        '
        'UploadFolderConfigurationDialog
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.FlagDocumentCheckBox)
        Me.Controls.Add(Me.KeywordsTextBox)
        Me.Controls.Add(Me.KeywordsPrefillLabel)
        Me.Controls.Add(Me.SubjectComboBox)
        Me.Controls.Add(Me.SubjectPrefillLabel)
        Me.Controls.Add(Me.AuthorComboBox)
        Me.Controls.Add(Me.AuthorPrefillLabel)
        Me.Controls.Add(Me.TitleComboBox)
        Me.Controls.Add(Me.TitlePrefillLabel)
        Me.Controls.Add(Me.FolderNameTextBox)
        Me.Controls.Add(Me.FolderNameLabel)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UploadFolderConfigurationDialog"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.FolderNameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents FolderNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FolderNameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TitleComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents FolderNameLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents KeywordsPrefillLabel As System.Windows.Forms.Label
    Friend WithEvents SubjectComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectPrefillLabel As System.Windows.Forms.Label
    Friend WithEvents AuthorComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AuthorPrefillLabel As System.Windows.Forms.Label
    Friend WithEvents TitlePrefillLabel As System.Windows.Forms.Label
    Friend WithEvents HelpProvider As System.Windows.Forms.HelpProvider
    Friend WithEvents FlagDocumentCheckBox As System.Windows.Forms.CheckBox

End Class
