// *****************************************************************************
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
// *****************************************************************************

using PDFKeeper.Core.Interop;
using System;

namespace PDFKeeper.WinForms.Helpers
{
    /// <summary>
    /// Helper class for centering dialogs relative to their owner windows.
    /// </summary>
    internal static class DialogCenteringHelper
    {
        private static NativeMethods.HookProc hookProc;
        private static IntPtr hookHandle;
        private static IntPtr ownerHandle;

        /// <summary>
        /// Installs a hook to center a dialog relative to its owner.
        /// </summary>
        /// <param name="owner">The handle of the owner window.</param>
        internal static void InstallCenteringHook(IntPtr owner)
        {
            ownerHandle = owner;
            hookProc = HookCallback;
            hookHandle = NativeMethods.SetWindowsHookEx(
                NativeMethods.WH_CBT,
                hookProc,
                IntPtr.Zero,
                NativeMethods.GetCurrentThreadId());
        }

        /// <summary>
        /// Callback method for the centering hook.
        /// </summary>
        /// <param name="code">The hook code.</param>
        /// <param name="wParam">The window handle of the activated window.</param>
        /// <param name="lParam">Additional hook-specific information.</param>
        /// <returns>The result of the next hook in the chain.</returns>
        private static IntPtr HookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == NativeMethods.HCBT_ACTIVATE)
            {
                NativeMethods.GetWindowRect(ownerHandle, out NativeMethods.RECT ownerRect);
                NativeMethods.GetWindowRect(wParam, out NativeMethods.RECT msgRect);
                int msgWidth = msgRect.Right - msgRect.Left;
                int msgHeight = msgRect.Bottom - msgRect.Top;
                int x = ownerRect.Left + (ownerRect.Right - ownerRect.Left - msgWidth) / 2;
                int y = ownerRect.Top + (ownerRect.Bottom - ownerRect.Top - msgHeight) / 2;
                NativeMethods.MoveWindow(wParam, x, y, msgWidth, msgHeight, true);
                NativeMethods.UnhookWindowsHookEx(hookHandle);
            }

            return NativeMethods.CallNextHookEx(hookHandle, code, wParam, lParam);
        }
    }
}
