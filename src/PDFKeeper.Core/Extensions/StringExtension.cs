// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

namespace PDFKeeper.Core.Extensions
{
    internal static class StringExtension
    {
        /// <summary>
        /// Checks the string for invalid file name characters as defined by the operating system.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns>true or false</returns>
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
        /// Appends the contents of a text file to the string.
        /// </summary>
        /// <param name="value">The string to append to.</param>
        /// <param name="file">The FileInfo object of the text file to append.</param>
        /// <returns>The new string.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string AppendTextFile(this string value, FileInfo file)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (file == null)
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
            return string.Concat(value, File.ReadAllText(file.FullName).Trim(),
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the string to a file.
        /// </summary>
        /// <param name="value">The string to write.</param>
        /// <param name="outputFileName">The output file name.</param>
        public static void WriteToFile(this string value, string outputFileName)
        {
            File.WriteAllText(outputFileName, value);
        }
    }
}
