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
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Properties;
using System;

namespace PDFKeeper.Core.Rules
{
    /// <summary>
    /// Specific implementation of the <see cref="IRule{T}"/> interface that verifies the size of
    /// the specified notes string does not exceed the data length of the <c>DOC_NOTES</c> column
    /// in the database when the <see cref="DatabaseSession.PlatformName"/> is
    /// <see cref="DatabaseSession.CompatiblePlatformName.Oracle"/>.
    /// </summary>
    public sealed class NotesSizeRule : IRule<string>
    {
        public RuleResult Evaluate(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (DatabaseSession.PlatformName.Equals(DatabaseSession.CompatiblePlatformName.Oracle))
            {
                int columnLength;

                using (var documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    columnLength = documentRepository.GetNotesColumnDataLength();
                }

                var size = value.GetByteCount();

                if (size > columnLength)
                {
                    var message = ResourceHelper.GetString(
                        Resources.ResourceManager,
                        "NotesSizeTooLarge",
                        size.ToString(),
                        columnLength.ToString());
                    return new RuleResult(true, message);
                }
            }

            return new RuleResult(false);
        }
    }
}
