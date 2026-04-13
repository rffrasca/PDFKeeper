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
    /// Provides an abstraction for displaying file dialog boxes to select or save files.
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// Displays a file dialog with the specified parent window, filter, and optional initial
        /// file name.
        /// </summary>
        /// <param name="parent">
        /// A handle to the parent window for the dialog.
        /// </param>
        /// <param name="filter">
        /// The file type filter string that determines which files are displayed.
        /// </param>
        /// <param name="fileName">
        /// The initial file name to display in the dialog, or null to leave blank.
        /// </param>
        /// <returns>
        /// The selected file path, or null if the dialog is canceled.
        /// </returns>
        string ShowDialog(IntPtr parent, string filter, string fileName = null);
    }
}
