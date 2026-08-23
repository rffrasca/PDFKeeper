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

using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Interop;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using System;
using System.IO;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IUploadFolderShortcutService"/> interface.
    /// </summary>
    public sealed class UploadFolderShortcutService : IUploadFolderShortcutService
    {
        private readonly IApplicationFolderManager applicationFolderManager;
        private readonly ApplicationInfoDto applicationInfo;
        private readonly string uploadShortcutName;
        private readonly string desktopShortcutPath;
        private readonly string downloadsShortcutPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFolderShortcutService"/> class.
        /// </summary>
        /// <param name="applicationFolderManager">
        /// The <see cref="IApplicationFolderManager"/> instance.
        /// </param>
        /// <param name="applicationInfoService">
        /// The <see cref="IApplicationInfoService"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="applicationInfoService"/> is null.
        /// </exception>
        public UploadFolderShortcutService(
            IApplicationFolderManager applicationFolderManager,
            IApplicationInfoService applicationInfoService)
        {
            this.applicationFolderManager = applicationFolderManager;
            applicationInfo = applicationInfoService?.GetApplicationInfo() ??
                throw new ArgumentNullException(nameof(applicationInfoService));
            var desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var downloadsFolderPath = NativeMethods.SHGetKnownFolderPath(
                new Guid("374DE290-123F-4565-9164-39C4925E467B"), 0, IntPtr.Zero);
            uploadShortcutName = $"{applicationInfo.ProductName} {Resources.Upload}.lnk";
            desktopShortcutPath = Path.Combine(desktopFolderPath, uploadShortcutName);
            downloadsShortcutPath = Path.Combine(downloadsFolderPath, uploadShortcutName);
        }

        public void CreateShortcuts()
        {
            var shortcutPaths = new[] { desktopShortcutPath, downloadsShortcutPath };

            foreach (var shortcutPath in shortcutPaths)
            {
                if (!File.Exists(shortcutPath))
                {
                    var uploadFolderPath = applicationFolderManager.GetOrCreateFolderPath(
                        ApplicationFolder.Upload);
                    var wshShell = new IWshRuntimeLibrary.WshShell();
                    var shortcut = (IWshRuntimeLibrary.IWshShortcut)
                        wshShell.CreateShortcut(shortcutPath);
                    shortcut.TargetPath = uploadFolderPath;
                    shortcut.Save();
                }
            }
        }

        public void DeleteShortcuts()
        {
            try
            {
                File.Delete(desktopShortcutPath);
                File.Delete(downloadsShortcutPath);
            }
            catch (IOException) { }
        }
    }
}
