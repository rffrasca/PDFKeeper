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

using System;
using System.Drawing;

namespace PDFKeeper.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with System.Drawing.Size structures.
    /// </summary>
    internal static class SizeExtension
    {
        /// <summary>
        /// Reduces the size by a specified percentage.
        /// </summary>
        /// <param name="size">The original size.</param>
        /// <param name="percentage">The percentage by which to reduce the size.</param>
        /// <returns>A new Size structure with the reduced dimensions.</returns>
        public static Size ReduceByPercentage(this Size size, float percentage)
        {
            return new Size(
                (int)Math.Round(size.Width * (1f - percentage / 100f)),
                (int)Math.Round(size.Height * (1f - percentage / 100f))
            );
        }
    }
}
