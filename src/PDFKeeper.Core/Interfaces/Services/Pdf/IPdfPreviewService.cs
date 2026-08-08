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

using System.Threading.Tasks;

namespace PDFKeeper.Core.Interfaces.Services.Pdf
{
    /// <summary>
    /// Provides functionality to create preview images from PDF files.
    /// </summary>
    public interface IPdfPreviewService
    {
        /// <summary>
        /// Generates a preview image from a PDF file at the specified pixel density.
        /// </summary>
        /// <param name="pdfPath">
        /// The file path of the PDF document to generate a preview from.
        /// </param>
        /// <param name="pixelDensity">
        /// The pixel density to use for the generated preview image.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the byte array of the
        /// generated preview image.
        /// </returns>
        Task<byte[]> CreatePreviewImageAsync(string pdfPath, decimal pixelDensity);
    }
}
