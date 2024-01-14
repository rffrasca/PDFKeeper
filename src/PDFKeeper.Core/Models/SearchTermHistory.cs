// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

namespace PDFKeeper.Core.Models
{
    internal class SearchTermHistory
    {       
        private readonly List<string> searchTerms;

        internal SearchTermHistory()
        {
            searchTerms = new List<string>();
        }

        /// <summary>
        /// Gets previously entered search terms.
        /// </summary>
        internal IEnumerable<string> SearchTerms
        {
            get
            {
                yield return string.Empty;
                foreach (string searchTerm in searchTerms)
                {
                    yield return searchTerm;
                }
            }
        }

        /// <summary>
        /// Adds the search term to the search term history.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        internal void Add(string searchTerm)
        {
            if (searchTerms.Contains(searchTerm).Equals(false))
            {
                searchTerms.Add(searchTerm);
            }
        }
    }
}
