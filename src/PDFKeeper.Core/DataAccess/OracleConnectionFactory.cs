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

namespace PDFKeeper.Core.DataAccess
{
    internal class OracleConnectionFactory
    {
        /// <summary>
        /// Creates and opens a new OracleConnection using the specified connection string,
        /// credentials, and default schema owner.
        /// </summary>
        /// <remarks>
        /// The returned connection is already opened and has its default schema set to the
        /// uppercase form of the specified owner. The caller is responsible for disposing the
        /// connection when it is no longer needed.
        /// </remarks>
        /// <param name="connectionString">
        /// The connection string used to establish the Oracle database connection. Cannot be null
        /// or empty.
        /// </param>
        /// <param name="oracleCredential">
        /// The OracleCredential object containing the user name and password for authentication.
        /// Cannot be null.
        /// </param>
        /// <param name="owner">
        /// The database schema owner to set as the default schema for the connection. Cannot be
        /// null or empty.
        /// </param>
        /// <returns>
        /// An open OracleConnection instance configured with the specified default schema.
        /// </returns>
        internal static OracleConnection Create(
            string connectionString,
            OracleCredential oracleCredential,
            string owner)
        {
            var connection = new OracleConnection(connectionString, oracleCredential);
            connection.Open();
            SetDefaultSchema(connection, owner.ToUpperInvariant());
            return connection;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Security",
            "CA2100:Review SQL queries for security vulnerabilities",
            Justification = "alter session does not accept bind variables")]
        private static void SetDefaultSchema(OracleConnection connection, string owner)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"alter session set current_schema = {owner}";
            command.ExecuteNonQuery();
        }
    }
}
