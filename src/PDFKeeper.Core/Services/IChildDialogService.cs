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

using PDFKeeper.Core.Models;
using System;

namespace PDFKeeper.Core.Services
{
    public interface IChildDialogService
    {
        /// <summary>
        /// Displays a dialog with the specified parent window, optional argument, and document.
        /// </summary>
        /// <param name="parent">
        /// A handle to the parent window for the dialog.
        /// </param>
        /// <param name="arg">
        /// An optional argument that specifies the input or context for the dialog.
        /// </param>
        /// <param name="document">
        /// An optional document to be used in the dialog.
        /// </param>
        /// <returns>A string result from the dialog.</returns>
        string ShowDialog(IntPtr parent, string arg = null, Document document = null);
    }
}
