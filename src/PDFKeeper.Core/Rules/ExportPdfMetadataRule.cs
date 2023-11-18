// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2023 Robert F. Frasca
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
using PDFKeeper.Core.Models;

namespace PDFKeeper.Core.Rules
{
    internal class ExportPdfMetadataRule : RuleBase
    {
        private readonly PdfMetadata pdfMetadata;
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of the ExportPdfMetadataRule class that verifies the
        /// properties in the PdfMetadata object match the cooresponding properties in the Document
        /// object.
        /// </summary>
        /// <param name="pdfMetadata">The PdfMetadata object.</param>
        /// <param name="document">The Document object.</param>
        internal ExportPdfMetadataRule(PdfMetadata pdfMetadata, Document document)
        {
            this.pdfMetadata = pdfMetadata;
            this.document = document;
            CheckForViolation();
        }

        protected override void CheckForViolation()
        {
            if (pdfMetadata.Title != document.Title || pdfMetadata.Author != document.Author ||
                pdfMetadata.Subject != document.Subject ||
                pdfMetadata.Keywords != document.Keywords)
            {
                ViolationFound = true;
            }
            else
            {
                ViolationFound = false;
            }
        }
    }
}
