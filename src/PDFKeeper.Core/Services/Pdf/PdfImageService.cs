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

using PDFKeeper.Core.Interfaces.Services.Pdf;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfImageService"/> interface.
    /// </summary>
    public sealed class PdfImageService : IPdfImageService
    {
        public async Task<IReadOnlyList<byte[]>> GetAllPagesAsTiffImagesAsync(
            string pdfPath,
            int targetDpi = 600)
        {
            var imageList = new List<byte[]>();
            var pdfFile = await StorageFile.GetFileFromPathAsync(
                pdfPath).AsTask().ConfigureAwait(false);
            var pdfDocument = await PdfDocument.LoadFromFileAsync(
                pdfFile).AsTask().ConfigureAwait(false);

            for (var i = 0; i < pdfDocument.PageCount; i++)
            {
                using (var page = pdfDocument.GetPage((uint)i))
                {
                    var renderWidth = (uint)Math.Round(page.Size.Width * (targetDpi / 96.0));

                    var renderOptions = new PdfPageRenderOptions
                    {
                        DestinationWidth = renderWidth
                    };

                    using (var memoryStream = new InMemoryRandomAccessStream())
                    {
                        var encoder = await BitmapEncoder.CreateAsync(
                            BitmapEncoder.TiffEncoderId,
                            memoryStream).AsTask().ConfigureAwait(false);
                        var propertySet = new BitmapPropertySet();
                        var compressionValue = new BitmapTypedValue(
                            TiffCompressionMode.Lzw,
                            Windows.Foundation.PropertyType.UInt32);
                        propertySet.Add("TiffCompressionMethod", compressionValue);
                        await page.RenderToStreamAsync(
                            memoryStream,
                            renderOptions).AsTask().ConfigureAwait(false);
                        var pageBytes = new byte[memoryStream.Size];
                        memoryStream.Seek(0);
                        await memoryStream.ReadAsync(
                            pageBytes.AsBuffer(),
                            (uint)memoryStream.Size,
                            InputStreamOptions.None).AsTask().ConfigureAwait(false);
                        imageList.Add(pageBytes);
                    }
                }
            }

            return imageList;
        }
    }
}
