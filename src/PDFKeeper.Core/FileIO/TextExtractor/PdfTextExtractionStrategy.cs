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
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.IO;
using System.Text;
using static iText.Kernel.Pdf.Canvas.Parser.Util.InlineImageParsingUtils;

namespace PDFKeeper.Core.FileIO.TextExtractor
{
    public class PdfTextExtractionStrategy : IPdfTextExtractionStrategy
    {
        public string GetText(FileInfo pdfFile)
        {
            using (var reader = new PdfReader(pdfFile))
            {
                var text = new StringBuilder();
                using (var document = new PdfDocument(reader))
                {
                    for (int page = 1, loopTo = document.GetNumberOfPages(); page <= loopTo;
                        page++)
                    {
                        try
                        {
                            ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                            var pageText =
                                iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(
                                    document.GetPage(page), strategy);
                            var lines = pageText.Split('\n');
                            foreach (var line in lines)
                            {
                                text.AppendLine(line);
                            }
                        }
                        catch (ArgumentException)   // PDF contains an invalid encoding.
                        {
                            return null;
                        }
                        catch (InlineImageParseException)
                        {
                            return null;
                        }
                    }
                }
                return text.ToString();
            }
        }
    }
}
