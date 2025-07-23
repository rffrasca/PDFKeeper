// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2025 Robert F. Frasca
// *
// * This file is part of PDFKeeper.
// *
// * PDFKeeper is free software: you can redistribute it and/or modify it
// * under the terms of the GNU General Public License as published by the
// * Free Software Foundation, either version 3 of the License, or (at your
// * option) any later version.
// *
// * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
// * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// * more details.
// *
// * You should have received a copy of the GNU General Public License along
// * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
// *****************************************************************************

namespace PDFKeeper.WinForms.Views
{
    partial class FindDocumentsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDocumentsForm));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.ClearSelectionsButton = new System.Windows.Forms.Button();
            this.FindDocumentsViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AllDocumentsRadioButton = new System.Windows.Forms.RadioButton();
            this.FindFlaggedDocumentsRadioButton = new System.Windows.Forms.RadioButton();
            this.DateAddedDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FindByDateAddedRadioButton = new System.Windows.Forms.RadioButton();
            this.FindBySelectionsRadioButton = new System.Windows.Forms.RadioButton();
            this.FindBySearchTermRadioButton = new System.Windows.Forms.RadioButton();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.TaxYearDropDownListUserControl = new PDFKeeper.WinForms.UserControls.TaxYearDropDownListUserControl();
            this.CategoryDropDownListUserControl = new PDFKeeper.WinForms.UserControls.CategoryDropDownListUserControl();
            this.SubjectDropDownListUserControl = new PDFKeeper.WinForms.UserControls.SubjectDropDownListUserControl();
            this.AuthorDropDownListUserControl = new PDFKeeper.WinForms.UserControls.AuthorDropDownListUserControl();
            this.SearchTermUserControl = new PDFKeeper.WinForms.UserControls.SearchTermUserControl();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FindDocumentsViewModelBindingSource)).BeginInit();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.ClearSelectionsButton);
            this.groupBox.Controls.Add(this.TaxYearDropDownListUserControl);
            this.groupBox.Controls.Add(this.CategoryDropDownListUserControl);
            this.groupBox.Controls.Add(this.SubjectDropDownListUserControl);
            this.groupBox.Controls.Add(this.AuthorDropDownListUserControl);
            this.groupBox.Controls.Add(this.SearchTermUserControl);
            this.groupBox.Controls.Add(this.AllDocumentsRadioButton);
            this.groupBox.Controls.Add(this.FindFlaggedDocumentsRadioButton);
            this.groupBox.Controls.Add(this.DateAddedDateTimePicker);
            this.groupBox.Controls.Add(this.FindByDateAddedRadioButton);
            this.groupBox.Controls.Add(this.FindBySelectionsRadioButton);
            this.groupBox.Controls.Add(this.FindBySearchTermRadioButton);
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Name = "groupBox";
            this.HelpProvider.SetShowHelp(this.groupBox, ((bool)(resources.GetObject("groupBox.ShowHelp"))));
            this.groupBox.TabStop = false;
            // 
            // ClearSelectionsButton
            // 
            resources.ApplyResources(this.ClearSelectionsButton, "ClearSelectionsButton");
            this.ClearSelectionsButton.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "ClearSelectionsEnabled", true));
            this.ClearSelectionsButton.Name = "ClearSelectionsButton";
            this.HelpProvider.SetShowHelp(this.ClearSelectionsButton, ((bool)(resources.GetObject("ClearSelectionsButton.ShowHelp"))));
            this.ClearSelectionsButton.UseVisualStyleBackColor = true;
            this.ClearSelectionsButton.Click += new System.EventHandler(this.ClearSelectionsButton_Click);
            // 
            // FindDocumentsViewModelBindingSource
            // 
            this.FindDocumentsViewModelBindingSource.DataSource = typeof(PDFKeeper.Core.ViewModels.FindDocumentsViewModel);
            // 
            // AllDocumentsRadioButton
            // 
            resources.ApplyResources(this.AllDocumentsRadioButton, "AllDocumentsRadioButton");
            this.AllDocumentsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.FindDocumentsViewModelBindingSource, "AllDocumentsChecked", true));
            this.AllDocumentsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "AllDocumentsEnabled", true));
            this.AllDocumentsRadioButton.Name = "AllDocumentsRadioButton";
            this.HelpProvider.SetShowHelp(this.AllDocumentsRadioButton, ((bool)(resources.GetObject("AllDocumentsRadioButton.ShowHelp"))));
            this.AllDocumentsRadioButton.TabStop = true;
            this.AllDocumentsRadioButton.UseVisualStyleBackColor = true;
            this.AllDocumentsRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // FindFlaggedDocumentsRadioButton
            // 
            resources.ApplyResources(this.FindFlaggedDocumentsRadioButton, "FindFlaggedDocumentsRadioButton");
            this.FindFlaggedDocumentsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.FindDocumentsViewModelBindingSource, "FindFlaggedDocumentsChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FindFlaggedDocumentsRadioButton.Name = "FindFlaggedDocumentsRadioButton";
            this.HelpProvider.SetShowHelp(this.FindFlaggedDocumentsRadioButton, ((bool)(resources.GetObject("FindFlaggedDocumentsRadioButton.ShowHelp"))));
            this.FindFlaggedDocumentsRadioButton.TabStop = true;
            this.FindFlaggedDocumentsRadioButton.UseVisualStyleBackColor = true;
            this.FindFlaggedDocumentsRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // DateAddedDateTimePicker
            // 
            resources.ApplyResources(this.DateAddedDateTimePicker, "DateAddedDateTimePicker");
            this.DateAddedDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.FindDocumentsViewModelBindingSource, "DateAdded", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DateAddedDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "DateAddedEnabled", true));
            this.DateAddedDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateAddedDateTimePicker.Name = "DateAddedDateTimePicker";
            this.HelpProvider.SetShowHelp(this.DateAddedDateTimePicker, ((bool)(resources.GetObject("DateAddedDateTimePicker.ShowHelp"))));
            // 
            // FindByDateAddedRadioButton
            // 
            resources.ApplyResources(this.FindByDateAddedRadioButton, "FindByDateAddedRadioButton");
            this.FindByDateAddedRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.FindDocumentsViewModelBindingSource, "FindByDateAddedChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FindByDateAddedRadioButton.Name = "FindByDateAddedRadioButton";
            this.HelpProvider.SetShowHelp(this.FindByDateAddedRadioButton, ((bool)(resources.GetObject("FindByDateAddedRadioButton.ShowHelp"))));
            this.FindByDateAddedRadioButton.TabStop = true;
            this.FindByDateAddedRadioButton.UseVisualStyleBackColor = true;
            this.FindByDateAddedRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // FindBySelectionsRadioButton
            // 
            resources.ApplyResources(this.FindBySelectionsRadioButton, "FindBySelectionsRadioButton");
            this.FindBySelectionsRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.FindDocumentsViewModelBindingSource, "FindBySelectionsChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FindBySelectionsRadioButton.Name = "FindBySelectionsRadioButton";
            this.HelpProvider.SetShowHelp(this.FindBySelectionsRadioButton, ((bool)(resources.GetObject("FindBySelectionsRadioButton.ShowHelp"))));
            this.FindBySelectionsRadioButton.TabStop = true;
            this.FindBySelectionsRadioButton.UseVisualStyleBackColor = true;
            this.FindBySelectionsRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // FindBySearchTermRadioButton
            // 
            resources.ApplyResources(this.FindBySearchTermRadioButton, "FindBySearchTermRadioButton");
            this.FindBySearchTermRadioButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.FindDocumentsViewModelBindingSource, "FindBySearchTermChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FindBySearchTermRadioButton.Name = "FindBySearchTermRadioButton";
            this.HelpProvider.SetShowHelp(this.FindBySearchTermRadioButton, ((bool)(resources.GetObject("FindBySearchTermRadioButton.ShowHelp"))));
            this.FindBySearchTermRadioButton.TabStop = true;
            this.FindBySearchTermRadioButton.UseVisualStyleBackColor = true;
            this.FindBySearchTermRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // TableLayoutPanel1
            // 
            resources.ApplyResources(this.TableLayoutPanel1, "TableLayoutPanel1");
            this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.HelpProvider.SetShowHelp(this.TableLayoutPanel1, ((bool)(resources.GetObject("TableLayoutPanel1.ShowHelp"))));
            // 
            // OK_Button
            // 
            resources.ApplyResources(this.OK_Button, "OK_Button");
            this.OK_Button.Name = "OK_Button";
            this.HelpProvider.SetShowHelp(this.OK_Button, ((bool)(resources.GetObject("OK_Button.ShowHelp"))));
            this.OK_Button.Click += new System.EventHandler(this.Button_Click);
            // 
            // Cancel_Button
            // 
            resources.ApplyResources(this.Cancel_Button, "Cancel_Button");
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Name = "Cancel_Button";
            this.HelpProvider.SetShowHelp(this.Cancel_Button, ((bool)(resources.GetObject("Cancel_Button.ShowHelp"))));
            this.Cancel_Button.Click += new System.EventHandler(this.Button_Click);
            // 
            // TaxYearDropDownListUserControl
            // 
            resources.ApplyResources(this.TaxYearDropDownListUserControl, "TaxYearDropDownListUserControl");
            this.TaxYearDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("TaxYear", this.FindDocumentsViewModelBindingSource, "TaxYear", true));
            this.TaxYearDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "TaxYearEnabled", true));
            this.TaxYearDropDownListUserControl.Name = "TaxYearDropDownListUserControl";
            this.HelpProvider.SetShowHelp(this.TaxYearDropDownListUserControl, ((bool)(resources.GetObject("TaxYearDropDownListUserControl.ShowHelp"))));
            this.TaxYearDropDownListUserControl.TaxYear = "";
            this.TaxYearDropDownListUserControl.TaxYears = null;
            this.TaxYearDropDownListUserControl.Enter += new System.EventHandler(this.UserControl_Enter);
            // 
            // CategoryDropDownListUserControl
            // 
            resources.ApplyResources(this.CategoryDropDownListUserControl, "CategoryDropDownListUserControl");
            this.CategoryDropDownListUserControl.Categories = null;
            this.CategoryDropDownListUserControl.Category = "";
            this.CategoryDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Category", this.FindDocumentsViewModelBindingSource, "Category", true));
            this.CategoryDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "CategoryEnabled", true));
            this.CategoryDropDownListUserControl.Name = "CategoryDropDownListUserControl";
            this.HelpProvider.SetShowHelp(this.CategoryDropDownListUserControl, ((bool)(resources.GetObject("CategoryDropDownListUserControl.ShowHelp"))));
            this.CategoryDropDownListUserControl.Enter += new System.EventHandler(this.UserControl_Enter);
            // 
            // SubjectDropDownListUserControl
            // 
            resources.ApplyResources(this.SubjectDropDownListUserControl, "SubjectDropDownListUserControl");
            this.SubjectDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Subject", this.FindDocumentsViewModelBindingSource, "Subject", true));
            this.SubjectDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "SubjectEnabled", true));
            this.SubjectDropDownListUserControl.Name = "SubjectDropDownListUserControl";
            this.HelpProvider.SetShowHelp(this.SubjectDropDownListUserControl, ((bool)(resources.GetObject("SubjectDropDownListUserControl.ShowHelp"))));
            this.SubjectDropDownListUserControl.Subject = "";
            this.SubjectDropDownListUserControl.Subjects = null;
            this.SubjectDropDownListUserControl.Enter += new System.EventHandler(this.UserControl_Enter);
            // 
            // AuthorDropDownListUserControl
            // 
            this.AuthorDropDownListUserControl.Author = "";
            this.AuthorDropDownListUserControl.Authors = null;
            resources.ApplyResources(this.AuthorDropDownListUserControl, "AuthorDropDownListUserControl");
            this.AuthorDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Author", this.FindDocumentsViewModelBindingSource, "Author", true));
            this.AuthorDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "AuthorEnabled", true));
            this.AuthorDropDownListUserControl.Name = "AuthorDropDownListUserControl";
            this.HelpProvider.SetShowHelp(this.AuthorDropDownListUserControl, ((bool)(resources.GetObject("AuthorDropDownListUserControl.ShowHelp"))));
            this.AuthorDropDownListUserControl.Enter += new System.EventHandler(this.UserControl_Enter);
            // 
            // SearchTermUserControl
            // 
            resources.ApplyResources(this.SearchTermUserControl, "SearchTermUserControl");
            this.SearchTermUserControl.DataBindings.Add(new System.Windows.Forms.Binding("SearchTerm", this.FindDocumentsViewModelBindingSource, "SearchTerm", true));
            this.SearchTermUserControl.DataBindings.Add(new System.Windows.Forms.Binding("SearchTerms", this.FindDocumentsViewModelBindingSource, "SearchTerms", true));
            this.SearchTermUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.FindDocumentsViewModelBindingSource, "SearchTermEnabled", true));
            this.SearchTermUserControl.Name = "SearchTermUserControl";
            this.SearchTermUserControl.SearchTerm = "";
            this.SearchTermUserControl.SearchTerms = null;
            this.HelpProvider.SetShowHelp(this.SearchTermUserControl, ((bool)(resources.GetObject("SearchTermUserControl.ShowHelp"))));
            this.SearchTermUserControl.Enter += new System.EventHandler(this.UserControl_Enter);
            // 
            // FindDocumentsForm
            // 
            this.AcceptButton = this.OK_Button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindDocumentsForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindDocumentsForm_FormClosing);
            this.Load += new System.EventHandler(this.FindDocumentsForm_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FindDocumentsViewModelBindingSource)).EndInit();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBox;
        internal System.Windows.Forms.RadioButton AllDocumentsRadioButton;
        internal System.Windows.Forms.RadioButton FindFlaggedDocumentsRadioButton;
        internal System.Windows.Forms.DateTimePicker DateAddedDateTimePicker;
        internal System.Windows.Forms.RadioButton FindByDateAddedRadioButton;
        internal System.Windows.Forms.RadioButton FindBySelectionsRadioButton;
        internal System.Windows.Forms.RadioButton FindBySearchTermRadioButton;
        internal UserControls.SearchTermUserControl SearchTermUserControl;
        internal UserControls.AuthorDropDownListUserControl AuthorDropDownListUserControl;
        internal UserControls.SubjectDropDownListUserControl SubjectDropDownListUserControl;
        internal UserControls.TaxYearDropDownListUserControl TaxYearDropDownListUserControl;
        internal UserControls.CategoryDropDownListUserControl CategoryDropDownListUserControl;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.BindingSource FindDocumentsViewModelBindingSource;
        private System.Windows.Forms.Button ClearSelectionsButton;
        private System.Windows.Forms.HelpProvider HelpProvider;
    }
}
