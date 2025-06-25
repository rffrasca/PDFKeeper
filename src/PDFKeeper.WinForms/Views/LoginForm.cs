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
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            presenter.LongRunningOperationStarted += Presenter_LongRunningOperationStarted;
            presenter.LongRunningOperationFinished += Presenter_LongRunningOperationFinished;
            presenter.ApplyPendingChangesRequested += Presenter_ApplyPendingChangesRequested;
            presenter.ViewCloseRequested += Presenter_ViewCloseRequested;
            presenter.ViewResetRequested += Presenter_ViewResetRequested;
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

        private void Presenter_LongRunningOperationStarted(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
        }

        private void Presenter_LongRunningOperationFinished(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void Presenter_ApplyPendingChangesRequested(object sender, EventArgs e)
        {
            viewModel.UserName = Settings.Default.Username;
            viewModel.Password = PasswordSecureTextBox.SecureText;
            viewModel.DataSource = Settings.Default.Datasource;
            viewModel.DbManagementSystem = Settings.Default.DbManagementSystem;            
        }

        private void Presenter_ViewCloseRequested(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Presenter_ViewResetRequested(object sender, EventArgs e)
        {
            PasswordSecureTextBox.SecureText.Dispose();
            PasswordSecureTextBox.Text = null;
            PasswordSecureTextBox.InitSecureString();
            UsernameTextBox.Select();
        }
    }
}
