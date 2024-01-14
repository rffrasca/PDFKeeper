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
Partial Class AddPdfView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddPdfView))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.SelectedPdfLabel = New System.Windows.Forms.Label()
        Me.SelectedPdfTextBox = New System.Windows.Forms.TextBox()
        Me.AddPdfViewModelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SetTitleToPdfFileNameLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.MandatoryFieldLabel = New System.Windows.Forms.Label()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.DeleteSelectedPdfWhenAddedCheckBox = New System.Windows.Forms.CheckBox()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.SubjectUserControl = New PDFKeeper.Presentation.SubjectUserControl()
        Me.TitleTextBox = New PDFKeeper.Presentation.CustomTextBox(Me.components)
        Me.AuthorUserControl = New PDFKeeper.Presentation.AuthorUserControl()
        Me.UploadOptionsUserControl = New PDFKeeper.Presentation.UploadOptionsUserControl()
        Me.TaxYearDropDownListUserControl = New PDFKeeper.Presentation.TaxYearDropDownListUserControl()
        Me.CategoryUserControl = New PDFKeeper.Presentation.CategoryUserControl()
        Me.KeywordsUserControl = New PDFKeeper.Presentation.KeywordsUserControl()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.AddPdfViewModelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
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
        'SelectedPdfLabel
        '
        resources.ApplyResources(Me.SelectedPdfLabel, "SelectedPdfLabel")
        Me.SelectedPdfLabel.Name = "SelectedPdfLabel"
        '
        'SelectedPdfTextBox
        '
        Me.SelectedPdfTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddPdfViewModelBindingSource, "SelectedPdf", True))
        resources.ApplyResources(Me.SelectedPdfTextBox, "SelectedPdfTextBox")
        Me.SelectedPdfTextBox.Name = "SelectedPdfTextBox"
        Me.SelectedPdfTextBox.ReadOnly = True
        Me.SelectedPdfTextBox.TabStop = False
        '
        'AddPdfViewModelBindingSource
        '
        Me.AddPdfViewModelBindingSource.DataSource = GetType(PDFKeeper.Core.ViewModels.AddPdfViewModel)
        '
        'SetTitleToPdfFileNameLinkLabel
        '
        resources.ApplyResources(Me.SetTitleToPdfFileNameLinkLabel, "SetTitleToPdfFileNameLinkLabel")
        Me.SetTitleToPdfFileNameLinkLabel.Name = "SetTitleToPdfFileNameLinkLabel"
        Me.SetTitleToPdfFileNameLinkLabel.TabStop = True
        '
        'MandatoryFieldLabel
        '
        resources.ApplyResources(Me.MandatoryFieldLabel, "MandatoryFieldLabel")
        Me.MandatoryFieldLabel.Name = "MandatoryFieldLabel"
        '
        'ViewButton
        '
        Me.ViewButton.Image = Global.PDFKeeper.Presentation.My.Resources.Resources.file_acrobat
        resources.ApplyResources(Me.ViewButton, "ViewButton")
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'DeleteSelectedPdfWhenAddedCheckBox
        '
        resources.ApplyResources(Me.DeleteSelectedPdfWhenAddedCheckBox, "DeleteSelectedPdfWhenAddedCheckBox")
        Me.DeleteSelectedPdfWhenAddedCheckBox.Checked = Global.PDFKeeper.Presentation.My.MySettings.Default.AddPdfDeleteSelectedPdfWhenAdded
        Me.DeleteSelectedPdfWhenAddedCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.PDFKeeper.Presentation.My.MySettings.Default, "AddPdfDeleteSelectedPdfWhenAdded", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DeleteSelectedPdfWhenAddedCheckBox.Name = "DeleteSelectedPdfWhenAddedCheckBox"
        Me.DeleteSelectedPdfWhenAddedCheckBox.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        '
        'SubjectUserControl
        '
        resources.ApplyResources(Me.SubjectUserControl, "SubjectUserControl")
        Me.SubjectUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Subject", Me.AddPdfViewModelBindingSource, "Subject", True))
        Me.SubjectUserControl.Name = "SubjectUserControl"
        Me.SubjectUserControl.Subject = ""
        Me.SubjectUserControl.Subjects = Nothing
        '
        'TitleTextBox
        '
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddPdfViewModelBindingSource, "Title", True))
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.Name = "TitleTextBox"
        '
        'AuthorUserControl
        '
        Me.AuthorUserControl.Author = ""
        Me.AuthorUserControl.Authors = Nothing
        resources.ApplyResources(Me.AuthorUserControl, "AuthorUserControl")
        Me.AuthorUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Author", Me.AddPdfViewModelBindingSource, "Author", True))
        Me.AuthorUserControl.Name = "AuthorUserControl"
        '
        'UploadOptionsUserControl
        '
        resources.ApplyResources(Me.UploadOptionsUserControl, "UploadOptionsUserControl")
        Me.UploadOptionsUserControl.DataBindings.Add(New System.Windows.Forms.Binding("FlagDocumentChecked", Me.AddPdfViewModelBindingSource, "FlagDocument", True))
        Me.UploadOptionsUserControl.DataBindings.Add(New System.Windows.Forms.Binding("OcrPdfTextAndImageDataPagesChecked", Me.AddPdfViewModelBindingSource, "OcrPdfTextAndImageDataPages", True))
        Me.UploadOptionsUserControl.FlagDocumentChecked = False
        Me.UploadOptionsUserControl.Name = "UploadOptionsUserControl"
        Me.UploadOptionsUserControl.OcrPdfTextAndImageDataPagesChecked = False
        '
        'TaxYearDropDownListUserControl
        '
        resources.ApplyResources(Me.TaxYearDropDownListUserControl, "TaxYearDropDownListUserControl")
        Me.TaxYearDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("TaxYear", Me.AddPdfViewModelBindingSource, "TaxYear", True))
        Me.TaxYearDropDownListUserControl.Name = "TaxYearDropDownListUserControl"
        Me.TaxYearDropDownListUserControl.TaxYear = ""
        Me.TaxYearDropDownListUserControl.TaxYears = Nothing
        '
        'CategoryUserControl
        '
        resources.ApplyResources(Me.CategoryUserControl, "CategoryUserControl")
        Me.CategoryUserControl.Categories = Nothing
        Me.CategoryUserControl.Category = ""
        Me.CategoryUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Category", Me.AddPdfViewModelBindingSource, "Category", True))
        Me.CategoryUserControl.Name = "CategoryUserControl"
        '
        'KeywordsUserControl
        '
        resources.ApplyResources(Me.KeywordsUserControl, "KeywordsUserControl")
        Me.KeywordsUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Keywords", Me.AddPdfViewModelBindingSource, "Keywords", True))
        Me.KeywordsUserControl.Keywords = ""
        Me.KeywordsUserControl.Name = "KeywordsUserControl"
        '
        'AddPdfView
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ControlBox = False
        Me.Controls.Add(Me.SubjectUserControl)
        Me.Controls.Add(Me.TitleTextBox)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.AuthorUserControl)
        Me.Controls.Add(Me.DeleteSelectedPdfWhenAddedCheckBox)
        Me.Controls.Add(Me.ViewButton)
        Me.Controls.Add(Me.MandatoryFieldLabel)
        Me.Controls.Add(Me.SetTitleToPdfFileNameLinkLabel)
        Me.Controls.Add(Me.UploadOptionsUserControl)
        Me.Controls.Add(Me.TaxYearDropDownListUserControl)
        Me.Controls.Add(Me.CategoryUserControl)
        Me.Controls.Add(Me.KeywordsUserControl)
        Me.Controls.Add(Me.SelectedPdfTextBox)
        Me.Controls.Add(Me.SelectedPdfLabel)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddPdfView"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        CType(Me.AddPdfViewModelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents SelectedPdfLabel As Label
    Friend WithEvents SelectedPdfTextBox As TextBox
    Friend WithEvents KeywordsUserControl As KeywordsUserControl
    Friend WithEvents CategoryUserControl As CategoryUserControl
    Friend WithEvents TaxYearDropDownListUserControl As TaxYearDropDownListUserControl
    Friend WithEvents UploadOptionsUserControl As UploadOptionsUserControl
    Friend WithEvents SetTitleToPdfFileNameLinkLabel As LinkLabel
    Friend WithEvents MandatoryFieldLabel As Label
    Friend WithEvents ViewButton As Button
    Friend WithEvents DeleteSelectedPdfWhenAddedCheckBox As CheckBox
    Friend WithEvents AuthorUserControl As AuthorUserControl
    Friend WithEvents TitleLabel As Label
    Friend WithEvents TitleTextBox As CustomTextBox
    Friend WithEvents SubjectUserControl As SubjectUserControl
    Friend WithEvents AddPdfViewModelBindingSource As BindingSource
    Friend WithEvents HelpProvider As HelpProvider
End Class
