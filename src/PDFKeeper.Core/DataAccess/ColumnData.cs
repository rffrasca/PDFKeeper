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

using PDFKeeper.Core.DataAccess.Repository;
using System.Collections.Generic;
using System.Data;

namespace PDFKeeper.Core.DataAccess
{
    internal class ColumnData
    {
        /// <summary>
        /// Gets authors by subject, category, and tax year.
        /// </summary>
        /// <param name="subject">The subject or null.</param>
        /// <param name="category">The category or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The collection.</returns>
        internal static IEnumerable<string> GetAuthors(string subject, string category,
            string taxYear)
        {
            return GetColumnData(DocumentRepositoryFactory.Instance.GetAuthors(subject, category,
                taxYear));
        }

        /// <summary>
        /// Gets subjects by author, category, and tax year.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="category">The category or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The collection.</returns>
        internal static IEnumerable<string> GetSubjects(string author, string category,
            string taxYear)
        {
            return GetColumnData(DocumentRepositoryFactory.Instance.GetSubjects(author, category,
                taxYear));
        }

        /// <summary>
        /// Gets categories by author, subject, and tax year.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="subject">The subject or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The collection.</returns>
        internal static IEnumerable<string> GetCategories(string author, string subject,
            string taxYear)
        {
            return GetColumnData(DocumentRepositoryFactory.Instance.GetCategories(author, subject,
                taxYear));
        }

        /// <summary>
        /// Gets tax years by author, subject, and category.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="subject">The subject or null.</param>
        /// <param name="category">The category or null.</param>
        /// <returns>The collection.</returns>
        internal static IEnumerable<string> GetTaxYears(string author, string subject,
            string category)
        {
            return GetColumnData(DocumentRepositoryFactory.Instance.GetTaxYears(author, subject,
                category));
        }

        private static IEnumerable<string> GetColumnData(DataTable dataTable)
        {
            var columnName = dataTable.Columns[0].ColumnName;
            yield return string.Empty;
            foreach (DataRow row in dataTable.Rows)
            {
                var item = row[columnName].ToString();
                if (item.Length > 0)
                {
                    yield return item;
                }
            }
        }
    }
}
