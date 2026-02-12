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

using Microsoft.Data.SqlClient;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using SoftCircuits.FullTextSearchQuery;
using System;
using System.Data;
using System.Globalization;

namespace PDFKeeper.Core.DataAccess.Repository
{
    public class SqlServerDocumentRepository : RepositoryBase<
        SqlConnectionStringBuilder,
        SqlCommand>,
        IDocumentRepository
    {
        private static SqlCredential sqlCredential;
        private bool disposedValue;

        public SqlServerDocumentRepository()
        {
            connStrBuilder = new SqlConnectionStringBuilder
            {
                // Encrypt does not need to be set since the default value is Mandatory.
                DataSource = DatabaseSession.DataSource,
                InitialCatalog = DatabaseSession.SchemaName,
                ConnectTimeout = 60,
                TrustServerCertificate = true
            };

            sqlCredential ??= new SqlCredential(
                DatabaseSession.UserName,
                DatabaseSession.Password);
        }

        public bool SearchTermSnippetsSupported => false;

        public int GetNotesColumnDataLength()
        {
            return 0;
        }

        public DataTable GetListOfDocumentsBySearchTerm(string searchTerm)
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs where contains(*, @doc_dummy)";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                var ftsQuery = new FtsQuery();
                command.Parameters.AddWithValue("@doc_dummy", ftsQuery.Transform(searchTerm));
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocuments(string author, string subject, string category, string taxYear)
        {
            if (string.IsNullOrEmpty(author))
            {
                author = null;
            }

            if (string.IsNullOrEmpty(subject))
            {
                subject = null;
            }

            if (string.IsNullOrEmpty(category))
            {
                category = null;
            }

            if (string.IsNullOrEmpty(taxYear))
            {
                taxYear = null;
            }

            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs " +
                "where (@doc_author is NULL or doc_author = @doc_author) " +
                "and (@doc_subject is NULL or doc_subject = @doc_subject) " +
                "and (@doc_category is NULL or doc_category = @doc_category) " +
                "and (@doc_tax_year is NULL or doc_tax_year = @doc_tax_year) ";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_author", author ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_subject", subject ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_category", category ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_tax_year", taxYear ?? (object)DBNull.Value);
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocumentsByDateAdded(string dateAdded)
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added,doc_flag from docs " +
                "where doc_added like @doc_added";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_added", dateAdded + "%");
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
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
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
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
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetAuthors(string subject, string category, string taxYear)
        {
            if (string.IsNullOrEmpty(subject))
            {
                subject = null;
            }

            if (string.IsNullOrEmpty(category))
            {
                category = null;
            }

            if (string.IsNullOrEmpty(taxYear))
            {
                taxYear = null;
            }
            
            var sql = "select doc_author from docs " +
                "where (@doc_subject is NULL or doc_subject = @doc_subject) " +
                "and (@doc_category is NULL or doc_category = @doc_category) " +
                "and (@doc_tax_year is NULL or doc_tax_year = @doc_tax_year) " +
                "group by doc_author";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_subject", subject ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_category", category ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_tax_year", taxYear ?? (object)DBNull.Value);
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetSubjects(string author, string category, string taxYear)
        {
            if (string.IsNullOrEmpty(author))
            {
                author = null;
            }

            if (string.IsNullOrEmpty(category))
            {
                category = null;
            }

            if (string.IsNullOrEmpty(taxYear))
            {
                taxYear = null;
            }

            var sql = "select doc_subject from docs " +
                "where (@doc_author is NULL or doc_author = @doc_author) " +
                "and (@doc_category is NULL or doc_category = @doc_category) " +
                "and (@doc_tax_year is NULL or doc_tax_year = @doc_tax_year) " +
                "group by doc_subject";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_author", author ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_category", category ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_tax_year", taxYear ?? (object)DBNull.Value);
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetCategories(string author, string subject, string taxYear)
        {
            if (string.IsNullOrEmpty(author))
            {
                author = null;
            }

            if (string.IsNullOrEmpty(subject))
            {
                subject = null;
            }

            if (string.IsNullOrEmpty(taxYear))
            {
                taxYear = null;
            }
            
            var sql = "select doc_category from docs " +
                "where (@doc_author is NULL or doc_author = @doc_author) " +
                "and (@doc_subject is NULL or doc_subject = @doc_subject) " +
                "and (@doc_tax_year is NULL or doc_tax_year = @doc_tax_year) " +
                "group by doc_category";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_author", author ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_subject", subject ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_tax_year", taxYear ?? (object)DBNull.Value);
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetTaxYears(string author, string subject, string category)
        {
            if (string.IsNullOrEmpty(author))
            {
                author = null;
            }

            if (string.IsNullOrEmpty(subject))
            {
                subject = null;
            }

            if (string.IsNullOrEmpty(category))
            {
                category = null;
            }
            
            var sql = "select doc_tax_year from docs " +
                "where (@doc_author is NULL or doc_author = @doc_author) " +
                "and (@doc_subject is NULL or doc_subject = @doc_subject) " +
                "and (@doc_category is NULL or doc_category = @doc_category) " +
                "group by doc_tax_year";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_author", author ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_subject", subject ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@doc_category", category ?? (object)DBNull.Value);
                connection.Open();
                return ExecuteQuery(command);
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public Document GetDocument(int id, string searchTerm, bool includePdf)
        {
            string sql;
            if (includePdf)
            {
                sql = "select doc_title,doc_author,doc_subject,doc_keywords,doc_added,doc_notes," +
                    "doc_pdf,doc_category,doc_flag,doc_tax_year,doc_text_annotations,doc_text " +
                    "from docs where doc_id = @doc_id";
            }
            else
            {
                sql = "select doc_title,doc_author,doc_subject,doc_keywords,doc_added,doc_notes," +
                    "doc_category,doc_flag,doc_tax_year,doc_text_annotations,doc_text " +
                    "from docs where doc_id = @doc_id";
            }
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                var document = new Document();
                command.Parameters.AddWithValue("@doc_id", id);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    document.Id = id;
                    document.Title = reader["doc_title"].ToString();
                    document.Author = reader["doc_author"].ToString();
                    document.Subject = reader["doc_subject"].ToString();
                    document.Keywords = reader["doc_keywords"].ToString();
                    document.Added = reader["doc_added"].ToString();
                    document.Notes = reader["doc_notes"].ToString();

                    if (includePdf)
                    {
                        document.Pdf = (byte[])reader["doc_pdf"];
                    }

                    document.Category = reader["doc_category"].ToString();
                    document.Flag = Convert.ToInt32(reader["doc_flag"]);
                    document.TaxYear = reader["doc_tax_year"].ToString();
                    document.TextAnnotations = reader["doc_text_annotations"].ToString();
                    document.Text = reader["doc_text"].ToString();
                    document.SearchTermSnippets = string.Empty; // Not available in SQL Server.
                }

                return document;
            }
            catch (SqlException ex)
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
                      "@doc_title," +
                      "@doc_author," +
                      "@doc_subject," +
                      "@doc_keywords," +
                      "@doc_added," +
                      "@doc_notes," +
                      "@doc_pdf," +
                      "@doc_category," +
                      "@doc_flag," +
                      "@doc_tax_year," +
                      "@doc_text_annotations," +
                      "@doc_text)";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_title", document.Title);
                command.Parameters.AddWithValue("@doc_author", document.Author);
                command.Parameters.AddWithValue("@doc_subject", document.Subject);
                command.Parameters.AddWithValue("@doc_keywords", document.Keywords);
                command.Parameters.AddWithValue("@doc_added", document.Added);
                command.Parameters.AddWithValue("@doc_notes", document.Notes);
                command.Parameters.AddWithValue("@doc_pdf", SqlDbType.Binary).Value = document.Pdf;
                command.Parameters.AddWithValue("@doc_category", document.Category);
                command.Parameters.AddWithValue("@doc_flag", document.Flag);
                command.Parameters.AddWithValue("@doc_tax_year", document.TaxYear);
                command.Parameters.AddWithValue("@doc_text_annotations", document.TextAnnotations);
                command.Parameters.AddWithValue("@doc_text", document.Text);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
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
                      "doc_title = @doc_title," +
                      "doc_author = @doc_author," +
                      "doc_subject = @doc_subject,";

            if (!updatePdf)
            {
                sql += "doc_added = @doc_added," +
                       "doc_notes = @doc_notes,";
            }
            else
            {
                sql += "doc_pdf = @doc_pdf,";
            }

            sql += "doc_category = @doc_category," +
                   "doc_tax_year = @doc_tax_year," +
                   "doc_flag = @doc_flag," +
                   "doc_text_annotations = @doc_text_annotations," +
                   "doc_text = @doc_text where doc_id = @doc_id";

            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_title", document.Title);
                command.Parameters.AddWithValue("@doc_author", document.Author);
                command.Parameters.AddWithValue("@doc_subject", document.Subject);
                command.Parameters.AddWithValue("@doc_added", document.Added);
                command.Parameters.AddWithValue("@doc_notes", document.Notes);

                if (updatePdf)
                {
                    command.Parameters.AddWithValue(
                        "@doc_pdf",
                        SqlDbType.Binary).Value = document.Pdf;
                }

                command.Parameters.AddWithValue("@doc_category", document.Category);
                command.Parameters.AddWithValue("@doc_tax_year", document.TaxYear);
                command.Parameters.AddWithValue("@doc_flag", document.Flag);
                command.Parameters.AddWithValue("@doc_text_annotations", document.TextAnnotations);
                command.Parameters.AddWithValue("@doc_text", document.Text);
                command.Parameters.AddWithValue("@doc_id", document.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
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
            var sql = "delete from docs where doc_id = @doc_id";

            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@doc_id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
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
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                connection.Open();
            }
            catch (SqlException ex)
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
            sqlCredential = null;
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

        protected override DataTable ExecuteQuery(SqlCommand command)
        {
            using var adapter = new SqlDataAdapter(command);
            using var table = new DataTable();
            table.Locale = CultureInfo.InvariantCulture;
            adapter.Fill(table);
            return table;
        }

        protected override string GetSearchTermSnippets(int id, string searchTerm)
        {
            throw new NotSupportedException();
        }

        protected override void GetDocsTableAccess()
        {
            var sql = "select permission_name from fn_my_permissions('docs', 'OBJECT') " +
                "where subentity_name = ''";
            
            try
            {
                using var connection = new SqlConnection(
                    connStrBuilder.ConnectionString,
                    sqlCredential);
                using var command = new SqlCommand(sql, connection);
                connection.Open();
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
            catch (SqlException ex)
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
