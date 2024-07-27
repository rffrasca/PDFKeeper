// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

using PDFKeeper.Core.Commands;
using PDFKeeper.WinForms.Helpers;
using PDFKeeper.WinForms.Views;

namespace PDFKeeper.WinForms.Commands
{
    public class HelpFileShowCommand : ICommand
    {
        private readonly HelpFile helpFile;
        private readonly MainForm form;

        /// <summary>
        /// Initializes a new instance of the HelpFileShowCommand class that shows the
        /// "Using PDFKeeper.html" help topic when executed.
        /// </summary>
        /// <param name="helpFile">The HelpFile instance.</param>
        /// <param name="form">The MainForm instance.</param>
        public HelpFileShowCommand(HelpFile helpFile, MainForm form)
        {
            this.helpFile = helpFile;
            this.form = form;
        }

        public void Execute()
        {
            helpFile.Show("Using PDFKeeper.html", form);
        }
    }
}
