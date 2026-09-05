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

using PDFKeeper.Core.Models;
using System.Collections.Generic;

namespace PDFKeeper.Core.Interfaces.Storage
{
    /// <summary>
    /// Defines an interface that provides methods for managing Upload Profiles in the
    /// <c>UploadProfiles</c> folder.
    /// </summary>
    public interface IUploadProfileManager
    {
        /// <summary>
        /// Retrieves the names of all Upload Profiles stored in the <c>UploadProfiles</c> folder.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> collection containing the names of all
        /// available Upload Profiles.
        /// </returns>
        IEnumerable<string> GetUploadProfileNames();

        /// <summary>
        /// Retrieves an Upload Profile by name.
        /// </summary>
        /// <param name="name">
        /// The name of the Upload Profile to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="UploadProfile"/> instance if found; otherwise, <c>null</c>.
        /// </returns>
        UploadProfile GetUploadProfile(string name);

        /// <summary>
        /// Saves an Upload Profile. If the profile name has changed, the previous file is
        /// removed before saving the new one.
        /// </summary>
        /// <param name="name">
        /// The name under which the Upload Profile will be saved.
        /// </param>
        /// <param name="uploadProfile">
        /// The <see cref="UploadProfile"/> instance to save.
        /// </param>
        /// <param name="formerName">
        /// The previous name of the Upload Profile, if it has changed; otherwise <c>null</c>.
        /// </param>
        void SaveUploadProfile(string name, UploadProfile uploadProfile, string formerName = null);

        /// <summary>
        /// Deletes an Upload Profile by name.
        /// </summary>
        /// <param name="name">
        /// The name of the Upload Profile to delete.
        /// </param>
        void DeleteUploadProfile(string name);
    }
}
