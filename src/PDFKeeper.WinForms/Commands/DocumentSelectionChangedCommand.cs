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
using System.Windows.Input;

namespace PDFKeeper.WinForms.Commands
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentSelectionChangedCommand"/> class
    /// that sets <see cref="MainViewModel.CurrentDocumentId"/> to the ID of the selected
    /// document in <see cref="MainForm.DocumentsDataGridView"/>.
    /// </summary>
    /// <param name="mainForm">The <see cref="MainForm"/> instance.</param>
    /// <param name="viewModel">The <see cref="MainViewModel"/> instance.</param>
    internal class DocumentSelectionChangedCommand(
        MainForm mainForm, 
        MainViewModel viewModel) : ICommand
    {
        private readonly MainForm mainForm = mainForm;
        private readonly MainViewModel viewModel = viewModel;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(object parameter)
        {
            viewModel.CurrentDocumentId = 0;    // No row is selected.

            // Prevent an empty DataGridView.
            if (mainForm.DocumentsDataGridView.SelectedRows.Count > 0)
            {
                viewModel.CurrentDocumentId = Convert.ToInt32(
                    mainForm.DocumentsDataGridView.SelectedRows[0].Cells[2].Value);
            }
        }
    }
}
