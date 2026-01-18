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

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PDFKeeper.Core.Extensions
{
    internal static class StringExtension
    {
        /// <summary>
        /// Checks the <see cref="string"/> for invalid file name characters as defined by the
        /// operating system.
        /// </summary>
        /// <param name="value">The <see cref="string"/>.</param>
        /// <returns>
        /// <c>true</c> or <c>false</c> if the <see cref="string"/> contains invalid file name
        /// characters.
        /// </returns>
        internal static bool ContainInvalidFileNameChars(this string value)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                if (value.Contains(invalidChar))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Appends the contents of a text file to the <see cref="string"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="string"/> to append to.
        /// </param>
        /// <param name="file">
        /// The <see cref="FileInfo"/> object of the text file to append.
        /// </param>
        /// <returns>The new <see cref="string"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string AppendTextFile(this string value, FileInfo file)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            
            if (value.Length > 0)
            {
                if (value.Substring(value.Length - 1) != "\n")
                {
                    value += Environment.NewLine;
                }
            }
            
            return string.Concat(
                value,
                File.ReadAllText(
                    file.FullName).Trim(),
                Environment.NewLine);
        }

        /// <summary>
        /// Gets the size of the <see cref="string"/> in bytes.
        /// </summary>
        /// <param name="value">The <see cref="string"/>.</param>
        /// <returns>The size of the <see cref="string"/> in bytes.</returns>
        public static int GetByteCount(this string value)
        {
            return Encoding.UTF8.GetByteCount(value);
        }

        /// <summary>
        /// Removes any characters from the provided string that are not valid for use in a file
        /// name on the current operating system.
        /// </summary>
        /// <param name="value">
        /// The string to sanitize by filtering out invalid file name characters.
        /// </param>
        /// <returns>
        /// A new string containing only characters permitted in file names.
        /// </returns>
        /// <remarks>
        /// This method uses <see cref="Path.GetInvalidFileNameChars"/> to determine which
        /// characters are disallowed and excludes them from the result.
        /// </remarks>
        public static string RemoveInvalidFileNameChars(this string value)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Concat(value.Where(c => !invalidChars.Contains(c)));
        }

        /// <summary>
        /// Replaces each invalid file name character in the string with an '_'.
        /// </summary>
        /// <param name="value">The <see cref="string"/>.</param>
        /// <returns>The modified <see cref="string"/>.</returns>
        internal static string ReplaceInvalidFileNameChars(this string value)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                value = value.Replace(invalidChar, '_');
            }

            return value;
        }

        /// <summary>
        /// Replaces each invalid path name character in the string with an '_'.
        /// </summary>
        /// <param name="value">The <see cref="string"/>.</param>
        /// <returns>The modified <see cref="string"/>.</returns>
        internal static string ReplaceInvalidPathChars(this string value)
        {
            foreach (char invalidChar in Path.GetInvalidPathChars())
            {
                value = value.Replace(invalidChar, '_');
            }

            return value;
        }

        /// <summary>
        /// Writes the <see cref="string"/> to a file.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to write.</param>
        /// <param name="outputFileName">The output file name.</param>
        public static void WriteToFile(this string value, string outputFileName)
        {
            File.WriteAllText(outputFileName, value);
        }
    }
}
