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

using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.Core.Services;
using System.Linq;
using PDFKeeper.Core.Helpers;

namespace PDFKeeper.Core.Presenters
{
    public class UploadProfilesPresenter : PresenterBase<UploadProfilesViewModel>
    {
        private readonly IMessageBoxService messageBoxService;
        private readonly IDialogService dialogService;
        private readonly UploadProfileManager uploadProfileManager;

        /// <summary>
        /// Initializes a new instance of the UploadProfilesPresenter class.
        /// </summary>
        /// <param name="messageBoxService">The MessageBoxService instance.</param>
        /// <param name="dialogService">The DialogService instance.</param>
        public UploadProfilesPresenter(IMessageBoxService messageBoxService,
            IDialogService dialogService)
        {
            this.messageBoxService = messageBoxService;
            this.dialogService = dialogService;
            uploadProfileManager = new UploadProfileManager();
            ViewModel = new UploadProfilesViewModel
            {
                UploadProfilesDirectoryPath = uploadProfileManager.UploadProfilesDirectoryPath
            };
            GetUploadProfileNames();
        }

        public void GetUploadProfileNames()
        {
            ViewModel.UploadProfileNames = uploadProfileManager.GetUploadProfileNames().ToArray();
            if (ViewModel.UploadProfileNames.Any())
            {
                ViewModel.EditEnabled = true;
                ViewModel.DeleteEnabled = true;
            }
            else
            {
                ViewModel.EditEnabled = false;
                ViewModel.DeleteEnabled = false;
            }
        }

        public void AddUploadProfile()
        {
            dialogService.ShowDialog(null);
        }

        public void EditUploadProfile()
        {
            dialogService.ShowDialog(ViewModel.CurrentUploadProfileName);
        }

        public void DeleteUploadProfile()
        {
            if (messageBoxService.ShowQuestion(ResourceHelper.GetString("DeleteToRecycleBin",
                ViewModel.CurrentUploadProfileName, null), false).Equals(6))
            {
                uploadProfileManager.DeleteUploadProfile(ViewModel.CurrentUploadProfileName);
            }
        }
    }
}
