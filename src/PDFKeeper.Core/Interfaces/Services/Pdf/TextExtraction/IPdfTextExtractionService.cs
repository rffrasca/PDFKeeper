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

namespace PDFKeeper.Core.Interfaces.Services.Pdf.TextExtraction
{
    /// <summary>
    /// Defines a service that provides text extraction for PDF files, including OCR-based
    /// extraction when text cannot be retrieved using standard PDF parsing.
    /// </summary>
    public interface IPdfTextExtractionService
    {
        /// <summary>
        /// Extracts text from the specified PDF file using the appropriate
        /// <see cref="IPdfTextExtractionStrategy"/> for each page.
        /// <para>
        /// <see cref="PdfTextExtractionStrategy"/>: Uses iText for text based PDF page except when
        /// page contains an invalid encoding due to iText's strict adherence to the PDF
        /// specification (ISO 32000) or when iText is unable to extract text from the page for
        /// another reason.
        /// </para>
        /// <para>
        /// <see cref="PdfOcrTextExtractionStrategy"/>: Uses OCR for text based PDF when page is
        /// rejected by iText or when the page is "Image-only". This strategy will also be used
        /// when PDF page contains image data and <c>ocrImageDataPages</c> is <c>true</c>.
        /// </para>
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to extract text from.
        /// </param>
        /// <param name="ocrImageDataPages">
        /// <c>true</c> to apply OCR to pages containing image data; otherwise, <c>false</c>.
        /// </param>
        /// <returns>
        /// The extracted text.
        /// </returns>
        string ExtractText(string pdfPath, bool ocrImageDataPages);
    }
}
