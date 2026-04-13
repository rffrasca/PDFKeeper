// ****************************************************************************
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
// ****************************************************************************

using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Helpers;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    /// <summary>
    /// Provides methods for displaying message boxes and questions to the user, supporting
    /// right-to-left languages and error indication.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance",
        "CA1812:Avoid uninstantiated internal classes",
        Justification = "Instantiated via dependency injection or reflection.")]
    internal sealed class MessageBoxService : IMessageBoxService
    {
        private readonly MessageBoxOptions messageBoxOptions;
        private MessageBoxButtons messageBoxButtons;
        private MessageBoxIcon messageBoxIcon;

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

        public void ShowMessage(string message, bool isError = false)
        {            
            messageBoxIcon = isError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(
                message,
                Application.ProductName,
                MessageBoxButtons.OK,
                messageBoxIcon,
                MessageBoxDefaultButton.Button1,
                messageBoxOptions);
        }

        public void ShowMessage(IntPtr parent, string message, bool isError = false)
        {
            messageBoxIcon = isError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            DialogCenteringHelper.InstallCenteringHook(parent);
            MessageBox.Show(
                NativeWindow.FromHandle(parent),
                message,
                Application.ProductName,
                MessageBoxButtons.OK,
                messageBoxIcon,
                MessageBoxDefaultButton.Button1,
                messageBoxOptions);
        }

        public int ShowQuestion(string message, bool showCancel = false)
        {
            messageBoxButtons = showCancel 
                ? MessageBoxButtons.YesNoCancel
                : MessageBoxButtons.YesNo;
            return Convert.ToInt32(
                MessageBox.Show(
                    message,
                    Application.ProductName,
                    messageBoxButtons,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    messageBoxOptions));
        }

        public int ShowQuestion(IntPtr parent, string message, bool showCancel = false)
        {
            messageBoxButtons = showCancel
                ? MessageBoxButtons.YesNoCancel
                : MessageBoxButtons.YesNo;
            DialogCenteringHelper.InstallCenteringHook(parent);
            return Convert.ToInt32(
                MessageBox.Show(
                    NativeWindow.FromHandle(parent),
                    message,
                    Application.ProductName,
                    messageBoxButtons,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    messageBoxOptions));
        }
    }
}
