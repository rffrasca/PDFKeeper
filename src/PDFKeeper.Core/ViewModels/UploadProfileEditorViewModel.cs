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

using System.Collections.Generic;
using PDFKeeper.Core.Models;

namespace PDFKeeper.Core.ViewModels
{
    public class UploadProfileEditorViewModel : CommonCollectionsViewModel, IUploadProfile
    {
        private string name;
        private readonly UploadProfile uploadProfile;
        private IEnumerable<string> titleTokens;

        /// <summary>
        /// Initializes a new instance of the UploadProfileEditorViewModel class.
        /// </summary>
        /// <param name="uploadProfileName">
        /// The upload profile name or null when editing a new upload profile.
        /// </param>
        /// <param name="uploadProfile">The UploadProfile object.</param>
        public UploadProfileEditorViewModel(string uploadProfileName, UploadProfile uploadProfile)
        {
            name = uploadProfileName;
            this.uploadProfile = uploadProfile;
        }

        public UploadProfile UploadProfile => uploadProfile;

        public IEnumerable<string> TitleTokens
        {
            get => titleTokens;
            set => SetProperty(ref titleTokens, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Category
        {
            get => uploadProfile.Category;
            set => uploadProfile.Category = value;
        }

        public string TaxYear
        {
            get => uploadProfile.TaxYear;
            set => uploadProfile.TaxYear = value;
        }

        public bool FlagDocument
        {
            get => uploadProfile.FlagDocument;
            set => uploadProfile.FlagDocument = value;
        }

        public bool OcrPdfTextAndImageDataPages
        {
            get => uploadProfile.OcrPdfTextAndImageDataPages;
            set => uploadProfile.OcrPdfTextAndImageDataPages = value;
        }

        public string Title
        {
            get => uploadProfile.Title;
            set => uploadProfile.Title = value;
        }

        public string Author
        {
            get => uploadProfile.Author;
            set => uploadProfile.Author = value;
        }

        public string Subject
        {
            get => uploadProfile.Subject;
            set
            {
                uploadProfile.Subject = value;
                OnPropertyChanged();
            }
        }

        public string Keywords
        {
            get => uploadProfile.Keywords;
            set => uploadProfile.Keywords = value;
        }
    }
}
