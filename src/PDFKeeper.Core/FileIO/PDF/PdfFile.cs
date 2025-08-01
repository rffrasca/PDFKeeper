// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2025 Robert F. Frasca
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

using ImageMagick;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.FileIO.TextExtractor;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using static iText.Kernel.Pdf.Canvas.Parser.Util.InlineImageParsingUtils;

namespace PDFKeeper.Core.FileIO.PDF
{
    public class PdfFile
    {
        private readonly FileInfo pdfFile;
        private readonly DirectoryInfo tempDirectory;

        /// <summary>
        /// <para>PDF Password type.</para>
        /// <list type="bullet">None: PDF is not password protected.</list>
        /// <list type="bullet">Owner: Owner password will be required to read PDF.</list>
        /// <list type="bullet">User: Prevents opening the PDF and is not supported.</list>
        /// <list type="bullet">
        /// Unknown: Unable to determine password type because PDF is invalid.
        /// </list>
        /// </summary>
        public enum PasswordType { None, Owner, User, Unknown }

        /// <summary>
        /// Type of attached files in the PDF.
        /// </summary>
        public enum AttachedFilesType { Attachment, EmbeddedFile }

        /// <summary>
        /// Initializes a new instance of the PdfFile class.
        /// </summary>
        /// <param name="pdfFile">
        /// The PDF FileInfo object.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public PdfFile(FileInfo pdfFile)
        {
            if (pdfFile == null)
            {
                throw new ArgumentNullException(nameof(pdfFile));
            }
            if (pdfFile.IsFileNameInvalid())
            {
                var message = ResourceHelper.GetString(
                    Resources.ResourceManager,
                    "FileNameInvalid",
                    pdfFile.FullName);
                throw new ArgumentException(message);
            }
            this.pdfFile = pdfFile;
            tempDirectory = new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.Temp);
            MagickNET.SetGhostscriptDirectory(new ExecutingAssembly().DirectoryPath);
            MagickNET.SetTempDirectory(tempDirectory.FullName);
        }

        /// <summary>
        /// Does the PDF contain attachments? (true or false)
        /// </summary>
        public bool ContainsAttachments => GetAllAttachments().Count > 0;

        /// <summary>
        /// Does the PDF contain embedded files? (true or false)
        /// </summary>
        public bool ContainsEmbeddedFiles => GetAllEmbeddedFiles().Count > 0;

        /// <summary>
        /// Does the PDF file exist? (true or false)
        /// </summary>
        public bool Exists => pdfFile.Exists;

        /// <summary>
        /// Gets the file name of the PDF without the extension.
        /// </summary>
        public string FileNameWithoutExtension => pdfFile.GetFileNameWithoutExtension();
        
        /// <summary>
        /// Gets the full path of the PDF.
        /// </summary>
        public string FullName => pdfFile.FullName;

        /// <summary>
        /// Gets the name of the PDF.
        /// </summary>
        public string Name => pdfFile.Name;

