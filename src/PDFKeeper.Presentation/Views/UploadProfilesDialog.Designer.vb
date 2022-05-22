'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2022 Robert F. Frasca
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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UploadProfilesDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadProfilesDialog))
        Me.ProfilesGroupBox = New System.Windows.Forms.GroupBox()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.EditButton = New System.Windows.Forms.Button()
        Me.NewButton = New System.Windows.Forms.Button()
        Me.ProfileComboBox = New System.Windows.Forms.ComboBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.ProfileNameTextBox = New System.Windows.Forms.TextBox()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.TitleComboBox = New System.Windows.Forms.ComboBox()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.CategoryComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryLabel = New System.Windows.Forms.Label()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.KeywordsLabel = New System.Windows.Forms.Label()
        Me.SubjectComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.AuthorComboBox = New System.Windows.Forms.ComboBox()
        Me.AuthorLabel = New System.Windows.Forms.Label()
        Me.FlagDocumentCheckBox = New System.Windows.Forms.CheckBox()
        Me.ProfileGroupBox = New System.Windows.Forms.GroupBox()
        Me.TaxYearComboBox = New System.Windows.Forms.ComboBox()
        Me.TaxYearLabel = New System.Windows.Forms.Label()
        Me.DiscardButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ProfilesGroupBox.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProfileGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProfilesGroupBox
        '
        Me.ProfilesGroupBox.Controls.Add(Me.DeleteButton)
        Me.ProfilesGroupBox.Controls.Add(Me.EditButton)
        Me.ProfilesGroupBox.Controls.Add(Me.NewButton)
        Me.ProfilesGroupBox.Controls.Add(Me.ProfileComboBox)
        resources.ApplyResources(Me.ProfilesGroupBox, "ProfilesGroupBox")
        Me.ProfilesGroupBox.Name = "ProfilesGroupBox"
        Me.ProfilesGroupBox.TabStop = False
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
        'ProfileComboBox
        '
        Me.ProfileComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ProfileComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ProfileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProfileComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.ProfileComboBox, "ProfileComboBox")
        Me.ProfileComboBox.Name = "ProfileComboBox"
        Me.ProfileComboBox.Sorted = True
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'ProfileNameTextBox
        '
        Me.HelpProvider.SetHelpKeyword(Me.ProfileNameTextBox, resources.GetString("ProfileNameTextBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.ProfileNameTextBox, CType(resources.GetObject("ProfileNameTextBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.ProfileNameTextBox, "ProfileNameTextBox")
        Me.ProfileNameTextBox.Name = "ProfileNameTextBox"
        Me.HelpProvider.SetShowHelp(Me.ProfileNameTextBox, CType(resources.GetObject("ProfileNameTextBox.ShowHelp"), Boolean))
        '
        'NameLabel
        '
        resources.ApplyResources(Me.NameLabel, "NameLabel")
        Me.HelpProvider.SetHelpKeyword(Me.NameLabel, resources.GetString("NameLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.NameLabel, CType(resources.GetObject("NameLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.NameLabel.Name = "NameLabel"
        Me.HelpProvider.SetShowHelp(Me.NameLabel, CType(resources.GetObject("NameLabel.ShowHelp"), Boolean))
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
        'TitleLabel
        '
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.HelpProvider.SetHelpKeyword(Me.TitleLabel, resources.GetString("TitleLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.TitleLabel, CType(resources.GetObject("TitleLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.TitleLabel.Name = "TitleLabel"
        Me.HelpProvider.SetShowHelp(Me.TitleLabel, CType(resources.GetObject("TitleLabel.ShowHelp"), Boolean))
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
        'CategoryLabel
        '
        resources.ApplyResources(Me.CategoryLabel, "CategoryLabel")
        Me.HelpProvider.SetHelpKeyword(Me.CategoryLabel, resources.GetString("CategoryLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.CategoryLabel, CType(resources.GetObject("CategoryLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.CategoryLabel.Name = "CategoryLabel"
        Me.HelpProvider.SetShowHelp(Me.CategoryLabel, CType(resources.GetObject("CategoryLabel.ShowHelp"), Boolean))
        '
        'KeywordsTextBox
        '
        Me.HelpProvider.SetHelpKeyword(Me.KeywordsTextBox, resources.GetString("KeywordsTextBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.KeywordsTextBox, CType(resources.GetObject("KeywordsTextBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.KeywordsTextBox, "KeywordsTextBox")
        Me.KeywordsTextBox.Name = "KeywordsTextBox"
        Me.HelpProvider.SetShowHelp(Me.KeywordsTextBox, CType(resources.GetObject("KeywordsTextBox.ShowHelp"), Boolean))
        '
        'KeywordsLabel
        '
        resources.ApplyResources(Me.KeywordsLabel, "KeywordsLabel")
        Me.HelpProvider.SetHelpKeyword(Me.KeywordsLabel, resources.GetString("KeywordsLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.KeywordsLabel, CType(resources.GetObject("KeywordsLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.KeywordsLabel.Name = "KeywordsLabel"
        Me.HelpProvider.SetShowHelp(Me.KeywordsLabel, CType(resources.GetObject("KeywordsLabel.ShowHelp"), Boolean))
        '
        'SubjectComboBox
        '
        Me.SubjectComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SubjectComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SubjectComboBox.FormattingEnabled = True
        Me.HelpProvider.SetHelpKeyword(Me.SubjectComboBox, resources.GetString("SubjectComboBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.SubjectComboBox, CType(resources.GetObject("SubjectComboBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.SubjectComboBox, "SubjectComboBox")
        Me.SubjectComboBox.Name = "SubjectComboBox"
        Me.HelpProvider.SetShowHelp(Me.SubjectComboBox, CType(resources.GetObject("SubjectComboBox.ShowHelp"), Boolean))
        Me.SubjectComboBox.Sorted = True
        '
        'SubjectLabel
        '
        resources.ApplyResources(Me.SubjectLabel, "SubjectLabel")
        Me.HelpProvider.SetHelpKeyword(Me.SubjectLabel, resources.GetString("SubjectLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.SubjectLabel, CType(resources.GetObject("SubjectLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.SubjectLabel.Name = "SubjectLabel"
        Me.HelpProvider.SetShowHelp(Me.SubjectLabel, CType(resources.GetObject("SubjectLabel.ShowHelp"), Boolean))
        '
        'AuthorComboBox
        '
        Me.AuthorComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.AuthorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AuthorComboBox.FormattingEnabled = True
        Me.HelpProvider.SetHelpKeyword(Me.AuthorComboBox, resources.GetString("AuthorComboBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.AuthorComboBox, CType(resources.GetObject("AuthorComboBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.AuthorComboBox, "AuthorComboBox")
        Me.AuthorComboBox.Name = "AuthorComboBox"
        Me.HelpProvider.SetShowHelp(Me.AuthorComboBox, CType(resources.GetObject("AuthorComboBox.ShowHelp"), Boolean))
        Me.AuthorComboBox.Sorted = True
        '
        'AuthorLabel
        '
        resources.ApplyResources(Me.AuthorLabel, "AuthorLabel")
        Me.HelpProvider.SetHelpKeyword(Me.AuthorLabel, resources.GetString("AuthorLabel.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.AuthorLabel, CType(resources.GetObject("AuthorLabel.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.AuthorLabel.Name = "AuthorLabel"
        Me.HelpProvider.SetShowHelp(Me.AuthorLabel, CType(resources.GetObject("AuthorLabel.ShowHelp"), Boolean))
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
        'ProfileGroupBox
        '
        Me.ProfileGroupBox.Controls.Add(Me.TaxYearComboBox)
        Me.ProfileGroupBox.Controls.Add(Me.TaxYearLabel)
        Me.ProfileGroupBox.Controls.Add(Me.DiscardButton)
        Me.ProfileGroupBox.Controls.Add(Me.SaveButton)
        Me.ProfileGroupBox.Controls.Add(Me.FlagDocumentCheckBox)
        Me.ProfileGroupBox.Controls.Add(Me.CategoryComboBox)
        Me.ProfileGroupBox.Controls.Add(Me.CategoryLabel)
        Me.ProfileGroupBox.Controls.Add(Me.KeywordsTextBox)
        Me.ProfileGroupBox.Controls.Add(Me.KeywordsLabel)
        Me.ProfileGroupBox.Controls.Add(Me.SubjectComboBox)
        Me.ProfileGroupBox.Controls.Add(Me.SubjectLabel)
        Me.ProfileGroupBox.Controls.Add(Me.AuthorComboBox)
        Me.ProfileGroupBox.Controls.Add(Me.AuthorLabel)
        Me.ProfileGroupBox.Controls.Add(Me.TitleComboBox)
        Me.ProfileGroupBox.Controls.Add(Me.TitleLabel)
        Me.ProfileGroupBox.Controls.Add(Me.ProfileNameTextBox)
        Me.ProfileGroupBox.Controls.Add(Me.NameLabel)
        Me.HelpProvider.SetHelpKeyword(Me.ProfileGroupBox, resources.GetString("ProfileGroupBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.ProfileGroupBox, CType(resources.GetObject("ProfileGroupBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.ProfileGroupBox, "ProfileGroupBox")
        Me.ProfileGroupBox.Name = "ProfileGroupBox"
        Me.HelpProvider.SetShowHelp(Me.ProfileGroupBox, CType(resources.GetObject("ProfileGroupBox.ShowHelp"), Boolean))
        Me.ProfileGroupBox.TabStop = False
        '
        'TaxYearComboBox
        '
        Me.TaxYearComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.TaxYearComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TaxYearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TaxYearComboBox.FormattingEnabled = True
        Me.HelpProvider.SetHelpKeyword(Me.TaxYearComboBox, resources.GetString("TaxYearComboBox.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me.TaxYearComboBox, CType(resources.GetObject("TaxYearComboBox.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        resources.ApplyResources(Me.TaxYearComboBox, "TaxYearComboBox")
        Me.TaxYearComboBox.Name = "TaxYearComboBox"
        Me.HelpProvider.SetShowHelp(Me.TaxYearComboBox, CType(resources.GetObject("TaxYearComboBox.ShowHelp"), Boolean))
        '
        'TaxYearLabel
        '
        resources.ApplyResources(Me.TaxYearLabel, "TaxYearLabel")
        Me.TaxYearLabel.Name = "TaxYearLabel"
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
        'UploadProfilesDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ProfileGroupBox)
        Me.Controls.Add(Me.ProfilesGroupBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UploadProfilesDialog"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.ProfilesGroupBox.ResumeLayout(False)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProfileGroupBox.ResumeLayout(False)
        Me.ProfileGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProfilesGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ProfileComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents EditButton As System.Windows.Forms.Button
    Friend WithEvents NewButton As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents HelpProvider As System.Windows.Forms.HelpProvider
    Friend WithEvents ProfileGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ProfileNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NameLabel As System.Windows.Forms.Label
    Friend WithEvents TitleComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents CategoryComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CategoryLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents KeywordsLabel As System.Windows.Forms.Label
    Friend WithEvents SubjectComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectLabel As System.Windows.Forms.Label
    Friend WithEvents AuthorComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AuthorLabel As System.Windows.Forms.Label
    Friend WithEvents FlagDocumentCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents DiscardButton As System.Windows.Forms.Button
    Friend WithEvents TaxYearComboBox As ComboBox
    Friend WithEvents TaxYearLabel As Label
End Class
