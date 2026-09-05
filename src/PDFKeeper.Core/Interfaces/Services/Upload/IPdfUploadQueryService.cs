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

namespace PDFKeeper.Core.Interfaces.Services.Upload
{
    /// <summary>
    /// Defines a service that provides read-only query operations to check the status of
    /// staged and rejected PDF files.
    /// </summary>
    public interface IPdfUploadQueryService
    {
        /// <summary>
        /// Determines whether any PDF files are currently staged and awaiting upload.
        /// </summary>
        /// <returns>
        /// <c>true</c> if one or more PDF files are ready for processing; otherwise, <c>false</c>.
        /// </returns>
        bool HasPendingUploads();

        /// <summary>
        /// Determines whether any PDF files were previously rejected during the upload process.
        /// </summary>
        /// <returns>
        /// <c>true</c> if rejected PDF files exist; otherwise, <c>false</c>.
        /// </returns>
        bool HasRejectedUploads();
    }
}
