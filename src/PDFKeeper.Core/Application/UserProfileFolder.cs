// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

using PDFKeeper.Core.Interop;
using System;

namespace PDFKeeper.Core.Application
{
    internal class UserProfileFolder
    {
        /// <summary>
        /// Gets the Desktop folder path.
        /// </summary>
        internal static string Desktop => Environment.GetFolderPath(
            Environment.SpecialFolder.Desktop);

        /// <summary>
        /// Gets the Downloads folder path.
        /// </summary>
        internal static string Downloads => NativeMethods.SHGetKnownFolderPath(
            new Guid("374DE290-123F-4565-9164-39C4925E467B"), 0, IntPtr.Zero);
    }
}
