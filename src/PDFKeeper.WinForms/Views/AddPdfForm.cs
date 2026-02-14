// *****************************************************************************
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
// *****************************************************************************

using PDFKeeper.Core.Application;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    internal partial class AddPdfForm : Form
    {
        private readonly string pdfPath;
        private readonly AddPdfViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the AddPdfForm class, setting up the view model,
        /// data binding, help provider, and event handlers.
        /// </summary>
        /// <param name="pdfPath">
        /// The file path of the PDF to add, or null to specify no initial PDF.
        /// </param>
        /// <param name="document">
        /// The Document object to associate with the form, or null if not applicable.
        /// </param>
        public AddPdfForm(string pdfPath = null, Document document = null)
        {
            InitializeComponent();
            this.pdfPath = pdfPath;
            viewModel = new AddPdfViewModel(Handle, document);
            AddPdfViewModelBindingSource.DataSource = viewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            SetActions();
        }

        private void SetActions()
        {
            viewModel.OnApplyPendingChanges = () => AddPdfViewModelBindingSource.EndEdit();
            viewModel.OnResetBindings = () => AddPdfViewModelBindingSource.ResetBindings(false);

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
                TitleTextBox.Select();
            };

            viewModel.OnCloseView = () =>
            {
                viewModel.CancelViewClosing = false;
                Close();
            };

            viewModel.OnSelectTitleControl = () => TitleTextBox.Select();
        }

        private void AddPdfForm_Load(object sender, EventArgs e)
        {
            viewModel.SelectPdfCommand.Execute(pdfPath);
            Activate();
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            viewModel.ViewPdfCommand.Execute(null);
        }

        private void SetTitleToPdfFileNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewModel.SetTitleToPdfFileNameCommand.Execute(null);
        }

        private void SubjectUserControl_Enter(object sender, EventArgs e)
        {
            viewModel.GetSubjectsCommand.Execute(null);
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            viewModel.AddPdfCommand.Execute(DeleteSelectedPdfWhenAddedCheckBox.Checked);
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            viewModel.CancelCommand.Execute(null);
        }
        
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(viewModel.ViewText):
                    Text = viewModel.ViewText;
                    HelpProvider.SetHelpKeyword(
                        this,
                        "Replace PDF Document in a Database Record.html");
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

        private void AddPdfForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = viewModel.CancelViewClosing;
        }
    }
}
