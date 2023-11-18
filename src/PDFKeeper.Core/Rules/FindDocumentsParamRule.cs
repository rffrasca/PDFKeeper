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

using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PDFKeeper.Core.Rules
{
    internal class FindDocumentsParamRule : RuleBase
    {
        private readonly FindDocumentsParam findDocumentsParam;

        /// <summary>
        /// <para>Initializes a new instance of the FindDocumentsParamRule class that:</para>
        /// <list type="bullet">
        /// Verifies that FindBySearchTermChecked, FindBySelectionsChecked, FindByDateAddedChecked,
        /// FindFlaggedDocumentsChecked, and AllDocumentsChecked are all not set to false.
        /// </list>
        /// <list type="bullet">
        /// Verifies the length of the SearchTerm property is > 0 when the FindBySearchTermChecked
        /// property is set to true.
        /// </list>
        /// <list type="bullet">
        /// Verifies the length of the Author, Subject, Category, and TaxYear properties is > 0
        /// when the FindBySelectionsChecked property is set to true.
        /// </list>
        /// <list type="bullet">
        /// Verifies the length of the DateAdded property is > 0 when the FindByDateAddedChecked
        /// property is set to true.
        /// </list>
        /// </summary>
        /// <param name="findDocumentsParam">The FindDocumentsParam object.</param>
        internal FindDocumentsParamRule(FindDocumentsParam findDocumentsParam)
        {
            this.findDocumentsParam = findDocumentsParam;
            CheckForViolation();
        }

        protected override void CheckForViolation()
        {
            if (findDocumentsParam.FindBySearchTermChecked.Equals(false) &&
                findDocumentsParam.FindBySelectionsChecked.Equals(false) &&
                findDocumentsParam.FindByDateAddedChecked.Equals(false) &&
                findDocumentsParam.FindFlaggedDocumentsChecked.Equals(false) &&
                findDocumentsParam.AllDocumentsChecked.Equals(false))
            {
                ViolationFound = true;
                ViolationMessage = Resources.OneFindFunctionMustBeTrue;
            }
            else if (findDocumentsParam.FindBySearchTermChecked &&
                string.IsNullOrEmpty(findDocumentsParam.SearchTerm))
            {
                ViolationFound = true;
                ViolationMessage = Resources.SearchTermCannotBeBlank;
            }
            else if (findDocumentsParam.FindBySearchTermChecked && IsSearchTermSyntaxCorrect(
                findDocumentsParam.SearchTerm).Equals(false))
            {
                ViolationFound = true;
                ViolationMessage = Resources.SearchTermSyntaxIncorrect;
            }
            else if (findDocumentsParam.FindBySelectionsChecked &&
                string.IsNullOrEmpty(findDocumentsParam.Author) &&
                string.IsNullOrEmpty(findDocumentsParam.Subject) &&
                string.IsNullOrEmpty(findDocumentsParam.Category) &&
                string.IsNullOrEmpty(findDocumentsParam.TaxYear))
            {
                ViolationFound = true;
                ViolationMessage = Resources.CommonFieldsCannotAllBeBlank;
            }
            else if (findDocumentsParam.FindByDateAddedChecked &&
                string.IsNullOrEmpty(findDocumentsParam.DateAdded))
            {
                ViolationFound = true;
                ViolationMessage = Resources.DateAddedCannotBeBlank;
            }
            else
            {
                ViolationFound = false;
            }
        }

        private static bool IsSearchTermSyntaxCorrect(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return false;
            }
            var result = false;
            if (DatabaseSession.PlatformName.Equals(DatabaseSession.CompatiblePlatformName.Oracle))
            {
                result = IsSearchTermSyntaxCorrectForOracle(searchTerm);
            }
            else if (DatabaseSession.PlatformName.Equals(
                DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                result = IsSearchTermSyntaxCorrectForSqlite(searchTerm);
            }
            return result;
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private static bool IsSearchTermSyntaxCorrectForOracle(string searchTerm)
        {
            if (searchTerm.Equals("MINUS", StringComparison.Ordinal) ||
                searchTerm.Equals("NEAR", StringComparison.Ordinal) ||
                searchTerm.Equals("NOT", StringComparison.Ordinal) ||
                searchTerm.Equals("AND", StringComparison.Ordinal) ||
                searchTerm.Equals("EQUIV", StringComparison.Ordinal) ||
                searchTerm.Equals("WITHIN", StringComparison.Ordinal) ||
                searchTerm.Equals("OR", StringComparison.Ordinal) ||
                searchTerm.Equals("ACCUM", StringComparison.Ordinal) ||
                searchTerm.Equals("FUZZY", StringComparison.Ordinal) ||
                searchTerm.Equals("ABOUT", StringComparison.Ordinal) ||
                searchTerm.StartsWith("MINUS ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("NEAR ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("NOT ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("AND ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("EQUIV ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("WITHIN ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("OR ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("ACCUM ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("FUZZY ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("ABOUT ", StringComparison.CurrentCulture) ||
                searchTerm.StartsWith("*", StringComparison.CurrentCulture) ||
                searchTerm.IndexOf("{}", StringComparison.Ordinal) != -1 ||
                searchTerm.IndexOf("()", StringComparison.Ordinal) != -1 ||
                searchTerm.Substring(0, 1).Equals("=", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals(";", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals(">", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals("-", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals("~", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals("&", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals("|", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals(",", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals("!", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals("{", StringComparison.Ordinal) ||
                searchTerm.Substring(0, 1).Equals("(", StringComparison.Ordinal) ||
                searchTerm.Equals("?", StringComparison.Ordinal) ||
                searchTerm.Equals("$", StringComparison.Ordinal))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsSearchTermSyntaxCorrectForSqlite(string searchTerm)
        {
            if (searchTerm.Contains("&") || searchTerm.Contains("!") || searchTerm.Contains("?") ||
                searchTerm.Contains("/") || searchTerm.Contains("\"") || searchTerm.Contains(",") ||
                searchTerm.StartsWith("*", StringComparison.CurrentCulture))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
