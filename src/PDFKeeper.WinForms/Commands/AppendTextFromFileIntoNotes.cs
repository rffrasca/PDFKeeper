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

using PDFKeeper.Core.Commands;
using PDFKeeper.Core.Presenters;

namespace PDFKeeper.WinForms.Commands
{
    public class AppendTextFromFileIntoNotes : ICommand
    {
        private readonly MainPresenter presenter;

        /// <summary>
        /// Initializes a new instance of the AppendTextFromFileIntoNotes class that inserts the
        /// contents of a text file at the end of the Notes property in the ViewModel when
        /// executed.
        /// </summary>
        /// <param name="presenter">The MainPresenter instance.</param>
        public AppendTextFromFileIntoNotes(MainPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Execute()
        {
            presenter.AppendTextFromFileIntoNotes();
        }
    }
}
