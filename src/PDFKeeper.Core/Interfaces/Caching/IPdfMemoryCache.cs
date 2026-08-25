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

namespace PDFKeeper.Core.Interfaces.Caching
{
    /// <summary>
    /// Defines an interface that provides secure, in-memory caching for PDF
    /// content, keyed by document ID.
    /// </summary>
    public interface IPdfMemoryCache
    {
        /// <summary>
        /// Attempts to retrieve the cached PDF document entry for the specified document ID.
        /// </summary>
        /// <param name="documentId">
        /// The unique identifier of the document.
        /// </param>
        /// <param name="pdfCacheEntry">
        /// When this method returns <c>true</c>, this out parameter will contain the cached
        /// document hash and PDF content. When <c>false</c>, the value is <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the document exists in the cache; otherwise, <c>false</c>.
        /// </returns>
        bool TryGetPdf(int documentId, out PdfCacheEntry pdfCacheEntry);

        /// <summary>
        /// Adds or updates the cached PDF entry for the specified document ID.
        /// </summary>
        /// <param name="documentId">
        /// The unique identifier of the document.
        /// </param>
        /// <param name="hash">
        /// The SHA‑256 hash of the PDF content, used to detect changes.
        /// </param>
        /// <param name="pdfBytes">
        /// The PDF file content that will be encrypted before storing it in memory.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="pdfBytes"/> is <c>null</c>.
        /// </exception>
        void StorePdf(int documentId, byte[] hash, byte[] pdfBytes);

        /// <summary>
        /// Removes the cached PDF entry for the specified document ID, if present.
        /// </summary>
        /// <param name="documentId">
        /// The unique identifier of the PDF document to remove.
        /// </param>
        void RemovePdf(int documentId);
    }
}
