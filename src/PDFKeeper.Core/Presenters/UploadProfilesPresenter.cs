// ****************************************************************************
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
// ****************************************************************************

using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.Core.Services;
using System.Linq;
using PDFKeeper.Core.Helpers;
using System;
using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Application;

namespace PDFKeeper.Core.Presenters
{
    public class UploadProfilesPresenter : PresenterBase<UploadProfilesViewModel>
    {
        private IDialogService dialogService;
        private IMessageBoxService messageBoxService;
        private readonly UploadProfileManager uploadProfileManager;

        public UploadProfilesPresenter()
        {
            GetServices(ServicesLocator.Services);
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
            dialogService.ShowDialog();
        }

        public void EditUploadProfile()
        {
            dialogService.ShowDialog(ViewModel.CurrentUploadProfileName);
        }

        public void DeleteUploadProfile()
        {
            var message = ResourceHelper.GetString(
                Resources.ResourceManager,
                "DeleteToRecycleBin",
                ViewModel.CurrentUploadProfileName);
            if (messageBoxService.ShowQuestion(message, false).Equals(6))
            {
                uploadProfileManager.DeleteUploadProfile(ViewModel.CurrentUploadProfileName);
            }
        }

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            foreach (var service in serviceProvider.GetServices<IDialogService>())
            {
                if (service.GetType().Name.Equals(
                    "UploadProfileEditorDialogService",
                    StringComparison.Ordinal))
                {
                    dialogService = service;
                }
            }

            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }
    }
}
