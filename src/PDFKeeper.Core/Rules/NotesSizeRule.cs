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

using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Helpers;

namespace PDFKeeper.Core.Rules
{
    internal class NotesSizeRule : RuleBase
    {
        private readonly string notes;

        /// <summary>
        /// Initializes a new instance of the NotesSizeRule class that verifies the size of the
        /// Notes string does not exceed the data length of the DOC_NOTES column in the database
        /// when the database platform is Oracle.
        /// </summary>
        /// <param name="notes">The Notes string.</param>
        internal NotesSizeRule(string notes)
        {
            this.notes = notes;
            CheckForViolation();
        }

        protected override void CheckForViolation()
        {
            ViolationFound = false;
            ViolationMessage = null;
            if (DatabaseSession.PlatformName.Equals(DatabaseSession.CompatiblePlatformName.Oracle))
            {
                int columnLength;
                using (var documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    columnLength = documentRepository.GetNotesColumnDataLength();
                }
                var size = notes.GetByteCount();
                if (size > columnLength)
                {
                    ViolationFound = true;
                    ViolationMessage = ResourceHelper.GetString(
                        "NotesSizeTooLarge",
                        size.ToString(),
                        columnLength.ToString());
                }
            }
        }
    }
}
