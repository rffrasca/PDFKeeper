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

using PDFKeeper.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PDFKeeper.Core.Models
{
    internal static class ColumnData
    {
        /// <summary>
        /// Gets authors by subject, category, and tax year from the repository.
        /// </summary>
        /// <param name="subject">The subject or null.</param>
        /// <param name="category">The category or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The array of authors in ascending order.</returns>
        internal static string[] GetAuthors(string subject, string category, string taxYear)
        {
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                return GetColumnData(
                    documentRepository.GetAuthors(
                        subject,
                        category,
                        taxYear)).OrderBy(author => author).ToArray();
            }
        }

        /// <summary>
        /// Gets subjects by author, category, and tax year from the repository.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="category">The category or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The array of subjects in ascending order.</returns>
        internal static string[] GetSubjects(string author, string category, string taxYear)
        {
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                return GetColumnData(
                    documentRepository.GetSubjects(
                        author,
                        category,
                        taxYear)).OrderBy(subject => subject).ToArray();
            }
        }

        /// <summary>
        /// Gets categories by author, subject, and tax year from the repository.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="subject">The subject or null.</param>
        /// <param name="taxYear">The tax year or null.</param>
        /// <returns>The array of categories in ascending order.</returns>
        internal static string[] GetCategories(string author, string subject, string taxYear)
        {
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                return GetColumnData(
                    documentRepository.GetCategories(
                        author,
                        subject,
                        taxYear)).OrderBy(category => category).ToArray();
            }
        }

        /// <summary>
        /// Gets tax years by author, subject, and category from the repository.
        /// </summary>
        /// <param name="author">The author or null.</param>
        /// <param name="subject">The subject or null.</param>
        /// <param name="category">The category or null.</param>
        /// <returns>The array of tax years in ascending order.</returns>
        internal static string[] GetTaxYears(string author, string subject, string category)
        {
            using (var documentRepository = DatabaseSession.GetDocumentRepository())
            {
                return GetColumnData(
                    documentRepository.GetTaxYears(
                        author,
                        subject,
                        category)).OrderBy(taxYear => taxYear).ToArray();
            }
        }

        /// <summary>
        /// Gets a range of tax years starting with the last 25 years and 1 year into the future.
        /// </summary>
        /// <returns>The array of tax years in descending order.</returns>
        internal static string[] GetRangeOfTaxYears() => GetRangeOfTaxYearsInternal().ToArray();

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

        private static IEnumerable<string> GetRangeOfTaxYearsInternal()
        {
            var tempYears = new List<string>();
            var thisYear = DateTime.Now.Year;
            var x = thisYear - 25;
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
