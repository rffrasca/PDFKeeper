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

namespace PDFKeeper.Core.FileIO
{
    internal static class XmlSerializer
    {
        /// <summary>
        /// Deserializes an XML file into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="xmlFile">The XML <see cref="FileInfo"/> object.</param>
        /// <returns>The object.</returns>
        internal static T Deserialize<T>(FileInfo xmlFile)
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
        /// Serializes the object of the specified type to an XML file.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="xmlFile">The XML <see cref="FileInfo"/> object.</param>
        internal static void Serialize<T>(T obj, FileInfo xmlFile)
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
