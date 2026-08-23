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

namespace PDFKeeper.Core.Interfaces.Services
{
    /// <summary>
    /// Defines an interface that provides access to application-specific registry key paths and
    /// operations used for managing user-level and policy-level configuration entries.
    /// </summary>
    public interface IApplicationRegistryProvider
    {
        /// <summary>
        /// Gets the full registry key path under HKEY_CURRENT_USER where
        /// application-specific configuration values are stored.
        /// </summary>
        string UserKeyPath { get; }

        /// <summary>
        /// Gets the full registry key path under HKEY_LOCAL_MACHINE where
        /// application policy configuration values are stored.
        /// </summary>
        string PoliciesKeyPath { get; }

        /// <summary>
        /// Deletes the local database path and file name values from the
        /// application's user-level registry key, if they exist.
        /// </summary>
        void DeleteLocalDatabaseKeys();
    }
}
