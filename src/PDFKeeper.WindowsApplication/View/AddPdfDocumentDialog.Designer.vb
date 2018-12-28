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
Partial Class AddPdfDocumentDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                m_OriginalPdfFilePassword.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddPdfDocumentDialog))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.PreviewButton = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.OriginalPdfPathNameLabel = New System.Windows.Forms.Label()
        Me.OriginalPdfPathNameTextBox = New System.Windows.Forms.TextBox()
        Me.ViewOriginalButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.SetToFileNameButton = New System.Windows.Forms.Button()
        Me.AuthorLabel = New System.Windows.Forms.Label()
        Me.AuthorComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectComboBox = New System.Windows.Forms.ComboBox()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.KeywordsLabel = New System.Windows.Forms.Label()
        Me.KeywordsTextBox = New System.Windows.Forms.TextBox()
        Me.DeleteOriginalPdfOnOKCheckBox = New System.Windows.Forms.CheckBox()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.PreviewButton, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.Cancel_Button, 3, 0)
        Me.TableLayoutPanel.Controls.Add(Me.OK_Button, 2, 0)
        Me.TableLayoutPanel.Controls.Add(Me.SaveButton, 0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        '
        'PreviewButton
        '
        resources.ApplyResources(Me.PreviewButton, "PreviewButton")
        Me.PreviewButton.Name = "PreviewButton"
        Me.PreviewButton.UseVisualStyleBackColor = True
        '
        'Cancel_Button
        '
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Name = "Cancel_Button"
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'SaveButton
        '
        resources.ApplyResources(Me.SaveButton, "SaveButton")
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'OriginalPdfPathNameLabel
        '
        resources.ApplyResources(Me.OriginalPdfPathNameLabel, "OriginalPdfPathNameLabel")
        Me.OriginalPdfPathNameLabel.Name = "OriginalPdfPathNameLabel"
        '
        'OriginalPdfPathNameTextBox
        '
        resources.ApplyResources(Me.OriginalPdfPathNameTextBox, "OriginalPdfPathNameTextBox")
        Me.OriginalPdfPathNameTextBox.Name = "OriginalPdfPathNameTextBox"
        Me.OriginalPdfPathNameTextBox.ReadOnly = True
        Me.OriginalPdfPathNameTextBox.TabStop = False
        '
        'ViewOriginalButton
        '
        resources.ApplyResources(Me.ViewOriginalButton, "ViewOriginalButton")
        Me.ViewOriginalButton.Name = "ViewOriginalButton"
        Me.ViewOriginalButton.UseVisualStyleBackColor = True
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
        'SetToFileNameButton
        '
        resources.ApplyResources(Me.SetToFileNameButton, "SetToFileNameButton")
        Me.SetToFileNameButton.Name = "SetToFileNameButton"
        Me.SetToFileNameButton.UseVisualStyleBackColor = True
        '
        'AuthorLabel
        '
        resources.ApplyResources(Me.AuthorLabel, "AuthorLabel")
        Me.AuthorLabel.Name = "AuthorLabel"
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
        'SubjectComboBox
        '
        Me.SubjectComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SubjectComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SubjectComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.SubjectComboBox, "SubjectComboBox")
        Me.SubjectComboBox.Name = "SubjectComboBox"
        Me.SubjectComboBox.Sorted = True
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
        'DeleteOriginalPdfOnOKCheckBox
        '
        resources.ApplyResources(Me.DeleteOriginalPdfOnOKCheckBox, "DeleteOriginalPdfOnOKCheckBox")
        Me.DeleteOriginalPdfOnOKCheckBox.Checked = Global.PDFKeeper.WindowsApplication.My.MySettings.Default.DeleteOriginalPdfOnOK
        Me.DeleteOriginalPdfOnOKCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.WindowsApplication.My.MySettings.Default, "DeleteOriginalPdfOnOK", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DeleteOriginalPdfOnOKCheckBox.Name = "DeleteOriginalPdfOnOKCheckBox"
        Me.DeleteOriginalPdfOnOKCheckBox.UseVisualStyleBackColor = True
        '
        'AddPdfDocumentDialog
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.DeleteOriginalPdfOnOKCheckBox)
        Me.Controls.Add(Me.KeywordsTextBox)
        Me.Controls.Add(Me.KeywordsLabel)
        Me.Controls.Add(Me.SubjectComboBox)
        Me.Controls.Add(Me.SubjectLabel)
        Me.Controls.Add(Me.AuthorComboBox)
        Me.Controls.Add(Me.AuthorLabel)
        Me.Controls.Add(Me.SetToFileNameButton)
        Me.Controls.Add(Me.TitleTextBox)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.ViewOriginalButton)
        Me.Controls.Add(Me.OriginalPdfPathNameTextBox)
        Me.Controls.Add(Me.OriginalPdfPathNameLabel)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddPdfDocumentDialog"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OriginalPdfPathNameLabel As System.Windows.Forms.Label
    Friend WithEvents OriginalPdfPathNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ViewOriginalButton As System.Windows.Forms.Button
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SetToFileNameButton As System.Windows.Forms.Button
    Friend WithEvents AuthorLabel As System.Windows.Forms.Label
    Friend WithEvents AuthorComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SubjectLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsLabel As System.Windows.Forms.Label
    Friend WithEvents KeywordsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PreviewButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents DeleteOriginalPdfOnOKCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents HelpProvider As System.Windows.Forms.HelpProvider

End Class
