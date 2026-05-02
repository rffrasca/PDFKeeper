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
using System.Drawing;

namespace PDFKeeper.Core.FileIO
{
    /// <summary>
    /// Defines a contract for caching PDF files and their generated preview images
    /// on the local file system. Implementations are responsible for storing,
    /// retrieving, validating, and deleting cached PDF content and preview images.
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
        /// Generates and caches a preview image for the specified document ID
        /// at the given pixel density. If a cached preview exists and its hash
        /// matches the stored hash, the preview is not regenerated.
        /// </summary>
        /// <param name="id">
        /// The document ID of the PDF file.
        /// </param>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) used to generate the preview image.
        /// </param>
        void CreatePreview(int id, decimal pixelDensity);

        /// <summary>
        /// Deletes all cached PDF files and preview images associated with
        /// the specified document ID.
        /// </summary>
        /// <param name="id">
        /// The document ID whose cached files should be removed.
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

        /// <summary>
        /// Gets the cached preview image for the specified document ID and pixel density.
        /// </summary>
        /// <param name="id">
        /// The document ID of the PDF preview.
        /// </param>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) of the preview image.
        /// </param>
        /// <returns>
        /// The preview image as a <see cref="Image"/> object.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the preview image does not exist in the cache.
        /// </exception>
        Image GetPreview(int id, decimal pixelDensity);
    }
}
