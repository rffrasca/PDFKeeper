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

using PDFKeeper.Core.Models;

namespace PDFKeeper.Core.Interfaces.Rules
{
    /// <summary>
    /// Defines a contract for validation rules that evaluate input values and return
    /// a <see cref="RuleResult"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value to evaluate.
    /// </typeparam>
    public interface IRule<in T>
    {
        /// <summary>
        /// Evaluates the rule against the specified input value.
        /// </summary>
        /// <param name="value">
        /// The value to evaluate.
        /// </param>
        /// <returns>
        /// A <see cref="RuleResult"/> describing the outcome of the evaluation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is null.
        /// </exception>
        RuleResult Evaluate(T value);
    }
}
