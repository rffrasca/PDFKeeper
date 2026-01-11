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
    public interface IDialogService
    {
        /// <summary>
        /// Displays a dialog associated with the specified parent, using optional arguments and
        /// document, and returns a result string.
        /// </summary>
        /// <typeparam name="T">The type of the parent window or control.</typeparam>
        /// <param name="parent">
        /// The parent window or control for the dialog.
        /// </param>
        /// <param name="arg">
        /// An optional argument that specifies the input or context for the dialog.
        /// </param>
        /// <returns>A string representing the result of the dialog.</returns>
        string ShowDialog(string arg = null);
    }
}
