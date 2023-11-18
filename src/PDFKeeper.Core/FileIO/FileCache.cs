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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.FileIO.PDF;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace PDFKeeper.Core.FileIO
{
    public class FileCache
    {
        private readonly Dictionary<string, string> fileHashes;
        private readonly DirectoryInfo cacheDirectory;
        private readonly ExecutingAssembly executingAssembly;

        public FileCache()
        {
            fileHashes = new Dictionary<string, string>();
            cacheDirectory = new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.Cache);
            executingAssembly = new ExecutingAssembly();
        }

        /// <summary>
        /// Adds a PDF to the file cache.
        /// </summary>
        /// <param name="id">The document ID of the PDF.</param>
        /// <param name="pdf">The contents of the PDF.</param>
        public void AddPdf(int id, byte[] pdf)
        {
            var cached = false;
            var pdfFile = GetPdfFile(id);
            if (pdfFile.Exists)
            {
                try
                {
                    if (pdfFile.ComputeHash().Equals(fileHashes[pdfFile.FullName],
                        System.StringComparison.Ordinal))
                    {
                        cached = true;
                    }
                }
                catch (KeyNotFoundException) { }
            }
            if (cached == false)
            {
                File.WriteAllBytes(pdfFile.FullName, pdf);
                if (fileHashes.ContainsKey(pdfFile.FullName)) 
                {
                    fileHashes[pdfFile.FullName] = pdfFile.ComputeHash();
                }
                else
                {
                    fileHashes.Add(pdfFile.FullName, pdfFile.ComputeHash());
                }
            }
        }

        /// <summary>
        /// Creates a preview image from the cached PDF.
        /// </summary>
        /// <param name="id">The document ID of the cached PDF.</param>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) of the PDF preview image.
        /// </param>
        /// <exception cref="FileNotFoundException"></exception>
        public void CreatePreview(int id, decimal pixelDensity)
        {
            var cached = false;
            var pdfFile = GetPdfFile(id);
            if (pdfFile.Exists == false)
            {
                throw new FileNotFoundException();
            }
            var imageFile = new ImageFile(GetPdfPreviewFile(id, pixelDensity));
            if (imageFile.Exists)
            {
                try
                {
                    if (imageFile.ComputeHash().Equals(fileHashes[imageFile.FullName],
                        System.StringComparison.Ordinal))
                    {
                        cached = true;
                    }
                }
                catch (KeyNotFoundException) { }
            }
            if (cached == false)
            {
                File.WriteAllBytes(imageFile.FullName, pdfFile.CreatePreviewImage(pixelDensity));
                fileHashes.Add(imageFile.FullName, imageFile.ComputeHash());
            }
        }

        /// <summary>
        /// Gets the preview image from the file cache.
        /// </summary>
        /// <param name="id">The document ID of the cached image.</param>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) of the preview image.
        /// </param>
        /// <returns>The preview image.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Image GetPreview(int id, decimal pixelDensity)
        {
            var imageFile = new ImageFile(GetPdfPreviewFile(id, pixelDensity));
            if (imageFile.Exists == false)
            {
                throw new FileNotFoundException();
            }
            return imageFile.GetImage();
        }

        /// <summary>
        /// Gets the cached PDF file object.
        /// </summary>
        /// <param name="id">The document ID of the PDF.</param>
        /// <returns>The PDF file object.</returns>
        public PdfFile GetPdfFile(int id)
        {
            return new PdfFile(new FileInfo(Path.Combine(cacheDirectory.FullName,
                string.Concat(executingAssembly.ProductName, id, ".pdf"))));
        }

        /// <summary>
        /// Gets the cached PDF preview FileInfo object.
        /// </summary>
        /// <param name="id">The document ID of the PDF preview.</param>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) of the PDF preview image.
        /// </param>
        /// <returns>The PDF preview FileInfo object.</returns>
        private FileInfo GetPdfPreviewFile(int id, decimal pixelDensity)
        {
            return new FileInfo(Path.Combine(cacheDirectory.FullName,
                string.Concat(executingAssembly.ProductName, id, "-", pixelDensity, ".png")));
        }
    }
}
