// ****************************************************************************
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
// ****************************************************************************

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.DataAccess.Repository;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.ViewModels;
using System;
using System.Collections;
using System.IO;

namespace PDFKeeper.Core.Presenters
{
    public class LoginPresenter : PresenterBase<LoginViewModel>
    {
        private IMessageBoxService messageBoxService;
        private readonly IntPtr handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPresenter"/> class.
        /// </summary>
        /// <param name="handle">The handle of the view.</param>
        public LoginPresenter(IntPtr handle)
        {
            GetServices(ServicesLocator.Services);
            this.handle = handle;
            ViewModel = new LoginViewModel();
        }

        public void Login()
        {
            OnApplyPendingChangesRequested();
            OnLongRunningOperationStarted();
            SetDatabasePlatformName();
            DatabaseSession.UserName = ViewModel.UserName;
            DatabaseSession.Password = ViewModel.Password;
            DatabaseSession.DataSource = ViewModel.DataSource;
            
            IDocumentRepository documentRepository = null;
            try
            {
                using (documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    documentRepository.TestConnection();
                    OnViewCloseRequested();
                }
            }
            catch (ArgumentException ex)
            {
                messageBoxService.ShowMessage(handle, ex.Message, true);
                OnViewResetRequested();
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(handle, ex.Message, true);
                
                try
                {
                    documentRepository.ResetCredential();
                }
                catch (NotSupportedException) { }
                
                OnViewResetRequested();
            }
            catch (FileNotFoundException)
            {
                messageBoxService.ShowMessage(handle, Resources.OracleOdpNetMissing, true);
                OnViewResetRequested();
            }
            finally
            {
                OnLongRunningOperationFinished();
            }
        }

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }

        private void SetDatabasePlatformName()
        {
            IList list = Enum.GetValues(typeof(DatabaseSession.CompatiblePlatformName));
            for (int i = 0, loopTo = list.Count - 1; i <= loopTo; i++)
            {
                var platform = list[i];
                if (platform.ToString().Equals(ViewModel.DbManagementSystem,
                    StringComparison.Ordinal))
                {
                    DatabaseSession.PlatformName = (
                        DatabaseSession.CompatiblePlatformName)platform;
                }
            }
        }
    }
}
