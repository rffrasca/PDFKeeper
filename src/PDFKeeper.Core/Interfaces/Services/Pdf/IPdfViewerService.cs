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

namespace PDFKeeper.Core.Interfaces.Services.Pdf
{
    /// <summary>
    /// Defines a service for opening PDF files in various viewer applications.
    /// </summary>
    public interface IPdfViewerService
    {
        /// <summary>
        /// Closes the restricted PDF viewer application if it is currently open.
        /// </summary>
        void CloseRestrictedViewer();
        
        /// <summary>
        /// Opens the PDF file for the specified document ID with the
        /// default application or the bundled PDF viewer.
        /// </summary>
        /// <param name="documentId">
        /// The ID of the document.
        /// </param>
        /// <param name="showPdfWithDefaultApplication">
        /// True to open with the default application; false to use the built-in PDF viewer.
        /// </param>
        void OpenPdf(int documentId, bool openPdfWithDefaultApplication);

        /// <summary>
        /// Opens the specified PDF file in a restricted PDF viewer application.
        /// </summary>
        /// <param name="pdfPath">The full path to the PDF file to be opened.</param>
        void OpenPdfInRestrictedViewer(string pdfPath);
    }
}
