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

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace PDFKeeper.Core.Application
{
    public class HelpFile
    {
        public enum Topic
        {
            Donate,
            License,
            SetupMultiUserDatabase,
            SetupSingleUserDatabase,
            ThirdPartyNotices,
            UsingPDFKeeper
        }

        public HelpFile()
        {
            var executingAssembly = new ExecutingAssembly();
            var productName = executingAssembly.ProductName;
            FullName = string.Concat(
                productName,
                CultureInfo.CurrentCulture.ToString(),
                ".chm");

            if (!File.Exists(FullName))
            {
                FullName = string.Concat(productName, ".en-US.chm");
            }
        }

        public string FullName { get; private set; }

        /// <summary>
        /// Gets the file name of a <see cref="Topic"/>.
        /// </summary>
        /// <param name="topic">The <see cref="Topic"/>.</param>
        /// <returns>The file name.</returns>
        public static string GetTopicFileName(Topic topic)
        {
            string topicFileName = null;
            switch (topic)
            {
                case Topic.Donate:
                    topicFileName = "Donate.html";
                    break;
                case Topic.License:
                    topicFileName = "COPYING.html";
                    break;
                case Topic.SetupMultiUserDatabase:
                    topicFileName = "Setup Multi-User Database.html";
                    break;
                case Topic.SetupSingleUserDatabase:
                    topicFileName = "Setup Single-User Database.html";
                    break;
                case Topic.ThirdPartyNotices:
                    topicFileName = "THIRD-PARTY-NOTICES.html";
                    break;
                case Topic.UsingPDFKeeper:
                    topicFileName = "Using PDFKeeper.html";
                    break;
            }
            
            return topicFileName;
        }

        /// <summary>
        /// Shows a help file <see cref="Topic"/> and waits for the help dialog to be closed by the
        /// user.
        /// </summary>
        /// <param name="topic">The <see cref="Topic"/>.</param>
        public void ShowHelp(Topic topic)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.Windows),
                    "hh.exe");
                process.StartInfo.Arguments = string.Concat(
                    "ms-its:",
                    FullName,
                    "::",
                    GetTopicFileName(topic));
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
