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

using PDFKeeper.Core.Models;
using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Dialogs;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    /// <summary>
    /// Service for displaying the About Box dialog.
    /// </summary>
    internal sealed class AboutBoxDialogService : IDialogService
    {
        private readonly IHelpService helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutBoxDialogService"/> class.
        /// </summary>
        /// <param name="helpService">A service that shows a Help file topic modelessly.</param>
        public AboutBoxDialogService(IHelpService helpService)
        {
            this.helpService = helpService;
        }

        public string ShowDialog(IntPtr parent, string arg = null, Document document = null)
        {
            using (var dialog = new AboutBox(helpService))
            {
                dialog.ShowDialog(NativeWindow.FromHandle(parent));
            }

            return null;
        }
    }
}
