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
    public partial class UploadProfileEditorForm : Form
    {
        private readonly UploadProfileEditorPresenter presenter;
        private readonly UploadProfileEditorViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the UploadProfileEditorForm class.
        /// </summary>
        /// <param name="uploadProfileName">
        /// The upload profile name or null when editing a new upload profile.
        /// </param>
        public UploadProfileEditorForm(string uploadProfileName)
        {
            InitializeComponent();

            presenter = new UploadProfileEditorPresenter(
                uploadProfileName,
                new MessageBoxService());
            viewModel = presenter.ViewModel;
            UploadProfileEditorViewModelBindingSource.DataSource = presenter.ViewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            presenter.ApplyPendingChangesRequested += Presenter_ApplyPendingChangesRequested;
            presenter.ViewCloseCancelled += Presenter_ViewCloseCancelled;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void UploadProfileEditorForm_Load(object sender, EventArgs e)
        {
            presenter.GetCollections();
            UploadProfileEditorViewModelBindingSource.ResetBindings(false);
        }

        private void SubjectUserControl_Enter(object sender, EventArgs e)
        {
            presenter.GetSubjects();
        }

        private void SetNameToAuthorSubjectLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            presenter.SetNameToAuthorAndSubject();
        }

        private void UploadOptionsUserControl_Leave(object sender, EventArgs e)
        {
            Presenter_ApplyPendingChangesRequested(this, null);
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            presenter.SaveUploadProfile();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            presenter.Cancel();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Presenter_ApplyPendingChangesRequested(object sender,EventArgs e)
        {
            UploadProfileEditorViewModelBindingSource.EndEdit();
        }

        private void Presenter_ViewCloseCancelled(object sender, EventArgs e)
        {
            NameUserControl.NameTextBox.Select();
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("TitleTokens", StringComparison.Ordinal))
            {
                TitleUserControl.TitleTokens = viewModel.TitleTokens;
            }
            else if (e.PropertyName.Equals("Authors", StringComparison.Ordinal))
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

        private void UploadProfileEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = presenter.CancelViewClosing;
        }
    }
}
