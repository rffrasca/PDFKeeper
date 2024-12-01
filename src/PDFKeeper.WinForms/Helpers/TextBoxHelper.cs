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
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Views;

namespace PDFKeeper.WinForms.Helpers
{
    internal static class TextBoxHelper
    {
        /// <summary>
        /// Gets the text box with focus.
        /// </summary>
        /// <param name="form">The MainForm instance.</param>
        /// <returns>The TextBox object.</returns>
        internal static TextBox GetFocusedTextBox(MainForm form)
        {
            object result = null;
            if (form.NotesTextBox.Focused)
            {
                result = form.NotesTextBox;
            }
            else if (form.KeywordsTextBox.Focused)
            {
                result = form.KeywordsTextBox;
            }
            else if (form.TextTextBox.Focused)
            {
                result = form.TextTextBox;
            }
            else if (form.SearchTermSnippetsTextBox.Focused)
            {
                result = form.SearchTermSnippetsTextBox;
            }
            return (TextBox)result;
        }

        /// <summary>
        /// Syncs the ViewModel SelectedText property for a text box.
        /// </summary>
        /// <param name="textBox">The TextBox object.</param>
        /// <param name="form">The MainForm instance.</param>
        /// <param name="viewModel">The MainViewModel instance.</param>
        internal static void SyncSelectedTextWithViewModel(
            TextBox textBox,
            MainForm form,
            MainViewModel viewModel)
        {
            if (textBox.Equals(form.NotesTextBox))
            {
                viewModel.SelectedNotes = form.NotesTextBox.SelectedText;
            }
            else if (textBox.Equals(form.KeywordsTextBox))
            {
                viewModel.SelectedKeywords = form.KeywordsTextBox.SelectedText;
            }
            else if (textBox.Equals(form.TextTextBox))
            {
                viewModel.SelectedText = form.TextTextBox.SelectedText;
            }
            else if (textBox.Equals(form.SearchTermSnippetsTextBox))
            {
                viewModel.SelectedSearchTermSnippets = form.SearchTermSnippetsTextBox.SelectedText;
            }
        }
    }
}
