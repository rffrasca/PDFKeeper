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
using PDFKeeper.WinForms.Views;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace PDFKeeper.WinForms.Commands
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainFormMouseDownCommand"/> class that on
    /// execution initiates a drag and drop of the PDF for the selected document when the right
    /// mouse button is pressed.
    /// </summary>
    /// <param name="mouseEventArgs">The <see cref="MouseEventArgs"/> object.</param>
    /// <param name="mainForm">The <see cref="MainForm"/> instance.</param>
    /// <param name="viewModel">The <see cref="MainViewModel"/> instance.</param>
    internal class MainFormMouseDownCommand(
        MouseEventArgs mouseEventArgs,
        MainForm mainForm,
        MainViewModel viewModel) : ICommand
    {
        private readonly MouseEventArgs mouseEventArgs = mouseEventArgs;
        private readonly MainForm mainForm = mainForm;
        private readonly MainViewModel viewModel = viewModel;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(object parameter)
        {
            if (mouseEventArgs.Button.Equals(MouseButtons.Right))
            {
                var hitTest = mainForm.DocumentsDataGridView.HitTest(
                    mouseEventArgs.X,
                    mouseEventArgs.Y);

                if (hitTest.RowIndex >= 0)
                {
                    var rowIndex = mainForm.DocumentsDataGridView.Rows[hitTest.RowIndex];
                    rowIndex.Selected = true;
                    viewModel.DoDragDropPdfForCurrentDocumentCommand.Execute(null);
                }
            }
        }
    }
}
