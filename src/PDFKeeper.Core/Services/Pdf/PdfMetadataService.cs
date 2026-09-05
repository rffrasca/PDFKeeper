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
using iText.Kernel.XMP;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Interfaces.Services.Security;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Rules;
using PDFKeeper.Core.Serializers;
using System;
using System.IO;
using System.Security;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfMetadataService"/> interface.
    /// </summary>
    public sealed class PdfMetadataService : IPdfMetadataService
    {
        private readonly IApplicationFolderManager applicationFolderManager;
        private readonly IPdfSecurityService pdfSecurityService;
        private readonly ISecureDataService secureDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfMetadataService"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <param name="pdfSecurityService">
        /// The <see cref="IPdfSecurityService"/> instance.
        /// </param>
        /// <param name="secureDataService">
        /// The <see cref="ISecureDataService"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfMetadataService(
            IApplicationFolderManager applicationFolderManager,
            IPdfSecurityService pdfSecurityService,
            ISecureDataService secureDataService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.applicationFolderManager = applicationFolderManager;
            this.pdfSecurityService = pdfSecurityService;
            this.secureDataService = secureDataService;
        }

        public PdfMetadataDto Read(string pdfPath, SecureString pdfOwnerPassword = null)
        {
            IPinnedBytes pinnedBytes = null;
            var pdfMetadataDto = new PdfMetadataDto();

            try
            {
                if (pdfOwnerPassword != null)
                {
                    pinnedBytes = secureDataService.ToPinnedByteArray(pdfOwnerPassword);
                }                

                using (var pdfReader = CreatePdfReader(pdfPath, pinnedBytes))
                {
                    using (var pdfDocument = new PdfDocument(pdfReader))
                    {
                        var documentInfo = pdfDocument.GetDocumentInfo();
                        pdfMetadataDto.Title = documentInfo.GetTitle();
                        pdfMetadataDto.Author = documentInfo.GetAuthor();
                        pdfMetadataDto.Subject = documentInfo.GetSubject();
                        pdfMetadataDto.Keywords = documentInfo.GetKeywords();
                    }
                }
            }
            finally
            {
                pinnedBytes?.Dispose();
            }

            var xmlPath = Path.ChangeExtension(pdfPath, "xml");

            if (File.Exists(xmlPath))
            {
                var pdfExternalMetadata = XmlSerializer.DeserializeFromFile<
                    PdfExternalMetadata>(xmlPath);
                pdfMetadataDto.Id = pdfExternalMetadata.Id;
                pdfMetadataDto.Notes = pdfExternalMetadata.Notes;
                pdfMetadataDto.Category = pdfExternalMetadata.Category;
                pdfMetadataDto.TaxYear = pdfExternalMetadata.TaxYear;
                pdfMetadataDto.Flag = pdfExternalMetadata.Flag;
                pdfMetadataDto.OcrPdfTextAndImageDataPages = 
                    pdfExternalMetadata.OcrPdfTextAndImageDataPages;
            }

            return pdfMetadataDto;
        }

        public string Write(
            string pdfPath,
            PdfMetadataDto pdfMetadataDto,
            SecureString pdfOwnerPassword = null)
        {
            if (pdfMetadataDto is null)
            {
                throw new ArgumentNullException(nameof(pdfMetadataDto));
            }

            IPinnedBytes pinnedBytes = null;
            var tempPdfPath = Path.Combine(
                applicationFolderManager.GetOrCreateFolderPath(ApplicationFolder.Temp),
                Path.GetFileName(pdfPath));

            try
            {
                if (pdfOwnerPassword != null)
                {
                    pinnedBytes = secureDataService.ToPinnedByteArray(pdfOwnerPassword);
                }

                using (var pdfReader = CreatePdfReader(pdfPath, pinnedBytes))
                {
                    using (var pdfWriter = new PdfWriter(tempPdfPath))
                    {
                        using (var pdfDocument = new PdfDocument(pdfReader, pdfWriter))
                        {
                            var documentInfo = pdfDocument.GetDocumentInfo();
                            documentInfo.SetTitle(pdfMetadataDto.Title);
                            documentInfo.SetAuthor(pdfMetadataDto.Author);
                            documentInfo.SetSubject(pdfMetadataDto.Subject);
                            documentInfo.SetKeywords(pdfMetadataDto.Keywords);
                            var rule = new PdfMetadataRule(documentInfo);

                            if (rule.ViolationFound)
                            {
                                throw new NullReferenceException(rule.ViolationMessage);
                            }
                            else
                            {
                                pdfDocument.SetXmpMetadata(XMPMetaFactory.Create());
                            }
                        }
                    }
                }
            }
            finally
            {
                pinnedBytes?.Dispose();
            }

            var pdfExternalMetadata = new PdfExternalMetadata
            {
                Id = pdfMetadataDto.Id,
                Notes = pdfMetadataDto.Notes,
                Category = pdfMetadataDto.Category,
                TaxYear = pdfMetadataDto.TaxYear,
                Flag = pdfMetadataDto.Flag,
                OcrPdfTextAndImageDataPages = pdfMetadataDto.OcrPdfTextAndImageDataPages
            };
            
            XmlSerializer.SerializeToFile(
                pdfExternalMetadata,
                Path.ChangeExtension(tempPdfPath, "xml"));

            return tempPdfPath;
        }

        /// <summary>
        /// Creates a <see cref="PdfReader"/> instance for the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file whose metadata will be updated.
        /// </param>
        /// <param name="pdfOwnerPassword">
        /// An optional owner password as a <see cref="IPinnedBytes"/> instance used to open
        /// password‑protected PDFs. If the PDF is not password‑protected, this value may be
        /// <c>null</c>.
        /// </param>
        /// <returns>
        /// A <see cref="PdfReader"/> instance for the specified PDF file.
        /// </returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="pdfOwnerPassword"/> is provided but is invalid
        /// for the specified PDF file.
        /// </exception>
        private PdfReader CreatePdfReader(string pdfPath, IPinnedBytes pdfOwnerPassword = null)
        {
            if (pdfOwnerPassword is null)
            {
                return new PdfReader(pdfPath);
            }
            else
            {
                if (!pdfSecurityService.ValidatePdfOwnerPassword(
                    pdfPath,
                    pdfOwnerPassword.GetBytes()))
                {
                    throw new UnauthorizedAccessException(Resources.PdfOwnerPasswordIncorrect);
                }

                var readerProperties = new ReaderProperties();
                readerProperties.SetPassword(pdfOwnerPassword.GetBytes());
                return new PdfReader(pdfPath, readerProperties);
            }
        }
    }
}
