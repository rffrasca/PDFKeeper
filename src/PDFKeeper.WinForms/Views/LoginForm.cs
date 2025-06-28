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
using PDFKeeper.Core.Presenters;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Properties;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    public partial class LoginForm : Form
    {
        private readonly LoginPresenter presenter;
        private readonly LoginViewModel viewModel;

        public LoginForm()
        {
            InitializeComponent();
            presenter = new LoginPresenter(Handle);
            viewModel = presenter.ViewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            SetActionDelegates();
        }

        private void SetActionDelegates()
        {
            presenter.OnApplyPendingChangesRequested = (() =>
            {
                viewModel.UserName = Settings.Default.Username;
                viewModel.Password = PasswordSecureTextBox.SecureText;
                viewModel.DataSource = Settings.Default.Datasource;
                viewModel.DbManagementSystem = Settings.Default.DbManagementSystem;
            });

            presenter.OnLongRunningOperationStarted = () => Cursor = Cursors.WaitCursor;
            presenter.OnLongRunningOperationFinished = () => Cursor = Cursors.Default;

            presenter.OnViewCloseRequested = (() =>
            {
                presenter.CancelViewClosing = false;
                DialogResult = DialogResult.OK;
                Close();
            });

            presenter.OnViewResetRequested = (() =>
            {
                PasswordSecureTextBox.SecureText.Dispose();
                PasswordSecureTextBox.Text = null;
                PasswordSecureTextBox.InitSecureString();
                UsernameTextBox.Select();
            });
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            presenter.Login();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
