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

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PDFKeeper.Core.Extensions
{
    internal static class DataTableExtension
    {
        /// <summary>
        /// Compares the <see cref="DataTable"/> object to a second <see cref="DataTable"/> object
        /// to determine if any differences exist between the two <see cref="DataTable"/> objects.
        /// </summary>
        /// <param name="dataTable1">The <see cref="DataTable"/> object.</param>
        /// <param name="dataTable2">The <see cref="DataTable"/> object to compare against.</param>
        /// <returns>
        /// <c>true</c> or <c>false</c> if differences exist between the two
        /// <see cref="DataTable"/> objects.
        /// </returns>
        internal static bool Compare(this DataTable dataTable1, DataTable dataTable2)
        {
            var diffsExist = false;

            if (dataTable1.Rows.Count.Equals(dataTable2.Rows.Count))
            {
                var set1 = new HashSet<string>(dataTable1.AsEnumerable().Select(row => string.Join(
                ",",
                row.ItemArray)));
                var set2 = new HashSet<string>(dataTable2.AsEnumerable().Select(row => string.Join(
                    ",",
                    row.ItemArray)));
                set1.Except(set2).ToList().ForEach(diff => diffsExist = true);
                set2.Except(set2).ToList().ForEach(diff => diffsExist = true);
            }
            else
            {
                diffsExist = true;
            }
            
            return diffsExist;
        }
    }
}
