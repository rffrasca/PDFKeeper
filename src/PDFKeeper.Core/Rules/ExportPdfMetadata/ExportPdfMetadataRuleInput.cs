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

namespace PDFKeeper.Core.Rules.ExportPdfMetadata
{
    /// <summary>
    /// Represents the input for an export PDF metadata rule, including
    /// the <see cref="PdfMetadataDto"/> instance and the associated
    /// <see cref="DocumentDto"/> instance.
    /// </summary>
    public sealed class ExportPdfMetadataRuleInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportPdfMetadataRuleInput"/> class.
        /// </summary>
        /// <param name="pdfMetadataDto">
        /// The <see cref="PdfMetadataDto"/> instance.
        /// </param>
        /// <param name="documentDto">
        /// The associated <see cref="DocumentDto"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public ExportPdfMetadataRuleInput(PdfMetadataDto pdfMetadataDto, DocumentDto documentDto)
#pragma warning restore IDE0290 // Use primary constructor
        {
            PdfMetadata = pdfMetadataDto;
            Document = documentDto;
        }

        /// <summary>
        /// Gets the <see cref="PdfMetadataDto"/> instance.
        /// </summary>
        public PdfMetadataDto PdfMetadata { get; }

        /// <summary>
        /// Gets the associated <see cref="DocumentDto"/> instance.
        /// </summary>
        public DocumentDto Document { get; }
    }
}
