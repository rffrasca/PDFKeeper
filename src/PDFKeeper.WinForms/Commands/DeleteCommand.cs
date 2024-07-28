// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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
using PDFKeeper.WinForms.Properties;

namespace PDFKeeper.WinForms.Commands
{
    public class DeleteCommand : ICommand
    {
        private readonly MainPresenter presenter;

        /// <summary>
        /// Initializes a new instance of the DeleteCommand class that deletes all checked
        /// documents when executed. If the database platform is SQLite and the
        /// "Compact database after deleting selected documents" option is selected, the database
        /// will be compacted.
        /// </summary>
        /// <param name="presenter">The MainPresenter instance.</param>
        public DeleteCommand(MainPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Execute()
        {
            presenter.DeleteEachSelectedDocument(Settings.Default.CompactLocalDatabaseAfterDelete);
        }
    }
}
