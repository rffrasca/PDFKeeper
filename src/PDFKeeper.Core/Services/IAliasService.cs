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

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Provides methods to retrieve and assign aliases for specified keys.
    /// </summary>
    public interface IAliasService
    {
        /// <summary>
        /// Retrieves the alias associated with the specified key.
        /// </summary>
        /// <param name="key">The key for which to retrieve the alias.</param>
        /// <returns>The alias corresponding to the given key.</returns>
        string GetAlias(string key);
        
        /// <summary>
        /// Associates an alias with the specified key.
        /// </summary>
        /// <param name="key">The key to associate with an alias.</param>
        /// <param name="alias">The alias to assign to the key.</param>
        void SetAlias(string key, string alias);
    }
}
