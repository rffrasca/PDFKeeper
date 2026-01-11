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

using PDFKeeper.Core.Extensions;
using System.Drawing;
using System.IO;

namespace PDFKeeper.Core.FileIO
{
    internal class ImageFile
    {
        private readonly FileInfo imageFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFile"/> class.
        /// </summary>
        /// <param name="imageFile">The image <see cref="FileInfo"/> object.</param>
        internal ImageFile(FileInfo imageFile)
        {
            this.imageFile = imageFile;
        }

        /// <summary>
        /// <c>true</c> or <c>false</c> if the image file exists.
        /// </summary>
        internal bool Exists => imageFile.Exists;

        /// <summary>
        /// Gets the full path name of the image file.
        /// </summary>
        internal string FullName => imageFile.FullName;

        /// <summary>
        /// Computes the hash value of the image file.
        /// </summary>
        /// <returns>The SHA512 hash value of the PDF.</returns>
        internal string ComputeHash()
        {
            return imageFile.ComputeHash();
        }

        /// <summary>
        /// Gets the contents of the image file.
        /// </summary>
        /// <returns>The contents as an <see cref="Image"/>.</returns>
        internal Image GetImage()
        {
            using var stream = new FileStream(imageFile.FullName, FileMode.Open, FileAccess.Read);
            return Image.FromStream(stream);
        }
    }
}
