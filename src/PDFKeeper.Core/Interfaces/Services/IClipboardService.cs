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

namespace PDFKeeper.Core.Interfaces.Services
{
    /// <summary>
    /// Defines a service that provides clipboard operations.
    /// </summary>
    public interface IClipboardService
    {
        /// <summary>
        /// Determines whether the clipboard currently contains text data.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the clipboard contains text data; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsText();

        /// <summary>
        /// Copies the specified file to the clipboard as a storage item.
        /// </summary>
        /// <param name="filePath">
        /// The full path of the file to copy.
        /// </param>
        void CopyFile(string filePath);

        /// <summary>
        /// Sets the specified text onto the clipboard.
        /// </summary>
        /// <param name="text">
        /// The text to place on the clipboard.
        /// </param>
        void SetText(string text);
    }
}
