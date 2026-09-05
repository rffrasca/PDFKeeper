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

using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Interfaces.Services.Pdf.TextExtraction;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Custom.Pdf;
using System;
using System.IO;
using System.Text;
using static iText.Kernel.Pdf.Canvas.Parser.Util.InlineImageParsingUtils;

namespace PDFKeeper.Core.Services.Pdf.TextExtraction
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfTextExtractionService"/> interface.
    /// </summary>
    public sealed class PdfTextExtractionService : IPdfTextExtractionService
    {
        private readonly IApplicationFolderManager applicationFolderManager;
        private readonly IPdfSplitterService pdfSplitterService;
        private readonly PdfTextExtractionStrategy pdfTextExtractionStrategy;
        private readonly PdfOcrTextExtractionStrategy pdfOcrTextExtractionStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTextExtractionService"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <param name="pdfSplitterService">
        /// The <see cref="IPdfSplitterService"/> instance.
        /// </param>
        /// <param name="pdfTextExtractionStrategy">
        /// The <see cref="PdfTextExtractionStrategy"/> instance.
        /// </param>
        /// <param name="pdfOcrTextExtractionStrategy">
        /// The <see cref="PdfOcrTextExtractionStrategy"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfTextExtractionService(
#pragma warning restore IDE0290 // Use primary constructor
            IApplicationFolderManager applicationFolderManager,
            IPdfSplitterService pdfSplitterService,
            PdfTextExtractionStrategy pdfTextExtractionStrategy,
            PdfOcrTextExtractionStrategy pdfOcrTextExtractionStrategy)
        {
            this.applicationFolderManager = applicationFolderManager;
            this.pdfSplitterService = pdfSplitterService;
            this.pdfTextExtractionStrategy = pdfTextExtractionStrategy;
            this.pdfOcrTextExtractionStrategy = pdfOcrTextExtractionStrategy;
        }

        public string ExtractText(string pdfPath, bool ocrImageDataPages)
        {
            var pdfText = new StringBuilder();

            foreach (var pdfPagePath in pdfSplitterService.SplitPdf(
                pdfPath,
                applicationFolderManager.GetOrCreateFolderPath(ApplicationFolder.Temp)))
            {
                string text;

                if (ocrImageDataPages && CheckForImageData(pdfPath))
                {
                    text = pdfOcrTextExtractionStrategy.ExtractText(pdfPath);
                }
                else
                {
                    text = pdfTextExtractionStrategy.ExtractText(pdfPath);

                    if (string.IsNullOrEmpty(text) || text.Trim().Length.Equals(0))
                    {
                        text = pdfOcrTextExtractionStrategy.ExtractText(pdfPath);
                    }
                }

                pdfText.Append(text);
                File.Delete(pdfPagePath);
            }

            return pdfText.ToString();
        }

        /// <summary>
        /// Determines whether the specified one-page PDF file contains embedded image data.
        /// </summary>
        /// <param name="pdfPagePath">
        /// The path of the one-page PDF file to inspect.
        /// </param>
        /// <returns>
        /// <c>true</c> if the PDF page contains image data; otherwise, <c>false</c>.
        /// </returns>
        private static bool CheckForImageData(string pdfPagePath)
        {
            using (var reader = new PdfReader(pdfPagePath))
            {
                using (var document = new PdfDocument(reader))
                {
                    try
                    {
                        var imageDetector = new PdfImageDetector();
                        var canvasProcessor = new PdfCanvasProcessor(imageDetector);
                        canvasProcessor.ProcessPageContent(document.GetPage(1));
                        return imageDetector.ImagesDetected;
                    }
                    catch (Exception ex) when (
                        ex is ArgumentException ||  // PDF contains an invalid encoding.
                        ex is InlineImageParseException)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
