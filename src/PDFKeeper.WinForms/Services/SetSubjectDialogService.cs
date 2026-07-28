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
using PDFKeeper.WinForms.Properties;
using PDFKeeper.WinForms.Views;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    /// <summary>
    /// Provides dialog functionality for setting a subject, including validation and user
    /// feedback.
    /// </summary>
    internal sealed class SetSubjectDialogService : IDialogService
    {
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetSubjectDialogService"/> class.
        /// </summary>
        /// <param name="messageBoxService">A dialog service that displays messages.</param>
        public SetSubjectDialogService(IMessageBoxService messageBoxService)
        {
            this.messageBoxService = messageBoxService;
        }

        public string ShowDialog(IntPtr parent, string arg = null, Document document = null)
        {
            using (var dialog = new SetSubjectForm(messageBoxService))
            {
                dialog.ShowDialog(NativeWindow.FromHandle(parent));

                if (dialog.DialogResult == DialogResult.OK)
                {
                    if (dialog.SubjectUserControl.Subject.Length > 0)
                    {
                        return dialog.SubjectUserControl.Subject;
                    }
                    else
                    {
                        messageBoxService.ShowMessage(
                            parent, 
                            Resources.SubjectCannotBeBlank,
                            true);
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
