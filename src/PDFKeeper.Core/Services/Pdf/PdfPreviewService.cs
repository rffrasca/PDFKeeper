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

using Microsoft.Extensions.Caching.Memory;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using System;
using System.IO;
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
        private readonly IMemoryCache memoryCache;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfPreviewService"/> class.
        /// </summary>
        /// <param name="memoryCache">
        /// The memory cache instance to use for caching preview images.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfPreviewService(IMemoryCache memoryCache)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.memoryCache = memoryCache;
        }

        public async Task<byte[]> CreatePreviewImageAsync(string pdfPath, decimal pixelDensity)
        {
            if (pdfPath is null)
            {
                throw new ArgumentNullException(nameof(pdfPath));
            }

            var cacheKey = BuildCacheKey(pdfPath, pixelDensity);
            
            if (memoryCache.TryGetValue(cacheKey, out byte[] cachedPreview))
            {
                return cachedPreview;
            }

            var pdfFile = await StorageFile.GetFileFromPathAsync(
                pdfPath).AsTask().ConfigureAwait(false);
            var pdfDocument = await PdfDocument.LoadFromFileAsync(
                pdfFile).AsTask().ConfigureAwait(false);
            byte[] previewImage;

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
                    previewImage = buffer.ToArray();
                }
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(CacheDuration)
                .SetSize(previewImage.Length);
            memoryCache.Set(cacheKey, previewImage, cacheOptions);
            return previewImage;
        }

        /// <summary>
        /// Builds a unique cache key for the given PDF path and pixel density.
        /// </summary>
        /// <param name="pdfPath">
        /// The path to the PDF file.
        /// </param>
        /// <param name="pixelDensity">
        /// The pixel density for the preview image.
        /// </param>
        /// <returns>
        /// The unique cache key string.
        /// </returns>
        private static string BuildCacheKey(string pdfPath, decimal pixelDensity)
        {
            var lastWriteTime = File.GetLastWriteTimeUtc(pdfPath);
            return $"PdfPreview_{pdfPath.ToUpperInvariant()}_{pixelDensity}_{lastWriteTime.Ticks}";
        }
    }
}
