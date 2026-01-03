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
using PDFKeeper.Core.Extensions;
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
        private static string schemaName;
        private static string oracleWalletPath;
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
        /// Retrieves the full file system path to the local database file as configured in the
        /// application registry.
        /// </summary>
        /// <remarks>
        /// If the required registry values are not set, default values are used for the directory
        /// and file name. The returned path may not correspond to an existing file if the database
        /// has not been created.
        /// </remarks>
        /// <returns>
        /// A string containing the absolute path to the local database file. The path is
        /// constructed from registry values specifying the directory and file name.
        /// </returns>
        public static string GetLocalDatabasePath()
        {
            var localDatabasePath = Registry.GetValue(
                ApplicationRegistry.UserKeyPath,
                "LocalDatabasePath",
                new ApplicationDirectory().GetDirectory(
                    ApplicationDirectory.SpecialName.ApplicationData)).ToString();
            var localDatabaseFileName = Registry.GetValue(
                ApplicationRegistry.UserKeyPath,
                "LocalDatabaseFileName",
                $"{new ExecutingAssembly().ProductName}.sqlite").ToString();
            return Path.Combine(localDatabasePath, localDatabaseFileName);
        }

        /// <summary>
        /// Gets or sets the callback that is invoked when the local database path changes.
        /// </summary>
        /// <remarks>
        /// Assign a method to this property to execute custom logic in response to changes in the
        /// local database path. Only one callback can be assigned at a time; assigning a new value
        /// replaces any existing callback.
        /// </remarks>
        internal static Action OnLocalDatabasePathChanged { get; set; }

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
        /// Gets or sets the name of the database schema to use for operations.
        /// </summary>
        /// <remarks>
        /// On platforms that do not support database schemas, such as SQLite, this property
        /// returns <see langword="null"/> regardless of its value.
        /// </remarks>
        internal static string SchemaName
        {
            get
            {
                if (PlatformName.Equals(CompatiblePlatformName.Sqlite))
                {
                    return null;
                }
                else
                {
                    return schemaName;
                }
            }
            set => schemaName = value;
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
        /// Sets the path to the local database file and updates the application registry with the
        /// specified location.
        /// </summary>
        /// <remarks>
        /// This method updates the application's registry settings to reflect the new database
        /// location. If validation is enabled, the method checks for the existence of the file and
        /// verifies that it is a valid database before updating the registry. After the path is
        /// set, any registered listeners are notified of the change.
        /// </remarks>
        /// <param name="path">
        /// The full file system path to the local database file. This value is used to update the
        /// registry and, if validation is enabled, must refer to an existing and valid database
        /// file.
        /// </param>
        /// <param name="validate">
        /// true to validate that the specified path exists and points to a valid database file;
        /// otherwise, false to set the path without validation. The default is false.
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// Thrown if validate is set to true and the file specified by path does not exist.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// Thrown if validate is set to true and the file specified by path does not represent a
        /// valid database file.
        /// </exception>
        internal static void SetLocalDatabasePath(string path, bool validate = false)
        {
            if (validate)
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException(path);
                }

                var dbBytes = File.ReadAllBytes(path);
                
                if (!dbBytes.ContainsUtf8String("CREATE TABLE docs"))
                {
                    throw new InvalidDataException(
                        "The specified file is not a valid database file.");
                }
            }

            Registry.SetValue(
                ApplicationRegistry.UserKeyPath,
                "LocalDatabasePath",
                Path.GetDirectoryName(path));
            Registry.SetValue(
                ApplicationRegistry.UserKeyPath,
                "LocalDatabaseFileName",
                Path.GetFileName(path));
            DocumentsListHasChanges = true;
            OnLocalDatabasePathChanged?.Invoke();
        }

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
