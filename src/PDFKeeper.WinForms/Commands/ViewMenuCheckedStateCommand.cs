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
using PDFKeeper.WinForms.Views;

namespace PDFKeeper.WinForms.Commands
{
    public class ViewMenuCheckedStateCommand : ICommand
    {
        private readonly MainForm form;

        /// <summary>
        /// Initializes a new instance of the ViewMenuCheckedStateCommand class that sets the
        /// MainForm Tool Bar and Status Bar menu items Checked state based on the ToolStrip and
        /// StatusStrip Visible states when executed.
        /// </summary>
        /// <param name="form">The MainForm instance.</param>
        public ViewMenuCheckedStateCommand(MainForm form)
        {
            this.form = form;
        }

        public void Execute()
        {
            form.ViewToolBarToolStripMenuItem.Checked = form.ToolStrip.Visible;
            form.ViewStatusBarToolStripMenuItem.Checked = form.StatusStrip.Visible;
        }
    }
}
