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

namespace PDFKeeper.Core.Services
{
    public interface IFileDialogService
    {
        /// <summary>
        /// Shows an <c>OpenFileDialog</c> or <c>SaveFileDialog</c> based on the implementation.
        /// </summary>
        /// <param name="filter">The file name filter <c>string</c>.</param>
        /// <param name="fileName">The optional selected file name.</param>
        /// <returns>The selected file name or <c>null</c> when no file was selected.</returns>
        string ShowDialog(string filter, string fileName = null);
    }
}
