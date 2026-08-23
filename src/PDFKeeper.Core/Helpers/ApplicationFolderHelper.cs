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
using PDFKeeper.Core.Services;
using PDFKeeper.Core.Storage;

namespace PDFKeeper.Core.Helpers
{
    /// <summary>
    /// Helper class for managing application folder paths. Provides methods to retrieve or create
    /// specific application folders based on the provided ApplicationFolder enum.
    /// </summary>
    internal static class ApplicationFolderHelper
    {
        /// <summary>
        /// Retrieves the full path of the specified application folder, creating it
        /// if it does not exist.
        /// </summary>
        /// <param name="applicationFolder">
        /// The application folder whose path is to be retrieved or created.
        /// </param>
        /// <returns>
        /// The full path to the specified application folder.
        /// </returns>
        internal static string GetApplicationFolderPath(ApplicationFolder applicationFolder)
        {
            var applicationFolderManager = new ApplicationFolderManager(
                new ApplicationInfoService());
            return applicationFolderManager.GetOrCreateFolderPath(applicationFolder);
        }
    }
}
