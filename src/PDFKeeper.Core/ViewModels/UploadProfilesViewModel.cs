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

using System.Collections.Generic;

namespace PDFKeeper.Core.ViewModels
{
    public class UploadProfilesViewModel : ViewModelBase
    {
        private IEnumerable<string> uploadProfileNames;
        private bool editEnabled;
        private bool deleteEnabled;

        public string UploadProfilesDirectoryPath { get; set; }

        public IEnumerable<string> UploadProfileNames
        {
            get => uploadProfileNames;
            set => SetProperty(ref uploadProfileNames, value);
        }

        public string CurrentUploadProfileName { get; set; }

        public bool EditEnabled
        {
            get => editEnabled;
            set => SetProperty(ref editEnabled, value);
        }

        public bool DeleteEnabled
        {
            get => deleteEnabled;
            set => SetProperty(ref deleteEnabled, value);
        }
    }
}
