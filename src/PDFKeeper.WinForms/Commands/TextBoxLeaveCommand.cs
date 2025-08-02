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

using PDFKeeper.Core.ViewModels;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace PDFKeeper.WinForms.Commands
{
    public class TextBoxLeaveCommand : TextBoxFocusCommandBase, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxLeaveCommand"/> class that sets the
        /// state in <see cref="MainViewModel"/> for a <see cref="TextBox"/> <c>Leave</c> event
        /// when executed.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox"/> object.</param>
        /// <param name="viewModel">The <see cref="MainViewModel"/> instance.</param>
        [CLSCompliant(false)]
        public TextBoxLeaveCommand(TextBox textBox, MainViewModel viewModel)
        {
            this.textBox = textBox;
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(object parameter)
        {
            SetTextBoxFocusedState(false);
            viewModel.SetStateOnTextBoxLeaveEventCommand.Execute(null);
        }
    }
}
