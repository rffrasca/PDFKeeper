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

using System;
using System.Runtime.InteropServices;

namespace PDFKeeper.Core.Interop
{
    public static class NativeMethods
    {
        internal const int WH_CBT = 5;
        internal const int HCBT_ACTIVATE = 5;

        /// <summary>
        /// The RECT structure defines the coordinates of the upper-left and lower-right corners of
        /// a rectangle.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        /// <summary>
        /// This function is a callback function used with the SetWindowsHookEx function.
        /// It processes events for a hook procedure.
        /// </summary>
        /// <param name="code">The hook code passed to the hook procedure.</param>
        /// <param name="wParam">Additional information about the hook event.</param>
        /// <param name="lParam">Additional information about the hook event.</param>
        /// <returns>An IntPtr value that is returned to the system.</returns>
        internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Adds the window into the Clipboard format listener list.
        /// </summary>
        /// <param name="hwnd">The <c>Handle</c> of the window.</param>
        /// <returns><c>true</c> or <c>false</c> if window was added.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AddClipboardFormatListener(IntPtr hwnd);

        /// <summary>
        /// This function passes the hook information to the next hook procedure in the current
        /// hook chain.
        /// </summary>
        /// <param name="hhk">A handle to the current hook.</param>
        /// <param name="nCode">The hook code passed to the hook procedure.</param>
        /// <param name="wParam">Additional information about the hook event.</param>
        /// <param name="lParam">Additional information about the hook event.</param>
        /// <returns>An IntPtr value that is returned to the system.</returns>
        [DllImport("user32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            IntPtr wParam,
            IntPtr lParam);

        /// <summary>
        /// Retrieves the thread identifier of the calling thread.
        /// </summary>
        /// <returns>The thread identifier of the calling thread.</returns>
        [DllImport("kernel32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetCurrentThreadId();

        /// <summary>
        /// Retrieves the status of the specified virtual key.
        /// </summary>
        /// <remarks>
        /// For more information about virtual key codes, see the Windows API documentation.
        /// </remarks>
        /// <param name="nVirtKey">The virtual key code for which to retrieve the status.</param>
        /// <returns>
        /// A short value indicating the status of the specified virtual key. If the high-order bit
        /// is set, the key is down; if the low-order bit is set, the key is toggled.
        /// </returns>
        [DllImport("user32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern short GetKeyState(int nVirtKey);

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpRect">
        /// A pointer to a RECT structure that receives the window's coordinates.
        /// </param>
        /// <returns>
        /// <c>true</c> if the function succeeds; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// Removes the window from the Clipboard format listener list.
        /// </summary>
        /// <param name="hwnd">The <c>Handle</c> of the window.</param>
        /// <returns><c>true</c> or <c>false</c> if window was removed.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain. You would install a
        /// hook procedure to monitor certain types of events, such as keyboard or mouse input.
        /// </summary>
        /// <param name="idHook">
        /// The type of hook procedure to be installed.
        /// </param>
        /// <param name="lpfn">
        /// A pointer to the hook procedure.
        /// </param>
        /// <param name="hMod"></param>
        /// A handle to the DLL containing the hook procedure.
        /// </param>
        /// <param name="dwThreadId">
        /// A handle to the thread with which the hook procedure is to be associated.
        /// </param>
        /// <returns>
        /// A handle to the hook procedure.
        /// </returns>
        [DllImport("user32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SetWindowsHookEx(
            int idHook,
            HookProc lpfn,
            IntPtr hMod,
            uint dwThreadId);

        /// <summary>
        /// Retrieves the full path name of a known folder identified by the folder's
        /// KNOWNFOLDERID.
        /// </summary>
        /// <param name="rfid">
        /// The reference to the KNOWNFOLDERID that identifies the folder.
        /// </param>
        /// <param name="dwFlags">
        /// The flags that specify special retrieval options. This value can be 0; otherwise, one
        /// or more of the KNOWN_FOLDER_FLAG values.
        /// </param>
        /// <param name="hToken">
        /// The access token that represents a particular user. If this parameter is <c>null</c>,
        /// which is the most common usage, the function requests the known folder for the current
        /// user.
        /// </param>
        /// <returns>The folder path name.</returns>
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

        /// <summary>
        /// Removes a hook procedure installed in a hook chain.
        /// </summary>
        /// <param name="hhk">A handle to the hook to be removed.</param>
        /// <returns>
        /// <c>true</c> if the hook was removed successfully; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// Moves the specified window to the specified position and size.
        /// </summary>
        /// <param name="hWnd">The handle of the window.</param>
        /// <param name="X">The new X-coordinate of the window.</param>
        /// <param name="Y">The new Y-coordinate of the window.</param>
        /// <param name="nWidth">The new width of the window.</param>
        /// <param name="nHeight">The new height of the window.</param>
        /// <param name="repaint">Indicates whether the window should be repainted.</param>
        /// <returns>
        /// <c>true</c> if the window was moved successfully; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool MoveWindow(
            IntPtr hWnd,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            bool repaint);
    }
}
