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

using PDFKeeper.Core.Models;
using System;
using System.Data;

namespace PDFKeeper.Core.DataAccess.Repository
{
    public interface IDocumentRepository : IDisposable
    {
        /// <summary>
        /// Gets <c>true</c> or <c>false</c> if Search Term Snippets are supported.
        /// </summary>
        bool SearchTermSnippetsSupported { get; }

        /// <summary>
        /// Gets the length of the DOC_NOTES column in the DOCS table.
        /// </summary>
        /// <returns>
        /// The length of the column or 0 when <see cref="DatabaseSession.PlatformName"/> is not
        /// <see cref="DatabaseSession.CompatiblePlatformName.Oracle"/>.
        /// </returns>
        int GetNotesColumnDataLength();

        /// <summary>
        /// Gets a list of documents in the database by search term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetListOfDocumentsBySearchTerm(string searchTerm);

        /// <summary>
        /// Gets a list of documents in the database by Author, Subject, Category, and Tax Year.
        /// </summary>
        /// <param name="author">The Author or <c>null</c>.</param>
        /// <param name="subject">The Subject or <c>null</c>.</param>
        /// <param name="category">The Category or <c>null</c>.</param>
        /// <param name="taxYear">The Tax Year or <c>null</c>.</param>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetListOfDocuments(string author, string subject, string category, string taxYear);

        /// <summary>
        /// Gets a list of documents in the database by Date Added.
        /// </summary>
        /// <param name="dateAdded">The Date Added in YYYY-MM-DD format.</param>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetListOfDocumentsByDateAdded(string dateAdded);

        /// <summary>
        /// Gets a list of all flagged documents in the database.
        /// </summary>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetListOfFlaggedDocuments();

        /// <summary>
        /// Gets a list of all documents in the database.
        /// </summary>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetListOfDocuments();

        /// <summary>
        /// Gets Authors by Subject, Category, and Tax Year from the database.
        /// </summary>
        /// <param name="subject">The Subject or <c>null</c>.</param>
        /// <param name="category">The Category or <c>null</c>.</param>
        /// <param name="taxYear">The Tax Year or <c>null</c>.</param>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetAuthors(string subject, string category, string taxYear);

        /// <summary>
        /// Gets Subjects by Author, Category, and Tax Year from the database.
        /// </summary>
        /// <param name="author">The Author or <c>null</c>.</param>
        /// <param name="category">The Category or <c>null</c>.</param>
        /// <param name="taxYear">The Tax Year or <c>null</c>.</param>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetSubjects(string author, string category, string taxYear);

        /// <summary>
        /// Gets Categories by Author, Subject, and Tax Year from the database.
        /// </summary>
        /// <param name="author">The Author or <c>null</c>.</param>
        /// <param name="subject">The Subject or <c>null</c>.</param>
        /// <param name="taxYear">The Tax Year or <c>null</c>.</param>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetCategories(string author, string subject, string taxYear);

        /// <summary>
        /// Gets Tax Years by Author, Subject, and Category from the database.
        /// </summary>
        /// <param name="author">The Author or <c>null</c>.</param>
        /// <param name="subject">The Subject or <c>null</c>.</param>
        /// <param name="category">The Category or <c>null</c>.</param>
        /// <returns>The <see cref="DataTable"/> object.</returns>
        /// <exception cref="DatabaseException"></exception>
        DataTable GetTaxYears(string author, string subject, string category);

        /// <summary>
        /// Gets the document from the database.
        /// </summary>
        /// <param name="id">
        /// The document ID.
        /// </param>
        /// <param name="searchTerm">
        /// The specified search term or <c>null</c>.
        /// </param>
        /// <param name="includePdf">
        /// <c>true</c> or <c>false</c> to include the PDF file contents.
        /// </param>
        /// <returns>
        /// The <see cref="Document"/> object with or without the PDF file contents.
        /// </returns>
        /// <exception cref="DatabaseException"></exception>
        Document GetDocument(int id, string searchTerm, bool includePdf = false);

        /// <summary>
        /// Inserts the <see cref="Document"/> into the database.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DatabaseException"></exception>
        void InsertDocument(Document document);

        /// <summary>
        /// Updates the specified <see cref="Document"/> in the database.
        /// <para>
        /// By default, only the doc_title, doc_author, doc_subject, doc_notes, doc_category,
        /// doc_tax_year, doc_flag, doc_text_annotations, and doc_text columns will be updated.
        /// The doc_pdf column will only be updated when <paramref name="updatePdf"/> is
        /// <see langword="true"/>.
        /// </para>
        /// </summary>
        /// <param name="document">
        /// The <see cref="Document"/> object containing the updated data. This parameter cannot be
        /// <see langword="null"/>.
        /// </param>
        /// <param name="updatePdf">
        /// A value indicating whether the PDF content of the document should also be updated. If
        /// <see langword="true"/>, the PDF content will be included in the update; otherwise, it
        /// will be ignored.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DatabaseException"></exception>
        void UpdateDocument(Document document, bool updatePdf = false);

        /// <summary>
        /// Deletes the <see cref="Document"/> from the database.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <exception cref="DatabaseException"></exception>
        void DeleteDocument(int id);

        /// <summary>
        /// Performs a test connection to the database.
        /// <para>
        /// NotSupportedException will be thrown by the implementing class when
        /// <see cref="DatabaseSession.PlatformName"/> is
        /// <see cref="DatabaseSession.CompatiblePlatformName.Sqlite"/>.
        /// </para>
        /// </summary>
        /// <exception cref="DatabaseException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        void TestConnection();

        /// <summary>
        /// Resets the credential object used for connecting to the database.
        /// <para>
        /// NotSupportedException will be thrown by the implementing class when
        /// <see cref="DatabaseSession.PlatformName"/> does not implement a <c>Credential</c>
        /// object.
        /// </para>
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        void ResetCredential();

        /// <summary>
        /// Creates the database.
        /// <para>
        /// NotSupportedException will be thrown by the implementing class when
        /// <see cref="DatabaseSession.PlatformName"/> is not
        /// <see cref="DatabaseSession.CompatiblePlatformName.Sqlite" />.
        /// </para>
        /// </summary>
        /// <exception cref="DatabaseException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        void CreateDatabase();

        /// <summary>
        /// Compacts the database.
        /// <para>
        /// NotSupportedException will be thrown by the implementing class when
        /// <see cref="DatabaseSession.PlatformName"/> is not
        /// <see cref="DatabaseSession.CompatiblePlatformName.Sqlite"/>.
        /// </para>
        /// </summary>
        /// <exception cref="DatabaseException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        void CompactDatabase();

        /// <summary>
        /// Upgrades the database.
        /// <para>
        /// NotSupportedException will be thrown by the implementing class when
        /// <see cref="DatabaseSession.PlatformName"/> is not
        /// <see cref="DatabaseSession.CompatiblePlatformName.Sqlite"/>.
        /// </para>
        /// </summary>
        /// <exception cref="DatabaseException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        void UpgradeDatabase();
    }
}
