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

using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Dialogs;
using PDFKeeper.WinForms.Properties;

namespace PDFKeeper.WinForms.Services
{
    public class SetTitleDialogService : IDialogService
    {
        public string ShowDialog(string arg = null)
        {
            var messageBoxService = ServicesLocator.Services.GetService<IMessageBoxService>();

            using (var dialog = new SetTitleForm())
            {
                dialog.ShowDialog();
                if (dialog.DialogResult.Equals(DialogResult.OK))
                {
                    if (dialog.Title.Length > 0)
                    {
                        return dialog.Title;
                    }
                    else
                    {
                        messageBoxService.ShowMessage(Resources.TitleCannotBeBlank, true);
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
