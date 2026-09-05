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
using System.Collections.Generic;

namespace PDFKeeper.Core.Interfaces.Services.Pdf
{
    public interface IPdfListingService
    {
        /// <summary>
        /// Gets the full paths of all PDF files located within the specified application folder
        /// including any subfolders.
        /// </summary>
        /// <param name="applicationFolder">
        /// The application folder to search.
        /// </param>
        /// <returns>
        /// A collection of PDF file paths ordered by last write time.
        /// </returns>
        IReadOnlyCollection<string> GetPdfPaths(ApplicationFolder applicationFolder);
    }
}
