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

using PDFKeeper.Core.Models;
using System.Data;

namespace PDFKeeper.Core.DataAccess.Repository
{
    public interface IDocumentRepository
    {
        /// <summary>
        /// Gets or sets if the documents list has changes? (true or false)
        /// </summary>
        bool DocumentsListHasChanges { get; set; }

        /// <summary>
        /// gets the length of the DOC_NOTES column in the DOCS table.
        /// </summary>
        /// <returns>
        /// The length of the column or 0 when the database platform is not Oracle.
        /// </returns>
        int GetNotesColumnDataLength();

        /// <summary>
        /// Gets a list of documents in the database by search term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>The DataTable object.</returns>
        DataTable GetListOfDocumentsBySearchTerm(string searchTerm);

        /// <summary>
        /// Gets a list of documents in the database by author, subject, category, and tax year.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="subject">The subject or null.</param>
        /// <param name="category">The category or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The DataTable object.</returns>
        DataTable GetListOfDocuments(string author, string subject, string category, string taxYear);

        /// <summary>
        /// Gets a list of documents in the database by date added.
        /// </summary>
        /// <param name="dateAdded">The date added in YYYY-MM-DD format.</param>
        /// <returns>The DataTable object.</returns>
        DataTable GetListOfDocumentsByDateAdded(string dateAdded);

        /// <summary>
        /// Gets a list of all flagged documents in the database.
        /// </summary>
        /// <returns>The DataTable object.</returns>
        DataTable GetListOfFlaggedDocuments();

        /// <summary>
        /// Gets a list of all documents in the database.
        /// </summary>
        /// <returns>The DataTable object.</returns>
        DataTable GetListOfDocuments();

        /// <summary>
        /// Gets authors by subject, category, and tax year from the database.
        /// </summary>
        /// <param name="subject">The subject or null.</param>
        /// <param name="category">The category or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The DataTable object.</returns>
        DataTable GetAuthors(string subject, string category, string taxYear);

        /// <summary>
        /// Gets subjects by author, category, and tax year from the database.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="category">The category or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The DataTable object.</returns>
        DataTable GetSubjects(string author, string category, string taxYear);

        /// <summary>
        /// Gets categories by author, subject, and tax year from the database.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="subject">The subject or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The DataTable object.</returns>
        DataTable GetCategories(string author, string subject, string taxYear);

        /// <summary>
        /// Gets tax years by author, subject, and category from the database.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="subject">The subject or null.</param>
        /// <param name="category">The category or null.</param>
        /// <returns>The DataTable object.</returns>
        DataTable GetTaxYears(string author, string subject, string category);

        /// <summary>
        /// Gets the document from the database.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="searchTerm">The specified search term or null.</param>
        /// <returns>The Document object.</returns>
        Document GetDocument(int id, string searchTerm);

        /// <summary>
        /// Inserts the document into the database.
        /// </summary>
        /// <param name="document">The Document object.</param>
        void InsertDocument(Document document);

        /// <summary>
        /// Updates the document in the database. Only the doc_title, doc_author, doc_subject,
        /// doc_notes, doc_category, doc_tax_year, doc_flag, doc_text_annotations, and doc_text
        /// columns will be updated.
        /// </summary>
        /// <param name="document">The Document object.</param>
        void UpdateDocument(Document document);
        
        /// <summary>
        /// Deletes the document from the database.
        /// </summary>
        /// <param name="id">The document ID.</param>
        void DeleteDocument(int id);

        /// <summary>
        /// Performs a test connection to the database. NotSupportedException will be thrown by the
        /// implementing class when the database platform is SQLite.
        /// </summary>
        void TestConnection();

        /// <summary>
        /// Resets the credential object used for connecting to the database. NotSupportedException
        /// will be thrown by the implementing class when the database platform does not implement
        /// a credential object.
        /// </summary>
        void ResetCredential();
    }
}
