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

using System;

namespace PDFKeeper.Core.DataAccess.Repository
{
    /// <summary>
    /// Represents a cached PDF document and its associated hash value.
    /// </summary>
    /// <remarks>
    /// Provides immutable, read‑only access to the stored hash and PDF content
    /// using <see cref="ReadOnlyMemory{T}"/> to satisfy CA1819 and prevent
    /// external mutation of the underlying arrays.
    /// </remarks>
    /// <param name="hash">
    /// The hash value of the document as a byte array. The data is exposed as
    /// <see cref="ReadOnlyMemory{T}"/> to ensure immutability.
    /// </param>
    /// <param name="pdf">
    /// The PDF file content as a byte array. The data is exposed as
    /// <see cref="ReadOnlyMemory{T}"/> to ensure immutability.
    /// </param>
    public sealed class DocumentCacheEntry(byte[] hash, byte[] pdf)
    {
        /// <summary>
        /// Gets the hash value of the document as read‑only memory.
        /// </summary>
        public ReadOnlyMemory<byte> Hash { get; } = hash;

        /// <summary>
        /// Gets the PDF content as read‑only memory.
        /// </summary>
        public ReadOnlyMemory<byte> Pdf { get; } = pdf;
    }
}
