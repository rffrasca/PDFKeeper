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
using System;

namespace PDFKeeper.Core.Rules.PdfMetadata
{
    /// <summary>
    /// Factory class for creating instances of <see cref="PdfMetadataRuleInput"/> from
    /// different input types.
    /// </summary>
    public static class PdfMetadataRuleFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="PdfMetadataRuleInput"/> from a
        /// <see cref="PdfMetadataDto"/> instance.
        /// </summary>
        /// <param name="pdfMetadataDto">
        /// The <see cref="PdfMetadataDto"/> instance from which to create the
        /// <see cref="PdfMetadataRuleInput"/> instance.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="PdfMetadataRuleInput"/> initialized
        /// with values from the <see cref="PdfMetadataDto"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="pdfMetadataDto"/> is <c>null</c>.
        /// </exception>
        public static PdfMetadataRuleInput Create(PdfMetadataDto pdfMetadataDto)
        {
            if (pdfMetadataDto is null)
            {
                throw new ArgumentNullException(nameof(pdfMetadataDto));
            }

            return new PdfMetadataRuleInput(
                pdfMetadataDto.Title,
                pdfMetadataDto.Author,
                pdfMetadataDto.Subject);
        }

        /// <summary>
        /// Creates a new instance of <see cref="PdfMetadataRuleInput"/> from a
        /// <see cref="UploadProfile"/> instance.
        /// </summary>
        /// <param name="uploadProfile">
        /// The <see cref="UploadProfile"/> instance from which to create the
        /// <see cref="PdfMetadataRuleInput"/> instance.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="PdfMetadataRuleInput"/> initialized
        /// with values from the <see cref="UploadProfile"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="uploadProfile"/> is <c>null</c>.
        /// </exception>
        public static PdfMetadataRuleInput Create(UploadProfile uploadProfile)
        {
            if (uploadProfile is null)
            {
                throw new ArgumentNullException(nameof(uploadProfile));
            }

            return new PdfMetadataRuleInput(
                uploadProfile.Title,
                uploadProfile.Author,
                uploadProfile.Subject);
        }
    }
}
