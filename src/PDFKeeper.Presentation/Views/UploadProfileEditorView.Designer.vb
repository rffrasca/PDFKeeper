' *****************************************************************************
' * PDFKeeper -- Open Source PDF Document Management
' * Copyright (C) 2009-2023 Robert F. Frasca
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
Partial Class UploadProfileEditorView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadProfileEditorView))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.SetNameToAuthorSubjectLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.MandatoryFieldLabel = New System.Windows.Forms.Label()
        Me.KeywordsUserControl = New PDFKeeper.Presentation.KeywordsUserControl()
        Me.UploadProfileEditorViewModelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SubjectUserControl = New PDFKeeper.Presentation.SubjectUserControl()
        Me.AuthorUserControl = New PDFKeeper.Presentation.AuthorUserControl()
        Me.UploadOptionsUserControl = New PDFKeeper.Presentation.UploadOptionsUserControl()
        Me.TaxYearDropDownListUserControl = New PDFKeeper.Presentation.TaxYearDropDownListUserControl()
        Me.CategoryUserControl = New PDFKeeper.Presentation.CategoryUserControl()
        Me.TitleUserControl = New PDFKeeper.Presentation.TitleUserControl()
        Me.NameUserControl = New PDFKeeper.Presentation.NameUserControl()
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.UploadProfileEditorViewModelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'SetNameToAuthorSubjectLinkLabel
        '
        resources.ApplyResources(Me.SetNameToAuthorSubjectLinkLabel, "SetNameToAuthorSubjectLinkLabel")
        Me.SetNameToAuthorSubjectLinkLabel.Name = "SetNameToAuthorSubjectLinkLabel"
        Me.SetNameToAuthorSubjectLinkLabel.TabStop = True
        '
        'MandatoryFieldLabel
        '
        resources.ApplyResources(Me.MandatoryFieldLabel, "MandatoryFieldLabel")
        Me.MandatoryFieldLabel.Name = "MandatoryFieldLabel"
        '
        'KeywordsUserControl
        '
        resources.ApplyResources(Me.KeywordsUserControl, "KeywordsUserControl")
        Me.KeywordsUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Keywords", Me.UploadProfileEditorViewModelBindingSource, "Keywords", True))
        Me.KeywordsUserControl.Keywords = ""
        Me.KeywordsUserControl.Name = "KeywordsUserControl"
        '
        'UploadProfileEditorViewModelBindingSource
        '
        Me.UploadProfileEditorViewModelBindingSource.DataSource = GetType(PDFKeeper.Core.ViewModels.UploadProfileEditorViewModel)
        '
        'SubjectUserControl
        '
        resources.ApplyResources(Me.SubjectUserControl, "SubjectUserControl")
        Me.SubjectUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Subject", Me.UploadProfileEditorViewModelBindingSource, "Subject", True))
        Me.SubjectUserControl.Name = "SubjectUserControl"
        Me.SubjectUserControl.Subject = ""
        Me.SubjectUserControl.Subjects = Nothing
        '
        'AuthorUserControl
        '
        Me.AuthorUserControl.Author = ""
        Me.AuthorUserControl.Authors = Nothing
        resources.ApplyResources(Me.AuthorUserControl, "AuthorUserControl")
        Me.AuthorUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Author", Me.UploadProfileEditorViewModelBindingSource, "Author", True))
        Me.AuthorUserControl.Name = "AuthorUserControl"
        '
        'UploadOptionsUserControl
        '
        resources.ApplyResources(Me.UploadOptionsUserControl, "UploadOptionsUserControl")
        Me.UploadOptionsUserControl.DataBindings.Add(New System.Windows.Forms.Binding("FlagDocumentChecked", Me.UploadProfileEditorViewModelBindingSource, "FlagDocument", True))
        Me.UploadOptionsUserControl.DataBindings.Add(New System.Windows.Forms.Binding("OcrPdfTextAndImageDataPagesChecked", Me.UploadProfileEditorViewModelBindingSource, "OcrPdfTextAndImageDataPages", True))
        Me.UploadOptionsUserControl.FlagDocumentChecked = False
        Me.UploadOptionsUserControl.Name = "UploadOptionsUserControl"
        Me.UploadOptionsUserControl.OcrPdfTextAndImageDataPagesChecked = False
        '
        'TaxYearDropDownListUserControl
        '
        resources.ApplyResources(Me.TaxYearDropDownListUserControl, "TaxYearDropDownListUserControl")
        Me.TaxYearDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("TaxYear", Me.UploadProfileEditorViewModelBindingSource, "TaxYear", True))
        Me.TaxYearDropDownListUserControl.Name = "TaxYearDropDownListUserControl"
        Me.TaxYearDropDownListUserControl.TaxYear = ""
        Me.TaxYearDropDownListUserControl.TaxYears = Nothing
        '
        'CategoryUserControl
        '
        resources.ApplyResources(Me.CategoryUserControl, "CategoryUserControl")
        Me.CategoryUserControl.Categories = Nothing
        Me.CategoryUserControl.Category = ""
        Me.CategoryUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Category", Me.UploadProfileEditorViewModelBindingSource, "Category", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CategoryUserControl.Name = "CategoryUserControl"
        '
        'TitleUserControl
        '
        resources.ApplyResources(Me.TitleUserControl, "TitleUserControl")
        Me.TitleUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Title", Me.UploadProfileEditorViewModelBindingSource, "Title", True))
        Me.TitleUserControl.Name = "TitleUserControl"
        Me.TitleUserControl.Title = ""
        Me.TitleUserControl.TitleTokens = Nothing
        '
        'NameUserControl
        '
        resources.ApplyResources(Me.NameUserControl, "NameUserControl")
        Me.NameUserControl.DataBindings.Add(New System.Windows.Forms.Binding("TName", Me.UploadProfileEditorViewModelBindingSource, "Name", True))
        Me.NameUserControl.Name = "NameUserControl"
        Me.NameUserControl.TName = ""
        '
        'UploadProfileEditorView
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ControlBox = False
        Me.Controls.Add(Me.NameUserControl)
        Me.Controls.Add(Me.TitleUserControl)
        Me.Controls.Add(Me.KeywordsUserControl)
        Me.Controls.Add(Me.SubjectUserControl)
        Me.Controls.Add(Me.AuthorUserControl)
        Me.Controls.Add(Me.MandatoryFieldLabel)
        Me.Controls.Add(Me.SetNameToAuthorSubjectLinkLabel)
        Me.Controls.Add(Me.UploadOptionsUserControl)
        Me.Controls.Add(Me.TaxYearDropDownListUserControl)
        Me.Controls.Add(Me.CategoryUserControl)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UploadProfileEditorView"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        CType(Me.UploadProfileEditorViewModelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents CategoryUserControl As CategoryUserControl
    Friend WithEvents TaxYearDropDownListUserControl As TaxYearDropDownListUserControl
    Friend WithEvents UploadOptionsUserControl As UploadOptionsUserControl
    Friend WithEvents SetNameToAuthorSubjectLinkLabel As LinkLabel
    Friend WithEvents MandatoryFieldLabel As Label
    Friend WithEvents AuthorUserControl As AuthorUserControl
    Friend WithEvents SubjectUserControl As SubjectUserControl
    Friend WithEvents KeywordsUserControl As KeywordsUserControl
    Friend WithEvents TitleUserControl As TitleUserControl
    Friend WithEvents NameUserControl As NameUserControl
    Friend WithEvents UploadProfileEditorViewModelBindingSource As BindingSource
    Friend WithEvents HelpProvider As HelpProvider
End Class
