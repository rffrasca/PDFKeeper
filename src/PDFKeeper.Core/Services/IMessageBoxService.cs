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
    /// Provides methods for displaying message and question dialogs to the user.
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// Displays a message to the user, optionally indicating an error.
        /// </summary>
        /// <param name="message">
        /// The message to display.
        /// </param>
        /// <param name="isError">
        /// true to indicate the message is an error; otherwise, false.
        /// </param>
        void ShowMessage(string message, bool isError = false);

        /// <summary>
        /// Displays a message dialog with the specified text and parent window.
        /// </summary>
        /// <param name="parent">
        /// A handle to the parent window for the message dialog.
        /// </param>
        /// <param name="message">
        /// The text to display in the message dialog.
        /// </param>
        /// <param name="isError">
        /// true to display the message as an error; otherwise, false.
        /// </param>
        void ShowMessage(IntPtr parent, string message, bool isError = false);

        /// <summary>
        /// Displays a question dialog with the specified message and optional Cancel button.
        /// </summary>
        /// <param name="message">
        /// The message to display in the dialog.
        /// </param>
        /// <param name="showCancel">
        /// true to display a Cancel button; otherwise, false.
        /// </param>
        /// <returns>
        /// An integer indicating the user's response. 
        /// Valid values are:
        ///   6 = <c>Yes</c>
        ///   7 = <c>No</c>
        ///   2 = <c>Cancel</c>
        /// </returns>
        int ShowQuestion(string message, bool showCancel = false);

        /// <summary>
        /// Displays a question dialog with the specified message and optional Cancel button.
        /// </summary>
        /// <param name="parent">
        /// A handle to the parent window for the message dialog.
        /// </param>
        /// <param name="message">
        /// The message to display in the dialog.
        /// </param>
        /// <param name="showCancel">
        /// true to display a Cancel button; otherwise, false.
        /// </param>
        /// <returns>
        /// An integer indicating the user's response. 
        /// Valid values are:
        ///   6 = <c>Yes</c>
        ///   7 = <c>No</c>
        ///   2 = <c>Cancel</c>
        /// </returns>
        int ShowQuestion(IntPtr parent, string message, bool showCancel = false);
    }
}
