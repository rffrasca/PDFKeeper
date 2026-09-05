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

namespace PDFKeeper.Core.Models
{
    /// <summary>
    /// Represents the settings associated with an Upload Profile, including metadata
    /// fields and processing options applied when uploading a document.
    ///
    /// NOTE: This class defines the XML‑serialized Upload Profile format. Its structure
    /// and property names must remain unchanged to preserve backward compatibility with
    /// existing Upload Profile XML files.
    ///
    /// This type is intended for use exclusively by <see cref="IUploadProfileManager"/>
    /// for loading and saving Upload Profiles, and by <see cref="UploadProfileEditorViewModel"/>
    /// for editing them. It should not be referenced by any other application logic.
    /// </summary>
    [Serializable()]
    public class UploadProfile
    {
        /// <summary>
        /// Gets or sets the document title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the document author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the document subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the document keywords.
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the document category assigned during upload.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the tax year associated with the document.
        /// </summary>
        public string TaxYear { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the document should be flagged.
        /// </summary>
        public bool FlagDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OCR should be performed on both
        /// text and image data pages.
        /// </summary>
        public bool OcrPdfTextAndImageDataPages { get; set; }
    }
}
