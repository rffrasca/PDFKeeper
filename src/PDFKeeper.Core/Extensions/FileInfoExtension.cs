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

using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace PDFKeeper.Core.Extensions
{
    internal static class FileInfoExtension
    {
        /// <summary>
        /// Appends an underline character followed by a new GUID to the name of a file.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>The modified FileInfo object.</returns>
        internal static FileInfo AppendGuidToFileName(this FileInfo file)
        {
            return new FileInfo(Path.Combine(file.DirectoryName, string.Concat(
                Path.GetFileNameWithoutExtension(file.FullName), "_", Guid.NewGuid(),
                file.Extension)));
        }

        /// <summary>
        /// Changes the directory path name of a file.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <param name="directory">The input DirectoryInfo object.</param>
        /// <returns>The modified FileInfo object.</returns>
        internal static FileInfo ChangeDirectory(this FileInfo file, DirectoryInfo directory)
        {
            return new FileInfo(Path.Combine(directory.FullName, file.Name));
        }

        /// <summary>
        /// Changes the extension of a file.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <param name="extension">The new extension.</param>
        /// <returns>The modified FileInfo object.</returns>
        internal static FileInfo ChangeExtension(this FileInfo file, string extension)
        {
            return new FileInfo(Path.ChangeExtension(file.FullName, extension));
        }

        /// <summary>
        /// Computes the hash value of a file.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>The SHA512 hash value of the file.</returns>
        internal static string ComputeHash(this FileInfo file)
        {
            using (var algorithm = HashAlgorithm.Create("SHA512"))
            {
                using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    var hash = algorithm.ComputeHash(stream);
                    return BitConverter.ToString(hash);
                }
            }
        }

        /// <summary>
        /// Deletes a file to the Recycle Bin of the operating system.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        internal static void DeleteToRecycleBin(this FileInfo file)
        {
            FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs,
                RecycleOption.SendToRecycleBin);
        }

        /// <summary>
        /// Gets the file name of a file without the extension.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>The file name without the extension.</returns>
        internal static string GetFileNameWithoutExtension(this FileInfo file)
        {
            return Path.GetFileNameWithoutExtension(file.FullName);
        }

        /// <summary>
        /// Is the filename invalid, contains % and/or + in the name? 
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>true or false</returns>
        internal static bool IsFileNameInvalid(this FileInfo file)
        {
            return file.Name.Contains("%") || file.Name.Contains("+");
        }

        /// <summary>
        /// Is the file locked?
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>true or false</returns>
        internal static bool IsLocked(this FileInfo file)
        {
            if (file.Exists)
            {
                try
                {
                    using (var stream = new FileStream(file.FullName, FileMode.Open,
                        FileAccess.ReadWrite, FileShare.None)) { }
                }
                catch (IOException)
                {
                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the contents of a file.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>The byte array containing the contents of the file object.</returns>
        internal static byte[] ReadAllBytes(this FileInfo file)
        {
            return File.ReadAllBytes(file.FullName);
        }

        /// <summary>
        /// Waits while the file is locked.
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        internal static void WaitWhileLocked(this FileInfo file)
        {
            while (file.IsLocked())
            {
                Thread.Sleep(15000);
            }
        }
    }
}
