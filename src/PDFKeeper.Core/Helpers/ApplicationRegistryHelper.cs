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

using PDFKeeper.Core.Services;

namespace PDFKeeper.Core.Helpers
{
    /// <summary>
    /// Helper class that provides access to the full registry key path under HKEY_CURRENT_USER
    /// where application-specific configuration values are stored.
    /// </summary>
    internal static class ApplicationRegistryHelper
    {
        /// <summary>
        /// Retrieves the full registry key path under HKEY_CURRENT_USER
        /// where application-specific configuration values are stored.
        /// </summary>
        /// <returns>
        /// The full path to the registry key.
        /// </returns>
        internal static string GetUserKeyPath()
        {
            var applicationRegistryProvider = new ApplicationRegistryProvider(
                new ApplicationInfoService());
            return applicationRegistryProvider.UserKeyPath;
        }
    }
}
