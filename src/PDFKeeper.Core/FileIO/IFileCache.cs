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

using PDFKeeper.Core.FileIO.PDF;

namespace PDFKeeper.Core.FileIO
{
    /// <summary>
    /// Defines the interface for a file cache that manages cached PDF files.
    /// </summary>
    public interface IFileCache
    {
        /// <summary>
        /// Adds or updates the cached PDF file for the specified document ID.
        /// If the file already exists, its hash is compared to determine whether
        /// the cached file should be overwritten.
        /// </summary>
        /// <param name="id">
        /// The document ID associated with the PDF file.
        /// </param>
        /// <param name="pdf">
        /// The raw PDF file bytes to cache.
        /// </param>
        void AddPdf(int id, byte[] pdf);

        /// <summary>
        /// Deletes the cached PDF file associated with the specified document ID.
        /// </summary>
        /// <param name="id">
        /// The document ID whose cached PDF file should be removed.
        /// </param>
        void Delete(int id);

        /// <summary>
        /// Gets the <see cref="PdfFile"/> object representing the cached PDF file
        /// for the specified document ID. The file may or may not exist on disk.
        /// </summary>
        /// <param name="id">
        /// The document ID of the PDF file.
        /// </param>
        /// <returns>
        /// A <see cref="PdfFile"/> object representing the cached PDF file.
        /// </returns>
        PdfFile GetPdfFile(int id);
    }
}
