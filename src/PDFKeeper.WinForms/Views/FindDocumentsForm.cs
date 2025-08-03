// *****************************************************************************
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
// *****************************************************************************

using PDFKeeper.Core.Application;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Commands;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    internal partial class FindDocumentsForm : Form
    {
        private readonly FindDocumentsViewModel viewModel;
        private RadioButton selectedRadioButtonOnOpen;

        public FindDocumentsForm()
        {
            InitializeComponent();
            viewModel = new FindDocumentsViewModel();
            FindDocumentsViewModelBindingSource.DataSource = viewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            SetActions();
            SetTags();
        }

        private void SetActions()
        {
            viewModel.OnLongOperationStarted = () => Cursor = Cursors.WaitCursor;
            viewModel.OnLongOperationFinished = () => Cursor = Cursors.Default;
            viewModel.OnApplyPendingChanges = () => FindDocumentsViewModelBindingSource.EndEdit();

            viewModel.OnCloseViewOKResult = () =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnCloseViewCancelResult = () =>
            {
                selectedRadioButtonOnOpen.Select();
                DialogResult = DialogResult.Cancel;
                Close();
            };

            viewModel.OnCancelCloseView = () =>
            {
                viewModel.CancelViewClosing = true;
                SelectNextControl(this, true, true, true, true);
            };

            viewModel.OnRelaySelectedFindAction = () =>
            {
                switch (viewModel.FindActionSelected)
                {
                    case FindDocumentsViewModel.FindAction.FindBySearchTerm:
                        selectedRadioButtonOnOpen = FindBySearchTermRadioButton;
                        break;
                    case FindDocumentsViewModel.FindAction.FindBySelections:
                        selectedRadioButtonOnOpen = FindBySelectionsRadioButton;
                        break;
                    case FindDocumentsViewModel.FindAction.FindByDateAdded:
                        selectedRadioButtonOnOpen = FindByDateAddedRadioButton;
                        break;
                    case FindDocumentsViewModel.FindAction.FindFlaggedDocuments:
                        selectedRadioButtonOnOpen = FindFlaggedDocumentsRadioButton;
                        break;
                    case FindDocumentsViewModel.FindAction.AllDocuments:
                        selectedRadioButtonOnOpen = AllDocumentsRadioButton;
                        break;
                }

                viewModel.DateAdded = DateAddedDateTimePicker.Text;
            };
        }

        private void SetTags()
        {
            SearchTermUserControl.Tag = viewModel.GetSearchTermHistoryCommand;
            AuthorDropDownListUserControl.Tag = viewModel.GetAuthorsCommand;
            SubjectDropDownListUserControl.Tag = viewModel.GetSubjectsCommand;
            CategoryDropDownListUserControl.Tag = viewModel.GetCategoriesCommand;
            TaxYearDropDownListUserControl.Tag = viewModel.GetTaxYearsCommand;
            OK_Button.Tag = viewModel.FindDocumentsCommand;
            Cancel_Button.Tag = viewModel.CancelCommand;
        }

        private void FindDocumentsForm_Load(object sender, EventArgs e)
        {
            viewModel.ApplyFindDocumentsParamObjectCommand.Execute(null);
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            ((RadioButton)sender).Checked = true;
        }

        private void ClearSelectionsButton_Click(object sender, EventArgs e)
        {
            viewModel.ClearSelectionsCommand.Execute(null);
        }

        private void UserControl_Enter(object sender, EventArgs e)
        {
            TagCommand.Invoke(sender);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            TagCommand.Invoke(sender);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(viewModel.Authors):
                    AuthorDropDownListUserControl.Authors = viewModel.Authors;
                    break;
                case nameof(viewModel.Subjects):
                    SubjectDropDownListUserControl.Subjects = viewModel.Subjects;
                    break;
                case nameof(viewModel.Categories):
                    CategoryDropDownListUserControl.Categories = viewModel.Categories;
                    break;
                case nameof(viewModel.TaxYears):
                    TaxYearDropDownListUserControl.TaxYears = viewModel.TaxYears;
                    break;
            }
        }

        private void FindDocumentsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = viewModel.CancelViewClosing;
        }
    }
}
