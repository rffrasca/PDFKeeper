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
using PDFKeeper.Core.FileIO.PDF;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.FileIO
{
    /// <summary>
    /// Default implementation of the <see cref="IFileCache"/> interface.
    /// </summary>
    public sealed class FileCache : IFileCache
    {
        private readonly IApplicationFolderManager applicationFolderManager;
        private readonly ApplicationInfoDto applicationInfo;
        private readonly Dictionary<string, string> fileHashes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCache"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance used to manage application
        /// folders.
        /// </param>
        /// <param name="applicationInfoService">
        /// The service that provides information about the application.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="applicationInfoService"/> is null.
        /// </exception>
        public FileCache(
            IApplicationFolderManager applicationFolderManager,
            IApplicationInfoService applicationInfoService)
        {
            this.applicationFolderManager = applicationFolderManager;
            applicationInfo = applicationInfoService?.GetApplicationInfo() ??
                throw new ArgumentNullException(nameof(applicationInfoService));
            fileHashes = [];
        }

        public void AddPdf(int id, byte[] pdf)
        {
            var pdfFile = GetPdfFile(id);

            if (fileHashes.ContainsKey(pdfFile.FullName))
            {
                if (pdfFile.Exists)
                {
                    if (!pdfFile.ComputeHash().Equals(
                        fileHashes[pdfFile.FullName],
                        StringComparison.Ordinal))
                    {
                        File.WriteAllBytes(pdfFile.FullName, pdf);
                    }
                }
                else
                {
                    File.WriteAllBytes(pdfFile.FullName, pdf);           
                }

                fileHashes[pdfFile.FullName] = pdfFile.ComputeHash();
            }
            else
            {
                File.WriteAllBytes(pdfFile.FullName, pdf);
                fileHashes.Add(pdfFile.FullName, pdfFile.ComputeHash());
            }
        }

        public void Delete(int id)
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

        public PdfFile GetPdfFile(int id)
        {
            return new PdfFile(
                new FileInfo(
                    Path.Combine(
                        applicationFolderManager.GetOrCreateFolderPath(ApplicationFolder.Cache),
                        $"{applicationInfo.ProductName}{id}.pdf")));
        }
    }
}
