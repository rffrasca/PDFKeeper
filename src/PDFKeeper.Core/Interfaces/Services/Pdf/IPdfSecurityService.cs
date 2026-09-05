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

using PDFKeeper.Core.Enums;

namespace PDFKeeper.Core.Interfaces.Services.Pdf
{
    /// <summary>
    /// Defines a service that provides PDF security detection for PDF files.
    /// </summary>
    public interface IPdfSecurityService
    {
        /// <summary>
        /// Determines the type of password protection applied to the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to inspect.
        /// </param>
        /// <returns>
        /// The <see cref="PdfPasswordType"/> value indicating whether the PDF is unprotected,
        /// requires an owner password, requires a user password, or is invalid/unreadable.
        /// </returns>
        PdfPasswordType GetPasswordType(string pdfPath);

        /// <summary>
        /// Validates whether the provided owner password is correct for the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to evaluate.
        /// </param>
        /// <param name="pdfOwnerPassword">
        /// The owner password as a byte array used to open password‑protected PDFs.
        /// </param>
        /// <returns>
        /// <c>true</c> if the owner password is valid for the specified PDF; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="pdfOwnerPassword"/> is <c>null</c>.
        /// </exception>
        bool ValidatePdfOwnerPassword(string pdfPath, byte[] pdfOwnerPassword);
    }
}
