// ****************************************************************************
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
// ****************************************************************************

using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PDFKeeper.Core.ViewModels
{
    /// <summary>
    /// View model for managing upload profiles, providing functionality to add, edit, and delete.
    /// </summary>
    [CLSCompliant(false)]
    public sealed class UploadProfilesViewModel : ViewModelBase
    {
        private readonly IWindowHandleProvider windowHandleProvider;
        private IDialogService dialogService;
        private IMessageBoxService messageBoxService;
        private readonly UploadProfileManager uploadProfileManager;
        private IEnumerable<string> uploadProfileNames;
        private bool editEnabled;
        private bool deleteEnabled;

        /// <summary>
        /// Initializes a new instance of the UploadProfilesViewModel class with the specified
        /// window handle provider.
        /// </summary>
        /// <param name="windowHandleProvider">
        /// An object that provides a handle to the window associated with this view model.
        /// </param>
        public UploadProfilesViewModel(IWindowHandleProvider windowHandleProvider)
        {
            this.windowHandleProvider = windowHandleProvider;
            GetServices(ServiceLocator.Services);
            uploadProfileManager = new UploadProfileManager();
            UploadProfilesDirectoryPath = uploadProfileManager.UploadProfilesDirectoryPath;
            InitializeCommands();
            GetUploadProfileNames();
        }

        public ICommand GetUploadProfileNamesCommand { get; private set; }
        public ICommand AddUploadProfileCommand { get; private set; }
        public ICommand EditUploadProfileCommand { get; private set; }
        public ICommand DeleteUploadProfileCommand { get; private set; }
        public string UploadProfilesDirectoryPath { get; set; }

        public IEnumerable<string> UploadProfileNames
        {
            get => uploadProfileNames;
            set => SetProperty(ref uploadProfileNames, value);
        }

        public string CurrentUploadProfileName { get; set; }

        public bool EditEnabled
        {
            get => editEnabled;
            set => SetProperty(ref editEnabled, value);
        }

        public bool DeleteEnabled
        {
            get => deleteEnabled;
            set => SetProperty(ref deleteEnabled, value);
        }
        
        protected override void GetServices(IServiceProvider serviceProvider)
        {
            foreach (var service in serviceProvider.GetServices<IDialogService>())
            {
                switch (service.GetType().Name)
                {
                    case "UploadProfileEditorDialogService":
                        dialogService = service;
                        break;
                }
            }

            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }

        private void InitializeCommands()
        {
            GetUploadProfileNamesCommand = new RelayCommand(GetUploadProfileNames);
            AddUploadProfileCommand = new RelayCommand(AddUploadProfile);
            EditUploadProfileCommand = new RelayCommand(EditUploadProfile);
            DeleteUploadProfileCommand = new RelayCommand(DeleteUploadProfile);
        }

        private void GetUploadProfileNames()
        {
            UploadProfileNames = [.. uploadProfileManager.GetUploadProfileNames()];
            if (UploadProfileNames.Any())
            {
                EditEnabled = true;
                DeleteEnabled = true;
            }
            else
            {
                EditEnabled = false;
                DeleteEnabled = false;
            }
        }

        private void AddUploadProfile() => dialogService.ShowDialog(
            windowHandleProvider.GetHandle());
        private void EditUploadProfile() => dialogService.ShowDialog(
            windowHandleProvider.GetHandle(),
            CurrentUploadProfileName);

        private void DeleteUploadProfile()
        {
            var message = ResourceHelper.GetString(
                Resources.ResourceManager,
                "DeleteToRecycleBin",
                CurrentUploadProfileName);

            if (messageBoxService.ShowQuestion(windowHandleProvider.GetHandle(), message) == 6)
            {
                uploadProfileManager.DeleteUploadProfile(CurrentUploadProfileName);
            }
        }
    }
}
