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

using PDFKeeper.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IClipboardService"/> interface.
    /// </summary>
    public sealed class ClipboardService : IClipboardService
    {
        public bool ContainsText()
        {
            return Clipboard.GetContent().Contains(StandardDataFormats.Text);
        }

        public async void CopyFile(string filePath)
        {
            var storageFile = await StorageFile.GetFileFromPathAsync(filePath);

            var list = new List<StorageFile>
            {
                storageFile
            };

            var dataPackage = new DataPackage();
            dataPackage.SetStorageItems(list);
            Clipboard.SetContent(dataPackage);
        }

        public void SetText(string text)
        {
            var package = new DataPackage();
            package.SetText(text);
            Clipboard.SetContent(package);
        }
    }
}
