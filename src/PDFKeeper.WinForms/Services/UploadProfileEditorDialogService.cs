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
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Services;
using PDFKeeper.WinForms.Views;
using System;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Services
{
    /// <summary>
    /// <see cref="UploadProfileEditorForm"/> implementation of the
    /// <see cref="IDialogService"/> interface.
    /// </summary>
    internal sealed class UploadProfileEditorDialogService : IDialogService
    {
        private readonly IHelpFileResolver helpFileResolver;
        private readonly IMessageBoxService messageBoxService;
        private readonly IUploadProfileManager uploadProfileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProfileEditorDialogService"/> class.
        /// </summary>
        /// <param name="helpFileResolver">
        /// The <see cref="IHelpFileResolver"/> instance.
        /// </param>
        /// <param name="messageBoxService">
        /// The <see cref="IMessageBoxService"/> instance.
        /// </param>
        /// <param name="uploadProfileManager">
        /// The <see cref="IUploadProfileManager"/> instance.
        /// </param>
        public UploadProfileEditorDialogService(
            IHelpFileResolver helpFileResolver,
            IMessageBoxService messageBoxService,
            IUploadProfileManager uploadProfileManager)
        {
            this.helpFileResolver = helpFileResolver;
            this.messageBoxService = messageBoxService;
            this.uploadProfileManager = uploadProfileManager;
        }

        public string ShowDialog(IntPtr parent, string arg = null, Document document = null)
        {
            using (var dialog = new UploadProfileEditorForm(
                helpFileResolver,
                messageBoxService,
                uploadProfileManager,
                arg))
            {
                dialog.ShowDialog(NativeWindow.FromHandle(parent));
            }

            return null;
        }
    }
}
