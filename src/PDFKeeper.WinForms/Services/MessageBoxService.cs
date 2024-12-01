// ****************************************************************************
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
// ****************************************************************************

using PDFKeeper.Core.Services;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        private readonly MessageBoxOptions messageBoxOptions;
        private MessageBoxButtons messageBoxButtons;

        public MessageBoxService()
        {
            if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
            {
                messageBoxOptions = MessageBoxOptions.RtlReading;
            }
            else
            {
                messageBoxOptions = 0;
            }
        }

        public void ShowMessage(string message, bool isError)
        {
            var icon = MessageBoxIcon.Information;
            if (isError)
            {
                icon = MessageBoxIcon.Error;
            }
            MessageBox.Show(
                message,
                Application.ProductName,
                MessageBoxButtons.OK,
                icon,
                MessageBoxDefaultButton.Button1,
                messageBoxOptions);
        }

        public void ShowMessage(IntPtr owner, string message, bool isError)
        {
            var icon = MessageBoxIcon.Information;
            if (isError)
            {
                icon = MessageBoxIcon.Error;
            }
            MessageBox.Show(
                NativeWindow.FromHandle(owner),
                message,
                Application.ProductName,
                MessageBoxButtons.OK,
                icon,
                MessageBoxDefaultButton.Button1,
                messageBoxOptions);
        }

        public int ShowQuestion(string message, bool showCancel)
        {
            if (showCancel)
            {
                messageBoxButtons = MessageBoxButtons.YesNoCancel;
            }
            else
            {
                messageBoxButtons = MessageBoxButtons.YesNo;
            }
            return Convert.ToInt32(
                MessageBox.Show(
                    message,
                    Application.ProductName,
                    messageBoxButtons,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    messageBoxOptions));
        }
    }
}
