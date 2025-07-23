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
    public partial class UploadProfileEditorForm : Form
    {
        private readonly UploadProfileEditorViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProfileEditorForm"/> class.
        /// </summary>
        /// <param name="uploadProfileName">
        /// The upload profile name only when editing an existing upload profile.
        /// </param>
        public UploadProfileEditorForm(string uploadProfileName = null)
        {
            InitializeComponent();
            viewModel = new UploadProfileEditorViewModel(uploadProfileName);
            UploadProfileEditorViewModelBindingSource.DataSource = viewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            SetActions();
            SetTags();
        }

        private void SetActions()
        {
            viewModel.OnApplyPendingChanges = ()
                => UploadProfileEditorViewModelBindingSource.EndEdit();
            viewModel.OnResetBindings = ()
                => UploadProfileEditorViewModelBindingSource.ResetBindings(false);

            viewModel.OnCloseViewOKResult = () =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            viewModel.OnCloseViewCancelResult = () =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            viewModel.OnCancelCloseView = () =>
            {
                viewModel.CancelViewClosing = true;
                NameUserControl.NameTextBox.Select();
            };
        }

        private void SetTags()
        {
            OK_Button.Tag = viewModel.SaveUploadProfileCommand;
            Cancel_Button.Tag = viewModel.CancelCommand;
        }

        private void UploadProfileEditorForm_Load(object sender, EventArgs e)
        {
            viewModel.GetCollectionsCommand.Execute(null);
        }

        private void SubjectUserControl_Enter(object sender, EventArgs e)
        {
            viewModel.GetSubjectsCommand.Execute(null);
        }

        private void SetNameToAuthorSubjectLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewModel.SetNameToAuthorAndSubjectCommand.Execute(null);
        }

        private void UploadOptionsUserControl_Leave(object sender, EventArgs e)
        {
            UploadProfileEditorViewModelBindingSource.EndEdit();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            TagCommand.Invoke(sender);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(viewModel.TitleTokens):
                    TitleUserControl.TitleTokens = viewModel.TitleTokens;
                    break;
                case nameof(viewModel.Authors):
                    AuthorUserControl.Authors = viewModel.Authors;
                    break;
                case nameof(viewModel.Subjects):
                    SubjectUserControl.Subjects = viewModel.Subjects;
                    break;
                case nameof(viewModel.Categories):
                    CategoryUserControl.Categories = viewModel.Categories;
                    break;
                case nameof(viewModel.TaxYears):
                    TaxYearDropDownListUserControl.TaxYears = viewModel.TaxYears;
                    break;
            }
        }

        private void UploadProfileEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = viewModel.CancelViewClosing;
        }
    }
}
