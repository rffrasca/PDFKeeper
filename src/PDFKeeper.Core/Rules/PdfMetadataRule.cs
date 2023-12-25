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

using iText.Kernel.Pdf;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;

namespace PDFKeeper.Core.Rules
{
    internal class PdfMetadataRule : RuleBase
    {
        private readonly string title;
        private readonly string author;
        private readonly string subject;

        /// <summary>
        /// Initializes a new instance of the PdfMetadataRule class that verifies the length of the
        /// Title, Author, and Subject properties in the UploadProfile object is > 0.
        /// </summary>
        /// <param name="uploadProfile">The UploadProfile object.</param>
        internal PdfMetadataRule(UploadProfile uploadProfile)
        {
            title = uploadProfile.Title;
            author = uploadProfile.Author;
            subject = uploadProfile.Subject;
            CheckForViolation();
        }

        /// <summary>
        /// Initializes a new instance of the PdfMetadataRule class that verifies the length of the
        /// Title, Author, and Subject properties in the PdfDocumentInfo object is > 0.
        /// </summary>
        /// <param name="pdfDocumentInfo">The PdfDocumentInfo object.</param>
        internal PdfMetadataRule(PdfDocumentInfo pdfDocumentInfo)
        {
            title = pdfDocumentInfo.GetTitle();
            author = pdfDocumentInfo.GetAuthor();
            subject = pdfDocumentInfo.GetSubject();
            CheckForViolation();
        }

        protected override void CheckForViolation()
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) ||
                string.IsNullOrEmpty(subject))
            {
                ViolationFound = true;
                ViolationMessage = Resources.MandatoryFieldsAreBlank;
            }
            else
            {
                ViolationFound = false;
            }
        }
    }
}
