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
using System.Windows.Input;

namespace PDFKeeper.WinForms.Commands
{
    internal static class TagCommand
    {
        /// <summary>
        /// Invokes an object that implements <see cref="ICommand"/> and is set to a
        /// <see cref="Timer"/>, <see cref="ToolStripMenuItem"/>, <see cref="ToolStripButton"/>, or
        /// <see cref="Control"/> <c>Tag</c> property.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="Timer"/>, <see cref="ToolStripMenuItem"/>,
        /// <see cref="ToolStripButton"/>, or <see cref="Control"/> object.
        /// </param>
        internal static void Invoke(object sender)
        {
            ICommand command = sender.GetType().Name switch
            {
                "Timer" => ((Timer)sender).Tag as ICommand,
                "ToolStripMenuItem" => ((ToolStripMenuItem)sender).Tag as ICommand,
                "ToolStripButton" => ((ToolStripButton)sender).Tag as ICommand,
                _ => ((Control)sender).Tag as ICommand,
            };
            command.Execute(null);
        }
    }
}
