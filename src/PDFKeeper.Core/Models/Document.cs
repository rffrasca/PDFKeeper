// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2025 Robert F. Frasca
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
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Keywords { get; set; }
        public string Added { get; set; }
        public string Notes { get; set; }
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Pdf { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public string Category { get; set; }
        public int Flag { get; set; }
        public string TaxYear { get; set; }
        public string TextAnnotations { get; set; }
        public string Text { get; set; }
        public string SearchTermSnippets { get; set; }
    }
}
