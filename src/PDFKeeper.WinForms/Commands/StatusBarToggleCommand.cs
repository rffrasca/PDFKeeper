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
    public class StatusBarToggleCommand : ICommand
    {
        private readonly MainForm form;

        /// <summary>
        /// Initializes a new instance of the StatusBarToggleCommand class that toggles the
        /// MainForm Status Bar menu item Checked state which controls the StatusStrip Visible
        /// state when executed.
        /// </summary>
        /// <param name="form">The MainForm instance.</param>
        public StatusBarToggleCommand(MainForm form)
        {
            this.form = form;
        }

        public void Execute()
        {
            if (form.ViewStatusBarToolStripMenuItem.Checked)
            {
                form.ViewStatusBarToolStripMenuItem.Checked = false;
            }
            else
            {
                form.ViewStatusBarToolStripMenuItem.Checked = true;
            }
            form.StatusStrip.Visible = form.ViewStatusBarToolStripMenuItem.Checked;
        }
    }
}
