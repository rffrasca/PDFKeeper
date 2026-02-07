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

namespace PDFKeeper.WinForms.Dialogs
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.OK_Button = new System.Windows.Forms.Button();
            this.ShowAllDocumentsOnStartupCheckBox = new System.Windows.Forms.CheckBox();
            this.FindFlaggedDocumentsOnStartupCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowPdfWithDefaultApplicationCheckBox = new System.Windows.Forms.CheckBox();
            this.SelectLastRowWhenListingDocumentsCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox = new System.Windows.Forms.CheckBox();
            this.DocumentsViewColumnHeaderAliasesGroupBox = new System.Windows.Forms.GroupBox();
            this.TaxYearTextBox = new System.Windows.Forms.TextBox();
            this.TaxYearLabel = new System.Windows.Forms.Label();
            this.CategoryTextBox = new System.Windows.Forms.TextBox();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.SubjectTextBox = new System.Windows.Forms.TextBox();
            this.SubjectLabel = new System.Windows.Forms.Label();
            this.AuthorTextBox = new System.Windows.Forms.TextBox();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.ResetAliasesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel.SuspendLayout();
            this.DocumentsViewColumnHeaderAliasesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OK_Button
            // 
            resources.ApplyResources(this.OK_Button, "OK_Button");
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // ShowAllDocumentsOnStartupCheckBox
            // 
            resources.ApplyResources(this.ShowAllDocumentsOnStartupCheckBox, "ShowAllDocumentsOnStartupCheckBox");
            this.ShowAllDocumentsOnStartupCheckBox.Checked = global::PDFKeeper.WinForms.Properties.Settings.Default.ShowAllDocumentsOnStartup;
            this.ShowAllDocumentsOnStartupCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PDFKeeper.WinForms.Properties.Settings.Default, "ShowAllDocumentsOnStartup", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowAllDocumentsOnStartupCheckBox.Name = "ShowAllDocumentsOnStartupCheckBox";
            this.ShowAllDocumentsOnStartupCheckBox.UseVisualStyleBackColor = true;
            this.ShowAllDocumentsOnStartupCheckBox.CheckStateChanged += new System.EventHandler(this.ShowAllDocumentsOnStartupCheckBox_CheckStateChanged);
            // 
            // FindFlaggedDocumentsOnStartupCheckBox
            // 
            resources.ApplyResources(this.FindFlaggedDocumentsOnStartupCheckBox, "FindFlaggedDocumentsOnStartupCheckBox");
            this.FindFlaggedDocumentsOnStartupCheckBox.Checked = global::PDFKeeper.WinForms.Properties.Settings.Default.FindFlaggedDocumentsOnStartup;
            this.FindFlaggedDocumentsOnStartupCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PDFKeeper.WinForms.Properties.Settings.Default, "FindFlaggedDocumentsOnStartup", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FindFlaggedDocumentsOnStartupCheckBox.Name = "FindFlaggedDocumentsOnStartupCheckBox";
            this.FindFlaggedDocumentsOnStartupCheckBox.UseVisualStyleBackColor = true;
            this.FindFlaggedDocumentsOnStartupCheckBox.CheckStateChanged += new System.EventHandler(this.FindFlaggedDocumentsOnStartupCheckBox_CheckStateChanged);
            // 
            // ShowPdfWithDefaultApplicationCheckBox
            // 
            resources.ApplyResources(this.ShowPdfWithDefaultApplicationCheckBox, "ShowPdfWithDefaultApplicationCheckBox");
            this.ShowPdfWithDefaultApplicationCheckBox.Checked = global::PDFKeeper.WinForms.Properties.Settings.Default.ShowPdfWithDefaultApplication;
            this.ShowPdfWithDefaultApplicationCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PDFKeeper.WinForms.Properties.Settings.Default, "ShowPdfWithDefaultApplication", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowPdfWithDefaultApplicationCheckBox.Name = "ShowPdfWithDefaultApplicationCheckBox";
            this.ShowPdfWithDefaultApplicationCheckBox.UseVisualStyleBackColor = true;
            // 
            // SelectLastRowWhenListingDocumentsCheckBox
            // 
            resources.ApplyResources(this.SelectLastRowWhenListingDocumentsCheckBox, "SelectLastRowWhenListingDocumentsCheckBox");
            this.SelectLastRowWhenListingDocumentsCheckBox.Checked = global::PDFKeeper.WinForms.Properties.Settings.Default.SelectLastDocumentRow;
            this.SelectLastRowWhenListingDocumentsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PDFKeeper.WinForms.Properties.Settings.Default, "SelectLastDocumentRow", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SelectLastRowWhenListingDocumentsCheckBox.Name = "SelectLastRowWhenListingDocumentsCheckBox";
            this.SelectLastRowWhenListingDocumentsCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.OK_Button, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // CompactDatabaseAfterDeletingSelectedDocumentsCheckBox
            // 
            resources.ApplyResources(this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox, "CompactDatabaseAfterDeletingSelectedDocumentsCheckBox");
            this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox.Checked = global::PDFKeeper.WinForms.Properties.Settings.Default.CompactLocalDatabaseAfterDelete;
            this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PDFKeeper.WinForms.Properties.Settings.Default, "CompactLocalDatabaseAfterDelete", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox.Name = "CompactDatabaseAfterDeletingSelectedDocumentsCheckBox";
            this.HelpProvider.SetShowHelp(this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox, ((bool)(resources.GetObject("CompactDatabaseAfterDeletingSelectedDocumentsCheckBox.ShowHelp"))));
            this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DocumentsViewColumnHeaderAliasesGroupBox
            // 
            resources.ApplyResources(this.DocumentsViewColumnHeaderAliasesGroupBox, "DocumentsViewColumnHeaderAliasesGroupBox");
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.TaxYearTextBox);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.TaxYearLabel);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.CategoryTextBox);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.CategoryLabel);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.SubjectTextBox);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.SubjectLabel);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.AuthorTextBox);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Controls.Add(this.AuthorLabel);
            this.DocumentsViewColumnHeaderAliasesGroupBox.Name = "DocumentsViewColumnHeaderAliasesGroupBox";
            this.DocumentsViewColumnHeaderAliasesGroupBox.TabStop = false;
            // 
            // TaxYearTextBox
            // 
            resources.ApplyResources(this.TaxYearTextBox, "TaxYearTextBox");
            this.TaxYearTextBox.Name = "TaxYearTextBox";
            // 
            // TaxYearLabel
            // 
            resources.ApplyResources(this.TaxYearLabel, "TaxYearLabel");
            this.TaxYearLabel.Name = "TaxYearLabel";
            // 
            // CategoryTextBox
            // 
            resources.ApplyResources(this.CategoryTextBox, "CategoryTextBox");
            this.CategoryTextBox.Name = "CategoryTextBox";
            // 
            // CategoryLabel
            // 
            resources.ApplyResources(this.CategoryLabel, "CategoryLabel");
            this.CategoryLabel.Name = "CategoryLabel";
            // 
            // SubjectTextBox
            // 
            resources.ApplyResources(this.SubjectTextBox, "SubjectTextBox");
            this.SubjectTextBox.Name = "SubjectTextBox";
            // 
            // SubjectLabel
            // 
            resources.ApplyResources(this.SubjectLabel, "SubjectLabel");
            this.SubjectLabel.Name = "SubjectLabel";
            // 
            // AuthorTextBox
            // 
            resources.ApplyResources(this.AuthorTextBox, "AuthorTextBox");
            this.AuthorTextBox.Name = "AuthorTextBox";
            // 
            // AuthorLabel
            // 
            resources.ApplyResources(this.AuthorLabel, "AuthorLabel");
            this.AuthorLabel.Name = "AuthorLabel";
            // 
            // ResetAliasesLinkLabel
            // 
            resources.ApplyResources(this.ResetAliasesLinkLabel, "ResetAliasesLinkLabel");
            this.ResetAliasesLinkLabel.Name = "ResetAliasesLinkLabel";
            this.ResetAliasesLinkLabel.TabStop = true;
            this.ResetAliasesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ResetAliasesLinkLabel_LinkClicked);
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.OK_Button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.ResetAliasesLinkLabel);
            this.Controls.Add(this.DocumentsViewColumnHeaderAliasesGroupBox);
            this.Controls.Add(this.CompactDatabaseAfterDeletingSelectedDocumentsCheckBox);
            this.Controls.Add(this.ShowAllDocumentsOnStartupCheckBox);
            this.Controls.Add(this.FindFlaggedDocumentsOnStartupCheckBox);
            this.Controls.Add(this.ShowPdfWithDefaultApplicationCheckBox);
            this.Controls.Add(this.SelectLastRowWhenListingDocumentsCheckBox);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            this.tableLayoutPanel.ResumeLayout(false);
            this.DocumentsViewColumnHeaderAliasesGroupBox.ResumeLayout(false);
            this.DocumentsViewColumnHeaderAliasesGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.CheckBox ShowAllDocumentsOnStartupCheckBox;
        internal System.Windows.Forms.CheckBox FindFlaggedDocumentsOnStartupCheckBox;
        internal System.Windows.Forms.CheckBox ShowPdfWithDefaultApplicationCheckBox;
        internal System.Windows.Forms.CheckBox SelectLastRowWhenListingDocumentsCheckBox;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        internal System.Windows.Forms.HelpProvider HelpProvider;
        private System.Windows.Forms.CheckBox CompactDatabaseAfterDeletingSelectedDocumentsCheckBox;
        private System.Windows.Forms.GroupBox DocumentsViewColumnHeaderAliasesGroupBox;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label SubjectLabel;
        private System.Windows.Forms.TextBox AuthorTextBox;
        private System.Windows.Forms.TextBox SubjectTextBox;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.TextBox CategoryTextBox;
        private System.Windows.Forms.Label TaxYearLabel;
        private System.Windows.Forms.TextBox TaxYearTextBox;
        private System.Windows.Forms.LinkLabel ResetAliasesLinkLabel;
    }
}
