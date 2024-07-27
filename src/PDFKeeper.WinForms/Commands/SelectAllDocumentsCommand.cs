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

using System.Windows.Forms;
using PDFKeeper.Core.Commands;
using PDFKeeper.WinForms.Views;

namespace PDFKeeper.WinForms.Commands
{
    public class SelectAllDocumentsCommand : ICommand
    {
        private readonly MainForm form;
        private readonly bool check;

        /// <summary>
        /// Initializes a new instance of the SelectAllDocumentsCommand class that checks/unchecks
        /// all documents in the DocumentsDataGridView when executed.
        /// </summary>
        /// <param name="form">The MainForm instance.</param>
        /// <param name="check">
        /// True to check all documents; False to uncheck all documents.
        /// </param>
        public SelectAllDocumentsCommand(MainForm form, bool check)
        {
            this.form = form;
            this.check = check;
        }

        public void Execute()
        {
            form.Cursor = Cursors.WaitCursor;
            foreach (DataGridViewRow row in form.DocumentsDataGridView.Rows)
            {
                row.Cells[0].Value = check;
            }
            form.DocumentsDataGridView.RefreshEdit();
            form.Cursor = Cursors.Default;
        }
    }
}
