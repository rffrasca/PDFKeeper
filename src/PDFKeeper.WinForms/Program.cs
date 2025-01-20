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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.WinForms.Helpers;
using PDFKeeper.WinForms.Properties;
using PDFKeeper.WinForms.Services;
using PDFKeeper.WinForms.Views;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

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
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(
                ExceptionEventHandler.HandleThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(
                ExceptionEventHandler.HandleUnhandledException);
            using (var mutex = new Mutex(true, Application.ProductName))
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    if (!Startup())
                    {
                        using (var form = new MainForm())
                        {
                            Application.Run(form);
                        }
                    }
                    Shutdown();
                    mutex.ReleaseMutex();
                    mutex.Dispose();
                }
            }
        }

        /// <summary>
        /// Application startup actions.
        /// </summary>
        /// <returns>
        /// User cancelled or startup encountered an exception? (true or false)
        /// </returns>
        static bool Startup()
        {
            var helpFile = new HelpFile();
            var userSettingsHelper = new UserSettingsHelper();
            var messageBoxService = new MessageBoxService();
            userSettingsHelper.Upgrade();
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
                            messageBoxService.ShowMessage(
                                ResourceHelper.GetString(
                                    "DatabaseCreated",
                                    DatabaseSession.LocalDatabasePath,
                                    null),
                                false);
                            helpFile.Show("Setup Single-User Database.html");
                            break;
                        case 7:
                            messageBoxService.ShowMessage(Resources.MultiUserDatabaseSetup, false);
                            helpFile.Show("Setup Multi-User Database.html");
                            var choice2 = messageBoxService.ShowQuestion(
                                Resources.ConnectingToOracle,
                                false);
                            if (choice2.Equals(6))
                            {
                                Settings.Default.DbManagementSystem =
                                    DatabaseSession.CompatiblePlatformName.Oracle.ToString();
                            }
                            else
                            {
                                var choice3 = messageBoxService.ShowQuestion(
                                Resources.ConnectingToSqlServer,
                                false);
                                if (choice3.Equals(6))
                                {
                                    Settings.Default.DbManagementSystem =
                                        DatabaseSession.CompatiblePlatformName.SqlServer.ToString();
                                }
                                else
                                {
                                    var choice4 = messageBoxService.ShowQuestion(
                                        Resources.ConnectingToMySql,
                                        false);
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
        /// Application shutdown actions.
        /// </summary>
        static void Shutdown()
        {
            var applicationDirectory = new ApplicationDirectory();
            applicationDirectory.DeleteUploadDirectoryShortcuts();
            applicationDirectory.GetDirectory(ApplicationDirectory.SpecialName.Cache).Empty();
            applicationDirectory.GetDirectory(ApplicationDirectory.SpecialName.Temp).Empty();
            Settings.Default.Save();
        }
    }
}
