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

using PDFKeeper.Core.Presenters;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Helpers;
using PDFKeeper.WinForms.Services;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    public partial class FindDocumentsForm : Form
    {
        private readonly FindDocumentsPresenter presenter;
        private readonly FindDocumentsViewModel viewModel;
        private RadioButton selectedRadioButton;

        public FindDocumentsForm()
        {
            InitializeComponent();

            presenter = new FindDocumentsPresenter(new MessageBoxService());
            viewModel = presenter.ViewModel;
            FindDocumentsViewModelBindingSource.DataSource = viewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            presenter.LongRunningOperationStarted += Presenter_LongRunningOperationStarted;
            presenter.LongRunningOperationFinished += Presenter_LongRunningOperationFinished;
            presenter.ApplyPendingChangesRequested += Presenter_ApplyPendingChangesRequested;
            presenter.ViewCloseCancelled += Presenter_ViewCloseCancelled;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void FindDocumentsForm_Load(object sender, EventArgs e)
        {
            presenter.ApplyParamObjectFromApplicationGlobal();
            if (FindBySearchTermRadioButton.Checked)
            {
                selectedRadioButton = FindBySearchTermRadioButton;
            }
            else if (FindBySelectionsRadioButton.Checked)
            {
                selectedRadioButton = FindBySelectionsRadioButton;
            }
            else if (FindByDateAddedRadioButton.Checked)
            {
                selectedRadioButton = FindByDateAddedRadioButton;
            }
            else if (FindFlaggedDocumentsRadioButton.Checked)
            {
                selectedRadioButton = FindFlaggedDocumentsRadioButton;
            }
            else if (AllDocumentsRadioButton.Checked)
            {
                selectedRadioButton = AllDocumentsRadioButton;
            }
            if (selectedRadioButton is null)
            {
                selectedRadioButton = FindBySearchTermRadioButton;
            }
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && !radioButton.Checked)
            {
                radioButton.Checked = !radioButton.Checked;
            }
            if (ReferenceEquals(
                radioButton,
                FindByDateAddedRadioButton) &
                FindByDateAddedRadioButton.Checked)
            {
                if (viewModel.DateAdded is null)
                {
                    viewModel.DateAdded = DateAddedDateTimePicker.Text;
                }
            }
        }

        private void SearchTermUserControl_Enter(object sender, EventArgs e)
        {
            presenter.GetSearchTermHistory();
        }

        private void AuthorDropDownListUserControl_Enter(object sender, EventArgs e)
        {
            presenter.GetAuthors();
        }

        private void SubjectDropDownListUserControl_Enter(object sender, EventArgs e)
        {
            presenter.GetSubjects();
        }

        private void CategoryDropDownListUserControl_Enter(object sender, EventArgs e)
        {
            presenter.GetCategories();
        }

        private void TaxYearDropDownListUserControl_Enter(object sender, EventArgs e)
        {
            presenter.GetTaxYears();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            presenter.FindDocuments();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            selectedRadioButton.Select();
            presenter.Cancel();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Presenter_LongRunningOperationStarted(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
        }

        private void Presenter_LongRunningOperationFinished(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void Presenter_ApplyPendingChangesRequested(object sender, EventArgs e)
        {
            FindDocumentsViewModelBindingSource.EndEdit();
        }

        private void Presenter_ViewCloseCancelled(object sender, EventArgs e)
        {
            SelectNextControl(this, true, true, true, true);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Authors", StringComparison.Ordinal))
            {
                AuthorDropDownListUserControl.Authors = viewModel.Authors;
            }
            else if (e.PropertyName.Equals("Subjects", StringComparison.Ordinal))
            {
                SubjectDropDownListUserControl.Subjects = viewModel.Subjects;
            }
            else if (e.PropertyName.Equals("Categories", StringComparison.Ordinal))
            {
                CategoryDropDownListUserControl.Categories = viewModel.Categories;
            }
            else if (e.PropertyName.Equals("TaxYears", StringComparison.Ordinal))
            {
                TaxYearDropDownListUserControl.TaxYears = viewModel.TaxYears;
            }
        }

        private void FindDocumentsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = presenter.CancelViewClosing;
        }
    }
}
