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

using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Models;
using System;
using System.IO;

namespace PDFKeeper.Core.Storage
{
    /// <summary>
    /// Default implementation of the <see cref="IApplicationFolderManager"/> interface.
    /// </summary>
    public sealed class ApplicationFolderManager : IApplicationFolderManager
    {
        private readonly ApplicationInfoDto applicationInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationFolderManager"/> class.
        /// </summary>
        /// <param name="applicationInfoService">
        /// The <see cref="IApplicationInfoService"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="applicationInfoService"/> is null.
        /// </exception>
#pragma warning disable IDE0290 // Use primary constructor
        public ApplicationFolderManager(IApplicationInfoService applicationInfoService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            applicationInfo = applicationInfoService?.GetApplicationInfo() ??
                throw new ArgumentNullException(nameof(applicationInfoService));
        }

        public string GetOrCreateFolderPath(ApplicationFolder applicationFolder)
        {
            string folderPath;

#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (applicationFolder)
            {
                case ApplicationFolder.ApplicationData:
                    folderPath = GetApplicationDataPath();
                    break;
                case ApplicationFolder.Log:
                    folderPath = Path.Combine(
                        GetApplicationDataPath(),
                        applicationInfo.ProductVersion);
                    break;
                case ApplicationFolder.Temp:
                    folderPath = Path.Combine(
                        Path.GetTempPath(),
                        applicationInfo.ProductName);
                    break;
                default:
                    folderPath = Path.Combine(
                        GetApplicationDataPath(),
                        applicationFolder.ToString());
                    break;
            }
#pragma warning restore IDE0066 // Convert switch statement to expression

            Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        /// <summary>
        /// Retrieves the base application data folder path under the user's roaming profile.
        /// </summary>
        /// <returns>
        /// The folder path.
        /// </returns>
        private string GetApplicationDataPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                applicationInfo.CompanyName,
                applicationInfo.ProductName);
        }
    }
}
