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

namespace PDFKeeper.Core.Enums
{
    /// <summary>
    /// Defines the various folders used by the application for different purposes.
    /// </summary>
    public enum ApplicationFolder
    {
        /// <summary>
        /// The folder used for application-specific data files.
        /// </summary>
        ApplicationData,

        /// <summary>
        /// The folder used for cached application data.
        /// </summary>
        Cache,

        /// <summary>
        /// The folder used for logging application unhandled exceptions.
        /// </summary>
        Log,

        /// <summary>
        /// The folder used for temporary application files.
        /// </summary>
        Temp,

        /// <summary>
        /// The folder used for files pending upload.
        /// </summary>
        Upload,

        /// <summary>
        /// The folder used for upload profile definitions.
        /// </summary>
        UploadProfiles,

        /// <summary>
        /// The folder used for files rejected during upload processing.
        /// </summary>
        UploadRejected,

        /// <summary>
        /// The folder used for staging files prior to upload.
        /// </summary>
        UploadStaging
    }
}
