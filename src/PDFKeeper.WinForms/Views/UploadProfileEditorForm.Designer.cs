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
    partial class UploadProfileEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadProfileEditorForm));
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.SetNameToAuthorSubjectLinkLabel = new System.Windows.Forms.LinkLabel();
            this.MandatoryFieldLabel = new System.Windows.Forms.Label();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SetNameToSubjectLinkLabel = new System.Windows.Forms.LinkLabel();
            this.NameUserControl = new PDFKeeper.WinForms.UserControls.NameUserControl();
            this.UploadProfileEditorViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TitleUserControl = new PDFKeeper.WinForms.UserControls.TitleUserControl();
            this.AuthorUserControl = new PDFKeeper.WinForms.UserControls.AuthorUserControl();
            this.SubjectUserControl = new PDFKeeper.WinForms.UserControls.SubjectUserControl();
            this.KeywordsUserControl = new PDFKeeper.WinForms.UserControls.KeywordsUserControl();
            this.CategoryUserControl = new PDFKeeper.WinForms.UserControls.CategoryUserControl();
            this.TaxYearDropDownListUserControl = new PDFKeeper.WinForms.UserControls.TaxYearDropDownListUserControl();
            this.UploadOptionsUserControl = new PDFKeeper.WinForms.UserControls.UploadOptionsUserControl();
            this.TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UploadProfileEditorViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SetNameToAuthorSubjectLinkLabel
            // 
            resources.ApplyResources(this.SetNameToAuthorSubjectLinkLabel, "SetNameToAuthorSubjectLinkLabel");
            this.SetNameToAuthorSubjectLinkLabel.Name = "SetNameToAuthorSubjectLinkLabel";
            this.HelpProvider.SetShowHelp(this.SetNameToAuthorSubjectLinkLabel, ((bool)(resources.GetObject("SetNameToAuthorSubjectLinkLabel.ShowHelp"))));
            this.SetNameToAuthorSubjectLinkLabel.TabStop = true;
            this.SetNameToAuthorSubjectLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // MandatoryFieldLabel
            // 
            resources.ApplyResources(this.MandatoryFieldLabel, "MandatoryFieldLabel");
            this.MandatoryFieldLabel.Name = "MandatoryFieldLabel";
            this.HelpProvider.SetShowHelp(this.MandatoryFieldLabel, ((bool)(resources.GetObject("MandatoryFieldLabel.ShowHelp"))));
            // 
            // TableLayoutPanel
            // 
            resources.ApplyResources(this.TableLayoutPanel, "TableLayoutPanel");
            this.TableLayoutPanel.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.HelpProvider.SetShowHelp(this.TableLayoutPanel, ((bool)(resources.GetObject("TableLayoutPanel.ShowHelp"))));
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
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Cancel_Button, "Cancel_Button");
            this.Cancel_Button.Name = "Cancel_Button";
            this.HelpProvider.SetShowHelp(this.Cancel_Button, ((bool)(resources.GetObject("Cancel_Button.ShowHelp"))));
            this.Cancel_Button.Click += new System.EventHandler(this.Button_Click);
            // 
            // SetNameToSubjectLinkLabel
            // 
            resources.ApplyResources(this.SetNameToSubjectLinkLabel, "SetNameToSubjectLinkLabel");
            this.SetNameToSubjectLinkLabel.Name = "SetNameToSubjectLinkLabel";
            this.HelpProvider.SetShowHelp(this.SetNameToSubjectLinkLabel, ((bool)(resources.GetObject("SetNameToSubjectLinkLabel.ShowHelp"))));
            this.SetNameToSubjectLinkLabel.TabStop = true;
            this.SetNameToSubjectLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // NameUserControl
            // 
            resources.ApplyResources(this.NameUserControl, "NameUserControl");
            this.NameUserControl.DataBindings.Add(new System.Windows.Forms.Binding("TName", this.UploadProfileEditorViewModelBindingSource, "Name", true));
            this.NameUserControl.Name = "NameUserControl";
            this.NameUserControl.TName = "";
            // 
            // UploadProfileEditorViewModelBindingSource
            // 
            this.UploadProfileEditorViewModelBindingSource.DataSource = typeof(PDFKeeper.Core.ViewModels.UploadProfileEditorViewModel);
            // 
            // TitleUserControl
            // 
            resources.ApplyResources(this.TitleUserControl, "TitleUserControl");
            this.TitleUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Title", this.UploadProfileEditorViewModelBindingSource, "Title", true));
            this.TitleUserControl.Name = "TitleUserControl";
            this.TitleUserControl.Title = "";
            this.TitleUserControl.TitleTokens = null;
            // 
            // AuthorUserControl
            // 
            this.AuthorUserControl.Author = "";
            this.AuthorUserControl.Authors = null;
            resources.ApplyResources(this.AuthorUserControl, "AuthorUserControl");
            this.AuthorUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Author", this.UploadProfileEditorViewModelBindingSource, "Author", true));
            this.AuthorUserControl.Name = "AuthorUserControl";
            // 
            // SubjectUserControl
            // 
            resources.ApplyResources(this.SubjectUserControl, "SubjectUserControl");
            this.SubjectUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Subject", this.UploadProfileEditorViewModelBindingSource, "Subject", true));
            this.SubjectUserControl.Name = "SubjectUserControl";
            this.SubjectUserControl.Subject = "";
            this.SubjectUserControl.Subjects = null;
            this.SubjectUserControl.Enter += new System.EventHandler(this.SubjectUserControl_Enter);
            // 
            // KeywordsUserControl
            // 
            resources.ApplyResources(this.KeywordsUserControl, "KeywordsUserControl");
            this.KeywordsUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Keywords", this.UploadProfileEditorViewModelBindingSource, "Keywords", true));
            this.KeywordsUserControl.Keywords = "";
            this.KeywordsUserControl.Name = "KeywordsUserControl";
            // 
            // CategoryUserControl
            // 
            resources.ApplyResources(this.CategoryUserControl, "CategoryUserControl");
            this.CategoryUserControl.Categories = null;
            this.CategoryUserControl.Category = "";
            this.CategoryUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Category", this.UploadProfileEditorViewModelBindingSource, "Category", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CategoryUserControl.Name = "CategoryUserControl";
            // 
            // TaxYearDropDownListUserControl
            // 
            resources.ApplyResources(this.TaxYearDropDownListUserControl, "TaxYearDropDownListUserControl");
            this.TaxYearDropDownListUserControl.DataBindings.Add(new System.Windows.Forms.Binding("TaxYear", this.UploadProfileEditorViewModelBindingSource, "TaxYear", true));
            this.TaxYearDropDownListUserControl.Name = "TaxYearDropDownListUserControl";
            this.TaxYearDropDownListUserControl.TaxYear = "";
            this.TaxYearDropDownListUserControl.TaxYears = null;
            // 
            // UploadOptionsUserControl
            // 
            resources.ApplyResources(this.UploadOptionsUserControl, "UploadOptionsUserControl");
            this.UploadOptionsUserControl.DataBindings.Add(new System.Windows.Forms.Binding("FlagDocumentChecked", this.UploadProfileEditorViewModelBindingSource, "FlagDocument", true));
            this.UploadOptionsUserControl.DataBindings.Add(new System.Windows.Forms.Binding("OcrPdfTextAndImageDataPagesChecked", this.UploadProfileEditorViewModelBindingSource, "OcrPdfTextAndImageDataPages", true));
            this.UploadOptionsUserControl.FlagDocumentChecked = false;
            this.UploadOptionsUserControl.Name = "UploadOptionsUserControl";
            this.UploadOptionsUserControl.OcrPdfTextAndImageDataPagesChecked = false;
            this.UploadOptionsUserControl.Leave += new System.EventHandler(this.UploadOptionsUserControl_Leave);
            // 
            // UploadProfileEditorForm
            // 
            this.AcceptButton = this.OK_Button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ControlBox = false;
            this.Controls.Add(this.SetNameToSubjectLinkLabel);
            this.Controls.Add(this.TableLayoutPanel);
            this.Controls.Add(this.MandatoryFieldLabel);
            this.Controls.Add(this.UploadOptionsUserControl);
            this.Controls.Add(this.TaxYearDropDownListUserControl);
            this.Controls.Add(this.CategoryUserControl);
            this.Controls.Add(this.KeywordsUserControl);
            this.Controls.Add(this.SetNameToAuthorSubjectLinkLabel);
            this.Controls.Add(this.SubjectUserControl);
            this.Controls.Add(this.AuthorUserControl);
            this.Controls.Add(this.TitleUserControl);
            this.Controls.Add(this.NameUserControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadProfileEditorForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UploadProfileEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.UploadProfileEditorForm_Load);
            this.TableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UploadProfileEditorViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.HelpProvider HelpProvider;
        internal UserControls.NameUserControl NameUserControl;
        internal UserControls.TitleUserControl TitleUserControl;
        internal UserControls.AuthorUserControl AuthorUserControl;
        internal UserControls.SubjectUserControl SubjectUserControl;
        internal System.Windows.Forms.LinkLabel SetNameToAuthorSubjectLinkLabel;
        internal UserControls.KeywordsUserControl KeywordsUserControl;
        internal UserControls.CategoryUserControl CategoryUserControl;
        internal UserControls.TaxYearDropDownListUserControl TaxYearDropDownListUserControl;
        internal UserControls.UploadOptionsUserControl UploadOptionsUserControl;
        internal System.Windows.Forms.Label MandatoryFieldLabel;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.BindingSource UploadProfileEditorViewModelBindingSource;
        internal System.Windows.Forms.LinkLabel SetNameToSubjectLinkLabel;
    }
}