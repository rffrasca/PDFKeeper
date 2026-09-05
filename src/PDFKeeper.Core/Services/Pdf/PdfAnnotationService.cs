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
using PDFKeeper.Core.Interfaces.Services.Pdf;
using System.Text;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfAnnotationService"/> interface.
    /// </summary>
    public sealed class PdfAnnotationService : IPdfAnnotationService
    {
        public string GetTextAnnotations(string pdfPath)
        {
            var text = new StringBuilder();

            using (var reader = new PdfReader(pdfPath))
            {
                using (var document = new PdfDocument(reader))
                {
                    for (int pageCounter = 1,
                        loopTo = document.GetNumberOfPages();
                        pageCounter <= loopTo;
                        pageCounter++)
                    {
                        var page = document.GetPage(pageCounter);
                        var annotations = page.GetAnnotations();

                        foreach (var annotation in annotations)
                        {
                            var dict = annotation.GetPdfObject();
                            var value = dict.GetAsString(PdfName.Contents);

                            if (value != null)
                            {
                                text.AppendLine(value.ToUnicodeString());
                            }
                        }
                    }
                }
            }

            return text.ToString();
        }
    }
}
