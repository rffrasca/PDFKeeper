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

using System;

namespace PDFKeeper.Core.Services
{
    public interface IMessageBoxService
    {
        /// <summary>
        /// Shows the specified message in a <c>MessageBox</c>.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="isError"><c>true</c> or <c>false</c> if the message an error.</param>
        void ShowMessage(string message, bool isError = false);

        /// <summary>
        /// Shows the specified message in a <c>MessageBox</c>.
        /// </summary>
        /// <param name="owner">The window <c>Handle</c> of the owner.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="isError"><c>true</c> or <c>false</c> if the message an error.</param>
        void ShowMessage(IntPtr owner, string message, bool isError = false);

        /// <summary>
        /// Shows the specified question in a <c>MessageBox</c>.
        /// </summary>
        /// <param name="message">
        /// The question to display.
        /// </param>
        /// <param name="showCancel">
        /// <c>true</c> or <c>false</c> to show <c>Cancel</c> button.
        /// </param>
        /// <returns>
        /// 6 = <c>Yes</c>, 7 = <c>No</c>, or 2 = <c>Cancel</c>.
        /// </returns>
        int ShowQuestion(string message, bool showCancel = false);
    }
}
