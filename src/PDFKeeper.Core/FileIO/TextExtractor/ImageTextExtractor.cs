// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2023 Robert F. Frasca
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

using PDFKeeper.Core.Application;
using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace PDFKeeper.Core.FileIO.TextExtractor
{
    internal class ImageTextExtractor
    {
        private readonly Collection<byte[]> images;
        private readonly ImageFormat imageFormat;

        /// <summary>
        /// Initializes a new instance of the ImageTextExtractor class.
        /// </summary>
        /// <param name="images">The collection of images.</param>
        /// <param name="imageFormat">The image format of the images.</param>
        internal ImageTextExtractor(Collection<byte[]> images, ImageFormat imageFormat)
        {
            this.images = images;
            this.imageFormat = imageFormat;
        }

        /// <summary>
        /// Gets text from a collection of images using Windows OCR.
        /// </summary>
        /// <returns>The extracted text.</returns>
        internal async Task<string> GetText()
        {
            var text = new StringBuilder();
            foreach (var image in images)
            {
                try
                {
                    var imageFile = Path.Combine(new ApplicationDirectory().GetDirectory(
                        ApplicationDirectory.SpecialName.Temp).FullName,
                        String.Concat(Guid.NewGuid(), ".", imageFormat.ToString()));
                    File.WriteAllBytes(imageFile, image);
                    using (var stream = File.Open(imageFile, FileMode.Open, FileAccess.Read))
                    {
                        var bmpDecoder = await BitmapDecoder.CreateAsync(
                            stream.AsRandomAccessStream()).AsTask().ConfigureAwait(false);
                        using (var softwareBmp = await bmpDecoder.GetSoftwareBitmapAsync())
                        {
                            if (softwareBmp.PixelWidth <= OcrEngine.MaxImageDimension &&
                                softwareBmp.PixelHeight <= OcrEngine.MaxImageDimension)
                            {
                                var ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
                                var ocrResult = await ocrEngine.RecognizeAsync(softwareBmp);
                                foreach (var line in ocrResult.Lines)
                                {
                                    text.AppendLine(line.Text);
                                }
                            }                                
                        }
                    }
                    File.Delete(imageFile);
                }
                catch (ArithmeticException) { }
            }
            return text.ToString();
        }
    }
}
