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
using PDFKeeper.Core.Presenters;
using PDFKeeper.Core.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    public partial class AddPdfForm : Form
    {
        private readonly AddPdfPresenter presenter;
        private readonly AddPdfViewModel viewModel;

        public AddPdfForm()
        {
            InitializeComponent();
            presenter = new AddPdfPresenter();
            viewModel = presenter.ViewModel;
            AddPdfViewModelBindingSource.DataSource = presenter.ViewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            SetActionDelegates();
        }

        private void SetActionDelegates()
        {
            presenter.OnApplyPendingChangesRequested = ()
                => AddPdfViewModelBindingSource.EndEdit();

            presenter.OnViewCloseCancelled = (() =>
            {
                presenter.CancelViewClosing = true;
                TitleTextBox.Select();
            });

            presenter.OnViewCloseRequested = (() =>
            {
                presenter.CancelViewClosing = false;
                Close();
            });
        }

        private void AddPdfForm_Load(object sender, EventArgs e)
        {
            presenter.SelectPdf();
            presenter.GetCollections();
            AddPdfViewModelBindingSource.ResetBindings(false);
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            presenter.ViewPdf();
        }

        private void SetTitleToPdfFileNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            presenter.SetTitleToPdfFileName();
            TitleTextBox.Select();
        }

        private void SubjectUserControl_Enter(object sender, EventArgs e)
        {
            presenter.GetSubjects();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            presenter.AddPdf(DeleteSelectedPdfWhenAddedCheckBox.Checked);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            presenter.Cancel();
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Authors", StringComparison.Ordinal))
            {
                AuthorUserControl.Authors = viewModel.Authors;
            }
            else if (e.PropertyName.Equals("Subjects", StringComparison.Ordinal))
            {
                SubjectUserControl.Subjects = viewModel.Subjects;
            }
            else if (e.PropertyName.Equals("Categories", StringComparison.Ordinal))
            {
                CategoryUserControl.Categories = viewModel.Categories;
            }
            else if (e.PropertyName.Equals("TaxYears", StringComparison.Ordinal))
            {
                TaxYearDropDownListUserControl.TaxYears = viewModel.TaxYears;
            }
        }

        private void AddPdfForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = presenter.CancelViewClosing;
        }
    }
}
