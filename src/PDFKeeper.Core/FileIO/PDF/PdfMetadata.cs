// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Kernel.XMP;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Rules;
using System;
using System.IO;
using System.Security;

namespace PDFKeeper.Core.FileIO.PDF
{
    public class PdfMetadata : IPdfMetadata, IPdfExternalMetadata
    {
        private readonly PdfFile pdfFile;
        private readonly SecureString pdfOwnerPassword;
        private readonly FileInfo xmlFile;

        /// <summary>
        /// Initializes a new instance of the PdfMetadata class.
        /// </summary>
        /// <param name="pdfFile">The PDF file object.</param>
        /// <param name="pdfOwnerPassword">
        /// The PDF owner password SecureString object or null.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public PdfMetadata(PdfFile pdfFile, SecureString pdfOwnerPassword)
        {
            this.pdfFile = pdfFile ?? throw new ArgumentNullException(nameof(pdfFile));
            this.pdfOwnerPassword = pdfOwnerPassword;
            xmlFile = new FileInfo(pdfFile.FullName).ChangeExtension("xml");
            if (pdfOwnerPassword != null)
            {
                if (ValidatePdfOwnerPassword().Equals(false))
                {
                    throw new ArgumentException(Resources.PdfOwnerPasswordInvalid,
                        nameof(pdfOwnerPassword));
                }
            }
            GetMetadata();
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Keywords { get; set; }
        public string Notes { get; set; }
        public string Category { get; set; }
        public int Flag { get; set; }
        public string TaxYear { get; set; }
        public bool OcrPdfTextAndImageDataPages { get; set; }

        /// <summary>
        /// Exports PdfMetadata object properties to an UploadProfile object.
        /// </summary>
        /// <returns>The UploadProfile object.</returns>
        public UploadProfile ExportUploadProfile()
        {
            var uploadProfile = new UploadProfile
            {
                Title = Title,
                Author = Author,
                Subject = Subject,
                Keywords = Keywords,
                Category = Category,
                FlagDocument = Convert.ToBoolean(Flag),
                TaxYear = TaxYear,
                OcrPdfTextAndImageDataPages = OcrPdfTextAndImageDataPages
            };
            return uploadProfile;
        }

        /// <summary>
        /// Imports an UploadProfile object into the PdfMetadata object properties.
        /// </summary>
        /// <param name="uploadProfile">The UploadProfile object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ImportUploadProfile(UploadProfile uploadProfile)
        {
            if (uploadProfile == null)
            {
                throw new ArgumentNullException(nameof(uploadProfile));
            }
            Title = uploadProfile.Title;
            Author = uploadProfile.Author;
            Subject = uploadProfile.Subject;
            Keywords = uploadProfile.Keywords;
            Category = uploadProfile.Category;
            Flag = Convert.ToInt32(uploadProfile.FlagDocument);
            TaxYear = uploadProfile.TaxYear;
            OcrPdfTextAndImageDataPages = uploadProfile.OcrPdfTextAndImageDataPages;
        }

        /// <summary>
        /// Writes the target PDF in the application Temp directory with the contents from the
        /// source PDF with the values of the Title, Author, Subject, and Keywords properties
        /// applied; and then calls WriteXml to Write the external metadata for the PDF (Notes,
        /// Category, TaxYear, FlagDocument, and OcrPdfTextAndImageDataPages) to the XML file with
        /// the same name and in the same directory as the target PDF.
        /// </summary>
        /// <returns>The target PDF FileInfo object.</returns>
        public FileInfo Write()
        {
            var targetPdfFile = new FileInfo(Path.Combine(new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.Temp).FullName,
                Path.GetFileName(pdfFile.FullName)));
            if (pdfOwnerPassword == null)
            {
                using (var reader = new PdfReader(pdfFile.FullName))
                {
                    WritePdf(reader, targetPdfFile);
                }
            }
            else
            {
                using (var reader = new PdfReader(pdfFile.FullName,
                    new ReaderProperties().SetPassword(System.Text.Encoding.ASCII.GetBytes(
                        pdfOwnerPassword.Decrypt()))))
                {
                    WritePdf(reader, targetPdfFile);
                }
            }
            WriteXml(targetPdfFile);
            return targetPdfFile;
        }

