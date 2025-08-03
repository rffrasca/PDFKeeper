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

using System.ComponentModel;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.UserControls
{
    internal partial class CategoryDropDownListUserControl : UserControl
    {
        public CategoryDropDownListUserControl()
        {
            InitializeComponent();
        }

        [Bindable(true)]
        public object Categories
        {
            get => CategoryComboBox.DataSource;
            set => CategoryComboBox.DataSource = value;
        }

        [Bindable(true)]
        public string Category
        {
            get => CategoryComboBox.Text;
            set => CategoryComboBox.Text = value;
        }
    }
}
