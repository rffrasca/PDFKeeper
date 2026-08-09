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

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDFKeeper.Core.Interfaces.Services.Pdf
{
    /// <summary>
    /// Provides functionality to extract images from PDF files.
    /// </summary>
    public interface IPdfImageService
    {
        /// <summary>
        /// Asynchronously converts all pages of a PDF document to TIFF images at the specified
        /// resolution.
        /// </summary>
        /// <param name="pdfPath">
        /// The file path of the PDF document to convert.
        /// </param>
        /// <param name="targetDpi">
        /// The resolution in DPI for the output TIFF images. Defaults to 600.
        /// </param>
        /// <returns>
        /// A read-only list of byte arrays, each representing a TIFF image of a PDF page.
        /// </returns>
        Task<IReadOnlyList<byte[]>> GetAllPagesAsTiffImagesAsync(
            string pdfPath,
            int targetDpi = 600);
    }
}
