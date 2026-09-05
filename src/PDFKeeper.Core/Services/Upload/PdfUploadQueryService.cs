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
using PDFKeeper.Core.Interfaces.Services.Upload;

namespace PDFKeeper.Core.Services.Upload
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfUploadQueryService"/> interface.
    /// </summary>
    public sealed class PdfUploadQueryService : IPdfUploadQueryService
    {
        private readonly IPdfListingService pdfListingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfUploadService"/> class.
        /// </summary>
        /// <param name="pdfListingService">
        /// The <see cref="IPdfListingService"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfUploadQueryService(IPdfListingService pdfListingService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.pdfListingService = pdfListingService;
        }

        public bool HasPendingUploads()
        {
            return pdfListingService.GetPdfPaths(ApplicationFolder.Upload).Count > 0 ||
                pdfListingService.GetPdfPaths(ApplicationFolder.UploadStaging).Count > 0;
        }

        public bool HasRejectedUploads()
        {
            return pdfListingService.GetPdfPaths(ApplicationFolder.UploadRejected).Count > 0;
        }
    }
}
