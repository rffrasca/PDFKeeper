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

using System.Windows.Forms;
using PDFKeeper.Core.Commands;

namespace PDFKeeper.WinForms.Commands
{
    public class DialogShowCommand : ICommand
    {
        private readonly Form dialog;
        private readonly ICommand postCommand;

        /// <summary>
        /// Initializes a new instance of the DialogShowCommand class that shows a dialog when
        /// executed.
        /// </summary>
        /// <param name="dialog">The dialog instance.</param>
        /// <param name="postCommand">
        /// The ICommand object to execute after dialog has been closed or Nothing.
        /// </param>
        public DialogShowCommand(Form dialog, ICommand postCommand)
        {
            this.dialog = dialog;
            this.postCommand = postCommand;
        }

        public void Execute()
        {
            dialog.ShowDialog();
            if (postCommand != null)
            {
                postCommand.Execute();
            }
        }
    }
}
