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

using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Serializers;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PDFKeeper.Core.Storage
{
    /// <summary>
    /// Default implementation of the <see cref="IUploadProfileManager"/> interface.
    /// </summary>
    public sealed class UploadProfileManager : IUploadProfileManager
    {
        private readonly string uploadProfilesFolderPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProfileManager"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="applicationFolderManager"/> is null.
        /// </exception>
        public UploadProfileManager(IApplicationFolderManager applicationFolderManager)
        {
            if (applicationFolderManager is null)
            {
                throw new ArgumentNullException(nameof(applicationFolderManager));
            }

            uploadProfilesFolderPath = applicationFolderManager.GetOrCreateFolderPath(
                ApplicationFolder.UploadProfiles); 
        }

        public IEnumerable<string> GetUploadProfileNames()
        {
            foreach (var filePath in Directory.GetFiles(uploadProfilesFolderPath, "*.xml"))
            {
                yield return Path.GetFileNameWithoutExtension(filePath);
            }
        }

        public UploadProfile GetUploadProfile(string name)
        {
            try
            {
                return XmlSerializer.DeserializeFromFile<UploadProfile>(
                    GetUploadProfilePath(name));
            }
            catch (FileNotFoundException)
            {
                return default;
            }
        }

        public void SaveUploadProfile(
            string name,
            UploadProfile uploadProfile,
            string formerName = null)
        {
            if (formerName != null)
            {
                File.Delete(GetUploadProfilePath(formerName));
            }

            XmlSerializer.SerializeToFile(uploadProfile, GetUploadProfilePath(name));
        }

        public void DeleteUploadProfile(string name)
        {
            new FileInfo(GetUploadProfilePath(name)).DeleteToRecycleBin();
        }

        /// <summary>
        /// Creates a file path representing the XML file associated with the specified
        /// Upload Profile name.
        /// </summary>
        /// <param name="name">
        /// The name of the Upload Profile whose file information is requested.
        /// </param>
        /// <returns>
        /// The path name to the corresponding <c>.xml</c> file in the <c>UploadProfiles</c>
        /// folder.
        /// </returns>
        private string GetUploadProfilePath(string name)
        {
            return Path.Combine(uploadProfilesFolderPath, $"{name}.xml");
        }
    }
}
