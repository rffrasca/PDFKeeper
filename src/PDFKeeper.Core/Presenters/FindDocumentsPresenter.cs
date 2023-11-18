// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2023 Robert F. Frasca
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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Rules;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.ViewModels;
using System.Linq;

namespace PDFKeeper.Core.Presenters
{
    public class FindDocumentsPresenter : PresenterBase<FindDocumentsViewModel>
    {
        private readonly IMessageBoxService messageBoxService;
        private readonly SearchTermHistory searchTermHistory;

        /// <summary>
        /// Initializes a new instance of the FindDocumentsPresenter class.
        /// </summary>
        /// <param name="messageBoxService">The MessageBoxService instance.</param>
        public FindDocumentsPresenter(IMessageBoxService messageBoxService)
        {
            this.messageBoxService = messageBoxService;
            ViewModel = new FindDocumentsViewModel();
            searchTermHistory = new SearchTermHistory();
            ApplyPolicy();
        }

        public void ApplyParamObjectFromApplicationGlobal()
        {
            if (FindDocumentsViewState.FindDocumentsParam != null)
            {
                ViewModel.FindDocumentsParam = FindDocumentsViewState.FindDocumentsParam.Clone();
            }
        }

        public void GetSearchTermHistory()
        {
            try
            {
                var entry = ViewModel.SearchTerm;
                ViewModel.SearchTerms = searchTermHistory.SearchTerms.ToArray();
                ViewModel.SearchTerm = entry;
            }
            catch (DatabaseException ex)
            {
                this.messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        public void GetAuthors()
        {
            try
            {
                OnLongRunningOperationStarted();
                var selection = ViewModel.Author;
                ViewModel.Authors = ColumnData.GetAuthors(ViewModel.Subject, ViewModel.Category,
                    ViewModel.TaxYear).OrderBy(author => author).ToArray();
                ViewModel.Author = selection;
            }
            catch (DatabaseException ex)
            {
                this.messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongRunningOperationFinished();
            }
        }

        public void GetSubjects()
        {
            try
            {
                OnLongRunningOperationStarted();
                var selection = ViewModel.Subject;
                ViewModel.Subjects = ColumnData.GetSubjects(ViewModel.Author, ViewModel.Category,
                    ViewModel.TaxYear).OrderBy(subject => subject).ToArray();
                ViewModel.Subject = selection;
            }
            catch (DatabaseException ex)
            {
                this.messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongRunningOperationFinished();
            }
        }

        public void GetCategories()
        {
            try
            {
                OnLongRunningOperationStarted();
                var selection = ViewModel.Category;
                ViewModel.Categories = ColumnData.GetCategories(ViewModel.Author, ViewModel.Subject,
                    ViewModel.TaxYear).OrderBy(category => category).ToArray();
                ViewModel.Category = selection;
            }
            catch (DatabaseException ex)
            {
                this.messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongRunningOperationFinished();
            }
        }

        public void GetTaxYears()
        {
            try
            {
                OnLongRunningOperationStarted();
                var selection = ViewModel.TaxYear;
                ViewModel.TaxYears = ColumnData.GetTaxYears(ViewModel.Author, ViewModel.Subject,
                    ViewModel.Category).OrderBy(taxYear => taxYear).ToArray();
                ViewModel.TaxYear = selection;
            }
            catch (DatabaseException ex)
            {
                this.messageBoxService.ShowMessage(ex.Message, true);
            }
            finally
            {
                OnLongRunningOperationFinished();
            }
        }

        public void FindDocuments()
        {            
            CancelViewClosing = false;
            OnApplyPendingChangesRequested();
            var rule = new FindDocumentsParamRule(ViewModel.FindDocumentsParam);
            if (!rule.ViolationFound)
            {
                FindDocumentsViewState.FindDocumentsParam = ViewModel.FindDocumentsParam.Clone();
                if (ViewModel.FindBySearchTermChecked)
                {
                    searchTermHistory.Add(ViewModel.SearchTerm);
                }
            }
            else
            {
                messageBoxService.ShowMessage(rule.ViolationMessage, true);
                OnViewCloseCancelled();
            }
        }

        public void Cancel()
        {
            CancelViewClosing = false;
        }

        private void ApplyPolicy()
        {
            if (ApplicationPolicy.GetPolicyValue(ApplicationPolicy.PolicyName.HideAllDocuments))
            {
                ViewModel.AllDocumentsEnabled = false;
            }
            else
            {
                ViewModel.AllDocumentsEnabled = true;
            }
        }
    }
}
