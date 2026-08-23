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
using PDFKeeper.Core.Interfaces.HelpSystem;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Models;
using System;
using System.Globalization;
using System.IO;

namespace PDFKeeper.Core.HelpSystem
{
    /// <summary>
    /// Default implementation of the <see cref="IHelpFileResolver"/> interface.
    /// </summary>
    public sealed class HelpFileResolver : IHelpFileResolver
    {
        private readonly ApplicationInfoDto applicationInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpFileResolver"/> class.
        /// </summary>
        /// <param name="applicationInfoService">
        /// The <see cref="IApplicationInfoService"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException"><
        /// Thrown when <paramref name="applicationInfoService"/> is null.
        /// </exception>
        public HelpFileResolver(IApplicationInfoService applicationInfoService)
        {
            applicationInfo = applicationInfoService?.GetApplicationInfo() ??
                throw new ArgumentNullException(nameof(applicationInfoService));
        }

        public string GetHelpFilePath()
        {
            var helpFilePath = Path.Combine(
                applicationInfo.BaseDirectory,
                $"{applicationInfo.ProductName}.{CultureInfo.CurrentCulture}.chm");

            if (!File.Exists(helpFilePath))
            {
                helpFilePath = Path.Combine(
                    applicationInfo.BaseDirectory,
                    $"{applicationInfo.ProductName}.en-US.chm");
            }

            return helpFilePath;
        }

        public string GetTopicFileName(HelpTopic helpTopic)
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (helpTopic)
            {
                case HelpTopic.Donate:
                    return "Donate.html";
                case HelpTopic.License:
                    return "COPYING.html";
                case HelpTopic.SetupMultiUserDatabase:
                    return "Setup Multi-User Database.html";
                case HelpTopic.SetupSingleUserDatabase:
                    return "Setup Single-User Database.html";
                case HelpTopic.ThirdPartyNotices:
                    return "THIRD-PARTY-NOTICES.html";
                case HelpTopic.UsingPDFKeeper:
                    return "Using PDFKeeper.html";
                default:
                    return null;
            }
#pragma warning restore IDE0066 // Convert switch statement to expression
        }

        public string GetViewerFilePath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                "hh.exe");
        }
    }
}
