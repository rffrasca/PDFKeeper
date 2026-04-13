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

using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Dialogs;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    /// <summary>
    /// Provides a dialog service for setting the date and time an item was added.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance",
        "CA1812:Avoid uninstantiated internal classes",
        Justification = "Instantiated via dependency injection or reflection.")]
    internal sealed class SetDateTimeAddedDialogService : IDialogService
    {
        public string ShowDialog(IntPtr parent, string arg = null)
        {
            using (var dialog = new SetDateTimeAddedForm())
            {
                dialog.ShowDialog(NativeWindow.FromHandle(parent));

                if (dialog.DialogResult == DialogResult.OK)
                {
                    return dialog.DateTimeAdded;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
