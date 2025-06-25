// *****************************************************************************
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
// *****************************************************************************

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.Commands;
using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Services;
using PDFKeeper.WinForms.Views;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Commands
{
    public class HelpContentsShowCommand : ICommand
    {
        private readonly MainForm form;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpContentsShowCommand"/> class that
        /// shows the "Using PDFKeeper.html" help topic when executed.
        /// </summary>
        /// <param name="form">The <see cref="MainForm"/> instance.</param>
        public HelpContentsShowCommand(MainForm form)
        {
            this.form = form;
        }

        public void Execute()
        {
            var helpService = ServicesLocator.Services.GetService<IHelpService>();
            helpService.ShowHelp<Control>(form, HelpFile.Topic.UsingPDFKeeper);
        }
    }
}
