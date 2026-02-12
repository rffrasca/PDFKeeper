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
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.FileIO.PDF;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Rules;
using System;
using System.IO;
using System.Windows.Input;

namespace PDFKeeper.Core.Commands
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExportDocumentCommand"/> class that that
    /// exports the PDF and external metadata (XML) when <see cref="Execute(object)"/> is
    /// invoked.
    /// <para>
    /// When invoking <see cref="Execute(object)"/>, set parameter to <c>null</c>.
    /// </para>
    /// </summary>
    /// <param name="id">The document ID.</param>
    /// <param name="exportDirectory">The export <see cref="DirectoryInfo"/> object.</param>
    /// <param name="fileCache">The export <see cref="FileCache"/> instance.</param>
    public class ExportDocumentCommand(
        int id,
        DirectoryInfo exportDirectory,
        FileCache fileCache) : ICommand
    {
        private readonly int id = id;
        private readonly DirectoryInfo exportDirectory = exportDirectory;
        private readonly FileCache fileCache = fileCache;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(object parameter)
        {
            Document document;
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                document = documentRepository.GetDocument(id, null, true);
            }
            
            fileCache.AddPdf(id, document.Pdf);
            var filesExportDirectoryPath = Path.Combine(
                exportDirectory.FullName,
                document.Author,
                document.Subject).ReplaceInvalidPathChars();
            Directory.CreateDirectory(filesExportDirectoryPath);
            var pdfFileName = string.Concat(
                "[",
                id,
                "]",
                document.Title,
                ".pdf").ReplaceInvalidFileNameChars();
            var pdfFile = new PdfFile(
                new FileInfo(
                    Path.Combine(
                        filesExportDirectoryPath,
                        pdfFileName)));
            var xmlFile = pdfFile.ChangeExtension("xml");
            File.WriteAllBytes(pdfFile.FullName, document.Pdf);
            var pdfMetadata = new PdfMetadata(pdfFile);
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
