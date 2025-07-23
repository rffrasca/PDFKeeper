﻿// *****************************************************************************
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

namespace PDFKeeper.WinForms.Commands
{
    public abstract class TextBoxFocusCommandBase
    {
        protected TextBox textBox;

        [CLSCompliant(false)]
        protected MainViewModel viewModel;

        /// <summary>
        /// Sets the  focused state in <see cref="MainViewModel"/>.
        /// </summary>
        /// <param name="enabled">Set focus to enabled. (true or false)</param>
        protected void SetTextBoxFocusedState(bool enabled)
        {
            switch (textBox.Name)
            {
                case "NotesTextBox":
                    viewModel.NotesFocused = enabled;
                    break;
                case "KeywordsTextBox":
                    viewModel.KeywordsFocused = enabled;
                    break;
                case "TextTextBox":
                    viewModel.TextFocused = enabled;
                    break;
                case "SearchTermSnippetsTextBox":
                    viewModel.SearchTermSnippetsFocused = enabled;
                    break;
            }
        }
    }
}
