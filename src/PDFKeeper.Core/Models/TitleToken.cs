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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.Models
{
    internal class TitleToken
    {
        /// <summary>
        /// Gets the value of <see cref="Resources.DateToken"/>.
        /// </summary>
        internal static string DateToken => Resources.DateToken;

        /// <summary>
        /// Gets the value of <see cref="Resources.DateTimeToken"/>.
        /// </summary>
        internal static string DateTimeToken => Resources.DateTimeToken;

        /// <summary>
        /// Gets the value of <see cref="Resources.FileNameToken"/>.
        /// </summary>
        internal static string FileNameToken => Resources.FileNameToken;

        /// <summary>
        /// Gets the current Date in yyyy-MM-dd format.
        /// </summary>
        internal static string GetDate() =>
            DateTime.Now.ToString(
                "yyyy-MM-dd",
                CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the current Date and Time in yyyy-MM-dd HH:mm:ss format.
        /// </summary>
        internal static string GetDateTime() => 
            DateTime.Now.ToString(
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the name of a file without the extension.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> object.</param>
        /// <returns>The file name without the extension.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string GetFileName(FileInfo file) =>
            Path.GetFileNameWithoutExtension(
                file.FullName);

        /// <summary>
        /// Gets an array containing all Title Tokens.
        /// </summary>
        /// <returns>The <c>string[]</c> of Title Tokens.</returns>
        internal static string[] GetTokens() => GetTokensInternal().ToArray();
        
        private static IEnumerable<string> GetTokensInternal()
        {
            yield return string.Empty;
            yield return DateToken;
            yield return DateTimeToken;
            yield return FileNameToken;
        }
    }
}
