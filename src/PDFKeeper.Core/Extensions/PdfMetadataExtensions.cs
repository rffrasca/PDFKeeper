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

namespace PDFKeeper.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with PDF metadata objects.
    /// </summary>
    public static class PdfMetadataExtensions
    {
        /// <summary>
        /// Converts a <see cref="PdfMetadataDto"/> instance to an <see cref="UploadProfile"/>
        /// instance with corresponding property values.
        /// </summary>
        /// <param name="pdfMetadataDto">
        /// The <see cref="PdfMetadataDto"/> instance to convert.
        /// </param>
        /// <returns>
        /// An <see cref="UploadProfile"/> instance populated with data from the
        /// <see cref="PdfMetadataDto"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="pdfMetadataDto"/> is null.
        /// </exception>
        public static UploadProfile ToUploadProfile(this PdfMetadataDto pdfMetadataDto)
        {
            if (pdfMetadataDto is null)
            {
                throw new ArgumentNullException(nameof(pdfMetadataDto));
            }

            return new UploadProfile
            {
                Title = pdfMetadataDto.Title,
                Author = pdfMetadataDto.Author,
                Subject = pdfMetadataDto.Subject,
                Keywords = pdfMetadataDto.Keywords,
                Category = pdfMetadataDto.Category,
                FlagDocument = Convert.ToBoolean(pdfMetadataDto.Flag),
                TaxYear = pdfMetadataDto.TaxYear,
                OcrPdfTextAndImageDataPages = pdfMetadataDto.OcrPdfTextAndImageDataPages
            };
        }

        /// <summary>
        /// Copies the properties from an <see cref="UploadProfile"/> instance to a
        /// <see cref="PdfMetadataDto"/> instance.
        /// </summary>
        /// <param name="pdfMetadataDto">
        /// The <see cref="PdfMetadataDto"/> instance to populate.
        /// </param>
        /// <param name="uploadProfile">
        /// The <see cref="UploadProfile"/> instance containing the data to copy.
        /// </param>
        public static void ToPdfMetadataDto(
            this PdfMetadataDto pdfMetadataDto,
            UploadProfile uploadProfile)
        {
            if (pdfMetadataDto is null)
            {
                throw new ArgumentNullException(nameof(pdfMetadataDto));
            }

            if (uploadProfile is null)
            {
                throw new ArgumentNullException(nameof(uploadProfile));
            }

            pdfMetadataDto.Title = uploadProfile.Title;
            pdfMetadataDto.Author = uploadProfile.Author;
            pdfMetadataDto.Subject = uploadProfile.Subject;
            pdfMetadataDto.Keywords = uploadProfile.Keywords;
            pdfMetadataDto.Category = uploadProfile.Category;
            pdfMetadataDto.Flag = Convert.ToInt32(uploadProfile.FlagDocument);
            pdfMetadataDto.TaxYear = uploadProfile.TaxYear;
            pdfMetadataDto.OcrPdfTextAndImageDataPages = uploadProfile.OcrPdfTextAndImageDataPages;
        }
    }
}
