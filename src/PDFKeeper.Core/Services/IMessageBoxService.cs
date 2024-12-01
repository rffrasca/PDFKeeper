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
        /// Shows a message box for displaying the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="isError">Is message an error? (true or false)</param>
        void ShowMessage(string message, bool isError);

        /// <summary>
        /// Shows a message box for displaying the message.
        /// </summary>
        /// <param name="owner">The window handle of the owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="isError">Is message an error? (true or false)</param>
        void ShowMessage(IntPtr owner, string message, bool isError);

        /// <summary>
        /// Shows a message box for asking the question.
        /// </summary>
        /// <param name="message">The question.</param>
        /// <param name="showCancel">Show Cancel button. (true or false)</param>
        /// <returns>6 (Yes), 7 (No), or 2 (Cancel).</returns>
        int ShowQuestion(string message, bool showCancel);
    }
}
