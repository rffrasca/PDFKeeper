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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.FileIO.PDF;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.FileIO
{
    /// <summary>
    /// Provides a file‑system‑based implementation of <see cref="IFileCache"/> for
    /// caching PDF files and their generated preview images. Cached files are stored
    /// in the application's designated cache directory, and file hashes are used to
    /// detect changes and avoid unnecessary writes.
    /// </summary>
    public sealed class FileCache : IFileCache
    {
        private readonly Dictionary<string, string> fileHashes;
        private readonly DirectoryInfo cacheDirectory;
        private readonly ExecutingAssembly executingAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCache"/> class and prepares
        /// the cache directory and internal hash tracking.
        /// </summary>
        public FileCache()
        {
            fileHashes = [];
            cacheDirectory = new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.Cache);
            executingAssembly = new ExecutingAssembly();
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
                        System.StringComparison.Ordinal))
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

        public void CreatePreview(int id, decimal pixelDensity)
        {
            var cached = false;
            var pdfFile = GetPdfFile(id);

            if (!pdfFile.Exists)
            {
                throw new FileNotFoundException();
            }

            var imageFile = new ImageFile(GetPdfPreviewFile(id, pixelDensity));

            if (imageFile.Exists)
            {
                try
                {
                    if (imageFile.ComputeHash().Equals(
                        fileHashes[imageFile.FullName],
                        System.StringComparison.Ordinal))
                    {
                        cached = true;
                    }
                }
                catch (KeyNotFoundException) { }
            }

            if (!cached)
            {
                File.WriteAllBytes(imageFile.FullName, pdfFile.CreatePreviewImage(pixelDensity));
                fileHashes.Add(imageFile.FullName, imageFile.ComputeHash());
            }
        }

        public void Delete(int id)
        {
            foreach (var key in fileHashes.Keys.ToList())
            {
                if (key.EndsWith(string.Concat("PDFKeeper", id, ".pdf")) ||
                    key.Contains(string.Concat("PDFKeeper", id, "-")))
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
                        cacheDirectory.FullName,
                        $"{executingAssembly.ProductName}{id}.pdf")));
        }

        public Image GetPreview(int id, decimal pixelDensity)
        {
            var imageFile = new ImageFile(GetPdfPreviewFile(id, pixelDensity));

            if (!imageFile.Exists)
            {
                throw new FileNotFoundException();
            }

            return imageFile.GetImage();
        }

        /// <summary>
        /// Gets the cached PDF preview <see cref="FileInfo"/> object for the specified
        /// document ID and pixel density. The file may or may not exist on disk.
        /// </summary>
        /// <param name="id">
        /// The document ID of the PDF preview.
        /// </param>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) of the preview image.
        /// </param>
        /// <returns>
        /// The <see cref="FileInfo"/> object representing the cached preview image.
        /// </returns>
        private FileInfo GetPdfPreviewFile(int id, decimal pixelDensity)
        {
            return new FileInfo(
                Path.Combine(
                    cacheDirectory.FullName,
                    $"{executingAssembly.ProductName}{id}-{pixelDensity}.png"));
        }
    }
}
