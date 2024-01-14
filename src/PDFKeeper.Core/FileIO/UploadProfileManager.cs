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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Models;
using System.Collections.Generic;
using System.IO;

namespace PDFKeeper.Core.FileIO
{
    internal class UploadProfileManager
    {
        private readonly DirectoryInfo uploadProfilesDirectory;

        internal UploadProfileManager()
        {
            uploadProfilesDirectory = new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.UploadProfiles);
        }

        /// <summary>
        /// Gets the UploadProfiles directory path.
        /// </summary>
        internal string UploadProfilesDirectoryPath => uploadProfilesDirectory.FullName;

        /// <summary>
        /// Gets all upload profile names.
        /// </summary>
        /// <returns>The collection.</returns>
        internal IEnumerable<string> GetUploadProfileNames()
        {
            foreach (var file in uploadProfilesDirectory.GetFiles("*.xml",
                SearchOption.TopDirectoryOnly))
            {
                yield return Path.GetFileNameWithoutExtension(file.FullName);
            }
        }

        /// <summary>
        /// Gets an upload profile.
        /// </summary>
        /// <param name="name">The upload profile name.</param>
        /// <returns>The upload profile object.</returns>
        internal UploadProfile GetUploadProfile(string name)
        {
            try
            {
                return XmlSerializer.Deserialize<UploadProfile>(GetUploadProfileInfo(name));
            }
            catch (FileNotFoundException)
            {
                return default;
            }
        }

        /// <summary>
        /// Saves the upload profile.
        /// </summary>
        /// <param name="name">The upload profile name.</param>
        /// <param name="uploadProfile">The UploadProfile object.</param>
        /// <param name="formerName">
        /// The former upload profile name or null when the name has not changed.
        /// </param>
        internal void SaveUploadProfile(string name, UploadProfile uploadProfile,
            string formerName)
        {
            if (formerName != null)
            {
                GetUploadProfileInfo(formerName).Delete();
            }
            XmlSerializer.Serialize(uploadProfile, GetUploadProfileInfo(name));
        }

        /// <summary>
        /// Deletes the upload proflle.
        /// </summary>
        /// <param name="name">The upload profile name.</param>
        internal void DeleteUploadProfile(string name)
        {
            GetUploadProfileInfo(name).DeleteToRecycleBin();
        }

        /// <summary>
        /// Gets the upload profile FileInfo object.
        /// </summary>
        /// <param name="name">The upload profile name.</param>
        /// <returns>The FileInfo object.</returns>
        private FileInfo GetUploadProfileInfo(string name)
        {
            return new FileInfo(Path.Combine(uploadProfilesDirectory.FullName,
                string.Concat(name, ".xml")));
        }
    }
}
