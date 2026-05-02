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

namespace PDFKeeper.Core.DataAccess.Repository
{
    /// <summary>
    /// Defines an in‑memory cache for storing document metadata and PDF content,
    /// keyed by document ID. Implementations may apply encryption or other
    /// protections transparently while preserving the public API.
    /// </summary>
    public interface IDocumentCache
    {
        /// <summary>
        /// Attempts to retrieve the cached document entry for the specified
        /// document ID.
        /// </summary>
        /// <param name="documentId">
        /// The unique identifier of the document.
        /// </param>
        /// <param name="documentCacheEntry">
        /// When this method returns <c>true</c>, contains the cached document
        /// hash and PDF content. When <c>false</c>, the value is <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the document exists in the cache; otherwise, <c>false</c>.
        /// </returns>
        bool TryGet(int documentId, out DocumentCacheEntry documentCacheEntry);

        /// <summary>
        /// Adds or updates the cached entry for the specified document ID.
        /// </summary>
        /// <param name="documentId">
        /// The unique identifier of the document.
        /// </param>
        /// <param name="hash">
        /// The SHA‑256 hash of the PDF content, used to detect changes.
        /// </param>
        /// <param name="pdf">
        /// The PDF file content. Implementations may encrypt this value before
        /// storing it in the cache.
        /// </param>
        void Set(int documentId, byte[] hash, byte[] pdf);

        /// <summary>
        /// Removes the cached entry for the specified document ID, if present.
        /// </summary>
        /// <param name="documentId">
        /// The unique identifier of the document to remove.
        /// </param>
        void Remove(int documentId);
    }
}
