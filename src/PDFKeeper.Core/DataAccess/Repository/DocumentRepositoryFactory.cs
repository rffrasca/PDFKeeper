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

namespace PDFKeeper.Core.DataAccess.Repository
{
    public static class DocumentRepositoryFactory
    {
        /// <summary>
        /// Initializes and returns the document repository instance.
        /// </summary>
        public static IDocumentRepository Instance
        {
            get
            {
                IDocumentRepository instance = null;
                if (DatabaseSession.PlatformName.Equals(
                    DatabaseSession.CompatiblePlatformName.Oracle))
                {
                    instance = GetOracleInstance();
                }
                else if (DatabaseSession.PlatformName.Equals(
                    DatabaseSession.CompatiblePlatformName.Sqlite))
                {
                    instance = GetSqliteInstance();
                }
                else if (DatabaseSession.PlatformName.Equals(
                    DatabaseSession.CompatiblePlatformName.MySql))
                {
                    instance = GetMySqlInstance();
                }
                return instance;
            }
        }


        // Repository object creation has to occur outside of the Instance property to avoid an
        // System.InvalidOperationException from being thrown when the database platform is SQLite.

        private static IDocumentRepository GetOracleInstance()
        {
            return new OracleDocumentRepository();
        }

        private static IDocumentRepository GetSqliteInstance()
        {
            return new SqliteDocumentRepository();
        }

        private static IDocumentRepository GetMySqlInstance()
        {
            return new MySqlDocumentRepository();
        }
    }
}
