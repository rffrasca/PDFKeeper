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

using PDFKeeper.Core.Properties;
using System.Globalization;

namespace PDFKeeper.Core.Helpers
{
    internal static class ResourceHelper
    {
        /// <summary>
        /// Gets a string containing the resource with {0} and {1} being replaced by the specified
        /// arguments.
        /// </summary>
        /// <param name="resource">The string that represents the resource.</param>
        /// <param name="arg1">The argument that replaces {0} in the resource string.</param>
        /// <param name="arg2">
        /// The argument that replaces {1} in the resource string or null.
        /// </param>
        /// <returns>The formatted string.</returns>
        internal static string GetString(string resource, string arg1, string arg2)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.ResourceManager.GetString(
                resource, CultureInfo.CurrentCulture), arg1, arg2);
        }
    }
}
