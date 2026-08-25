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

namespace PDFKeeper.Core.Interfaces.Caching
{
    /// <summary>
    /// Defines an interface for a PDF file cache.
    /// </summary>
    public interface IPdfFileCache
    {
        /// <summary>
        /// Gets the absolute file path of the cached PDF associated with the specified
        /// document ID.
        /// </summary>
        /// <param name="id">
        /// The unique document ID.
        /// </param>
        /// <returns>
        /// The absolute file path of the cached PDF.
        /// </returns>
        string GetPdfPath(int id);

        /// <summary>
        /// Stores or updates the cached PDF file for the specified document ID.
        /// </summary>
        /// <param name="id">
        /// The document ID.
        /// </param>
        /// <param name="pdf">
        /// The PDF file bytes.
        /// </param>
        void StorePdf(int id, byte[] pdfBytes);

        /// <summary>
        /// Deletes the cached PDF file for the specified document ID.
        /// </summary>
        /// <param name="id">
        /// The document ID.
        /// </param>
        void DeletePdf(int id);
    }
}
