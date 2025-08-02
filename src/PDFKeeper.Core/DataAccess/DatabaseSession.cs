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
using System.Collections;
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
            Sqlite,
            Oracle,
            SqlServer,
            MySql
        }

        /// <summary>
        /// Gets or sets a <see cref="CompatiblePlatformName"/> as the database platform name.
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
            internal set => Registry.SetValue(
                ApplicationRegistry.UserKeyPath,
                "LocalDatabasePath",
                Path.GetDirectoryName(value));
        }

        /// <summary>
        /// Gets a <see cref="IDocumentRepository"/> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="IDocumentRepository"/> instance.
        /// </returns>
        public static IDocumentRepository GetDocumentRepository()
        {
            return DocumentRepositoryFactory.Create();
        }

        /// <summary>
        /// Gets or sets the database user name.
        /// <para>
        /// On a get, <see cref="Environment.UserName"/> will be returned when
        /// <see cref="PlatformName"/> is <see cref="CompatiblePlatformName.Sqlite"/>.
        /// </para>
        /// </summary>
        internal static string UserName
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
        /// Gets or sets the database password <see cref="SecureString"/>.
        /// <para>
        /// On a get, <c>null</c> will be returned when <see cref="PlatformName"/> is
        /// <see cref="CompatiblePlatformName.Sqlite"/>.
        /// </para>
        /// </summary>
        internal static SecureString Password
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
        /// Gets or sets the data source.
        /// <para>
        /// On a get, <c>null</c> will be returned when <see cref="PlatformName"/> is
        /// <see cref="CompatiblePlatformName.Sqlite"/>.
        /// </para>
        /// </summary>
        internal static string DataSource
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
        /// Gets or sets if user has Select access granted to the DOCS table.
        /// </summary>
        internal static bool SelectGranted { get; set; }

        /// <summary>
        /// Gets or sets if user has Insert access granted to the DOCS table.
        /// </summary>
        internal static bool InsertGranted { get; set; }

        /// <summary>
        /// Gets or sets if user has Update access granted to the DOCS table.
        /// </summary>
        internal static bool UpdateGranted { get; set; }

        /// <summary>
        /// Gets or sets if user has Delete access granted to the DOCS table.
        /// </summary>
        internal static bool DeleteGranted { get; set; }

        /// <summary>
        /// Gets the Oracle Wallet path used for Mutual TLS (mTLS) authentication or <c>null</c>
        /// will be returned when an Oracle Wallet is not setup.
        /// </summary>
        internal static string OracleWalletPath
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
        /// Gets the MySQL port number to use for connecting to the database server.
        /// </summary>
        internal static uint MySqlPort
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
        /// <c>true</c> or <c>false</c> if the documents list has changes.
        /// </summary>
        internal static bool DocumentsListHasChanges { get; set; }

        /// <summary>
        /// Sets <see cref="PlatformName"/> to the <see cref="CompatiblePlatformName"/> that
        /// matches the specified compatible database management system.
        /// </summary>
        /// <param name="dbManagementSystem">The compatible database management system.</param>
        internal static void SetPlatformName(string dbManagementSystem)
        {
            IList list = Enum.GetValues(typeof(CompatiblePlatformName));

            for (int i = 0, loopTo = list.Count - 1; i <= loopTo; i++)
            {
                var platform = list[i];

                if (platform.ToString().Equals(dbManagementSystem, StringComparison.Ordinal))
                {
                    PlatformName = (CompatiblePlatformName)platform;
                }
            }
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
