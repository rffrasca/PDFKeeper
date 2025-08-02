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
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Services;
using PDFKeeper.PDFViewer.Services;
using PDFKeeper.WinForms.Properties;
using PDFKeeper.WinForms.Services;
using PDFKeeper.WinForms.Views;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static PDFKeeper.Core.Extensions.ExceptionExtension;

namespace PDFKeeper.WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(
                HandleUnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(
                HandleThreadException);

            using (var mutex = new Mutex(true, Application.ProductName))
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    ServiceLocator.Services = ConfigureServices();

                    if (!Startup())
                    {
                        using (var form = new MainForm())
                        {
                            Application.Run(form);
                        }
                    }

                    Shutdown();
                }
            }
        }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <returns>The <see cref="ServiceProvider"/> instance.</returns>
        static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IDialogService,
                AddPdfDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                SetTitleDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                SetAuthorDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                SetSubjectDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                SetCategoryDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                SetTaxYearDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                SetDateTimeAddedDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                SetPreviewPixelDensityDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                OptionsDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                UploadProfileEditorDialogService>();
            serviceCollection.AddSingleton<IDialogService,
                AboutBoxDialogService>();
            serviceCollection.AddSingleton<IFileDialogService,
                OpenFileDialogService>();
            serviceCollection.AddSingleton<IFileDialogService,
                SaveFileDialogService>();
            serviceCollection.AddSingleton<IFolderBrowserDialogService,
                FolderBrowserDialogService>();
            serviceCollection.AddSingleton<IFolderExplorerService,
                FolderExplorerService>();
            serviceCollection.AddSingleton<IHelpService, HelpService>();
            serviceCollection.AddSingleton<IMessageBoxService,
                MessageBoxService>();
            serviceCollection.AddSingleton<IPasswordDialogService,
                PdfOwnerPasswordDialogService>();
            serviceCollection.AddSingleton<IPdfViewerService,
                PdfViewerService>();
            serviceCollection.AddSingleton<IPrintDialogService,
                PrintDialogService>();
            serviceCollection.AddSingleton<IPrintPreviewDialogService,
                PrintPreviewDialogService>();
            serviceCollection.AddSingleton<IRestrictedPdfViewerService,
                RestrictedPdfViewerService>();
            return serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Performs application startup actions.
        /// </summary>
        /// <returns>
        /// <c>true</c> or <c>false</c> if user cancelled or startup encountered an exception.
        /// </returns>
        static bool Startup()
        {
            var helpFile = new HelpFile();
            var messageBoxService = ServiceLocator.Services.GetService<IMessageBoxService>();
            UpgradeUserSettings();

            if (Settings.Default.DbManagementSystem.Length.Equals(0))
            {
                if (File.Exists(DatabaseSession.LocalDatabasePath))
                {
                    Settings.Default.DbManagementSystem = 
                        DatabaseSession.CompatiblePlatformName.Sqlite.ToString();
                }
                else
                {
                    var choice = messageBoxService.ShowQuestion(Resources.DatabaseSetup, true);

                    switch (choice)
                    {
                        case 6:
                            DatabaseSession.PlatformName =
                                DatabaseSession.CompatiblePlatformName.Sqlite;
                            
                            try
                            {
                                using (var repository = DatabaseSession.GetDocumentRepository())
                                {
                                    repository.CreateDatabase();
                                }
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
                                DatabaseSession.LocalDatabasePath);
                            messageBoxService.ShowMessage(message);
                            helpFile.ShowHelp(HelpFile.Topic.SetupSingleUserDatabase);
                            break;
                        case 7:
                            messageBoxService.ShowMessage(Resources.MultiUserDatabaseSetup);
                            helpFile.ShowHelp(HelpFile.Topic.SetupMultiUserDatabase);
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
                using (var form = new LoginForm())
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
        /// Performs application shutdown actions.
        /// </summary>
        static void Shutdown()
        {
            var applicationDirectory = new ApplicationDirectory();
            applicationDirectory.DeleteUploadDirectoryShortcuts();
            applicationDirectory.GetDirectory(ApplicationDirectory.SpecialName.Cache).Empty();
            applicationDirectory.GetDirectory(ApplicationDirectory.SpecialName.Temp).Empty();
            Settings.Default.Save();
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

        static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ((Exception)e.ExceptionObject).HandleException(ExceptionType.UnhandledException);
            Application.Exit();
        }

        static void HandleThreadException(object sender, ThreadExceptionEventArgs e)
        {
            e.Exception.HandleException(ExceptionType.ThreadException);
            Application.Exit();
        }
    }
}
