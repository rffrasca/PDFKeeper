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
using System.Security;

namespace PDFKeeper.Core.Interfaces.Services.Pdf
{
    /// <summary>
    /// Defines a service that provides functionality for reading and writing PDF metadata.
    /// </summary>
    public interface IPdfMetadataService
    {
        /// <summary>
        /// Reads the combined internal and external metadata for the specified PDF file and its
        /// corresponding XML file into a <see cref="PdfMetadataDto"/> instance.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file whose metadata will be read.
        /// </param>
        /// <param name="pdfOwnerPassword">
        /// An optional owner password as a <see cref="SecureString"/> used to open
        /// password‑protected PDFs. If the PDF is not password‑protected, this value
        /// may be <c>null</c>.
        /// </param>
        /// <returns>
        /// A <see cref="PdfMetadataDto"/> instance containing the metadata extracted
        /// from the PDF file and its corresponding external metadata XML file.
        /// </returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="pdfOwnerPassword"/> is provided but is invalid
        /// for the specified PDF file.
        /// </exception>
        PdfMetadataDto Read(string pdfPath, SecureString pdfOwnerPassword = null);

        /// <summary>
        /// Writes the specified metadata to the target PDF file in
        /// <see cref="ApplicationFolder.Temp"/> with the contents from the source PDF file and
        /// internal metadata applied; and then writes the external metadata to the XML file with
        /// the same name and reside in the same folder as the target PDF file.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the source PDF file whose metadata will be updated.
        /// </param>
        /// <param name="pdfMetadataDto">
        /// The <see cref="PdfMetadataDto"/> instance containing the metadata values to write.
        /// </param>
        /// <param name="pdfOwnerPassword">
        /// An optional owner password as a <see cref="SecureString"/> used to open
        /// password‑protected PDFs. If the PDF is not password‑protected, this value
        /// may be <c>null</c>.
        /// </param>
        /// <returns>
        /// The full path of the PDF file that was written. The external metadata XML file will
        /// have the same name and reside in the same folder as the target PDF file.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="pdfMetadataDto"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="pdfOwnerPassword"/> is provided but is invalid
        /// for the specified PDF file.
        /// </exception>
        string Write(
            string pdfPath,
            PdfMetadataDto pdfMetadataDto,
            SecureString pdfOwnerPassword = null);
    }
}
