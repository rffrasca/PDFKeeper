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

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Interfaces.HelpSystem;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Composition;
using PDFKeeper.WinForms.Properties;
using PDFKeeper.WinForms.Views;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PDFKeeper.WinForms
{
    static class Program
    {
        private static IExceptionHandler exceptionHandler;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(
                HandleUnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(HandleThreadException);

            using (var mutex = new Mutex(true, Application.ProductName))
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    var serviceProvider = CompositionRoot.BuildServiceProvider();
                    exceptionHandler = serviceProvider.GetRequiredService<IExceptionHandler>();

                    if (!Startup(serviceProvider))
                    {
                        using (var form = serviceProvider.GetRequiredService<MainForm>())
                        {
                            Application.Run(form);
                        }
                    }

                    Shutdown(serviceProvider);
                }
            }
        }
        
        /// <summary>
        /// Performs application startup actions.
        /// </summary>
        /// <param name="serviceProvider">
        /// The <see cref="IServiceProvider"/> containing services required by the application.
        /// /param>
        /// <returns>
        /// <c>true</c> or <c>false</c> if user cancelled or startup encountered an exception.
        /// </returns>
        static bool Startup(IServiceProvider serviceProvider)
        {
            var applicationRegistryProvider = serviceProvider.GetRequiredService<
                IApplicationRegistryProvider>();
            var helpViewer = serviceProvider.GetRequiredService<IHelpViewer>();
            var messageBoxService = serviceProvider.GetRequiredService<
                IMessageBoxService>();
            UpgradeUserSettings();

            if (Settings.Default.DbManagementSystem.Length.Equals(0))
            {
                var localDatabasePath = OneDriveHelper.ReadLocalDatabasePathIfapplicable();
                if (!string.IsNullOrEmpty(localDatabasePath))
                {
                    DatabaseSession.SetLocalDatabasePath(localDatabasePath);
                }

                if (File.Exists(DatabaseSession.GetLocalDatabasePath()))
                {
                    Settings.Default.DbManagementSystem = 
                        DatabaseSession.CompatiblePlatformName.Sqlite.ToString();
                }
                else
                {
                    applicationRegistryProvider.DeleteLocalDatabaseKeys();
                    var choice = messageBoxService.ShowQuestion(Resources.DatabaseSetup, true);

                    switch (choice)
                    {
                        case 6:
                            DatabaseSession.PlatformName =
                                DatabaseSession.CompatiblePlatformName.Sqlite;
                            
                            try
                            {
                                DatabaseSession.SetLocalDatabasePath(
                                    DatabaseSession.GetLocalDatabasePath());
                            }
                            catch (DatabaseException ex)
                            {
                                messageBoxService.ShowMessage(ex.Message, true);
                                return true;
                            }
                            
                            Settings.Default.DbManagementSystem =
                                DatabaseSession.CompatiblePlatformName.Sqlite.ToString();
                            var message = ResourceHelper.GetString(
                                Resources.ResourceManager,
                                "DatabaseCreated",
                                DatabaseSession.GetLocalDatabasePath());
                            messageBoxService.ShowMessage(message);
                            helpViewer.ShowHelp(HelpTopic.SetupSingleUserDatabase);
                            break;
                        case 7:
                            messageBoxService.ShowMessage(Resources.MultiUserDatabaseSetup);
                            helpViewer.ShowHelp(HelpTopic.SetupMultiUserDatabase);
                            var choice2 = messageBoxService.ShowQuestion(
                                Resources.ConnectingToOracle);
                            
                            if (choice2.Equals(6))
                            {
                                Settings.Default.DbManagementSystem =
                                    DatabaseSession.CompatiblePlatformName.Oracle.ToString();
                            }
                            else
                            {
                                var choice3 = messageBoxService.ShowQuestion(
                                    Resources.ConnectingToSqlServer);
                            
                                if (choice3.Equals(6))
                                {
                                    Settings.Default.DbManagementSystem =
                                        DatabaseSession.CompatiblePlatformName.SqlServer.ToString();
                                }
                                else
                                {
                                    var choice4 = messageBoxService.ShowQuestion(
                                        Resources.ConnectingToMySql);
                                
                                    if (choice4.Equals(6))
                                    {
                                        Settings.Default.DbManagementSystem =
                                            DatabaseSession.CompatiblePlatformName.MySql.ToString();
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }

                            break;
                        case 2:
                            return true;
                    }
                }
            }

            if (!Settings.Default.DbManagementSystem.Equals(
                DatabaseSession.CompatiblePlatformName.Sqlite.ToString(),
                StringComparison.Ordinal))
            {
                using (var form = serviceProvider.GetRequiredService<LoginForm>())
                {
                    if (form.ShowDialog().Equals(DialogResult.Cancel))
                    {
                        return true;
                    }
                }
            }
            else
            {
                DatabaseSession.PlatformName = DatabaseSession.CompatiblePlatformName.Sqlite;
                
                try
                {
                    using (var repository = DatabaseSession.GetDocumentRepository())
                    {
                        repository.UpgradeDatabase();
                    }
                }
                catch (DatabaseException ex)
                {
                    messageBoxService.ShowMessage(ex.Message, true);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Upgrades user settings.
        /// </summary>
        static void UpgradeUserSettings()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.PerUserRoamingAndLocal);

            if (!configuration.HasFile)
            {
                if (Settings.Default.UpgradeSettings)
                {
                    Settings.Default.Upgrade();
                    Settings.Default.UpgradeSettings = false;
                    Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Performs application shutdown actions.
        /// </summary>
        /// <param name="serviceProvider">
        /// The <see cref="IServiceProvider"/> containing services required by the application.
        /// </param>
        static void Shutdown(IServiceProvider serviceProvider)
        {
            var applicationFolderCleaner = serviceProvider.GetRequiredService<
                IApplicationFolderCleaner>();
            applicationFolderCleaner.CleanFolder(ApplicationFolder.Cache);
            applicationFolderCleaner.CleanFolder(ApplicationFolder.Temp);
            Settings.Default.Save();
        }

        /// <summary>
        /// Handles unhandled exceptions raised on non‑UI threads.
        /// </summary>
        /// <param name="sender">
        /// The source of the unhandled exception event.
        /// </param>
        /// <param name="e">
        /// The <see cref="UnhandledExceptionEventArgs"/> containing the exception object.
        /// </param>
        static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            exceptionHandler.HandleException(
                (Exception)e.ExceptionObject,
                ExceptionType.UnhandledException);
            Application.Exit();
        }

        /// <summary>
        /// Handles exceptions raised on the UI thread.
        /// </summary>
        /// <param name="sender">
        /// The source of the thread exception event.
        /// </param>
        /// <param name="e">
        /// The <see cref="ThreadExceptionEventArgs"/> containing the exception.
        /// </param>
        static void HandleThreadException(object sender, ThreadExceptionEventArgs e)
        {
            exceptionHandler.HandleException(e.Exception, ExceptionType.ThreadException);
            Application.Exit();
        }
    }
}
