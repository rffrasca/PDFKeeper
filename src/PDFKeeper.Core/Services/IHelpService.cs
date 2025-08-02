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

using PDFKeeper.Core.Application;

namespace PDFKeeper.Core.Services
{
    public interface IHelpService
    {
        /// <summary>
        /// Shows a Help file topic modelessly.
        /// </summary>
        /// <typeparam name="T">
        /// The parent control type. Can be <c>Form</c> or <c>Control</c>.
        /// </typeparam>
        /// <param name="control">
        /// The parent <c>Form</c> or <c>Control</c> of the Help dialog.
        /// </param>
        /// <param name="topic">The <see cref="HelpFile.Topic"/>.</param>
        void ShowHelp<T>(T control, HelpFile.Topic topic);
    }
}
