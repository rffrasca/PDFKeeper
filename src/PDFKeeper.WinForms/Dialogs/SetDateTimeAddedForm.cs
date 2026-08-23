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

using PDFKeeper.Core.Interfaces.HelpSystem;
using System.Globalization;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Dialogs
{
    internal partial class SetDateTimeAddedForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetDateTimeAddedForm"/> class.
        /// </summary>
        /// <param name="helpFileResolver">
        /// The <see cref="IHelpFileResolver"/> instance.
        /// </param>
        public SetDateTimeAddedForm(IHelpFileResolver helpFileResolver)
        {
            InitializeComponent();
            HelpProvider.HelpNamespace = helpFileResolver.GetHelpFilePath();
        }

        public string DateTimeAdded => DateTimePicker.Value.ToString(
            DateTimePicker.CustomFormat, 
            CultureInfo.CurrentCulture);

        private void OK_Button_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
