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

using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Interfaces.Caching;
using PDFKeeper.Core.Interfaces.Rules;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Interfaces.Services.Pdf.TextExtraction;
using PDFKeeper.Core.Interfaces.Services.Upload;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Rules;
using System;
using System.Globalization;
using System.IO;

namespace PDFKeeper.Core.Services.Upload
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfUploadService"/> interface.
    /// </summary>
    public sealed class PdfUploadService : IPdfUploadService
    {
        private readonly IRule<string> filenameCharacterRule;
        private readonly IPdfAnnotationService pdfAnnotationService;
        private readonly IPdfFileCache pdfFileCache;
        private readonly IPdfListingService pdfListingService;
        private readonly IPdfMetadataService pdfMetadataService;
        private readonly IPdfSecurityService pdfSecurityService;
        private readonly IPdfTextExtractionService pdfTextExtractionService;
        private readonly IPdfUploadStagingService pdfUploadStagingService;
        private readonly IUploadProfileManager uploadProfileManager;
        private readonly string uploadFolderPath;
        private readonly string uploadRejectedFolderPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfUploadService"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <param name="filenameCharacterRule">
        /// The <see cref="IRule"/> instance that implements <see cref="FilenameCharacterRule"/>.
        /// </param>
        /// <param name="pdfAnnotationService">
        /// The <see cref="IPdfAnnotationService"/> instance.
        /// </param>
        /// <param name="pdfFileCache">
        /// The <see cref="IPdfFileCache"/> instance.
        /// </param>
        /// <param name="pdfListingService">
        /// The <see cref="IPdfListingService"/> instance.
        /// </param>
        /// <param name="pdfMetadataService">
        /// The <see cref="IPdfMetadataService"/> instance.
        /// </param>
        /// <param name="pdfSecurityService">
        /// The <see cref="IPdfSecurityService"/> instance.
        /// </param>
        /// <param name="pdfTextExtractionService">
        /// The <see cref="IPdfTextExtractionService"/> instance.
        /// </param>
        /// <param name="pdfUploadStagingService">
        /// The <see cref="IPdfUploadStagingService"/> instance.
        /// </param>
        /// <param name="uploadProfileManager">
        /// The <see cref="IUploadProfileManager"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="applicationFolderManager"/> is null.
        /// </exception>
        public PdfUploadService(
            IApplicationFolderManager applicationFolderManager,
            IRule<string> filenameCharacterRule,
            IPdfAnnotationService pdfAnnotationService,
            IPdfFileCache pdfFileCache,
            IPdfListingService pdfListingService,
            IPdfMetadataService pdfMetadataService,
            IPdfSecurityService pdfSecurityService,
            IPdfTextExtractionService pdfTextExtractionService,
            IPdfUploadStagingService pdfUploadStagingService,
            IUploadProfileManager uploadProfileManager)
        {
            if (applicationFolderManager is null)
            {
                throw new ArgumentNullException(nameof(applicationFolderManager));
            }

            this.filenameCharacterRule = filenameCharacterRule;
            this.pdfAnnotationService = pdfAnnotationService;
            this.pdfFileCache = pdfFileCache;
            this.pdfListingService = pdfListingService;
            this.pdfMetadataService = pdfMetadataService;
            this.pdfSecurityService = pdfSecurityService;
            this.pdfTextExtractionService = pdfTextExtractionService;
            this.pdfUploadStagingService = pdfUploadStagingService;
            this.uploadProfileManager = uploadProfileManager;
            uploadFolderPath = applicationFolderManager.GetOrCreateFolderPath(
                ApplicationFolder.Upload);
            uploadRejectedFolderPath = applicationFolderManager.GetOrCreateFolderPath(
                ApplicationFolder.UploadRejected);
        }

        public void Upload()
        {
            StagePdfs();
            UploadStagedPdfs();
        }

        /// <summary>
        /// Stages PDF files from the Upload folder into the UploadStaging folder.
        /// </summary>
        private void StagePdfs()
        {
            foreach (var pdfPath in pdfListingService.GetPdfPaths(ApplicationFolder.Upload))
            {
                new FileInfo(pdfPath).WaitWhileLocked();

                if (pdfSecurityService.GetPasswordType(pdfPath) != PdfPasswordType.None)
                {
                    MoveFileToUploadRejected(pdfPath);
                }
                else
                {
                    var ruleResult = filenameCharacterRule.Evaluate(Path.GetFileName(pdfPath));

                    if (!ruleResult.ViolationFound)
                    {
                        StagePdf(pdfPath);
                    }
                    else
                    {
                        MoveFileToUploadRejected(pdfPath);
                    }
                }
            }
        }

        /// <summary>
        /// Stages a single PDF file by applying an Upload Profile when available or
        /// validating metadata rules, then moves the file into the UploadStaging folder.
        /// </summary>
        /// <param name="pdfPath">
        /// The full path of the PDF file to be staged.
        /// </param>
        private void StagePdf(string pdfPath)
        {
            var folderName = pdfPath.Substring(uploadFolderPath.Length + 1);
            
            if (folderName.Equals(Path.GetFileName(pdfPath), StringComparison.Ordinal))
            {
                folderName = uploadFolderPath;
            }
            else
            {
                folderName = folderName.Substring(
                    0,
                    folderName.IndexOf(Path.DirectorySeparatorChar));
            }

            var pdfMetadataDto = pdfMetadataService.Read(pdfPath);
            
            if (uploadProfileManager.GetUploadProfile(folderName) != null)
            {
                var uploadProfile = uploadProfileManager.GetUploadProfile(folderName);

                if (!uploadProfile.Title.Equals(
                    UploadProfileToken.TitleToken,
                    StringComparison.Ordinal))
                {
                    if (uploadProfile.Title.Equals(
                        UploadProfileToken.DateToken,
                        StringComparison.Ordinal))
                    {
                        pdfMetadataDto.Title = UploadProfileTokenHelper.GetDate();
                    }
                    else if (uploadProfile.Title.Equals(
                        UploadProfileToken.DateTimeToken,
                        StringComparison.Ordinal))
                    {
                        pdfMetadataDto.Title = UploadProfileTokenHelper.GetDateTime();
                    }
                    else if (uploadProfile.Title.Equals(
                        UploadProfileToken.FileNameToken,
                        StringComparison.Ordinal))
                    {
                        pdfMetadataDto.Title = UploadProfileTokenHelper.GetFileName(
                            new FileInfo(pdfPath));
                    }
                    else
                    {
                        pdfMetadataDto.Title = uploadProfile.Title;
                    }
                }
                else
                {
                    pdfMetadataDto.Title ??= string.Empty;
                }

                if (!uploadProfile.Author.Equals(
                    UploadProfileToken.AuthorToken,
                    StringComparison.Ordinal))
                {
                    pdfMetadataDto.Author = uploadProfile.Author;
                }
                else
                {
                    pdfMetadataDto.Author ??= string.Empty;
                }

                if (!uploadProfile.Subject.Equals(
                    UploadProfileToken.SubjectToken,
                    StringComparison.Ordinal))
                {
                    pdfMetadataDto.Subject = uploadProfile.Subject;
                }
                else
                {
                    pdfMetadataDto.Subject ??= string.Empty;
                }

                if (!uploadProfile.Keywords.Equals(
                    UploadProfileToken.KeywordsToken,
                    StringComparison.Ordinal))
                {
                    pdfMetadataDto.Keywords = uploadProfile.Keywords;
                }
                else
                {
                    pdfMetadataDto.Keywords ??= string.Empty;
                }

                pdfMetadataDto.Category = uploadProfile.Category;
                pdfMetadataDto.Flag = Convert.ToInt32(uploadProfile.FlagDocument);
                pdfMetadataDto.TaxYear = uploadProfile.TaxYear;
                pdfMetadataDto.OcrPdfTextAndImageDataPages = 
                    uploadProfile.OcrPdfTextAndImageDataPages;

                try
                {
                    var modifiedPdfPath = pdfMetadataService.Write(pdfPath,pdfMetadataDto);
                    pdfUploadStagingService.StagePdf(modifiedPdfPath);
                    new FileInfo(pdfPath).DeleteToRecycleBin();
                    var xmlPath = Path.ChangeExtension(pdfPath, "xml");

                    if (File.Exists(xmlPath))
                    {
                        new FileInfo(xmlPath).DeleteToRecycleBin();
                    }
                }
                catch (Exception ex) when (
                    ex is iText.IO.Exceptions.IOException ||
                    ex is NullReferenceException)
                {
                    MoveFileToUploadRejected(pdfPath);
                }
            }
            else
            {
                if (new PdfMetadataRule(pdfMetadataDto.ToUploadProfile()).ViolationFound)
                {
                    MoveFileToUploadRejected(pdfPath);
                }
                else
                {
                    pdfUploadStagingService.StagePdf(pdfPath);
                }
            }
        }

        /// <summary>
        /// Moves the specified file into the UploadRejected folder, appending a GUID
        /// to the file name if a naming conflict occurs.
        /// </summary>
        /// <param name="filePath">
        /// The full path of the file to move.
        /// </param>
        private void MoveFileToUploadRejected(string filePath)
        {
            var destFilePath = Path.Combine(uploadRejectedFolderPath, Path.GetFileName(filePath));
            Directory.CreateDirectory(Path.GetDirectoryName(destFilePath));

            if (File.Exists(destFilePath))
            {
                destFilePath = new FileInfo(destFilePath).AppendGuidToFileName().FullName;
            }

            File.Move(filePath, destFilePath);
        }

        /// <summary>
        /// Uploads staged PDF files to the database. When updating an existing document, the
        /// cached PDF file will be deleted from the cache if it exists.
        /// </summary>
        private void UploadStagedPdfs()
        {
            foreach (var pdfPath in pdfListingService.GetPdfPaths(ApplicationFolder.UploadStaging))
            {
                var document = new Document();
                var pdfMetadataDto = pdfMetadataService.Read(pdfPath);
                document.Id = pdfMetadataDto.Id;
                document.Title = pdfMetadataDto.Title;
                document.Author = pdfMetadataDto.Author;
                document.Subject = pdfMetadataDto.Subject;
                document.Keywords = pdfMetadataDto.Keywords;

                if (document.Id == 0)
                {
                    document.Added = DateTime.Now.ToString(
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.CurrentCulture);
                    document.Notes = string.Empty;
                }

                document.Pdf = File.ReadAllBytes(pdfPath);
                document.Category = pdfMetadataDto.Category;
                document.Flag = pdfMetadataDto.Flag;
                document.TaxYear = pdfMetadataDto.TaxYear;
                document.TextAnnotations = pdfAnnotationService.GetTextAnnotations(pdfPath);
                document.Text = pdfTextExtractionService.ExtractText(
                    pdfPath,
                    pdfMetadataDto.OcrPdfTextAndImageDataPages);
                using (var documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    if (document.Id.Equals(0))
                    {
                        documentRepository.InsertDocument(document);
                    }
                    else
                    {
                        pdfFileCache.DeletePdf(document.Id);
                        documentRepository.UpdateDocument(document, true);
                    }
                }

                File.Delete(pdfPath);
                var xmlPath = Path.ChangeExtension(pdfPath, "xml");

                if (File.Exists(xmlPath))
                {
                    File.Delete(xmlPath);
                }
            }
        }
    }
}
