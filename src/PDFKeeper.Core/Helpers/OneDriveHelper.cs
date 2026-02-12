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
using System.IO;

namespace PDFKeeper.Core.Helpers
{
    /// <summary>
    /// Provides helper methods for interacting with the user's OneDrive directory.
    /// </summary>
    public static class OneDriveHelper
    {
        private static readonly string oneDrivePath = Environment.GetEnvironmentVariable(
            "OneDrive");
        private static readonly string localDatabasePathFilePath = Path.Combine(
            oneDrivePath, "PDFKeeperLocalDatabasePath.txt");

        /// <summary>
        /// Retrieves the local database file path if OneDrive is configured and the path is
        /// available.
        /// </summary>
        /// <remarks>
        /// This method checks for the presence of a OneDrive path and a specific file containing
        /// the database path. If the file exists and the path it contains points to an existing
        /// file, that path is returned. Otherwise, the method returns null. This can be used to
        /// determine if a local database is available for use in environments where OneDrive
        /// integration is present.
        /// </remarks>
        /// <returns>
        /// A string containing the local database file path if it exists and is accessible;
        /// otherwise, null.
        /// </returns>
        public static string ReadLocalDatabasePathIfapplicable()
        {
            string result = null;
            if (!string.IsNullOrEmpty(oneDrivePath))
            {
                if (File.Exists(localDatabasePathFilePath))
                {
                    result = File.ReadAllText(localDatabasePathFilePath);
                    if (!File.Exists(result))
                    {
                        result = null;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Writes the specified database path to a local file if the path is located within the
        /// OneDrive directory; otherwise, deletes the local database path file if it exists.
        /// </summary>
        /// <remarks>
        /// This method is typically used to persist the database path for later retrieval when the
        /// database is stored in a OneDrive location. If the path does not reside within the
        /// OneDrive directory, any previously stored local database path file will be deleted.
        /// </remarks>
        /// <param name="path">The full file system path to the database. Must not be null.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="path"/> is null.
        /// </exception>
        public static void WriteLocalDatabasePathIfapplicable(string path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!string.IsNullOrEmpty(oneDrivePath))
            {
                if (path.StartsWith(oneDrivePath, StringComparison.CurrentCultureIgnoreCase))
                {
                    File.WriteAllText(localDatabasePathFilePath, path);
                }
                else
                {
                    File.Delete(localDatabasePathFilePath);
                }
            }
        }
    }
}
