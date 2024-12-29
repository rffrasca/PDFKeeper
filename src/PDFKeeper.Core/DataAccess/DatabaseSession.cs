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

using Microsoft.Win32;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess.Repository;
using System;
using System.IO;
using System.Reflection;
using System.Security;

namespace PDFKeeper.Core.DataAccess
{
    public static class DatabaseSession
    {
        private static CompatiblePlatformName platformName;
        private static string userName;
        private static SecureString password;
        private static string dataSource;
        private static string oracleWalletPath;
        private static string localDatabasePath;
        private static uint mySqlPort;
        private static bool oracleOdpNetHandlerEnabled;

        /// <summary>
        /// Compatible database platform name.
        /// </summary>
        public enum CompatiblePlatformName
        {
            Sqlite,     // 0
            Oracle,     // 1
            SqlServer,  // 2
            MySql       // 3
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
                    string.Concat(
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
        /// Gets the MySQL port number to use for connecting to the database server.
        /// </summary>
        [CLSCompliant(false)]
        public static uint MySqlPort
        {
            get
            {
                mySqlPort = Convert.ToUInt32(
                    Registry.GetValue(
                        ApplicationRegistry.UserKeyPath,
                        "MySqlPort",
                        3306));
                return mySqlPort;
            }
        }

        /// <summary>
        /// Gets or sets if the documents list has changes? (true or false)
        /// </summary>
        public static bool DocumentsListHasChanges { get; set; }

        /// <summary>
        /// Gets a document repository instance for the database platform in use.
        /// </summary>
        /// <returns>
        /// The document repository instance.
        /// </returns>
        public static IDocumentRepository GetDocumentRepository()
        {
            return DocumentRepositoryFactory.Create();
        }

        private static void OnCompatiblePlatformNameChanged()
        {
            if (PlatformName.Equals(CompatiblePlatformName.Oracle) && !oracleOdpNetHandlerEnabled)
            {
                AppDomain.CurrentDomain.AssemblyResolve += LoadOracleOdpNet;
                oracleOdpNetHandlerEnabled = true;
            }
        }

        private static Assembly LoadOracleOdpNet(object sender, ResolveEventArgs args)
        {
            var assemblyPath = Path.Combine(
                (string)Registry.GetValue(
                    ApplicationRegistry.UserKeyPath,
                    "OracleOdpNetPath",
                    string.Empty),
                "Oracle.ManagedDataAccess.dll");
            return Assembly.LoadFile(assemblyPath);
        }
    }
}
