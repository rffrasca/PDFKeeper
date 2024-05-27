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
using PDFKeeper.Core.DataAccess.Repository;
using PDFKeeper.Core.Helpers;

namespace PDFKeeper.Core.Rules
{
    internal class NotesLengthRule : RuleBase
    {
        private readonly string notes;

        /// <summary>
        /// Initializes a new instance of the NotesLengthRule class that verifies the length of the
        /// Notes string does not exceed the maximum length of the DOC_NOTES column in the
        /// database when the database platform is Oracle.
        /// </summary>
        /// <param name="notes">The Notes string.</param>
        internal NotesLengthRule(string notes)
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
                var columnLength = DocumentRepositoryFactory.Instance.GetNotesColumnDataLength();
                if (notes.Length > columnLength)
                {
                    ViolationFound = true;
                    ViolationMessage = ResourceHelper.GetString(
                        "NotesLengthTooLarge",
                        notes.Length.ToString(),
                        columnLength.ToString());
                }
            }
        }
    }
}
