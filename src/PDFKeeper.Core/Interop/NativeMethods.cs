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
using System.Runtime.InteropServices;

namespace PDFKeeper.Core.Interop
{
    internal static class NativeMethods
    {
        /// <summary>
        /// Adds the window into the clipboard format listener list.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <returns>Windows added? (true or false)</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AddClipboardFormatListener(IntPtr hwnd);

        /// <summary>
        /// Removes the window from the clipboard format listener list.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <returns>Windows removed? (true or false)</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        /// <summary>
        /// Retrieves the full path of a known folder identified by the folder's KNOWNFOLDERID.
        /// </summary>
        /// <param name="rfid">
        /// The reference to the KNOWNFOLDERID that identifies the folder.
        /// </param>
        /// <param name="dwFlags">
        /// The flags that specify special retrieval options. This value can be 0; otherwise, one
        /// or more of the KNOWN_FOLDER_FLAG values.
        /// </param>
        /// <param name="hToken">
        /// The access token that represents a particular user. If this parameter is null, which is
        /// the most common usage, the function requests the known folder for the current user.
        /// </param>
        /// <returns>The folder path.</returns>
        [DllImport(
            "shell32.dll",
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            PreserveSig = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern string SHGetKnownFolderPath(
            [MarshalAs(
            UnmanagedType.LPStruct)] Guid rfid,
            uint dwFlags,
            IntPtr hToken = default);
    }
}
