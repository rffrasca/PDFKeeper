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

using PDFKeeper.Core.Interfaces.Caching;

namespace PDFKeeper.Core.DataAccess.Repository
{
    internal class DocumentRepositoryFactory
    {
        /// <summary>
        /// Factory method that gets an <see cref="IDocumentRepository"/> instance.
        /// </summary>
        /// <param name="pdfMemoryCache">
        /// The <see cref="IPdfMemoryCache"/> instance.
        /// </param>
        /// <returns>
        /// The <see cref="IDocumentRepository"/> instance.
        /// </returns>
        internal static IDocumentRepository Create(IPdfMemoryCache pdfMemoryCache)
        {
            IDocumentRepository instance = null;

            switch (DatabaseSession.PlatformName)
            {
                case DatabaseSession.CompatiblePlatformName.Oracle:
                    instance = GetOracleInstance(pdfMemoryCache);
                    break;
                case DatabaseSession.CompatiblePlatformName.Sqlite:
                    instance = GetSqliteInstance(pdfMemoryCache);
                    break;
                case DatabaseSession.CompatiblePlatformName.SqlServer:
                    instance = GetSqlServerInstance(pdfMemoryCache);
                    break;
                case DatabaseSession.CompatiblePlatformName.MySql:
                    instance = GetMySqlInstance(pdfMemoryCache);
                    break;
            }

            return instance;
        }

        // Repository object creation has to occur outside of the GetDocumentRepository method
        // to avoid an InvalidOperationException from being thrown when the database platform
        // is SQLite.

        private static IDocumentRepository GetOracleInstance(IPdfMemoryCache pdfMemoryCache)
        {
            return new OracleDocumentRepository(pdfMemoryCache);
        }

        private static IDocumentRepository GetSqliteInstance(IPdfMemoryCache pdfMemoryCache)
        {
            return new SqliteDocumentRepository(pdfMemoryCache);
        }

        private static IDocumentRepository GetSqlServerInstance(IPdfMemoryCache pdfMemoryCache)
        {
            return new SqlServerDocumentRepository(pdfMemoryCache);
        }

        private static IDocumentRepository GetMySqlInstance(IPdfMemoryCache pdfMemoryCache)
        {
            return new MySqlDocumentRepository(pdfMemoryCache);
        }
    }
}
