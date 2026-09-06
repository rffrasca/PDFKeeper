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

using PDFKeeper.Core.Properties;
using System;

namespace PDFKeeper.Core.Rules.PdfMetadata
{
    /// <summary>
    /// Specific implementation of the <see cref="IRule{T}"/> interface
    /// that detects if the Title, Author, and Subject properties are
    /// blank in the <see cref="PdfMetadataRuleInput"/> instance.
    /// </summary>
    public sealed class PdfMetadataRule : IRule<PdfMetadataRuleInput>
    {
        public RuleResult Evaluate(PdfMetadataRuleInput value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrEmpty(value.Title) ||
                string.IsNullOrEmpty(value.Author) ||
                string.IsNullOrEmpty(value.Subject))
            {
                return new RuleResult(true, Resources.MandatoryFieldsAreBlank);
            }

            return new RuleResult(false);
        }
    }
}
