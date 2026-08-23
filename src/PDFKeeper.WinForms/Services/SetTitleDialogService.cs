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
using PDFKeeper.WinForms.Properties;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    /// <summary>
    /// <see cref="SetTitleForm"/> implementation of the <see cref="IDialogService"/> interface.
    /// </summary>    
    internal sealed class SetTitleDialogService : IDialogService
    {
        private readonly IHelpFileResolver helpFileResolver;
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTitleDialogService"/> class.
        /// </summary>
        /// <param name="helpFileResolver">
        /// The <see cref="IHelpFileResolver"/> instance.
        /// </param>
        /// <param name="messageBoxService">
        /// The <see cref="IMessageBoxService"/> instance.
        /// </param>
        public SetTitleDialogService(
            IHelpFileResolver helpFileResolver,
            IMessageBoxService messageBoxService)
        {
            this.helpFileResolver = helpFileResolver;
            this.messageBoxService = messageBoxService;
        }

        public string ShowDialog(IntPtr parent, string arg = null, Document document = null)
        {
            using (var dialog = new SetTitleForm(helpFileResolver))
            {
                dialog.ShowDialog(NativeWindow.FromHandle(parent));

                if (dialog.DialogResult == DialogResult.OK)
                {
                    if (dialog.Title.Length > 0)
                    {
                        return dialog.Title;
                    }
                    else
                    {
                        messageBoxService.ShowMessage(parent, Resources.TitleCannotBeBlank, true);
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
