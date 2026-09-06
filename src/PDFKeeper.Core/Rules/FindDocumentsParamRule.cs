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

using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using System;

namespace PDFKeeper.Core.Rules
{
    /// <summary>
    /// Specific implementation of the <see cref="IRule{T}"/> interface that
    /// evaluates a <see cref="FindDocumentsParam"/> instance.
    /// </summary>
    public sealed class FindDocumentsParamRule : IRule<FindDocumentsParam>
    {
        public RuleResult Evaluate(FindDocumentsParam value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.FindBySearchTermChecked.Equals(false) &&
                value.FindBySelectionsChecked.Equals(false) &&
                value.FindByDateAddedChecked.Equals(false) &&
                value.FindFlaggedDocumentsChecked.Equals(false) &&
                value.AllDocumentsChecked.Equals(false))
            {
                return new RuleResult(true, Resources.OneFindFunctionMustBeTrue);
            }
            else if (value.FindBySearchTermChecked && string.IsNullOrEmpty(value.SearchTerm))
            {
                return new RuleResult(true, Resources.SearchTermCannotBeBlank);
            }
            else if (value.FindBySearchTermChecked &&
                IsSearchTermSyntaxCorrect(value.SearchTerm).Equals(false))
            {
                return new RuleResult(true, Resources.SearchTermSyntaxIncorrect);
            }
            else if (value.FindBySelectionsChecked &&
                string.IsNullOrEmpty(value.Author) &&
                string.IsNullOrEmpty(value.Subject) &&
                string.IsNullOrEmpty(value.Category) &&
                string.IsNullOrEmpty(value.TaxYear))
            {
                return new RuleResult(true, Resources.CommonFieldsCannotAllBeBlank);
            }
            else if (value.FindByDateAddedChecked && string.IsNullOrEmpty(value.DateAdded))
            {
                return new RuleResult(true, Resources.DateAddedCannotBeBlank);
            }

            return new RuleResult(false);
        }

        /// <summary>
        /// Evaluates the syntax of the search term based on the database platform
        /// currently in use.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term to evaluate.
        /// </param>
        /// <returns>
        /// <c>true</c> if the syntax is correct; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSearchTermSyntaxCorrect(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return false;
            }

#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (DatabaseSession.PlatformName)
            {
                case DatabaseSession.CompatiblePlatformName.Oracle:
                    return IsSearchTermSyntaxCorrectForOracle(searchTerm);
                case DatabaseSession.CompatiblePlatformName.Sqlite:
                    return IsSearchTermSyntaxCorrectForSqlite(searchTerm);
                case DatabaseSession.CompatiblePlatformName.SqlServer:
                    return IsSearchTermSyntaxCorrectForSqlServer(searchTerm);
                case DatabaseSession.CompatiblePlatformName.MySql:
                    return IsSearchTermSyntaxCorrectForMySql(searchTerm);
            }
#pragma warning restore IDE0066 // Convert switch statement to expression

            return false;
        }

        /// <summary>
        /// Evaluates the syntax of the search term for the Oracle database platform.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term to evaluate.
        /// </param>
        /// <returns>
        /// <c>true</c> if the syntax is correct; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSearchTermSyntaxCorrectForOracle(string searchTerm)
        {
            var result = searchTerm.Equals("MINUS", StringComparison.Ordinal)
                || searchTerm.Equals("NEAR", StringComparison.Ordinal)
                || searchTerm.Equals("NOT", StringComparison.Ordinal)
                || searchTerm.Equals("AND", StringComparison.Ordinal)
                || searchTerm.Equals("EQUIV", StringComparison.Ordinal)
                || searchTerm.Equals("WITHIN", StringComparison.Ordinal)
                || searchTerm.Equals("OR", StringComparison.Ordinal)
                || searchTerm.Equals("ACCUM", StringComparison.Ordinal)
                || searchTerm.Equals("FUZZY", StringComparison.Ordinal)
                || searchTerm.Equals("ABOUT", StringComparison.Ordinal)
                || searchTerm.StartsWith("MINUS ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("NEAR ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("NOT ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("AND ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("EQUIV ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("WITHIN ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("OR ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("ACCUM ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("FUZZY ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("ABOUT ", StringComparison.CurrentCulture)
                || searchTerm.StartsWith("*", StringComparison.CurrentCulture)
                || searchTerm.IndexOf("{}", StringComparison.Ordinal) != -1
                || searchTerm.IndexOf("()", StringComparison.Ordinal) != -1
                || searchTerm.Substring(0, 1).Equals("=", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals(";", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals(">", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals("-", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals("~", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals("&", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals("|", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals(",", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals("!", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals("{", StringComparison.Ordinal)
                || searchTerm.Substring(0, 1).Equals("(", StringComparison.Ordinal)
                || searchTerm.Equals("?", StringComparison.Ordinal)
                || searchTerm.Equals("$", StringComparison.Ordinal);
            return !result;
        }

        /// <summary>
        /// Evaluates the syntax of the search term for the SQLite database platform.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term to evaluate.
        /// </param>
        /// <returns>
        /// <c>true</c> if the syntax is correct; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSearchTermSyntaxCorrectForSqlite(string searchTerm)
        {
            var result = searchTerm.Contains("&")
                || searchTerm.Contains("!")
                || searchTerm.Contains("?")
                || searchTerm.Contains("/")
                || searchTerm.Contains("\"")
                || searchTerm.Contains(",")
                || searchTerm.StartsWith("*", StringComparison.CurrentCulture);
            return !result;
        }

        /// <summary>
        /// Evaluates the syntax of the search term for the SQL Server database platform.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term to evaluate.
        /// </param>
        /// <returns>
        /// <c>true</c> if the syntax is correct; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSearchTermSyntaxCorrectForSqlServer(string searchTerm)
        {
            var result = searchTerm.Equals("%", StringComparison.Ordinal)
                || searchTerm.Equals("?", StringComparison.Ordinal)
                || searchTerm.StartsWith("not", StringComparison.CurrentCultureIgnoreCase);
            return !result;
        }

        /// <summary>
        /// Evaluates the syntax of the search term for the MySQL database platform.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term to evaluate.
        /// </param>
        /// <returns>
        /// <c>true</c> if the syntax is correct; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSearchTermSyntaxCorrectForMySql(string searchTerm)
        {
            var result = searchTerm.Contains("%") || searchTerm.Contains("?");
            return !result;
        }
    }
}
