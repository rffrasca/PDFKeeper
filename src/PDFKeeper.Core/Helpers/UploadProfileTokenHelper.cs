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

using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Models;
using System;
using System.Globalization;
using System.IO;

namespace PDFKeeper.Core.Helpers
{
    /// <summary>
    /// Provides helper methods for generating date, time, and file name tokens used in upload
    /// profile operations.
    /// </summary>
    internal class UploadProfileTokenHelper
    {
        /// <summary>
        /// Returns the current date as a string in the format yyyy-MM-dd using the current
        /// culture.
        /// </summary>
        /// <returns>A string representing the current date in yyyy-MM-dd format.</returns>
        internal static string GetDate()
            => DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns the current date and time as a string in the format yyyy-MM-dd HH:mm:ss using
        /// the current culture.
        /// </summary>
        /// <returns>
        /// A string representing the current date and time in yyyy-MM-dd HH:mm:ss format.
        /// </returns>
        internal static string GetDateTime() => 
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

        /// <summary>
        /// Extracts the file name (without extension) from the specified <see cref="FileInfo"/>.
        /// </summary>
        /// <param name="file">The file from which to extract the name.</param>
        /// <returns>The file name without its extension.</returns>
        internal static string GetFileName(FileInfo file) => file.GetFileNameWithoutExtension();

        /// <summary>
        /// Returns the list of supported title tokens used in upload profile operations.
        /// </summary>
        /// <returns>
        /// An array of token strings including date, date/time, file name, and title tokens.
        /// </returns>
        internal static string[] GetTitleTokens()
        {
            var tokens = new string[5];
            tokens[0] = string.Empty;
            tokens[1] = UploadProfileToken.DateToken;
            tokens[2] = UploadProfileToken.DateTimeToken;
            tokens[3] = UploadProfileToken.FileNameToken;
            tokens[4] = UploadProfileToken.TitleToken;
            return tokens;
        }
    }
}
