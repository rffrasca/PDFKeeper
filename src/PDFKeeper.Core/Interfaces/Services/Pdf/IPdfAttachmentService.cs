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
    /// Defines a service for extracting attachments and embedded files from PDF files.
    /// </summary>
    public interface IPdfAttachmentService
    {
        /// <summary>
        /// Returns the number of standard PDF attachments in the document.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to evaluate.
        /// </param>
        /// <returns>
        /// The number of standard PDF attachments.
        /// </returns>
        int GetAttachmentCount(string pdfPath);

        /// <summary>
        /// Returns the number of embedded file streams in the document.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to evaluate.
        /// </param>
        /// <returns>
        /// The number of embedded file streams.
        /// </returns>
        int GetEmbeddedFileCount(string pdfPath);

        /// <summary>
        /// Extracts all attachments or embedded files to the specified folder.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file whose attachments will be extracted.
        /// </param>
        /// <param name="pdfAttachmentType">
        /// The type of attached content to extract.
        /// </param>
        /// <param name="destinationPath">
        /// The full path of the folder where extracted files will be written.
        /// </param>
        void ExtractAllToFolder(
            string pdfPath,
            PdfAttachmentType pdfAttachmentType,
            string destinationPath);

        /// <summary>
        /// Extracts all attachments or embedded files into a ZIP archive.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file whose attachments will be extracted.
        /// </param>
        /// <param name="pdfAttachmentType">
        /// The type of attached content to extract.
        /// </param>
        /// <param name="zipPath">
        /// The full path of the ZIP archive to create.
        /// </param>
        void ExtractAllToZip(string pdfPath, PdfAttachmentType pdfAttachmentType, string zipPath);
    }
}
