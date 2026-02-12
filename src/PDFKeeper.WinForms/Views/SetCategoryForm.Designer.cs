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
    partial class SetCategoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetCategoryForm));
            this.CategoryUserControl = new PDFKeeper.WinForms.UserControls.CategoryUserControl();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.ColumnDataListViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnDataListViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // CategoryUserControl
            // 
            resources.ApplyResources(this.CategoryUserControl, "CategoryUserControl");
            this.CategoryUserControl.Categories = null;
            this.CategoryUserControl.Category = "";
            this.CategoryUserControl.DataBindings.Add(new System.Windows.Forms.Binding("Categories", this.ColumnDataListViewModelBindingSource, "Items", true));
            this.CategoryUserControl.Name = "CategoryUserControl";
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
            // SetCategoryForm
            // 
            this.AcceptButton = this.OK_Button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.Controls.Add(this.TableLayoutPanel);
            this.Controls.Add(this.CategoryUserControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetCategoryForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            this.TableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColumnDataListViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal UserControls.CategoryUserControl CategoryUserControl;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        internal System.Windows.Forms.HelpProvider HelpProvider;
        private System.Windows.Forms.BindingSource ColumnDataListViewModelBindingSource;
    }
}
