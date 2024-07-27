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

using PDFKeeper.Core.Presenters;
using PDFKeeper.WinForms.Helpers;
using PDFKeeper.WinForms.Services;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    public partial class SetSubjectForm : Form
    {
        public SetSubjectForm()
        {
            InitializeComponent();

            StringEnumerableViewModelBindingSource.DataSource = new SetSubjectPresenter(
                new MessageBoxService()).ViewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
