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

namespace PDFKeeper.Core.DataAccess.Repository
{
    internal class DocumentRepositoryFactory
    {
        /// <summary>
        /// Factory method that gets an <see cref="IDocumentRepository"/> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="IDocumentRepository"/> instance.
        /// </returns>
        internal static IDocumentRepository Create()
        {
            IDocumentRepository instance = null;

            switch (DatabaseSession.PlatformName)
            {
                case DatabaseSession.CompatiblePlatformName.Oracle:
                    instance = GetOracleInstance();
                    break;
                case DatabaseSession.CompatiblePlatformName.Sqlite:
                    instance = GetSqliteInstance();
                    break;
                case DatabaseSession.CompatiblePlatformName.SqlServer:
                    instance = GetSqlServerInstance();
                    break;
                case DatabaseSession.CompatiblePlatformName.MySql:
                    instance = GetMySqlInstance();
                    break;
            }

            return instance;
        }

        // Repository object creation has to occur outside of the GetDocumentRepository method to
        // avoid an InvalidOperationException from being thrown when the database platform is
        // SQLite.

        private static IDocumentRepository GetOracleInstance()
        {
            return new OracleDocumentRepository();
        }

        private static IDocumentRepository GetSqliteInstance()
        {
            return new SqliteDocumentRepository();
        }

        private static IDocumentRepository GetSqlServerInstance()
        {
            return new SqlServerDocumentRepository();
        }

        private static IDocumentRepository GetMySqlInstance()
        {
            return new MySqlDocumentRepository();
        }
    }
}
