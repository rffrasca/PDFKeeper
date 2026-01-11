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

using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Properties;
using System;
using System.IO;

namespace PDFKeeper.Core.Application
{
    public class ApplicationDirectory
    {
        private readonly ExecutingAssembly executingAssembly;
        private readonly string uploadShortcutName;
        private readonly FileInfo desktopLinkFile;
        private readonly FileInfo downloadsLinkFile;
        
        public enum SpecialName
        {
            ApplicationData,
            Cache,
            Log,
            Temp,
            Upload,
            UploadProfiles,
            UploadRejected,
            UploadStaging            
        }

        public ApplicationDirectory()
        {
            executingAssembly = new ExecutingAssembly();
            uploadShortcutName = string.Concat(
                executingAssembly.ProductName,
                " ",
                Resources.Upload,
                ".lnk");
            desktopLinkFile = new FileInfo(
                Path.Combine(
                    UserProfileFolder.Desktop,
                    uploadShortcutName));
            downloadsLinkFile = new FileInfo(
                Path.Combine(
                    UserProfileFolder.Downloads,
                    uploadShortcutName));
        }

        /// <summary>
        /// Gets the <see cref="DirectoryInfo"/> object for the specified
        /// <see cref="SpecialName"/>.
        /// <para>
        /// If the directory does not exist, it will be created.
        /// </para>
        /// </summary>
        /// <param name="specialName">The <see cref="SpecialName"/>.</param>
        /// <returns>The <see cref="DirectoryInfo"/> object.</returns>
        public DirectoryInfo GetDirectory(SpecialName specialName)
        {
            DirectoryInfo directory = specialName switch
            {
                SpecialName.ApplicationData => new DirectoryInfo(GetApplicationDataPath()),
                SpecialName.Log => new DirectoryInfo(
                                        Path.Combine(
                                            GetApplicationDataPath(),
                                            executingAssembly.Version)),
                SpecialName.Temp => new DirectoryInfo(
                                        Path.Combine(
                                            Path.GetTempPath(),
                                            executingAssembly.ProductName)),
                _ => new DirectoryInfo(
                                        Path.Combine(
                                            GetApplicationDataPath(),
                                            specialName.ToString())),
            };
            directory.Create();

            if (specialName.Equals(SpecialName.Upload))
            {
                directory.CreateShortcut(desktopLinkFile);
                directory.CreateShortcut(downloadsLinkFile);
            }
            
            return directory;
        }

        public void DeleteUploadDirectoryShortcuts()
        {
            try
            {
                desktopLinkFile.Delete();
                downloadsLinkFile.Delete();
            }
            catch (IOException) { }
        }

        private string GetApplicationDataPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                executingAssembly.CompanyName,
                executingAssembly.ProductName);
        }
    }
}
