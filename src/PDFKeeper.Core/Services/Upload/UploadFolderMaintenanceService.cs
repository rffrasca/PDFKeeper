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
using PDFKeeper.Core.Interfaces.Services.Upload;
using PDFKeeper.Core.Interfaces.Storage;
using System;
using System.IO;

namespace PDFKeeper.Core.Services.Upload
{
    /// <summary>
    /// Default implementation of the <see cref="IUploadFolderMaintenanceService"/> interface.
    /// </summary>
    public sealed class UploadFolderMaintenanceService : IUploadFolderMaintenanceService
    {
        private readonly IUploadProfileManager uploadProfileManager;
        private readonly string uploadFolderPath;
        private readonly string uploadRejectedFolderPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFolderMaintenanceService"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <param name="uploadProfileManager">
        /// The <see cref="IUploadProfileManager"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public UploadFolderMaintenanceService(
            IApplicationFolderManager applicationFolderManager,
            IUploadProfileManager uploadProfileManager)
#pragma warning restore IDE0290 // Use primary constructor
        {
            if (applicationFolderManager is null)
            {
                throw new ArgumentNullException(nameof(applicationFolderManager));
            }

            this.uploadProfileManager = uploadProfileManager;
            uploadFolderPath = applicationFolderManager.GetOrCreateFolderPath(
                ApplicationFolder.Upload);
            uploadRejectedFolderPath = applicationFolderManager.GetOrCreateFolderPath(
                ApplicationFolder.UploadRejected);
        }

        public void PerformMaintenance()
        {
            CreateMissingUploadProfileFolders();
            DeleteDormantUploadFolders();
            DeleteEmptyUploadRejectedFolders();
        }

        /// <summary>
        /// Creates Upload Profile subfolders under the Upload folder when they do not
        /// already exist.
        /// </summary>
        private void CreateMissingUploadProfileFolders()
        {
            foreach (var uploadProfileName in uploadProfileManager.GetUploadProfileNames())
            {
                Directory.CreateDirectory(Path.Combine(uploadFolderPath, uploadProfileName));
            }
        }

        /// <summary>
        /// Removes empty Upload subfolders that are not associated with an Upload Profile,
        /// and deletes empty subfolders under profile folders.
        /// </summary>
        private void DeleteDormantUploadFolders()
        {
            foreach (var folderPath in Directory.GetDirectories(uploadFolderPath))
            {
                var folderName = Path.GetFileName(folderPath);

                if (uploadProfileManager.GetUploadProfile(folderName) != null)
                {
                    foreach (var subFolderPath in Directory.GetDirectories(folderPath))
                    {
                        if (Directory.GetFiles(subFolderPath, "*", SearchOption.AllDirectories)
                            .Length == 0)
                        {
                            Directory.Delete(subFolderPath, true);
                        }
                    }
                }
                else
                {
                    if (Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)
                        .Length == 0)
                    {
                        Directory.Delete(folderPath, true);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes empty subfolders under the UploadRejected folder.
        /// </summary>
        private void DeleteEmptyUploadRejectedFolders()
        {
            foreach (var folderPath in Directory.GetDirectories(uploadRejectedFolderPath))
            {
                if (Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)
                    .Length == 0)
                {
                    Directory.Delete(folderPath, true);
                }
            }
        }
    }
}
