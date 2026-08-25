// ****************************************************************************
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
// ****************************************************************************

using CommunityToolkit.Mvvm.Input;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.DataAccess.Repository;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.IO;
using System.Security;

namespace PDFKeeper.Core.ViewModels
{
    /// <summary>
    /// View model for managing the login process and related parameters.
    /// </summary>
    [CLSCompliant(false)]
    public sealed class LoginViewModel : ViewModelBase
    {
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="messageBoxService">
        /// The <see cref="IMessageBoxService"/> instance.
        /// </param>
        public LoginViewModel(IMessageBoxService messageBoxService)
        {
            this.messageBoxService = messageBoxService;
            LoginCommand = new RelayCommand(Login);
        }

        public IRelayCommand LoginCommand { get; }
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public string DataSource { get; set; }
        public string SchemaName { get; set; }
        public string DbManagementSystem { get; set; }

        private void Login()
        {
            OnApplyPendingChanges?.Invoke();
            OnLongOperationStarted?.Invoke();
            DatabaseSession.SetPlatformName(DbManagementSystem);
            DatabaseSession.UserName = UserName;
            DatabaseSession.Password = Password;
            DatabaseSession.DataSource = DataSource;
            DatabaseSession.SchemaName = SchemaName;
            IDocumentRepository documentRepository = null;

            try
            {
                using (documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    documentRepository.TestConnection();
                    OnCloseView?.Invoke();
                }
            }
            catch (ArgumentException ex)
            {
                messageBoxService.ShowMessage(GetWindowHandle.Invoke(), ex.Message, true);
                OnResetView?.Invoke();
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(GetWindowHandle.Invoke(), ex.Message, true);

                try
                {
                    documentRepository.ResetCredential();
                }
                catch (NotSupportedException) { }

                OnResetView?.Invoke();
            }
            catch (FileNotFoundException)
            {
                messageBoxService.ShowMessage(
                    GetWindowHandle.Invoke(),
                    Resources.OracleOdpNetMissing,
                    true);
                OnResetView?.Invoke();
            }
            finally
            {
                OnLongOperationFinished?.Invoke();
            }
        }
    }
}
