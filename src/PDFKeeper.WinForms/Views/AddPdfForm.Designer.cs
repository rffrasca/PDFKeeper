// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2026 Robert F. Frasca
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
    partial class AddPdfForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPdfForm));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ViewButton = new System.Windows.Forms.Button();
            this.SetTitleToPdfFileNameLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SelectedPdfTextBox = new System.Windows.Forms.TextBox();
            this.AddPdfViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SelectedPdfLabel = new System.Windows.Forms.Label();
            this.TitleTextBox = new PDFKeeper.WinForms.Components.CustomTextBox(this.components);
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.MandatoryFieldLabel = new System.Windows.Forms.Label();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.DeleteSelectedPdfWhenAddedCheckBox = new System.Windows.Forms.CheckBox();
            this.AuthorUserControl = new PDFKeeper.WinForms.UserControls.AuthorUserControl();
            this.SubjectUserControl = new PDFKeeper.WinForms.UserControls.SubjectUserControl();
            this.KeywordsUserControl = new PDFKeeper.WinForms.UserControls.KeywordsUserControl();
            this.CategoryUserControl = new PDFKeeper.WinForms.UserControls.CategoryUserControl();
            this.TaxYearDropDownListUserControl = new PDFKeeper.WinForms.UserControls.TaxYearDropDownListUserControl();
            this.UploadOptionsUserControl = new PDFKeeper.WinForms.UserControls.UploadOptionsUserControl();
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.AddPdfViewModelBindingSource)).BeginInit();
            this.TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            resources.ApplyResources(this.TitleLabel, "TitleLabel");
            this.TitleLabel.Name = "TitleLabel";
            // 
            // ViewButton
            // 
            this.ViewButton.Image = global::PDFKeeper.WinForms.Properties.Resources.file_acrobat;
            resources.ApplyResources(this.ViewButton, "ViewButton");
            this.ViewButton.Name = "ViewButton";
            this.ViewButton.UseVisualStyleBackColor = true;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // SetTitleToPdfFileNameLinkLabel
            // 
            resources.ApplyResources(this.SetTitleToPdfFileNameLinkLabel, "SetTitleToPdfFileNameLinkLabel");
            this.SetTitleToPdfFileNameLinkLabel.Name = "SetTitleToPdfFileNameLinkLabel";
            this.SetTitleToPdfFileNameLinkLabel.TabStop = true;
            this.SetTitleToPdfFileNameLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SetTitleToPdfFileNameLinkLabel_LinkClicked);
            // 
            // SelectedPdfTextBox
            // 
            this.SelectedPdfTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.AddPdfViewModelBindingSource, "SelectedPdf", true));
            resources.ApplyResources(this.SelectedPdfTextBox, "SelectedPdfTextBox");
            this.SelectedPdfTextBox.Name = "SelectedPdfTextBox";
            this.SelectedPdfTextBox.ReadOnly = true;
            this.SelectedPdfTextBox.TabStop = false;
            // 
            // AddPdfViewModelBindingSource
            // 
            this.AddPdfViewModelBindingSource.DataSource = typeof(PDFKeeper.Core.ViewModels.AddPdfViewModel);
            // 
            // SelectedPdfLabel
            // 
            resources.ApplyResources(this.SelectedPdfLabel, "SelectedPdfLabel");
            this.SelectedPdfLabel.Name = "SelectedPdfLabel";
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.AddPdfViewModelBindingSource, "Title", true));
            resources.ApplyResources(this.TitleTextBox, "TitleTextBox");
            this.TitleTextBox.Name = "TitleTextBox";
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Cancel_Button, "Cancel_Button");
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // MandatoryFieldLabel
            // 
            resources.ApplyResources(this.MandatoryFieldLabel, "MandatoryFieldLabel");
            this.MandatoryFieldLabel.Name = "MandatoryFieldLabel";
            // 
            // TableLayoutPanel
            // 
            resources.ApplyResources(this.TableLayoutPanel, "TableLayoutPanel");
            this.TableLayoutPanel.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            // 
            // OK_Button
            // 
            resources.ApplyResources(this.OK_Button, "OK_Button");
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // DeleteSelectedPdfWhenAddedCheckBox
            // 
            resources.ApplyResources(this.DeleteSelectedPdfWhenAddedCheckBox, "DeleteSelectedPdfWhenAddedCheckBox");
            this.DeleteSelectedPdfWhenAddedCheckBox.Checked = global::PDFKeeper.WinForms.Properties.Settings.Default.AddPdfDeleteSelectedPdfWhenAdded;
            this.DeleteSelectedPdfWhenAddedCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PDFKeeper.WinForms.Properties.Settings.Default, "AddPdfDeleteSelectedPdfWhenAdded", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DeleteSelectedPdfWhenAddedCheckBox.Name = "DeleteSelectedPdfWhenAddedCheckBox";
            this.DeleteSelectedPdfWhenAddedCheckBox.UseVisualStyleBackColor = true;
            // 
            // AuthorUserControl
            // 
            this.AuthorUserControl.Author = "";
            this.AuthorUserControl.Authors = null;
            resources.ApplyResources(this.AuthorUserControl, "AuthorUserControl");
            this.AuthorUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Author", this.AddPdfViewModelBindingSource, "Author", true));
            this.AuthorUserControl.Name = "AuthorUserControl";
            // 
            // SubjectUserControl
            // 
            resources.ApplyResources(this.SubjectUserControl, "SubjectUserControl");
            this.SubjectUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Subject", this.AddPdfViewModelBindingSource, "Subject", true));
            this.SubjectUserControl.Name = "SubjectUserControl";
            this.SubjectUserControl.Subject = "";
            this.SubjectUserControl.Subjects = null;
            this.SubjectUserControl.Enter += new System.EventHandler(this.SubjectUserControl_Enter);
            // 
            // KeywordsUserControl
            // 
            resources.ApplyResources(this.KeywordsUserControl, "KeywordsUserControl");
            this.KeywordsUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Keywords", this.AddPdfViewModelBindingSource, "Keywords", true));
            this.KeywordsUserControl.Keywords = "";
            this.KeywordsUserControl.Name = "KeywordsUserControl";
            // 
            // CategoryUserControl
            // 
            resources.ApplyResources(this.CategoryUserControl, "CategoryUserControl");
            this.CategoryUserControl.Categories = null;
            this.CategoryUserControl.Category = "";
            this.CategoryUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Category", this.AddPdfViewModelBindingSource, "Category", true));
            this.CategoryUserControl.Name = "CategoryUserControl";
            // 
            // TaxYearDropDownListUserControl
            // 
            resources.ApplyResources(this.TaxYearDropDownListUserControl, "TaxYearDropDownListUserControl");
            this.TaxYearDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("TaxYear", this.AddPdfViewModelBindingSource, "TaxYear", true));
            this.TaxYearDropDownListUserControl.Name = "TaxYearDropDownListUserControl";
            this.TaxYearDropDownListUserControl.TaxYear = "";
            this.TaxYearDropDownListUserControl.TaxYears = null;
            // 
            // UploadOptionsUserControl
            // 
            resources.ApplyResources(this.UploadOptionsUserControl, "UploadOptionsUserControl");
            this.UploadOptionsUserControl.DataBindings.Add(new System.Windows.Forms.Binding("FlagDocumentChecked", this.AddPdfViewModelBindingSource, "FlagDocument", true));
            this.UploadOptionsUserControl.DataBindings.Add(new System.Windows.Forms.Binding("OcrPdfTextAndImageDataPagesChecked", this.AddPdfViewModelBindingSource, "OcrPdfTextAndImageDataPages", true));
            this.UploadOptionsUserControl.FlagDocumentChecked = false;
            this.UploadOptionsUserControl.Name = "UploadOptionsUserControl";
            this.UploadOptionsUserControl.OcrPdfTextAndImageDataPagesChecked = false;
            // 
            // AddPdfForm
            // 
            this.AcceptButton = this.OK_Button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ControlBox = false;
            this.Controls.Add(this.MandatoryFieldLabel);
            this.Controls.Add(this.TableLayoutPanel);
            this.Controls.Add(this.DeleteSelectedPdfWhenAddedCheckBox);
            this.Controls.Add(this.UploadOptionsUserControl);
            this.Controls.Add(this.TaxYearDropDownListUserControl);
            this.Controls.Add(this.CategoryUserControl);
            this.Controls.Add(this.KeywordsUserControl);
            this.Controls.Add(this.SubjectUserControl);
            this.Controls.Add(this.AuthorUserControl);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.ViewButton);
            this.Controls.Add(this.SetTitleToPdfFileNameLinkLabel);
            this.Controls.Add(this.SelectedPdfTextBox);
            this.Controls.Add(this.SelectedPdfLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPdfForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddPdfForm_FormClosing);
            this.Load += new System.EventHandler(this.AddPdfForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AddPdfViewModelBindingSource)).EndInit();
            this.TableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label TitleLabel;
        internal System.Windows.Forms.Button ViewButton;
        internal System.Windows.Forms.LinkLabel SetTitleToPdfFileNameLinkLabel;
        internal System.Windows.Forms.TextBox SelectedPdfTextBox;
        internal System.Windows.Forms.Label SelectedPdfLabel;
        internal Components.CustomTextBox TitleTextBox;
        private System.Windows.Forms.BindingSource AddPdfViewModelBindingSource;
        internal UserControls.AuthorUserControl AuthorUserControl;
        internal UserControls.SubjectUserControl SubjectUserControl;
        internal UserControls.KeywordsUserControl KeywordsUserControl;
        internal UserControls.CategoryUserControl CategoryUserControl;
        internal UserControls.TaxYearDropDownListUserControl TaxYearDropDownListUserControl;
        internal UserControls.UploadOptionsUserControl UploadOptionsUserControl;
        internal System.Windows.Forms.Button Cancel_Button;
        internal System.Windows.Forms.Label MandatoryFieldLabel;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.CheckBox DeleteSelectedPdfWhenAddedCheckBox;
        private System.Windows.Forms.HelpProvider HelpProvider;
    }
}
