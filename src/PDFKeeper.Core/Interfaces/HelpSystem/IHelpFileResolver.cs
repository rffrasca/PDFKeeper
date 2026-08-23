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
    /// Defines an interface that provides application-level help file resolution methods.
    /// </summary>
    public interface IHelpFileResolver
    {
        /// <summary>
        /// Retrieves the absolute path name of the compiled HTML help file (.chm) selected for
        /// the current application instance. 
        /// </summary>
        /// <returns>
        /// The full path to the compiled HTML help file.
        /// </returns>
        string GetHelpFilePath();

        /// <summary>
        /// Resolves the HTML file name associated with a specific <see cref="HelpTopic"/>
        /// contained within the application's help file.
        /// </summary>
        /// <param name="helpTopic">
        /// The help topic to resolve.
        /// </param>
        /// <returns>
        /// The corresponding HTML file name for the specified help topic, or <c>null</c>
        /// if the topic does not have an associated file.
        /// </returns>
        string GetTopicFileName(HelpTopic helpTopic);

        /// <summary>
        /// Resolves the absolute path to the Windows HTML Help Viewer executable (hh.exe).
        /// </summary>
        /// <returns>
        /// The full path to hh.exe.
        /// </returns>
        string GetViewerFilePath();
    }
}
