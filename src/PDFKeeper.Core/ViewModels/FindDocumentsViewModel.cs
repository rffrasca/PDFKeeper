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

using PDFKeeper.Core.Models;
using System.Collections.Generic;

namespace PDFKeeper.Core.ViewModels
{
    public class FindDocumentsViewModel : CommonCollectionsViewModel, IFindDocumentsParam
    {
        private FindDocumentsParam findDocumentsParam;
        private bool searchTermEnabled;
        private IEnumerable<string> searchTerms;
        private bool authorEnabled;
        private bool subjectEnabled;
        private bool categoryEnabled;
        private bool taxYearEnabled;
        private bool dateAddedEnabled;
        private bool allDocumentsEnabled;

        public FindDocumentsViewModel()
        {
            findDocumentsParam = new FindDocumentsParam();
        }

        public FindDocumentsParam FindDocumentsParam
        {
            get => findDocumentsParam;
            set
            {
                findDocumentsParam = value;
                OnPropertyChanged("FindBySearchTermChecked");
                OnPropertyChanged("SearchTerm");
                OnPropertyChanged("FindBySelectionsChecked");
                OnPropertyChanged("Author");
                OnPropertyChanged("Subject");
                OnPropertyChanged("Category");
                OnPropertyChanged("TaxYear");
                OnPropertyChanged("FindByDateAddedChecked");
                OnPropertyChanged("DateAdded");
                OnPropertyChanged("FindFlaggedDocumentsChecked");
                OnPropertyChanged("AllDocumentsChecked");
            }
        }

        public bool FindBySearchTermChecked
        {
            get => findDocumentsParam.FindBySearchTermChecked;
            set
            {
                findDocumentsParam.FindBySearchTermChecked = value;
                OnPropertyChanged();
                SearchTermEnabled = value;
            }
        }

        public bool SearchTermEnabled
        {
            get => searchTermEnabled;
            set => SetProperty(ref searchTermEnabled, value);
        }

        public IEnumerable<string> SearchTerms
        {
            get => searchTerms;
            set => SetProperty(ref searchTerms, value);
        }

        public string SearchTerm
        {
            get => findDocumentsParam.SearchTerm;
            set
            {
                findDocumentsParam.SearchTerm = value;
                OnPropertyChanged();
            }
        }

        public bool FindBySelectionsChecked
        {
            get => findDocumentsParam.FindBySelectionsChecked;
            set
            {
                findDocumentsParam.FindBySelectionsChecked = value;
                OnPropertyChanged();
                AuthorEnabled = value;
                SubjectEnabled = value;
                CategoryEnabled = value;
                TaxYearEnabled = value;
            }
        }

        public bool AuthorEnabled
        {
            get => authorEnabled;
            set => SetProperty(ref authorEnabled, value);
        }

        public string Author
        {
            get => findDocumentsParam.Author;
            set
            {
                findDocumentsParam.Author = value;
                OnPropertyChanged();
            }
        }

        public bool SubjectEnabled
        {
            get => subjectEnabled;
            set => SetProperty(ref subjectEnabled, value);
        }

        public string Subject
        {
            get => findDocumentsParam.Subject;
            set
            {
                findDocumentsParam.Subject = value;
                OnPropertyChanged();
            }
        }

        public bool CategoryEnabled
        {
            get => categoryEnabled;
            set => SetProperty(ref categoryEnabled, value);
        }

        public string Category
        {
            get => findDocumentsParam.Category;
            set
            {
                findDocumentsParam.Category = value;
                OnPropertyChanged();
            }
        }

        public bool TaxYearEnabled
        {
            get => taxYearEnabled;
            set => SetProperty(ref taxYearEnabled, value);
        }

        public string TaxYear
        {
            get => findDocumentsParam.TaxYear;
            set
            {
                findDocumentsParam.TaxYear = value;
                OnPropertyChanged();
            }
        }

        public bool FindByDateAddedChecked
        {
            get => findDocumentsParam.FindByDateAddedChecked;
            set
            {
                findDocumentsParam.FindByDateAddedChecked = value;
                OnPropertyChanged();
                DateAddedEnabled = value;
            }
        }

        public bool DateAddedEnabled
        {
            get => dateAddedEnabled;
            set => SetProperty(ref dateAddedEnabled, value);
        }

        public string DateAdded
        {
            get => findDocumentsParam.DateAdded;
            set
            {
                findDocumentsParam.DateAdded = value;
                OnPropertyChanged();
            }
        }

        public bool FindFlaggedDocumentsChecked
        {
            get => findDocumentsParam.FindFlaggedDocumentsChecked;
            set
            {
                findDocumentsParam.FindFlaggedDocumentsChecked = value;
                OnPropertyChanged();
            }
        }

        public bool AllDocumentsEnabled
        {
            get => allDocumentsEnabled;
            set => SetProperty(ref allDocumentsEnabled, value);
        }

        public bool AllDocumentsChecked
        {
            get => findDocumentsParam.AllDocumentsChecked;
            set
            {
                findDocumentsParam.AllDocumentsChecked = value;
                OnPropertyChanged();
            }
        }
    }
}
