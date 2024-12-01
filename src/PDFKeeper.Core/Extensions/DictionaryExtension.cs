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

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace PDFKeeper.Core.Extensions
{
    internal static class DictionaryExtension
    {
        /// <summary>
        /// Creates/Replaces a ZIP file with file entries using the key as the file name and the
        /// value as the contents for each pair in a Dictionary object.
        /// </summary>
        /// <param name="keyValuePairs">
        /// The Dictionary object.
        /// </param>
        /// <param name="zipFile">
        /// The FileInfo object of the ZIP file. If the file referenced in the FileInfo object
        /// exists, it will be overwritten.
        /// </param>
        internal static void ToZipFile(
            this Dictionary<string, byte[]> keyValuePairs,
            FileInfo zipFile)
        {
            byte[] zipContents;
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(
                    memoryStream,
                    ZipArchiveMode.Create,
                    leaveOpen: true))
                {
                    foreach (var key in keyValuePairs.ToArray())
                    {
                        var zipEntry = zipArchive.CreateEntry(key.Key);
                        using (Stream stream = zipEntry.Open())
                        {
                            stream.Write(key.Value, 0, key.Value.Length);
                        }
                    }
                }
                zipContents = memoryStream.ToArray();
            }
            File.WriteAllBytes(zipFile.FullName, zipContents);
        }
    }
}
