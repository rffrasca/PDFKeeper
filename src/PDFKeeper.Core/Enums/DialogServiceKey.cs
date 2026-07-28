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

namespace PDFKeeper.Core.Enums
{
    /// <summary>
    /// Defines the unique keys used to resolve specific <see cref="IDialogService"/>
    /// implementations from the dependency injection container. Each dialog in
    /// the application is registered under one of these keys, allowing ViewModels
    /// and other components to explicitly request the correct dialog service.
    /// </summary>
    public enum DialogServiceKey
    {
        /// <summary>
        /// Dialog for adding a new PDF document to the database.
        /// </summary>
        AddPdf,

        /// <summary>
        /// Dialog for editing the document title.
        /// </summary>
        SetTitle,

        /// <summary>
        /// Dialog for editing the document author.
        /// </summary>
        SetAuthor,

        /// <summary>
        /// Dialog for editing the document subject.
        /// </summary>
        SetSubject,

        /// <summary>
        /// Dialog for editing the document category.
        /// </summary>
        SetCategory,

        /// <summary>
        /// Dialog for editing the document tax year.
        /// </summary>
        SetTaxYear,

        /// <summary>
        /// Dialog for editing the date and time the document was added.
        /// </summary>
        SetDateTimeAdded,

        /// <summary>
        /// Dialog for editing the preview pixel density setting.
        /// </summary>
        SetPreviewPixelDensity,

        /// <summary>
        /// Dialog for editing application options and preferences.
        /// </summary>
        Options,

        /// <summary>
        /// Dialog for editing upload profiles.
        /// </summary>
        UploadProfileEditor,

        /// <summary>
        /// Dialog for displaying application information and credits.
        /// </summary>
        AboutBox
    }
}
