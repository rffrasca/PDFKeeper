// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

using Microsoft.Win32;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.IO;
using System.Security;

namespace PDFKeeper.Core.DataAccess
{
    public static class DatabaseSession
    {
        private static IMessageBoxService messageBoxService;
        private static CompatiblePlatformName platformName;
        private static string userName;
        private static SecureString password;
        private static string dataSource;
        private static string oracleWalletPath;
        private static string localDatabasePath;
        private static bool odpHandlerEnabled;
        
        /// <summary>
        /// Compatible database platform name.
        /// </summary>
        public enum CompatiblePlatformName
        {
            Sqlite, // 0
            Oracle  // 1            
        }

        /// <summary>
        /// Gets or sets the database platform name.
        /// </summary>
        public static CompatiblePlatformName PlatformName
        {
            get => platformName;
            set
            {
                platformName = value;
                OnCompatiblePlatformNameChanged();
            }
        }

        /// <summary>
        /// Gets or sets the database user name. On a get, the operating system user name will be
        /// returned when the database platform is SQLite.
        /// </summary>
        public static string UserName
        {
            get
            {
                if (PlatformName.Equals(CompatiblePlatformName.Sqlite))
                {
                    return Environment.UserName;
                }
                else
                {
                    return userName;
                }
            }
            set => userName = value;
        }

        /// <summary>
        /// Gets or sets the database password SecureString. On a get, null will be returned when
        /// the database platform is SQLite.
        /// </summary>
        public static SecureString Password
        {
            get
            {
                if (PlatformName.Equals(CompatiblePlatformName.Sqlite))
                {
                    return null;
                }
                else
                {
                    return password;
                }
            }
            set
            {
                if (value != null)
                {
                    password = new SecureString();
                    password = value;
                    password.MakeReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets or sets the data source. On a get, null will be returned when the database
        /// platform is SQLite.
        /// </summary>
        public static string DataSource
        {
            get
            {
                if (PlatformName.Equals(CompatiblePlatformName.Sqlite))
                {
                    return null;
                }
                else
                {
                    return dataSource;
                }
            }
            set => dataSource = value;
        }

        /// <summary>
        /// Gets the Oracle Wallet path used for Mutual TLS (mTLS) authentication or null will be
        /// returned when an Oracle Wallet is not setup.
        /// </summary>
        public static string OracleWalletPath
        {
            get
            {
                oracleWalletPath = (string)Registry.GetValue(
                    ApplicationRegistry.UserKeyPath,
                    "OracleWalletPath",
                    null);
                return oracleWalletPath;
            }
        }

        /// <summary>
        /// Gets or sets the local SQLite database path.
        /// </summary>
        public static string LocalDatabasePath
        {
            get
            {
                localDatabasePath = Registry.GetValue(
                    ApplicationRegistry.UserKeyPath,
                    "LocalDatabasePath",
                    new ApplicationDirectory().GetDirectory(
                        ApplicationDirectory.SpecialName.ApplicationData)).ToString();
                var filePath = new FileInfo(
                    Path.Combine(localDatabasePath,
                    String.Concat(
                        new ExecutingAssembly().ProductName,
                        ".sqlite")));
                return filePath.FullName;
            }
            set => Registry.SetValue(
                ApplicationRegistry.UserKeyPath,
                "LocalDatabasePath",
                Path.GetDirectoryName(value));
        }

        /// <summary>
        /// Sets the MessageBoxService instance.
        /// </summary>
        /// <param name="messageBoxService">The MessageBoxService instance.</param>
        public static void SetMessageBoxService(IMessageBoxService messageBoxService)
        {
            DatabaseSession.messageBoxService = messageBoxService;
        }

        private static void OnCompatiblePlatformNameChanged()
        {
            if (PlatformName.Equals(CompatiblePlatformName.Oracle) &&
                odpHandlerEnabled.Equals(false))
            {
                AppDomain.CurrentDomain.AssemblyResolve += LoadOracleDataProvider;
                odpHandlerEnabled = true;
            }
        }

        private static System.Reflection.Assembly LoadOracleDataProvider(
            object sender,
            ResolveEventArgs args)
        {
            try
            {
                var dllPath = Registry.GetValue(
                    string.Concat(
                        @"HKEY_LOCAL_MACHINE\SOFTWARE\Oracle\ODP.NET\",
                        Resources.OracleDataProviderVersion),
                    "DllPath",
                    string.Empty).ToString();
                var oraKeyPath = string.Concat(
                    @"HKEY_LOCAL_MACHINE\",
                    File.ReadAllText(
                        Path.Combine(
                            dllPath,
                            "oracle.key"))).TrimEnd();
                var assemblyPath = string.Concat(
                    Registry.GetValue(
                        oraKeyPath,
                        "ORACLE_HOME",
                        string.Empty),
                    @"\odp.net\managed\common\Oracle.ManagedDataAccess.dll");
                return System.Reflection.Assembly.LoadFile(assemblyPath);
            }
            catch (FileNotFoundException)
            {
                messageBoxService.ShowMessage(Resources.OracleDataProviderMissing, true);
                return null;
            }
        }
    }
}
