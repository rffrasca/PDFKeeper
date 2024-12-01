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

using System;
using System.Collections.Generic;

namespace PDFKeeper.Core.Models
{
    internal class TaxYear
    {
        /// <summary>
        /// Gets tax years starting with the last 10 years and 1 year into the future.
        /// </summary>
        /// <returns>The collection.</returns>
        internal static IEnumerable<string> GetElevenYearRange()
        {
            var tempYears = new List<string>();
            var thisYear = DateTime.Now.Year;
            var x = thisYear - 10;
            while (x <= thisYear)
            {
                x++;
                tempYears.Add(x.ToString());
            }
            tempYears.Reverse();
            yield return string.Empty;
            foreach (string year in tempYears)
            {
                yield return year;
            }
        }
    }
}
