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
Partial Class FindDocumentsView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FindDocumentsView))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.GroupBox = New System.Windows.Forms.GroupBox()
        Me.SearchTermUserControl = New PDFKeeper.Presentation.SearchTermUserControl()
        Me.FindDocumentsViewModelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CategoryDropDownListUserControl = New PDFKeeper.Presentation.CategoryDropDownListUserControl()
        Me.SubjectDropDownListUserControl = New PDFKeeper.Presentation.SubjectDropDownListUserControl()
        Me.AuthorDropDownListUserControl = New PDFKeeper.Presentation.AuthorDropDownListUserControl()
        Me.AllDocumentsRadioButton = New System.Windows.Forms.RadioButton()
        Me.FindFlaggedDocumentsRadioButton = New System.Windows.Forms.RadioButton()
        Me.DateAddedDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.FindByDateAddedRadioButton = New System.Windows.Forms.RadioButton()
        Me.TaxYearDropDownListUserControl = New PDFKeeper.Presentation.TaxYearDropDownListUserControl()
        Me.FindBySelectionsRadioButton = New System.Windows.Forms.RadioButton()
        Me.FindBySearchTermRadioButton = New System.Windows.Forms.RadioButton()
        Me.AuthorComboBox = New PDFKeeper.Presentation.CustomComboBox(Me.components)
        Me.HelpProvider = New System.Windows.Forms.HelpProvider()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox.SuspendLayout()
        CType(Me.FindDocumentsViewModelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'GroupBox
        '
        Me.GroupBox.Controls.Add(Me.SearchTermUserControl)
        Me.GroupBox.Controls.Add(Me.CategoryDropDownListUserControl)
        Me.GroupBox.Controls.Add(Me.SubjectDropDownListUserControl)
        Me.GroupBox.Controls.Add(Me.AuthorDropDownListUserControl)
        Me.GroupBox.Controls.Add(Me.AllDocumentsRadioButton)
        Me.GroupBox.Controls.Add(Me.FindFlaggedDocumentsRadioButton)
        Me.GroupBox.Controls.Add(Me.DateAddedDateTimePicker)
        Me.GroupBox.Controls.Add(Me.FindByDateAddedRadioButton)
        Me.GroupBox.Controls.Add(Me.TaxYearDropDownListUserControl)
        Me.GroupBox.Controls.Add(Me.FindBySelectionsRadioButton)
        Me.GroupBox.Controls.Add(Me.FindBySearchTermRadioButton)
        resources.ApplyResources(Me.GroupBox, "GroupBox")
        Me.GroupBox.Name = "GroupBox"
        Me.GroupBox.TabStop = False
        '
        'SearchTermUserControl
        '
        resources.ApplyResources(Me.SearchTermUserControl, "SearchTermUserControl")
        Me.SearchTermUserControl.DataBindings.Add(New System.Windows.Forms.Binding("SearchTerm", Me.FindDocumentsViewModelBindingSource, "SearchTerm", True))
        Me.SearchTermUserControl.DataBindings.Add(New System.Windows.Forms.Binding("SearchTerms", Me.FindDocumentsViewModelBindingSource, "SearchTerms", True))
        Me.SearchTermUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.FindDocumentsViewModelBindingSource, "SearchTermEnabled", True))
        Me.SearchTermUserControl.Name = "SearchTermUserControl"
        Me.SearchTermUserControl.SearchTerm = ""
        Me.SearchTermUserControl.SearchTerms = Nothing
        '
        'FindDocumentsViewModelBindingSource
        '
        Me.FindDocumentsViewModelBindingSource.DataSource = GetType(PDFKeeper.Core.ViewModels.FindDocumentsViewModel)
        '
        'CategoryDropDownListUserControl
        '
        resources.ApplyResources(Me.CategoryDropDownListUserControl, "CategoryDropDownListUserControl")
        Me.CategoryDropDownListUserControl.Categories = Nothing
        Me.CategoryDropDownListUserControl.Category = ""
        Me.CategoryDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Category", Me.FindDocumentsViewModelBindingSource, "Category", True))
        Me.CategoryDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.FindDocumentsViewModelBindingSource, "CategoryEnabled", True))
        Me.CategoryDropDownListUserControl.Name = "CategoryDropDownListUserControl"
        '
        'SubjectDropDownListUserControl
        '
        resources.ApplyResources(Me.SubjectDropDownListUserControl, "SubjectDropDownListUserControl")
        Me.SubjectDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Subject", Me.FindDocumentsViewModelBindingSource, "Subject", True))
        Me.SubjectDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.FindDocumentsViewModelBindingSource, "SubjectEnabled", True))
        Me.SubjectDropDownListUserControl.Name = "SubjectDropDownListUserControl"
        Me.SubjectDropDownListUserControl.Subject = ""
        Me.SubjectDropDownListUserControl.Subjects = Nothing
        '
        'AuthorDropDownListUserControl
        '
        Me.AuthorDropDownListUserControl.Author = ""
        Me.AuthorDropDownListUserControl.Authors = Nothing
        resources.ApplyResources(Me.AuthorDropDownListUserControl, "AuthorDropDownListUserControl")
        Me.AuthorDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Author", Me.FindDocumentsViewModelBindingSource, "Author", True))
        Me.AuthorDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.FindDocumentsViewModelBindingSource, "AuthorEnabled", True))
        Me.AuthorDropDownListUserControl.Name = "AuthorDropDownListUserControl"
        '
        'AllDocumentsRadioButton
        '
        resources.ApplyResources(Me.AllDocumentsRadioButton, "AllDocumentsRadioButton")
        Me.AllDocumentsRadioButton.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.FindDocumentsViewModelBindingSource, "AllDocumentsChecked", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.AllDocumentsRadioButton.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.FindDocumentsViewModelBindingSource, "AllDocumentsEnabled", True))
        Me.AllDocumentsRadioButton.Name = "AllDocumentsRadioButton"
        Me.AllDocumentsRadioButton.TabStop = True
        Me.AllDocumentsRadioButton.UseVisualStyleBackColor = True
        '
        'FindFlaggedDocumentsRadioButton
        '
        resources.ApplyResources(Me.FindFlaggedDocumentsRadioButton, "FindFlaggedDocumentsRadioButton")
        Me.FindFlaggedDocumentsRadioButton.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.FindDocumentsViewModelBindingSource, "FindFlaggedDocumentsChecked", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FindFlaggedDocumentsRadioButton.Name = "FindFlaggedDocumentsRadioButton"
        Me.FindFlaggedDocumentsRadioButton.TabStop = True
        Me.FindFlaggedDocumentsRadioButton.UseVisualStyleBackColor = True
        '
        'DateAddedDateTimePicker
        '
        resources.ApplyResources(Me.DateAddedDateTimePicker, "DateAddedDateTimePicker")
        Me.DateAddedDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.FindDocumentsViewModelBindingSource, "DateAdded", True))
        Me.DateAddedDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.FindDocumentsViewModelBindingSource, "DateAddedEnabled", True))
        Me.DateAddedDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateAddedDateTimePicker.Name = "DateAddedDateTimePicker"
        '
        'FindByDateAddedRadioButton
        '
        resources.ApplyResources(Me.FindByDateAddedRadioButton, "FindByDateAddedRadioButton")
        Me.FindByDateAddedRadioButton.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.FindDocumentsViewModelBindingSource, "FindByDateAddedChecked", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FindByDateAddedRadioButton.Name = "FindByDateAddedRadioButton"
        Me.FindByDateAddedRadioButton.TabStop = True
        Me.FindByDateAddedRadioButton.UseVisualStyleBackColor = True
        '
        'TaxYearDropDownListUserControl
        '
        resources.ApplyResources(Me.TaxYearDropDownListUserControl, "TaxYearDropDownListUserControl")
        Me.TaxYearDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("TaxYear", Me.FindDocumentsViewModelBindingSource, "TaxYear", True))
        Me.TaxYearDropDownListUserControl.DataBindings.Add(New System.Windows.Forms.Binding("Enabled", Me.FindDocumentsViewModelBindingSource, "TaxYearEnabled", True))
        Me.TaxYearDropDownListUserControl.Name = "TaxYearDropDownListUserControl"
        Me.TaxYearDropDownListUserControl.TaxYear = ""
        Me.TaxYearDropDownListUserControl.TaxYears = Nothing
        '
        'FindBySelectionsRadioButton
        '
        resources.ApplyResources(Me.FindBySelectionsRadioButton, "FindBySelectionsRadioButton")
        Me.FindBySelectionsRadioButton.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.FindDocumentsViewModelBindingSource, "FindBySelectionsChecked", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FindBySelectionsRadioButton.Name = "FindBySelectionsRadioButton"
        Me.FindBySelectionsRadioButton.TabStop = True
        Me.FindBySelectionsRadioButton.UseVisualStyleBackColor = True
        '
        'FindBySearchTermRadioButton
        '
        resources.ApplyResources(Me.FindBySearchTermRadioButton, "FindBySearchTermRadioButton")
        Me.FindBySearchTermRadioButton.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.FindDocumentsViewModelBindingSource, "FindBySearchTermChecked", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FindBySearchTermRadioButton.Name = "FindBySearchTermRadioButton"
        Me.FindBySearchTermRadioButton.TabStop = True
        Me.FindBySearchTermRadioButton.UseVisualStyleBackColor = True
        '
        'AuthorComboBox
        '
        Me.AuthorComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.AuthorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AuthorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.AuthorComboBox, "AuthorComboBox")
        Me.AuthorComboBox.FormattingEnabled = True
        Me.AuthorComboBox.Name = "AuthorComboBox"
        '
        'FindDocumentsView
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.GroupBox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpProvider.SetHelpKeyword(Me, resources.GetString("$this.HelpKeyword"))
        Me.HelpProvider.SetHelpNavigator(Me, CType(resources.GetObject("$this.HelpNavigator"), System.Windows.Forms.HelpNavigator))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FindDocumentsView"
        Me.HelpProvider.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox.ResumeLayout(False)
        Me.GroupBox.PerformLayout()
        CType(Me.FindDocumentsViewModelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox As GroupBox
    Friend WithEvents FindBySearchTermRadioButton As RadioButton
    Friend WithEvents FindBySelectionsRadioButton As RadioButton
    Friend WithEvents TaxYearDropDownListUserControl As TaxYearDropDownListUserControl
    Friend WithEvents FindByDateAddedRadioButton As RadioButton
    Friend WithEvents DateAddedDateTimePicker As DateTimePicker
    Friend WithEvents AllDocumentsRadioButton As RadioButton
    Friend WithEvents FindFlaggedDocumentsRadioButton As RadioButton
    Friend WithEvents SubjectDropDownListUserControl As SubjectDropDownListUserControl
    Friend WithEvents AuthorDropDownListUserControl As AuthorDropDownListUserControl
    Friend WithEvents AuthorComboBox As CustomComboBox
    Friend WithEvents CategoryDropDownListUserControl As CategoryDropDownListUserControl
    Friend WithEvents SearchTermUserControl As SearchTermUserControl
    Friend WithEvents FindDocumentsViewModelBindingSource As BindingSource
    Friend WithEvents HelpProvider As HelpProvider
End Class
