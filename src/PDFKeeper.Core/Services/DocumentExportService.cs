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
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Interfaces.Caching;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Rules;
using System.IO;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IDocumentExportService"/> interface.
    /// </summary>
    public sealed class DocumentExportService : IDocumentExportService
    {
        private readonly IPdfFileCache pdfFileCache;
        private readonly IPdfMetadataService pdfMetadataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentExportService"/> class.
        /// </summary>
        /// <param name="pdfFileCache">
        /// The <see cref="IPdfFileCache"/> instance.
        /// </param>
        /// <param name="pdfMetadataService">
        /// The <see cref="IPdfMetadataService"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public DocumentExportService(
            IPdfFileCache pdfFileCache,
            IPdfMetadataService pdfMetadataService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.pdfFileCache = pdfFileCache;
            this.pdfMetadataService = pdfMetadataService;
        }

        public void ExportDocument(int documentId, string baseExportFolderPath)
        {
            Document document;

            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                document = documentRepository.GetDocument(documentId, null);
            }

            pdfFileCache.StorePdf(documentId, document.Pdf);
            var exportFolderPath = Path.Combine(
                baseExportFolderPath,
                document.Author,
                document.Subject).ReplaceInvalidPathChars();
            Directory.CreateDirectory(exportFolderPath);
            var pdfFileName = $"[{documentId}]{document.Title}.pdf";
            pdfFileName = pdfFileName.ReplaceInvalidFileNameChars();
            var pdfPath = Path.Combine(exportFolderPath, pdfFileName);
            var xmlPath = Path.ChangeExtension(pdfPath, "xml");
            File.WriteAllBytes(pdfPath, document.Pdf);
            var pdfMetadataDto = pdfMetadataService.Read(pdfPath);
            var rule = new ExportPdfMetadataRule(pdfMetadataDto, document);

            if (rule.ViolationFound)
            {
                pdfMetadataDto.Title = document.Title;
                pdfMetadataDto.Author = document.Author;
                pdfMetadataDto.Subject = document.Subject;
                pdfMetadataDto.Keywords = document.Keywords;
            }

            pdfMetadataDto.Notes = document.Notes;
            pdfMetadataDto.Category = document.Category;
            pdfMetadataDto.TaxYear = document.TaxYear;
            pdfMetadataDto.Flag = document.Flag;
            var tempPdfPath = pdfMetadataService.Write(pdfPath, pdfMetadataDto);
            var tempXmlPath = Path.ChangeExtension(tempPdfPath, "xml");
            File.Delete(pdfPath);
            File.Move(tempPdfPath, pdfPath);
            File.Move(tempXmlPath, xmlPath);
        }
    }
}
