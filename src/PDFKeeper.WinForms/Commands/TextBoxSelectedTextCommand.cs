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

using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Views;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace PDFKeeper.WinForms.Commands
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextBoxSelectedTextCommand"/> class that
    /// synchronizes the <see cref="TextBox.SelectedText"/> and sets the state in
    /// <see cref="MainViewModel"/> when executed.
    /// </summary>
    internal class TextBoxSelectedTextCommand : ICommand
    {
        private readonly MainForm mainForm;
        private readonly MainViewModel mainViewModel;
        private readonly TextBox textBox;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxSelectedTextCommand"/> class.
        /// </summary>
        /// <param name="mainForm">The <see cref="MainForm"/> instance.</param>
        /// <param name="mainViewModel">The <see cref="MainViewModel"/> instance.</param>
        /// <param name="textBox">The <see cref="TextBox"/> object.</param>
        internal TextBoxSelectedTextCommand(
            MainForm mainForm,
            MainViewModel mainViewModel,
            TextBox textBox)
        {
            this.mainForm = mainForm;
            this.mainViewModel = mainViewModel;
            this.textBox = textBox;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(object parameter)
        {
            SyncSelectedTextWithViewModel();
            mainViewModel.SetStateForTextBoxSelectedTextCommand.Execute(null);
        }

        private void SyncSelectedTextWithViewModel()
        {
            if (textBox.Equals(mainForm.NotesTextBox))
            {
                mainViewModel.SelectedNotes = mainForm.NotesTextBox.SelectedText;
            }
            else if (textBox.Equals(mainForm.KeywordsTextBox))
            {
                mainViewModel.SelectedKeywords = mainForm.KeywordsTextBox.SelectedText;
            }
            else if (textBox.Equals(mainForm.TextTextBox))
            {
                mainViewModel.SelectedText = mainForm.TextTextBox.SelectedText;
            }
            else if (textBox.Equals(mainForm.SearchTermSnippetsTextBox))
            {
                mainViewModel.SelectedSearchTermSnippets =
                    mainForm.SearchTermSnippetsTextBox.SelectedText;
            }
        }
    }
}
