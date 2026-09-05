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
using PDFKeeper.Core.Interfaces.Storage;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfListingService"/> interface.
    /// </summary>
    public sealed class PdfListingService : IPdfListingService
    {
        private readonly IApplicationFolderManager applicationFolderManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfListingService"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfListingService(IApplicationFolderManager applicationFolderManager)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.applicationFolderManager = applicationFolderManager;
        }

        public IReadOnlyCollection<string> GetPdfPaths(ApplicationFolder applicationFolder)
        {
            var directory = new DirectoryInfo(
                applicationFolderManager.GetOrCreateFolderPath(applicationFolder));
#pragma warning disable IDE0305 // Simplify collection initialization
            return directory
                .GetFiles("*.pdf", SearchOption.AllDirectories)
                .OrderBy(file => file.LastWriteTimeUtc)
                .Select(file => file.FullName)
                .ToList();
#pragma warning restore IDE0305 // Simplify collection initialization
        }
    }
}
