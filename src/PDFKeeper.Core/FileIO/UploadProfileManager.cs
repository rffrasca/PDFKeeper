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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.FileIO.Serializers;
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
        /// Gets the full path name of the
        /// <see cref="ApplicationDirectory.SpecialName.UploadProfiles"/> directory.
        /// </summary>
        internal string UploadProfilesDirectoryPath => uploadProfilesDirectory.FullName;

        /// <summary>
        /// Gets all Upload Profile names.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> collection of strings.</returns>
        internal IEnumerable<string> GetUploadProfileNames()
        {
            foreach (var file in uploadProfilesDirectory.GetFiles(
                "*.xml",
                SearchOption.TopDirectoryOnly))
            {
                yield return Path.GetFileNameWithoutExtension(file.FullName);
            }
        }

        /// <summary>
        /// Gets an <see cref="UploadProfile"/>.
        /// </summary>
        /// <param name="name">The Upload Profile name.</param>
        /// <returns>The <see cref="UploadProfile"/> object.</returns>
        internal UploadProfile GetUploadProfile(string name)
        {
            try
            {
                return XmlSerializer.DeserializeFromFile<UploadProfile>(GetUploadProfileInfo(name));
            }
            catch (FileNotFoundException)
            {
                return default;
            }
        }

        /// <summary>
        /// Saves the <see cref="UploadProfile"/>.
        /// </summary>
        /// <param name="name">
        /// The Upload Profile name.
        /// </param>
        /// <param name="uploadProfile">
        /// The <see cref="UploadProfile"/> object.
        /// </param>
        /// <param name="formerName">
        /// The former Upload Profile name only when the name has changed.
        /// </param>
        internal void SaveUploadProfile(
            string name,
            UploadProfile uploadProfile,
            string formerName = null)
        {
            if (formerName != null)
            {
                GetUploadProfileInfo(formerName).Delete();
            }

            XmlSerializer.SerializeToFile(uploadProfile, GetUploadProfileInfo(name));
        }

        /// <summary>
        /// Deletes an Upload Proflle.
        /// </summary>
        /// <param name="name">The Upload Profile name.</param>
        internal void DeleteUploadProfile(string name)
        {
            GetUploadProfileInfo(name).DeleteToRecycleBin();
        }

        /// <summary>
        /// Gets the Upload Profile <see cref="FileInfo"/> object.
        /// </summary>
        /// <param name="name">The Upload Profile name.</param>
        /// <returns>The <see cref="FileInfo"/> object.</returns>
        private FileInfo GetUploadProfileInfo(string name)
        {
            return new FileInfo(Path.Combine(uploadProfilesDirectory.FullName,
                string.Concat(name, ".xml")));
        }
    }
}
