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
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfPreviewService"/> interface.
    /// </summary>
    public sealed class PdfPreviewService : IPdfPreviewService
    {
        public async Task<byte[]> CreatePreviewImageAsync(string pdfPath, decimal pixelDensity)
        {
            var pdfFile = await StorageFile.GetFileFromPathAsync(
                pdfPath).AsTask().ConfigureAwait(false);
            var pdfDocument = await PdfDocument.LoadFromFileAsync(
                pdfFile).AsTask().ConfigureAwait(false);

            using (var page = pdfDocument.GetPage(0))
            {
                using (var stream = new InMemoryRandomAccessStream())
                {
                    var scale = (double)pixelDensity / 96.0;

                    var renderOptions = new PdfPageRenderOptions
                    {
                        DestinationWidth = (uint)(page.Size.Width * scale),
                        DestinationHeight = (uint)(page.Size.Height * scale)
                    };

                    await page.RenderToStreamAsync(
                        stream,
                        renderOptions).AsTask().ConfigureAwait(false);
                    stream.Seek(0);
                    var buffer = new Windows.Storage.Streams.Buffer((uint)stream.Size);
                    await stream.ReadAsync(
                        buffer,
                        (uint)stream.Size,
                        InputStreamOptions.None).AsTask().ConfigureAwait(false);
                    return buffer.ToArray();
                }
            }
        }
    }
}
