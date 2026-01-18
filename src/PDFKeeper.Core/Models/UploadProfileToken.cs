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

using PDFKeeper.Core.Properties;

namespace PDFKeeper.Core.Models
{
    /// <summary>
    /// Provides access to various upload profile token strings from resources.
    /// </summary>
    internal class UploadProfileToken
    {
        /// <summary>
        /// Gets the date token string from the resources.
        /// </summary>
        internal static string DateToken => Resources.DateToken;

        /// <summary>
        /// Gets the date and time token string from the resources.
        /// </summary>
        internal static string DateTimeToken => Resources.DateTimeToken;

        /// <summary>
        /// Gets the file name token string from the resources.
        /// </summary>
        internal static string FileNameToken => Resources.FileNameToken;

        /// <summary>
        /// Gets the titler token string from the resources.
        /// </summary>
        internal static string TitleToken => Resources.TitleToken;

        /// <summary>
        /// Gets the author token string from the resources.
        /// </summary>
        internal static string AuthorToken => Resources.AuthorToken;

        /// <summary>
        /// Gets the subject token string from the resources.
        /// </summary>
        internal static string SubjectToken => Resources.SubjectToken;

        /// <summary>
        /// Gets the keywords token string from the resources.
        /// </summary>
        internal static string KeywordsToken => Resources.KeywordsToken;
    }
}
