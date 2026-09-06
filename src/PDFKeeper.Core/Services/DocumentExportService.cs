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
using PDFKeeper.Core.Rules.ExportPdfMetadata;
using System.IO;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IDocumentExportService"/> interface.
    /// </summary>
    public sealed class DocumentExportService : IDocumentExportService
    {
        private readonly IRule<ExportPdfMetadataRuleInput> exportPdfMetadataRule;
        private readonly IPdfFileCache pdfFileCache;
        private readonly IPdfMetadataService pdfMetadataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentExportService"/> class.
        /// </summary>
        /// <param name="exportPdfMetadataRule">
        /// The <see cref="IRule{ExportPdfMetadataRuleInput}"/> instance.
        /// </param>
        /// <param name="pdfFileCache">
        /// The <see cref="IPdfFileCache"/> instance.
        /// </param>
        /// <param name="pdfMetadataService">
        /// The <see cref="IPdfMetadataService"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public DocumentExportService(
            IRule<ExportPdfMetadataRuleInput> exportPdfMetadataRule,
            IPdfFileCache pdfFileCache,
            IPdfMetadataService pdfMetadataService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.exportPdfMetadataRule = exportPdfMetadataRule;
            this.pdfFileCache = pdfFileCache;
            this.pdfMetadataService = pdfMetadataService;
        }

        public void ExportDocument(int documentId, string baseExportFolderPath)
        {
            DocumentDto documentDto;

            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                documentDto = documentRepository.GetDocument(documentId, null);
            }

            pdfFileCache.StorePdf(documentId, documentDto.Pdf);
            var exportFolderPath = Path.Combine(
                baseExportFolderPath,
                documentDto.Author,
                documentDto.Subject).ReplaceInvalidPathChars();
            Directory.CreateDirectory(exportFolderPath);
            var pdfFileName = $"[{documentId}]{documentDto.Title}.pdf";
            pdfFileName = pdfFileName.ReplaceInvalidFileNameChars();
            var pdfPath = Path.Combine(exportFolderPath, pdfFileName);
            var xmlPath = Path.ChangeExtension(pdfPath, "xml");
            File.WriteAllBytes(pdfPath, documentDto.Pdf);
            var pdfMetadataDto = pdfMetadataService.Read(pdfPath);
            var ruleResult = exportPdfMetadataRule.Evaluate(
                new ExportPdfMetadataRuleInput(pdfMetadataDto, documentDto));

            if (ruleResult.ViolationFound)
            {
                pdfMetadataDto.Title = documentDto.Title;
                pdfMetadataDto.Author = documentDto.Author;
                pdfMetadataDto.Subject = documentDto.Subject;
                pdfMetadataDto.Keywords = documentDto.Keywords;
            }

            pdfMetadataDto.Notes = documentDto.Notes;
            pdfMetadataDto.Category = documentDto.Category;
            pdfMetadataDto.TaxYear = documentDto.TaxYear;
            pdfMetadataDto.Flag = documentDto.Flag;
            var tempPdfPath = pdfMetadataService.Write(pdfPath, pdfMetadataDto);
            var tempXmlPath = Path.ChangeExtension(tempPdfPath, "xml");
            File.Delete(pdfPath);
            File.Move(tempPdfPath, pdfPath);
            File.Move(tempXmlPath, xmlPath);
        }
    }
}
