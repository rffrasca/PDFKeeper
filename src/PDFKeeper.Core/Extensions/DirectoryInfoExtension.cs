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

using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Linq;

namespace PDFKeeper.Core.Extensions
{
    public static class DirectoryInfoExtension
    {
        /// <summary>
        /// Empties a directory (excluding sub-directories) of all files. For any file that cannot
        /// be deleted, its extension will be changed to "delete" so that it can be deleted during
        /// the next run.
        /// </summary>
        /// <param name="directory">The DirectoryInfo object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Empty(this DirectoryInfo directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            foreach (FileInfo file in directory.GetFiles("*.*"))
            {
                try
                {
                    file.Delete();
                }
                catch (IOException)
                {
                    if (file.Exists)
                    {
                        file.MoveTo(file.ChangeExtension("delete").FullName);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a shortcut to a directory.
        /// </summary>
        /// <param name="directory">The DirectoryInfo object.</param>
        /// <param name="shortcutLinkFile">The shortcut link FileInfo object.</param>
        internal static void CreateShortcut(this DirectoryInfo directory, FileInfo shortcutLinkFile)
        {
            var wshShell = new WshShell();
            var shortcut = (IWshShortcut)wshShell.CreateShortcut(shortcutLinkFile.FullName);
            shortcut.TargetPath = directory.FullName;
            // Only create the shortcut if it does not exist. This is to prevent the occassional
            // IOException: "The process cannot access the file because it is being used by another
            // process" from being thrown.
            if (!shortcutLinkFile.Exists)
            {
                shortcut.Save();
            }
        }

        /// <summary>
        /// Gets all PDF files in the directory, including all sub-directories, ordered by last write time.
        /// </summary>
        /// <param name="directory">The DirectoryInfo object.</param>
        /// <returns>The sorted seqeuence of FileInfo objects.</returns>
        internal static IOrderedEnumerable<FileInfo> GetPdfFilesOrderByLastWriteTime(this DirectoryInfo directory)
        {
            return directory.GetFiles("*.pdf", SearchOption.AllDirectories).OrderBy(
                f => new FileInfo(f.FullName).LastWriteTime);
        }
    }
}
