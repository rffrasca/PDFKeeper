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

using PDFKeeper.Core.Enums;

namespace PDFKeeper.Core.Interfaces.HelpSystem
{
    /// <summary>
    /// Defines an interface that shows help file topics using the operating system help viewer.
    /// </summary>
    public interface IHelpViewer
    {
        /// <summary>
        /// Shows a help file topic, optionally anchored to a parent UI control or blocking
        /// execution.
        /// </summary>
        /// <param name="topic">
        /// The topic to display.
        /// </param>
        /// <param name="parentControl">
        /// The optional UI owner control or window (e.g., Form, Control).
        /// When <paramref name="parentControl"/> is null, the help topic is shown in a
        /// blocking execution.
        /// </param>
        void ShowHelp(HelpTopic topic, object parentControl = null);
    }
}
