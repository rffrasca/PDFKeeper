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
using System.Text;

namespace PDFKeeper.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with byte arrays.
    /// </summary>
    internal static class ByteExtension
    {
        /// <summary>
        /// Determines whether the specified UTF-8 encoded byte array contains the given string as
        /// a UTF-8 substring.
        /// </summary>
        /// <remarks>
        /// The search is performed using a byte-wise comparison of the UTF-8 encoded form of text
        /// within data. No normalization or decoding is performed on the input byte array.
        /// </remarks>
        /// <param name="data">
        /// The byte array to search, interpreted as a UTF-8 encoded sequence.
        /// </param>
        /// <param name="text">
        /// The string to search for within the byte array. The string is encoded as UTF-8 before
        /// searching.
        /// </param>
        /// <returns>
        /// true if the UTF-8 encoded representation of text is found within data; otherwise,
        /// false.
        /// </returns>
        internal static bool ContainsUtf8String(this byte[] data, string text)
        {
            ReadOnlySpan<byte> haystack = data;
            ReadOnlySpan<byte> needle = Encoding.UTF8.GetBytes(text);
            return haystack.IndexOf(needle) >= 0;
        }
    }
}
