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

namespace PDFKeeper.Core.Rules.PdfMetadata
{
    /// <summary>
    /// Represents the input for a PDF metadata rule, including title, author, and subject.
    /// </summary>
    public sealed class PdfMetadataRuleInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfMetadataRuleInput"/> class.
        /// </summary>
        /// <param name="title">
        /// The title of the PDF.
        /// </param>
        /// <param name="author">
        /// The author of the PDF.
        /// </param>
        /// <param name="subject">
        /// The subject of the PDF.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfMetadataRuleInput(string title, string author, string subject)
#pragma warning restore IDE0290 // Use primary constructor
        {
            Title = title;
            Author = author;
            Subject = subject;
        }

        /// <summary>
        /// Gets the title of the PDF.
        /// </summary>
        public string Title { get; }
        
        /// <summary>
        /// Gets the author of the PDF.
        /// </summary>
        public string Author { get; }
        
        /// <summary>
        /// Gets the subject of the PDF.
        /// </summary>
        public string Subject { get; }
    }
}
