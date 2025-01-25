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

using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.FileIO.PDF;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Rules;
using System.IO;

namespace PDFKeeper.Core.Commands
{
    public class ExportDocumentCommand : ICommand
    {
        private readonly int id;
        private readonly DirectoryInfo exportTargetDirectory;
        private readonly FileCache fileCache;

        /// <summary>
        /// Initializes a new instance of the <c>ExportDocumentCommand</c> class that exports the
        /// PDF and external metadata (XML) when executed.
        /// </summary>
        /// <param name="id">
        /// The document ID.
        /// </param>
        /// <param name="exportTargetDirectory">
        /// The export target <c>DirectoryInfo</c> object.
        /// </param>
        /// <param name="fileCache">
        /// The <c>FileCache</c> instance.
        /// </param>
        public ExportDocumentCommand(
            int id,
            DirectoryInfo exportTargetDirectory,
            FileCache fileCache)
        {
            this.id = id;
            this.exportTargetDirectory = exportTargetDirectory;
            this.fileCache = fileCache;
        }

        public void Execute()
        {
            Document document;
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                document = documentRepository.GetDocument(id, null, true);
            }
            fileCache.AddPdf(id, document.Pdf);
            var authorDirectory = new DirectoryInfo(
                Path.Combine(
                    exportTargetDirectory.FullName,
                    document.Author));
            var subjectDirectory = new DirectoryInfo(
                Path.Combine(
                    authorDirectory.FullName,
                    document.Subject));
            authorDirectory.Create();
            subjectDirectory.Create();
            var pdfFile = new PdfFile(
                new FileInfo(
                    Path.Combine(
                        subjectDirectory.FullName,
                        string.Concat("[",
                        id,
                        "]",
                        document.Title,
                        ".pdf"))));
            var xmlFile = pdfFile.ChangeExtension("xml");
            File.WriteAllBytes(pdfFile.FullName, document.Pdf);
            var pdfMetadata = new PdfMetadata(pdfFile, null);
            var rule = new ExportPdfMetadataRule(pdfMetadata, document);
            if (rule.ViolationFound)
            {
                pdfMetadata.Title = document.Title;
                pdfMetadata.Author = document.Author;
                pdfMetadata.Subject = document.Subject;
                pdfMetadata.Keywords = document.Keywords;
            }
            pdfMetadata.Notes = document.Notes;
            pdfMetadata.Category = document.Category;
            pdfMetadata.TaxYear = document.TaxYear;
            pdfMetadata.Flag = document.Flag;
            var tempPdfFile = pdfMetadata.Write();
            var tempXmlFile = tempPdfFile.ChangeExtension("xml");
            pdfFile.Delete();
            tempPdfFile.MoveTo(pdfFile.FullName);
            tempXmlFile.MoveTo(xmlFile.FullName);
        }
    }
}
