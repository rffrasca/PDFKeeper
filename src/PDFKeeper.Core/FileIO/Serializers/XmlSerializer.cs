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

using System.IO;
using System.Xml;

namespace PDFKeeper.Core.FileIO.Serializers
{
    /// <summary>
    /// Provides static methods for serializing and deserializing objects to and from XML files.
    /// </summary>
    internal static class XmlSerializer
    {
        /// <summary>
        /// Deserializes an object of type T from the specified XML file.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize.</typeparam>
        /// <param name="xmlFile">The XML file containing the serialized object.</param>
        /// <returns>The deserialized object of type T.</returns>
        internal static T DeserializeFromFile<T>(FileInfo xmlFile)
        {
            var settings = new XmlReaderSettings()
            {
                DtdProcessing = DtdProcessing.Prohibit
            };

            using var reader = XmlReader.Create(xmlFile.FullName, settings);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }

        /// <summary>
        /// Serializes the specified object to an XML file using indentation and entitized new
        /// lines.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize to XML.</param>
        /// <param name="xmlFile">The file to which the XML will be written.</param>
        internal static void SerializeToFile<T>(T obj, FileInfo xmlFile)
        {
            var settings = new XmlWriterSettings()
            {
                NewLineHandling = NewLineHandling.Entitize,
                Indent = true
            };

            using var writer = XmlWriter.Create(xmlFile.FullName, settings);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            serializer.Serialize(writer, obj);
        }
    }
}