        /// <summary>
        /// Validates the Owner password for the PDF.
        /// </summary>
        /// <returns>Is the Owner Password valid? (true or false)</returns>
        private bool ValidatePdfOwnerPassword()
        {
            try
            {
                using (var reader = new PdfReader(pdfFile.FullName,
                    new ReaderProperties().SetPassword(System.Text.Encoding.ASCII.GetBytes(
                        pdfOwnerPassword.Decrypt()))))
                {
                    using (var document = new PdfDocument(reader)) { }
                }
                return true;
            }
            catch (BadPasswordException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the metadata from the PDF and corresponding XML file.
        /// </summary>
        private void GetMetadata()
        {
            if (pdfOwnerPassword == null)
            {
                using (var reader = new PdfReader(pdfFile.FullName))
                {
                    GetInfoMetadata(reader);
                }
            }
            else
            {
                using (var reader = new PdfReader(pdfFile.FullName,
                    new ReaderProperties().SetPassword(System.Text.Encoding.ASCII.GetBytes(
                        pdfOwnerPassword.Decrypt()))))
                {
                    GetInfoMetadata(reader);
                }
            }
            GetExternalMetadata();
        }

        /// <summary>
        /// Gets the information metadata from the PDF.
        /// </summary>
        /// <param name="pdfReader">The PdfReader object.</param>
        private void GetInfoMetadata(PdfReader pdfReader)
        {
            using (var document = new PdfDocument(pdfReader))
            {
                var info = document.GetDocumentInfo();
                Title = info.GetTitle();
                Author = info.GetAuthor();
                Subject = info.GetSubject();
                Keywords = info.GetKeywords();
            }
        }

        /// <summary>
        /// Gets the external metadata for the PDF from the XML with the same name and in the same
        /// directory as the PDF.
        /// </summary>
        private void GetExternalMetadata()
        {
            if (xmlFile.Exists)
            {
                var metaData = XmlSerializer.Deserialize<PdfExternalMetadata>(xmlFile);
                Notes = metaData.Notes;
                Category = metaData.Category;
                TaxYear = metaData.TaxYear;
                Flag = metaData.Flag;
                OcrPdfTextAndImageDataPages = metaData.OcrPdfTextAndImageDataPages;
            }
        }

        /// <summary>
        /// Writes the target PDF with the contents of the PDF with the values from the Title,
        /// Author, Subject, and Keywords properties applied.
        /// </summary>
        /// <param name="pdfReader">The PdfReader object.</param>
        /// <param name="targetPdfFile">The target PDF FileInfo object.</param>
        /// <exception cref="NullReferenceException"></exception>
        private void WritePdf(PdfReader pdfReader, FileInfo targetPdfFile)
        {
            using (var writer = new PdfWriter(targetPdfFile))
            {
                using (var document = new PdfDocument(pdfReader, writer))
                {
                    var info = document.GetDocumentInfo();
                    info.SetTitle(Title);
                    info.SetAuthor(Author);
                    info.SetSubject(Subject);
                    info.SetKeywords(Keywords);
                    var rule = new PdfMetadataRule(info);
                    if (rule.ViolationFound)
                    {
                        throw new NullReferenceException(rule.ViolationMessage);
                    }
                    else
                    {
                        document.SetXmpMetadata(XMPMetaFactory.Create());
                    }
                }
            }
        }

        /// <summary>
        /// Writes the external metadata for the PDF (Notes, Category, TaxYear, FlagDocument,
        /// and OcrPdfTextAndImageDataPages) to the XML file with the same name and in the same
        /// directory as the target PDF.
        /// </summary>
        /// <param name="targetPdfFile">The target PDF FileInfo object.</param>
        private void WriteXml(FileInfo targetPdfFile)
        {
            var metaData = new PdfExternalMetadata
            {
                Notes = Notes,
                Category = Category,
                TaxYear = TaxYear,
                Flag = Flag,
                OcrPdfTextAndImageDataPages = OcrPdfTextAndImageDataPages
            };
            XmlSerializer.Serialize(metaData, targetPdfFile.ChangeExtension("xml"));
        }
    }
}
