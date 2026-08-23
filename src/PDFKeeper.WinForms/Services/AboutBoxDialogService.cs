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
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Dialogs;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    /// <summary>
    /// <see cref="AboutBox"/> implementation of the <see cref="IDialogService"/> interface.
    /// </summary> 
    internal sealed class AboutBoxDialogService : IDialogService
    {
        private readonly IHelpViewer helpViewer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutBoxDialogService"/> class.
        /// </summary>
        /// <param name="helpViewer">
        /// The <see cref="IHelpViewer"/> instance.
        /// </param>
        public AboutBoxDialogService(IHelpViewer helpViewer)
        {
            this.helpViewer = helpViewer;
        }

        public string ShowDialog(IntPtr parent, string arg = null, Document document = null)
        {
            using (var dialog = new AboutBox(helpViewer))
            {
                dialog.ShowDialog(NativeWindow.FromHandle(parent));
            }

            return null;
        }
    }
}
