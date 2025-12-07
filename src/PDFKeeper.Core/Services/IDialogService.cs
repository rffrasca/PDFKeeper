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

using PDFKeeper.Core.Models;

namespace PDFKeeper.Core.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Displays a dialog to the user and returns the result as a string.
        /// </summary>
        /// <param name="arg">
        /// An optional argument that specifies the initial input or context for the dialog. Can be
        /// <see langword="null"/>.
        /// </param>
        /// <param name="document">
        /// An optional <see cref="Document"/> object that provides additional context or data for
        /// the dialog. Can be <see langword="null"/>.
        /// </param>
        /// <returns>
        /// A string representing the result of the dialog interaction. The return value may vary
        /// depending on the user's input or the dialog's configuration.
        /// </returns>
        string ShowDialog(string arg = null, Document document = null);
    }
}
