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
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Interfaces.Caching;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.Caching
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfFileCache"/> interface.
    /// </summary>
    public sealed class PdfFileCache : IPdfFileCache
    {
        private readonly IApplicationFolderManager applicationFolderManager;
        private readonly ApplicationInfoDto applicationInfo;
        private readonly Dictionary<string, string> fileHashes;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfFileCache"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <param name="applicationInfoService">
        /// The <see cref="IApplicationInfoService"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="applicationInfoService"/> is null.
        /// </exception>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfFileCache(
#pragma warning restore IDE0290 // Use primary constructor
            IApplicationFolderManager applicationFolderManager,
            IApplicationInfoService applicationInfoService)
        {
            this.applicationFolderManager = applicationFolderManager;
            applicationInfo = applicationInfoService?.GetApplicationInfo() ??
                throw new ArgumentNullException(nameof(applicationInfoService));
#pragma warning disable IDE0028 // Simplify collection initialization
            fileHashes = new Dictionary<string, string>();
#pragma warning restore IDE0028 // Simplify collection initialization
        }

        public string GetPdfPath(int id)
        {
            return Path.Combine(
                applicationFolderManager.GetOrCreateFolderPath(ApplicationFolder.Cache),
                $"{applicationInfo.ProductName}{id}.pdf");
        }

        public void StorePdf(int id, byte[] pdfBytes)
        {
            var pdfPath = GetPdfPath(id);
            var pdfFile = new FileInfo(pdfPath);

            if (fileHashes.ContainsKey(pdfPath))
            {
                if (File.Exists(pdfPath))
                {
                    if (!pdfFile.ComputeHash().Equals(
                        fileHashes[pdfFile.FullName],
                        StringComparison.Ordinal))
                    {
                        File.WriteAllBytes(pdfPath, pdfBytes);
                    }
                }
                else
                {
                    File.WriteAllBytes(pdfPath, pdfBytes);
                }

                fileHashes[pdfPath] = pdfFile.ComputeHash();
            }
            else
            {
                File.WriteAllBytes(pdfPath, pdfBytes);
                fileHashes.Add(pdfPath, pdfFile.ComputeHash());
            }
        }

        public void DeletePdf(int id)
        {
            foreach (var key in fileHashes.Keys.ToList())
            {
                if (key.EndsWith(string.Concat("PDFKeeper", id, ".pdf")))
                {
                    File.Delete(key);
                    fileHashes.Remove(key);
                }
            }
        }
    }
}
