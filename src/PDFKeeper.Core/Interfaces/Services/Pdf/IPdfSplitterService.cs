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

using System.Collections.ObjectModel;

namespace PDFKeeper.Core.Interfaces.Services.Pdf
{
    /// <summary>
    /// Defines a service that splits multi-page PDF files into one-page PDF files.
    /// </summary>
    public interface IPdfSplitterService
    {
        /// <summary>
        /// Splits the specified PDF file into individual one-page PDF files.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to split.
        /// </param>
        /// <param name="destinationPath">
        /// The full path of the folder where the one-page PDF files will be created.
        /// </param>
        /// <returns>
        /// A collection of file paths representing the one-page PDF files.
        /// </returns>
        Collection<string> SplitPdf(string pdfPath, string destinationPath);
    }
}
