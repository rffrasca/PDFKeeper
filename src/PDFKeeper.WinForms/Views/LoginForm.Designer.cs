// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.DatasourceLabel = new System.Windows.Forms.Label();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.OK_Button = new System.Windows.Forms.Button();
            this.DatasourceTextBox = new PDFKeeper.WinForms.Components.CustomTextBox(this.components);
            this.PasswordSecureTextBox = new PDFKeeper.WinForms.Components.SecureTextBox(this.components);
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.HelpLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox.Image = global::PDFKeeper.WinForms.Properties.Resources.PDFKeeper_200x200;
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PDFKeeper.WinForms.Properties.Settings.Default, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.UsernameTextBox, "UsernameTextBox");
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Text = global::PDFKeeper.WinForms.Properties.Settings.Default.Username;
            // 
            // UsernameLabel
            // 
            resources.ApplyResources(this.UsernameLabel, "UsernameLabel");
            this.UsernameLabel.Name = "UsernameLabel";
            // 
            // PasswordLabel
            // 
            resources.ApplyResources(this.PasswordLabel, "PasswordLabel");
            this.PasswordLabel.Name = "PasswordLabel";
            // 
            // DatasourceLabel
            // 
            resources.ApplyResources(this.DatasourceLabel, "DatasourceLabel");
            this.DatasourceLabel.Name = "DatasourceLabel";
            // 
            // Cancel_Button
            // 
            resources.ApplyResources(this.Cancel_Button, "Cancel_Button");
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // OK_Button
            // 
            resources.ApplyResources(this.OK_Button, "OK_Button");
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // DatasourceTextBox
            // 
            this.DatasourceTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PDFKeeper.WinForms.Properties.Settings.Default, "Datasource", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.DatasourceTextBox, "DatasourceTextBox");
            this.DatasourceTextBox.Name = "DatasourceTextBox";
            this.DatasourceTextBox.Text = global::PDFKeeper.WinForms.Properties.Settings.Default.Datasource;
            // 
            // PasswordSecureTextBox
            // 
            resources.ApplyResources(this.PasswordSecureTextBox, "PasswordSecureTextBox");
            this.PasswordSecureTextBox.Name = "PasswordSecureTextBox";
            this.PasswordSecureTextBox.ShortcutsEnabled = false;
            // 
            // HelpLabel
            // 
            resources.ApplyResources(this.HelpLabel, "HelpLabel");
            this.HelpLabel.Name = "HelpLabel";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.OK_Button;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.Controls.Add(this.HelpLabel);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.DatasourceTextBox);
            this.Controls.Add(this.DatasourceLabel);
            this.Controls.Add(this.PasswordSecureTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox pictureBox;
        internal System.Windows.Forms.TextBox UsernameTextBox;
        internal System.Windows.Forms.Label UsernameLabel;
        internal System.Windows.Forms.Label PasswordLabel;
        internal Components.SecureTextBox PasswordSecureTextBox;
        internal System.Windows.Forms.Label DatasourceLabel;
        internal Components.CustomTextBox DatasourceTextBox;
        internal System.Windows.Forms.Button Cancel_Button;
        internal System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.HelpProvider HelpProvider;
        private System.Windows.Forms.Label HelpLabel;
    }
}