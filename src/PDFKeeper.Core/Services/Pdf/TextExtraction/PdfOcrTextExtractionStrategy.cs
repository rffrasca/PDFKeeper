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
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Interfaces.Services.Pdf.TextExtraction;
using PDFKeeper.Core.Interfaces.Storage;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace PDFKeeper.Core.Services.Pdf.TextExtraction
{
    /// <summary>
    /// Windows OCR-based implementation of the <see cref="IPdfTextExtractionStrategy"/> interface.
    /// </summary>
    public sealed class PdfOcrTextExtractionStrategy : IPdfTextExtractionStrategy
    {
        private readonly IApplicationFolderManager applicationFolderManager;
        private readonly IPdfImageService pdfImageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfOcrTextExtractionStrategy"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <param name="pdfImageService">
        /// The <see cref="IPdfImageService"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfOcrTextExtractionStrategy(
            IApplicationFolderManager applicationFolderManager,
            IPdfImageService pdfImageService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.applicationFolderManager = applicationFolderManager;
            this.pdfImageService = pdfImageService;
        }

        public string ExtractText(string pdfPath)
        {
            var text = new StringBuilder();
            var tiffImages = pdfImageService.GetAllPagesAsTiffImagesAsync(pdfPath)
                .GetAwaiter()
                .GetResult();

            foreach (var tiffImage in tiffImages)
            {
                try
                {
                    var tiffFilePath = Path.Combine(
                        applicationFolderManager.GetOrCreateFolderPath(ApplicationFolder.Temp),
                        $"{Guid.NewGuid()}.{ImageFormat.Tiff}");
                    File.WriteAllBytes(tiffFilePath, tiffImage);

                    using (var stream = File.Open(tiffFilePath, FileMode.Open, FileAccess.Read))
                    {
                        var bmpDecoder = BitmapDecoder.CreateAsync(
                            stream.AsRandomAccessStream()).GetAwaiter().GetResult();

                        using (var softwareBmp = bmpDecoder.GetSoftwareBitmapAsync()
                            .GetAwaiter()
                            .GetResult())
                        {
                            if (softwareBmp.PixelWidth <= OcrEngine.MaxImageDimension &&
                                softwareBmp.PixelHeight <= OcrEngine.MaxImageDimension)
                            {
                                var ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
                                var ocrResult = ocrEngine.RecognizeAsync(
                                    softwareBmp).GetAwaiter().GetResult();

                                foreach (var line in ocrResult.Lines)
                                {
                                    text.AppendLine(line.Text);
                                }
                            }
                        }
                    }

                    File.Delete(tiffFilePath);
                }
                catch (ArithmeticException) { }
            }

            return text.ToString();
        }
    }
}
