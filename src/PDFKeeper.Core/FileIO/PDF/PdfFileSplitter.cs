// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

namespace PDFKeeper.Core.FileIO.PDF
{
    internal class PdfFileSplitter : PdfSplitter
    {
        private readonly DirectoryInfo destination;
        private readonly string pdfName;
        private int pageNumber = 1;

        /// <summary>
        /// Initializes a new instance of the PdfFileSplitter class.
        /// </summary>
        /// <param name="pdfDocument">The PdfDocument object.</param>
        /// <param name="destination">The destination DirectoryInfo object.</param>
        /// <param name="pdfName">The PDF file name.</param>
        internal PdfFileSplitter(PdfDocument pdfDocument, DirectoryInfo destination,
            string pdfName) : base(pdfDocument)
        {
            this.destination = destination;
            this.pdfName = Path.GetFileNameWithoutExtension(pdfName);
        }

        protected override PdfWriter GetNextPdfWriter(PageRange pageRange)
        {
            return new PdfWriter(Path.Combine(destination.FullName, String.Concat(pdfName, "_",
                Math.Min(Interlocked.Increment(ref pageNumber), pageNumber - 1), ".pdf")));
        }
    }
}
