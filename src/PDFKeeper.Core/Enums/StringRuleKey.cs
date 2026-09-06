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
    /// Defines the unique keys used to resolve specific string rules from the dependency injection
    /// container. Each string rule is registered under one of these keys, allowing ViewModels and
    /// other components to explicitly request the correct string rule implementation.
    /// </summary>
    public enum StringRuleKey
    {
        /// <summary>
        /// File name string rule used to detect if a file contains '%' or '+' in the filename.
        /// </summary>
        Filename,

        /// <summary>
        /// Notes string rule used to verify that the size of the notes string does not exceed the
        /// data length of the <c>DOC_NOTES</c> column in the database when the
        /// <see cref="DatabaseSession.PlatformName"/> is
        /// <see cref="DatabaseSession.CompatiblePlatformName.Oracle"/>.
        /// </summary>
        Notes
    }
}
