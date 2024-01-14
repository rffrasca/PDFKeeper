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

namespace PDFKeeper.Core.Models
{
    public class FindDocumentsParam : IFindDocumentsParam
    {
        private bool findBySearchTermChecked;
        private bool findBySelectionsChecked;
        private bool findByDateAddedChecked;
        private bool findFlaggedDocumentsChecked;
        private bool allDocumentsChecked;

        public bool FindBySearchTermChecked
        {
            get => findBySearchTermChecked;
            set
            {
                findBySearchTermChecked = value;
                if (findBySearchTermChecked)
                {
                    FindBySelectionsChecked = false;
                    FindByDateAddedChecked = false;
                    FindFlaggedDocumentsChecked = false;
                    AllDocumentsChecked = false;
                }
            }
        }

        public string SearchTerm { get; set; }
        
        public bool FindBySelectionsChecked
        {
            get => findBySelectionsChecked;
            set
            {
                findBySelectionsChecked = value;
                if (findBySelectionsChecked)
                {
                    FindBySearchTermChecked = false;
                    FindByDateAddedChecked = false;
                    FindFlaggedDocumentsChecked = false;
                    AllDocumentsChecked = false;
                }
            }
        }
        
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Category { get; set; }
        public string TaxYear { get; set; }
        
        public bool FindByDateAddedChecked
        {
            get => findByDateAddedChecked;
            set
            {
                findByDateAddedChecked = value;
                if (findByDateAddedChecked)
                {
                    FindBySearchTermChecked = false;
                    FindBySelectionsChecked = false;
                    FindFlaggedDocumentsChecked = false;
                    AllDocumentsChecked = false;
                }
            }
        }

        public string DateAdded { get; set; }
        
        public bool FindFlaggedDocumentsChecked
        {
            get => findFlaggedDocumentsChecked;
            set
            {
                findFlaggedDocumentsChecked = value;
                if (findFlaggedDocumentsChecked)
                {
                    FindBySearchTermChecked = false;
                    FindBySelectionsChecked = false;
                    FindByDateAddedChecked = false;
                    AllDocumentsChecked = false;
                }
            }
        }
        
        public bool AllDocumentsChecked
        {
            get => allDocumentsChecked;
            set
            {
                allDocumentsChecked = value;
                if (allDocumentsChecked)
                {
                    FindBySearchTermChecked = false;
                    FindBySelectionsChecked = false;
                    FindByDateAddedChecked = false;
                    FindFlaggedDocumentsChecked = false;
                }
            }
        }
    }
}
