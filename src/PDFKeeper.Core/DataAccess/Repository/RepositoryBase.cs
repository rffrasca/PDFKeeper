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

using System;
using System.Data;
using System.Linq;

namespace PDFKeeper.Core.DataAccess.Repository
{
    /// <summary>
    /// Provides a base class for repository implementations that manage PDF documents and related
    /// data access operations with caching support.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the connection string builder used for database connections.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the command used for executing queries against the database.
    /// </typeparam>
    /// <param name="documentCache">
    /// The document cache used for storing and retrieving PDF documents efficiently.
    /// </param>
    public abstract class RepositoryBase<T1, T2>(IDocumentCache documentCache)
    {
        protected T1 connStrBuilder;
        protected IDocumentCache documentCache = documentCache;

        /// <summary>
        /// Executes the specified query command and returns the results as a DataTable.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A DataTable containing the results of the query.</returns>
        protected abstract DataTable ExecuteQuery(T2 command);

        /// <summary>
        /// Calculates the hash value of the specified PDF document.
        /// </summary>
        /// <param name="documentId">The unique identifier of the PDF document.</param>
        /// <returns>A byte array containing the hash of the PDF document.</returns>
        protected abstract byte[] GetPdfHash(int documentId);

        /// <summary>
        /// Retrieves the PDF bytes of the specified document.
        /// </summary>
        /// <param name="documentId">The unique identifier of the PDF document.</param>
        /// <returns>A byte array containing the PDF bytes of the document.</returns>
        protected abstract byte[] GetPdfBytes(int documentId);

        /// <summary>
        /// Retrieves a PDF document with caching. It first retrieves the hash of the document from
        /// the database and compares it with the cached hash. If they match, it returns the cached
        /// PDF. If not, it retrieves the PDF from the database, updates the cache, and returns the
        /// new PDF.
        /// </summary>
        /// <param name="documentId">
        /// The ID of the document to retrieve.
        /// </param>
        /// <param name="getHashFromDb">
        /// A function to retrieve the hash of the document from the database.
        /// </param>
        /// <param name="getPdfFromDb">
        /// A function to retrieve the PDF from the database.
        /// </param>
        /// <returns>
        /// The PDF document as a byte array.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected byte[] GetPdfWithCache(
            int documentId,
            Func<int, byte[]> getHashFromDb,
            Func<int, byte[]> getPdfFromDb)
        {
            if (getHashFromDb is null)
            {
                throw new ArgumentNullException(nameof(getHashFromDb));
            }

            if (getPdfFromDb is null)
            {
                throw new ArgumentNullException(nameof(getPdfFromDb));
            }

            var dbHash = getHashFromDb(documentId);

            if (documentCache.TryGet(documentId, out var entry))
            {
                if (!entry.Hash.IsEmpty &&
                    !entry.Pdf.IsEmpty &&
                    entry.Hash.Span.SequenceEqual(dbHash))
                {
                    return entry.Pdf.ToArray();
                }
            }

            var pdf = getPdfFromDb(documentId);
            documentCache.Set(documentId, dbHash, pdf);
            return pdf;
        }

        /// <summary>
        /// Gets the search term snippets for the specified document ID and search term. This
        /// method is intended to be implemented by derived classes to provide database-specific
        /// logic for retrieving search term snippets.
        /// </summary>
        /// <param name="id">The unique identifier of the document.</param>
        /// <param name="searchTerm">The search term to find snippets for.</param>
        /// <returns>A string containing the search term snippets.</returns>
        protected abstract string GetSearchTermSnippets(int id, string searchTerm);

        /// <summary>
        /// Gets the access permissions for the documents table. This method is intended to be
        /// overridden by derived classes to provide database-specific logic for setting access
        /// permissions.
        /// </summary>
        protected virtual void GetDocsTableAccess()
        {
            DatabaseSession.SelectGranted = true;
            DatabaseSession.InsertGranted = true;
            DatabaseSession.UpdateGranted = true;
            DatabaseSession.DeleteGranted = true;
        }
    }
}
