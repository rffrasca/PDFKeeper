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
using PDFKeeper.Core.Custom.Pdf;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using System.Collections.ObjectModel;
using System.IO;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfSplitterService"/> interface.
    /// </summary>
    public sealed class PdfSplitterService : IPdfSplitterService
    {
        public Collection<string> SplitPdf(string pdfPath, string destinationPath)
        {
            var pdfPaths = new Collection<string>();

            using (var reader = new PdfReader(pdfPath))
            {
                using (var document = new PdfDocument(reader))
                {
                    var pdfSplitter = new PdfSplitter(document, destinationPath, pdfPath);

                    foreach (var splittedDocument in pdfSplitter.SplitByPageCount(1))
                    {
                        splittedDocument.Close();
                    }
                }
            }

            var searchPattern = $"{Path.GetFileNameWithoutExtension(pdfPath)}_*.pdf";

            foreach (var splitPdfPath in Directory.GetFiles(destinationPath, searchPattern))
            {
                pdfPaths.Add(splitPdfPath);
            }

            return pdfPaths;
        }
    }
}
