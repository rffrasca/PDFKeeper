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
    public class TextBoxSelectedTextCommand : ICommand
    {
        private readonly TextBox textBox;
        private readonly MainForm mainForm;
        private readonly MainViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxSelectedTextCommand"/> class that
        /// synchronizes the selected text property of a <see cref="TextBox"/> and sets the state
        /// in the <see cref="MainViewModel"/> when executed.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox"/> object.</param>
        /// <param name="mainForm">The <see cref="MainForm"/> instance.</param>
        /// <param name="viewModel">The <see cref="MainViewModel"/> instance.</param>
        [CLSCompliant(false)]
        public TextBoxSelectedTextCommand(TextBox textBox, MainForm mainForm, MainViewModel viewModel)
        {
            this.textBox = textBox;
            this.mainForm = mainForm;
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(object parameter)
        {
            SyncSelectedTextWithViewModel();
            viewModel.SetStateForTextBoxSelectedTextCommand.Execute(null);
        }

        private void SyncSelectedTextWithViewModel()
        {
            if (textBox.Equals(mainForm.NotesTextBox))
            {
                viewModel.SelectedNotes = mainForm.NotesTextBox.SelectedText;
            }
            else if (textBox.Equals(mainForm.KeywordsTextBox))
            {
                viewModel.SelectedKeywords = mainForm.KeywordsTextBox.SelectedText;
            }
            else if (textBox.Equals(mainForm.TextTextBox))
            {
                viewModel.SelectedText = mainForm.TextTextBox.SelectedText;
            }
            else if (textBox.Equals(mainForm.SearchTermSnippetsTextBox))
            {
                viewModel.SelectedSearchTermSnippets =
                    mainForm.SearchTermSnippetsTextBox.SelectedText;
            }
        }
    }
}
