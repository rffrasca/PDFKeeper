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
    partial class SetAuthorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetAuthorForm));
            this.AuthorUserControl = new PDFKeeper.WinForms.UserControls.AuthorUserControl();
            this.label = new System.Windows.Forms.Label();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.ColumnDataListViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnDataListViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // AuthorUserControl
            // 
            this.AuthorUserControl.Author = "";
            this.AuthorUserControl.Authors = null;
            resources.ApplyResources(this.AuthorUserControl, "AuthorUserControl");
            this.AuthorUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Authors", this.ColumnDataListViewModelBindingSource, "Items", true));
            this.AuthorUserControl.Name = "AuthorUserControl";
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // TableLayoutPanel1
            // 
            resources.ApplyResources(this.TableLayoutPanel1, "TableLayoutPanel1");
            this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            // 
            // OK_Button
            // 
            resources.ApplyResources(this.OK_Button, "OK_Button");
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Cancel_Button, "Cancel_Button");
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // ColumnDataListViewModelBindingSource
            // 
            this.ColumnDataListViewModelBindingSource.DataSource = typeof(PDFKeeper.Core.ViewModels.ColumnDataListViewModel);
            // 
            // SetAuthorForm
            // 
            this.AcceptButton = this.OK_Button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.AuthorUserControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetAuthorForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            this.TableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColumnDataListViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal UserControls.AuthorUserControl AuthorUserControl;
        internal System.Windows.Forms.Label label;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        internal System.Windows.Forms.HelpProvider HelpProvider;
        private System.Windows.Forms.BindingSource ColumnDataListViewModelBindingSource;
    }
}
