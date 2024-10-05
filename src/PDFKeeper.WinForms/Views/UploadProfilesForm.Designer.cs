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
    partial class UploadProfilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadProfilesForm));
            this.UploadProfileNamesListBox = new System.Windows.Forms.ListBox();
            this.UploadProfilesViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.UploadProfilesFileSystemWatcher = new System.IO.FileSystemWatcher();
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.UploadProfilesViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadProfilesFileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // UploadProfileNamesListBox
            // 
            this.UploadProfileNamesListBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.UploadProfilesViewModelBindingSource, "CurrentUploadProfileName", true));
            this.UploadProfileNamesListBox.DataBindings.Add(new System.Windows.Forms.Binding("DataSource", this.UploadProfilesViewModelBindingSource, "UploadProfileNames", true));
            this.UploadProfileNamesListBox.FormattingEnabled = true;
            resources.ApplyResources(this.UploadProfileNamesListBox, "UploadProfileNamesListBox");
            this.UploadProfileNamesListBox.Name = "UploadProfileNamesListBox";
            this.UploadProfileNamesListBox.Sorted = true;
            this.UploadProfileNamesListBox.SelectedIndexChanged += new System.EventHandler(this.UploadProfileNamesListBox_SelectedIndexChanged);
            // 
            // UploadProfilesViewModelBindingSource
            // 
            this.UploadProfilesViewModelBindingSource.DataSource = typeof(PDFKeeper.Core.ViewModels.UploadProfilesViewModel);
            // 
            // DeleteButton
            // 
            resources.ApplyResources(this.DeleteButton, "DeleteButton");
            this.DeleteButton.Image = global::PDFKeeper.WinForms.Properties.Resources.database_delete;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditButton
            // 
            resources.ApplyResources(this.EditButton, "EditButton");
            this.EditButton.Image = global::PDFKeeper.WinForms.Properties.Resources.database_edit;
            this.EditButton.Name = "EditButton";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            resources.ApplyResources(this.AddButton, "AddButton");
            this.AddButton.Image = global::PDFKeeper.WinForms.Properties.Resources.database_add;
            this.AddButton.Name = "AddButton";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // UploadProfilesFileSystemWatcher
            // 
            this.UploadProfilesFileSystemWatcher.EnableRaisingEvents = true;
            this.UploadProfilesFileSystemWatcher.Filter = "*.xml";
            this.UploadProfilesFileSystemWatcher.SynchronizingObject = this;
            this.UploadProfilesFileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.UploadProfilesFileSystemWatcher_Created);
            this.UploadProfilesFileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.UploadProfilesFileSystemWatcher_Deleted);
            this.UploadProfilesFileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.UploadProfilesFileSystemWatcher_Renamed);
            // 
            // UploadProfilesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UploadProfileNamesListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadProfilesForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.UploadProfilesViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadProfilesFileSystemWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.IO.FileSystemWatcher UploadProfilesFileSystemWatcher;
        internal System.Windows.Forms.ListBox UploadProfileNamesListBox;
        internal System.Windows.Forms.Button DeleteButton;
        internal System.Windows.Forms.Button EditButton;
        internal System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.BindingSource UploadProfilesViewModelBindingSource;
        private System.Windows.Forms.HelpProvider HelpProvider;
    }
}