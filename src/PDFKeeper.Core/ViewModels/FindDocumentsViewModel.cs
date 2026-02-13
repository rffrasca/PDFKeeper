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

using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Rules;
using PDFKeeper.Core.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace PDFKeeper.Core.ViewModels
{
    [CLSCompliant(false)]
    public class FindDocumentsViewModel : ColumnDataListsViewModel, IFindDocumentsParam
    {
        private IMessageBoxService messageBoxService;
        private FindDocumentsParam findDocumentsParam;
        private readonly SearchTermHistory searchTermHistory;
        private bool searchTermEnabled;
        private IEnumerable<string> searchTerms;
        private bool clearSelectionsEnabled;
        private bool authorEnabled;
        private bool subjectEnabled;
        private bool categoryEnabled;
        private bool taxYearEnabled;
        private bool dateAddedEnabled;
        private bool allDocumentsEnabled;

        public enum FindAction
        {
            FindBySearchTerm,
            FindBySelections,
            FindByDateAdded,
            FindFlaggedDocuments,
            AllDocuments
        }

        public FindDocumentsViewModel()
        {
            GetServices(ServiceLocator.Services);
            findDocumentsParam = new FindDocumentsParam();
            searchTermHistory = new SearchTermHistory();
            ApplyPolicy();
            InitializeCommands();
        }

        public Action OnRelaySelectedFindAction { get; set; }
        public ICommand ApplyFindDocumentsParamObjectCommand { get; private set; }
        public ICommand GetSearchTermHistoryCommand { get; private set; }
        public ICommand ClearSelectionsCommand { get; private set; }
        public ICommand GetAuthorsCommand { get; private set; }
        public ICommand GetSubjectsCommand { get; private set; }
        public ICommand GetCategoriesCommand { get; private set; }
        public ICommand GetTaxYearsCommand { get; private set; }
        public ICommand FindDocumentsCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        
        public FindDocumentsParam FindDocumentsParam
        {
            get => findDocumentsParam;
            set
            {
                findDocumentsParam = value;
                OnPropertyChanged(nameof(FindBySearchTermChecked));
                OnPropertyChanged(nameof(SearchTerm));
                OnPropertyChanged(nameof(FindBySelectionsChecked));
                OnPropertyChanged(nameof(Author));
                OnPropertyChanged(nameof(Subject));
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(TaxYear));
                OnPropertyChanged(nameof(FindByDateAddedChecked));
                OnPropertyChanged(nameof(DateAdded));
                OnPropertyChanged(nameof(FindFlaggedDocumentsChecked));
                OnPropertyChanged(nameof(AllDocumentsChecked));
                SetFindActionSelection();
            }
        }

        public FindAction FindActionSelected { get; private set; }

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
                ClearSelectionsEnabled = value;
                AuthorEnabled = value;
                SubjectEnabled = value;
                CategoryEnabled = value;
                TaxYearEnabled = value;
            }
        }

        public bool ClearSelectionsEnabled
        {
            get => clearSelectionsEnabled;
            set => SetProperty(ref clearSelectionsEnabled, value);
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

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }

        private void ApplyPolicy() =>
            AllDocumentsEnabled = !ApplicationPolicy.GetPolicyValue(
                ApplicationPolicy.PolicyName.HideAllDocuments);

        private void InitializeCommands()
        {
            ApplyFindDocumentsParamObjectCommand = new RelayCommand(ApplyFindDocumentsParamObject);
            GetSearchTermHistoryCommand = new RelayCommand(GetSearchTermHistory);
            ClearSelectionsCommand = new RelayCommand(ClearSelections);
            GetAuthorsCommand = new RelayCommand(GetAuthors);
            GetSubjectsCommand = new RelayCommand(GetSubjects);
            GetCategoriesCommand = new RelayCommand(GetCategories);
            GetTaxYearsCommand = new RelayCommand(GetTaxYears);
            FindDocumentsCommand = new RelayCommand(FindDocuments);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void ApplyFindDocumentsParamObject()
        {
            if (FindDocumentsViewState.FindDocumentsParam != null)
            {
                FindDocumentsParam = FindDocumentsViewState.FindDocumentsParam.Clone();
            }
            else
            {
                SetFindActionSelection();
            }
        }

        private void GetSearchTermHistory()
        {
            var entry = SearchTerm;
            SearchTerms = searchTermHistory.GetSearchTerms();
            SearchTerm = entry;
        }

        private void ClearSelections()
        {
            Author = null;
            Subject = null;
            Category = null;
            TaxYear = null;
        }

        private void GetAuthors()
        {
            try
            {
                OnLongOperationStarted?.Invoke();
                var selection = Author;
                Authors = ColumnData.GetAuthors(Subject, Category, TaxYear);
                Author = selection;
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongOperationFinished?.Invoke();
            }
        }

        private void GetSubjects()
        {
            try
            {
                OnLongOperationStarted?.Invoke();
                var selection = Subject;
                Subjects = ColumnData.GetSubjects(Author, Category, TaxYear);
                Subject = selection;
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongOperationFinished?.Invoke();
            }
        }

        private void GetCategories()
        {
            try
            {
                OnLongOperationStarted?.Invoke();
                var selection = Category;
                Categories = ColumnData.GetCategories(Author, Subject, TaxYear);
                Category = selection;
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongOperationFinished?.Invoke();
            }
        }

        private void GetTaxYears()
        {
            try
            {
                OnLongOperationStarted?.Invoke();
                var selection = TaxYear;
                TaxYears = ColumnData.GetTaxYears(Author, Subject, Category);
                TaxYear = selection;
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongOperationFinished?.Invoke();
            }
        }

        private void FindDocuments()
        {
            CancelViewClosing = false;
            OnApplyPendingChanges?.Invoke();

            var rule = new FindDocumentsParamRule(FindDocumentsParam);
            if (!rule.ViolationFound)
            {
                FindDocumentsViewState.FindDocumentsParam = FindDocumentsParam.Clone();

                if (FindBySearchTermChecked)
                {
                    searchTermHistory.Add(SearchTerm);
                }

                OnCloseViewOKResult?.Invoke();
            }
            else
            {
                messageBoxService.ShowMessage(rule.ViolationMessage, true);
                OnCancelCloseView?.Invoke();
            }
        }

        private void Cancel()
        {
            CancelViewClosing = false;
            OnCloseViewCancelResult?.Invoke();
        }

        private void SetFindActionSelection()
        {
            if (FindBySearchTermChecked)
            {
                FindActionSelected = FindAction.FindBySearchTerm;
            }
            else if (FindBySelectionsChecked)
            {
                FindActionSelected = FindAction.FindBySelections;
            }
            else if (FindByDateAddedChecked)
            {
                FindActionSelected = FindAction.FindByDateAdded;
            }
            else if (FindFlaggedDocumentsChecked)
            {
                FindActionSelected = FindAction.FindFlaggedDocuments;
            }
            else if (AllDocumentsChecked)
            {
                FindActionSelected = FindAction.AllDocuments;
            }
            else
            {
                FindActionSelected = FindAction.FindBySearchTerm;
            }

            OnRelaySelectedFindAction?.Invoke();
        }
    }
}
