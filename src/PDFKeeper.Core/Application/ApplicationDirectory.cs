// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2023 Robert F. Frasca
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

using Microsoft.VisualBasic.FileIO;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Properties;
using System;
using System.IO;
using SearchOption = System.IO.SearchOption;

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
            Upload,
            UploadProfiles,
            UploadRejected,
            UploadStaging,
            Temp
        }

        public ApplicationDirectory()
        {
            executingAssembly = new ExecutingAssembly();
            uploadShortcutName = string.Concat(executingAssembly.ProductName, " ",
                Resources.Upload, ".lnk");
            desktopLinkFile = new FileInfo(Path.Combine(UserProfileFolder.Desktop,
                uploadShortcutName));
            downloadsLinkFile = new FileInfo(Path.Combine(UserProfileFolder.Downloads,
                uploadShortcutName));
        }

        /// <summary>
        /// Gets the DirectoryInfo object for the SpecialName type constant. If the directory does
        /// not exist, it will be created.
        /// </summary>
        /// <param name="specialName">The SpecialName type constant.</param>
        /// <returns>The DirectoryInfo object.</returns>
        public DirectoryInfo GetDirectory(SpecialName specialName)
        {
            DirectoryInfo directory;
            if (specialName.Equals(SpecialName.ApplicationData))
            {
                directory = new DirectoryInfo(GetApplicationDataPath());
            }
            else if (specialName.Equals(SpecialName.Temp))
            {
                directory = new DirectoryInfo(Path.Combine(Path.GetTempPath(),
                    executingAssembly.ProductName));
            }
            else
            {
                directory = new DirectoryInfo(Path.Combine(GetApplicationDataPath(),
                    specialName.ToString()));
            }
            if (specialName.Equals(SpecialName.UploadProfiles))
            {
                if (!directory.Exists)
                {
                    MigrateUploadConfigs(directory);
                }
                else
                {
                    UpgradeUploadProfiles(directory);                    
                }
            }
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
            return Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), executingAssembly.CompanyName,
                executingAssembly.ProductName);
        }

        private static void MigrateUploadConfigs(DirectoryInfo directory)
        {
            var uploadConfigDirectory = new DirectoryInfo(Path.Combine(Directory.GetParent(
                directory.FullName).FullName, "UploadConfig"));
            if (uploadConfigDirectory.Exists && uploadConfigDirectory.GetFiles("*.xml",
                SearchOption.TopDirectoryOnly).Length > 0)
            {
                FileSystem.CopyDirectory(uploadConfigDirectory.FullName, directory.FullName);
                foreach (FileInfo profile in directory.GetFiles("*.xml",
                    SearchOption.TopDirectoryOnly))
                {
                    var xmlText = File.ReadAllText(profile.FullName).Replace(
                        "UploadFolderConfiguration", "UploadProfile").Replace("TitlePrefill",
                        "Title").Replace("AuthorPrefill", "Author").Replace("SubjectPrefill",
                        "Subject").Replace("KeywordsPrefill", "Keywords").Replace(
                        "CategoryPrefill", "Category").Replace("TaxYearPrefill", "TaxYear");
                    File.WriteAllText(profile.FullName, xmlText);
                }
            }
        }

        private static void UpgradeUploadProfiles(DirectoryInfo directory)
        {
            var backupDirectory = new DirectoryInfo(Path.Combine(Directory.GetParent(
                directory.FullName).FullName, "UploadProfiles.Bak"));
            if (!backupDirectory.Exists && directory.GetFiles("*.xml",
                SearchOption.TopDirectoryOnly).Length > 0)
            {
                FileSystem.CopyDirectory(directory.FullName, backupDirectory.FullName);
                foreach (FileInfo profile in directory.GetFiles("*.xml",
                    SearchOption.TopDirectoryOnly))
                {
                    var xmlText = File.ReadAllText(profile.FullName).Replace("UploadProfileModel",
                        "UploadProfile");
                    File.WriteAllText(profile.FullName, xmlText);
                }
            }
        }
    }
}
