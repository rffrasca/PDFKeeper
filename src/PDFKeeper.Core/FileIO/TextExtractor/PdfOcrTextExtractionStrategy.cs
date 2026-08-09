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

using PDFKeeper.Core.Interfaces.Services.Pdf;
using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.FileIO.TextExtractor
{
    public class PdfOcrTextExtractionStrategy : IPdfTextExtractionStrategy
    {
        private readonly IPdfImageService pdfImageService;

        /// <summary>
        /// Initializes a new instance of the PdfOcrTextExtractionStrategy class.
        /// </summary>
        /// <param name="pdfImageService">The service for processing PDF images.</param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfOcrTextExtractionStrategy(IPdfImageService pdfImageService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.pdfImageService = pdfImageService;
        }

        public string GetText(FileInfo pdfFile)
        {
            if (pdfFile is null)
            {
                throw new ArgumentNullException(nameof(pdfFile));
            }

            var images = pdfImageService.GetAllPagesAsTiffImagesAsync(
                pdfFile.FullName).GetAwaiter().GetResult();
            var imageCollection = new Collection<byte[]>(images.ToList());
            var ocr = new ImageTextExtractor(imageCollection, ImageFormat.Tiff);
            var result = ocr.GetText();
            return result.Result;
        }
    }
}
