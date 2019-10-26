'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Partial Class AddPdfDocumentsDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                presenter.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddPdfDocumentsDialog))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.PreviewButton = New System.Windows.Forms.Button()
        Me.DiscardButton = New System.Windows.Forms.Button()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.SourcePdfDocumentLabel = New System.Windows.Forms.Label()
        Me.SelectedPdfDocumentTextBox = New System.Windows.Forms.TextBox()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.SetTitleToFileNameButton = New System.Windows.Forms.Button()
        Me.AuthorLabel = New System.Windows.Forms.Label()
        Me.AuthorPairedComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectPairedComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.KeywordsLabel = New System.Windows.Forms.Label()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.DeleteSelectedPdfOnOKCheckBox = New System.Windows.Forms.CheckBox()
        Me.FlagDocumentCheckBox = New System.Windows.Forms.CheckBox()
        Me.CategoryComboBox = New System.Windows.Forms.ComboBox()
        Me.CategoryLabel = New System.Windows.Forms.Label()
        Me.SelectButton = New System.Windows.Forms.Button()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.PreviewButton, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.DiscardButton, 3, 0)
        Me.TableLayoutPanel.Controls.Add(Me.AddButton, 2, 0)
        Me.TableLayoutPanel.Controls.Add(Me.SaveButton, 0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        '
        'PreviewButton
        '
        resources.ApplyResources(Me.PreviewButton, "PreviewButton")
        Me.PreviewButton.Name = "PreviewButton"
        Me.PreviewButton.UseVisualStyleBackColor = True
        '
        'DiscardButton
        '
        resources.ApplyResources(Me.DiscardButton, "DiscardButton")
        Me.DiscardButton.Name = "DiscardButton"
        '
        'AddButton
        '
        resources.ApplyResources(Me.AddButton, "AddButton")
        Me.AddButton.Name = "AddButton"
        '
        'SaveButton
        '
        resources.ApplyResources(Me.SaveButton, "SaveButton")
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'SourcePdfDocumentLabel
        '
        resources.ApplyResources(Me.SourcePdfDocumentLabel, "SourcePdfDocumentLabel")
        Me.SourcePdfDocumentLabel.Name = "SourcePdfDocumentLabel"
        '
        'SelectedPdfDocumentTextBox
        '
        resources.ApplyResources(Me.SelectedPdfDocumentTextBox, "SelectedPdfDocumentTextBox")
        Me.SelectedPdfDocumentTextBox.Name = "SelectedPdfDocumentTextBox"
        Me.SelectedPdfDocumentTextBox.ReadOnly = True
        Me.SelectedPdfDocumentTextBox.TabStop = False
        '
        'ViewButton
        '
        resources.ApplyResources(Me.ViewButton, "ViewButton")
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        '
        'TitleTextBox
        '
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.Name = "TitleTextBox"
        '
        'SetTitleToFileNameButton
        '
        resources.ApplyResources(Me.SetTitleToFileNameButton, "SetTitleToFileNameButton")
        Me.SetTitleToFileNameButton.Name = "SetTitleToFileNameButton"
        Me.SetTitleToFileNameButton.UseVisualStyleBackColor = True
        '
        'AuthorLabel
        '
        resources.ApplyResources(Me.AuthorLabel, "AuthorLabel")
        Me.AuthorLabel.Name = "AuthorLabel"
        '
        'AuthorPairedComboBox
        '
        Me.AuthorPairedComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.AuthorPairedComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AuthorPairedComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.AuthorPairedComboBox, "AuthorPairedComboBox")
        Me.AuthorPairedComboBox.Name = "AuthorPairedComboBox"
        Me.AuthorPairedComboBox.Sorted = True
        '
        'SubjectPairedComboBox
        '
        Me.SubjectPairedComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SubjectPairedComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SubjectPairedComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.SubjectPairedComboBox, "SubjectPairedComboBox")
        Me.SubjectPairedComboBox.Name = "SubjectPairedComboBox"
        Me.SubjectPairedComboBox.Sorted = True
        '
        'SubjectLabel
        '
        resources.ApplyResources(Me.SubjectLabel, "SubjectLabel")
        Me.SubjectLabel.Name = "SubjectLabel"
        '
        'KeywordsLabel
        '
        resources.ApplyResources(Me.KeywordsLabel, "KeywordsLabel")
        Me.KeywordsLabel.Name = "KeywordsLabel"
        '
        'KeywordsTextBox
        '
        resources.ApplyResources(Me.KeywordsTextBox, "KeywordsTextBox")
        Me.KeywordsTextBox.Name = "KeywordsTextBox"
        '
        'DeleteSelectedPdfOnOKCheckBox
        '
        resources.ApplyResources(Me.DeleteSelectedPdfOnOKCheckBox, "DeleteSelectedPdfOnOKCheckBox")
        Me.DeleteSelectedPdfOnOKCheckBox.Checked = Global.PDFKeeper.WindowsApplication.My.MySettings.Default.AddPdfDeleteInputPdfOnOK
        Me.DeleteSelectedPdfOnOKCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "AddPdfDeleteInputPdfOnOK", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DeleteSelectedPdfOnOKCheckBox.Name = "DeleteSelectedPdfOnOKCheckBox"
        Me.DeleteSelectedPdfOnOKCheckBox.UseVisualStyleBackColor = True
        '
        'FlagDocumentCheckBox
        '
        resources.ApplyResources(Me.FlagDocumentCheckBox, "FlagDocumentCheckBox")
        Me.FlagDocumentCheckBox.Checked = Global.PDFKeeper.WindowsApplication.My.MySettings.Default.AddPdfFlagDocument
        Me.FlagDocumentCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "AddPdfFlagDocument", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FlagDocumentCheckBox.Name = "FlagDocumentCheckBox"
        Me.FlagDocumentCheckBox.UseVisualStyleBackColor = True
        '
        'CategoryComboBox
        '
        Me.CategoryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CategoryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CategoryComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.CategoryComboBox, "CategoryComboBox")
        Me.CategoryComboBox.Name = "CategoryComboBox"
        Me.CategoryComboBox.Sorted = True
        '
        'CategoryLabel
        '
        resources.ApplyResources(Me.CategoryLabel, "CategoryLabel")
        Me.CategoryLabel.Name = "CategoryLabel"
        '
        'SelectButton
        '
        resources.ApplyResources(Me.SelectButton, "SelectButton")
        Me.SelectButton.Name = "SelectButton"
        Me.SelectButton.UseVisualStyleBackColor = True
        '
        'AddPdfDocumentsDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SelectButton)
        Me.Controls.Add(Me.CategoryComboBox)
        Me.Controls.Add(Me.CategoryLabel)
        Me.Controls.Add(Me.FlagDocumentCheckBox)
        Me.Controls.Add(Me.DeleteSelectedPdfOnOKCheckBox)
        Me.Controls.Add(Me.KeywordsTextBox)
        Me.Controls.Add(Me.KeywordsLabel)
        Me.Controls.Add(Me.SubjectPairedComboBox)
        Me.Controls.Add(Me.SubjectLabel)
        Me.Controls.Add(Me.AuthorPairedComboBox)
        Me.Controls.Add(Me.AuthorLabel)
        Me.Controls.Add(Me.SetTitleToFileNameButton)
        Me.Controls.Add(Me.TitleTextBox)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.ViewButton)
        Me.Controls.Add(Me.SelectedPdfDocumentTextBox)
        Me.Controls.Add(Me.SourcePdfDocumentLabel)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddPdfDocumentsDialog"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents DiscardButton As System.Windows.Forms.Button
    Friend WithEvents SourcePdfDocumentLabel As System.Windows.Forms.Label
    Friend WithEvents SelectedPdfDocumentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SetTitleToFileNameButton As System.Windows.Forms.Button
    Friend WithEvents AuthorLabel As System.Windows.Forms.Label
    Friend WithEvents AuthorPairedComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectPairedComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PreviewButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents DeleteSelectedPdfOnOKCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FlagDocumentCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CategoryComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CategoryLabel As System.Windows.Forms.Label
    Public WithEvents ViewButton As System.Windows.Forms.Button
    Public WithEvents SelectButton As System.Windows.Forms.Button
    Friend WithEvents HelpProvider As System.Windows.Forms.HelpProvider

End Class
