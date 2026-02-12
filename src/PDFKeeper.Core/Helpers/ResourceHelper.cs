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
using System.Globalization;
using System.Resources;

namespace PDFKeeper.Core.Helpers
{
    public static class ResourceHelper
    {
        /// <summary>
        /// Gets a string containing the resource with <c>{0}</c> and <c>{1}</c> being replaced by
        /// the specified arguments.
        /// </summary>
        /// <param name="resourceManager">
        /// The <see cref="ResourceManager"/> instance.
        /// </param>
        /// <param name="resource">
        /// The string that represents the resource.
        /// </param>
        /// <param name="arg1">
        /// The argument that replaces <c>{0}</c> in the resource string.
        /// </param>
        /// <param name="arg2">
        /// The optional argument that replaces <c>{1}</c> in the resource string.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string GetString(
            ResourceManager resourceManager,
            string resource,
            string arg1,
            string arg2 = null)
        {
            if (resourceManager is null)
            {
                throw new ArgumentNullException(nameof(resourceManager));
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                resourceManager.GetString(
                    resource,
                    CultureInfo.CurrentCulture),
                arg1,
                arg2);
        }
    }
}
