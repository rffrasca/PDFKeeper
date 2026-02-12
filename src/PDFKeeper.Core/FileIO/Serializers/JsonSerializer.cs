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

using System.IO;
using System.Text.Json;

namespace PDFKeeper.Core.FileIO.Serializers
{
    /// <summary>
    /// Provides static methods for serializing objects to JSON files and deserializing objects
    /// from JSON files using customizable options.
    /// </summary>
    internal static class JsonSerializer
    {
        private static JsonSerializerOptions options;

        /// <summary>
        /// Deserializes JSON content from the specified file into an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="jsonFile">The file containing the JSON data to deserialize.</param>
        /// <returns>An object of type T deserialized from the JSON file.</returns>
        internal static T DeserializeFromFile<T>(FileInfo jsonFile)
        {
            options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
            using var stream = jsonFile.OpenRead();
            return System.Text.Json.JsonSerializer.Deserialize<T>(stream, options)!;
        }

        /// <summary>
        /// Serializes the specified object to a JSON file using indented formatting.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize to JSON.</param>
        /// <param name="jsonFile">The file to which the JSON will be written.</param>
        internal static void SerializeToFile<T>(T obj, FileInfo jsonFile)
        {
            options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using var stream = jsonFile.Open(FileMode.Create, FileAccess.Write, FileShare.None);
            System.Text.Json.JsonSerializer.Serialize(stream, obj, options);
        }
    }
}
