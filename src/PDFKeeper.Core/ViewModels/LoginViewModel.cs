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
using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.DataAccess.Repository;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.IO;
using System.Security;
using System.Windows.Input;

namespace PDFKeeper.Core.ViewModels
{
    /// <summary>
    /// View model for managing the login process and related parameters.
    /// </summary>
    [CLSCompliant(false)]
    public sealed class LoginViewModel : ViewModelBase
    {
        private readonly IWindowHandleProvider windowHandleProvider;
        private IMessageBoxService messageBoxService;

        /// <summary>
        /// Initializes a new instance of the LoginViewModel class with the specified window handle
        /// provider.
        /// </summary>
        /// <param name="windowHandleProvider">
        /// An object that provides a handle to the window associated with this view model.
        /// </param>
        public LoginViewModel(IWindowHandleProvider windowHandleProvider)
        {
            this.windowHandleProvider = windowHandleProvider;
            GetServices(ServiceLocator.Services);
            LoginCommand = new RelayCommand(Login);
        }

        public ICommand LoginCommand { get; }
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public string DataSource { get; set; }
        public string SchemaName { get; set; }
        public string DbManagementSystem { get; set; }
        
        protected override void GetServices(IServiceProvider serviceProvider)
        {
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }

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
                messageBoxService.ShowMessage(windowHandleProvider.GetHandle(), ex.Message, true);
                OnResetView?.Invoke();
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(windowHandleProvider.GetHandle(), ex.Message, true);

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
                    windowHandleProvider.GetHandle(),
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
