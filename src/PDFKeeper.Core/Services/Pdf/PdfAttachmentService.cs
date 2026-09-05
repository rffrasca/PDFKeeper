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

using iText.Kernel.Pdf;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfAttachmentService"/> interface.
    /// </summary>
    public sealed class PdfAttachmentService : IPdfAttachmentService
    {
        public int GetAttachmentCount(string pdfPath)
        {
            return GetAllAttachments(pdfPath).Count;
        }

        public int GetEmbeddedFileCount(string pdfPath)
        {
            return GetAllEmbeddedFiles(pdfPath).Count;
        }

        public void ExtractAllToFolder(
            string pdfPath,
            PdfAttachmentType pdfAttachmentType,
            string destinationPath)
        {
            switch (pdfAttachmentType)
            {
                case PdfAttachmentType.Attachment:
                    foreach (var key in GetAllAttachments(pdfPath).ToArray())
                    {
                        File.WriteAllBytes(Path.Combine(destinationPath, key.Key), key.Value);
                    }

                    break;
                case PdfAttachmentType.EmbeddedFile:
                    foreach (var key in GetAllEmbeddedFiles(pdfPath).ToArray())
                    {
                        string dirPath = null;
                        string keyName = null;

                        if (key.Key.Contains(@"\"))
                        {
                            keyName = key.Key;
                            dirPath = Path.Combine(
                                destinationPath,
                                Path.GetDirectoryName(keyName));
                            Directory.CreateDirectory(dirPath);
                        }

                        if (keyName is null)
                        {
                            keyName = key.Key;
                        }
                        else
                        {
                            keyName = Path.GetFileName(key.Key);
                        }

                        File.WriteAllBytes(Path.Combine(dirPath, keyName), key.Value);
                    }

                    break;
            }
        }

        public void ExtractAllToZip(
            string pdfPath,
            PdfAttachmentType pdfAttachmentType,
            string zipPath)
        {
            switch (pdfAttachmentType)
            {
                case PdfAttachmentType.Attachment:
                    GetAllAttachments(pdfPath).ToZipFile(zipPath);
                    break;
                case PdfAttachmentType.EmbeddedFile:
                    GetAllEmbeddedFiles(pdfPath).ToZipFile(zipPath);
                    break;
            }
        }

        /// <summary>
        /// Gets a dictionary containing the names and contents of all attachments in
        /// the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to inspect.
        /// </param>
        /// <returns>
        /// The dictionary mapping file names to their corresponding byte content.
        /// </returns>
        private static Dictionary<string, byte[]> GetAllAttachments(string pdfPath)
        {
            var attachments = new Dictionary<string, byte[]>();

            using (var reader = new PdfReader(pdfPath))
            {
                using (var document = new PdfDocument(reader))
                {
                    try
                    {
                        var catalog = document.GetCatalog().GetPdfObject();
                        var names = catalog.GetAsDictionary(PdfName.Names);
                        var filespecs = names.GetAsDictionary(
                            PdfName.EmbeddedFiles).GetAsArray(
                            PdfName.Names);

                        for (int i = 1; i < filespecs.Size(); i++)
                        {
                            var filespec = filespecs.GetAsDictionary(i);
                            var file = filespec.GetAsDictionary(PdfName.EF);

                            foreach (PdfName key in file.KeySet())
                            {
                                var filename = filespec.GetAsString(key).ToString();

                                if (!attachments.ContainsKey(filename))
                                {
                                    attachments.Add(filename, file.GetAsStream(key).GetBytes());
                                }
                            }
                        }
                    }
                    catch (NullReferenceException) { }
                }
            }

            return attachments;
        }

        /// <summary>
        /// Gets a dictionary containing the names and contents of all embedded files in
        /// the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to inspect.
        /// </param>
        /// <returns>
        /// The dictionary mapping file names to their corresponding byte content.
        /// </returns>
        private static Dictionary<string, byte[]> GetAllEmbeddedFiles(string pdfPath)
        {
            var embeddedFiles = new Dictionary<string, byte[]>();

            using (var reader = new PdfReader(pdfPath))
            {
                using (var document = new PdfDocument(reader))
                {
                    for (int i = 1; i <= document.GetNumberOfPages(); i++)
                    {
                        var pdfArray = document.GetPage(i).GetPdfObject().GetAsArray(
                            PdfName.Annots);

                        if (pdfArray != null)
                        {
                            for (int j = 0; j < pdfArray.Size(); j++)
                            {
                                var annot = pdfArray.GetAsDictionary(j);

                                if (PdfName.FileAttachment.Equals(
                                    annot.GetAsName(PdfName.Subtype)))
                                {
                                    var filespec = annot.GetAsDictionary(PdfName.FS);
                                    var refs = filespec.GetAsDictionary(PdfName.EF);

                                    foreach (PdfName key in refs.KeySet())
                                    {
                                        var filename = filespec.GetAsString(
                                            key).ToString().Replace(
                                            Path.AltDirectorySeparatorChar,
                                            Path.DirectorySeparatorChar).Substring(3);

                                        if (!embeddedFiles.ContainsKey(filename))
                                        {
                                            embeddedFiles.Add(
                                                filename,
                                                refs.GetAsStream(key).GetBytes());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return embeddedFiles;
        }
    }
}
