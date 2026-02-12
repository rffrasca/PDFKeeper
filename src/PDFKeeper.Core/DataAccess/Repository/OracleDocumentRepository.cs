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

using Oracle.ManagedDataAccess.Client;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using System;
using System.Data;
using System.Globalization;
using System.IO;

namespace PDFKeeper.Core.DataAccess.Repository
{
    public class OracleDocumentRepository : RepositoryBase<
        OracleConnectionStringBuilder,
        OracleCommand>,
        IDocumentRepository
    {
        private static OracleCredential oracleCredential;
        private bool disposedValue;

        public OracleDocumentRepository()
        {
            connStrBuilder = new OracleConnectionStringBuilder
            {
                DataSource = DatabaseSession.DataSource,
                ConnectionTimeout = 60
            };

            if (oracleCredential is null)
            {
                oracleCredential = new OracleCredential(
                    DatabaseSession.UserName,
                    DatabaseSession.Password);

                if (DatabaseSession.OracleWalletPath != null)
                {
                    if (OracleConfiguration.WalletLocation.Length.Equals(0))
                    {
                        OracleConfiguration.TnsAdmin = DatabaseSession.OracleWalletPath;
                        OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
                    }
                }
            }
        }

        public bool SearchTermSnippetsSupported => true;

        public int GetNotesColumnDataLength()
        {
            var sql = "select data_length from all_tab_columns " +
                "where table_name='DOCS' and column_name='DOC_NOTES'";

            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                using var reader = command.ExecuteReader();
                reader.Read();
                return reader.GetInt32(0);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocumentsBySearchTerm(string searchTerm)
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs " +
                "where (contains(doc_dummy,:doc_dummy)) > 0";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_dummy", searchTerm);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocuments(string author, string subject, string category, string taxYear)
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs " +
                "where (:doc_author is NULL or doc_author = :doc_author) " +
                "and (:doc_subject is NULL or doc_subject = :doc_subject) " +
                "and (:doc_category is NULL or doc_category = :doc_category) " +
                "and (:doc_tax_year is NULL or doc_tax_year = :doc_tax_year) ";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_author", author);
                command.Parameters.Add("doc_subject", subject);
                command.Parameters.Add("doc_category", category);
                command.Parameters.Add("doc_tax_year", taxYear);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocumentsByDateAdded(string dateAdded)
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs " +
                "where doc_added like :doc_added || '%'";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_added", dateAdded);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfFlaggedDocuments()
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs where doc_flag = 1";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocuments()
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetAuthors(string subject, string category, string taxYear)
        {
            var sql = "select doc_author from docs " +
                "where (:doc_subject is NULL or doc_subject = :doc_subject) " +
                "and (:doc_category is NULL or doc_category = :doc_category) " +
                "and (:doc_tax_year is NULL or doc_tax_year = :doc_tax_year) " +
                "group by doc_author";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_subject", subject);
                command.Parameters.Add("doc_category", category);
                command.Parameters.Add("doc_tax_year", taxYear);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetSubjects(string author, string category, string taxYear)
        {
            var sql = "select doc_subject from docs " +
                "where (:doc_author is NULL or doc_author = :doc_author) " +
                "and (:doc_category is NULL or doc_category = :doc_category) " +
                "and (:doc_tax_year is NULL or doc_tax_year = :doc_tax_year) " +
                "group by doc_subject";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_author", author);
                command.Parameters.Add("doc_category", category);
                command.Parameters.Add("doc_tax_year", taxYear);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetCategories(string author, string subject, string taxYear)
        {
            var sql = "select doc_category from docs " +
                "where (:doc_author is NULL or doc_author = :doc_author) " +
                "and (:doc_subject is NULL or doc_subject = :doc_subject) " +
                "and (:doc_tax_year is NULL or doc_tax_year = :doc_tax_year) " +
                "group by doc_category";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_author", author);
                command.Parameters.Add("doc_subject", subject);
                command.Parameters.Add("doc_tax_year", taxYear);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetTaxYears(string author, string subject, string category)
        {
            var sql = "select doc_tax_year from docs " +
                "where (:doc_author is NULL or doc_author = :doc_author) " +
                "and (:doc_subject is NULL or doc_subject = :doc_subject) " +
                "and (:doc_category is NULL or doc_category = :doc_category) " +
                "group by doc_tax_year";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_author", author);
                command.Parameters.Add("doc_subject", subject);
                command.Parameters.Add("doc_category", category);
                return ExecuteQuery(command);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public Document GetDocument(int id, string searchTerm, bool includePdf)
        {
            searchTerm ??= string.Empty;
            string sql;
            
            if (includePdf)
            {
                sql = "select doc_title,doc_author,doc_subject,doc_keywords,doc_added,doc_notes," +
                    "doc_pdf,doc_category,doc_flag,doc_tax_year,doc_text " +
                    "from docs where doc_id = :doc_id";
            }
            else
            {
                sql = "select doc_title,doc_author,doc_subject,doc_keywords,doc_added,doc_notes," +
                    "doc_category,doc_flag,doc_tax_year,doc_text " +
                    "from docs where doc_id = :doc_id";
            }
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_id", id);
                using var reader = command.ExecuteReader();
                reader.Read();

                var document = new Document
                {
                    Id = id,
                    Title = reader["doc_title"].ToString(),
                    Author = reader["doc_author"].ToString(),
                    Subject = reader["doc_subject"].ToString(),
                    Keywords = reader["doc_keywords"].ToString(),
                    Added = reader["doc_added"].ToString(),
                    Notes = reader["doc_notes"].ToString(),
                    Category = reader["doc_category"].ToString(),
                    Flag = Convert.ToInt32(reader["doc_flag"]),
                    TaxYear = reader["doc_tax_year"].ToString(),
                    Text = reader["doc_text"].ToString(),
                    SearchTermSnippets = GetSearchTermSnippets(id, searchTerm)
                };

                if (includePdf)
                {
                    var blob = reader.GetOracleBlob(6);
                    using var memoryStream = new MemoryStream(blob.Value);
                    document.Pdf = memoryStream.ToArray();
                }

                return document;
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseException(ex.Message);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public void InsertDocument(Document document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var sql = "insert into docs values(" +
                      "docs_seq.NEXTVAL," +
                      ":doc_title," +
                      ":doc_author," +
                      ":doc_subject," +
                      ":doc_keywords," +
                      ":doc_added," +
                      ":doc_notes," +
                      ":doc_pdf," +
                      "''," +
                      ":doc_category," +
                      ":doc_flag," +
                      ":doc_tax_year," +
                      ":doc_text_annotations," +
                      ":doc_text)";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_title", document.Title);
                command.Parameters.Add("doc_author", document.Author);
                command.Parameters.Add("doc_subject", document.Subject);
                command.Parameters.Add("doc_keywords", document.Keywords);
                command.Parameters.Add("doc_added", document.Added);
                command.Parameters.Add("doc_notes", document.Notes);
                command.Parameters.Add(
                    "doc_pdf",
                    OracleDbType.Blob,
                    document.Pdf,
                    ParameterDirection.Input);
                command.Parameters.Add("doc_category", document.Category);
                command.Parameters.Add("doc_flag", document.Flag);
                command.Parameters.Add("doc_tax_year", document.TaxYear);
                command.Parameters.Add("doc_text_annotations", document.TextAnnotations);
                command.Parameters.Add("doc_text", document.Text);
                command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
            finally
            {
                DatabaseSession.DocumentsListHasChanges = true;
            }
        }

        public void UpdateDocument(Document document, bool updatePdf = false)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var sql = "update docs set " +
                      "doc_title = :doc_title," +
                      "doc_author = :doc_author," +
                      "doc_subject = :doc_subject,";

            if (!updatePdf)
            {
                sql += "doc_added = :doc_added," +
                       "doc_notes = :doc_notes,";
            }
            else
            {
                sql += "doc_pdf = :doc_pdf,";
            }

            sql += "doc_dummy = ''," +
                   "doc_category = :doc_category," +
                   "doc_tax_year = :doc_tax_year," +
                   "doc_flag = :doc_flag," +
                   "doc_text_annotations = :doc_text_annotations," +
                   "doc_text = :doc_text where doc_id = :doc_id";

            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_title", document.Title);
                command.Parameters.Add("doc_author", document.Author);
                command.Parameters.Add("doc_subject", document.Subject);
                command.Parameters.Add("doc_added", document.Added);
                command.Parameters.Add("doc_notes", document.Notes);

                if (updatePdf)
                {
                    command.Parameters.Add(
                        "doc_pdf",
                        OracleDbType.Blob,
                        document.Pdf,
                        ParameterDirection.Input);
                }

                command.Parameters.Add("doc_category", document.Category);
                command.Parameters.Add("doc_tax_year", document.TaxYear);
                command.Parameters.Add("doc_flag", document.Flag);
                command.Parameters.Add("doc_text_annotations", document.TextAnnotations);
                command.Parameters.Add("doc_text", document.Text);
                command.Parameters.Add("doc_id", document.Id);
                command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
            finally
            {
                DatabaseSession.DocumentsListHasChanges = true;
            }
        }

        public void DeleteDocument(int id)
        {
            var sql = "delete from docs where doc_id = :doc_id";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("doc_id", id);
                command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
            finally
            {
                DatabaseSession.DocumentsListHasChanges = true;
            }
        }

        public void TestConnection()
        {
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
            }
            catch (OracleException ex)
            {
                ResetCredential();
                throw new DatabaseException(ex.Message);
            }

            GetDocsTableAccess();

            if (!DatabaseSession.SelectGranted)
            {
                ResetCredential();
                throw new DatabaseException(Resources.NoAccessToLogin);
            }
        }

        public void ResetCredential()
        {
            oracleCredential = null;
        }

        public void CreateDatabase()
        {
            throw new NotSupportedException();
        }

        public void CompactDatabase()
        {
            throw new NotSupportedException();
        }

        public void UpgradeDatabase()
        {
            throw new NotSupportedException();
        }

        protected override DataTable ExecuteQuery(OracleCommand command)
        {
            using var adapter = new OracleDataAdapter(command);
            using var table = new DataTable();
            table.Locale = CultureInfo.InvariantCulture;
            adapter.Fill(table);
            return table;
        }

        protected override string GetSearchTermSnippets(int id, string searchTerm)
        {
            if (searchTerm is null)
            {
                throw new ArgumentNullException(nameof(searchTerm));
            }

            string result = string.Empty;

            if (searchTerm.Length > 0)
            {
                var sql = "select ctx_doc.snippet('docs_idx', :doc_id, :doc_dummy, " +
                    "'[', ']', :translate, '||') from dual";
                try
                {
                    using var connection = OracleConnectionFactory.Create(
                        connStrBuilder.ConnectionString,
                        oracleCredential,
                        DatabaseSession.SchemaName);
                    using var command = new OracleCommand(sql, connection);
                    command.Parameters.Add("doc_id", id);
                    command.Parameters.Add("doc_dummy", searchTerm);
                    command.Parameters.Add(
                        "translate",
                        OracleDbType.Boolean,
                        false,
                        ParameterDirection.Input);
                    connection.Open();
                    using var reader = command.ExecuteReader();
                    reader.Read();
                    result = reader.GetString(0);
                }
                catch (OracleException ex)
                {
                    throw new DatabaseException(ex.Message);
                }
            }
            return result;
        }

        protected override void GetDocsTableAccess()
        {
            var sql = "select privilege from all_tab_privs " +
                "where grantor = :grantor " +
                "and grantee = :grantee " +
                "and table_name = upper('docs')";
            
            try
            {
                using var connection = OracleConnectionFactory.Create(
                    connStrBuilder.ConnectionString,
                    oracleCredential,
                    DatabaseSession.SchemaName);
                using var command = new OracleCommand(sql, connection);
                command.BindByName = true;
                command.Parameters.Add("grantor", DatabaseSession.SchemaName.ToUpper());
                command.Parameters.Add("grantee", DatabaseSession.UserName.ToUpper());
                using var reader = command.ExecuteReader();
                DatabaseSession.SelectGranted = false;
                DatabaseSession.InsertGranted = false;
                DatabaseSession.UpdateGranted = false;
                DatabaseSession.DeleteGranted = false;

                while (reader.Read())
                {
                    switch (reader.GetString(0))
                    {
                        case "SELECT":
                            DatabaseSession.SelectGranted = true;
                            break;
                        case "INSERT":
                            DatabaseSession.InsertGranted = true;
                            break;
                        case "UPDATE":
                            DatabaseSession.UpdateGranted = true;
                            break;
                        case "DELETE":
                            DatabaseSession.DeleteGranted = true;
                            break;
                    }
                }
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    connStrBuilder.Clear();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
