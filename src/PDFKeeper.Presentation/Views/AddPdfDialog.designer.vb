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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddPdfDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddPdfDialog))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.PreviewButton = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.SelectedPdfLabel = New System.Windows.Forms.Label()
        Me.SelectedPdfTextBox = New System.Windows.Forms.TextBox()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.SetTitleToFileNameButton = New System.Windows.Forms.Button()
        Me.AuthorLabel = New System.Windows.Forms.Label()
        Me.AuthorComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.KeywordsLabel = New System.Windows.Forms.Label()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.CategoryComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryLabel = New System.Windows.Forms.Label()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TaxYearComboBox = New System.Windows.Forms.ComboBox()
        Me.TaxYearLabel = New System.Windows.Forms.Label()
        Me.FlagDocumentCheckBox = New System.Windows.Forms.CheckBox()
        Me.DeleteSelectedPdfWhenAddedCheckBox = New System.Windows.Forms.CheckBox()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.TableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.PreviewButton, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.Cancel_Button, 3, 0)
        Me.TableLayoutPanel.Controls.Add(Me.AddButton, 2, 0)
        Me.TableLayoutPanel.Controls.Add(Me.SaveButton, 0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.HelpProvider.SetShowHelp(Me.TableLayoutPanel, CType(resources.GetObject("TableLayoutPanel.ShowHelp"), Boolean))
        '
        'PreviewButton
        '
        resources.ApplyResources(Me.PreviewButton, "PreviewButton")
        Me.PreviewButton.Name = "PreviewButton"
        Me.HelpProvider.SetShowHelp(Me.PreviewButton, CType(resources.GetObject("PreviewButton.ShowHelp"), Boolean))
        Me.PreviewButton.UseVisualStyleBackColor = True
        '
        'Cancel_Button
        '
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.HelpProvider.SetShowHelp(Me.Cancel_Button, CType(resources.GetObject("Cancel_Button.ShowHelp"), Boolean))
        '
        'AddButton
        '
        resources.ApplyResources(Me.AddButton, "AddButton")
        Me.AddButton.Name = "AddButton"
        Me.HelpProvider.SetShowHelp(Me.AddButton, CType(resources.GetObject("AddButton.ShowHelp"), Boolean))
        '
        'SaveButton
        '
        resources.ApplyResources(Me.SaveButton, "SaveButton")
        Me.SaveButton.Name = "SaveButton"
        Me.HelpProvider.SetShowHelp(Me.SaveButton, CType(resources.GetObject("SaveButton.ShowHelp"), Boolean))
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'SelectedPdfLabel
        '
        resources.ApplyResources(Me.SelectedPdfLabel, "SelectedPdfLabel")
        Me.SelectedPdfLabel.Name = "SelectedPdfLabel"
        Me.HelpProvider.SetShowHelp(Me.SelectedPdfLabel, CType(resources.GetObject("SelectedPdfLabel.ShowHelp"), Boolean))
        '
        'SelectedPdfTextBox
        '
        resources.ApplyResources(Me.SelectedPdfTextBox, "SelectedPdfTextBox")
        Me.SelectedPdfTextBox.Name = "SelectedPdfTextBox"
        Me.SelectedPdfTextBox.ReadOnly = True
        Me.HelpProvider.SetShowHelp(Me.SelectedPdfTextBox, CType(resources.GetObject("SelectedPdfTextBox.ShowHelp"), Boolean))
        Me.SelectedPdfTextBox.TabStop = False
        '
        'ViewButton
        '
        resources.ApplyResources(Me.ViewButton, "ViewButton")
        Me.ViewButton.Name = "ViewButton"
        Me.HelpProvider.SetShowHelp(Me.ViewButton, CType(resources.GetObject("ViewButton.ShowHelp"), Boolean))
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        Me.HelpProvider.SetShowHelp(Me.TitleLabel, CType(resources.GetObject("TitleLabel.ShowHelp"), Boolean))
        '
        'TitleTextBox
        '
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.Name = "TitleTextBox"
        Me.HelpProvider.SetShowHelp(Me.TitleTextBox, CType(resources.GetObject("TitleTextBox.ShowHelp"), Boolean))
        '
        'SetTitleToFileNameButton
        '
        resources.ApplyResources(Me.SetTitleToFileNameButton, "SetTitleToFileNameButton")
        Me.SetTitleToFileNameButton.Name = "SetTitleToFileNameButton"
        Me.HelpProvider.SetShowHelp(Me.SetTitleToFileNameButton, CType(resources.GetObject("SetTitleToFileNameButton.ShowHelp"), Boolean))
        Me.SetTitleToFileNameButton.UseVisualStyleBackColor = True
        '
        'AuthorLabel
        '
        resources.ApplyResources(Me.AuthorLabel, "AuthorLabel")
        Me.AuthorLabel.Name = "AuthorLabel"
        Me.HelpProvider.SetShowHelp(Me.AuthorLabel, CType(resources.GetObject("AuthorLabel.ShowHelp"), Boolean))
        '
        'AuthorComboBox
        '
        Me.AuthorComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.AuthorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AuthorComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.AuthorComboBox, "AuthorComboBox")
        Me.AuthorComboBox.Name = "AuthorComboBox"
        Me.HelpProvider.SetShowHelp(Me.AuthorComboBox, CType(resources.GetObject("AuthorComboBox.ShowHelp"), Boolean))
        Me.AuthorComboBox.Sorted = True
        '
        'SubjectComboBox
        '
        Me.SubjectComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SubjectComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SubjectComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.SubjectComboBox, "SubjectComboBox")
        Me.SubjectComboBox.Name = "SubjectComboBox"
        Me.HelpProvider.SetShowHelp(Me.SubjectComboBox, CType(resources.GetObject("SubjectComboBox.ShowHelp"), Boolean))
        Me.SubjectComboBox.Sorted = True
        '
        'SubjectLabel
        '
        resources.ApplyResources(Me.SubjectLabel, "SubjectLabel")
        Me.SubjectLabel.Name = "SubjectLabel"
        Me.HelpProvider.SetShowHelp(Me.SubjectLabel, CType(resources.GetObject("SubjectLabel.ShowHelp"), Boolean))
        '
        'KeywordsLabel
        '
        resources.ApplyResources(Me.KeywordsLabel, "KeywordsLabel")
        Me.KeywordsLabel.Name = "KeywordsLabel"
        Me.HelpProvider.SetShowHelp(Me.KeywordsLabel, CType(resources.GetObject("KeywordsLabel.ShowHelp"), Boolean))
        '
        'KeywordsTextBox
        '
        resources.ApplyResources(Me.KeywordsTextBox, "KeywordsTextBox")
        Me.KeywordsTextBox.Name = "KeywordsTextBox"
        Me.HelpProvider.SetShowHelp(Me.KeywordsTextBox, CType(resources.GetObject("KeywordsTextBox.ShowHelp"), Boolean))
        '
        'CategoryComboBox
        '
        Me.CategoryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CategoryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CategoryComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.CategoryComboBox, "CategoryComboBox")
        Me.CategoryComboBox.Name = "CategoryComboBox"
        Me.HelpProvider.SetShowHelp(Me.CategoryComboBox, CType(resources.GetObject("CategoryComboBox.ShowHelp"), Boolean))
        Me.CategoryComboBox.Sorted = True
        '
        'CategoryLabel
        '
        resources.ApplyResources(Me.CategoryLabel, "CategoryLabel")
        Me.CategoryLabel.Name = "CategoryLabel"
        Me.HelpProvider.SetShowHelp(Me.CategoryLabel, CType(resources.GetObject("CategoryLabel.ShowHelp"), Boolean))
        '
        'TaxYearComboBox
        '
        Me.TaxYearComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.TaxYearComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TaxYearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TaxYearComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.TaxYearComboBox, "TaxYearComboBox")
        Me.TaxYearComboBox.Name = "TaxYearComboBox"
        Me.HelpProvider.SetShowHelp(Me.TaxYearComboBox, CType(resources.GetObject("TaxYearComboBox.ShowHelp"), Boolean))
        '
        'TaxYearLabel
        '
        resources.ApplyResources(Me.TaxYearLabel, "TaxYearLabel")
        Me.TaxYearLabel.Name = "TaxYearLabel"
        Me.HelpProvider.SetShowHelp(Me.TaxYearLabel, CType(resources.GetObject("TaxYearLabel.ShowHelp"), Boolean))
        '
        'FlagDocumentCheckBox
        '
        resources.ApplyResources(Me.FlagDocumentCheckBox, "FlagDocumentCheckBox")
        Me.FlagDocumentCheckBox.Checked = Global.PDFKeeper.Presentation.My.MySettings.Default.AddPdfFlagDocument
        Me.FlagDocumentCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.Presentation.My.MySettings.Default, "AddPdfFlagDocument", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FlagDocumentCheckBox.Name = "FlagDocumentCheckBox"
        Me.HelpProvider.SetShowHelp(Me.FlagDocumentCheckBox, CType(resources.GetObject("FlagDocumentCheckBox.ShowHelp"), Boolean))
        Me.FlagDocumentCheckBox.UseVisualStyleBackColor = True
        '
        'DeleteSelectedPdfWhenAddedCheckBox
        '
        resources.ApplyResources(Me.DeleteSelectedPdfWhenAddedCheckBox, "DeleteSelectedPdfWhenAddedCheckBox")
        Me.DeleteSelectedPdfWhenAddedCheckBox.Checked = Global.PDFKeeper.Presentation.My.MySettings.Default.AddPdfDeleteSelectedPdfWhenAdded
        Me.DeleteSelectedPdfWhenAddedCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.Presentation.My.MySettings.Default, "AddPdfDeleteSelectedPdfWhenAdded", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DeleteSelectedPdfWhenAddedCheckBox.Name = "DeleteSelectedPdfWhenAddedCheckBox"
        Me.HelpProvider.SetShowHelp(Me.DeleteSelectedPdfWhenAddedCheckBox, CType(resources.GetObject("DeleteSelectedPdfWhenAddedCheckBox.ShowHelp"), Boolean))
        Me.DeleteSelectedPdfWhenAddedCheckBox.UseVisualStyleBackColor = True
        '
        'OpenFileDialog
        '
        resources.ApplyResources(Me.OpenFileDialog, "OpenFileDialog")
        '
        'AddPdfDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.TaxYearComboBox)
        Me.Controls.Add(Me.TaxYearLabel)
        Me.Controls.Add(Me.CategoryComboBox)
        Me.Controls.Add(Me.CategoryLabel)
        Me.Controls.Add(Me.FlagDocumentCheckBox)
        Me.Controls.Add(Me.DeleteSelectedPdfWhenAddedCheckBox)
        Me.Controls.Add(Me.KeywordsTextBox)
        Me.Controls.Add(Me.KeywordsLabel)
        Me.Controls.Add(Me.SubjectComboBox)
        Me.Controls.Add(Me.SubjectLabel)
        Me.Controls.Add(Me.AuthorComboBox)
        Me.Controls.Add(Me.AuthorLabel)
        Me.Controls.Add(Me.SetTitleToFileNameButton)
        Me.Controls.Add(Me.TitleTextBox)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.ViewButton)
        Me.Controls.Add(Me.SelectedPdfTextBox)
        Me.Controls.Add(Me.SelectedPdfLabel)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddPdfDialog"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents SelectedPdfLabel As System.Windows.Forms.Label
    Friend WithEvents SelectedPdfTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SetTitleToFileNameButton As System.Windows.Forms.Button
    Friend WithEvents AuthorLabel As System.Windows.Forms.Label
    Friend WithEvents AuthorComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PreviewButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents DeleteSelectedPdfWhenAddedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FlagDocumentCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CategoryComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CategoryLabel As System.Windows.Forms.Label
    Public WithEvents ViewButton As System.Windows.Forms.Button
    Friend WithEvents HelpProvider As System.Windows.Forms.HelpProvider
    Friend WithEvents TaxYearLabel As Label
    Friend WithEvents TaxYearComboBox As ComboBox
    Friend WithEvents OpenFileDialog As OpenFileDialog
End Class
