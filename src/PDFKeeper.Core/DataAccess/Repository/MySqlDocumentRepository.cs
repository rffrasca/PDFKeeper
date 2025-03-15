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

using MySql.Data.MySqlClient;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Models;
using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace PDFKeeper.Core.DataAccess.Repository
{
    [CLSCompliant(false)]
    public class MySqlDocumentRepository : RepositoryBase<
        MySqlConnectionStringBuilder,
        MySqlCommand>,
        IDocumentRepository
    {
        private bool disposedValue;

        public MySqlDocumentRepository()
        {
            connStrBuilder = new MySqlConnectionStringBuilder
            {
                UserID = DatabaseSession.UserName,
                Password = Encoding.UTF8.GetString(
                    DatabaseSession.Password.GetAsByteArray()),
                Server = DatabaseSession.DataSource,
                Port = DatabaseSession.MySqlPort,
                Database = "pdfkeeper",
                SslMode = MySqlSslMode.Required,
                ConnectionTimeout = 30
            };
        }

        public bool SearchTermSnippetsSupported => false;

        public int GetNotesColumnDataLength()
        {
            return 0;
        }

        public DataTable GetListOfDocumentsBySearchTerm(string searchTerm)
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_tax_year," +
                "doc_added from docs " +
                "where match (doc_title,doc_author,doc_subject,doc_keywords,doc_added,doc_notes," +
                "doc_category,doc_tax_year,doc_text_annotations,doc_text) " +
                "against (@doc_dummy in boolean mode)";
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_dummy", searchTerm);
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
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
                "doc_tax_year,doc_added from docs " +
                "where (@doc_author is NULL or doc_author = @doc_author) " +
                "and (@doc_subject is NULL or doc_subject = @doc_subject) " +
                "and (@doc_category is NULL or doc_category = @doc_category) " +
                "and (@doc_tax_year is NULL or doc_tax_year = @doc_tax_year) ";
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_author", author);
                        command.Parameters.AddWithValue("doc_subject", subject);
                        command.Parameters.AddWithValue("doc_category", category);
                        command.Parameters.AddWithValue("doc_tax_year", taxYear);
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocumentsByDateAdded(string dateAdded)
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added from docs " +
                "where doc_added like @doc_added";
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue(
                            "doc_added",
                            string.Concat(dateAdded, "%"));
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfFlaggedDocuments()
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added from docs where doc_flag = 1";
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public DataTable GetListOfDocuments()
        {
            var sql = "select doc_id,doc_title,doc_author,doc_subject,doc_category, " +
                "doc_tax_year,doc_added from docs";
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
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
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_subject", subject);
                        command.Parameters.AddWithValue("doc_category", category);
                        command.Parameters.AddWithValue("doc_tax_year", taxYear);
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
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
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_author", author);
                        command.Parameters.AddWithValue("doc_category", category);
                        command.Parameters.AddWithValue("doc_tax_year", taxYear);
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
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
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_author", author);
                        command.Parameters.AddWithValue("doc_subject", subject);
                        command.Parameters.AddWithValue("doc_tax_year", taxYear);
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
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
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_author", author);
                        command.Parameters.AddWithValue("doc_subject", subject);
                        command.Parameters.AddWithValue("doc_category", category);
                        connection.Open();
                        return ExecuteQuery(command);
                    }
                }
            }
            catch (MySqlException ex)
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
                "doc_pdf,doc_category,doc_flag,doc_tax_year,doc_text " +
                "from docs where doc_id = @doc_id";
            }
            else
            {
                sql = "select doc_title,doc_author,doc_subject,doc_keywords,doc_added,doc_notes," +
                "doc_category,doc_flag,doc_tax_year,doc_text " +
                "from docs where doc_id = @doc_id";
            }
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        var document = new Document();
                        command.Parameters.AddWithValue("doc_id", id);
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
                            document.Text = reader["doc_text"].ToString();
                            document.SearchTermSnippets = string.Empty; // Not available in MySQL.
                        }
                        return document;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public void InsertDocument(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            var sql = "insert into docs values(null," +
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
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@doc_title", document.Title);
                        command.Parameters.AddWithValue("@doc_author", document.Author);
                        command.Parameters.AddWithValue("@doc_subject", document.Subject);
                        command.Parameters.AddWithValue("@doc_keywords", document.Keywords);
                        command.Parameters.AddWithValue("@doc_added", document.Added);
                        command.Parameters.AddWithValue("@doc_notes", document.Notes);
                        command.Parameters.Add(
                            "@doc_pdf",
                            MySqlDbType.Binary).Value = document.Pdf;
                        command.Parameters.AddWithValue("@doc_category", document.Category);
                        command.Parameters.AddWithValue("@doc_flag", document.Flag);
                        command.Parameters.AddWithValue("@doc_tax_year", document.TaxYear);
                        command.Parameters.AddWithValue(
                            "@doc_text_annotations",
                            document.TextAnnotations);
                        command.Parameters.AddWithValue("@doc_text", document.Text);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
            finally
            {
                DatabaseSession.DocumentsListHasChanges = true;
            }
        }

        public void UpdateDocument(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            var sql = "update docs set " +
                      "doc_title = @doc_title," +
                      "doc_author = @doc_author," +
                      "doc_subject = @doc_subject," +
                      "doc_added = @doc_added," +
                      "doc_notes = @doc_notes," +
                      "doc_category = @doc_category," +
                      "doc_tax_year = @doc_tax_year," +
                      "doc_flag = @doc_flag," +
                      "doc_text_annotations = @doc_text_annotations," +
                      "doc_text = @doc_text where doc_id = @doc_id";
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_title", document.Title);
                        command.Parameters.AddWithValue("doc_author", document.Author);
                        command.Parameters.AddWithValue("doc_subject", document.Subject);
                        command.Parameters.AddWithValue("doc_added", document.Added);
                        command.Parameters.AddWithValue("doc_notes", document.Notes);
                        command.Parameters.AddWithValue("doc_category", document.Category);
                        command.Parameters.AddWithValue("doc_tax_year", document.TaxYear);
                        command.Parameters.AddWithValue("doc_flag", document.Flag);
                        command.Parameters.AddWithValue(
                            "doc_text_annotations",
                            document.TextAnnotations);
                        command.Parameters.AddWithValue("doc_text", document.Text);
                        command.Parameters.AddWithValue("doc_id", document.Id);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
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
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("doc_id", id);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
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
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    connection.Open();
                }
            }
            catch (MySqlException ex)
            {
                throw new DatabaseException(ex.Message);
            }
            GetDocsTableAccess();
        }

        public void ResetCredential()
        {
            throw new NotSupportedException();
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

        protected override DataTable ExecuteQuery(MySqlCommand command)
        {
            using (var adapter = new MySqlDataAdapter(command))
            {
                using (var table = new DataTable())
                {
                    table.Locale = CultureInfo.InvariantCulture;
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        protected override string GetSearchTermSnippets(int id, string searchTerm)
        {
            throw new NotSupportedException();
        }

        protected override void GetDocsTableAccess()
        {
            var sql = "show grants for current_user";
            string result;
            try
            {
                using (var connection = new MySqlConnection(connStrBuilder.ConnectionString))
                {
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            DatabaseSession.SelectGranted = false;
                            DatabaseSession.InsertGranted = false;
                            DatabaseSession.UpdateGranted = false;
                            DatabaseSession.DeleteGranted = false;
                            while (reader.Read())
                            {
                                result = reader.GetString(0);
                                if (result.Contains("pdfkeeper") && result.Contains("docs"))
                                {
                                    if (result.Contains("SELECT"))
                                    {
                                        DatabaseSession.SelectGranted = true;
                                    }
                                    if (result.Contains("INSERT"))
                                    {
                                        DatabaseSession.InsertGranted = true;
                                    }
                                    if (result.Contains("UPDATE"))
                                    {
                                        DatabaseSession.UpdateGranted = true;
                                    }
                                    if (result.Contains("DELETE"))
                                    {
                                        DatabaseSession.DeleteGranted = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
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
