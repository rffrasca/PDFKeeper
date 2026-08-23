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

namespace PDFKeeper.Core.Enums
{
    /// <summary>
    /// Defines the names of optional policies used by the application to control
    /// state and behavior.
    /// </summary>
    public enum ApplicationPolicy
    {
        /// <summary>
        /// Disables the ability to list all documents in the user interface.
        /// </summary>
        DisableAllDocumentsListing,

        /// <summary>
        /// Forces a modal, blocking UI dialog during the upload, preventing the user from
        /// interacting with the application until the upload completes.
        /// </summary>
        BlockUIDuringUpload
    }
}
