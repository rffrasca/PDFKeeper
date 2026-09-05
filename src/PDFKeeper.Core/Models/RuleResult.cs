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

namespace PDFKeeper.Core.Models
{
    /// <summary>
    /// Represents the result of evaluating a validation rule, including whether a
    /// violation was detected and an optional message describing the violation.
    /// </summary>
    public sealed class RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleResult"/> class.
        /// </summary>
        /// <param name="violationFound">
        /// <c>true</c> if the rule evaluation detected a violation; otherwise, <c>false</c>.
        /// </param>
        /// <param name="violationMessage">
        /// The message describing the violation, or <c>null</c> if no violation occurred.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public RuleResult(bool violationFound, string violationMessage = null)
#pragma warning restore IDE0290 // Use primary constructor
        {
            ViolationFound = violationFound;
            ViolationMessage = violationMessage;
        }

        /// <summary>
        /// Gets a value indicating whether the rule evaluation detected a violation.
        /// </summary>
        public bool ViolationFound { get; }

        /// <summary>
        /// Gets the message describing the violation, or <c>null</c> if no violation occurred.
        /// </summary>
        public string ViolationMessage { get; }
    }
}
