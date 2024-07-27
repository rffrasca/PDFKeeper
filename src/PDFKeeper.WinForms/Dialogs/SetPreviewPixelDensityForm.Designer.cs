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
    partial class SetPreviewPixelDensityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetPreviewPixelDensityForm));
            this.OK_Button = new System.Windows.Forms.Button();
            this.PixelsPerInchNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.PixelsPerInchNumericUpDown)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OK_Button
            // 
            resources.ApplyResources(this.OK_Button, "OK_Button");
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // PixelsPerInchNumericUpDown
            // 
            this.PixelsPerInchNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PDFKeeper.WinForms.Properties.Settings.Default, "PreviewPixelDensity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PixelsPerInchNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            resources.ApplyResources(this.PixelsPerInchNumericUpDown, "PixelsPerInchNumericUpDown");
            this.PixelsPerInchNumericUpDown.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.PixelsPerInchNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PixelsPerInchNumericUpDown.Name = "PixelsPerInchNumericUpDown";
            this.PixelsPerInchNumericUpDown.ReadOnly = true;
            this.PixelsPerInchNumericUpDown.Value = global::PDFKeeper.WinForms.Properties.Settings.Default.PreviewPixelDensity;
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.OK_Button, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // SetPreviewPixelDensityForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.PixelsPerInchNumericUpDown);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetPreviewPixelDensityForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.PixelsPerInchNumericUpDown)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.NumericUpDown PixelsPerInchNumericUpDown;
        internal System.Windows.Forms.Label label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        internal System.Windows.Forms.HelpProvider HelpProvider;
    }
}