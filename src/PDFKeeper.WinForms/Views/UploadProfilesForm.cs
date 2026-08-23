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

using PDFKeeper.Core.Interfaces.HelpSystem;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Commands;
using System;
using System.IO;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    internal partial class UploadProfilesForm : Form
    {
        private readonly UploadProfilesViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProfilesForm"/> class.
        /// </summary>
        /// <param name="helpFileResolver">
        /// The <see cref="IHelpFileResolver"/> instance.
        /// </param>
        /// <param name="viewModel">
        /// The view model to be used by this form.
        /// The <see cref="UploadProfilesViewModel"/> instance.
        /// </param>
        public UploadProfilesForm(
            IHelpFileResolver helpFileResolver,
            UploadProfilesViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            UploadProfilesViewModelBindingSource.DataSource = viewModel;
            HelpProvider.HelpNamespace = helpFileResolver.GetHelpFilePath();
            UploadProfilesFileSystemWatcher.Path = viewModel.UploadProfilesDirectoryPath;
            viewModel.GetWindowHandle = () => Handle;
            SetTags();            
        }

        private void SetTags()
        {
            AddButton.Tag = viewModel.AddUploadProfileCommand;
            EditButton.Tag = viewModel.EditUploadProfileCommand;
            DeleteButton.Tag = viewModel.DeleteUploadProfileCommand;
        }
        
        private void UploadProfileNamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UploadProfileNamesListBox.Select();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            TagCommand.Invoke(sender);
        }

        private void UploadProfilesFileSystemWatcher_CreatedDeleted(
            object sender,
            FileSystemEventArgs e)
        {
            viewModel.GetUploadProfileNamesCommand.Execute(null);
        }

        private void UploadProfilesFileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            viewModel.GetUploadProfileNamesCommand.Execute(null);
        }
    }
}
