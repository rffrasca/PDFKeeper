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
using PDFKeeper.Core.Interfaces.HelpSystem;
using PDFKeeper.Core.Interfaces.Services;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.HelpSystem
{
    /// <summary>
    /// Default implementation of the <see cref="IHelpViewer"/> interface.
    /// </summary>
    internal sealed class HelpViewer : IHelpViewer
    {
        private readonly IHelpFileResolver helpFileResolver;
        private readonly IProcessService processService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpViewer"/> class.
        /// </summary>
        /// <param name="helpFileResolver">
        /// The <see cref="IHelpFileResolver"/> instance.
        /// </param>
        /// <param name="processService">
        /// The <see cref="IProcessService"/> instance.
        /// </param>
        public HelpViewer(IHelpFileResolver helpFileResolver, IProcessService processService)
        {
            this.helpFileResolver = helpFileResolver;
            this.processService = processService;
        }

        public void ShowHelp(HelpTopic topic, object parentControl = null)
        {
            if (parentControl is Control control)
            {
                Help.ShowHelp(
                    control,
                    helpFileResolver.GetHelpFilePath(),
                    helpFileResolver.GetTopicFileName(topic));
            }
            else
            {
                processService.StartAndWaitForExit(
                    helpFileResolver.GetViewerFilePath(),
                    $"ms-its:{helpFileResolver.GetHelpFilePath()}" +
                    $"::{helpFileResolver.GetTopicFileName(topic)}");
            }
        }
    }
}
