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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Properties;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    internal partial class LoginForm : Form
    {
        private readonly LoginViewModel viewModel;

        public LoginForm()
        {
            InitializeComponent();
            viewModel = new LoginViewModel(Handle);
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            SetActions();
        }

        private void SetActions()
        {
            viewModel.OnLongOperationStarted = () => Cursor = Cursors.WaitCursor;
            viewModel.OnLongOperationFinished = () => Cursor = Cursors.Default;

            viewModel.OnApplyPendingChanges = () =>
            {
                viewModel.UserName = Settings.Default.Username;
                viewModel.Password = PasswordSecureTextBox.SecureText;
                viewModel.DataSource = Settings.Default.Datasource;
                viewModel.SchemaName = Settings.Default.Schemaname;
                viewModel.DbManagementSystem = Settings.Default.DbManagementSystem;
            };
            
            viewModel.OnCloseView = () =>
            {
                viewModel.CancelViewClosing = false;
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnResetView = () =>
            {
                PasswordSecureTextBox.SecureText.Dispose();
                PasswordSecureTextBox.Text = null;
                PasswordSecureTextBox.InitSecureString();
                UsernameTextBox.Select();
            };
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            viewModel.LoginCommand.Execute(null);
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
