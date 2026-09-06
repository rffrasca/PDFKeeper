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

using System;

namespace PDFKeeper.Core.Rules.ExportPdfMetadata
{
    /// <summary>
    /// Specific implementation of the <see cref="IRule{T}"/> interface that verifies the property
    /// values of a <c>PdfMetadataDto</c> match the corresponding properties of a
    /// <c>DocumentDto</c> provided within an <see cref="ExportPdfMetadataRuleInput"/> instance.
    /// </summary>
    public sealed class ExportPdfMetadataRule : IRule<ExportPdfMetadataRuleInput>
    {
        public RuleResult Evaluate(ExportPdfMetadataRuleInput value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.PdfMetadata.Title != value.Document.Title ||
                value.PdfMetadata.Author != value.Document.Author ||
                value.PdfMetadata.Subject != value.Document.Subject ||
                value.PdfMetadata.Keywords != value.Document.Keywords)
            {
                return new RuleResult(true);
            }

            return new RuleResult(false);
        }
    }
}
