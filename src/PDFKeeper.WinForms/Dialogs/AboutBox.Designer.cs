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

namespace PDFKeeper.WinForms.Dialogs
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DonateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.WebsiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.ThirdPartyNoticesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LicenseLinkLabel = new System.Windows.Forms.LinkLabel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.DonateLinkLabel, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.WebsiteLinkLabel, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.ThirdPartyNoticesLinkLabel, 0, 8);
            this.tableLayoutPanel.Controls.Add(this.LicenseLinkLabel, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 7);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // DonateLinkLabel
            // 
            resources.ApplyResources(this.DonateLinkLabel, "DonateLinkLabel");
            this.DonateLinkLabel.Name = "DonateLinkLabel";
            this.DonateLinkLabel.TabStop = true;
            this.DonateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DonateLinkLabel_LinkClicked);
            // 
            // WebsiteLinkLabel
            // 
            resources.ApplyResources(this.WebsiteLinkLabel, "WebsiteLinkLabel");
            this.WebsiteLinkLabel.Name = "WebsiteLinkLabel";
            this.WebsiteLinkLabel.TabStop = true;
            this.WebsiteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebsiteLinkLabel_LinkClicked);
            // 
            // ThirdPartyNoticesLinkLabel
            // 
            resources.ApplyResources(this.ThirdPartyNoticesLinkLabel, "ThirdPartyNoticesLinkLabel");
            this.ThirdPartyNoticesLinkLabel.Name = "ThirdPartyNoticesLinkLabel";
            this.ThirdPartyNoticesLinkLabel.TabStop = true;
            this.ThirdPartyNoticesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ThirdPartyNoticesLinkLabel_LinkClicked);
            // 
            // LicenseLinkLabel
            // 
            resources.ApplyResources(this.LicenseLinkLabel, "LicenseLinkLabel");
            this.LicenseLinkLabel.Name = "LicenseLinkLabel";
            this.LicenseLinkLabel.TabStop = true;
            this.LicenseLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LicenseLinkLabel_LinkClicked);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.logoPictureBox, "logoPictureBox");
            this.logoPictureBox.Image = global::PDFKeeper.WinForms.Properties.Resources.PDFKeeper_200x200;
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 7);
            this.logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.Name = "labelProductName";
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.Name = "labelVersion";
            // 
            // labelCopyright
            // 
            resources.ApplyResources(this.labelCopyright, "labelCopyright");
            this.labelCopyright.Name = "labelCopyright";
            // 
            // labelCompanyName
            // 
            resources.ApplyResources(this.labelCompanyName, "labelCompanyName");
            this.labelCompanyName.Name = "labelCompanyName";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.TabStop = false;
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Name = "okButton";
            this.tableLayoutPanel.SetRowSpan(this.okButton, 2);
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox textBoxDescription;
        internal System.Windows.Forms.LinkLabel LicenseLinkLabel;
        internal System.Windows.Forms.LinkLabel ThirdPartyNoticesLinkLabel;
        internal System.Windows.Forms.LinkLabel WebsiteLinkLabel;
        internal System.Windows.Forms.LinkLabel DonateLinkLabel;
        private System.Windows.Forms.Button okButton;
    }
}
