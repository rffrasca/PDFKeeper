'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2021 Robert F. Frasca
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
Partial Class ManageUploadFolderConfigurationsDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                commonPresenter.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManageUploadFolderConfigurationsDialog))
        Me.UploadFolderConfigurationsGroupBox = New System.Windows.Forms.GroupBox()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.EditButton = New System.Windows.Forms.Button()
        Me.NewButton = New System.Windows.Forms.Button()
        Me.UploadFolderConfigurationsComboBox = New System.Windows.Forms.ComboBox()
        Me.FolderNameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.FolderNameTextBox = New System.Windows.Forms.TextBox()
        Me.FolderNameLabel = New System.Windows.Forms.Label()
        Me.TitleComboBox = New System.Windows.Forms.ComboBox()
        Me.TitlePrefillLabel = New System.Windows.Forms.Label()
        Me.CategoryComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryPrefillLabel = New System.Windows.Forms.Label()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.KeywordsPrefillLabel = New System.Windows.Forms.Label()
        Me.SubjectPairedComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectPrefillLabel = New System.Windows.Forms.Label()
        Me.AuthorPairedComboBox = New System.Windows.Forms.ComboBox()
        Me.AuthorPrefillLabel = New System.Windows.Forms.Label()
        Me.FlagDocumentCheckBox = New System.Windows.Forms.CheckBox()
        Me.UploadFolderConfigurationGroupBox = New System.Windows.Forms.GroupBox()
        Me.DiscardButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.UploadFolderConfigurationsGroupBox.SuspendLayout()
        CType(Me.FolderNameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UploadFolderConfigurationGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'UploadFolderConfigurationsGroupBox
        '
        Me.UploadFolderConfigurationsGroupBox.Controls.Add(Me.DeleteButton)
        Me.UploadFolderConfigurationsGroupBox.Controls.Add(Me.EditButton)
        Me.UploadFolderConfigurationsGroupBox.Controls.Add(Me.NewButton)
        Me.UploadFolderConfigurationsGroupBox.Controls.Add(Me.UploadFolderConfigurationsComboBox)
        resources.ApplyResources(Me.UploadFolderConfigurationsGroupBox, "UploadFolderConfigurationsGroupBox")
        Me.UploadFolderConfigurationsGroupBox.Name = "UploadFolderConfigurationsGroupBox"
        Me.UploadFolderConfigurationsGroupBox.TabStop = False
        '
        'DeleteButton
        '
        resources.ApplyResources(Me.DeleteButton, "DeleteButton")
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'EditButton
        '
        resources.ApplyResources(Me.EditButton, "EditButton")
        Me.EditButton.Name = "EditButton"
        Me.EditButton.UseVisualStyleBackColor = True
        '
        'NewButton
        '
        resources.ApplyResources(Me.NewButton, "NewButton")
        Me.NewButton.Name = "NewButton"
        Me.NewButton.UseVisualStyleBackColor = True
        '
        'UploadFolderConfigurationsComboBox
        '
        Me.UploadFolderConfigurationsComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.UploadFolderConfigurationsComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.UploadFolderConfigurationsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UploadFolderConfigurationsComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.UploadFolderConfigurationsComboBox, "UploadFolderConfigurationsComboBox")
        Me.UploadFolderConfigurationsComboBox.Name = "UploadFolderConfigurationsComboBox"
        Me.UploadFolderConfigurationsComboBox.Sorted = True
        '
        'FolderNameErrorProvider
        '
        Me.FolderNameErrorProvider.ContainerControl = Me
        '
        'FolderNameTextBox
        '
        Me.HelpProvider.SetHelpKeyword(Me.FolderNameTextBox, resources.GetString("FolderNameTextBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.FolderNameTextBox, CType(resources.GetObject("FolderNameTextBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.FolderNameTextBox, "FolderNameTextBox")
        Me.FolderNameTextBox.Name = "FolderNameTextBox"
        Me.HelpProvider.SetShowHelp(Me.FolderNameTextBox, CType(resources.GetObject("FolderNameTextBox.ShowHelp"), Boolean))
        '
        'FolderNameLabel
        '
        resources.ApplyResources(Me.FolderNameLabel, "FolderNameLabel")
        Me.HelpProvider.SetHelpKeyword(Me.FolderNameLabel, resources.GetString("FolderNameLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.FolderNameLabel, CType(resources.GetObject("FolderNameLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.FolderNameLabel.Name = "FolderNameLabel"
        Me.HelpProvider.SetShowHelp(Me.FolderNameLabel, CType(resources.GetObject("FolderNameLabel.ShowHelp"), Boolean))
        '
        'TitleComboBox
        '
        Me.TitleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.TitleComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TitleComboBox.FormattingEnabled = True
        Me.HelpProvider.SetHelpKeyword(Me.TitleComboBox, resources.GetString("TitleComboBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.TitleComboBox, CType(resources.GetObject("TitleComboBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.TitleComboBox, "TitleComboBox")
        Me.TitleComboBox.Name = "TitleComboBox"
        Me.HelpProvider.SetShowHelp(Me.TitleComboBox, CType(resources.GetObject("TitleComboBox.ShowHelp"), Boolean))
        Me.TitleComboBox.Sorted = True
        '
        'TitlePrefillLabel
        '
        resources.ApplyResources(Me.TitlePrefillLabel, "TitlePrefillLabel")
        Me.HelpProvider.SetHelpKeyword(Me.TitlePrefillLabel, resources.GetString("TitlePrefillLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.TitlePrefillLabel, CType(resources.GetObject("TitlePrefillLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.TitlePrefillLabel.Name = "TitlePrefillLabel"
        Me.HelpProvider.SetShowHelp(Me.TitlePrefillLabel, CType(resources.GetObject("TitlePrefillLabel.ShowHelp"), Boolean))
        '
        'CategoryComboBox
        '
        Me.CategoryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CategoryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CategoryComboBox.FormattingEnabled = True
        Me.HelpProvider.SetHelpKeyword(Me.CategoryComboBox, resources.GetString("CategoryComboBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.CategoryComboBox, CType(resources.GetObject("CategoryComboBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.CategoryComboBox, "CategoryComboBox")
        Me.CategoryComboBox.Name = "CategoryComboBox"
        Me.HelpProvider.SetShowHelp(Me.CategoryComboBox, CType(resources.GetObject("CategoryComboBox.ShowHelp"), Boolean))
        Me.CategoryComboBox.Sorted = True
        '
        'CategoryPrefillLabel
        '
        resources.ApplyResources(Me.CategoryPrefillLabel, "CategoryPrefillLabel")
        Me.HelpProvider.SetHelpKeyword(Me.CategoryPrefillLabel, resources.GetString("CategoryPrefillLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.CategoryPrefillLabel, CType(resources.GetObject("CategoryPrefillLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.CategoryPrefillLabel.Name = "CategoryPrefillLabel"
        Me.HelpProvider.SetShowHelp(Me.CategoryPrefillLabel, CType(resources.GetObject("CategoryPrefillLabel.ShowHelp"), Boolean))
        '
        'KeywordsTextBox
        '
        Me.HelpProvider.SetHelpKeyword(Me.KeywordsTextBox, resources.GetString("KeywordsTextBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.KeywordsTextBox, CType(resources.GetObject("KeywordsTextBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.KeywordsTextBox, "KeywordsTextBox")
        Me.KeywordsTextBox.Name = "KeywordsTextBox"
        Me.HelpProvider.SetShowHelp(Me.KeywordsTextBox, CType(resources.GetObject("KeywordsTextBox.ShowHelp"), Boolean))
        '
        'KeywordsPrefillLabel
        '
        resources.ApplyResources(Me.KeywordsPrefillLabel, "KeywordsPrefillLabel")
        Me.HelpProvider.SetHelpKeyword(Me.KeywordsPrefillLabel, resources.GetString("KeywordsPrefillLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.KeywordsPrefillLabel, CType(resources.GetObject("KeywordsPrefillLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.KeywordsPrefillLabel.Name = "KeywordsPrefillLabel"
        Me.HelpProvider.SetShowHelp(Me.KeywordsPrefillLabel, CType(resources.GetObject("KeywordsPrefillLabel.ShowHelp"), Boolean))
        '
        'SubjectPairedComboBox
        '
        Me.SubjectPairedComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SubjectPairedComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SubjectPairedComboBox.FormattingEnabled = True
        Me.HelpProvider.SetHelpKeyword(Me.SubjectPairedComboBox, resources.GetString("SubjectPairedComboBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.SubjectPairedComboBox, CType(resources.GetObject("SubjectPairedComboBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.SubjectPairedComboBox, "SubjectPairedComboBox")
        Me.SubjectPairedComboBox.Name = "SubjectPairedComboBox"
        Me.HelpProvider.SetShowHelp(Me.SubjectPairedComboBox, CType(resources.GetObject("SubjectPairedComboBox.ShowHelp"), Boolean))
        Me.SubjectPairedComboBox.Sorted = True
        '
        'SubjectPrefillLabel
        '
        resources.ApplyResources(Me.SubjectPrefillLabel, "SubjectPrefillLabel")
        Me.HelpProvider.SetHelpKeyword(Me.SubjectPrefillLabel, resources.GetString("SubjectPrefillLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.SubjectPrefillLabel, CType(resources.GetObject("SubjectPrefillLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.SubjectPrefillLabel.Name = "SubjectPrefillLabel"
        Me.HelpProvider.SetShowHelp(Me.SubjectPrefillLabel, CType(resources.GetObject("SubjectPrefillLabel.ShowHelp"), Boolean))
        '
        'AuthorPairedComboBox
        '
        Me.AuthorPairedComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.AuthorPairedComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AuthorPairedComboBox.FormattingEnabled = True
        Me.HelpProvider.SetHelpKeyword(Me.AuthorPairedComboBox, resources.GetString("AuthorPairedComboBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.AuthorPairedComboBox, CType(resources.GetObject("AuthorPairedComboBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.AuthorPairedComboBox, "AuthorPairedComboBox")
        Me.AuthorPairedComboBox.Name = "AuthorPairedComboBox"
        Me.HelpProvider.SetShowHelp(Me.AuthorPairedComboBox, CType(resources.GetObject("AuthorPairedComboBox.ShowHelp"), Boolean))
        Me.AuthorPairedComboBox.Sorted = True
        '
        'AuthorPrefillLabel
        '
        resources.ApplyResources(Me.AuthorPrefillLabel, "AuthorPrefillLabel")
        Me.HelpProvider.SetHelpKeyword(Me.AuthorPrefillLabel, resources.GetString("AuthorPrefillLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.AuthorPrefillLabel, CType(resources.GetObject("AuthorPrefillLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.AuthorPrefillLabel.Name = "AuthorPrefillLabel"
        Me.HelpProvider.SetShowHelp(Me.AuthorPrefillLabel, CType(resources.GetObject("AuthorPrefillLabel.ShowHelp"), Boolean))
        '
        'FlagDocumentCheckBox
        '
        resources.ApplyResources(Me.FlagDocumentCheckBox, "FlagDocumentCheckBox")
        Me.HelpProvider.SetHelpKeyword(Me.FlagDocumentCheckBox, resources.GetString("FlagDocumentCheckBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.FlagDocumentCheckBox, CType(resources.GetObject("FlagDocumentCheckBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.FlagDocumentCheckBox.Name = "FlagDocumentCheckBox"
        Me.HelpProvider.SetShowHelp(Me.FlagDocumentCheckBox, CType(resources.GetObject("FlagDocumentCheckBox.ShowHelp"), Boolean))
        Me.FlagDocumentCheckBox.UseVisualStyleBackColor = True
        '
        'UploadFolderConfigurationGroupBox
        '
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.DiscardButton)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.SaveButton)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.FlagDocumentCheckBox)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.CategoryComboBox)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.CategoryPrefillLabel)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.KeywordsTextBox)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.KeywordsPrefillLabel)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.SubjectPairedComboBox)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.SubjectPrefillLabel)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.AuthorPairedComboBox)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.AuthorPrefillLabel)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.TitleComboBox)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.TitlePrefillLabel)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.FolderNameTextBox)
        Me.UploadFolderConfigurationGroupBox.Controls.Add(Me.FolderNameLabel)
        Me.HelpProvider.SetHelpKeyword(Me.UploadFolderConfigurationGroupBox, resources.GetString("UploadFolderConfigurationGroupBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.UploadFolderConfigurationGroupBox, CType(resources.GetObject("UploadFolderConfigurationGroupBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.UploadFolderConfigurationGroupBox, "UploadFolderConfigurationGroupBox")
        Me.UploadFolderConfigurationGroupBox.Name = "UploadFolderConfigurationGroupBox"
        Me.HelpProvider.SetShowHelp(Me.UploadFolderConfigurationGroupBox, CType(resources.GetObject("UploadFolderConfigurationGroupBox.ShowHelp"), Boolean))
        Me.UploadFolderConfigurationGroupBox.TabStop = False
        '
        'DiscardButton
        '
        Me.HelpProvider.SetHelpKeyword(Me.DiscardButton, resources.GetString("DiscardButton.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.DiscardButton, CType(resources.GetObject("DiscardButton.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.DiscardButton, "DiscardButton")
        Me.DiscardButton.Name = "DiscardButton"
        Me.HelpProvider.SetShowHelp(Me.DiscardButton, CType(resources.GetObject("DiscardButton.ShowHelp"), Boolean))
        Me.DiscardButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.HelpProvider.SetHelpKeyword(Me.SaveButton, resources.GetString("SaveButton.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.SaveButton, CType(resources.GetObject("SaveButton.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.SaveButton, "SaveButton")
        Me.SaveButton.Name = "SaveButton"
        Me.HelpProvider.SetShowHelp(Me.SaveButton, CType(resources.GetObject("SaveButton.ShowHelp"), Boolean))
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ManageUploadFolderConfigurationsDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UploadFolderConfigurationGroupBox)
        Me.Controls.Add(Me.UploadFolderConfigurationsGroupBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ManageUploadFolderConfigurationsDialog"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.UploadFolderConfigurationsGroupBox.ResumeLayout(False)
        CType(Me.FolderNameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UploadFolderConfigurationGroupBox.ResumeLayout(False)
        Me.UploadFolderConfigurationGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UploadFolderConfigurationsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents UploadFolderConfigurationsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents EditButton As System.Windows.Forms.Button
    Friend WithEvents NewButton As System.Windows.Forms.Button
    Friend WithEvents FolderNameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents HelpProvider As System.Windows.Forms.HelpProvider
    Friend WithEvents UploadFolderConfigurationGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents FolderNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FolderNameLabel As System.Windows.Forms.Label
    Friend WithEvents TitleComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TitlePrefillLabel As System.Windows.Forms.Label
    Friend WithEvents CategoryComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CategoryPrefillLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents KeywordsPrefillLabel As System.Windows.Forms.Label
    Friend WithEvents SubjectPairedComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectPrefillLabel As System.Windows.Forms.Label
    Friend WithEvents AuthorPairedComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AuthorPrefillLabel As System.Windows.Forms.Label
    Friend WithEvents FlagDocumentCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents DiscardButton As System.Windows.Forms.Button

End Class