        /// <summary>
        /// Changes the directory path name of the PDF.
        /// </summary>
        /// <param name="directory">The input DirectoryInfo object.</param>
        /// <returns>The modified FileInfo object.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public FileInfo ChangeDirectory(DirectoryInfo directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            return pdfFile.ChangeDirectory(directory);
        }

        /// <summary>
        /// Changes the extension of the PDF.
        /// </summary>
        /// <param name="extension">The new extension.</param>
        /// <returns>The modified FileInfo object.</returns>
        public FileInfo ChangeExtension(string extension)
        {
            return pdfFile.ChangeExtension(extension);
        }
        
        /// <summary>
        /// Computes the hash value of the PDF.
        /// </summary>
        /// <returns>The SHA512 hash value of the PDF.</returns>
        public string ComputeHash()
        {
            return pdfFile.ComputeHash();
        }

        /// <summary>
        /// Copies the PDF to a new file.
        /// </summary>
        /// <param name="destFileName">The destination file name.</param>
        /// <param name="overwrite">Overwrite (true) or false.</param>
        public void CopyTo(string destFileName, bool overwrite)
        {
            pdfFile.CopyTo(destFileName, overwrite);
        }

        /// <summary>
        /// Copies the PDF to the Windows Clipboard.
        /// </summary>
        public async void CopyToClipboard()
        {
            var storageFile = await StorageFile.GetFileFromPathAsync(FullName);
            var list = new List<StorageFile>
            {
                storageFile
            };
            var dataPackage = new DataPackage();
            dataPackage.SetStorageItems(list);
            Clipboard.SetContent(dataPackage);
        }

        /// <summary>
        /// Deletes the PDF permanently.
        /// </summary>
        public void Delete()
        {
            pdfFile.Delete();
        }

        /// <summary>
        /// Gets the password type set in the PDF.
        /// </summary>
        /// <returns>
        /// PdfPasswordType.None: PDF is not password protected.
        /// PdfPasswordType.Owner: Owner password will be required to read PDF.
        /// PdfPasswordType.User: Prevents opening the PDF and is not supported.
        /// PdfPasswordType.Unknown: Unable to determine password type because PDF is invalid.
        /// </returns>
        public PasswordType GetPasswordType()
        {
            try
            {
                using (var reader = new PdfReader(pdfFile))
                {
                    using (var document = new PdfDocument(reader))
                    {
                        if (reader.IsOpenedWithFullPermission())
                        {
                            return PasswordType.None;
                        }
                        else
                        {
                            return PasswordType.Owner;
                        }
                    }
                }
            }
            catch (BadPasswordException)
            {
                return PasswordType.User;
            }
            catch (iText.IO.Exceptions.IOException)
            {
                return PasswordType.Unknown;
            }
        }

        /// <summary>
        /// Creates a preview image containing the first page of the PDF.
        /// </summary>
        /// <param name="pixelDensity">
        /// The pixel density (pixels per inch) of the PDF preview image.
        /// </param>
        /// <returns>The preview image in PNG format as a byte array.</returns>
        public byte[] CreatePreviewImage(decimal pixelDensity)
        {
            using (var image = new MagickImageCollection())
            {
                var settings = new MagickReadSettings
                {
                    Density = new Density(((double)pixelDensity)),
                    FrameIndex = 0,
                    FrameCount = 1
                };
                image.Read(pdfFile, settings);
                using (var stream = new MemoryStream())
                {
                    image.Write(stream, MagickFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Extracts all attached files from the PDF to a directory.
        /// </summary>
        /// <param name="attachedFilesType">
        /// The type of attached files in the PDF to extract.
        /// </param>
        /// <param name="directory">
        /// The DirectoryInfo object of the directory.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ExtractAllAttachedFiles(AttachedFilesType attachedFilesType, DirectoryInfo directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            switch (attachedFilesType)
            {
                case AttachedFilesType.Attachment:
                    foreach (var key in GetAllAttachments().ToArray())
                    {
                        File.WriteAllBytes(Path.Combine(directory.FullName, key.Key), key.Value);
                    }
                    break;
                case AttachedFilesType.EmbeddedFile:
                    foreach (var key in GetAllEmbeddedFiles().ToArray())
                    {
                        string dirPath = null;
                        string keyName = null;
                        if (key.Key.Contains(@"\"))
                        {
                            keyName = key.Key;
                            dirPath = Path.Combine(
                                directory.FullName,
                                Path.GetDirectoryName(keyName));
                            Directory.CreateDirectory(dirPath);
                        }
                        if (dirPath == null)
                        {
                            dirPath = directory.FullName;
                        }
                        if (keyName == null)
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

        /// <summary>
        /// Extracts all attached files from the PDF to a ZIP file.
        /// </summary>
        /// <param name="attachedFilesType">
        /// The type of attached files in the PDF to extract.
        /// </param>
        /// <param name="zipFile">
        /// The FileInfo object of the ZIP file. If the file referenced in the FileInfo object
        /// exists, it will be overwritten.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ExtractAllAttachedFiles(AttachedFilesType attachedFilesType, FileInfo zipFile)
        {
            if (zipFile == null)
            {
                throw new ArgumentNullException(nameof(zipFile));
            }
            switch (attachedFilesType)
            {
                case AttachedFilesType.Attachment:
                    GetAllAttachments().ToZipFile(zipFile);
                    break;
                case AttachedFilesType.EmbeddedFile:
                    GetAllEmbeddedFiles().ToZipFile(zipFile);
                    break;
            }
        }

        /// <summary>
        /// Gets a collection containing each page of the PDF as a TIFF image.
        /// </summary>
        /// <returns>The collection.</returns>
        public Collection<byte[]> GetAllPagesAsTiffImages()
        {
            var imageList = new Collection<byte[]>();
            using (var images = new MagickImageCollection())
            {
                var settings = new MagickReadSettings
                {
                    Density = new Density(600, 600),
                    Compression = CompressionMethod.LZW
                };
                images.Read(pdfFile, settings);
                foreach (var image in images)
                {
                    using (var output = new MemoryStream())
                    {
                        image.Write(output, MagickFormat.Tiff);
                        imageList.Add(output.ToArray());
                    }
                }
            }
            return imageList;
        }

        /// <summary>
        /// Gets text annotations from the PDF.
        /// </summary>
        /// <returns>The text annotations.</returns>
        public string GetTextAnnot()
        {
            using (var reader = new PdfReader(pdfFile))
            {
                var output = new StringBuilder();
                using (var document = new PdfDocument(reader))
                {
                    for (int pageCounter = 1, loopTo = document.GetNumberOfPages();
                        pageCounter <= loopTo; pageCounter++)
                    {
                        var page = document.GetPage(pageCounter);
                        var annotations = page.GetAnnotations();
                        foreach (var annotation in annotations)
                        {
                            var dict = annotation.GetPdfObject();
                            var text = dict.GetAsString(PdfName.Contents);
                            if (text != null)
                            {
                                output.AppendLine(text.ToUnicodeString());
                            }
                        }
                    }
                }
                return output.ToString();
            }
        }

        /// <summary>
        /// <para>
        /// Gets text from the PDF using the appropriate extraction strategy for the PDF page being
        /// processed.
        /// </para>
        /// <para>
        /// Text Extraction Strategy:
        /// Uses iText for text based PDF page except when page contains an invalid encoding due to
        /// iText's strict adherence to the PDF specification (ISO 32000) or when iText is unable
        /// to extract text from the page for another reason.
        /// </para>
        /// <para>
        /// OCR Text Extraction Strategy:
        /// Uses OCR for text based PDF when page is rejected by iText or when the page is
        /// "Image-only". This strategy will also be used when PDF page contains image data and
        /// ocrImageDataPages is true.
        /// </para>
        /// </summary>
        /// <param name="ocrImageDataPages">
        /// OCR each PDF page containing image data. (true or false)
        /// </param>
        /// <returns>The extracted text.</returns>
        public string GetText(bool ocrImageDataPages)
        {
            var pdfText = new StringBuilder();
            IPdfTextExtractionStrategy strategy;
            foreach (var pdf in new PdfFile(pdfFile).Split(tempDirectory))
            {
                string text = null;
                if (ocrImageDataPages && CheckForImageData())
                {
                    strategy = new PdfOcrTextExtractionStrategy();
                    text = strategy.GetText(pdf);
                }
                else
                {
                    strategy = new PdfTextExtractionStrategy();
                    text = strategy.GetText(pdf);
                    if (string.IsNullOrEmpty(text) || text.Trim().Length.Equals(0))
                    {
                        strategy = new PdfOcrTextExtractionStrategy();
                        text = strategy.GetText(pdf);
                    }
                }
                pdfText.Append(text);
                pdf.Delete();
            }
            return pdfText.ToString();
        }

        /// <summary>
        /// Splits PDF into separate one page PDF files.
        /// </summary>
        /// <param name="destination">The destination DirectoryInfo object.</param>
        /// <returns>The collection.</returns>
        /// <exception>ArgumentNullException</exception>
        public Collection<FileInfo> Split(DirectoryInfo destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            var pdfFiles = new Collection<FileInfo>();
            using (var reader = new PdfReader(pdfFile))
            {
                using (var document = new PdfDocument(reader))
                {
                    var splitter = new PdfFileSplitter(document, destination, pdfFile.Name);
                    foreach (var splittedDocument in splitter.SplitByPageCount(1))
                    {
                        splittedDocument.Close();
                    }
                }
            }
            foreach (var splitPdfFile in destination.GetFiles(string.Concat(
                Path.GetFileNameWithoutExtension(pdfFile.Name), "_*.pdf")))
            {
                pdfFiles.Add(splitPdfFile);
            }
            return pdfFiles;
        }

        /// <summary>
        /// Checks if PDF contains image data.
        /// </summary>
        /// <returns>true or false</returns>
        private bool CheckForImageData()
        {
            var result = false;
            using (var reader = new PdfReader(pdfFile))
            {
                using (var document = new PdfDocument(reader))
                {
                    for (int page = 1, loopTo = document.GetNumberOfPages(); page <= loopTo;
                        page++)
                    {
                        try
                        {
                            var imageDetector = new PdfImageDetector();
                            var canvasProcessor = new PdfCanvasProcessor(imageDetector);
                            canvasProcessor.ProcessPageContent(document.GetPage(1));
                            if (imageDetector.ImagesDetected)
                            {
                                result = true;
                            }
                        }
                        catch (ArgumentException)   // PDF contains an invalid encoding.
                        {
                            return result;
                        }
                        catch (InlineImageParseException)
                        {
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets a Dictionary array containing the name and contents of each attachment in the PDF.
        /// </summary>
        /// <returns>
        /// The Dictionary object.
        /// </returns>
        private Dictionary<string, byte[]> GetAllAttachments()
        {
            var attachments = new Dictionary<string, byte[]>();
            using (var reader = new PdfReader(pdfFile))
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
                            try
                            {
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
                            catch (NullReferenceException) { }
                        }
                    }
                    catch (NullReferenceException) { }
                }
            }
            return attachments;
        }

        /// <summary>
        /// Gets a Dictionary array containing the name and contents of each embedded file in the PDF.
        /// </summary>
        /// <returns>
        /// The Dictionary object.
        /// </returns>
        private Dictionary<string, byte[]> GetAllEmbeddedFiles()
        {
            var embeddedFiles = new Dictionary<string, byte[]>();
            using (var reader = new PdfReader(pdfFile))
            {
                using (var document = new PdfDocument(reader))
                {
                    for (int i = 1; i <= document.GetNumberOfPages(); i++)
                    {
                        var pdfArray = document.GetPage(i).GetPdfObject().GetAsArray(PdfName.Annots);
                        if (pdfArray != null)
                        {
                            for (int j = 0; j < pdfArray.Size(); j++)
                            {
                                var annot = pdfArray.GetAsDictionary(j);
                                if (PdfName.FileAttachment.Equals(annot.GetAsName(PdfName.Subtype)))
                                {
                                    var filespec = annot.GetAsDictionary(PdfName.FS);
                                    var refs = filespec.GetAsDictionary(PdfName.EF);
                                    foreach (PdfName key in refs.KeySet())
                                    {
                                        var filename = filespec.GetAsString(key).ToString().Replace(
                                            Path.AltDirectorySeparatorChar,
                                            Path.DirectorySeparatorChar).Substring(3);
                                        if (!embeddedFiles.ContainsKey(filename))
                                        {
                                            embeddedFiles.Add(filename, refs.GetAsStream(key).GetBytes());
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
