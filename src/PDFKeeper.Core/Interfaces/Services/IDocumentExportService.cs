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

namespace PDFKeeper.Core.Interfaces.Services
{
    /// <summary>
    /// Defines a service for exporting a document's PDF and metadata to the filesystem.
    /// </summary>
    public interface IDocumentExportService
    {
        /// <summary>
        /// Exports the specified document's PDF file and metadata in XML format to the target
        /// folder as <paramref name="baseExportFolderPath"/>\Author\Subject.
        /// </summary>
        /// <param name="documentId">
        /// The ID of the document to export.
        /// </param>
        /// <param name="baseExportFolderPath">
        /// The full path of the base folder where the exported PDF and metadata files
        /// will be written. The folder structure will be created if it does not already exist.
        /// </param>
        void ExportDocument(int documentId, string baseExportFolderPath);
    }
}
