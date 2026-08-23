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
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Models;
using System;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IApplicationRegistryProvider"/> interface.
    /// </summary>
    public sealed class ApplicationRegistryProvider : IApplicationRegistryProvider
    {
        private readonly ApplicationInfoDto applicationInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRegistryProvider"/> class.
        /// </summary>
        /// <param name="applicationInfoService">
        /// The <see cref="IApplicationInfoService"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="applicationInfoService"/> is null.
        /// </exception>
        public ApplicationRegistryProvider(IApplicationInfoService applicationInfoService)
        {
            applicationInfo = applicationInfoService?.GetApplicationInfo() ??
                throw new ArgumentNullException(nameof(applicationInfoService));
        }

        public string UserKeyPath => BuildKeyPath(@"HKEY_CURRENT_USER\SOFTWARE");

        public string PoliciesKeyPath => BuildKeyPath(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies");

        public void DeleteLocalDatabaseKeys()
        {
            const string hivePrefix = @"HKEY_CURRENT_USER\";
            var strippedUserKeyPath = UserKeyPath.StartsWith(
                hivePrefix,
                StringComparison.OrdinalIgnoreCase)
                ? UserKeyPath.Substring(hivePrefix.Length)
                : UserKeyPath;

            using (var key = Registry.CurrentUser.OpenSubKey(strippedUserKeyPath, writable: true))
            {
                if (key != null)
                {
                    key.DeleteValue("LocalDatabasePath", throwOnMissingValue: false);
                    key.DeleteValue("LocalDatabaseFileName", throwOnMissingValue: false);
                }
            }
        }

        /// <summary>
        /// Builds a fully qualified registry key path using the application's
        /// company and product names.
        /// </summary>
        /// <param name="basePath">
        /// The base registry path.
        /// </param>
        /// <returns>
        /// The fully qualified registry key path.
        /// </returns>
        private string BuildKeyPath(string basePath)
        {
            return $@"{basePath}\{applicationInfo.CompanyName}\{applicationInfo.ProductName}";
        }
    }
}
