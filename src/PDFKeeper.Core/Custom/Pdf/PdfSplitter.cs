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
using iText.Kernel.Utils;
using System;
using System.IO;
using System.Threading;

namespace PDFKeeper.Core.Custom.Pdf
{
    /// <summary>
    /// Custom implementation of the <see cref="iText.Kernel.Utils.PdfSplitter"/> class.
    /// </summary>
    internal class PdfSplitter : iText.Kernel.Utils.PdfSplitter
    {
        private readonly string destinationPath;
        private readonly string pdfName;
        private int pageNumber = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfSplitter"/> class.
        /// </summary>
        /// <param name="pdfDocument">
        /// The <see cref="PdfDocument"/> instance.
        /// </param>
        /// <param name="destination">
        /// The destination folder path.
        /// </param>
        /// <param name="pdfPath">
        /// The full path of the PDF file to split.
        /// </param>
        internal PdfSplitter(
            PdfDocument pdfDocument,
            string destinationPath,
            string pdfPath) : base(pdfDocument)
        {
            this.destinationPath = destinationPath;
            pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        }

        protected override PdfWriter GetNextPdfWriter(PageRange pageRange)
        {
            return new PdfWriter(
                Path.Combine(
                    destinationPath,
                    $"{pdfName}_{Math.Min(
                        Interlocked.Increment(ref pageNumber),
                        pageNumber - 1)}.pdf"));
        }
    }
}
