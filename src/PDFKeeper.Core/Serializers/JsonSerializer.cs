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

namespace PDFKeeper.Core.Serializers
{
    /// <summary>
    /// Provides static methods for serializing and deserializing objects to and from JSON files.
    /// </summary>
    internal static class JsonSerializer
    {
        private static JsonSerializerOptions jsonSerializerOptions;

        /// <summary>
        /// Deserializes an object of type <typeparamref name="T"/> from the specified
        /// JSON file path.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object to deserialize.
        /// </typeparam>
        /// <param name="jsonFilePath">
        /// The full path of the JSON file.
        /// </param>
        /// <returns>
        /// The deserialized object of type <typeparamref name="T"/>.
        /// </returns>
        internal static T DeserializeFromFile<T>(string jsonFilePath)
        {
            jsonSerializerOptions = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };

            using (var stream = File.OpenRead(jsonFilePath))
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(
                    stream,
                    jsonSerializerOptions);
            }
        }

        /// <summary>
        /// Serializes the specified object to a JSON file using indented formatting.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object to serialize.
        /// </typeparam>
        /// <param name="obj">
        /// The object to serialize to JSON.
        /// </param>
        /// <param name="jsonFilePath">
        /// The full path of the JSON file to write.
        /// </param>
        internal static void SerializeToFile<T>(T obj, string jsonFilePath)
        {
            jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using (var stream = File.Open(
                jsonFilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None))
            {
                System.Text.Json.JsonSerializer.Serialize(stream, obj, jsonSerializerOptions);
            }
        }
    }
}
