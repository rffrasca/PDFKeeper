// *****************************************************************************
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
// *****************************************************************************

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.ViewModels;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace PDFKeeper.WinForms.Commands
{
    /// <summary>
    /// Represents a command that handles the drag enter event for the main form, enabling PDF file
    /// drops.
    /// </summary>
    /// <param name="dragEventArgs">
    /// The drag event data associated with the drag enter operation.
    /// </param>
    /// <param name="viewModel">
    /// The main view model used to execute PDF-related commands.
    /// </param>
    internal class MainFormDragEnterCommand(
        DragEventArgs dragEventArgs,
        MainViewModel viewModel) : ICommand
    {
        private readonly DragEventArgs dragEventArgs = dragEventArgs;
        private readonly MainViewModel viewModel = viewModel;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Handles a drag-and-drop operation, executing the AddPdfCommand if a single PDF file is
        /// dropped with the left mouse button pressed.
        /// </summary>
        /// <param name="parameter">
        /// The value must be null. This command does not use the parameter.
        /// </param>
        public void Execute(object parameter)
        {
            var virtualKeyService = ServiceLocator.Services.GetService<IVirtualKeyService>();

            if (virtualKeyService.IsLeftButtonDown())
            {
                if (dragEventArgs.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = (string[])dragEventArgs.Data.GetData(DataFormats.FileDrop);
                    if (files.Length.Equals(1) &&
                        Path.GetExtension(files[0]).Equals(
                            ".pdf",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        var file = files[0];
                        viewModel.AddPdfCommand.Execute(file);
                    }
                }
            }
            else
            {
                dragEventArgs.Effect = DragDropEffects.None;
            }
        }
    }
}
