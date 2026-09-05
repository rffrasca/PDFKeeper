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

namespace PDFKeeper.Core.Rules
{
    internal class ExportPdfMetadataRule : RuleBase
    {
        private readonly PdfMetadataDto pdfMetadataDto;
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportPdfMetadataRule"/> class
        /// that verifies the properties in the <see cref="PdfMetadataDto"/> instance match
        /// the cooresponding properties in the <see cref="Document"/> instance.
        /// </summary>
        /// <param name="pdfMetadataDto">
        /// The <see cref="PdfMetadataDto"/> instance.
        /// </param>
        /// <param name="document">
        /// The <see cref="Document"/> instance.
        /// </param>
        internal ExportPdfMetadataRule(PdfMetadataDto pdfMetadataDto, Document document)
        {
            this.pdfMetadataDto = pdfMetadataDto;
            this.document = document;
            CheckForViolation();
        }

        protected override void CheckForViolation()
        {
            if (pdfMetadataDto.Title != document.Title ||
                pdfMetadataDto.Author != document.Author ||
                pdfMetadataDto.Subject != document.Subject ||
                pdfMetadataDto.Keywords != document.Keywords)
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
