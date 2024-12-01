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
using PDFKeeper.WinForms.Helpers;
using PDFKeeper.WinForms.Services;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    public partial class UploadProfilesForm : Form
    {
        private readonly UploadProfilesPresenter presenter;

        public UploadProfilesForm()
        {
            InitializeComponent();

            presenter = new UploadProfilesPresenter(
                new MessageBoxService(),
                new UploadProfileEditorDialogService<string>());
            var viewModel = presenter.ViewModel;
            UploadProfilesViewModelBindingSource.DataSource = viewModel;
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            UploadProfilesFileSystemWatcher.Path = viewModel.UploadProfilesDirectoryPath;
        }

        private void UploadProfileNamesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UploadProfileNamesListBox.Select();
        }

        private void AddButton_Click(object sender, System.EventArgs e)
        {
            presenter.AddUploadProfile();
        }

        private void EditButton_Click(object sender, System.EventArgs e)
        {
            presenter.EditUploadProfile();
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            presenter.DeleteUploadProfile();
        }

        private void UploadProfilesFileSystemWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            presenter.GetUploadProfileNames();
        }

        private void UploadProfilesFileSystemWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            presenter.GetUploadProfileNames();
        }

        private void UploadProfilesFileSystemWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            presenter.GetUploadProfileNames();
        }
    }
}
