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

using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Interfaces.HelpSystem;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.WinForms.Properties;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Dialogs
{
    internal partial class OptionsForm : Form
    {
        private readonly IAliasService aliasService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsForm"/> class.
        /// </summary>
        /// <param name="aliasService">
        /// The <see cref="IAliasService"/> instance.
        /// </param>
        /// <param name="helpFileResolver">
        /// The <see cref="IHelpFileResolver"/> instance.
        /// </param>
        public OptionsForm(IAliasService aliasService, IHelpFileResolver helpFileResolver)
        {
            InitializeComponent();
            this.aliasService = aliasService;
            HelpProvider.HelpNamespace = helpFileResolver.GetHelpFilePath();

            if (!DatabaseSession.PlatformName.Equals(
                DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                ShowAllDocumentsOnStartupCheckBox.Visible = false;
                CompactDatabaseAfterDeletingSelectedDocumentsCheckBox.Visible = false;
            }

            GetAliases();
        }
        
        private void FindFlaggedDocumentsOnStartupCheckBox_CheckStateChanged(
            object sender,
            EventArgs e)
        {
            if (FindFlaggedDocumentsOnStartupCheckBox.Checked)
            {
                ShowAllDocumentsOnStartupCheckBox.Checked = false;
            }
        }

        private void ShowAllDocumentsOnStartupCheckBox_CheckStateChanged(
            object sender,
            EventArgs e)
        {
            if (ShowAllDocumentsOnStartupCheckBox.Checked)
            {
                FindFlaggedDocumentsOnStartupCheckBox.Checked = false;
            }
        }

        private void ResetAliasesLinkLabel_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            AuthorTextBox.Text = Resources.Author;
            SubjectTextBox.Text = Resources.Subject;
            CategoryTextBox.Text = Resources.Category;
            TaxYearTextBox.Text = Resources.TaxYear;
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            TrimAliases();
            SetAliases();
            Close();
        }

        private void GetAliases()
        {
            AuthorTextBox.Text = aliasService.GetAlias("Author");
            SubjectTextBox.Text = aliasService.GetAlias("Subject");
            CategoryTextBox.Text = aliasService.GetAlias("Category");
            TaxYearTextBox.Text = aliasService.GetAlias("Tax Year");
        }

        private void SetAliases()
        {
            if (AuthorTextBox.Text.Length.Equals(0))
            {
                AuthorTextBox.Text = Resources.Author;
            }
            if (SubjectTextBox.Text.Length.Equals(0))
            {
                SubjectTextBox.Text = Resources.Subject;
            }
            if (CategoryTextBox.Text.Length.Equals(0))
            {
                CategoryTextBox.Text = Resources.Category;
            }
            if (TaxYearTextBox.Text.Length.Equals(0))
            {
                TaxYearTextBox.Text = Resources.TaxYear;
            }

            aliasService.SetAlias("Author", AuthorTextBox.Text);
            aliasService.SetAlias("Subject", SubjectTextBox.Text);
            aliasService.SetAlias("Category", CategoryTextBox.Text);
            aliasService.SetAlias("Tax Year", TaxYearTextBox.Text);
        }

        private void TrimAliases()
        {
            AuthorTextBox.Text = AuthorTextBox.Text.Trim();
            SubjectTextBox.Text = SubjectTextBox.Text.Trim();
            CategoryTextBox.Text = CategoryTextBox.Text.Trim();
            TaxYearTextBox.Text = TaxYearTextBox.Text.Trim();
        }
    }
}
