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

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Provides an abstraction for displaying a folder browser dialog and retrieving the user's
    /// selection.
    /// </summary>
    public interface IFolderBrowserDialogService
    {
        /// <summary>
        /// Displays a dialog box with the specified parent window and description.
        /// </summary>
        /// <param name="parent">A handle to the parent window for the dialog box.</param>
        /// <param name="description">A description to display in the dialog box.</param>
        /// <returns>The user input or selection from the dialog box.</returns>
        string ShowDialog(IntPtr parent, string description);
    }
}
