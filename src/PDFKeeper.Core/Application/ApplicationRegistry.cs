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

using Microsoft.Win32;
using System;

namespace PDFKeeper.Core.Application
{
    public static class ApplicationRegistry
    {
        /// <summary>
        /// Gets the current user registry key path for the application.
        /// </summary>
        public static string UserKeyPath => GetAbsoluteKeyPath(
            @"HKEY_CURRENT_USER\SOFTWARE");

        /// <summary>
        /// Gets the registry key path for application policies.
        /// </summary>
        public static string PoliciesKeyPath => GetAbsoluteKeyPath(
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies");

        /// <summary>
        /// Deletes the local database path and file name values from the current user's registry
        /// key, if they exist.
        /// </summary>
        /// <remarks>
        /// This method removes the "LocalDatabasePath" and "LocalDatabaseFileName" values from the
        /// registry key specified by UserKeyPath under HKEY_CURRENT_USER. If either value does not
        /// exist, no exception is thrown. This operation affects only the current user's registry
        /// hive and does not impact other users or system-wide settings.
        /// </remarks>
        public static void DeleteLocalDatabaseKeys()
        {
            var hivePrefix = @"HKEY_CURRENT_USER\";
            var strippedUserKeyPath = UserKeyPath.StartsWith(
                hivePrefix,
                StringComparison.OrdinalIgnoreCase)
                ? UserKeyPath.Substring(hivePrefix.Length)
                : UserKeyPath;
            using var key = Registry.CurrentUser.OpenSubKey(strippedUserKeyPath, writable: true);
            if (key != null)
            {
                key.DeleteValue("LocalDatabasePath", throwOnMissingValue: false);
                key.DeleteValue("LocalDatabaseFileName", throwOnMissingValue: false);
            }
        }

        /// <summary>
        /// Gets the absolute registry key path for the application. 
        /// </summary>
        /// <param name="startingKeyPath">The starting registry key path.</param>
        /// <returns>The absolute key path.</returns>
        private static string GetAbsoluteKeyPath(string startingKeyPath)
        {
            var executingAssembly = new ExecutingAssembly();
            return string.Concat(
                startingKeyPath,
                @"\",
                executingAssembly.CompanyName,
                @"\",
                executingAssembly.ProductName);
        }
    }
}
